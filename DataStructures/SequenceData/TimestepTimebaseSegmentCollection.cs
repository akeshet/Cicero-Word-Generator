using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataStructures
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class VariableTimebaseSegmentCollection : List<SequenceData.VariableTimebaseSegment>
    {
        public VariableTimebaseSegmentCollection()
            : base()
        {
        }

        public override string ToString()
        {
            int ss=0;
            int ms = 0;
            foreach (SequenceData.VariableTimebaseSegment seg in this)
            {
                ss += seg.NSegmentSamples;
                ms += seg.NSegmentSamples * seg.MasterSamplesPerSegmentSample;
            }

            return this.Count + " segs, " + ss + " segsamps, " + ms + " mastsamps.";
        }
    }

 

    public class TimestepTimebaseSegmentCollection : Dictionary<TimeStep, VariableTimebaseSegmentCollection>
    {
        public TimestepTimebaseSegmentCollection() : base()
        {
            
        }

        public int nSegmentSamples(TimeStep ts)
        {
            int ans=0;
            if (this.ContainsKey(ts))
            {
                foreach (SequenceData.VariableTimebaseSegment seg in this[ts])
                {
                    ans += seg.NSegmentSamples;
                }
                return ans;
            }
            else
            {
                return 0;
            }
        }

        public int nMasterSamples(TimeStep ts)
        {
            int ans = 0;
            if (this.ContainsKey(ts))
            {
                foreach (SequenceData.VariableTimebaseSegment segment in this[ts])
                {
                    ans += segment.MasterSamplesPerSegmentSample * segment.NSegmentSamples;
                }
            }
            return ans;
        }

        public int nMasterSamples()
        {
            int ans = 0;
            foreach (TimeStep step in this.Keys)
            {
                ans += this.nMasterSamples(step);
            }
            return ans;
        }

        public int nSegmentSamples()
        {
            int ans = 0;
            foreach (TimeStep step in this.Keys)
            {
                ans += this.nSegmentSamples(step);
            }
            return ans;
        }
    }
}
