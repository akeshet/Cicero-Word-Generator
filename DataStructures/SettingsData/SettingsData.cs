using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataStructures
{
    /// <summary>
    /// The SettingsData class stores permanent application data which does not chance for
    /// different sequence programs. Examples include names of logical channels and their mappings
    /// to hardware channels, and information about what servers to connect to.
    /// </summary>
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class SettingsData
    {
        private LogicalChannelManager myLogicalChannelManager;

        public LogicalChannelManager logicalChannelManager
        {
            get { return myLogicalChannelManager; }
            set { myLogicalChannelManager = value; }
        }

        /// <summary>
        /// Returns a list of servers which have an enabled channel mapped to them, but which are not connected.
        /// If there are no such servers, returns an empty list.
        /// </summary>
        /// <returns></returns>
        public List<string> unconnectedRequiredServers()
        {
            List<string> required = logicalChannelManager.requiredServers();
            List<string> ans = new List<string>();
            foreach (string servername in required)
            {
                if (!serverManager.isServerConnected(servername))
                    ans.Add(servername);
            }
            return ans;
        }

        private List<System.Drawing.Color> colors;

        [Description("Colors to be used for the digital grid panel. To return to default values, remove all elements from the list. Colors can either be selected from pre-existing list, or specified in R,G,B coordinates.")]
        public List<System.Drawing.Color> DigitalGridColors
        {
            get {
                if (colors == null)
                {
                    colors = new List<System.Drawing.Color>();
                }

                if (colors.Count == 0)
                {
                    colors.Add(System.Drawing.Color.RoyalBlue);
                    colors.Add(System.Drawing.Color.OrangeRed);
                    colors.Add(System.Drawing.Color.Teal);
                    colors.Add(System.Drawing.Color.Sienna);
                }

                return colors; }
            set {
                colors = value; }
        }

        private bool outputAnalogDwellValuesOnOutputNow;

        [Description("If false, then when using the Output Now feature to output a timestep, the analog values output will be those at the end of the timestep. If true the analog values will be those from the dwell timestep (the first enabled timestep in the sequence).")]
        public bool OutputAnalogDwellValuesOnOutputNow
        {
            get { return outputAnalogDwellValuesOnOutputNow; }
            set { outputAnalogDwellValuesOnOutputNow = value; }
        }

        private ServerManager myServerManager;

        public ServerManager serverManager
        {
            get { return myServerManager; }
            set { myServerManager = value; }
        }

        public SettingsData()
        {
            myLogicalChannelManager = new LogicalChannelManager();
            myServerManager = new ServerManager();
        }
    }
}
