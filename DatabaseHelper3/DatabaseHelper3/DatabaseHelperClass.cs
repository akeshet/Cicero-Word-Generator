using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text.RegularExpressions;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;



namespace DatabaseHelper
{
    public class DatabaseHelper
    {
        private MySqlConnection conn = null; //The connection
        
        private MemcachedClientConfiguration mcc = new MemcachedClientConfiguration();
        private MemcachedClient client;
           
        public DatabaseHelper(string memcachedServerIP, string MYSQLserverIP,string username, string password, string databaseName) //Constructor - establishes the connection
        {
            try
             {
                mcc.AddServer(memcachedServerIP + ":11211");
                mcc.SocketPool.ReceiveTimeout = new TimeSpan(0, 0, 10);
                mcc.SocketPool.ConnectionTimeout = new TimeSpan(0, 0, 10);
                mcc.SocketPool.DeadTimeout = new TimeSpan(0, 0, 20);
                client = new MemcachedClient(mcc);
            }
            catch
            {

            }

            try
            {

                string myConnectionString = "server=" + MYSQLserverIP + ";uid=" + username + ";" + "pwd=" + password + ";database=" + databaseName + ";";
                this.conn = new MySqlConnection(myConnectionString);
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    try { conn.Close(); }
                    catch (Exception) { };
                    conn = null;
                }
            }
            catch
            {
            }

        }

        ~DatabaseHelper() //Destructor
        {
            closeConnection();
        }

