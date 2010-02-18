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
    }
}
