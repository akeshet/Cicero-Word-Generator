using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace DataStructures
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class AnalogGroup : Group<AnalogGroupChannelData>
    {


        private DimensionedParameter timeResolution;

        public DimensionedParameter TimeResolution
        {
            get {
                //default time resolution is 1 ms.
                if (timeResolution == null)
                {
                    timeResolution = new DimensionedParameter(new Units("ms"), 1);
                }
                return timeResolution; }
            set { timeResolution = value; }
        }


        public AnalogGroup(string groupName)
            : base(groupName)
        {
        }


        public void addChannel(int channelID, Waveform waveform, bool enabled, bool common)
        {
            addChannel(channelID, new AnalogGroupChannelData(waveform, enabled, common));
        }

        public override Dictionary<Variable, string> usedVariables()
        {
            Dictionary<Variable, string> ans = new Dictionary<Variable, string>();

            foreach (int id in channelDatas.Keys)
            {
                if (channelDatas[id] != null)
                {
                    if (channelDatas[id].waveform != null)
                    {
                        Dictionary<Variable, string> temp = channelDatas[id].waveform.usedVariables();
                        foreach (Variable var in temp.Keys)
                        {
                            if (!ans.ContainsKey(var))
                            {
                                ans.Add(var, "Waveform for channel id " + id + " " + temp[var]);
                            }
                        }
                    }
                }
            }

            if (timeResolution.myParameter.variable != null)
            {
                ans.Add(timeResolution.myParameter.variable, "Time resolution.");
            }

            return ans;
        }


        public override Dictionary<Waveform, string> usedWaveforms()
        {
            Dictionary<Waveform, string> ans = new Dictionary<Waveform, string>();

            foreach (int id in channelDatas.Keys)
            {
                if (channelDatas[id] != null)
                {
                    if (channelDatas[id].waveform != null)
                    {
                        if (!ans.ContainsKey(channelDatas[id].waveform))
                        {
                            ans.Add(channelDatas[id].waveform, "Channel " + id);
                        }
                    }
                }
            }
            return ans;
        }


        /// <summary>
        /// Returns a list of the analog group's waveforms, sorted by channel ID.
        /// </summary>
        /// <returns></returns>
        public List<Waveform> getAnalogGroupWaveforms()
        {
            List<int> channelIDs = getAnalogGroupWaveformChannelIDs();
            List<Waveform> wfs = new List<Waveform>();
            foreach (int id in channelIDs)
            {
                wfs.Add(channelDatas[id].waveform);
            }
            return wfs;
        }

        /// <summary>
        /// Returns a list of channel id #s that correspond to the waveforms returned in getAnalogGroupWaveforms. 
        /// Note that only channel IDs which have their common waveform bit set to false will be in this list.
        /// </summary>
        /// <returns></returns>
        public List<int> getAnalogGroupWaveformChannelIDs()
        {
            List<int> channelIDs = new List<int>();
            foreach (int id in channelDatas.Keys)
            {
                if (!channelDatas[id].ChannelWaveformIsCommon)
                    channelIDs.Add(id);
            }

            channelIDs.Sort();
            return channelIDs;
        }

        public bool channelEnabled(int channelID)
        {
            if (!channelExists(channelID))
                return false;
            return channelDatas[channelID].ChannelEnabled;
        }

        public double getEffectiveDuration()
        {
            double ans = 0;
            foreach (AnalogGroupChannelData channelData in ChannelDatas.Values)
            {
                if (channelData.ChannelEnabled)
                {
                    if (channelData.waveform != null)
                    {
                        double temp = channelData.waveform.getEffectiveWaveformDuration();
                        if (temp > ans)
                            ans = temp;
                    }
                }
            }
            return ans;
        }

        

    }

    


    #region AnalogGroupChannelData

    /// <summary>
    /// Contains all the channel-specific information in an analog group.
    /// </summary>
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class AnalogGroupChannelData
    {
        /// <summary>
        /// Contains a dictionary of waveforms associated with channel ID#s. Note, these waveforms may point to a common waveform,
        /// in which case they should not be editable from within the analog group editor.
        /// </summary>
        private Waveform myWaveform;

        [Description("The waveform assigned to this channel.")]
        public Waveform waveform
        {
            get { return myWaveform; }
            set { myWaveform = value; }
        }

        /// <summary>
        /// True if the specified channel ID is enabled, false if "continue".
        /// </summary>
        private bool channelEnabled;

        [Description("True is the channel is enabled in this group. False of the channel is to \"Continue\"")]
        public bool ChannelEnabled
        {
            get { return channelEnabled; }
            set { channelEnabled = value; }
        }

        /// <summary>
        /// True if the specified channel ID's waveform is a common waveform.
        /// </summary>
        private bool channelWaveformIsCommon;

        [Description("True of the waveform that this channel is assigned to is one of the sequence\'s Common Waveforms.")]
        public bool ChannelWaveformIsCommon
        {
            get { return channelWaveformIsCommon; }
            set { channelWaveformIsCommon = value; }
        }

        public AnalogGroupChannelData(Waveform waveform, bool channelEnabled, bool channelWaveformIsCommon)
        {
            this.myWaveform = waveform;
            this.channelEnabled = channelEnabled;
            this.channelWaveformIsCommon = channelWaveformIsCommon;
        }

        public AnalogGroupChannelData()
            : this(new Waveform(), false, false)
        {
        }
    }

    #endregion
}
