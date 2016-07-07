using System;
using System.Collections.Generic;
using System.Text;
using DataStructures;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using DatabaseHelper;
using System.Diagnostics;

namespace Zeus
{
    class ExampleServer : ServerCommunicator
    {

        /// <summary>
        /// To be called to add messages to the server message log. Eventually may support logging to a file
        /// as well as to the screen.
        /// </summary>
        public EventHandler<MessageEvent> messageLog;

        private MainExampleServerlForm exampleServerForm;

        public ExampleServerSettings serverSettings;

        public DatabaseHelper.DatabaseHelper dbhelper;

        public ExampleServer(MainExampleServerlForm form, ExampleServerSettings serverSettings)
        {
            this.exampleServerForm = form;
            this.serverSettings = serverSettings;
            
        }

    
       

        #region Implementation of ServerCommunicator
        public override bool armTasks(UInt32 clockID)
        {
            return true;
        }

        public override BufferGenerationStatus generateBuffers(int listIterationNumber)
        {
            return BufferGenerationStatus.Success;
        }

        public override bool generateTrigger()
        {
            return true;
        }

        public override List<HardwareChannel> getHardwareChannels()
        {
            return new List<HardwareChannel>();
        }

        public override string getServerName()
        {
            return serverSettings.ServerName;
        }

        public override ServerSettingsInterface getServerSettings()
        {
            return this.serverSettings;
        }

        public override void nextRunTimeStamp(DateTime timeStamp)
        {
            
        }

        public override bool outputGPIBGroup(GPIBGroup gpibGroup, SettingsData settings)
        {
            return true;
        }

        public override bool outputRS232Group(RS232Group rs232Group, SettingsData settings)
        {
            return true;
        }

        public override bool outputSingleTimestep(SettingsData settings, SingleOutputFrame output)
        {
            return true;
        }

        public override bool ping()
        {
            return true;
        }

        public override bool runSuccess()
        {
            return true;
        }

        public override bool setSequence(SequenceData sequence)
        {
            return true;
        }

        public override bool setSettings(SettingsData settings)
        {
            return true;
        }

        public override void stop()
        {
            
        }
        
       
        //----------------------------------------------------
        //Begin new methods to be used by the database server
        //----------------------------------------------------
        public override bool checkIfCiceroCanRun()
        {
            bool errorFlag = true;

            if (waitForZeus)
            {
                //1: See if database is responsive
                try
                {
                    dbhelper.getLastRunID();
                    dbhelper.getLastNImageIDs(1);
                }
                catch
                {
                    errorFlag = false;
                }
                //2: See if memcached server is responsive
                try
                {
                    dbhelper.checkForImageUpdate();
                }
                catch
                {
                    errorFlag = false;
                }

                //3: Check heartbeats
                try
                {
                    Dictionary<int, int> activeHeartBeats = new Dictionary<int, int>();
                    for (int i = 0; i < serverSettings.Heartbeats.Count; i++)
                    {
                        heartBeat tempHB = serverSettings.Heartbeats[i];
                        if (activeHeartBeats.ContainsKey(tempHB.group) && exampleServerForm.heartbeatStatuses[i])
                        {
                            activeHeartBeats[tempHB.group]++;
                        }
                        else if (exampleServerForm.heartbeatStatuses[i])
                        {
                            activeHeartBeats.Add(tempHB.group, 1);
                        }

                    }

                    foreach (int groupID in activeHeartBeats.Keys)
                    {
                        if (activeHeartBeats[groupID] != 1)
                        {
                            errorFlag = false;
                        }
                    }

                    if (activeHeartBeats.Count == 0)
                    {
                        errorFlag = false;
                    }



                }

                catch
                {

                }

                //4: Check interlock values

                //Not yet implemented

             
            }
           
            //Check human override
            if (humanOverride == true)
            {
                errorFlag = false;

            }

            if (errorFlag == true)
            {
                exampleServerForm.addMessageLogText(this, new MessageEvent("Run check successful - Cicero is allowed to run."));
            }

            return errorFlag;
        }

