using System;
using System.Collections.Generic;
using System.Text;
using DataStructures;

namespace Virgil
{
    [Serializable]
    public class VirgilServerSettings : ServerSettingsInterface
    {
        private string serverName;

        public string ServerName
        {
            get
            {
                if (serverName == null)
                    serverName = "";
                return serverName;
            }
            set { serverName = value; }
        }

        private string hdf5FilePath;

        public string Hdf5FilePath
        {
            get
            {
                if (hdf5FilePath == null)
                    hdf5FilePath = "";

                return hdf5FilePath;
            }
            set { hdf5FilePath = value; }
        }
    }
}
