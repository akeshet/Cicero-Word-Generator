using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cicero.DataStructures2 {
    public abstract class Cicero2DataObject
    {
        private ResourceID resourceID;

        public ResourceID ResourceID
        {
            get { return resourceID; }
        }

        protected Cicero2DataObject(ResourceID resourceID)
        {
            this.resourceID = resourceID;
        }

        protected Cicero2DataObject()
        {
            this.resourceID = ResourceID.newRandom();
        }

        protected Cicero2DataObject(ICollection<ResourceID> exclusionList)
        {
            this.resourceID = ResourceID.newRandom(exclusionList);
        }

		/// <summary>
		/// To be implemented by subclasses of Cicero2DataObject.
		/// Should return a List (or other enumerable type) of all
		/// directly referenced resources in use by this object. (Ie just 
		/// those ResourceIDs that are currently directly referenced).
		/// 
		/// May return null if none referenced.
		/// </summary>
		/// <returns>
		/// The resources_ internal.
		/// </returns>
		protected abstract IEnumerable<ResourceID> ReferencedResources_Internal();

		/// <summary>
		/// Walks the tree of resourceIDs used by this resource, and returns a set
		/// of all used ResourceIDs.
		/// </summary>
		/// <returns>
		/// The resources.
		/// </returns>
		/// <param name='resourceDictionary'>
		/// Resource dictionary.
		/// </param>
		/// <param name='usedSet'>
		/// Used set.
		/// </param>
		public HashSet<ResourceID> UsedResources(Cicero2ResourceDictionary resourceDictionary, 
		                                         HashSet<ResourceID> usedSet = null) {
			if (usedSet==null)
				usedSet = new HashSet<ResourceID>();

			if (usedSet.Contains(this.ResourceID))
				return usedSet;

			usedSet.Add(this.ResourceID);

			IEnumerable<ResourceID> referencedResources = this.ReferencedResources_Internal();
			if (referencedResources == null)
				return usedSet;

			foreach (ResourceID id in referencedResources) {
				if (id==ResourceID.Null)
					continue;
				Cicero2DataObject referencedObject = resourceDictionary.Get(id);
				if (referencedObject!=null)
					referencedObject.UsedResources(resourceDictionary, usedSet);
			}

			return usedSet;
		}
    }
}
