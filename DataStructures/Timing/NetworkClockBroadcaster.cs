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
            logMessage(new MessageEvent("Creating new clock broadcaster, clockID " + String.Format("{0:X8}", clockID)));
            this.clockID = clockID;
            this.maxTime = maximumElapsedTime;
            if (clients == null)
            {
                throw new NetworkClockBroadcasterException("Attempted to create a network broadcast clock before starting broadcaster.");
            }
        }

        private static Dictionary<string, UdpClient> clients;


        public static void startBroadcaster(List<String> listenerAddresses = null)
        {
            logMessage(new MessageEvent("Starting Network Clock broadcaster", 0, MessageEvent.MessageTypes.Routine, MessageEvent.MessageCategories.SoftwareClock));
            clients = new Dictionary<string, UdpClient>();

            if (listenerAddresses != null)
                foreach (string add in listenerAddresses)
                    addListener(add);

        }

        public static void addListener(String hostname)
        {
            logMessage(new MessageEvent("Adding network clock listener at hostname: " + hostname, 0, MessageEvent.MessageTypes.Routine, MessageEvent.MessageCategories.SoftwareClock));
            if (!clients.ContainsKey(hostname)) {
                UdpClient client = new UdpClient(hostname, NetworkClockProvider.networkClockDestinationPort);
                clients.Add(hostname, client);
            }
            logMessage(new MessageEvent("...done", 0, MessageEvent.MessageTypes.Routine, MessageEvent.MessageCategories.SoftwareClock));
                
        }

        public static void stopBroadcaster()
        {
            logMessage(new MessageEvent("Stopping broadcaster..."));
            List<string> hostnames = clients.Keys.ToList();
            foreach (string add in hostnames)
            {
                clients[add].Close();
                clients.Remove(add);
            }
        }

        private static UInt32 ndCount = 0;
        
        public bool reachedTime(UInt32 elaspedTime_ms, int p)
        {
            if (maxTime > 0 && elaspedTime_ms > maxTime)
                return false;

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

            return true;
        }

        public bool handleExceptionOnClockThread(Exception e)
        {
            return false;
        }
    }
}
