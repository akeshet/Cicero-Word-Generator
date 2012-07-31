using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cicero.DataStructures2.Extensions;

namespace Cicero.DataStructures2
{
    public struct ResourceID
    {
        public static readonly ResourceID Null = new ResourceID(0);

        private static readonly UInt64[] specialIDs = new UInt64[] {
            Null.idInteger
        };

        private UInt64 idInteger;

        private ResourceID(UInt64 idInteger) {
            this.idInteger = idInteger;
        }

        public override int GetHashCode()
        {
            return (int) (idInteger % int.MaxValue);
        }

        public bool Equals(ResourceID other)
        {
            return idInteger == other.idInteger;
        }

        public bool Equals(UInt64 other)
        {
            return idInteger == other;
        }

        public override bool Equals(object other)
        {
            if (other is UInt64)
                return this.Equals((UInt64)other);
            if (other is ResourceID)
                return this.Equals((ResourceID) other);
            return false;
        }
 
        public override string ToString()
        {
            return String.Format("{0:X16}", idInteger);
        }

        public static bool operator==(ResourceID a, ResourceID b) {
            return a.idInteger==b.idInteger;
        }

        public static bool operator!=(ResourceID a, ResourceID b) {
            return a.idInteger!=b.idInteger;
        }

        private static Random randomGenerator;

        /// <summary>
        /// Create and return a new random ResourceID.
        /// </summary>
        /// <returns></returns>
        public static ResourceID newRandom() {
            if (randomGenerator==null)
                randomGenerator = new Random();

            UInt64 randomInt64;
            while (true) {
                randomInt64 = randomGenerator.NextUInt64();
                if (specialIDs.Contains(randomInt64))
                    continue;
                break;
            }

            return new ResourceID(randomInt64);
        }

        /// <summary>
        /// Create and return a new random ResourceID that is guaranteed not to be on the 
        /// supplied exclusionList. If exclusionList is null, identical to newRandom.
        /// 
        /// </summary>
        /// <param name="exclusionList"></param>
        /// <returns></returns>
        public static ResourceID newRandom(ICollection<ResourceID> exclusionList)
        {
            if (exclusionList == null)
                return newRandom();
            while (true)
            {
                ResourceID ans = newRandom();
                if (!exclusionList.Contains(ans))
                    return ans;
            }
        }
    }
}
