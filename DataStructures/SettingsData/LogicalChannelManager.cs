using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel;


namespace DataStructures
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class LogicalChannelManager
    {
        private Dictionary<HardwareChannel.HardwareConstants.ChannelTypes, ChannelCollection> channelCollections;

        public Dictionary<HardwareChannel.HardwareConstants.ChannelTypes, ChannelCollection> ChannelCollections
        {
            get { return channelCollections; }
            set { channelCollections = value; }
        }


        public List<HardwareChannel> AssignedHardwareChannels
        {
            get
            {
                List<HardwareChannel> ans = new List<HardwareChannel>();
                foreach (ChannelCollection channelCollection in ChannelCollections.Values)
                {
                    foreach (LogicalChannel logicalChannel in channelCollection.Channels.Values)
                    {
                        if (logicalChannel.HardwareChannel != HardwareChannel.Unassigned)
                            ans.Add(logicalChannel.HardwareChannel);
                    }
                }
                return ans;
            }
        }

        /// <summary>
        /// Returns a list of the names of servers which have channels mapped to them.
        /// </summary>
        /// <returns></returns>
        public List<string> requiredServers()
        {
            List<string> ans = new List<string>();
            foreach (ChannelCollection coll in channelCollections.Values)
            {
                foreach (LogicalChannel lc in coll.Channels.Values)
                {
                    if (lc.HardwareChannel != HardwareChannel.Unassigned)
                    {
                        if (!ans.Contains(lc.HardwareChannel.ServerName))
                            ans.Add(lc.HardwareChannel.ServerName);
                    }
                }
            }
            return ans;
        }

        public Dictionary<int, LogicalChannel> Analogs
        {
            get
            {
                return channelCollections[HardwareChannel.HardwareConstants.ChannelTypes.analog].Channels;
            }
        }

        public Dictionary<int, LogicalChannel> Digitals
        {
            get
            {
                return channelCollections[HardwareChannel.HardwareConstants.ChannelTypes.digital].Channels;
            }
        }

        public Dictionary<int, LogicalChannel> GPIBs
        {
            get
            {
                return channelCollections[HardwareChannel.HardwareConstants.ChannelTypes.gpib].Channels;
            }
        }

        public Dictionary<int, LogicalChannel> RS232s
        {
            get
            {
                return channelCollections[HardwareChannel.HardwareConstants.ChannelTypes.rs232].Channels;
            }
        }

        public LogicalChannelManager()
        {
            channelCollections = new Dictionary<HardwareChannel.HardwareConstants.ChannelTypes, ChannelCollection>();


            foreach (HardwareChannel.HardwareConstants.ChannelTypes ct in HardwareChannel.HardwareConstants.allChannelTypes)
                channelCollections.Add(ct, new ChannelCollection());
        }

        public ChannelCollection GetDeviceCollection(HardwareChannel.HardwareConstants.ChannelTypes ct)
        {
            return channelCollections[ct];
        }


        [OnDeserialized]
        public void ensureProperDeserialization(StreamingContext sc)
        {
            if (channelCollections == null)
            {
                channelCollections = new Dictionary<HardwareChannel.HardwareConstants.ChannelTypes, ChannelCollection>();
            }
            else
            {
                channelCollections.OnDeserialization(this);
            }

            foreach (HardwareChannel.HardwareConstants.ChannelTypes ct in HardwareChannel.HardwareConstants.allChannelTypes)
            {
                if (!channelCollections.ContainsKey(ct))
                {
                    ChannelCollection dc = new ChannelCollection();
                    channelCollections.Add(ct, dc);
                }
            }
        }       
    }
}
