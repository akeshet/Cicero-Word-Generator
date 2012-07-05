using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DataStructures;



namespace DataStructures
{
    /// <summary>
    /// The HardwareChannel class encapsulates the information regarding a specific
    /// physical device.
    /// </summary>
    [Serializable, TypeConverter(typeof(ExpandableStructConverter))]
    public struct HardwareChannel
    {

        /// <summary>
        /// If true, and if this is a GPIB channel, then it means that this is actually some other channel type which is 
        /// from a Cicero UI point of masquerading as a gpib channel. For instance, an RFSG channel. 
        /// </summary>
        public bool gpibMasquerade;

        public enum GpibMasqueradeType { NONE, RFSG };
        public GpibMasqueradeType myGpibMasqueradeType;

        public static readonly HardwareChannel Unassigned = HardwareChannel.createUnassignedHC();


        private string serverName;

        [Description("The name of the server that this channel resides on."),
        Category("Global")]
        public string ServerName
        {
            get { return serverName; }
        }

        /// <summary>
        /// eg returns "dev1" when passed /dev1/chasdlkjfh/234
        /// 
        /// This method just parses out the string between the first two right slashes
        /// </summary>
        /// <param name="channelString"></param>
        /// <returns></returns>
        public static string parseDeviceNameStringFromPhysicalChannelString(string channelString)
        {
            

  
            if (channelString == null)
                return null;

            if (channelString[0]!='/') {
                throw new Exception("Channel string does not begin with \"/\"");
            }

            channelString = channelString.Substring(1);

            int slashIndex = channelString.IndexOf("/");
            channelString = channelString.Substring(0, slashIndex);

            return channelString;
        }

        /// <summary>
        /// eg returns /dev1/port0 when given /dev1/port0/line1
        /// </summary>
        /// <param name="channelString"></param>
        /// <returns></returns>
        public static string parsePortStringFromChannelString(string channelString)
        {
            if (channelString == null)
                return null;

            channelString = channelString.ToUpper();
            int lineStart = channelString.IndexOf("LINE");
            channelString = channelString.Substring(0, lineStart-1);
            return channelString;
        }

        /// <summary>
        /// eg returns 2 when given /dev1/port2/line3. Assumes port0 to port9
        /// </summary>
        /// <param name="channelString"></param>
        /// <returns></returns>
        public static int parsePortNumberFromChannelString(string channelString)
        {
            channelString = channelString.ToUpper();
            int portStart = channelString.IndexOf("PORT");
            string temp = channelString.Substring(portStart + 4, 1);
            return int.Parse(temp);
        }

        /// <summary>
        /// eg returns 3 when given /dev1/port2/line3. Assumes line0 to line9
        /// </summary>
        /// <param name="channelString"></param>
        /// <returns></returns>
        public static int parseLineNumberFromChannelString(string channelString)
        {
            channelString = channelString.ToUpper();
            int lineStart = channelString.IndexOf("LINE");
            string temp = channelString.Substring(lineStart + 4, 1);
            return int.Parse(temp);
        }

        private string deviceName;

        [Description("The name of the device that this channel resides on."),
        Category("Global")]
        public string DeviceName
        {
            get { return deviceName; }
        }

        private string channelName;

        [Description("The channel identifier for this channel."),
        Category("Global")]
        public string ChannelName
        {
            get { return channelName; }
        }


        private HardwareChannel.HardwareConstants.ChannelTypes channelType;

        [Description("The type of channel."),
        Category("Global")]
        public HardwareChannel.HardwareConstants.ChannelTypes ChannelType
        {
            get { return channelType; }
        }

        private HardwareChannel.HardwareConstants.GPIBDeviceType gpibDeviceType;

        [Description("Applies only for GPIB channels. Specifies the type of device that this gpib channel is connected to."),
        Category("GPIB")]
        public HardwareChannel.HardwareConstants.GPIBDeviceType GpibDeviceType
        {
            get { return gpibDeviceType; }
        }

        private string channelDescription;

        [Description("Description of the channel, if applicable."),
        Category("Global")]
        public string ChannelDescription
        {
            get { return channelDescription; }
        }

        private DataStructures.Gpib.Address gpibAddress;

        [Description("Applies only to GPIB devices. Specifies the gpib address of the channel."),
        Category("GPIB")]
        public DataStructures.Gpib.Address GpibAddress
        {
            get { return gpibAddress; }
        }

        // true if this hardware channel represents "unnasigned"
        public bool isUnAssigned;


        private static HardwareChannel createUnassignedHC()
        {
            HardwareChannel ans = new HardwareChannel();
            ans.isUnAssigned = true;
            return ans;
        }

        public HardwareChannel(string serverName, string deviceName, string channelName, HardwareChannel.HardwareConstants.ChannelTypes ct)
            : this(serverName, deviceName, channelName, "", ct)
        {
            
        }