        public void closeConnection() //Function for closing the DB connection
        {
            try { conn.Close(); }
            catch (Exception) { };
            conn = null;
           
        }
//#region Testing functions
//        public void modifyTablesAndWriteToDB(List<testVariable> incomingVariables)
//        {

//            MySqlCommand cmd1 = new MySqlCommand("SELECT column_name from information_schema.columns where table_name = 'ciceroOut'", this.conn); //get list of columns
//            MySqlDataReader reader = cmd1.ExecuteReader(); //Reading the columns
//            List<string> columnNames = new List<string>(); //List of the column names

//            //First we see if we have to add any columns
            
//            while (reader.Read())
//            {
//                    columnNames.Add(reader[0].ToString()); //Add each column name
//            }

//            columnNames.RemoveAt(0); //We don't want to include the primary key here
//            reader.Close();

//            foreach (testVariable var in incomingVariables)
//            {
//                string name = var.Name;
//                int flag = 0;
//                foreach (string colName in columnNames)
//                {
//                    if (name == colName) flag++;
//                }
//                if (flag == 0) //need to add a column
//                {
//                    cmd1 = new MySqlCommand("ALTER TABLE ciceroOut ADD "+name +" dec(20,10)", this.conn);
//                    object result1 = cmd1.ExecuteScalar();
//                }
//            }

//            //Second we add the variables into the database

//            string commandString = "INSERT INTO ciceroOut (";
//            foreach (testVariable var in incomingVariables)
//            {
//                string name = var.Name;
//                commandString += (name + ",");
//            }
//            commandString = commandString.TrimEnd(',');
//            commandString += ") VALUES (";
//            foreach (testVariable var in incomingVariables)
//            {
//                double val = var.VarValue;
//                commandString += (val.ToString() + ",");
//            }
//            commandString = commandString.TrimEnd(',');
//            commandString += ")";

//            cmd1 = new MySqlCommand(commandString, this.conn);
//            object result2 = cmd1.ExecuteScalar();
//        }
//        public void writeCachedImageData(Int16[,] atoms,Int16[,] noAtoms, Int16[,] dark, int cameraID, int runID, int seqID)
//        {


//          int height = atoms.GetLength(0);
//          int width = atoms.GetLength(1);

//            byte[] bytesAtoms = new byte[2 * width * height];
//            byte[] bytesNoAtoms = new byte[2 * width * height];
//            byte[] bytesDark = new byte[2 * width * height];

           
//            Buffer.BlockCopy(atoms, 0, bytesAtoms, 0, bytesAtoms.Length);
//            Buffer.BlockCopy(noAtoms, 0, bytesNoAtoms, 0, bytesNoAtoms.Length);
//            Buffer.BlockCopy(dark, 0, bytesDark, 0, bytesDark.Length);

//         client.Store(Enyim.Caching.Memcached.StoreMode.Set, "atoms", bytesAtoms);
//         client.Store(Enyim.Caching.Memcached.StoreMode.Set, "noAtoms", bytesAtoms);
//         client.Store(Enyim.Caching.Memcached.StoreMode.Set, "dark", bytesAtoms);
//         client.Store(Enyim.Caching.Memcached.StoreMode.Set, "camID", cameraID);
//         client.Store(Enyim.Caching.Memcached.StoreMode.Set, "runID", runID);
//         client.Store(Enyim.Caching.Memcached.StoreMode.Set, "seqID", seqID);
         

//        }
//        public byte[] tryReadingFromImagesTable()
//        {
           

       
          
//                byte[] atoms = (byte[]) client.Get("cs");
//                byte[] atoms2 = (byte[])client.Get("cs2");
//                byte[] atoms3 = (byte[])client.Get("cs3");
//                return atoms;

         
                        
                
//        }
//       /* public void modifyAndRebuildTheMemoryTable(int[,] atoms, int[,] noAtoms, int[,] dark, int cameraID, int runID, int seqID)
//        {
//            //The memory table has 2 million columns and three rows. Holy shit. But this is actually going to be ok
//            {

//            }
//        }
//        */
//        public void buildMemoryTable(int[,] image)
//        {
            
//            MySqlCommand cmd = new MySqlCommand("", this.conn);
//            cmd.CommandText = "INSERT INTO temp (tempcol) VALUES (@image)";
//            BinaryFormatter bf = new BinaryFormatter();
//            MemoryStream stream = new MemoryStream();
//            bf.Serialize(stream,image);
//            cmd.Prepare();

    
//            int IMAX = 4;
         
            

//            cmd.Parameters.AddWithValue("@image", (byte[]) stream.ToArray() );
//            for (int k = 0; k < 10; k++)
//            {
              

//                 cmd.ExecuteNonQuery();
//            }
           
            
//            /*
//            MySqlCommand cmd = new MySqlCommand("", this.conn);
//            cmd.CommandText = "ALTER TABLE temp ADD (";
//            int IMAX = 10000;
//            for (int i = 973; i < IMAX; i++)
//            {

//                cmd.CommandText += "pixel" + i + " INTEGER";
//                if (i < (IMAX - 1)) cmd.CommandText += ", ";
//                else cmd.CommandText += ")";

//            }
//             * */
            
           
//        }
//        public imageStruct oldReadImage (int imageID) //(OVERLOADED) Returns the three arrays, camera info, and all cicero variables from the specified run.
//        {
//            MySqlCommand cmd1 = new MySqlCommand("SELECT images.atoms,images.noAtoms,images.dark,cameras.cameraWidth,cameras.cameraHeight,cameras.cameraPixelSize FROM images INNER JOIN cameras ON images.cameraID_fk=cameras.cameraID WHERE images.imageID = @imageID", this.conn);
//            cmd1.Prepare();
//            imageStruct outputStruct = new imageStruct();
//            cmd1.Parameters.AddWithValue("@imageID", imageID);
//            MySqlDataReader reader = cmd1.ExecuteReader(); //Reading the columns

//            while (reader.Read())
//            {
//                int width = (int)reader[3];
//                int height = (int)reader[4];
//                double pixelSize = Convert.ToDouble(reader[5]);
//                byte[] atomBytes = (byte[])reader[0];
//                byte[] noAtomBytes = (byte[])reader[1];
//                byte[] darkBytes = (byte[])reader[2];
//                int[] atomValues = new int[width * height];
//                int[] noAtomValues = new int[width * height];
//                int[] darkValues = new int[width * height];

//                for (int i = 0; i < atomBytes.Count() / 4; i++) //get the integers
//                {
//                    atomValues[i] = BitConverter.ToInt32(atomBytes, 4 * i);
//                }
//                for (int i = 0; i < noAtomBytes.Count() / 4; i++) //get the integers
//                {
//                    noAtomValues[i] = BitConverter.ToInt32(noAtomBytes, 4 * i);
//                }
//                for (int i = 0; i < darkBytes.Count() / 4; i++) //get the integers
//                {
//                    darkValues[i] = BitConverter.ToInt32(darkBytes, 4 * i);
//                }

//                int[,] atomImage = new int[height, width];
//                int[,] noAtomImage = new int[height, width];
//                int[,] darkImage = new int[height, width];

//                for (int i = 0; i < height; i++)
//                {
//                    for (int j = 0; j < width; j++)
//                    {
//                        atomImage[i, j] = atomValues[width * i + j];
//                    }
//                }
//                for (int i = 0; i < height; i++)
//                {
//                    for (int j = 0; j < width; j++)
//                    {
//                        noAtomImage[i, j] = noAtomValues[width * i + j];
//                    }
//                }
//                for (int i = 0; i < height; i++)
//                {
//                    for (int j = 0; j < width; j++)
//                    {
//                        darkImage[i, j] = darkValues[width * i + j];
//                    }
//                }


//                outputStruct.atoms = atomImage;
//                outputStruct.noAtoms = noAtomImage;
//                outputStruct.dark = darkImage;
//                outputStruct.pixelSize = pixelSize;
//            }
//            reader.Close();
//            return outputStruct;


//        }

//        public void testWriteImageData(int[,] atoms, int[,] noAtoms, int[,] dark, int cameraID, int runID, int seqID)
//        {
//            MySqlCommand cmd1 = new MySqlCommand("INSERT INTO images (runID_fk,sequenceID_fk,cameraID_fk,atoms,noAtoms,dark) VALUES(@runID,@sequenceID,@cameraID,@atoms, @noAtoms, @dark)", this.conn);
//            cmd1.Prepare();
           

//            byte[] bytesAtoms = new byte[ 1000 * 4000];
//            byte[] bytesNoAtoms = new byte[1000 * 4000];
//            byte[] bytesDark = new byte[1000 * 4000];
        
           
//            for (int i = 0; i < 1000; i++)
//            {
//                for (int j = 0; j < 1000; j++)
//                {
//                    Array.Copy(BitConverter.GetBytes(atoms[i, j]), 0, bytesAtoms, 4*(j + 1000 * i), 4);
//                    Array.Copy(BitConverter.GetBytes(noAtoms[i, j]), 0, bytesNoAtoms, 4*(j + 1000 * i), 4);
//                    Array.Copy(BitConverter.GetBytes(dark[i, j]), 0, bytesDark, 4*(j + 1000 * i), 4);
             
//                }
//            }
           

//            cmd1.Parameters.AddWithValue("@atoms", bytesAtoms);
//            cmd1.Parameters.AddWithValue("@noAtoms", bytesNoAtoms);
//            cmd1.Parameters.AddWithValue("@dark", bytesDark);
//            cmd1.Parameters.AddWithValue("@runID", runID);
//            cmd1.Parameters.AddWithValue("@sequenceID", seqID);
//            cmd1.Parameters.AddWithValue("@cameraID", cameraID);
            
//            cmd1.ExecuteNonQuery();
            


//        }
//#endregion

#region Writing functions used by cicero
        public void writeVariableValues(variableStruct incomingVariableStruct)
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT column_name from information_schema.columns where table_name = 'ciceroOut'", this.conn);
     
                    //get list of columns
                    MySqlDataReader reader = cmd1.ExecuteReader(); //Reading the columns
                    List<string> columnNames = new List<string>(); //List of the column names

                    //First we see if we have to add any columns

                    while (reader.Read())
                    {
                        columnNames.Add(reader[0].ToString()); //Add each column name
                    }

                    columnNames.RemoveAt(0); //We don't want to include the primary key here
                    reader.Close();
         

