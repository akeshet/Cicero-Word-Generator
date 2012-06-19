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

        public class NetworkClockBroadcasterException : Exception {
            public NetworkClockBroadcasterException(string message) : base(message) {}
        };

        private UInt32 clockID;
        public NetworkClockBroadcaster(UInt32 clockID)
        {
            this.clockID = clockID;
            if (clients == null)
            {
                throw new NetworkClockBroadcasterException("Attempted to create a network broadcast clock before starting broadcaster.");
            }
        }

        private static Dictionary<string, UdpClient> clients;


        public static void startBroadcaster(List<String> listenerAddresses = null)
        {
            clients = new Dictionary<string, UdpClient>();

            if (listenerAddresses != null)
                foreach (string add in listenerAddresses)
                    addListener(add);

        }

        public static void addListener(String hostname)
        {
            if (!clients.ContainsKey(hostname)) {
                UdpClient client = new UdpClient(hostname, NetworkClockProvider.networkClockDestinationPort);
                clients.Add(hostname, client);
            }
                
        }

        public static void stopBroadcaster()
        {
            List<string> hostnames = clients.Keys.ToList();
            foreach (string add in hostnames)
            {
                clients[add].Close();
                clients.Remove(add);
            }
        }

        private static UInt32 ndCount = 0;
        
        public bool reachedTime(UInt32 elaspedTime_ms)
        {

            NetworkClockDatagram ndgram = new NetworkClockDatagram(elaspedTime_ms, clockID, ndCount);
            ndCount++;
            byte [] buffer = ndgram.toByteStream();
            Dictionary<UdpClient, IAsyncResult> results = new Dictionary<UdpClient,IAsyncResult>();
            foreach (UdpClient client in clients.Values)
            {
                results.Add(client, client.BeginSend(buffer, NetworkClockDatagram.datagramByteLength, null, null));
            }

            foreach (UdpClient client in results.Keys)
            {
                client.EndSend(results[client]);
            }

            return true;
        }

        public bool handleExceptionOnClockThread(Exception e)
        {
            return false;
        }
    }
}
