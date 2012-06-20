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

        private static event EventHandler<MessageEvent> messageLog;

        private static Thread listenerThread;

        public static readonly int networkClockDestinationPort = 39721;

        private static NetworkClockProvider dummyClockProvider;

        private static Dictionary<uint, NetworkClockProvider> providers;
        

        public static void startListener() {
            lock (lockObj)
            {
                if (listenerThread != null)
                {
                    throw new SoftwareClockProviderException("Attempted to start listener thread when already started.");
                }
                listenerThread = new Thread(listenerThreadProc);
                listenerThread.Start();
                dummyClockProvider = new NetworkClockProvider();
                providers = new Dictionary<uint, NetworkClockProvider>();
            }
        }

        public static void shutDown()
        {
            lock (lockObj)
            {
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

            }
        }

        public static bool listenerRunning()
        {
            return (listenerThread != null);
        }

        public static void registerMessageLogHandler(EventHandler<MessageEvent> handler)
        {
            messageLog += handler;
        }

        private static UdpClient udpClient;

        private static void listenerThreadProc()
        {
            if (messageLog != null)
                messageLog(dummyClockProvider, new MessageEvent("Starting Network Clock listener thread...", 1, MessageEvent.MessageTypes.Log, MessageEvent.MessageCategories.Networking));


            udpClient = new UdpClient(networkClockDestinationPort);

            if (messageLog!=null)
                messageLog(dummyClockProvider, new MessageEvent("...done", 1, MessageEvent.MessageTypes.Log, MessageEvent.MessageCategories.Networking));

            while (true)
            {
                System.Net.IPEndPoint remoteEnd = null;
                //byte[] received = udpClient.Receive(ref remoteEnd); // does not abort when thread aborts
                //byte[] received = udpClient.b
                IAsyncResult result = udpClient.BeginReceive(null, null);
                byte [] received = udpClient.EndReceive(result, ref remoteEnd);

                if (received.Length != NetworkClockDatagram.datagramByteLength)
                {
                    if (messageLog != null)
                        messageLog(dummyClockProvider, new MessageEvent("Received wrong sized (" + received.Length + ") datagram. Dropping.", 1, MessageEvent.MessageTypes.Error, MessageEvent.MessageCategories.Networking));
                    continue;
                }
                NetworkClockDatagram ndgram = new NetworkClockDatagram(received);

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
