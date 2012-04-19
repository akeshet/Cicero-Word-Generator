using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Threading;

namespace DataStructures.UtilityClasses
{
    /// <summary>
    /// Contains functions used to obtain the directory and name where files are saved.
    /// </summary>
    public class NamingFunctions
    {
        //Adds the appropriate number of zeros so that number has n0 digit
        public static string number_to_string(int number, int n0)
        {
            string res = number.ToString();
            for (int i = 0; i < n0 - number.ToString().Length; i++)
            {
                res = "0" + res;
            }
            return res;
        }

        //Obtains a list '_' separated of the bound variables and their values, reported in the name of the shot
        public static string listBoundVariables(SequenceData sequence)
        {
            string listBoundVariableValues = "";

            foreach (Variable var in sequence.Variables)
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

        //Obtains the directory in which the files will be saved
        public static string get_fileDirectory(SettingsData settings)
        {
            string fileDirectory;
            if (!settings.UseParisStyleFileTimestamps)
            {
                if (settings.SavePath.EndsWith("/") || settings.SavePath.EndsWith(@"\"))
                    fileDirectory = settings.SavePath.Remove(settings.SavePath.Length - 1);
                else if (settings.SavePath != "")
                    fileDirectory = settings.SavePath;
                else
                    fileDirectory = AppDomain.CurrentDomain.BaseDirectory;
            }
            else
            {
                DateTime today = DateTime.Today;
                string the_year = today.Year.ToString();
                CultureInfo the_current_culture = CultureInfo.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
                string the_month = String.Format("{0:MMM}", today).ToString();
                Thread.CurrentThread.CurrentCulture = the_current_culture;
                the_month = the_month.Substring(0, 1).ToUpper() + the_month.Substring(1);
                string the_day = number_to_string(today.Day, 2);

                if (settings.SavePath.EndsWith("/") || settings.SavePath.EndsWith(@"\"))
                    fileDirectory = settings.SavePath.Remove(settings.SavePath.Length - 1) + @"\" + the_year + @"\" + the_month + the_year + @"\" + the_day + the_month + the_year;
                else if (settings.SavePath != "")
                    fileDirectory = settings.SavePath + @"\" + the_year + @"\" + the_month + the_year + @"\" + the_day + the_month + the_year;
                else
                    fileDirectory = AppDomain.CurrentDomain.BaseDirectory + the_year + @"\" + the_month + the_year + @"\" + the_day + the_month + the_year;
            }
            return fileDirectory;

        }

        //Obtains the name of the shot, containing bound variables, the name and the description of the sequence
        public static string get_fileStamp(SequenceData sequence, SettingsData settings, DateTime runTime)
        {
            string fileStamp;
            if (!settings.UseParisStyleFileTimestamps)
            {
                fileStamp = "RunLog-" + CiceroUtilityFunctions.getTimeStampString(runTime);
            }
            else
            {
                fileStamp = number_to_string(runTime.Hour, 2) + number_to_string(runTime.Minute, 2) + number_to_string(runTime.Second, 2);
                if (sequence.SequenceName != "")
                    fileStamp = fileStamp + "_" + ProcessName(sequence.SequenceName,sequence);
                if (sequence.SequenceDescription != "")
                    fileStamp = fileStamp + "_" + ProcessName(sequence.SequenceDescription,sequence);
                if (listBoundVariables(sequence) != "")
                    fileStamp = fileStamp + "_" + listBoundVariables(sequence);
            }
            return fileStamp;
        }

        //Looks for variable names in the sequence name or description to add the value afterwards
        public static string ProcessName(string theName, SequenceData sequence)
        {
            string ans = theName;
            foreach (Variable var in sequence.Variables)
            {

                if (!var.ListDriven || var.PermanentVariable)
                {
                    if (theName.Contains(var.VariableName))
                        ans = ans.Replace(var.VariableName, var.VariableName + " = " + var.VariableValue.ToString());
                }
            }
            return ans;
        }
    }
}
