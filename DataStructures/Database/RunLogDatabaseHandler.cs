using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Windows.Forms;

namespace DataStructures.Database
{
    
    /// <summary>
    /// Initial author: Edward Su
    /// This class can be used to export run log information into a MySql database.
    /// 
    /// </summary>
    public class RunlogDatabaseHandler
    {

        public MySqlConnection conn = null;

        public RunlogDatabaseHandler(RunLogDatabaseSettings databaseSettings)
        {
            if (conn != null)
            {
                throw new RunLogDatabaseException("Attempted to re-open a database connection without first closing the same handle.");
            }

            string connStr = databaseSettings.getConnectionString();
            this.conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                try { conn.Close(); } catch(Exception) {};

                conn = null;
                RunLogDatabaseException newex = new RunLogDatabaseException("Unable to open database due to exception: " + ex.Message, ex);
                throw newex;
            }            
        }

        ~RunlogDatabaseHandler()
        {
            closeConnection();
        }

        /// <summary>
        /// If database connection is open, close it. Otherwise do nothing, harmlessly.
        /// </summary>
        public void closeConnection()
        {
            if (conn == null)
                return;

            conn.Close();
            conn = null;
        }


        private void ExecuteNonQuery(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, this.conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new RunLogDatabaseException("Exception when attempting to execute sql query: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Add run log to database.
        /// </summary>
        /// <param name="runLogFileName"></param>
        /// <param name="log"></param>
        public void addRunLog(string runLogFileName, RunLog log)
        {
            if (conn == null)
            {
                throw new RunLogDatabaseException("Handler not connected to database, unable to add run log.");
            }

            MySqlCommand cmdq = new MySqlCommand(@"SELECT path FROM filelist_runloginfo WHERE path=@path", this.conn);
            cmdq.Parameters.AddWithValue("@path", runLogFileName);

            object result = cmdq.ExecuteScalar();

            if (result == null)
            {
                MySqlCommand cmd = new MySqlCommand(@"INSERT IGNORE INTO filelist_runloginfo 
                         (path, loc_key, time, sequencepath, listiterationnumber, liststarttime, sequenceduration, description) 
                         VALUES (@path, @loc_key, @time, @sequencepath, @listiterationnumber, @liststarttime, @sequenceduration, @description)", this.conn);

                cmd.Parameters.AddWithValue("@path", runLogFileName);
                cmd.Parameters.AddWithValue("@loc_key", "default");
                cmd.Parameters.AddWithValue("@time", log.RunTime);
                cmd.Parameters.AddWithValue("@sequencepath", log.SequenceFileName);
                cmd.Parameters.AddWithValue("@listiterationnumber", log.RunSequence.ListIterationNumber);
                cmd.Parameters.AddWithValue("@liststarttime", log.ListStartTime);
                cmd.Parameters.AddWithValue("@sequenceduration", log.RunSequence.SequenceDuration);
                cmd.Parameters.AddWithValue("@description", log.RunSequence.SequenceDescription);

                cmd.ExecuteNonQuery();

                this.addVariables(runLogFileName, log);
            }
        }


        private void addVariables(string fileName, RunLog log)
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
