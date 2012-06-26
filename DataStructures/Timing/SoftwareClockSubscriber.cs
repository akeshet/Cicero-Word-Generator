using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures.Timing
{
    /// <summary>
    /// Common interface for all objects that need to make use of a software clock.
    /// Guarantees:
    /// reachedTime will be called at a rate determined by the details of the software clock
    /// provider.
    /// 
    /// It will always be called on the same thread, so Subscribers do not need to worry about concurrency.
    /// It will always be called on its own dedicated thread, so Subscribers do not need to worry about blocking.
    /// 
    /// It will always be called with increasing values of time_ms.
    /// </summary>
    public interface SoftwareClockSubscriber
    {
        /// <summary>
        /// Return true if object still want to subscribe to more timing events.
        /// Return false if object is finished.
        /// </summary>
        /// <param name="time_ms"></param>
        /// <returns></returns>
        bool reachedTime(uint time_ms, int priority);

        /// <summary>
        /// Handle an exception that occured on the clock thread. 
        /// Return true if the exception was handled, otherwise return false
        /// and the exception will be re-thrown.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        bool handleExceptionOnClockThread(Exception e);

        /// <summary>
        /// Will be possibly be called after there are no further calls
        /// to reachedTime, if the provider has reached its maximum output time limit
        /// </summary>
        /// <returns></returns>
        bool providerTimerFinished(int priority);
    }
}
