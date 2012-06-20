using System;

namespace DataStructures.Timing
{
	public class DummySubscriber : SoftwareClockSubscriber
	{
		public delegate bool DummySubscriberListener (uint elaspedTime);
		public delegate bool DummySubscriberExceptionHandler (Exception e);

		private event DummySubscriberListener timerCallback;
		private event DummySubscriberExceptionHandler exceptionCallback;

		public DummySubscriber(DummySubscriberListener timerCallback, DummySubscriberExceptionHandler exceptionCallback) {
			this.timerCallback = timerCallback;
			this.exceptionCallback = exceptionCallback;
		}

		public bool reachedTime(uint elaspedTime_ms) {
			return timerCallback(elaspedTime_ms);
		}

		public bool handleExceptionOnClockThread(Exception e) {
			return exceptionCallback(e);
		}
	}
}

