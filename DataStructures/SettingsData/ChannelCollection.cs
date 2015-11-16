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

        public void SwapChannelKeys(int key1, int key2)
        {
            //hold on to logical channels that will be swapped
            LogicalChannel lc1 = Channels[key1];
            LogicalChannel lc2 = Channels[key2];

            //remove logical channels from dictionary
            channels.Remove(key1);
            channels.Remove(key2);

            //re-enter into dictionary, but with swapped keys
            channels.Add(key1, lc2);
            channels.Add(key2, lc1);

        }

        public void AddChannel(LogicalChannel lc)
        {
            channels.Add(GetNextSuggestedKey(), lc);
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
