using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    /// <summary>
    /// This class is used mostly for the "output word now" functionality. It 
    /// provides a mapping from logical channel IDs to channel values. This in general
    /// represents the channel values at, say, the end of a given timestep. 
    /// </summary>
    [Serializable]
    public class SingleOutputFrame
    {
        /// <summary>
        /// A mapping from logical channel ID to analog channel value.
        /// </summary>
        public Dictionary<int, double> analogValues;

        /// <summary>
        /// A mapping from logical channel ID to digital channel value.
        /// </summary>
        public Dictionary<int, bool> digitalValues;

        public SingleOutputFrame()
        {
            analogValues = new Dictionary<int, double>();
            digitalValues = new Dictionary<int, bool>();
        }
    }
}
