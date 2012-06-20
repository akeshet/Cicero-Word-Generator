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
        private static object lockObj = new object();

        private uint clockID;

        private NetworkClockProvider() { }

        public  NetworkClockProvider(uint clockID) : base()
        {
            this.clockID = clockID;

            lock (lockObj)
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
            if (staticMessageLogHandler != null)
                staticMessageLogHandler(dummy, e);
        }

        private static Thread listenerThread;

        public static readonly int networkClockDestinationPort = 39721;

        

        private static Dictionary<uint, NetworkClockProvider> providers;
        

        public static void startListener() {
            lock (lockObj)
            {
                staticLogMessage(new MessageEvent("Starting network clock listener...", 0, MessageEvent.MessageTypes.Routine, MessageEvent.MessageCategories.SoftwareClock));
                if (listenerThread != null)
                {
                    throw new SoftwareClockProviderException("Attempted to start listener thread when already started.");
                }
                listenerThread = new Thread(listenerThreadProc);
                listenerThread.Start();
                providers = new Dictionary<uint, NetworkClockProvider>();
                staticLogMessage(new MessageEvent("...done", 0, MessageEvent.MessageTypes.Routine, MessageEvent.MessageCategories.SoftwareClock));
            }
        }

        public static void shutDown()
        {
            lock (lockObj)
            {
                staticLogMessage(new MessageEvent("Shutting down network clock listener.", 0, MessageEvent.MessageTypes.Routine, MessageEvent.MessageCategories.SoftwareClock));
                if (listenerThread == null)
                {
                    throw new SoftwareClockProviderException("Attempted to stop a listener that was not running.");
                }
                listenerThread.Abort();
                listenerThread = null;

                lock (providers)
                {
                    List<uint> providerIDs = providers.Keys.ToList();
                    foreach (uint id in providerIDs)
                    {
                        providers[id].Abort();
                    }

                    providers = null;
                }
                staticLogMessage(new MessageEvent("...done"));
            }
        }

        public static bool listenerRunning()
        {
            return (listenerThread != null);
        }

        public static void registerStaticMessageLogHandler(EventHandler<MessageEvent> handler)
        {
            staticMessageLogHandler += handler;
        }

        private static UdpClient udpClient;

        private static void listenerThreadProc()
        {
            
                staticLogMessage(new MessageEvent("Starting Network Clock listener thread...", 1, MessageEvent.MessageTypes.Log, MessageEvent.MessageCategories.Networking));


            udpClient = new UdpClient(networkClockDestinationPort);

            
            staticLogMessage( new MessageEvent("...done", 1, MessageEvent.MessageTypes.Log, MessageEvent.MessageCategories.Networking));

            while (true)
            {
                System.Net.IPEndPoint remoteEnd = null;
                //byte[] received = udpClient.Receive(ref remoteEnd); // does not abort when thread aborts
                //byte[] received = udpClient.b
                IAsyncResult result = udpClient.BeginReceive(null, null);
                byte[] received = udpClient.EndReceive(result, ref remoteEnd);

                if (received.Length != NetworkClockDatagram.datagramByteLength)
                {
                    staticLogMessage(new MessageEvent("Received wrong sized (" + received.Length + ") datagram. Dropping.", 1, MessageEvent.MessageTypes.Error, MessageEvent.MessageCategories.Networking));
                    continue;
                }
                NetworkClockDatagram ndgram = new NetworkClockDatagram(received);

#if DEBUG
                staticLogMessage(new MessageEvent("Received network clock datagram " + ndgram.ToString(), 2, MessageEvent.MessageTypes.Debug, MessageEvent.MessageCategories.Networking));
#endif

                lock (providers)
                {
                    if (providers.ContainsKey(ndgram.ClockID))
                    {
                        if (providers[ndgram.ClockID].isRunning)
                        {
                            providers[ndgram.ClockID].reachTime(ndgram.ElaspedTime);
                        }
                    }
                }
            }


        }

        protected override void abortTimer()
        {
            isRunning = false;
            providers.Remove(clockID);
        }

        protected override void armTimer()
        {
            // nothing to do
        }

        protected override void startTimer()
        {
            // nothing to do
        }

        private bool isRunning = false;

        public override void Start()
        {
            isRunning = true;
        }
    }
}
