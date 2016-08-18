using System;
using System.Collections.Generic;
using System.Text;
using DataStructures;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

namespace Zeus
{
    public class ExampleServerSettings : ServerSettingsInterface
    {
        private List<heartBeat> heartbeats;
        [Description("The names and group ID's of heartbeat fields to listen to. If more than one heartbeat with a particular group ID is alive, then Zeus will not let Cicero run."),
         Category("Memcached")]
        public List<heartBeat> Heartbeats
        {
            get{return heartbeats;}
            set { heartbeats = value; }
        }
    
        private string serverName;
             [Description("Sets the name of this Zeus server"),
        Category("General")]
        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        
        private string databaseServerIP;
        [Description("Sets the IP address of the computer running the MYSQL/MariaDB database"),
        Category("Database")]
        public string DatabaseServerIP
        {
            get { return databaseServerIP; }
            set { databaseServerIP = value; }
        }

        
        private string memcachedServerIP;
        [Description("Sets the IP address of the computer running the memcached server"),
        Category("Memcached")]
        public string MemcachedServerIP
        {
            get { return memcachedServerIP; }
            set { memcachedServerIP = value; }
        }

        
        private string username;
        [Description("Sets the username for  the MYSQL/MariaDB database"),
        Category("Database")]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        
        private string password;
        [Description("Sets the password for the MYSQL/MariaDB database"),
        Category("Database")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        
        private string dbName;
         [Description("Sets the name of the MYSQL/MariaDB database to connect to"),
        Category("Database")]
        public string DBName
        {
            get { return dbName; }
            set { dbName = value; }
        }

         private List<HardwareChannelRule> rules;
         [Description("Monitored USB peripheral hardware channels and desired states"),
          Category("USB")]
         public List<HardwareChannelRule> Rules
         {
             get { return rules; }
             set { rules = value; }
         }


         private int numberOfRelocks;
         [Description("Number of Auto-Relocks to try"),
    Category("USB")]
         public int NumberOfRelocks
         {
             get { return numberOfRelocks; }
             set { numberOfRelocks = value; }
         }


         private List<emailList> emails;
         [Description("Email/SMS addresses to notify if relocks fail"),
          Category("USB")]
         public List<emailList> Emails
         {
             get { return emails;}
             set { emails = value; }
         }
    }
}
