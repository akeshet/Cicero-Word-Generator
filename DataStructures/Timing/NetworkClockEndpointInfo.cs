using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DataStructures.Timing
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class NetworkClockEndpointInfo
    {
        private string hostName;

        public string HostName
        {
            get { return hostName; }
            set { hostName = value; }
        }

        public enum HostTypes { Cicero_Client, Atticus_Server };

        private HostTypes hostType;

        public HostTypes HostType
        {
            get { return hostType; }
            set { hostType = value; }
        }

        public static int getListenerPort(HostTypes hostType)
        {
            if (hostType == HostTypes.Cicero_Client)
                return 39721;
            if (hostType == HostTypes.Atticus_Server)
                return 39722;

            throw new Exception("Unknown host type.");
        }

        public int getPort()
        {
            return getListenerPort(HostType);
        }
    }
}
