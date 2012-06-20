using System;

namespace DataStructures.Timing
{
	public class DummySubscriber : SoftwareClockSubscriber
	{
		public delegate bool DummySubscriberListener (uint elaspedTime, int priority);
		public delegate bool DummySubscriberExceptionHandler (Exception e);

		private event DummySubscriberListener timerCallback;
		private event DummySubscriberExceptionHandler exceptionCallback;

		public DummySubscriber(DummySubscriberListener timerCallback, DummySubscriberExceptionHandler exceptionCallback) {
			this.timerCallback = timerCallback;
			this.exceptionCallback = exceptionCallback;
		}

		public bool reachedTime(uint elaspedTime_ms, int p) {
			return timerCallback(elaspedTime_ms, p);
		}

		public bool handleExceptionOnClockThread(Exception e) {
			return exceptionCallback(e);
		}
	}
}

