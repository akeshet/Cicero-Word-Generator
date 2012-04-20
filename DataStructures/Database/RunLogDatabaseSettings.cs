using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DataStructures.Database
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class RunLogDatabaseSettings
    {
        private string url;

        [Description("URL for MySql server.")]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }


        private string port;
        [Description("Port # MySql server (default: 3306)."), DefaultValue("3306")]
        public string Port
        {
            get { return port; }
            set { port = value; }
        }
        private string username;

        [Description("Username for MySql server.")]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }


        private string password;

        [Description("Password for MySql server.")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        private string databaseName;

        [Description("Database name for MySql server.")]
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        private bool enabled;

        [Description("Use this RunLogDatabase to store run file information after each shot?")]
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        private bool verboseErrorReporting;

        [Description("Report errors in detail if they occur?")]
        public bool VerboseErrorReporting
        {
            get { return verboseErrorReporting; }
            set { verboseErrorReporting = value; }
        }


        public string getConnectionString()
        {
            return "server=" + Url + ";user=" + username + ";database=" + databaseName + ";port=" + port + ";password=" + password + ";";
        }
    }
}
