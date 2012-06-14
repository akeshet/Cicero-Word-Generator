using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace DataStructures
{
    /// <summary>
    /// This object represents a parameter with units. This object should be used in most of the places where
    /// a user specifies a number and its units / unit multiplier. (ie to set the timestep lengths, to set the interpolation
    /// values for waveforms, etc).
    /// </summary>
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class DimensionedParameter
    {
        public static bool Equivalent(DimensionedParameter a, DimensionedParameter b) {
            if (!Parameter.Equivalent(a.myParameter, b.myParameter))
                return false;
            if (a.ParameterString != b.ParameterString)
                return false;
            if (!Units.Equivalent(a.ParameterUnits, b.ParameterUnits))
                return false;
            if (a.ParameterValue != b.ParameterValue)
                return false;

            return true;
        }

        public Parameter parameter;

        [Description("The parameter object underlying this dimensioned parameter.")]
        public Parameter myParameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        /// <summary>
        /// Forces this dimensioned parameter to take on a manual value with specified units. This will stop use of
        /// any previously used variable.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="units"></param>
        public void forceToManualValue(double value, Units units)
        {
            this.units = units;
            this.parameter.forceToManualValue(value);
        }

        public Units units;

        [Description("The units associated with this dimensioned parameter.")]
        public Units ParameterUnits
        {
            get { return units; }
            set { units = value; }
        }

        [Description("Text depiction of the value and units of this dimensioned parameter.")]
        public string ParameterString
        {
            get
            {
                return this.ToString();
            }
        }

        public void setBaseValue(double value)
        {
            double orderOfMag = Math.Log10(Math.Abs(value));

            Units.Multiplier multToUse = new Units.Multiplier();

            if (value != 0)
            {
                if (orderOfMag >= 7.5)
                {
                    multToUse = Units.Multiplier.G;
                }
                else if (orderOfMag > 4.5)
                {
                    multToUse = Units.Multiplier.M;
                }
                else if (orderOfMag > 1.5)
                {
                    multToUse = Units.Multiplier.k;
                }
                else if (orderOfMag > -1.5)
                {
                    multToUse = Units.Multiplier.unity;
                }
                else if (orderOfMag > -4.5)
                {
                    multToUse = Units.Multiplier.m;
                }
                else if (orderOfMag > -7.5)
                {
                    multToUse = Units.Multiplier.u;
                }
                else
                    multToUse = Units.Multiplier.n;
            }
            else
            {
                multToUse = Units.Multiplier.unity;
            }

            double unMultipliedValue = value / multToUse.getMultiplierFactor();

            this.units.multiplier = multToUse;
            this.parameter.ManualValue = unMultipliedValue;
        }

        [Description("The numerical value of this dimensioned parameter, taking into account unit multipliers.")]
        public double ParameterValue
        {
            get
            {
                return this.getBaseValue();
            }
        }

        public DimensionedParameter(Units units, double manualValue)
        {
            this.units = units;
            this.parameter.forceToManualValue(manualValue);
        }

        public DimensionedParameter(Units.Dimension dim)
        {
            this.parameter.ManualValue = 1;
            this.parameter.variable = null;
            this.units.dimension = dim;
            this.units.multiplier = Units.Multiplier.unity;
        }

        public DimensionedParameter(DimensionedParameter parameter)
        {
            this.parameter = parameter.parameter;
            this.units = parameter.units;
        }

        /*     /// <summary>
             /// This returns the manual value of the parameter in "base units", taking into account the unit multiplier. 
             /// IE, if the parameter is 1, and the units are ms, then this returns .001.
             /// </summary>
             /// <returns></returns>
             public double getManualBaseValue()
             {
                 return units.multiplier * parameter.getManualValue();
             }
             */
        /// <summary>
        /// This returns the value (taking into account variables) of the parameter in base units.
        /// </summary>
        /// <param name="variableValues"></param>
        /// <returns></returns>
        public double getBaseValue()
        {
            return units.multiplier * parameter.getValue();
        }

        /*     /// <summary>
             /// Returns an array of manual base values corresponding to the manual values of
             /// the DimensionedParameters in the list.
             /// </summary>
             /// <param name="?"></param>
             /// <returns></returns>
             public static double [] getManualBaseValues(List<DimensionedParameter> list) 
             {
                 if (list == null) return null;
                 double[] ans = new double[list.Count];
                 for (int i = 0; i < list.Count; i++)
                 {
                     ans[i] = list[i].getManualBaseValue();
                 }
                 return ans;
             }*/

        /// <summary>
        /// Returns an array of base values corresponding to the values of the 
        /// DimensionedParameters in the list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="variableValues"></param>
        /// <returns></returns>
        public static double[] getBaseValues(List<DimensionedParameter> list)
        {
            if (list == null) return null;
            double[] ans = new double[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                ans[i] = list[i].getBaseValue();
            }
            return ans;
        }

        public override string ToString()
        {
            return this.parameter.Value.ToString() + " " + this.units.ToString();
        }

        /// <summary>
        /// Same as ToString, but without the space character
        /// </summary>
        /// <returns></returns>
        public string ToShortString()
        {
            return this.parameter.Value.ToString() + this.units.ToString();
        }
    }
}
