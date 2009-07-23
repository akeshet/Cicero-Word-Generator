using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Globalization;
using System.Threading;

namespace DataStructures
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class RunLog
    {

        private string sequenceFileName;

        public string SequenceFileName
        {
            get { return sequenceFileName; }
            set { sequenceFileName = value; }
        }

        private string settingsFileName;

        public string SettingsFileName
        {
            get { return settingsFileName; }
            set { settingsFileName = value; }
        }

        private DateTime runTime;

        private DateTime listStartTime;

        public DateTime ListStartTime
        {
            get {
            
                return listStartTime; 
            }
            set { listStartTime = value; }
        }

        public DateTime RunTime
        {
            get { return runTime; }
            set { runTime = value; }
        }
        private SequenceData runSequence;

        public SequenceData RunSequence
        {
            get { return runSequence; }
            set { runSequence = value; }
        }
        private SettingsData runSettings;

        public SettingsData RunSettings
        {
            get { return runSettings; }
            set { runSettings = value; }
        }

        private RunLogAnnotation annotation;

        public RunLogAnnotation Annotation
        {
            get
            {
                if (annotation == null)
                {
                    annotation = new RunLogAnnotation();
                }
                return annotation;
            }
            set { annotation = value; }
        }

        public override string ToString()
        {
          
            return runSequence.ToString();
        }

        public RunLog(DateTime runTime, DateTime listStartTime, SequenceData sequence, SettingsData settings, string sequenceFileName, string settingsFileName)
        {
            this.runTime = runTime;
            this.listStartTime = listStartTime;
            this.runSequence = sequence;
            this.runSettings = settings;
            this.SequenceFileName = sequenceFileName;
            this.SettingsFileName = settingsFileName;
        }

        /// <summary>
        /// Writes the runlog to a timestamped file in the RunLogs directory. Returns the filename that the log was written to. Returns null if
        /// no log was written (for instance, if one already exists with the same stamp).
        /// </summary>
        /// <returns></returns>
        public string WriteLogFile()
        {
            BinaryFormatter b = new BinaryFormatter();
            
            string fileStamp;
            string fileExt = ".clg";
            string fileDirectory;

            if (runSettings.UseMitFileStamp)
            {
                //runTime.ToString("s") returns a date time string which is formatted to make it string sortable by time
                // and suitable for file names (once the : are replaced with -)
                fileStamp = "RunLog-" + CiceroUtilityFunctions.getTimeStampString(runTime);

                if (runSettings.SavePath.EndsWith("/") || runSettings.SavePath.EndsWith(@"\"))
                    fileDirectory = runSettings.SavePath.Remove(runSettings.SavePath.Length - 1) + @"\RunLogs\\";
                else if (runSettings.SavePath != "")
                    fileDirectory = runSettings.SavePath + @"\RunLogs\";
                else
                    fileDirectory = AppDomain.CurrentDomain.BaseDirectory + "RunLogs\\";
            }
            else
            {
                fileStamp = number_to_string(runTime.Hour, 2) + number_to_string(runTime.Minute, 2) + number_to_string(runTime.Second, 2);
                if (runSequence.SequenceName != "")
                    fileStamp = fileStamp + "_" + ProcessName(runSequence.SequenceName);
                if (runSequence.SequenceDescription != "")
                    fileStamp = fileStamp + "_" + ProcessName(runSequence.SequenceDescription);
                if (listBoundVariables() != "")
                    fileStamp = fileStamp + "_" + listBoundVariables();

                DateTime today = DateTime.Today;
                string the_year = today.Year.ToString();
                CultureInfo the_current_culture = CultureInfo.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
                string the_month = String.Format("{0:MMM}", today).ToString();
                Thread.CurrentThread.CurrentCulture = the_current_culture;
                the_month = the_month.Substring(0, 1).ToUpper() + the_month.Substring(1);
                string the_day = number_to_string(today.Day, 2);

                if (runSettings.SavePath.EndsWith("/") || runSettings.SavePath.EndsWith(@"\"))
                    fileDirectory = runSettings.SavePath.Remove(runSettings.SavePath.Length - 1) + @"\" + the_year + @"\" + the_month + the_year + @"\" + the_day + the_month + the_year + @"\RunLogs\";
                else if (runSettings.SavePath != "")
                    fileDirectory = runSettings.SavePath + @"\" + the_year + @"\" + the_month + the_year + @"\" + the_day + the_month + the_year + @"\RunLogs\";
                else
                    fileDirectory = AppDomain.CurrentDomain.BaseDirectory + the_year + @"\" + the_month + the_year + @"\" + the_day + the_month + the_year;
            }

            try
            {
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }
            }
            catch { }

            string fullFileName = fileDirectory + fileStamp + fileExt;

            if (File.Exists(fullFileName))
            {
                return null;
            }

            FileStream fs = new FileStream(fullFileName, FileMode.Create);
            b.Serialize(fs, this);
            fs.Close();
            return fullFileName;
        }

        public string listBoundVariables()
        {
            string listBoundVariableValues = "";

            

            foreach (Variable var in runSequence.Variables)
            {

                if (var.ListDriven && !var.PermanentVariable)
                {
                    if (listBoundVariableValues != "")
                    {
                        listBoundVariableValues += "_";
                    }
                    listBoundVariableValues += var.VariableName + " = " + var.VariableValue.ToString();
                }
            }

            
                return listBoundVariableValues;
            
        }
        public string ProcessName(string theName)
        {
            string ans = theName;
            foreach (Variable var in runSequence.Variables)
            {

                if (!var.ListDriven || var.PermanentVariable)
                {
                    if (theName.Contains(var.VariableName))
                        ans=ans.Replace(var.VariableName, var.VariableName + " = " + var.VariableValue.ToString());
                }
            }
            return ans;
        }
        static string number_to_string(int number, int n0)
        {
            string res = number.ToString();
            for (int i = 0; i < n0 - number.ToString().Length; i++)
            {
                res = "0" + res;
            }
            return res;
        }
    }
}