            foreach (variable var in incomingVariableStruct.variableList)
            {
                string name = var.Name;
                int flag = 0;
                foreach (string colName in columnNames)
                {
                    if (name == colName) flag++;
                }
                if (flag == 0) //need to add a column, so we see if there is an unused column to rename
                {
                    bool matchFound = false;
                    string key = "";
                    foreach (string columnName in columnNames)
                    {
                        Match match = Regex.Match(columnName, @"^(_unusedcolumn_[0-9]*)$", RegexOptions.IgnoreCase);

                        // Here we check the Match instance.
                        if (match.Success && matchFound == false)
                        {
                            // Finally, we get the Group value and display it.
                            key = match.Groups[1].Value;
                            matchFound = true;
                        }
                    }

                    if (matchFound)
                    {
                        cmd1 = new MySqlCommand("ALTER TABLE ciceroOut CHANGE " + key + " " + name + " decimal(20,10)", conn);
                        object result1 = cmd1.ExecuteScalar();
                        columnNames.Remove(key);
                        //cmd1 = new MySqlCommand("ALTER TABLE ciceroOut ADD " + name + " dec(20,10)", this.conn);
                        //object result1 = cmd1.ExecuteScalar();
                    }

                    else
                    {
                        MessageBox.Show("Database Error! There are no more available columns for your newly named variables. Database table maintenance is required - see documentation or email LundenWill@gmail.com for help.");
                    }


                    
                }
            }

            //Second we add the variables into the database

            string commandString = "INSERT INTO ciceroOut (timestamp,";
            foreach (variable var in incomingVariableStruct.variableList)
            {
                string name = var.Name;
                commandString += (name + ",");
            }
            commandString = commandString.TrimEnd(',');
            commandString += ") VALUES ('" + DateTime.Now.ToString() +"',";
            foreach (variable var in incomingVariableStruct.variableList)
            {
                double val = var.VarValue;
                commandString += (val.ToString() + ",");
            }
            commandString = commandString.TrimEnd(',');
            commandString += ")";

            cmd1 = new MySqlCommand(commandString, this.conn);
            object result2 = cmd1.ExecuteScalar();
        } //Write the variable values each time the sequence begins
        public void createNewSequence(string name, string description)
        {
            MySqlCommand cmd1 = new MySqlCommand("INSERT INTO sequence (sequenceName,sequenceDescription) VALUES (@name,@description)", this.conn);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@name", name);
            cmd1.Parameters.AddWithValue("@description", description);

            cmd1.ExecuteNonQuery();

        } //create a new sequence each time the running index is set to 1
        public void updateNewRun() //Now obsolete
        {
            MySqlCommand cmd1 = new MySqlCommand("UPDATE updates SET newRun = 1 WHERE idupdates = 0",this.conn);
            cmd1.ExecuteNonQuery();
        }
        public void resetVariableUpdateColumns(List<int> fieldsToReset) //Reset the 'updated' fields in the InputValues table
        {
            string query = "UPDATE InputValues SET ";
            foreach (int field in fieldsToReset)
            {
                query += "field" + field + "_updated = 0, ";
            }
            string trimmedQuery = query.Remove(query.Length - 2);
            MySqlCommand cmd1 = new MySqlCommand(trimmedQuery, this.conn); 
            cmd1.ExecuteScalar(); 
        }
#endregion

#region Reading functions used by cicero
        public bool checkIfVariablesHaveBeenUpdated(List<int> fieldsToCheck) //See if the db-bound variables have been updated
        {
            bool flag = true;
            try
            {
                MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM InputValues ", this.conn); //get list of columns
                MySqlDataReader reader = cmd1.ExecuteReader(); //Reading the columns
                while (reader.Read())
                {
                    foreach (int field in fieldsToCheck)
                    {

                        if (Convert.ToBoolean(reader[2 * (field - 1) + 1]) == false)
                        {
                            flag = false;
                        }
                    }
                }

                reader.Close();
            }
            catch
            {
                 flag = false;
            }
            return flag;
        }
#endregion

