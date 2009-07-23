using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace AtticusServer
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class AnalogInLogTime
    {
        

        private int timeStep;
        [Description("Timestep at the beginning of which data will be saved.")]
        [Category("Data will be saved at this timestep")]
        public int TimeStep
        {
            get { return timeStep; }
            set { timeStep = value; }
        }

        private int timeBefore;
        [Description("Time in milliseconds when the data will start being saved, relative to the beginning of the timestep.")]
        [Category("From (Time before TimeStep in ms)")]
        public int TimeBefore
        {
            get { return timeBefore; }
            set { timeBefore = value; }
        }

        private int timeAfter;
        [Description("Time in milliseconds when the data will start being saved, relative to the beginning of the timestep.")]
        [Category("To (Time after TimeStep in ms)")]
        public int TimeAfter
        {
            get { return timeAfter; }
            set { timeAfter = value; }
        }


        public AnalogInLogTime()
        {
            timeStep = 2;
            timeBefore = 10;
            timeAfter = 10;
        }
    }
}
