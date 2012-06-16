using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class Information
    {
        public static string VersionString
        {
            get
            {
                if (DataStructures.Properties.Resources.RevisionString != null)
                    return DataStructures.Properties.Resources.VersionString + DataStructures.Properties.Resources.RevisionString;

                else
                    return DataStructures.Properties.Resources.VersionString;
            }
        }

        public static string AuthorString
        {
            get
            {
                return DataStructures.Properties.Resources.AuthorString;
            }
        }

        public static string BuildDateString
        {
            get
            {
                return DataStructures.Properties.Resources.BuildDateString;
            }
        }

        public static string ContribString
        {
            get
            {
                return DataStructures.Properties.Resources.ContribsString;
            }
        }
    }
}