#region Writing functions used by slaves
        public void writeImageDataToDB(Int16[] atoms, Int16[] noAtoms, Int16[] dark, int width, int height, int cameraID, int runID, int seqID)
        {
          

            MySqlCommand cmd1 = new MySqlCommand("INSERT INTO images (timestamp,runID_fk,sequenceID_fk,cameraID_fk,atoms,noAtoms,dark) VALUES(@timestamp,@runID,@sequenceID,@cameraID,@atoms, @noAtoms, @dark)", this.conn);
            cmd1.Prepare();


            byte[] bytesAtoms = new byte[2 * height* width];
            byte[] bytesNoAtoms = new byte[2 * height * width];
            byte[] bytesDark = new byte[2 * height * width];

        //The conversion for loop below is actually slower. Don't forget this.
          /*  for (int i = 0; i < atoms.GetLength(0); i++)
            {
                for (int j = 0; j < atoms.GetLength(1); j++)
                {
                    Array.Copy(BitConverter.GetBytes(atoms[i, j]), 0, bytesAtoms, 2 * (j + width * i),2);
                    Array.Copy(BitConverter.GetBytes(noAtoms[i, j]), 0, bytesNoAtoms, 2 * (j + width * i), 2);
                    Array.Copy(BitConverter.GetBytes(dark[i, j]), 0, bytesDark,2 * (j + width * i), 2);

                }
            }*/


            Buffer.BlockCopy(atoms, 0, bytesAtoms, 0, bytesAtoms.Length);
            Buffer.BlockCopy(noAtoms, 0, bytesNoAtoms, 0, bytesNoAtoms.Length );
            Buffer.BlockCopy(dark, 0, bytesDark, 0, bytesDark.Length);


            cmd1.Parameters.AddWithValue("@atoms", bytesAtoms);
            cmd1.Parameters.AddWithValue("@noAtoms", bytesNoAtoms);
            cmd1.Parameters.AddWithValue("@dark", bytesDark);
            cmd1.Parameters.AddWithValue("@runID", runID);
            cmd1.Parameters.AddWithValue("@sequenceID", seqID);
            cmd1.Parameters.AddWithValue("@cameraID", cameraID);
            cmd1.Parameters.AddWithValue("@timestamp", DateTime.Now.ToString());

            cmd1.ExecuteNonQuery();
           

        }
        public void writeImageDataToCache(Int16[] atoms, Int16[] noAtoms, Int16[] dark, int width, int height, int cameraID, int runID, int seqID)
        {


            byte[] bytesAtoms = new byte[2 * width * height];
            byte[] bytesNoAtoms = new byte[2 * width * height];
            byte[] bytesDark = new byte[2 * width * height];


            Buffer.BlockCopy(atoms, 0, bytesAtoms, 0, bytesAtoms.Length);
            Buffer.BlockCopy(noAtoms, 0, bytesNoAtoms, 0, bytesNoAtoms.Length);
            Buffer.BlockCopy(dark, 0, bytesDark, 0, bytesDark.Length);

            client.Store(Enyim.Caching.Memcached.StoreMode.Set, "atoms", bytesAtoms);
            client.Store(Enyim.Caching.Memcached.StoreMode.Set, "noAtoms", bytesNoAtoms);
            client.Store(Enyim.Caching.Memcached.StoreMode.Set, "dark", bytesDark);
            client.Store(Enyim.Caching.Memcached.StoreMode.Set, "camID", cameraID);
            client.Store(Enyim.Caching.Memcached.StoreMode.Set, "runID", runID);
            client.Store(Enyim.Caching.Memcached.StoreMode.Set, "seqID", seqID);
         

        }
        public void writeFreeRunImageDataToCache(Int16[] shot, int width, int height, int cameraID)
        {


            byte[] bytes = new byte[2 * width * height];
        


            Buffer.BlockCopy(shot, 0, bytes, 0, bytes.Length);
          

            client.Store(Enyim.Caching.Memcached.StoreMode.Set, "free", bytes);
            client.Store(Enyim.Caching.Memcached.StoreMode.Set, "camID", cameraID);
            client.Store(Enyim.Caching.Memcached.StoreMode.Set, "height", height);
            client.Store(Enyim.Caching.Memcached.StoreMode.Set, "width", width);


        }
        public void updateNewImage()
        {
            MySqlCommand cmd1 = new MySqlCommand("UPDATE updates SET newImage = 1 WHERE idupdates = 0",this.conn);
            cmd1.ExecuteNonQuery();
        }
        public void undoUpdateNewRun()
        {
            MySqlCommand cmd1 = new MySqlCommand("UPDATE updates SET newRun = 0 WHERE idupdates = 0",this.conn);
            cmd1.ExecuteNonQuery();
        }
#endregion
    
#region Reading functions used by slaves
        public int getLastRunID() //This should run when the table is, and then be kept in memory until an image arrives OR a new update occurs 
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT runID from ciceroOut ORDER BY runID DESC LIMIT 1", this.conn);
            return (int)cmd1.ExecuteScalar();
        }
        public int getSequenceID()//Likewise for this guy
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT sequenceID from sequence ORDER BY sequenceID DESC LIMIT 1", this.conn);
            return (int)cmd1.ExecuteScalar();
        }
        public int checkForRunUpdate()
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT newRun FROM updates",this.conn);
           return (int) cmd1.ExecuteScalar();
        }
#endregion

#region Heartbeat Functions

        public void setHeartbeat(string cameraName)
        {
            Random rnd = new Random();
            Byte[] bytes = new Byte[8];
            rnd.NextBytes(bytes); 
            client.Store(Enyim.Caching.Memcached.StoreMode.Set, cameraName, bytes);
        }

        public Int64 getHeartbeat(string cameraName)
        {
             Int64 heartbeatValue = 0;
            try
            {
               heartbeatValue = BitConverter.ToInt64((byte[])client.Get(cameraName), 0);
            }
            catch
            {
                client.Store(Enyim.Caching.Memcached.StoreMode.Set, cameraName, 1); //After a long period of not using a camera, we may need to remake the cache entry.

            }
            return heartbeatValue;
        }
#endregion

#region Hub-Related Functions

        public void setHubHandshakeValue(bool hsValue) //Used by Hub to set all-clear to true or false
        {
            client.Store(Enyim.Caching.Memcached.StoreMode.Set, "hubHandshake", hsValue);
        }

        public bool checkHubHandshakeValue() //Used by Cicero to check if the Hub gives the all-clear
        {
            return (bool)client.Get("hubHandshake");
        }

        public double[] getVariableValuesFromInputValuesTable()
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM InputValues" , this.conn); //get list of columns
            MySqlDataReader reader = cmd1.ExecuteReader(); //Reading the columns
            double[] output = new double[20];
            while (reader.Read())
            {
                for (int h = 0; h < 19; h++) //Get only the values
                {
                    output[h] = (double)reader[2 * h];
                }
            }
            reader.Close();
            return output;
        }

        public bool[] getEnableStatesFromInputValuesTable()
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM InputValues", this.conn); //get list of columns
            MySqlDataReader reader = cmd1.ExecuteReader(); //Reading the columns
            bool[] output = new bool[20];
            while (reader.Read())
            {
                for (int h = 0; h < 19; h++) //Get only the enable states
                {
                    output[h] = Convert.ToBoolean(reader[2 * h + 1]);
                }
            }
            reader.Close();
            return output;
        }

#endregion

