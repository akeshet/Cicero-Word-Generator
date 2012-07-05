using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace DataStructures.Timing
{
    public class NetworkClockBroadcaster : SoftwareClockSubscriber
    {
        public static readonly int networkClockBroadcastSourcePort = 26914;

        private static EventHandler<MessageEvent> messageLog;

        private static NetworkClockBroadcaster dummy = new NetworkClockBroadcaster();

        private NetworkClockBroadcaster() { }

        public static void registerMessageLogHandler(EventHandler<MessageEvent> handler)
        {
            messageLog += handler;
        }

        protected static void logMessage(MessageEvent message)
        {
            if (messageLog != null)
                messageLog(dummy, message);
        }

        public class NetworkClockBroadcasterException : Exception {
            public NetworkClockBroadcasterException(string message) : base(message) {}
        };

        private UInt32 clockID;
        private UInt32 maxTime;
        public NetworkClockBroadcaster(UInt32 clockID, UInt32 maximumElapsedTime=0)
        {
            logMessage(new MessageEvent("Creating new clock broadcaster, clockID " + Shared.clockIDToString(clockID)));
            this.clockID = clockID;
            this.maxTime = maximumElapsedTime;
            if (clients == null)
            {
                throw new NetworkClockBroadcasterException("Attempted to create a network broadcast clock before starting broadcaster.");
            }
        }

        private static Dictionary<string, UdpClient> clients;


        public static void startBroadcasterThreads(List<NetworkClockEndpointInfo> listenerAddresses = null)
        {
            logMessage(new MessageEvent("Starting Network Clock broadcaster", 0, MessageEvent.MessageTypes.Routine, MessageEvent.MessageCategories.SoftwareClock));
            clients = new Dictionary<string, UdpClient>();

            if (listenerAddresses != null)
                foreach (NetworkClockEndpointInfo add in listenerAddresses)
                    addListener(add);

        }

        public static void addListener(NetworkClockEndpointInfo endPoint)
        {
            logMessage(new MessageEvent("Adding network " + endPoint.HostType.ToString() + " clock listener at hostname: " + endPoint.HostName, 0, MessageEvent.MessageTypes.Routine, MessageEvent.MessageCategories.SoftwareClock));
            if (!clients.ContainsKey(endPoint.HostName)) {
                UdpClient client = new UdpClient(endPoint.HostName, endPoint.getPort());
                clients.Add(endPoint.HostName, client);
            }
            logMessage(new MessageEvent("...done", 0, MessageEvent.MessageTypes.Routine, MessageEvent.MessageCategories.SoftwareClock));
                
        }

        public static void clearListeners()
        {
            List<string> hostnames = clients.Keys.ToList();
            foreach (string add in hostnames)
            {
                clients[add].Close();
                clients.Remove(add);
            }
        }

        public static void stopBroadcasterThread()
        {
            logMessage(new MessageEvent("Stopping broadcaster threads..."));
            clearListeners();
        }

        private static UInt32 ndCount = 0;
        

        public bool reachedTime(UInt32 elaspedTime_ms, int p)
        {
           
            NetworkClockDatagram ndgram = new NetworkClockDatagram(elaspedTime_ms, clockID, ndCount);
            ndCount++;
            byte [] buffer = ndgram.toByteStream();
            Dictionary<UdpClient, IAsyncResult> results = new Dictionary<UdpClient,IAsyncResult>();
            foreach (UdpClient client in clients.Values)
            {
                results.Add(client, client.BeginSend(buffer, NetworkClockDatagram.datagramByteLength, null, null));
#if DEBUG
                logMessage(new MessageEvent("Began sending datagram to client " + client.ToString(), 2, MessageEvent.MessageTypes.Debug, MessageEvent.MessageCategories.Networking));
#endif
            }

            foreach (UdpClient client in results.Keys)
            {
                client.EndSend(results[client]);
#if DEBUG
                logMessage(new MessageEvent("Finished sending datagram to client " + client.ToString(), 2, MessageEvent.MessageTypes.Debug, MessageEvent.MessageCategories.Networking));
#endif
            }

            if (maxTime > 0 && elaspedTime_ms > maxTime)
                return false;

            return true;
        }

        public bool providerTimerFinished(int priority)
        {
            return reachedTime(maxTime, priority);
        }

        public bool handleExceptionOnClockThread(Exception e)
        {
            return false;
        }
    }
}
