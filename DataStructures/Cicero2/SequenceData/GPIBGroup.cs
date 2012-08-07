using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using EnumWrapperTypeConverter = DataStructures.EnumWrapperTypeConverter;
using System.Linq;

namespace Cicero.DataStructures2
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class GPIBGroup : Group<GPIBGroupChannelData>
    {

        protected override IEnumerable<ResourceID> ReferencedResources_Internal()
        {
            return this.ChannelDatas.Values.DownCast();
        }

        public bool channelEnabled(int channelID)
        {
            if (!channelExists(channelID))
                return false;
            return channelDatas[channelID].Enabled;
        }

        /*public override Dictionary<Variable, string> usedVariables()
        {
            Dictionary<Variable, string> ans = new Dictionary<Variable,string>();

            foreach (int id in this.ChannelDatas.Keys)
            {
                if (ChannelDatas[id] != null)
                {
                    Dictionary<Variable, string> temp = ChannelDatas[id].usedVariables();
                    foreach (Variable var in temp.Keys)
                    {
                        if (!ans.ContainsKey(var))
                        {
                            ans.Add(var, "Channel ID " + id + " " + temp[var]);
                        }
                    }
                }
            }

            return ans;
        }*/

       /* public override Dictionary<Waveform, string> usedWaveforms()
        {
            Dictionary<Waveform, string> ans = new Dictionary<Waveform, string>();

            foreach (int id in this.ChannelDatas.Keys)
            {
                if (ChannelDatas[id] != null)
                {
                    if (ChannelDatas[id].DataType == GPIBGroupChannelData.GpibChannelDataType.voltage_frequency_waveform)
                    {
                        ans.Add(ChannelDatas[id].volts, "Channel id " + id);
                        ans.Add(ChannelDatas[id].frequency, "Channel id " + id);
                    }
                }
            }

            return ans;
        }*/


    }



    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class GPIBGroupChannelData : Cicero2DataObject
    {

        protected override IEnumerable<ResourceID> ReferencedResources_Internal()
        {
            List<ResourceID> ans = new List<ResourceID>();
            ans.AddRange(StringParameterStrings.ReferencedResources_Internal());
            ans.Add(this.frequency);
            ans.Add(this.volts);
            return ans;
        }

        [Serializable, TypeConverter(typeof(GpibChannelDataType.GpibChannelDataTypeConverter))]
        public struct GpibChannelDataType
        {
            public class GpibChannelDataTypeConverter : EnumWrapperTypeConverter
            {
                public GpibChannelDataTypeConverter()
                    : base(allTypes)
                {
                }
            }

            public static bool operator==(GpibChannelDataType sonny, GpibChannelDataType cher)
            {
                return sonny.myType == cher.myType;
            }

            public static bool operator!=(GpibChannelDataType sonny, GpibChannelDataType cher) {
                return ! (sonny == cher);
            }

            public override bool Equals(object obj)
            {
                if (!(obj is GpibChannelDataType))
                    return false;
                GpibChannelDataType other = (GpibChannelDataType)obj;
                return this == other;
            }

            public override int GetHashCode()
            {
                return 1;
            }

            private enum TypeEnum { raw_string, voltage_frequency_waveform, string_param_string };
            private static readonly string[] names = new string[] { "Raw", "A+F Ramp", "Parameter" };

            private GpibChannelDataType(TypeEnum myType)
            {
                this.myType = myType;
            }

            private TypeEnum myType;

            public static readonly GpibChannelDataType raw_string = new GpibChannelDataType(TypeEnum.raw_string);
            public static readonly GpibChannelDataType voltage_frequency_waveform = new GpibChannelDataType(TypeEnum.voltage_frequency_waveform);
            public static readonly GpibChannelDataType string_param_string = new GpibChannelDataType(TypeEnum.string_param_string);

            public static readonly GpibChannelDataType[] allTypes = new GpibChannelDataType[] { raw_string, voltage_frequency_waveform, string_param_string };

            public override string ToString()
            {
                return names[(int)myType];
            }

 
        }


        private GpibChannelDataType dataType;

        public GpibChannelDataType DataType
        {
            get { return dataType; }
            set { 
                dataType = value;
                if (dataType == GpibChannelDataType.voltage_frequency_waveform)
                {
                    if (volts == null)
                        volts = new Waveform();
                    if (frequency == null)
                        frequency = new Waveform(Units.Dimension.Hz);
                }
            }
        }

        private bool enabled;

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        // for now, gpib only supports volt-frequency output to our ESG series agilent microwave generator
        // thus some of the other options are commented out.
        /*
        
        public enum GPIBDataStyle { Text, VoltFreq };
        private string rawOutputString;

        public string RawOutputString
        {
            get
            {
                if (dataStyle == GPIBDataStyle.Text)
                    return rawOutputString;
                else
                    return "";
            }
            set
            {
                if (dataStyle == GPIBDataStyle.Text)
                    rawOutputString = value;
            }
        }
        private GPIBDataStyle dataStyle;

        public GPIBDataStyle DataStyle
        {
            get { return dataStyle; }
            set { dataStyle = value; }
        }*/

        public ResourceID<Waveform> volts;



        public ResourceID<Waveform> frequency;

        private string rawString;


        
            public Dictionary<Variable, string> usedVariables()
            {
                Dictionary<Variable, string> ans = new Dictionary<Variable, string>();
                if (this.volts != null)
                {
                    Dictionary<Variable, string> temp = volts.usedVariables();
                    foreach (Variable var in temp.Keys)
                    {
                        ans.Add(var, "Voltage waveform " + " " + temp[var]);
                    }
                }
                if (this.frequency != null)
                {
                    Dictionary<Variable, string> temp = frequency.usedVariables();
                    foreach (Variable var in temp.Keys)
                    {
                        if (!ans.ContainsKey(var))
                        {
                            ans.Add(var, "Frequency waveform " + " " + temp[var]);
                        }
                    }
                }
                if (this.stringParameterStrings != null)
                {
                    foreach (StringParameterString sps in this.stringParameterStrings)
                    {
                        if (sps.Parameter.myParameter.variable != null)
                        {
                            if (!ans.ContainsKey(sps.Parameter.myParameter.variable)) {
                                ans.Add(sps.Parameter.myParameter.variable, "Parameter");
                            }
                        }
                    }
                }

                return ans;
            }



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

        public GPIBGroupChannelData()
        {
            this.dataType = GpibChannelDataType.raw_string;
            this.enabled = false;
        }

        private List<StringParameterString> stringParameterStrings;

        public List<StringParameterString> StringParameterStrings
        {
            get { return stringParameterStrings; }
            set { stringParameterStrings = value; }
        }

    }

}