#region Writing functions used by analyzer
        public void undoUpdateNewImage()
        {
            MySqlCommand cmd1 = new MySqlCommand("UPDATE updates SET newImage = 0 WHERE idupdates = 0",this.conn);
            cmd1.ExecuteNonQuery();
        }
        public void writeToAnalysisResultsTable(double nC, double nBEC)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE analysisResults set nBEC=" + Convert.ToString(nBEC), conn);
            cmd.ExecuteNonQuery();
            cmd = new MySqlCommand("UPDATE analysisResults set updated = 1", conn);
            cmd.ExecuteNonQuery();
        }
        //Functions for creating and modifying series
        public void createSeries(int[] analysisIDs, string name, string description) //create a new series
        {
            MySqlCommand cmd1 = new MySqlCommand("INSERT INTO series (name,description) VALUES (@name,@description)", this.conn);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@name", name);
            cmd1.Parameters.AddWithValue("@description", description);
            cmd1.ExecuteNonQuery();

            cmd1 = new MySqlCommand("SELECT seriesID FROM series ORDER BY seriesID DESC LIMIT 1", this.conn);
            int createdSeriesID = (int)cmd1.ExecuteScalar();

            foreach (int analysisID in analysisIDs)
            {
                cmd1 = new MySqlCommand("INSERT INTO seriesToAnalysis (seriesID_fk,analysisID_fk) VALUES (@series,@analysis)", this.conn);
                cmd1.Prepare();
                cmd1.Parameters.AddWithValue("@series", createdSeriesID);
                cmd1.Parameters.AddWithValue("@analysis", analysisID);
                cmd1.ExecuteNonQuery();
            }
        }
        public void renameSeries(int seriesID, string name) //Update the name of a series
        {
            MySqlCommand cmd1 = new MySqlCommand("UPDATE series SET name=@name WHERE seriesID=@ID",this.conn);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@name",name);
            cmd1.Parameters.AddWithValue("@ID",seriesID);
            cmd1.ExecuteNonQuery();
        }
        public void updateSeriesDescription(int seriesID, string description) //Update the description of a series
        {
            MySqlCommand cmd1 = new MySqlCommand("UPDATE series SET description=@description WHERE seriesID=@ID",this.conn);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@description",description);
            cmd1.Parameters.AddWithValue("@ID",seriesID);
            cmd1.ExecuteNonQuery();
        }
        public int mergeSeries(int seriesID1, int seriesID2) //Merge two series by creating a new series containing all of the elements of both series, and returning the new series ID
        {
            //1 Get all IDs associated with either series
            MySqlCommand cmd1 = new MySqlCommand("SELECT staID_pk FROM seriesToAnalysis WHERE analysisID_fk = @ID1 OR analysisID_fk = @ID2", this.conn);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@ID1", seriesID1);
            cmd1.Parameters.AddWithValue("@ID2", seriesID2);
            MySqlDataReader reader = cmd1.ExecuteReader();

            List<int> IDs = new List<int>();
            while (reader.Read())
            {
                IDs.Add((int)reader[0]);
            }

            //Now use them to create the new series
            createSeries(IDs.ToArray(), "Merged Series", "Merged Series");

            //Get the ID of the newly created series:
            cmd1 = new MySqlCommand("SELECT seriesID from series ORDER BY seriesID DESC LIMIT 1", this.conn);
            return (int) cmd1.ExecuteScalar();
        }
        //Functions for saving analysis results
        /* Writing the results of analysis
        public int saveImageAnalysisResults(int imageID, int analysisType, string[] commonPropertyNames, double[] commonPropertyValues, string[] specificPropertyNames, double[] specificPropertyValues);
        public int saveSeriesAnalysisResults(int seriesID, int seriesAnalysisType, string[] commonPropertyNames, double[] commonPropertyValues, string[] specificPropertyNames, double[] specificPropertyValues);
        */
#endregion

