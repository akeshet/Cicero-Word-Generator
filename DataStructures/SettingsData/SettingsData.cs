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
            get
            {
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
        Category("Paris-fork options")]
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
        Category("Paris-fork options")]
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

        [Description("If true, time variables will all be used in ms."),
        Category("Paris-fork options")]
        public bool FreezeTimeVariableUnit
        {
            get { return freezeTimeVariableUnit; }
            set { freezeTimeVariableUnit = value; }

        }

        private string savePath;

        [Description("Sets the path in which any file produced should be saved"),
        Category("Paris-fork options")]
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
        Category("Paris-fork options"),DefaultValue(false)]
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



        private List<Database.RunLogDatabaseSettings> runlogDatabaseSettings;

        [Description("Run log database MySql servers to connect to, if any.")]
        public List<Database.RunLogDatabaseSettings> RunlogDatabaseSettings
        {
            get
            {
                if (runlogDatabaseSettings == null)
                    runlogDatabaseSettings = new List<Database.RunLogDatabaseSettings>();
                return runlogDatabaseSettings;
            }
            set { runlogDatabaseSettings = value; }
        }

        private List<Database.VariableDatabaseSettings> variableDatabaseSettings;

        [Description("MySQL database to get variable values from, if any variables are bound to database fields."), Category("Database Binding of Variables")]
        public List<Database.VariableDatabaseSettings> VariableDatabaseSettings
        {
            get
            {
                if (variableDatabaseSettings == null)
                    variableDatabaseSettings = new List<Database.VariableDatabaseSettings>();
                return variableDatabaseSettings;
            }
            set { variableDatabaseSettings = value; }
        }


      

        public SettingsData()
        {
            myLogicalChannelManager = new LogicalChannelManager();
            myServerManager = new ServerManager();
            cameraPCs = new List<IPAdresses>();
            versionNumberAtFirstCreation = DataStructuresVersionNumber.CurrentVersion;
            versionNumberAtLastSerialization = DataStructuresVersionNumber.CurrentVersion;
        }

        private bool alwaysUseNetworkClock;

        /// <summary>
        /// If true, Cicero will use ONLY the network clock.
        /// If false, Cicero will use the network clock only if it has received
        /// a clock datagram for that run, otherwise it will use the built in software clock.
        /// </summary>
        public bool AlwaysUseNetworkClock
        {
            get { return alwaysUseNetworkClock; }
            set { alwaysUseNetworkClock = value; }
        }

        #region Version Number Tracking

        private DataStructuresVersionNumber versionNumberAtFirstCreation;

        public DataStructuresVersionNumber VersionNumberAtFirstCreation
        {
            get { return versionNumberAtFirstCreation; }
        }


        private DataStructuresVersionNumber versionNumberAtLastSerialization;

        public DataStructuresVersionNumber VersionNumberAtLastSerialization
        {
            get { return versionNumberAtLastSerialization; }
        }

        [OnSerializing]
        private void setSerializationVersionNumber(StreamingContext sc)
        {
            this.versionNumberAtLastSerialization = DataStructuresVersionNumber.CurrentVersion;
        }

        #endregion
    }
}
