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

        private Dictionary<string, double> permanentVariables;

        public Dictionary<string, double> PermanentVariables
        {
            get
            {
                if (permanentVariables == null)
                    permanentVariables = new Dictionary<string, double>();

                return permanentVariables;
            }
            set { permanentVariables = value; }
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

        private bool useCameras;

        [Description("If true, instructions are sent to cameras."),
        Category("Cameras")]
        public bool UseCameras
        {
            get {return useCameras; }
            set { useCameras = value; }
        }

        private List<IPAdresses> cameraPCs;

        [TypeConverter(typeof(ExpandableObjectConverter)), Serializable]
        public class IPAdresses
        {
            private string PcAddress;

            public string pcAddress
            {
                get { return PcAddress; }
                set { PcAddress = value; }
            }

            private int port;
            
            public int Port
            {
                get { return port; }
                set { port = value; }
            }

            private bool InUse;

            public bool inUse
            {
                get { return InUse; }
                set { InUse = value; }
            }

            private bool UseFWCamera;

            public bool useFWCamera
            {
                get { return UseFWCamera; }
                set { UseFWCamera = value; }
            }

            private bool UseUSBCamera;

            public bool useUSBCamera
            {
                get { return UseUSBCamera; }
                set { UseUSBCamera = value; }
            }

            public IPAdresses()
            {
                PcAddress = "";
                port = 66666;
                InUse = true;
                UseUSBCamera = true;
                UseFWCamera = true;
            }
        }

        [Description("List of the IP Adresses where the cameras are."),
        Category("Cameras")]
        public List<IPAdresses> CameraPCs
        {
            get
            {
                if (cameraPCs == null)
                    cameraPCs = new List<IPAdresses>();

                return cameraPCs;
            }
            set { cameraPCs = value; }
        }

        private bool freezeTimeVariableUnit;

        [Description("If true, time variables will all be used in ms.")]
        public bool FreezeTimeVariableUnit
        {
            get { return freezeTimeVariableUnit; }
            set { freezeTimeVariableUnit = value; }

        }

        private string savePath;

        [Description("Sets the path in which any file produced should be saved"),
        Category("Saving")]
        public string SavePath
        {
            get {
                if (savePath == null)
                    savePath = "";
                return savePath; 
            }
            set { savePath = value; }

        }

        bool useParisStyleFileTimestamps;

        [Description("If false, original MIT-style file timestamps will be used. If true, Paris-style file ."),
        Category("Saving"),DefaultValue(false)]
        public bool UseParisStyleFileTimestamps
        {
            get { return useParisStyleFileTimestamps; }
            set { useParisStyleFileTimestamps = value; }

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
            cameraPCs = new List<IPAdresses>();
        }



        private List<DeviceTimingOverride> deviceTimingOverrides;

        public List<DeviceTimingOverride> DeviceTimingOverrides
        {
            get
            {
                if (deviceTimingOverrides == null)
                    deviceTimingOverrides = new List<DeviceTimingOverride>();
                return deviceTimingOverrides;
            }
            set { deviceTimingOverrides = value; }
        }


        [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
        public class DeviceTimingOverride
        {
            public enum OverrideType { None, VariableTimebaseOverride }

            private string deviceName;

            public string DeviceName
            {
                get { return deviceName; }
                set { deviceName = value; }
            }

            private string serverName;

            public string ServerName
            {
                get { return serverName; }
                set { serverName = value; }
            }

            private string overrideIdentifier;

            public string OverrideIdentifier
            {
                get { return overrideIdentifier; }
                set { overrideIdentifier = value; }
            }
            private OverrideType myOverrideType;

            public OverrideType MyOverrideType
            {
                get { return myOverrideType; }
                set { myOverrideType = value; }
            }
            private bool enabled;

            public bool Enabled
            {
                get { return enabled; }
                set { enabled = value; }
            }

            public DeviceTimingOverride()
            {
                deviceName = "";
                serverName = "";
                overrideIdentifier = "";
                myOverrideType = OverrideType.None;
                enabled = false;
            }

        }


#region Helper code for using variable frequency clocks that are differen per-device

        /// <summary>
        /// Returns "default" if there is no override for this device. (ie we should use the "default" variable timebase)
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public string getVariableTimebaseIdentifier(string deviceName, string serverName)
        {
            string ans = null;
            foreach (DeviceTimingOverride over in this.DeviceTimingOverrides)
            {
                if (over.ServerName == serverName && over.DeviceName == deviceName && over.Enabled 
                    && over.MyOverrideType == DeviceTimingOverride.OverrideType.VariableTimebaseOverride)
                {
                    ans = over.OverrideIdentifier;
                }
            }

            if (ans == null)
                return "default";
            return ans;
        }

        public List<int> getIgnoredAnalogsForVariableTimebaseGeneration(string timebaseIdentifier)
        {
            List<int> ans = new List<int>();

            // caches results of calls to getVariableTimebaseIdentifier
            Dictionary<string, string> identifierCache = new Dictionary<string, string>();


            foreach (int channelID in this.logicalChannelManager.Analogs.Keys)
            {
                LogicalChannel channel = this.logicalChannelManager.Analogs[channelID];
                if (channel.hardwareChannel == null || channel.hardwareChannel.isUnAssigned)
                {
                    ans.Add(channelID);
                }
                else
                {

                    string deviceName = channel.hardwareChannel.DeviceName;
                    string serverName = channel.hardwareChannel.ServerName;
                    string temp = serverName + "/" + deviceName;
                    if (!identifierCache.ContainsKey(temp))
                    {
                        identifierCache.Add(temp, getVariableTimebaseIdentifier(deviceName, serverName));
                    }
                    string channelTimebaseIdentifier = identifierCache[temp];

                    if (timebaseIdentifier != channelTimebaseIdentifier)
                        ans.Add(channelID);

                }
            }

            return ans;
        }

        public List<int> getIgnoredDigitalsForVariableTimebaseGeneration(string timebaseIdentifier)
        {
            List<int> ans = new List<int>();

            // caches results of calls to getVariableTimebaseIdentifier
            Dictionary<string, string> identifierCache = new Dictionary<string, string>();


            foreach (int channelID in this.logicalChannelManager.Digitals.Keys)
            {
                LogicalChannel channel = this.logicalChannelManager.Digitals[channelID];
                if (channel.hardwareChannel == null || channel.hardwareChannel.isUnAssigned)
                {
                    ans.Add(channelID);
                }
                else
                {

                    string deviceName = channel.hardwareChannel.DeviceName;
                    string serverName = channel.hardwareChannel.ServerName;
                    string temp = serverName + "/" + deviceName;
                    if (!identifierCache.ContainsKey(temp))
                    {
                        identifierCache.Add(temp, getVariableTimebaseIdentifier(deviceName, serverName));
                    }
                    string channelTimebaseIdentifier = identifierCache[temp];

                    if (timebaseIdentifier != channelTimebaseIdentifier)
                        ans.Add(channelID);

                }
            }

            return ans;
        }

#endregion


    }
}
