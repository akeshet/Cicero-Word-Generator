#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;


namespace DataStructures.Timing
{
    public class NetworkClockProvider : SoftwareClockProvider
    {
        private static object staticLockObj = new object();
        private object instanceLockObj = new object();

        private bool receivedFirstClock = false;

        private uint clockID;
        
        private NetworkClockProvider() { }

        private static uint lastNGramID = uint.MaxValue;

        public  NetworkClockProvider(uint clockID)
        {
            this.clockID = clockID;

            lock (staticLockObj)
            {
                if (listenerThread == null)
                    throw new SoftwareClockProviderException("Attempted to create a NetworkClockProvider before starting the network clock listener.");

                if (providers.ContainsKey(clockID))
                    throw new SoftwareClockProviderException("Attempted to create a duplicate NetworkClockProvider with ID.");

                providers.Add(clockID, this);
            }
        }

        private static EventHandler<MessageEvent> staticMessageLogHandler;

        private static NetworkClockProvider dummy = new NetworkClockProvider();

        private static void staticLogMessage(MessageEvent e)
        {
            lock (staticLockObj)
                if (staticMessageLogHandler != null)
                    staticMessageLogHandler.BeginInvoke(dummy, e, null, null);
        }

        private static Thread listenerThread;



        

        private static Dictionary<uint, NetworkClockProvider> providers = new Dictionary<uint,NetworkClockProvider>();
        private static NetworkClockEndpointInfo.HostTypes myListenerType;

        /// <summary>
        /// Returns true if successful, false otherwise.
        /// </summary>
        /// <param name="listenerType"></param>
        /// <returns></returns>
        public static bool startListener(NetworkClockEndpointInfo.HostTypes listenerType) {
            lock (staticLockObj)
            {
                staticLogMessage(new MessageEvent("Starting network clock listener...", 0, MessageEvent.MessageTypes.Routine, MessageEvent.MessageCategories.SoftwareClock));
                myListenerType = listenerType;

                // Check if a listener thread is running already.
                if (listenerThread != null)
                {
                    throw new SoftwareClockProviderException("Attempted to start listener thread when already started.");
                }


                // Open a UDP socket
                staticLogMessage(new MessageEvent("Opening UDP input socket for network clock.", 1, MessageEvent.MessageTypes.Log, MessageEvent.MessageCategories.Networking));
                try
                {
                    udpClient = new UdpClient(NetworkClockEndpointInfo.getListenerPort(myListenerType));
                }
                catch (System.Net.Sockets.SocketException e)
                {
                    staticLogMessage(new MessageEvent("Unable to open input socket for network clock due to socket exception: " + e.Message,
                                                        0, MessageEvent.MessageTypes.Error, MessageEvent.MessageCategories.Networking));
                    udpClient = null;
                    return false;
                }
               


                listenerThread = new Thread(listenerThreadProc);
                listenerThread.Start();
                providers = new Dictionary<uint, NetworkClockProvider>();
                
            }
            return true;
        }

        public static void shutDown()
        {
            lock (staticLockObj)
            {
                staticLogMessage(new MessageEvent("Shutting down network clock listener.", 0, MessageEvent.MessageTypes.Routine, MessageEvent.MessageCategories.SoftwareClock));
                if (listenerThread == null)
                {
                    //throw new SoftwareClockProviderException("Attempted to stop a listener that was not running.");
                    return;
                }
                listenerThread.Abort();
                listenerThread = null;

                if (udpClient != null)
                {
                    udpClient.Close();
                    udpClient = null;
                }

                List<uint> providerIDs = providers.Keys.ToList();
                foreach (uint id in providerIDs)
                {
                    providers[id].AbortClockProvider();
                }

                providers = null;
            }
        }

        public static bool listenerRunning()
        {
            lock (staticLockObj)
                return (listenerThread != null);
        }

