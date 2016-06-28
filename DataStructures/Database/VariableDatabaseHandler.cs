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
    public class VariableDatabaseHandler
    {

        public MySqlConnection conn = null;

        public VariableDatabaseHandler(VariableDatabaseSettings databaseSettings)
        {
            if (conn != null)
            {
                throw new VariableDatabaseException("Attempted to re-open a database connection without first closing the same handle.");
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
                VariableDatabaseException newex = new VariableDatabaseException("Unable to open database due to exception: " + ex.Message, ex);
                throw newex;
            }            
        }

        ~VariableDatabaseHandler()
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
                throw new VariableDatabaseException("Exception when attempting to execute sql query: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Read from the database
        /// </summary>
        /// <param name="runLogFileName"></param>
        /// <param name="log"></param>
        public double readFromDB(int fieldIndex)
        {

            string updatedColumnName = "field" + fieldIndex + "_updated";
            string columnName = "field" + fieldIndex;

            if (conn == null)
            {
                throw new VariableDatabaseException("Handler not connected to database");
            }

            
            //We set the update field to false, and then grab the variable value
            MySqlCommand cmd1 = new MySqlCommand("UPDATE InputValues SET " + updatedColumnName + " = false", this.conn);
            object result1 = cmd1.ExecuteScalar();
            cmd1 = new MySqlCommand("SELECT "+columnName+" FROM InputValues", this.conn);
            result1 = cmd1.ExecuteScalar();
          

            return Convert.ToDouble(result1);
        }


       

    }
}
