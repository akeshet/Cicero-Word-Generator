using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    [Serializable]
    public struct DataStructuresVersionNumber
    {


        private const double FIRST_TRACKED_VERSION = 1.6;
        private const double VERSION_NUMBER_PARSE_ERROR = -1;
        private const double VERSION_NUMBER_UNASSIGNED = 0;

        private double versionNumber;

        public double VersionNumber
        {
            get { return versionNumber; }
        }


        public DataStructuresVersionNumber(double versionNumber)
        {
            this.versionNumber = versionNumber;
        }

        public override string ToString()
        {
            if (versionNumber == VERSION_NUMBER_PARSE_ERROR)
                return "Error parsing version number string!";
            else if (versionNumber > FIRST_TRACKED_VERSION)
                return versionNumber.ToString("0.00");
            else
                return "<=1.60";
        }

        public static DataStructuresVersionNumber getCurrentVersion()
        {
            string versionNumberString = DataStructures.Properties.Resources.VersionString;
            double number = 0;
            bool couldParse = Double.TryParse(versionNumberString, out number);
            if (!couldParse)
            {
                number = VERSION_NUMBER_PARSE_ERROR;
            }

            return new DataStructuresVersionNumber(number);
        }


        public static DataStructuresVersionNumber CurrentVersion
        {
            get { return getCurrentVersion(); }
        }

        public bool isValid()
        {
            if (versionNumber == VERSION_NUMBER_PARSE_ERROR)
                return false;

            return true;
        }

    }
}
