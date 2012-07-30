using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures.Cicero2
{
    public static class Cicero2ExtensionMethods
    {
        public static UInt64 NextUInt64(this Random rnd)
        {
            var buffer = new byte[sizeof(UInt64)];
            rnd.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }
    }
}
