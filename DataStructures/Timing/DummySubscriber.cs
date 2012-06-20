using System;

namespace DataStructures.Timing
{
	public class DummySubscriber : SoftwareClockSubscriber
	{
		public delegate bool DummySubscriberListener (uint elaspedTime, int priority);
		public delegate bool DummySubscriberExceptionHandler (Exception e);
        public delegate bool DummyFinishHandler(int priority);

		private event DummySubscriberListener timerCallback;
		private event DummySubscriberExceptionHandler exceptionCallback;
        private event DummyFinishHandler finishCallback;

		public DummySubscriber(DummySubscriberListener timerCallback, DummySubscriberExceptionHandler exceptionCallback, DummyFinishHandler finishCallback) {
			this.timerCallback = timerCallback;
			this.exceptionCallback = exceptionCallback;
            this.finishCallback = finishCallback;
		}

		public bool reachedTime(uint elaspedTime_ms, int p) {
			return timerCallback(elaspedTime_ms, p);
		}

		public bool handleExceptionOnClockThread(Exception e) {
			return exceptionCallback(e);
		}

        public bool providerTimerFinished(int priority)
        {
            return finishCallback(priority);
        }
	}
}

