using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Cicero.DataStructures2
{
    /// <summary>
    /// This is a wrapper for ResourceID which allows you to keep track of the 
    /// type of resource that this ID should point to. It is mostly for convenience, though with proper
    /// use it might provide some type safety. Can be downcast to a ResourceID, and ResourceIDs can be upcast to it.
    /// </summary>
    /// <typeparam name="ResourceType"></typeparam>
    public struct ResourceID<ResourceType> : IEquatable<ResourceID>,
                                             IEquatable<ResourceID<ResourceType>>
                                                where ResourceType : Cicero2DataObject
    {
        private ResourceID resourceID;

        public static implicit operator ResourceID(ResourceID<ResourceType> convertMe)
        {
            return convertMe.resourceID;
        }

        public static implicit operator ResourceID<ResourceType>(ResourceID convertMe)
        {
            ResourceID<ResourceType> ans;
            ans.resourceID = convertMe;
            return ans;
        }

        public bool Equals(ResourceID<ResourceType> other)
        {
            return this.resourceID == other.resourceID;
        }

        public bool Equals(ResourceID other)
        {
            return this.resourceID == other;
        }

        public override int GetHashCode()
        {
            return resourceID.GetHashCode();
        }

        public override string ToString()
        {
            return resourceID.ToString();
        }

        public bool Equals<OtherType>(ResourceID<OtherType> other) where OtherType : Cicero2DataObject
        {
            return this.resourceID == other.resourceID;
        }

        public static bool operator ==(ResourceID<ResourceType> a, ResourceID<ResourceType> b)
        {
            return a.resourceID == b.resourceID;
        }

        public static bool operator !=(ResourceID<ResourceType> a, ResourceID<ResourceType> b)
        {
            return a.resourceID != b.resourceID;
        }

        public static bool operator ==(ResourceID<ResourceType> a, ResourceID<Cicero2DataObject> b)
        {
            return a.resourceID == b.resourceID;
        }

        public static bool operator !=(ResourceID<ResourceType> a, ResourceID<Cicero2DataObject> b)
        {
            return a.resourceID != b.resourceID;
        }


        public static bool operator ==(ResourceID<ResourceType> a, ResourceID b)
        {
            return a.resourceID == b;
        }

        public static bool operator !=(ResourceID<ResourceType> a, ResourceID b)
        {
            return a.resourceID == b;
        }


        public override bool Equals(object obj)
        {
            if (obj is ResourceID)
                return this == (ResourceID)obj;
            if (obj is ResourceID<ResourceType>)
                return this == (ResourceID<ResourceType>)obj;
            if (obj is ResourceID<Cicero2DataObject>)
                return this == (ResourceID<Cicero2DataObject>)obj;
            return false;
        }
    }

    /// <summary>
    /// This struct is effectively just a wrapper for UInt64.
    /// It also provides some convenient random-UInt64 generating features,
    /// Nullability (and null testing), and the requisite boilerplate operator
    /// overloads.
    /// </summary>
    public struct ResourceID
    {
        public static readonly ResourceID Null = new ResourceID(0);

        private static readonly UInt64[] specialIDs = new UInt64[] {
            Null.idInteger
        };

        private UInt64 idInteger;

        private ResourceID(UInt64 idInteger)
        {
            this.idInteger = idInteger;
        }

        public override int GetHashCode()
        {
            return (int)(idInteger % int.MaxValue);
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
                return this.Equals((ResourceID)other);
            return false;
        }

        public override string ToString()
        {
            return String.Format("{0:X16}", idInteger);
        }

        public static bool operator ==(ResourceID a, UInt64 b)
        {
            return a.idInteger == b;
        }

        public static bool operator !=(ResourceID a, UInt64 b)
        {
            return a.idInteger != b;
        }

        public static bool operator ==(ResourceID a, ResourceID b)
        {
            return a.idInteger == b.idInteger;
        }

        public static bool operator !=(ResourceID a, ResourceID b)
        {
            return a.idInteger != b.idInteger;
        }


        private static Random randomGenerator;

        /// <summary>
        /// Create and return a new random ResourceID.
        /// </summary>
        /// <returns></returns>
        public static ResourceID newRandom()
        {
            if (randomGenerator == null)
                randomGenerator = new Random();

            UInt64 randomInt64;
            while (true)
            {
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

	public static class ResourceIDCastExtensions {
		public static IEnumerable<ResourceID> DownCast<ResourceType>(this IEnumerable<ResourceID<ResourceType>> a) 
		where ResourceType : Cicero2DataObject
		{
			return a.Cast<ResourceID>();
		}
	}

    public class ResourceException : Exception
    {
        public ResourceException(String message) : base(message) {}
    }
}
