using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
namespace DataStructures
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class LUT
    {

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private SortedDictionary<double, double> table;
        public SortedDictionary<double, double> Table
        {
            get { return table; }
            set { table = value; }
        }


        public LUT(string inName)
        {
            name = inName;
            table = new SortedDictionary<double, double>();
        }

    }       

}
