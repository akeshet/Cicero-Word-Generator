using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Cicero.DataStructures2
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class TimestepGroup : Cicero2DataObject
    {
        protected override IEnumerable<ResourceID> ReferencedResources_Internal()
        {
            return new ResourceID[] { LoopCount };
        }

        private string timestepGroupName;

        public string TimestepGroupName
        {
            get { return timestepGroupName; }
            set { timestepGroupName = value; }
        }

       

        public bool GroupHidden
        {
            get { return !groupEnabled; }
           
        }


        private bool groupEnabled;

        public bool GroupEnabled
        {
            get { return groupEnabled; }
            set { groupEnabled = value; }
        }

        public TimestepGroup(string groupName)
        {
            this.timestepGroupName = groupName;
            this.groupEnabled = true;
        }

        public override string ToString()
        {
            return timestepGroupName;
        }

        private bool loopTimestepGroup;

        public bool LoopTimestepGroup
        {
            get { return loopTimestepGroup; }
            set { loopTimestepGroup = value; }
        }

        private ResourceID<DimensionedParameter> loopCount;

        public ResourceID<DimensionedParameter> LoopCount
        {
            get
            {
                if (loopCount == null)
                    loopCount = new DimensionedParameter(Units.Dimension.unity);
                return loopCount;
            }
            set { loopCount = value; }
        }

        public int LoopCountInt
        {
            get
            {
                if (LoopCount != null)
                {
                    return (int)LoopCount.getBaseValue();
                }
                else return 0;
            }
        }


    }
}
