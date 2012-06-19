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

        private EventWaitHandle waitHandle;

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

        public void addSubscriber(SoftwareClockSubscriber sub, int minimumPollingPerios_ms = 0)
        {
            subscribers.Add(sub);
            subscriberPollingPeriods_ms.Add(sub, minimumPollingPerios_ms);
        }

        public void Arm()
        {
            lock (lockObj)
            {
                armTimer();

                elapsedTime_ms = 0;
                waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
                foreach (SoftwareClockSubscriber sub in subscribers)
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(subscriberThreadProc));
                    subscriberThreads.Add(sub, thread);
                    thread.Start(new SubscriberThreadParameters(sub, subscriberPollingPeriods_ms[sub]));
                }
                threadsRunning = true;
            }
        }

        public abstract void Start();

        protected abstract void abortTimer();
        protected abstract void armTimer();


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

                waitHandle.Close();
                waitHandle = null;
            }
        }

        

        protected void reachTime(uint time_ms) {
            if (time_ms < elapsedTime_ms)
                throw new SoftwareClockProviderException("Attempted to go back in time!");

            elapsedTime_ms = time_ms;
            waitHandle.Set();
            waitHandle.Reset();
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
            while (true)
            {
                waitHandle.WaitOne(); // Wait for the next clock signal to arrive
                
                uint thisPoll = elapsedTime_ms;
                if ((thisPoll - lastPoll) < minPollingPeriod_ms)
                    continue;

                lastPoll = thisPoll;
                subscriber.reachedTime(thisPoll);
            }
        }
    }
}
