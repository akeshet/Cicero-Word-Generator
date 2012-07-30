using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructures.Cicero2.Extensions;

namespace DataStructures.Cicero2
{
    public struct ResourceID
    {
        private UInt64 idInteger;

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

        private static Random randomGenerator;

        /// <summary>
        /// Create and return a new random ResourceID.
        /// </summary>
        /// <returns></returns>
        public static ResourceID newRandom() {
            if (randomGenerator==null)
                randomGenerator = new Random();
           
            ResourceID ans;
            ans.idInteger = randomGenerator.NextUInt64();
            return ans;
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
