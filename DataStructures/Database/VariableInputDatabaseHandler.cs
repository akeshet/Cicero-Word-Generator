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
    //Based on the RunLogDatabaseHandler class, this class allows for variables to bound to a database field


    /// 
    /// </summary>
    public class VariableInputDatabaseHandler
    {

        public MySqlConnection conn = null;

        public VariableInputDatabaseHandler(RunLogDatabaseSettings databaseSettings)
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

        ~VariableInputDatabaseHandler()
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
        /// Read from the database
        /// </summary>
        /// <param name="runLogFileName"></param>
        /// <param name="log"></param>
        public double readFromDB()
        {
            if (conn == null)
            {
                throw new RunLogDatabaseException("Handler not connected to database");
            }

           
            //We wait in an infinite loop until the "updated" field has been set to true
            //(This will hang up the run dialog prior to buffer creation)

            MySqlCommand cmd1 = new MySqlCommand("SELECT field1_updated FROM InputValues", this.conn);
            object result1 = cmd1.ExecuteScalar();
            while(Convert.ToInt32(result1) != 1)
            {
                //This is where we wait
                cmd1 = new MySqlCommand("SELECT field1_updated FROM InputValues", this.conn);
                result1 = cmd1.ExecuteScalar();
            }
            //After the update has happened, we set the update field to false, and then grab teh variable value
            cmd1 = new MySqlCommand("UPDATE InputValues SET field1_updated = false",this.conn);
            result1 = cmd1.ExecuteScalar();
            cmd1 = new MySqlCommand("SELECT field1 FROM InputValues", this.conn);
            result1 = cmd1.ExecuteScalar();
          

            return Convert.ToDouble(result1);
        }


       

    }
}