        public static void registerStaticMessageLogHandler(EventHandler<MessageEvent> handler)
        {
            lock (staticLockObj)
                staticMessageLogHandler += handler;
        }

        private static UdpClient udpClient;



        private static void listenerThreadProc()
        {
             staticLogMessage(new MessageEvent("Network Clock listener thread is alive", 1, MessageEvent.MessageTypes.Log, MessageEvent.MessageCategories.Networking));


            while (true)
            {
                // Wait for a datagram from the udpClient.
                // This makes use of a temporary local reference to udpClient so that we can be sure
                // that even if the static udpClient member is changed when we don't hold the static lock
                // we will at least run the callback on the correct instance of udpClient.
                // (I didn't want to lock the receive callback on staticLockObj, because the callback may
                // take arbitrarily long to return, and I don't want to be locked out of other static activities like
                // shutting down the listener.

                System.Net.IPEndPoint remoteEnd = null;
                IAsyncResult result;
                byte[] received;
                UdpClient udpClientNonstatic;
                lock (staticLockObj)
                {
                    udpClientNonstatic = udpClient;
                    result = udpClient.BeginReceive(null, null);
                }
                received = udpClientNonstatic.EndReceive(result, ref remoteEnd);

                if (received.Length != NetworkClockDatagram.datagramByteLength)
                {
                    staticLogMessage(new MessageEvent("Received wrong sized (" + received.Length + ") datagram. Dropping.", 1, MessageEvent.MessageTypes.Error, MessageEvent.MessageCategories.Networking));
                    continue;
                }

                NetworkClockDatagram ndgram = new NetworkClockDatagram(received);

#if DEBUG
                staticLogMessage(new MessageEvent("Received network clock datagram " + ndgram.ToString(), 2, MessageEvent.MessageTypes.Debug, MessageEvent.MessageCategories.Networking));
#endif
                if (ndgram.DatagramCount != lastNGramID + 1)
                {
                    staticLogMessage(new MessageEvent("Warning! Received network clock datagram #" + ndgram.DatagramCount + ", expected #" + (lastNGramID + 1) + ".", 0, MessageEvent.MessageTypes.Warning, MessageEvent.MessageCategories.Networking));
                }
                lastNGramID = ndgram.DatagramCount;

                NetworkClockProvider pr = null;

                // Pull designated provider out of provider's dictionary (locked on staticLockObj since
                // we are accessing static providers dictionary)
                // Will leave pr as null if no such ClockID listener exists.
                // Will throw an exception if clockID does exist, but listener is null.
                lock (staticLockObj)
                {
                    if (providers.ContainsKey(ndgram.ClockID))
                    {
                        pr = providers[ndgram.ClockID];
                        if (pr == null)
                            throw new SoftwareClockProviderException("Unexpected null network clock provider.");
                    }
                }

                // If provider does exist, (is not null), relay to it the latest
                // clock message
                if (pr != null)
                {
                    // Thread lock on pr object
                    lock (pr.instanceLockObj)
                    {
                        if (pr.isRunning)
                        {
                            if (!pr.receivedFirstClock)
                            {
                                staticLogMessage(new MessageEvent("Received first clock datagram for clockID " + Shared.clockIDToString(pr.clockID),
                                    0, MessageEvent.MessageTypes.Routine, MessageEvent.MessageCategories.SoftwareClock));
                                pr.receivedFirstClock = true;
                            }
                            pr.reachTime(ndgram.ElaspedTime);
                        }
                    }
                }   
            }
        }

        protected override void cleanupClockProvider()
        {
            lock (instanceLockObj)
            {
                isRunning = false;
            }

            lock (staticLockObj)
            {
                providers.Remove(clockID);
            }
        }

        protected override void armClockProvider()
        {
            // nothing to do
        }

        protected override void startClockProvider()
        {
            lock (instanceLockObj)
                isRunning = true;
        }

        private bool isRunning = false;


    }
}
