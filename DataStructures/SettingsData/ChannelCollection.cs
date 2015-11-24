using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Linq;

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
