using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace DataStructures.Timing
{
    /// <summary>
    /// The most basic default SoftwareClockProvider, works in the absence of any other hardware.
    /// Polls the Computer's clock (with user selectable polling period) on a dedicated thread.
    /// </summary>
    public class ComputerClockSoftwareClockProvider : PollingThreadSoftwareClockProvider
    {
        private uint pollingPeriod_ms;
		private Stopwatch stopwatch;

        public ComputerClockSoftwareClockProvider(uint pollingPeriod_ms) : base()
        {
            this.pollingPeriod_ms = pollingPeriod_ms;
			stopwatch = new Stopwatch();
        }

        
        protected override void armTimerThread()
        {
			stopwatch.Reset();
			stopwatch.Start();
        }


        protected override sealed void timerThreadProc()
        {
			try {
	            uint lastTime = 0;
	            bool keepGoing = true;
	            while (keepGoing)
	            {
	                Thread.Sleep((int)pollingPeriod_ms);
                    if (stopwatch.ElapsedMilliseconds > uint.MaxValue)
                        throw new SoftwareClockProviderException("Stopwatch reached time beyond maximum value of " + uint.MaxValue + "ms.");
					uint nowTime = (uint) stopwatch.ElapsedMilliseconds;
	                if (nowTime > lastTime)
	                {
	                    lastTime = nowTime;
	                    keepGoing = reachTime(nowTime);
	                }
	            }
			}
			finally {
				stopwatch.Stop(); // when thread gets aborted,
							      // this finally block will execute
			}
        }
    }
}
