using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataStructures
{

    /// <summary>
    /// This is a generic type that provides the functionality of a group of channel actions, which all start together
    /// on one timestep.
    /// </summary>
    /// <typeparam name="?"></typeparam>
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    abstract public class Group<ChannelDataType> 
        where ChannelDataType : new()
    {
        protected string groupName;

        [Description("Name of the group."),
        Category("Global")]
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }


        public override string ToString()
        {
            return groupName;
        }

        private string groupDescription;

        [Description("Description of the group."),
        Category("Global")]
        public string GroupDescription
        {
            get
            {
                if (groupDescription == null)
                {
                    groupDescription = "";
                }
                return groupDescription;
            }
            set { groupDescription = value; }
        }


        protected Dictionary<int, ChannelDataType> channelDatas;

        [Description("List, indexed by logical channel ID #, of the channel-specific data for each channel in the group.")]
        public Dictionary<int, ChannelDataType> ChannelDatas
        {
            get { return channelDatas; }
            set { channelDatas = value; }
        }


        protected Group()
        {
            this.channelDatas = new Dictionary<int, ChannelDataType>();
        }


        public void addChannel(int channelID, ChannelDataType channelData)
        {
            if (channelDatas.ContainsKey(channelID))
                channelDatas[channelID] = channelData;
            else
                channelDatas.Add(channelID, channelData);
        }

        public void addChannel(int channelID)
        {
            addChannel(channelID, new ChannelDataType());
        }

        public bool containsChannelID(int channelID)
        {
            return channelDatas.ContainsKey(channelID);
        }

        public Group(string groupName)
            : this()
        {
            this.GroupName = groupName;
        }

        /// <summary>
        /// Creates a blank group that contains ChannelData for all of the given channel IDs.
        /// </summary>
        /// <param name="analogGroupName"></param>
        /// <param name="channelIDs"></param>
        public Group(string groupName, List<int> channelIDs)
            : this(groupName)
        {
            foreach (int id in channelIDs)
                channelDatas.Add(id, new ChannelDataType());
        }

        /// <summary>
        /// If given channel ID is already "known" in the group, this returns its channel data. Otherwise, it 
        /// first creates a blank channel data and adds it to the internal colleciton, then returns it.
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public ChannelDataType getChannelData(int channelID)
        {
            if (channelDatas.ContainsKey(channelID))
                return channelDatas[channelID];

            ChannelDataType ans = new ChannelDataType();
            channelDatas.Add(channelID, ans);
            return ans;
        }


        /// <summary>
        /// Returns true if channel data contains a non null data structure for the given channel ID. returns false otherwise.
        /// 
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public bool channelExists(int channelID)
        {
            if (!channelDatas.ContainsKey(channelID))
                return false;
            if (channelDatas[channelID] == null)
                return false;
            return true;
        }



        /// <summary>
        /// Returns a sorted list of the group's known channel IDs. This list is unsorted.
        /// </summary>
        /// <returns></returns>
        public List<int> getChannelIDs()
        {
            List<int> ans = new List<int>();
            foreach (int id in channelDatas.Keys)
                ans.Add(id);
            return ans;
        }

        /// <summary>
        /// Returns a dictionary of all variables used in this group, along with a use descriptor string.
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<Variable, string> usedVariables();

        public abstract Dictionary<Waveform, string> usedWaveforms();

    }
}

