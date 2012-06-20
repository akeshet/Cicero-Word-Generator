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
        private object lockObj = new object();
        private bool threadsRunning = false;

        public class SoftwareClockProviderException : Exception {
            public SoftwareClockProviderException(string message) : base(message)
            {}
        }

        protected List<SoftwareClockSubscriber> subscribers;
        protected Dictionary<SoftwareClockSubscriber, Thread> subscriberThreads;
        protected Dictionary<SoftwareClockSubscriber, int> subscriberPollingPeriods_ms;
        protected UInt32 elapsedTime_ms;

        private EventWaitHandle subscriberWaitHandle;
        private EventWaitHandle providerWaitHandle;

        protected SoftwareClockProvider()
        {
            subscribers = new List<SoftwareClockSubscriber>();
            subscriberThreads = new Dictionary<SoftwareClockSubscriber,Thread>();
            subscriberPollingPeriods_ms = new Dictionary<SoftwareClockSubscriber,int>();
        }

        public void clearSubscribers()
        {
            if (threadsRunning)
                throw new SoftwareClockProviderException("Attempted to clear subscribers while threads are still running. You must call Abort() first.");

            subscribers.Clear();
            subscriberThreads.Clear();
            subscriberPollingPeriods_ms.Clear();
        }

        /// <summary>
        /// May add a subscriber even while clock is running.
        /// </summary>
        /// <param name="sub"></param>
        /// <param name="minimumPollingPerios_ms"></param>
        public void addSubscriber(SoftwareClockSubscriber sub, int minimumPollingPerios_ms = 0)
        {
            lock (lockObj)
            {
                subscribers.Add(sub);
                subscriberPollingPeriods_ms.Add(sub, minimumPollingPerios_ms);

                if (threadsRunning)
                {
                    addAndStartSubscriberThread(sub);
                }
            }
        }

        public void Arm()
        {
            lock (lockObj)
            {
                armTimer();

                elapsedTime_ms = 0;
                subscriberWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
                providerWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
                foreach (SoftwareClockSubscriber sub in subscribers)
                {
                    addAndStartSubscriberThread(sub);
                }
                threadsRunning = true;
            }
        }

        private void addAndStartSubscriberThread(SoftwareClockSubscriber sub)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(subscriberThreadProc));
            subscriberThreads.Add(sub, thread);
            runningThreads++;
            thread.Start(new SubscriberThreadParameters(sub, subscriberPollingPeriods_ms[sub]));
            
        }

        public abstract void Start();

        protected abstract void abortTimer();
        protected abstract void armTimer();
        protected abstract void startTimer();

        private int runningThreads;

        public void Abort()
        {
            lock (lockObj)
            {
                abortTimer();

                foreach (Thread thread in subscriberThreads.Values)
                {
                    thread.Abort();
                }
                threadsRunning = false;
                
                clearSubscribers();

                subscriberWaitHandle.Close();
                subscriberWaitHandle = null;
            }
        }


        public UInt32 getElapsedTime()
        {
            return elapsedTime_ms;
        }

        // Returns true if clock should keep running (there are still subscribers listening)
        // false otherwise
        protected bool reachTime(uint time_ms) {
            if (time_ms < elapsedTime_ms)
                throw new SoftwareClockProviderException("Attempted to go back in time!");

            elapsedTime_ms = time_ms;
            subscriberWaitHandle.Set();
            providerWaitHandle.WaitOne(10); // This ensures that if there are any threads
                                            // blocking on subscriberWaitHandle.WaitOne
                                            // they get a chance to be released befor the subsequent call to 
                                            // subscriberWaitHandle.Reset
                                            
                                            // This is due to an apparent bug in ManualResetEvent which does
                                            // not seem to always release blocked threads on every .Set() command
                                            
                                            // The previous workaround was a Thread.Sleep(1) line here,
                                            // which seemed to work, but seemed more fragile or potentially problematic

                                            // The 10ms timeout is to avoid deadlocks (though I think they are impossible
                                            // even without the timeout). It should be harmless.
            subscriberWaitHandle.Reset();
            if (runningThreads == 0)
                return false;
            return true;
        }

        private class SubscriberThreadParameters
        {
            public SoftwareClockSubscriber subscriber;
            public int minPollTime_ms;
            public SubscriberThreadParameters(SoftwareClockSubscriber sub, int minPollTime_ms)
            {
                this.subscriber = sub;
                this.minPollTime_ms = minPollTime_ms;
            }
        }

        private void subscriberThreadProc(object param)
        {
            if (!(param is SubscriberThreadParameters))
                throw new SoftwareClockProviderException("Passed wrong parameter type to subscriberThreadProc");

            SubscriberThreadParameters parameters = (SubscriberThreadParameters) param;

            SoftwareClockSubscriber subscriber = parameters.subscriber;
            int minPollingPeriod_ms = parameters.minPollTime_ms;

            uint lastPoll = elapsedTime_ms;
            bool keepGoing = true;
            while (keepGoing)
            {
                providerWaitHandle.Set();
                subscriberWaitHandle.WaitOne(); // Wait for the next clock signal to arrive
                

                
                uint thisPoll = elapsedTime_ms;
                if ((thisPoll - lastPoll) < minPollingPeriod_ms)
                    continue;

                lastPoll = thisPoll;
                try
                {
                    keepGoing = subscriber.reachedTime(thisPoll);
                }
                catch (Exception e)
                {
                    if (!subscriber.handleExceptionOnClockThread(e))
                    {
                        runningThreads--;
                        throw;
                    }
                }
            }
            runningThreads--;
        }
    }
}
