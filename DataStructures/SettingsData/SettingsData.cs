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
            get { return cameraPCs; }
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
            get { return savePath; }
            set { savePath = value; }

        }

        private bool useMitFileStamp=true;

        [Description("Decides whether the old naming pattern should be used."),
        Category("Saving"),DefaultValue(true)]
        public bool UseMitFileStamp
        {
            get { return useMitFileStamp; }
            set { useMitFileStamp = value; }

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
            useMitFileStamp = true;
        }
    }
}
