using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DataStructures.Timing
{
    public abstract class PollingThreadSoftwareClockProvider : SoftwareClockProvider
    {
        private object lockObj = new object();

        private Thread timerThread;
        private bool timerRunning;

        protected override sealed void startClockProvider()
        {
            lock (lockObj)
            {
                if (timerRunning)
                    throw new SoftwareClockProviderException("Attempted to start timer that was already running.");
                if (timerThread == null)
                    throw new SoftwareClockProviderException("Attempted to start timer before arming it.");

                armTimerThread();
                timerThread.Start();
                timerRunning = true;
            }
        }

        protected override sealed void cleanupClockProvider()
        {
            lock (lockObj)
            {
                if (!timerRunning)
                    throw new SoftwareClockProviderException("Attempted to abort when timer was not running.");

                if (timerThread == null)
                    throw new SoftwareClockProviderException("Unexpected null timer thread.");

                timerThread.Abort();
                timerThread = null;
                timerRunning = false;
            }
        }

        protected override sealed void armClockProvider()
        {
            lock (lockObj)
            {
                if (timerRunning)
                    throw new SoftwareClockProviderException("Attempted to arm timer while already running.");

                if (timerThread != null)
                    throw new SoftwareClockProviderException("Attempted to arm timer while thread not cleared.");

                timerThread = new Thread(timerThreadProc);
            }
        }

        protected abstract void timerThreadProc();
        
        /// <summary>
        /// Function that is called immediately before timer
        /// thread is started. A convenient last-second place to reset any "start time"
        /// </summary>
        protected abstract void armTimerThread();


    }
}
