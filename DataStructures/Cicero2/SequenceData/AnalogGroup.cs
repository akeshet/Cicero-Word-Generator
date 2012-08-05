using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Cicero.DataStructures2
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class AnalogGroup : Group<AnalogGroupChannelData>
    {

        protected override IEnumerable<ResourceID> ReferencedResources_Internal()
        {
            List<ResourceID> ans = new List<ResourceID>();
            ans.Add(TimeResolution);
            ans.AddRange(ChannelDatas.Values.DownCast());
            return ans;
        }

        private ResourceID<DimensionedParameter> timeResolution;

        public ResourceID<DimensionedParameter> TimeResolution
        {
            get {
                //default time resolution is 1 ms.
/*                if (timeResolution == null)
                {
                    timeResolution = new DimensionedParameter(new Units("ms"), 1);
                }*/
                return timeResolution; }
            set { timeResolution = value; }
        }


        public AnalogGroup(string groupName)
            : base(groupName)
        {
        }


        public void addChannel(int channelID, ResourceID<Waveform> waveform, bool enabled, bool common, Cicero2ResourceDictionary resources)
        {
            addChannel(channelID, resources.AddNew(new AnalogGroupChannelData(waveform, enabled, common)));
        }

        /// <summary>
        /// Returns a list of the analog group's waveforms, sorted by channel ID.
        /// </summary>
        /// <returns></returns>
        public List<ResourceID<Waveform>> getAnalogGroupWaveforms(Cicero2ResourceDictionary resources)
        {
            List<int> channelIDs = getAnalogGroupWaveformChannelIDs(resources);
            List<ResourceID<Waveform>> wfs = new List<ResourceID<Waveform>>();
            foreach (int id in channelIDs)
            {
                wfs.Add(resources.Get(channelDatas[id]).waveform);
            }
            return wfs;
        }

        /// <summary>
        /// Returns a list of channel id #s that correspond to the waveforms returned in getAnalogGroupWaveforms. 
        /// Note that only channel IDs which have their common waveform bit set to false will be in this list.
        /// </summary>
        /// <returns></returns>
        public List<int> getAnalogGroupWaveformChannelIDs(Cicero2ResourceDictionary resources)
        {
            List<int> channelIDs = new List<int>();
            foreach (int id in channelDatas.Keys)
            {
                if (!resources.Get(channelDatas[id]).ChannelWaveformIsCommon)
                    channelIDs.Add(id);
            }

            channelIDs.Sort();
            return channelIDs;
        }

        public bool channelEnabled(int channelID, Cicero2ResourceDictionary resources)
        {
            if (!channelExists(channelID))
                return false;
            return resources.Get(channelDatas[channelID]).ChannelEnabled;
        }

        public double getEffectiveDuration(Cicero2ResourceDictionary resources)
        {
            double ans = 0;
            foreach (ResourceID<AnalogGroupChannelData> channelData in ChannelDatas.Values)
            {
                if (resources.Get(channelData).ChannelEnabled)
                {
                    if (resources.Get(channelData).waveform != null)
                    {
                        double temp = resources.Get(channelData).waveform.getEffectiveWaveformDuration(resources);
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
    public class AnalogGroupChannelData : Cicero2DataObject
    {
        protected override IEnumerable<ResourceID> ReferencedResources_Internal()
        {
            return new ResourceID[] {
                this.waveform
            };
        }

        /// <summary>
        /// Contains a dictionary of waveforms associated with channel ID#s. Note, these waveforms may point to a common waveform,
        /// in which case they should not be editable from within the analog group editor.
        /// </summary>
        private ResourceID<Waveform> myWaveform;

        [Description("The waveform assigned to this channel.")]
        public ResourceID<Waveform> waveform
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

        public AnalogGroupChannelData(ResourceID<Waveform> waveform, bool channelEnabled, bool channelWaveformIsCommon)
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