#region Reading functions used by analyzer
        //Functions for getting the image data and variable values:
        public variableStruct readVariableValues(int runID)
        {

            variableStruct outgoing = new variableStruct();

            MySqlCommand cmd1 = new MySqlCommand("SELECT column_name from information_schema.columns where table_name = 'ciceroOut'", this.conn); //get list of columns
            MySqlDataReader varReader = cmd1.ExecuteReader(); //Reading the columns
            List<string> columnNames = new List<string>(); //List of the column names
            List<variable> varsList = new List<variable>(); //List of vars, wil be converted to the output struct


            //First we get all the column names

            while (varReader.Read())
            {
              
                    columnNames.Add(varReader[0].ToString()); //Add each column name
            }

            varReader.Close();
            // Next we get all the values

            cmd1 = new MySqlCommand("SELECT * FROM ciceroOut WHERE runID = @ID", this.conn);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@ID", runID);

            varReader = cmd1.ExecuteReader();

            while (varReader.Read())
            {
                for (int h = 0; h < varReader.FieldCount; h++)
                {
                    variable temp = new variable("-", 0.0);
                    if (varReader[h] != DBNull.Value && columnNames[h] != "timestamp")
                    {
                        temp.Name = columnNames[h];
                        temp.VarValue = Convert.ToDouble(varReader[h]);
                        varsList.Add(temp);
                    }

                }
            }
            varReader.Close();
            outgoing.variableList = varsList.ToArray();
            return outgoing;
        }
        public variableStruct readVariableValues()
        {

            variableStruct outgoing = new variableStruct();
            List<string> columnNames = new List<string>(); //List of the column names
            List<variable> varsList = new List<variable>(); //List of vars, wil be converted to the output struct
            MySqlDataReader varReader;
            try
            {
                MySqlCommand cmd1 = new MySqlCommand("SELECT column_name from information_schema.columns where table_name = 'ciceroOut'", this.conn); //get list of columns
                varReader = cmd1.ExecuteReader(); //Reading the columns
       
                //First we get all the column names

                while (varReader.Read())
                {
                    columnNames.Add(varReader[0].ToString()); //Add each column name
                }

                varReader.Close();
               
                // Next we get all the values

                cmd1 = new MySqlCommand("SELECT * FROM ciceroOut ORDER BY runID DESC LIMIT 1", this.conn);

                varReader = cmd1.ExecuteReader();

                while (varReader.Read())
                {
                    for (int h = 0; h < varReader.FieldCount; h++)
                    {
                        variable temp = new variable("-", 0.0);
                        if (varReader[h] != DBNull.Value && columnNames[h] != "timestamp")
                        {
                            temp.Name = columnNames[h];
                            temp.VarValue = Convert.ToDouble(varReader[h]);
                            varsList.Add(temp);
                        }

                    }
                }
                varReader.Close();
                outgoing.variableList = varsList.ToArray();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
   
                

              
                
           
            return outgoing;
        } //Overloaded - gets most recent
        public freeImageStruct readFreeImageFromCache() //Returns the 3 arrays, camera info, cicero variables. but draws the 3 arrays from the cache
        {
            freeImageStruct outputStruct = new freeImageStruct();
            int height = (int) client.Get("height");
            int width = (int) client.Get("width");

            byte[] shotBytes = (byte[])client.Get("free");

            Int16[] shot = new Int16[height * width];

            Buffer.BlockCopy(shotBytes, 0, shot, 0, shotBytes.Length);

   
            outputStruct.shot = shot;
            outputStruct.height = height;
            outputStruct.width = width;


            return outputStruct;


        }
        public imageStruct readImageFromCache() //Returns the 3 arrays, camera info, cicero variables. but draws the 3 arrays from the cache
        {
            imageStruct outputStruct = new imageStruct();
            int height = new int();
            int width = new int();
            double pixelSize = new double();
            byte[] atomsBytes = (byte[]) client.Get("atoms");
            byte[] noAtomsBytes = (byte[])client.Get("noAtoms");
            byte[] darkBytes = (byte[])client.Get("dark");
            int cameraID = (int)client.Get("camID");
            int runID = (int)client.Get("runID");
            int sequenceID = (int)client.Get("seqID");

            MySqlCommand cmd1 = new MySqlCommand("SELECT cameraHeight,cameraWidth,cameraPixelSize FROM cameras WHERE cameraID = @ID", this.conn);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@ID", cameraID);

            MySqlDataReader reader = cmd1.ExecuteReader();

            while (reader.Read())
            {
                height = (int)reader[0];
                width = (int)reader[1];
                pixelSize = Convert.ToDouble(reader[2]);
            }
            reader.Close();

            Int16[] atoms = new Int16[height*width];
            Int16[] noAtoms = new Int16[height*width];
            Int16[] dark = new Int16[height* width];

            Buffer.BlockCopy(atomsBytes,0,atoms,0,atomsBytes.Length);
            Buffer.BlockCopy(darkBytes, 0, dark, 0, darkBytes.Length);
            Buffer.BlockCopy(noAtomsBytes,0,noAtoms,0,noAtomsBytes.Length);

            outputStruct.variables = readVariableValues();
            outputStruct.atoms = atoms;
            outputStruct.noAtoms = noAtoms;
            outputStruct.dark = dark;
            outputStruct.pixelSize = pixelSize;
            outputStruct.height = height;
            outputStruct.width = width;
            outputStruct.runID = runID;
            outputStruct.seqID = sequenceID;
            outputStruct.cameraID = cameraID;


            return outputStruct;


        }       
        public imageStruct readImage() //(OVERLOADED) Returns the three arrays, camera info, and all cicero variables from the last run
        {

            MySqlCommand cmd1 = new MySqlCommand("SELECT atoms, noAtoms, dark, cameraID_fk,runID_fk,sequenceID_fk,timestamp FROM images ORDER BY imageID DESC LIMIT 1", this.conn);
            cmd1.Prepare();
            imageStruct outputStruct = new imageStruct();
            MySqlDataReader reader = cmd1.ExecuteReader(); //Reading the columns

            int height = new int();
            int width = new int();
            int runID = new int();
            int seqID = new int();
            double pixelSize = new double();
            byte[] atomBytes = null;
            byte[] noAtomBytes = null;
            byte[] darkBytes = null;
            string timestamp = "";


            while (reader.Read())
            {

               atomBytes = (byte[])reader[0];
               noAtomBytes = (byte[])reader[1];
               darkBytes = (byte[])reader[2];
               runID = (int)reader[4];
               seqID = (int)reader[5];
               timestamp = (string)reader[6];
               
                cmd1 = new MySqlCommand("SELECT cameraHeight,cameraWidth,cameraPixelSize FROM cameras WHERE cameraID = @ID", this.conn);
                cmd1.Prepare();
                cmd1.Parameters.AddWithValue("@ID", (int)reader[3]);
            }
                reader.Close();
                MySqlDataReader reader2 = cmd1.ExecuteReader();

             while (reader2.Read())
             {
                    height = (int)reader2[0];
                    width = (int)reader2[1];
                    pixelSize = Convert.ToDouble(reader2[2]);
             }
                reader2.Close();


                Int16[] atoms = new Int16[height * width];
                Int16[] noAtoms = new Int16[height * width];
                Int16[] dark = new Int16[height * width];

                Buffer.BlockCopy(atomBytes, 0, atoms, 0, atomBytes.Length);
                Buffer.BlockCopy(darkBytes, 0, dark, 0, darkBytes.Length);
                Buffer.BlockCopy(noAtomBytes, 0, noAtoms, 0, noAtomBytes.Length);

                outputStruct.variables = readVariableValues();
                outputStruct.timestamp = timestamp;
                outputStruct.atoms = atoms;
                outputStruct.noAtoms = noAtoms;
                outputStruct.dark = dark;
                outputStruct.pixelSize = pixelSize;
                outputStruct.height = height;
                outputStruct.width = width;
                outputStruct.runID = runID;
                outputStruct.seqID = seqID;
     
            
          
            return outputStruct;

        }
        public imageStruct readImage(int imageID) //(OVERLOADED) Returns the three arrays, camera info, and all cicero variables from the specified run.
    {
        MySqlCommand cmd1 = new MySqlCommand("SELECT atoms, noAtoms, dark, cameraID_fk,runID_fk,sequenceID_fk, timestamp FROM images WHERE imageID = @id", this.conn);
        cmd1.Prepare();
        cmd1.Parameters.AddWithValue("@id", imageID);

        imageStruct outputStruct = new imageStruct();
        MySqlDataReader reader = cmd1.ExecuteReader(); //Reading the columns

        int height = new int();
        string timestamp = "";
        int width = new int();
        int runID = new int();
        int seqID = new int();
        double pixelSize = new double();
        byte[] atomBytes = null;
        byte[] noAtomBytes = null;
        byte[] darkBytes = null;


        while (reader.Read())
        {
            atomBytes = (byte[])reader[0];
            noAtomBytes = (byte[])reader[1];
            darkBytes = (byte[])reader[2];
            runID = (int)reader[4];
            seqID = (int)reader[5];
            timestamp = (string)reader[6];

            cmd1 = new MySqlCommand("SELECT cameraHeight,cameraWidth,cameraPixelSize FROM cameras WHERE cameraID = @ID", this.conn);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@ID", (int)reader[3]);
        }
        reader.Close();
        MySqlDataReader reader2 = cmd1.ExecuteReader();

        while (reader2.Read())
        {
            height = (int)reader2[0];
            width = (int)reader2[1];
            pixelSize = Convert.ToDouble(reader2[2]);
        }
        reader2.Close();

        Int16[] atoms = new Int16[height * width];
        Int16[] noAtoms = new Int16[height * width];
        Int16[] dark = new Int16[height * width];

        Buffer.BlockCopy(atomBytes, 0, atoms, 0, atomBytes.Length);
        Buffer.BlockCopy(darkBytes, 0, dark, 0, darkBytes.Length);
        Buffer.BlockCopy(noAtomBytes, 0, noAtoms, 0, noAtomBytes.Length);

        outputStruct.atoms = atoms;
        outputStruct.noAtoms = noAtoms;
        outputStruct.dark = dark;
        outputStruct.timestamp = timestamp;
        outputStruct.pixelSize = pixelSize;
        outputStruct.variables = readVariableValues(imageID);
        outputStruct.height = height;
        outputStruct.width = width;
        outputStruct.runID = runID;
        outputStruct.seqID = seqID;
        return outputStruct;

            
    }
        public imageStruct[] readLastNImages(int N) //Returns the three arrays, camera info, and all of the cicero variables from the last N runs
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT atoms, noAtoms, dark, cameraID_fk, imageID, timestamp FROM images ORDER BY imageID DESC LIMIT @N", this.conn);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@N", N);
            imageStruct outputStruct = new imageStruct();
            MySqlDataReader reader = cmd1.ExecuteReader(); //Reading the columns

            int height = new int();
            int width = new int();
            double pixelSize = new double();
            byte[] atomBytes = null;
            byte[] noAtomBytes = null;
            byte[] darkBytes = null;
            string timestamp = "";
            List<imageStruct> imagesList = new List<imageStruct>();

            while (reader.Read())
            {

                atomBytes = (byte[])reader[0];
                noAtomBytes = (byte[])reader[1];
                darkBytes = (byte[])reader[2];
                timestamp = (string)reader[5];

                cmd1 = new MySqlCommand("SELECT cameraHeight,cameraWidth,cameraPixelSize FROM cameras WHERE cameraID = @ID", this.conn);
                cmd1.Prepare();
                cmd1.Parameters.AddWithValue("@ID", (int)reader[3]);
                MySqlDataReader reader2 = cmd1.ExecuteReader();
                while (reader2.Read())
                {
                    height = (int)reader2[0];
                    width = (int)reader2[1];
                    pixelSize = Convert.ToDouble(reader2[2]);
                }
                reader2.Close();


                Int16[] atoms = new Int16[height * width];
                Int16[] noAtoms = new Int16[height * width];
                Int16[] dark = new Int16[height * width];

                Buffer.BlockCopy(atomBytes, 0, atoms, 0, atomBytes.Length);
                Buffer.BlockCopy(darkBytes, 0, dark, 0, darkBytes.Length);
                Buffer.BlockCopy(noAtomBytes, 0, noAtoms, 0, noAtomBytes.Length);

                outputStruct.atoms = atoms;
                outputStruct.noAtoms = noAtoms;
                outputStruct.dark = dark;
                outputStruct.timestamp = timestamp;
                outputStruct.pixelSize = pixelSize;
                outputStruct.height = height;
                outputStruct.width = width;
                

                imagesList.Add(outputStruct);
            }
            reader.Close();
            return imagesList.ToArray();
        }
        public imageStruct[] readAllImagesFromSequence(int sequenceID) //Returns the three arrays, camera info, and all of the cicero variables from the last sequence
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT atoms, noAtoms, dark, cameraID_fk, imageID, timestamp FROM images WHERE sequenceID_fk = @seqID", this.conn);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@seqID", sequenceID);
            imageStruct outputStruct = new imageStruct();
            MySqlDataReader reader = cmd1.ExecuteReader(); //Reading the columns

            int height = new int();
            int width = new int();
            double pixelSize = new double();
            byte[] atomBytes = null;
            byte[] noAtomBytes = null;
            byte[] darkBytes = null;
            string timestamp = "";
            List<imageStruct> imagesList = new List<imageStruct>();

            while (reader.Read())
            {

                atomBytes = (byte[])reader[0];
                noAtomBytes = (byte[])reader[1];
                darkBytes = (byte[])reader[2];
                timestamp = (string)reader[5];

                cmd1 = new MySqlCommand("SELECT cameraHeight,cameraWidth,cameraPixelSize FROM cameras WHERE cameraID = @ID", this.conn);
                cmd1.Prepare();
                cmd1.Parameters.AddWithValue("@ID", (int)reader[3]);
                MySqlDataReader reader2 = cmd1.ExecuteReader();
                while (reader2.Read())
                {
                    height = (int)reader2[0];
                    width = (int)reader2[1];
                    pixelSize = Convert.ToDouble(reader2[2]);
                }
                reader2.Close();


                Int16[] atoms = new Int16[height * width];
                Int16[] noAtoms = new Int16[height * width];
                Int16[] dark = new Int16[height * width];

                Buffer.BlockCopy(atomBytes, 0, atoms, 0, atomBytes.Length);
                Buffer.BlockCopy(darkBytes, 0, dark, 0, darkBytes.Length);
                Buffer.BlockCopy(noAtomBytes, 0, noAtoms, 0, noAtomBytes.Length);


                outputStruct.atoms = atoms;
                outputStruct.noAtoms = noAtoms;
                outputStruct.dark = dark;
                outputStruct.timestamp = timestamp;
                outputStruct.pixelSize = pixelSize;
                outputStruct.height = height;
                outputStruct.width = width;

   

                imagesList.Add(outputStruct);
            }
            reader.Close();
            return imagesList.ToArray();
        }
        //Functions for populating lists in the analysis gui/data viewer
        public description[] getLastNSeriesIDsAndNames(int N)
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT seriesID,name FROM series ORDER BY seriesID DESC LIMIT @number", this.conn);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@number", N);
            MySqlDataReader reader = cmd1.ExecuteReader();

            int[] IDs = new int[N];
            string[] names = new string[N];
            int k = 0;
            description[] output = new description[N];
            while (reader.Read())
            {
                output[k].ID = (int)reader[0];
                output[k].name = (string)reader[1];
                k++;
            }
            reader.Close();
           
          
            return output;

        }
        public description[] getLastNSequencesIDsAndNames(int N)
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT sequenceID,sequenceName FROM sequence ORDER BY sequenceID DESC LIMIT @number", this.conn);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@number", N);
            MySqlDataReader reader = cmd1.ExecuteReader();

            int[] IDs = new int[N];
            string[] names = new string[N];
            int k = 0;
            description[] output = new description[N];
            while (reader.Read())
            {
                output[k].ID = (int)reader[0];
                output[k].name = (string)reader[1];
                k++;
            }
            reader.Close();

            return output;
        }
        public int[] getLastNImageIDs(int N)
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT imageID from images ORDER BY imageID DESC LIMIT @number", this.conn);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@number", N);
            MySqlDataReader reader = cmd1.ExecuteReader();

            int[] IDs = new int[N];
            int k = 0;
            while (reader.Read())
            {
                IDs[k] = (int)reader[0];
                k++;
            }

            reader.Close();

            return IDs;
        }
        public string[] getLastNImageTimestamps(int N)
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT timestamp from images ORDER BY imageID DESC LIMIT @number", this.conn);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@number", N);
            MySqlDataReader reader = cmd1.ExecuteReader();

            string[] stamps = new string[N];
            int k = 0;
            while (reader.Read())
            {
                if (reader[0] != DBNull.Value) stamps[k] = (string)reader[0];
                else stamps[k] = "None";
                k++;
            }

            reader.Close();

            return stamps;
        }
       //Functions for reading series and sequence information
        public description readSeriesNameAndDescription(int seriesID)
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT name,description FROM series WHERE seriesID = @ID", this.conn);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@ID", seriesID);
            MySqlDataReader reader = cmd1.ExecuteReader();
            description output = new description();

            while(reader.Read()) //Should only be one row obviously
            {
                output.ID = seriesID;
                output.name = (string) reader[0];
                output.descriptionString = (string)reader[1];
            }
            reader.Close();
            return output;
        }
        public object readSequenceNameAndDescription(int sequenceID)
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT sequenceName,sequenceDescription FROM sequence WHERE sequenceID = @ID", this.conn);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@ID", sequenceID);
            MySqlDataReader reader = cmd1.ExecuteReader();
            description output = new description();

            while (reader.Read()) //Should only be one row obviously
            {
                output.ID = sequenceID;
                output.name = (string)reader[0];
                output.descriptionString = (string)reader[1];
            }
            reader.Close();
            return output;
        }
        public int checkForImageUpdate()
        {
            int result = 0;
            using (MySqlCommand cmd1 = new MySqlCommand("SELECT newImage FROM updates", this.conn))
            {
                try
                {
                    result = (int)cmd1.ExecuteScalar();
                }
                catch
                {

                }
            }
            return result;
        }
