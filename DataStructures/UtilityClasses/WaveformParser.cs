using System;
using System.IO;
using System.Text.RegularExpressions;

namespace DataStructures.UtilityClasses
{

    /// <summary>
    /// Class for parsing text files
    /// REO 10/2008
    /// </summary>
    public class WaveformParser
    {
        TextReader rdr;
        static Regex separator;

        //optional method to apply to each line before we apply regex
        public delegate string processStringDelegate(string line);

        processStringDelegate processString;

        public WaveformParser(TextReader rdr, String regexString)
            : this(rdr, regexString, delegate(string s) {return s;})
        {
        }

        public WaveformParser(TextReader rdr, String regexString, processStringDelegate processString)
        {
            this.rdr = rdr;
            separator = new Regex(regexString);
            this.processString = processString;
        }
        
        public string[] ReadStrings()
        {
            string line = rdr.ReadLine();
            if (line == null)
                return null;
            return separator.Split(processString(line));
        }

        public double[] ReadFloats()
        {
            string[] stringfields = ReadStrings();
            if (stringfields == null)
                return null;
            return Array.ConvertAll(stringfields, new Converter<string, double>(double.Parse));
        }
    }
}
