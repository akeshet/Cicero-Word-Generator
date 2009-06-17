using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    /// <summary>
    /// Contains general-purposes processing functions that are generally useful.
    /// </summary>
    public class CiceroUtilityFunctions
    {
        /// <summary>
        /// Returns a time stamp string in a suitable format for use in a file name,
        /// and which will sort correctly by name.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string getTimeStampString(DateTime time)
        {
            string temp = time.ToString("s");
            temp = temp.Replace(":", "-");
            return temp;
        }
    }
}
