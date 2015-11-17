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

        public void MoveValue(int key, int target)
        {
            //I'm not sure how this functions should behave: the channels object is a dictionary, which is
            //unordered, and in principle it is such that the key is somehow related to the value. Yet the
            //channels are drawn in an order by their keys...

            //So here is one weird thing: if I remove a channel, I will also remove that key. So there could
            //be holes in the keys, e.g. 1,2,4,5 (3 is missing). But this MoveValue function will fill in the
            //holes, because it will dump the channels dictionary to a list, and then repopulate the dictionary
            //with the proper LogicalChannel moved. The repopulation of the dictionary uses sequential keys, ie
            //1,2,3 etc. So if there was a hole in the keys, it will be filled. Is this important? Does anyway
            //purposely want holes floating around? I dont know.
            
            //hold on to logical channel that will be inserted
            LogicalChannel lc1 = channels[key];
           
            //remove the logical channel that we will late re-insert
            RemoveChannel(key);


            //dump dictionary to a list (only possible because of System.Linq, I think)
            List <KeyValuePair<int, LogicalChannel>> tempList = channels.ToList();

            //sort the list by keys to preserve old ordering when repopulating
            tempList.Sort(
                delegate (KeyValuePair<int, LogicalChannel> p1, KeyValuePair<int,LogicalChannel> p2)
                {
                    return p1.Key.CompareTo(p2.Key);
                });

            int newKey=0;
            channels.Clear();

            //repopulate list, inserting the moved logical channel when we are at the right key value
            foreach(KeyValuePair<int,LogicalChannel> i in tempList)
            {
                if (newKey == target)
                {
                    channels.Add(newKey, lc1);
                    newKey++;
                    channels.Add(newKey, i.Value);
                }
                else
                    channels.Add(newKey, i.Value);

                newKey++;
            }
           

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
