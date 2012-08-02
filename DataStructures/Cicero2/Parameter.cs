using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ExpandableStructConverter = DataStructures.ExpandableStructConverter;

namespace Cicero.DataStructures2
{

    /// <summary>
    /// This object describes a "parameter", which is a number that can be either specified manually or bound to a variable.
    /// Most of the numbers that a user specifies will actually be parameters.
    /// </summary>
    [Serializable, TypeConverter(typeof(ExpandableStructConverter))]
    public struct Parameter
    {

        /// <summary>
        /// Variable resourceID
        /// </summary>
        public ResourceID var;

        [Description("The variable that is driving the parameter, if any.")]
        public ResourceID<Variable> variable
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
            variable = ResourceID.Null;
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


        /// <summary>
        /// Returns manual value if variableID == null (manual). Otherwise returns the appropriate variable value
        /// </summary>
        /// <param name="variableValues"></param>
        /// <returns></returns>
        public double getValue(Cicero2ResourceDictionary resourceDictionary)
        {
            if (variable == ResourceID.Null) return manualValue;
            else return ((Variable) resourceDictionary[variable]).VariableValue;
        }

        public override string ToString()
        {
            return this.getManualValue().ToString();
        }

        public static bool Equivalent(Parameter a, Parameter b)
        {
            if (a.ManualValue != b.ManualValue)
                return false;
            if (a.variable != b.variable)
                return false;

            return true;
        }
    }
}
