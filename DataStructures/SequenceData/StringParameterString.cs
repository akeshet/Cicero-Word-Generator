using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataStructures
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class StringParameterString
    {
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
        private DimensionedParameter parameter;

        public DimensionedParameter Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public StringParameterString()
        {
            prefix = "";
            postfix = "";
            parameter = new DimensionedParameter(Units.Dimension.unity);
        }

        public override string ToString()
        {
            return prefix + parameter.getBaseValue().ToString() + postfix;
        }
    }
}
