using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DataStructures.Timing
{

    /// <summary>
    /// Base class for software clock providers.
    /// </summary>
    public abstract class SoftwareClockProvider
    {
        /// <summary>
        /// Lock, to be acquired whenever making changes to this object or its members.
        /// </summary>
        private object lockObj = new object();
        private bool threadsRunning = false;

        public class SoftwareClockProviderException : Exception {
            public SoftwareClockProviderException(string message) : base(message)
            {}
        }

        private List<SoftwareClockSubscriber> subscribers = new List<SoftwareClockSubscriber>();
        private Dictionary<SoftwareClockSubscriber, Thread> subscriberThreads = new Dictionary<SoftwareClockSubscriber,Thread>();
        private Dictionary<SoftwareClockSubscriber, int> subscriberPollingPeriods_ms = new Dictionary<SoftwareClockSubscriber,int>();
        private Dictionary<SoftwareClockSubscriber, int> subscriberPritorities = new Dictionary<SoftwareClockSubscriber,int>();

        protected UInt32 elapsedTime_ms;

		private object synchronizationObject = new object();

        private EventHandler<MessageEvent> messageLog;

        /// <summary>
        /// Registers handler to be used for logging messages, errors, and warnings.
        /// </summary>
        /// <param name="handler"></param>
        public void registerMessageLogHandler(EventHandler<MessageEvent> handler)
        {
            lock (lockObj)
                messageLog += handler;
        }

        public enum Status { Idle, Armed, Running, Aborted };

        private Status clockStatus;

        public Status ClockStatus
        {
            get { return clockStatus; }
        }


        /// <summary>
        /// Safely logs a message.
        /// </summary>
        /// <param name="message"></param>
        protected void logMessage(MessageEvent message)
        {
            if (messageLog != null)
                messageLog(this, message);
        }

        public void clearSubscribers()
        {
            lock (lockObj)
            {
                if (threadsRunning)
                    throw new SoftwareClockProviderException("Attempted to clear subscribers while threads are still running. You must call Abort() first.");

                subscribers.Clear();
                subscriberThreads.Clear();
                subscriberPollingPeriods_ms.Clear();
                subscriberPritorities.Clear();
            }
        }

        /// <summary>
        /// Add a subscriber to this clock provider.
        /// 
        /// This function may be called even after the software clock has been started with a Start() call.
        /// 
        /// Priority is an optional integer parameter that gets passed to the subscriber in addition to the time
        /// Subscriber may use this to decide among several clock sources.
        /// 
        /// All calls to the subscriber's reachTime and finish() methods are guaranteed to occur on one dedicated thread.
        /// 
        /// Note: if a subscriber subscriber to multiple providers, calls from different providers are NOT 
        /// guaranteed to be on the same thread.
        /// </summary>
        /// <param name="sub"></param>
        /// <param name="minimumPollingPerios_ms"></param>
        public void addSubscriber(SoftwareClockSubscriber sub, int minimumPollingPerios_ms = 0, int priority=0)
        {
            lock (lockObj)
            {
                subscribers.Add(sub);
                subscriberPollingPeriods_ms.Add(sub, minimumPollingPerios_ms);
                subscriberPritorities.Add(sub, priority);

                if (threadsRunning)
                {
                    addAndStartSubscriberThread(sub);
                }
            }
        }

        /// <summary>
        /// To be called after the provider is constructed, and before a call to Start().
        /// 
        /// Used to set up the provider so that a subsequence call to Start() will be faster.
        /// </summary>
        public void ArmClockProvider()
        {
             lock (lockObj)
            {
                clockStatus = Status.Armed;

                armClockProvider();

                elapsedTime_ms = 0;
                foreach (SoftwareClockSubscriber sub in subscribers)
                {
                    addAndStartSubscriberThread(sub);
                }
                threadsRunning = true;
            }
        }

        private void addAndStartSubscriberThread(SoftwareClockSubscriber sub)
        {
            lock (lockObj)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(subscriberThreadProc));
                subscriberThreads.Add(sub, thread);
                runningSubscriberThreads++;
                thread.Start(new SubscriberThreadParameters(sub, subscriberPollingPeriods_ms[sub], subscriberPritorities[sub]));
            }
        }

        /// <summary>
        /// Start the clock provider. No reachTime calls to subscribers
        /// will occur before a call to Start()
        /// </summary>
        public void StartClockProvider() {
            lock (lockObj) {
                clockStatus = Status.Running;
                startClockProvider();
            }
        }

        /// <summary>
        /// Called when clock provider is to be aborted, so that inheriting classes
        /// can clean up any threads they have running.
        /// </summary>
        protected abstract void cleanupClockProvider();

        /// <summary>
        /// Called when clock provider is to be armed, so that inheriting classes
        /// can prepare any threads they want to run.
        /// </summary>
        protected abstract void armClockProvider();

        /// <summary>
        /// Called when the clock provider is to be started, so that inheriting classes
        /// can get to work.
        /// </summary>
        protected abstract void startClockProvider();


        private int runningSubscriberThreads;

        public void AbortClockProvider()
        {
            lock (lockObj)
            {
                clockStatus = Status.Aborted;
                cleanupClockProvider();

                foreach (Thread thread in subscriberThreads.Values)
                {
                    thread.Abort();
                }
                threadsRunning = false;
                
                clearSubscribers();

            }
        }


        public UInt32 getElapsedTime()
        {
            return elapsedTime_ms;
        }

        /// <summary>
        /// Inform subscribers that a given time has been reached.
        /// 
        /// Returns true if there is still at least 1 subscriber listening.
        /// Returns false otherwise.
        /// 
        /// Throws an exception if you pass this method a time that is earlier than previously
        /// specified time.
        /// </summary>
        /// <param name="time_ms"></param>
        /// <returns></returns>
        protected bool reachTime(uint time_ms) {
            if (time_ms < elapsedTime_ms)
                throw new SoftwareClockProviderException("Attempted to go back in time!");

			lock(synchronizationObject) {
				elapsedTime_ms = time_ms;
				Monitor.PulseAll(synchronizationObject);
			}

            lock (lockObj)
                if (runningSubscriberThreads == 0)
                    return false;

            return true;
        }

        private class SubscriberThreadParameters
        {
            public SoftwareClockSubscriber subscriber;
            public int minPollTime_ms;
            public int priority;
            public SubscriberThreadParameters(SoftwareClockSubscriber sub, int minPollTime_ms, int priority)
            {
                this.subscriber = sub;
                this.minPollTime_ms = minPollTime_ms;
                this.priority = priority;
            }
        }

        private void subscriberThreadProc(object param)
        {
            try
            {
                if (!(param is SubscriberThreadParameters))
                    throw new SoftwareClockProviderException("Passed wrong parameter type to subscriberThreadProc");

                SubscriberThreadParameters parameters = (SubscriberThreadParameters)param;

                SoftwareClockSubscriber subscriber = parameters.subscriber;
                int minPollingPeriod_ms = parameters.minPollTime_ms;
                int priority = parameters.priority;

                uint lastPoll = elapsedTime_ms;
                bool keepGoing = true;
                while (keepGoing)
                {

                    lock (synchronizationObject)
                    {
                        Monitor.Wait(synchronizationObject);
                    }

                    uint thisPoll = elapsedTime_ms;

                    if ((thisPoll - lastPoll) < minPollingPeriod_ms)
                        continue;

                    lastPoll = thisPoll;
                    try
                    {
                        keepGoing = subscriber.reachedTime(thisPoll, priority);
                    }
                    catch (Exception e)
                    {
                        if (!subscriber.handleExceptionOnClockThread(e))
                            throw;
                    }


                }
            }
            finally
            {
                lock (lockObj)
                    runningSubscriberThreads--;
            }
        }
    }
}
