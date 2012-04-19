using System;
using System.Collections.Generic;

using DataStructures;

namespace WordGenerator.ChannelManager
{
    /// <summary>
    /// The SelectedDevice class encapsulates all the things that we might want to know about a selected
    /// logical device as we go to edit/delete it.
    /// </summary>
    public class SelectedDevice
    {
        public HardwareChannel.HardwareConstants.ChannelTypes channelType;
        public string channelTypeString;
        public int logicalID;
        public LogicalChannel lc;

        public SelectedDevice(string selectedTypeString, HardwareChannel.HardwareConstants.ChannelTypes channelType, int logicalID, LogicalChannel lc)
        {
            this.channelTypeString = selectedTypeString;
            this.channelType = channelType;
            this.logicalID = logicalID;
            this.lc = lc;
        }
    }
}
