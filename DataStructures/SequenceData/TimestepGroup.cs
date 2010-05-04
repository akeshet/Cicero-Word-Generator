using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace DataStructures
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class TimestepGroup
    {
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

        private DimensionedParameter loopCount;

        public DimensionedParameter LoopCount
        {
            get
            {
                if (loopCount == null)
                    loopCount = new DimensionedParameter(Units.Dimension.unity);
                return loopCount;
            }
            set { loopCount = value; }
        }

        public Dictionary<Variable, string> usedVariables()
        {
            Dictionary<Variable, string> ans = new Dictionary<Variable, string>();

            if (this.LoopCount.myParameter.variable != null)
                ans.Add(this.LoopCount.myParameter.variable, "Loop Count.");
            return ans;
        }
    }
}
