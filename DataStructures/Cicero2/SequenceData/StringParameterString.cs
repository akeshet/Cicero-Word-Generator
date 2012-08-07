using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ExpandableStructConverter = DataStructures.ExpandableStructConverter;
using System.Linq;

namespace Cicero.DataStructures2
{
    [Serializable, TypeConverter(typeof(ExpandableStructConverter))]
    public struct StringParameterString : Cicero2ResourceReferrer
    {
        public IEnumerable<ResourceID> ReferencedResources_Internal()
        {
            return new ResourceID[] { Parameter };
        }

        private string prefix;

        public string Prefix
        {
            get { return prefix; }
            set { prefix = value; }
        }
        private string postfix;

        public string Postfix
        {
            get { return postfix; }
            set { postfix = value; }
        }

        private ResourceID<DimensionedParameter> parameter;

        public ResourceID<DimensionedParameter> Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public string ToString(Cicero2ResourceDictionary resources)
        {
            return prefix + parameter.getBaseValue(resources).ToString() + postfix;
        }

        /// <summary>
        /// Not to be used!
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }

}
