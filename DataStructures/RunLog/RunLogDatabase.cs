using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Windows.Forms;

namespace DataStructures
{
    public class MySQLHandler
    {

        public MySqlConnection conn;

        public MySQLHandler()
        {
            string connStr = "server=bec2.mit.edu;user=root;database=filelist;port=3306;password=password;";
            this.conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open database connection due to exception: " + ex.Message + ex.StackTrace);
            }

            

            
        }
        public void CloseConnection()
        {
            conn.Close();
        }
        public void ExecuteNonQuery(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, this.conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to execute command due to exception: " + ex.Message + ex.StackTrace);
            }
        }
        public void addRunLog(string fileName, RunLog log)
        {
            MySqlCommand cmdq = new MySqlCommand(@"SELECT path FROM filelist_runloginfo WHERE path=@path", this.conn);
            cmdq.Parameters.AddWithValue("@path", fileName);

            object result = cmdq.ExecuteScalar();

            if (result == null)
            {
                MySqlCommand cmd = new MySqlCommand(@"INSERT IGNORE INTO filelist_runloginfo 
                         (path, loc_key, time, sequencepath, listiterationnumber, liststarttime, sequenceduration, description) 
                         VALUES (@path, @loc_key, @time, @sequencepath, @listiterationnumber, @liststarttime, @sequenceduration, @description)", this.conn);

                cmd.Parameters.AddWithValue("@path", fileName);
                cmd.Parameters.AddWithValue("@loc_key", "default");
                cmd.Parameters.AddWithValue("@time", log.RunTime);
                cmd.Parameters.AddWithValue("@sequencepath", log.SequenceFileName);
                cmd.Parameters.AddWithValue("@listiterationnumber", log.RunSequence.ListIterationNumber);
                cmd.Parameters.AddWithValue("@liststarttime", log.ListStartTime);
                cmd.Parameters.AddWithValue("@sequenceduration", log.RunSequence.SequenceDuration);
                cmd.Parameters.AddWithValue("@description", log.RunSequence.SequenceDescription);

                cmd.ExecuteNonQuery();

                this.addVariables(fileName, log);
            }
        }
        public void addVariables(string fileName, RunLog log)
        {
            foreach (Variable var in log.RunSequence.Variables)
            {
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO filelist_variablevalue
                       (name, value, runlog_id) 
                       VALUES (@name, @value, @runlog)", this.conn);

                cmd.Parameters.AddWithValue("@name", var.VariableName);
                cmd.Parameters.AddWithValue("@value", var.VariableValue);
                cmd.Parameters.AddWithValue("@runlog", fileName);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
