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
    }
}
