using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataStructures
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class DigitalDataPoint
    {
        private Parameter parameter;

        private Pulse digitalPulse;

        public Pulse DigitalPulse
        {
            get { return digitalPulse; }
            set { digitalPulse = value; }
        }

        /// <summary>
        /// Returns true if DigitalPulse is not null.
        /// </summary>
        /// <returns></returns>
        public bool usesPulse()
        {
            if (digitalPulse!=null) {
                return true;
            }
            return false;
        }

        public DigitalDataPoint()
        {
            parameter.variable = null;
            parameter.ManualValue = 0;
        
        }

        public DigitalDataPoint(DigitalDataPoint copyMe)
        {
            // parameter is a struct, so this is a copy operation
            this.parameter = copyMe.parameter;
            this.digitalPulse = copyMe.digitalPulse;
        }


        public bool ManualValue
        {
            get
            {
                if (parameter.ManualValue == 0)
                    return false;
                return true;
            }
            set
            {
                if (value)
                    parameter.ManualValue = 1;
                else
                    parameter.ManualValue = 0;
            }
        }

        public Variable variable
        {
            get
            {
                return parameter.variable;
            }
            set
            {
                parameter.variable = value;
            }
        }

        public bool getValue()
        {
            double temp = parameter.getValue();
            if (temp == 0)
                return false;
            else
                return true;
        }
    }
}
