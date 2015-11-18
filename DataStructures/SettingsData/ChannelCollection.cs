using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataStructures
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class ChannelCollection
    {
        private readonly Dictionary<int, LogicalChannel> channels;

        public Dictionary<int, LogicalChannel> Channels
        {
            get { return channels; }
        } 


        public ChannelCollection()
        {
            channels = new Dictionary<int, LogicalChannel>();
        }

        public void AddChannel(LogicalChannel lc)
        {
            channels.Add(GetNextSuggestedKey(), lc);
        }
        public void EditChannel(int oldKey,int newKey,LogicalChannel lc)
        {
            //First remove old channel
            channels.Remove(oldKey);
            //Then check if a channel already has the desired ID
            #region Find if a channel already has the desired ID
            Dictionary<int, LogicalChannel>.KeyCollection keys = channels.Keys;
            int conflictFlag = 0;
            foreach (int i in keys)
                if (i == newKey)
                    conflictFlag = 1;
            #endregion
            //If so, we need to delete THAT channel and re-add it with the old ID
            if (conflictFlag == 1)
            {
                LogicalChannel lc2 = this.Channels[newKey];
                channels.Remove(newKey);
                channels.Add(oldKey, lc2);
                channels.Add(newKey, lc);
            }
            //If not, we just add the new channel at the new ID
            else
            {
                channels.Add(newKey, lc);
            }
        }
        public void SwapChannels(int oldKey, int newKey)
        {

            //First get old channel and delete the old channel;
            LogicalChannel lc1 = this.Channels[oldKey];
            channels.Remove(oldKey);
            
            //Then check if a channel already has the desired ID
            #region Find if a channel already has the desired ID
            Dictionary<int, LogicalChannel>.KeyCollection keys = channels.Keys;
            int conflictFlag = 0;
            foreach (int i in keys)
                if (i == newKey)
                    conflictFlag = 1;
            #endregion
            //If so, we need to delete THAT channel and re-add it with the old ID
            if (conflictFlag == 1)
            {
                LogicalChannel lc2 = this.Channels[newKey];
                channels.Remove(newKey);
                channels.Add(oldKey, lc2);
            }

                channels.Add(newKey, lc1);
        }
        public void RemoveChannel(int logicalID)
        {
            channels.Remove(logicalID);
        }
        public int GetNextSuggestedKey()
        {
            int largestInteger = -1;
            #region Find the largest integer among the integer keys
            Dictionary<int, LogicalChannel>.KeyCollection keys = channels.Keys;
            foreach (int i in keys)
                if (i > largestInteger)
                    largestInteger = i;
            #endregion

            return largestInteger + 1;
        }

        public List<int> getSortedChannelIDList()
        {
            List<int> ans = new List<int>(channels.Keys);
            ans.Sort();
            return ans;
        }

        public int Count
        {
            get
            {
                return channels.Count;
            }
        }
    }
}
