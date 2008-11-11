using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataStructures
{

    /// <summary>
    /// This object describes a "parameter", which is a number that can be either specified manually or bound to a variable.
    /// Most of the numbers that a user specifies will actually be parameters.
    /// </summary>
    [Serializable, TypeConverter(typeof(ExpandableStructConverter))]
    public struct Parameter
    {
        /// <summary>
        /// null = manual.
        /// NOTE: This field has to be public due to a bug in .NET remoting. If this field is private,
        /// then it will intermittently fail to be correctly handled by remoting.
        /// </summary>
        public Variable var;

        [Description("The variable that is driving the parameter, if any.")]
        public Variable variable
        {
            get { return var; }
            set { var = value; }
        }

        /// <summary>
        /// Sets the parameters manual value, and disables any variables which were in use.
        /// </summary>
        /// <param name="value"></param>
        public void forceToManualValue(double value)
        {
            ManualValue = value;
            variable = null;
        }

        private double manualValue;

        [Description("The manual value of this parameter. This takes effect if and only if there is no variable driving this parameter.")]
        public double ManualValue
        {
            get { return manualValue; }
            set { manualValue = value; }
        }

        public double getManualValue()
        {
            return manualValue;
        }

        [Description("The value of this parameter, taking into account if it is being driven by a variable.")]
        public double Value
        {
            get
            {
                return getValue();
            }
        }

        /// <summary>
        /// Returns manual value if variableID == null (manual). Otherwise returns the appropriate variable value
        /// </summary>
        /// <param name="variableValues"></param>
        /// <returns></returns>
        public double getValue()
        {
            if (variable == null) return manualValue;
            else return variable.VariableValue;
        }

        public override string ToString()
        {
            return this.getManualValue().ToString();
        }
    }
}
