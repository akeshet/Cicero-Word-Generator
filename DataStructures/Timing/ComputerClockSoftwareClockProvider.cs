using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DataStructures.Timing
{
    /// <summary>
    /// The most basic default SoftwareClockProvider, works in the absence of any other hardware.
    /// Polls the Computer's clock (with user selectable polling period) on a dedicated thread.
    /// </summary>
    public class ComputerClockSoftwareClockProvider : SoftwareClockProvider
    {
        private long startTicks;
        private uint pollingPeriod_ms;
        Thread timerThread;
        private bool timerRunning;

        public override void Start()
        {
            Start(100);
        }

        public void Start(uint pollingPeriod_ms)
        {
            if (timerRunning)
                throw new SoftwareClockProviderException("Attempted to start timer that was already running.");
            if (timerThread == null)
                throw new SoftwareClockProviderException("Attempted to start timer before arming it.");

            startTicks = DateTime.Now.Ticks;
            timerThread.Start();
        }

        protected override void abortTimer()
        {
            if (!timerRunning)
                throw new SoftwareClockProviderException("Attempted to abort when timer was not running.");

            if (timerThread == null)
                throw new SoftwareClockProviderException("Unexpected null timer thread.");

            timerThread.Abort();
            timerThread = null;
            timerRunning = false;
        }

        protected override void armTimer()
        {
            if (timerRunning)
                throw new SoftwareClockProviderException("Attempted to arm timer while already running.");

            if (timerThread != null)
                throw new SoftwareClockProviderException("Attempted to arm timer while thread not cleared.");

            timerThread = new Thread(timerThreadProc);
        }

        private void timerThreadProc()
        {
            uint lastTime = 0;
            while (true)
            {
                Thread.Sleep((int)pollingPeriod_ms);
                uint nowTime = Shared.TicksToMilliseconds(DateTime.Now.Ticks - startTicks);
                if (nowTime > lastTime)
                {
                    reachTime(nowTime);
                }
            }
        }
    }
}