        public override bool waitForDatabaseUpdates(List<Variable> Variables)
        {

            //First we update the "last bound variable name" list that belongs to the form
            foreach (Variable var in Variables)
            {
                if (var.DBDriven)
                {
                    exampleServerForm.lastBoundVarName[var.DBFieldNumber - 1] = var.VariableName;
                }
            }

            //Enumerate all of the db-bound variables and pass their db field indices to a dll function that checks if they have been updated
            if (waitForVariableUpdates)
            {
                List<int> indices = new List<int>();
                foreach (Variable var in Variables)
                {
                    if (var.DBDriven)
                    {
                        indices.Add(var.DBFieldNumber);
                    }
                }
                if (!dbhelper.checkIfVariablesHaveBeenUpdated(indices))
                {
                    return false;
                }
                else
                {
                    //Once we've received updated values from all of the variables, we can reset their updated columns to "false";
                    if (indices.Count > 0)
                    {
                        dbhelper.resetVariableUpdateColumns(indices);
                    }
                    exampleServerForm.addMessageLogText(this, new MessageEvent("Database check complete - database-bound variable values have been updated."));
                    return true;
                }
            }
            else
            {
                exampleServerForm.addMessageLogText(this, new MessageEvent("Database check bypassed."));
                return true;
            }
        }

        public override bool writeVariablesIntoDatabase(List<Variable> Variables)
        {
            // Note the unfortunate naming scheme here:
            // Variable (capital V) is the data type for a variable in the Word Generator project
            // variable (little v) is the data type for a variable in the dbhelper project
            variableStruct temporaryVariableStruct = new variableStruct();
            List<variable> temporaryVariableList = new List<variable>();
            foreach (Variable var in Variables)
            {
                variable tempVariable = new variable("default", 0); 
                tempVariable.Name = var.VariableName.Replace(" ",string.Empty); //kill spaces in variable names before database
                tempVariable.VarValue = var.VariableValue;
                temporaryVariableList.Add(tempVariable);
            } 
            temporaryVariableStruct.variableList = temporaryVariableList.ToArray();
            dbhelper.writeVariableValues(temporaryVariableStruct);

            dbhelper.undoUpdateNewImage(); //Also clear the "new image" field in the database (reset it to 0)

            exampleServerForm.addMessageLogText(this, new MessageEvent("Variable value save complete - all variable values have been written into the database."));
            return true;
        }

        public override bool moveImageDataFromCacheToDatabase()
        {
            if (saveToDatabase)
            {
                //1: Check for new cache data, wait if it's not available
                if (dbhelper.checkForImageUpdate() != 1)
                {
                    return false;
                }
                //2: Write the cache data to the database if the checkbox is checked, then return control to Cicero
                else
                {
                    dbhelper.undoUpdateNewImage();
                    int runIDVar = dbhelper.getLastRunID();
                    int seqIDVar = dbhelper.getSequenceID();
                    imageStruct cacheImage = dbhelper.readImageFromCache();
                    dbhelper.writeImageDataToDB(cacheImage.atoms, cacheImage.noAtoms, cacheImage.dark, cacheImage.width, cacheImage.height, cacheImage.cameraID, runIDVar, seqIDVar);
                    exampleServerForm.addMessageLogText(this, new MessageEvent("Image save complete - image data was detected in the cache and moved into the database."));
                    return true;
                }
            }

            else
            {
                exampleServerForm.addMessageLogText(this, new MessageEvent("Image save bypassed - image data was not saved to the database, but may be in the cache."));
                return true;
            }
         
        }


        //----------------------------------------------------
        //End new methods to be used by the database server
        //----------------------------------------------------
        //----------------------------------------------------
        //Begin new properties to be used by the database server
        //----------------------------------------------------

        public bool saveToDatabase = false;
        public bool waitForZeus = false;
        public bool humanOverride = false;
        public bool waitForVariableUpdates = false;

        //----------------------------------------------------
        //End new properties to be used by the database server
        //----------------------------------------------------


        #endregion


        #region Methods called by MainVirgilForm

        public void openConnection()
        {
            dbhelper = new DatabaseHelper.DatabaseHelper(serverSettings.MemcachedServerIP, serverSettings.DatabaseServerIP, serverSettings.Username, serverSettings.Password, serverSettings.DBName);
            messageLog(this, new MessageEvent("Attempting to open connection."));
            Thread thread = new Thread(new ThreadStart(startMarshalProc));
            thread.Start();
        }

        #endregion



        #region Thread procedures

        private object marshalLock = new object();
        private TcpChannel tcpChannel;
        private ObjRef objRef;

        /// <summary>
        /// Adapted from corresponding procedure in AtticusServerRuntime
        /// </summary>
        private void startMarshalProc()
        {
            try
            {
                lock (marshalLock)
                {
                    tcpChannel = new TcpChannel(5678);
                    ChannelServices.RegisterChannel(tcpChannel, false);
                    objRef = RemotingServices.Marshal(this, "serverCommunicator");
                }
                messageLog(this, new MessageEvent("Connection suceeded."));
            }
            catch (Exception e)
            {
                messageLog(this, new MessageEvent("Unable to start Marshal due to exception: " + e.Message + e.StackTrace));
                exampleServerForm.reenableConnectButton();
            }
        }



        #endregion
    }
}
