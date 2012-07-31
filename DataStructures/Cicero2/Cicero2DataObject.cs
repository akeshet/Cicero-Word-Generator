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

        public abstract string ResourceType {
            get;
        }
    }
}
