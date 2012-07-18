using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace DataStructures
{
    [TypeConverter(typeof(ExpandableObjectConverter)),
    Serializable]
    public class RetriggerOptions
    {
    
        private bool waitForRetrigger;

        public bool WaitForRetrigger
        {
            get { return waitForRetrigger; }
            set { waitForRetrigger = value; }
        }

        private DimensionedParameter retriggerTimeout;

        public DimensionedParameter RetriggerTimeout
        {
            get { return retriggerTimeout; }
            set { retriggerTimeout = value; }
        }

        private bool retriggerOnEdge;

        public bool RetriggerOnEdge
        {
            get { return retriggerOnEdge; }
            set { retriggerOnEdge = value; }
        }

        private bool retriggerOnNegativeValueOrEdge;

        public bool RetriggerOnNegativeValueOrEdge
        {
            get { return retriggerOnNegativeValueOrEdge; }
            set { retriggerOnNegativeValueOrEdge = value; }
        }

        public RetriggerOptions(
            bool waitForRetrigger,
            bool retriggerOnNegativeValueOrEdge,
            bool retriggerOnEdge,
            DimensionedParameter retriggerTimeout
            )
        {
            this.waitForRetrigger = waitForRetrigger;
            this.retriggerTimeout = retriggerTimeout;
            this.retriggerOnEdge = retriggerOnEdge;
            this.retriggerOnNegativeValueOrEdge = retriggerOnNegativeValueOrEdge;
        }


        public RetriggerOptions(RetriggerOptions copyMe)
        {
            this.retriggerOnEdge = copyMe.retriggerOnEdge;
            this.retriggerOnNegativeValueOrEdge = copyMe.retriggerOnNegativeValueOrEdge;
            this.retriggerTimeout = new DimensionedParameter(copyMe.retriggerTimeout);
            this.waitForRetrigger = copyMe.waitForRetrigger;
        }
    }
}