        public HardwareChannel(string serverName, string deviceName, string channelName, string channelDescription, HardwareChannel.HardwareConstants.ChannelTypes ct)
        {
            gpibMasquerade = false;
            myGpibMasqueradeType = GpibMasqueradeType.NONE;
            this.serverName = serverName;
            this.deviceName = deviceName;
            this.channelName = channelName;
            this.channelDescription = channelDescription;
            this.channelType = ct;
            this.isUnAssigned = false;
            this.gpibAddress = new DataStructures.Gpib.Address();
            this.gpibDeviceType = HardwareConstants.GPIBDeviceType.Unknown;
        }

        public HardwareChannel(string serverName, string deviceName, string channelName, string channelDescription, HardwareChannel.HardwareConstants.ChannelTypes ct, DataStructures.Gpib.Address gpibAddress, HardwareChannel.HardwareConstants.GPIBDeviceType gpibDeviceType)
            : this(serverName, deviceName, channelName,channelDescription, ct)
        {
            if (this.ChannelType != HardwareConstants.ChannelTypes.gpib)
            {
                throw new Exception("Do not call gpib channel constructor for a non gpib device.");
            }
            this.gpibAddress = gpibAddress;
            this.gpibDeviceType = gpibDeviceType;
        }

 

        public override string ToString()
        {
            if (!isUnAssigned)
                return this.serverName + "/" + this.physicalChannelName();
            else return "Unassigned";
        }

        /// <summary>
        /// This string follows the national instruments string convention for channel naming.
        /// </summary>
        /// <returns></returns>
        public string physicalChannelName()
        {
            if (!isUnAssigned)
                return deviceName + "/" + channelName;
            else return "Unassigned";
        }


        public static bool operator==(HardwareChannel sonny, HardwareChannel cher) {


            if (sonny.serverName != cher.serverName)
                return false;
            if (sonny.deviceName != cher.deviceName)
                return false;
            if (sonny.ChannelName != cher.channelName)
                return false;
            if (sonny.channelType != cher.channelType)
                return false;
            if (sonny.gpibAddress.ToString() != cher.gpibAddress.ToString())
                return false;

            return true;
        }

        public static bool operator !=(HardwareChannel a, HardwareChannel b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is HardwareChannel)
                return (this == (HardwareChannel)obj);
            return false;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public int gpibBoardNumber()
        {
            if (this.ChannelType != HardwareConstants.ChannelTypes.gpib)
                throw new Exception("Cannot get the GPIB board number of a non gpib channel.");
            // remove the characters GPIB
            string channelnumber = deviceName.Remove(0, 4);
            int indexOfSlash = channelnumber.IndexOf('/');
            channelnumber = channelnumber.Remove(indexOfSlash);
            return int.Parse(channelnumber);
        }

        public int daqMxDigitalPortNumber()
        {
            // string processing. for instance, take the string port0/line1 and return the int 0. 

            assertDigital();

            // trim the "port" from the beginning of the channel name.
            string temp = channelName.Remove(0, 4);
            // now trim everything after the /
            int slashLocation = temp.IndexOf('/');
            temp = temp.Remove(slashLocation);
            return int.Parse(temp);
        }

        private void assertDigital()
        {
            if (this.ChannelType != HardwareConstants.ChannelTypes.digital)
                throw new Exception("Cannot get the daqMx digital port number of a non digital hardware channel.");
        }

        public int daqMxDigitalLineNumber()
        {
            assertDigital();

            // string processing. Take the string port0/line1 and return the int 1
            // find "line"
            int lineLocation = channelName.IndexOf("line");
            string temp = channelName.Remove(0, lineLocation + 4);
            return int.Parse(temp);
        }


        public static string digitalPhysicalChannelName(int portnum, int linenum)
        {
            return "port" + portnum + "/line" + linenum;
        }

        public static string digitalPhysicalChannelName(int portnum)
        {
            return "port" + portnum;
        }

        


        #region HardwareConstants

        public class HardwareConstants
        {
            public enum ChannelTypes { analog = 0, digital, gpib, rs232 };

            /// <summary>
            /// Enumerates the various gpib devices which the software knows how to drive. Unknown devices can only be driven
            /// by user specification of the output string.
            /// </summary>
            public enum GPIBDeviceType { Unknown, Agilent_ESG_SIG_Generator };

            /// <summary>
            /// This array of string holds the initialization commands which are to be sent at the beginning of a run
            /// to a gpib device, depending on the gpibDeviceType.
            /// When adding new gpib device types, add their initialization commands here.
            /// </summary>
            public static readonly string[] gpibInitializationCommands = new string[] 
        { "",   // for unknown devices
            ":OUTP:MOD OFF\n" +
            ":OUTP ON\n" + 
            ":DISP:ANN:AMPL:UNIT V\n"// for esg devices
        };

            public static Array allChannelTypes = Enum.GetValues(typeof(ChannelTypes));
            public static int numChannelTypes = allChannelTypes.Length;

            public const string DefaultHardwareChannelMessage = "(Unspecified)";
            public static ChannelTypes ParseChannelTypeFromString(string channelTypeString)
            {
                return (ChannelTypes)Enum.Parse(typeof(ChannelTypes), channelTypeString);
            }
        }

        #endregion



    }
}
