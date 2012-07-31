using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cicero.DataStructures2
{
    public class Cicero2ResourceDictionary : Dictionary<ResourceID, Cicero2DataObject>
    {
        public IEnumerable<KeyValuePair<ResourceID, Cicero2DataObject>> KeyValuePairsOfType(string ResourceType)
        {
            return this.Where(
                    x => { return (x.Value != null && x.Value.ResourceType == ResourceType); }
                );
        }

        public IEnumerable<Cicero2DataObject> ResourcesOfType(string ResourceType)
        {
            return Values.Where(
                x => { return x.ResourceType == ResourceType; }
                );
        }

        public IEnumerable<KeyValuePair<ResourceID, Cicero2DataObject>> KeyValuePairsOfType<FilterType>()
        {

            return this.Where(
                    x => { return (x is FilterType); }
                );
        }

        public IEnumerable<Cicero2DataObject> ResourcesOfType<FilterType>()
        {
          
            return Values.Where(
                x => { return x is FilterType; }
                );
        }

    }
}
