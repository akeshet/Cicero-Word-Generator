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
    public class ComputerClockSoftwareClockProvider : PollingThreadSoftwareClockProvider
    {
        private long startTicks;
        private uint pollingPeriod_ms;

        public ComputerClockSoftwareClockProvider(uint pollingPeriod_ms) : base()
        {
            this.pollingPeriod_ms = pollingPeriod_ms;
        }

        
        protected override void armTimerThread()
        {
            startTicks = DateTime.Now.Ticks;
        }


        protected override sealed void timerThreadProc()
        {
            uint lastTime = 0;
            bool keepGoing = true;
            while (keepGoing)
            {
                Thread.Sleep((int)pollingPeriod_ms);
                uint nowTime = Shared.TicksToMilliseconds(DateTime.Now.Ticks - startTicks);
                if (nowTime > lastTime)
                {
                    lastTime = nowTime;
                    keepGoing = reachTime(nowTime);
                }
            }
        }
    }
}
