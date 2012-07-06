using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures.Timing
{
    public static class Shared
    {
        public static uint TicksToMilliseconds(long ticks)
        {
            return (uint) (ticks / 10000);
        }

        public static long MillisecondsToTicks(uint ms)
        {
            return ms * 10000;
        }

        public static long SecondsToTicks(double seconds)
        {
            return (long)(seconds * 10000000);
        }

        public static double TicksToSeconds(long ticks)
        {
            return ((double)ticks) / 10000000.0;
        }

        public static String clockIDToString(UInt32 clockID)
        {
            return String.Format("{0:X8}", clockID);
        }
    }
}
