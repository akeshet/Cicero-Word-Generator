using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Cicero.DataStructures2
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class RS232Group : Group<RS232GroupChannelData>
    {
        protected override IEnumerable<ResourceID> ReferencedResources_Internal()
        {
            return ChannelDatas.Values.DownCast();
        }

        /*public override Dictionary<Variable, string> usedVariables()
        {
           Dictionary<Variable, string> ans = new Dictionary<Variable, string>();
           foreach (int id in this.ChannelDatas.Keys)
           {
               RS232GroupChannelData data = ChannelDatas[id];
               if (data.StringParameterStrings != null)
               {
                   foreach (StringParameterString sps in data.StringParameterStrings)
                   {
                       if (sps.Parameter.myParameter.variable != null)
                       {
                           if (!ans.ContainsKey(sps.Parameter.myParameter.variable))
                           {
                               ans.Add(sps.Parameter.myParameter.variable, " channel ID " + id + ".");
                           }
                       }
                   }
               }
           }
           return ans;

        }

        public override Dictionary<Waveform, string> usedWaveforms()
        {
            return new Dictionary<Waveform, string>();
        }
        */

        public bool channelEnabled(int rs232ID) { 
            if (!channelDatas.ContainsKey(rs232ID))
                return false;
            return channelDatas[rs232ID].Enabled;
        }

        public RS232Group(string name)
            : base(name)
        {
        }
    }

    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class RS232GroupChannelData : Cicero2DataObject {

        protected override IEnumerable<DataStructures2.ResourceID> ReferencedResources_Internal()
        {
            return StringParameterStrings;
        }

        public enum RS232DataType { 
        /// <summary>
        /// Raw string output.
        /// </summary>
            Raw ,
            Parameter

        };

        /// <summary>
        /// Array of all supported rs-232 data types.
        /// </summary>
        public static readonly RS232DataType[] allDataTypes = new RS232DataType[] { RS232DataType.Raw, RS232DataType.Parameter };


        private RS232DataType dataType;

        public RS232DataType DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        private string rawString;

        public string RawString
        {
            get
            {
                if (rawString == null)
                    rawString = "";

                return rawString;
            }
            set { rawString = value; }
        }

        private bool channelEnabled;

        public bool Enabled
        {
            get { return channelEnabled; }
            set { channelEnabled = value; }
        }

        private List<ResourceID<StringParameterString>> stringParameterStrings;

        public List<ResourceID<StringParameterString>> StringParameterStrings
        {
            get { return stringParameterStrings; }
            set { stringParameterStrings = value; }
        }

    }
}
