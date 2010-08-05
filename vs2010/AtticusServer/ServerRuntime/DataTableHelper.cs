using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using AtticusServer;
using DataStructures;


public class DataTableHelper
{

    ///
    ///Produces a CVS file with the data from analog input channels
    ///
    public static void ProduceCSV(DataTable dt, System.IO.TextWriter httpStream, bool WriteHeader)
    {
        if (WriteHeader)
        {
            string[] arr = new String[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                arr[i] = dt.Columns[i].ColumnName;
                arr[i] = GetWriteableValue(arr[i]);
            }

            httpStream.WriteLine(string.Join(",", arr));
        }

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            string[] dataArr = new String[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                object o = dt.Rows[j][i];
                dataArr[i] = GetWriteableValue(o);
            }
            httpStream.WriteLine(string.Join(",", dataArr));
        }
    }

    #region CSV Producer
    public static void ProduceCSV(DataTable dt, System.IO.StreamWriter file, bool WriteHeader, double rate_ana_in)
    {


        if (WriteHeader)
        {
            string[] arr = new String[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                arr[i] = dt.Columns[i].ColumnName;
                arr[i] = GetWriteableValue(arr[i]);
            }

            file.WriteLine("t," + string.Join(",", arr));
        }

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            string[] dataArr = new String[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                object o = dt.Rows[j][i];
                dataArr[i] = GetWriteableValue(o);
            }
            file.WriteLine(Convert.ToString(j * rate_ana_in) + "," + string.Join(",", dataArr));
        }
    }

    public static void ProduceCSV(DataTable dt, System.IO.StreamWriter file, bool WriteHeader)
    {


        if (WriteHeader)
        {
            string[] arr = new String[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                arr[i] = dt.Columns[i].ColumnName;
                arr[i] = GetWriteableValue(arr[i]);
            }

            file.WriteLine(string.Join(",", arr));
        }

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            string[] dataArr = new String[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                object o = dt.Rows[j][i];
                dataArr[i] = GetWriteableValue(o);
            }
            file.WriteLine(string.Join(",", dataArr));
        }
    }

    public static string GetWriteableValue(object o)
    {
        #region passage en systeme US
        System.Globalization.CultureInfo oldCI;
        oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
        System.Threading.Thread.CurrentThread.CurrentCulture =
            new System.Globalization.CultureInfo("en-US");
        #endregion

        if (o == null || o == Convert.DBNull)
            return "";
        else if (o.ToString().IndexOf(",") == -1)
            return o.ToString();
        else
            return "\"" + o.ToString() + "\"";
        // Retour au systeme normal
        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
    }
    #endregion

    //Writes analog input data in a properly formatted DataTable. Data is time indexed.
    public static DataTable dataToDataTable(double[,] sourceArray,DataTable dataTable,int frequency)
    {
        int channelCount = sourceArray.GetLength(0);
        int dataCount;

        dataCount = dataTable.Rows.Count;


        for (int currentDataIndex = 0; currentDataIndex < dataCount; currentDataIndex++)
        {
            for (int currentChannelIndex = 1; currentChannelIndex < channelCount + 1; currentChannelIndex++)
                dataTable.Rows[currentDataIndex][currentChannelIndex] = sourceArray.GetValue(currentChannelIndex - 1, (int)((Double.Parse((dataTable.Rows[currentDataIndex][0]).ToString())) * frequency));
        }
        return dataTable;

    }

    //Creates a DataTable with the proper number of rows and columns, and fills in the "time" column taking into account the analogInLogTimes defined in Atticus 
    public static DataTable InitializeDataTable(DataTable dataTable, SequenceData sequence,ServerSettings settings,List<string> analog_in_names)
    {
        int numOfChannels = analog_in_names.Count;
        dataTable.Rows.Clear();
        dataTable.Columns.Clear();
        DataColumn[] dataColumn = new DataColumn[numOfChannels + 1];

        dataColumn[0] = new DataColumn();
        dataColumn[0].DataType = typeof(double);
        dataColumn[0].ColumnName = "t";

        for (int currentChannelIndex = 1; currentChannelIndex < numOfChannels + 1; currentChannelIndex++)
        {
            dataColumn[currentChannelIndex] = new DataColumn();
            dataColumn[currentChannelIndex].DataType = typeof(double);
            dataColumn[currentChannelIndex].ColumnName = analog_in_names[currentChannelIndex - 1];
        }

        dataTable.Columns.AddRange(dataColumn);

        List<double> theTimes = new List<double>();
        int samplesFreq = settings.AIFrequency;
        foreach (AnalogInLogTime theLogTime in settings.AILogTimes)
        {
            double timestep = Math.Round(sequence.timeAtTimestep(theLogTime.TimeStep - 1), 3, MidpointRounding.AwayFromZero);
            double timebefore = Math.Max(timestep - ((double)theLogTime.TimeBefore) / 1000, 0);
            double timeafter = Math.Min(timestep + ((double)theLogTime.TimeAfter) / 1000, Math.Round(sequence.SequenceDuration, 3, MidpointRounding.AwayFromZero));
            for (int k = (int)(timebefore * samplesFreq); k < (int)(timeafter * samplesFreq + 1); k++)
            {
                theTimes.Add((double)k / (double)samplesFreq);
            }
        }
        theTimes = DedupCollection(theTimes);
        theTimes.Sort();

        int numOfRows = theTimes.Count;
        for (int currentDataIndex = 0; currentDataIndex < numOfRows; currentDataIndex++)
        {
            object[] rowArr = new object[numOfChannels + 1];
            dataTable.Rows.Add(rowArr);
            dataTable.Rows[currentDataIndex][0] = theTimes[currentDataIndex];
        }
        return dataTable;
    }

    //Removes duplicates in a double List
    public static List<double> DedupCollection(List<double> input)
    {
        List<double> passedValues = new List<double>();

        //relatively simple dupe check alg used as example
        foreach (double item in input)
        {
            if (passedValues.Contains(item))
                continue;
            else
            {
                passedValues.Add(item);
            }
        }
        return passedValues;
    }
}