//        public object readSeriesAnalysisData(int seriesID);

#endregion

#region Genetic Algorithm Functions
  
#endregion 

    }
   
}
#region Datatypes
public struct imageStruct
{
    public Int16[] atoms;
    public Int16[] noAtoms;
    public Int16[] dark;
    public string timestamp;
    public int height;
    public int width;
    public double pixelSize;
    public variableStruct variables;
    public int cameraID; //Only used when reading from cache
    public int runID; //Only used when reading from cache
    public int seqID; //Only used when reading from cache

}
public struct freeImageStruct
{
    public Int16[] shot;
    public int height;
    public int width;


}

public class variable
    {
        private string name;
        public string Name
        {
            set{ this.name = value;}
            get { return this.name; }

        }
        private double varValue;
        public double VarValue
        {
            set { this.varValue = value; }
            get { return this.varValue; }

        }
        public variable(string n, double v)
        {
            name = n;
            varValue = v;
        }
    }
public struct variableStruct
{
    public variable[] variableList;
    public string timestamp;
}
public struct description
{
    public int ID;
    public string name;
    public string descriptionString;
}


#endregion

//Old testing datatypes:
public class testVariable
    {
        private string name;
        public string Name
        {
            set{ this.name = value;}
            get { return this.name; }

        }
        private double varValue;
        public double VarValue
        {
            set { this.varValue = value; }
            get { return this.varValue; }

        }
        public testVariable(string n, double v)
        {
            name = n;
            varValue = v;
        }
    }
    public struct testStruct
        {
              public double[] little;
              public double[] big;
              public string word;

              public testStruct(int i1, int i2)
              {
                  little = new double[i1];
                  big = new double[i2];
                  word = "wee";
              }
        }