using System;
using System.Data;
using System.IO;

public class DataTableHelper
{

    ///
    /// Can stream DataTable to Browser, directly, you need to set
    ///
    /// Response.Clear();
    /// Response.Buffer= true;
    /// Response.ContentType = "application/vnd.ms-excel";
    /// Response.AddHeader("Content-Disposition", "inline;filename=Clientes.xls"); Response.Charset = "";
    /// this.EnableViewState = false
    /// // ACTUAL CODE
    /// ProduceCSV(dt, Response.Output, true);
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

}