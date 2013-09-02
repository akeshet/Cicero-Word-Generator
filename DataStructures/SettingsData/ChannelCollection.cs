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
            drawGUIMap = new Map<int, int>();
        }


        //MakeGUIMap is necessary for backwards compatibility with previous settings files.
        //If there exists a settings file which was saved before the drawGUIMap feature,
        //then when that settings file is deserialized the ChannelCollection() constructor
        //will never be run, hence drawGUIMap will be null and this will result in problems when the
        //code tries to use it to draw.

        //To fix this, MakeGUIMap is like a constructor only for drawGUIMap, which will be run if a 
        //NullReferenceException exception is caught
        public void MakeGUIMap()
        {
            drawGUIMap = new Map<int, int>();
            foreach (int key in channels.Keys)
                drawGUIMap.Add(key, key);
        }

        public void AddChannel(LogicalChannel lc)
        {
            int next_key = GetNextSuggestedKey();
            channels.Add(next_key, lc);

            try
            {
                drawGUIMap.Add(next_key, next_key);
            }
            catch (NullReferenceException e)
            {
                MakeGUIMap();
                drawGUIMap.Add(next_key, next_key);
            }
            
        }
        public void RemoveChannel(int logicalID)
        {
            channels.Remove(logicalID);

            try
            {
                drawGUIMap.RemoveBySecondKey(logicalID);
            }
            catch (NullReferenceException e)
            {
                MakeGUIMap();
                drawGUIMap.RemoveBySecondKey(logicalID);
            }
            
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

        public int GetDrawID(int logicalID)
        {
            try
            {
                return drawGUIMap.GetByFirstKey(logicalID);
            }
            catch (NullReferenceException e)
            {
                MakeGUIMap();
                return drawGUIMap.GetByFirstKey(logicalID);
            }
            
        }

        //Map to order channels when drawing gui.
        //First Key is draw ID, Second Key logical ID
        private Map<int, int> drawGUIMap;

        public Map<int, int> DrawGUIMap
        {
            get
            {
                try
                {
                    return drawGUIMap;
                }
                catch (NullReferenceException e)
                {
                    MakeGUIMap();
                    return drawGUIMap;
                }
            }
         }

        public List<int> getSortedChannelIDList()
        {
            List<int> ans = new List<int>(channels.Keys);
            ans.Sort();
            return ans;
        }

        public List<int> getSortedDrawIDList()
        {
            List<int> ans = new List<int> (drawGUIMap.GetFirstKeys());
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


    //To make a drawMap, which is just a 1-to-1 map between two sets of integers (drawID and logicalID)
    //I have to make the map myself, because .NET doesn't have a Map datatype.
    //This is based on a stackoverflow answer:
    //http://stackoverflow.com/questions/10966331/two-way-bidirectional-dictionary-in-c

    public class Map<T1, T2>
    {
        private Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
        private Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();

        public Map()
        {
            this.Forward = new Indexer<T1, T2>(_forward);
            this.Reverse = new Indexer<T2, T1>(_reverse);
        }

        public class Indexer<T3, T4>
        {
            private Dictionary<T3, T4> _dictionary;
            public Indexer(Dictionary<T3, T4> dictionary)
            {
                _dictionary = dictionary;
            }
            public T4 this[T3 index]
            {
                get { return _dictionary[index]; }
                set { _dictionary[index] = value; }
            }
        }

        public void Add(T1 t1, T2 t2)
        {
            _forward.Add(t1, t2);
            _reverse.Add(t2, t1);
        }

        public T2 GetByFirstKey(T1 t1)
        {
            return _forward[t1];
        }

        public T1 GetBySecondKey(T2 t2)
        {
            return _reverse[t2];
        }

        public void RemoveByFirstKey(T1 t1)
        {
            _reverse.Remove(_forward[t1]);
            _forward.Remove(t1);
        
        }
        public void RemoveBySecondKey(T2 t2)
        {
            _forward.Remove(_reverse[t2]);
            _reverse.Remove(t2);
        }

        public Dictionary<T1,T2>.KeyCollection GetFirstKeys()
        {
            return _forward.Keys;
        }
      

        public Indexer<T1, T2> Forward { get; private set; }
        public Indexer<T2, T1> Reverse { get; private set; }
    }
}
