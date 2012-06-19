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
        private static Thread listenerThread;

        public static readonly int networkClockDestinationPort = 39721;

        public static void startListener() {
            if (listenerThread != null)
            {
                throw new SoftwareClockProviderException("Attempted to start listener thread when already started.");
            }
            listenerThread = new Thread(listenerThreadProc);
            listenerThread.Start();
        }

        public static void shutDown()
        {
            if (listenerThread == null)
            {
                throw new SoftwareClockProviderException("Attempted to stop a listener that was not running.");
            }
            listenerThread.Abort();
            listenerThread = null;
        }

        public static bool listenerRunning()
        {
            return (listenerThread != null);
        }

        private static UdpClient udpClient;

        private static void listenerThreadProc()
        {
            udpClient = new UdpClient(networkClockDestinationPort);
        }

        protected override void abortTimer()
        {
            throw new NotImplementedException();
        }

        protected override void armTimer()
        {
            throw new NotImplementedException();
        }

        protected override void startTimer()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }
    }
}
