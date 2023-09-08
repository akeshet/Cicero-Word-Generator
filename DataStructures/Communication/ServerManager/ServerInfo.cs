using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataStructures
{
    [TypeConverter(typeof(ExpandableObjectConverter)), Serializable]
    public class ServerInfo
    {
        private bool serverEnabled;

        [Description("Determines whether or not this server is enabled.")]
        public bool ServerEnabled
        {
            get { return serverEnabled; }
            set { serverEnabled = value; }
        }


        private bool connectOnStartup;

        [Description("Determines whether or not to connect to this server on startup.")]
        public bool ConnectOnStartup
        {
            get { return connectOnStartup; }
            set { connectOnStartup = value; }
        }
        private string serverAddress;

        [Description("IP address of the server. If the server is running on the same machine as the client, set this to localhost .")]
        public string ServerAddress
        {
            get { return serverAddress; }
            set { serverAddress = value; }
        }

        private int serverPort;

        [Description("Port number of the server.")]
        public int ServerPort
        {
            get { return serverPort; }
            set { serverPort = value; }
        }

        private string serverName;

        [Description("Name of the server. This is determined by conencting to the server.")]
        public string ServerName
        {
            get { return serverName; }
        }

        public void setServerName(string serverName)
        {
            this.serverName = serverName;
        }

        public override string ToString()
        {
            if (serverName != "")
                return serverName;
            else
                return serverAddress;
        }

        public ServerInfo()
        {
            connectOnStartup = false;
            ServerAddress = "localhost";
            serverName = "";
        }
    }
}
