using System;
using System.Collections.Generic;
using System.Text;
using DataStructures;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO.Ports;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using DataStructures.UtilityClasses;



namespace SnippetServer
{
    class SnippetServerCommunicator: ServerCommunicator
    {

        /// <summary>
        /// Properties
        /// </summary>
        private ServerStructures.ServerCommunicatorStatus communicatorStatus;


        // The next three objects are used in marshalling / unmarshalling this class
        // (ie sharing it over .NET remoting)
        private Object marshalLock = new Object();
        private ObjRef objRef;
        private TcpChannel tcpChannel;

        private object remoteLockObj = new object();

        public ServerStructures.ServerCommunicatorStatus CommunicatorStatus
        {
            get { return communicatorStatus; }
            //set { communicatorStatus = value; }
        }


        //Message log
        private event EventHandler<MessageEvent> messageLogHandler;

        public void registerMessageEventHandler(EventHandler<MessageEvent> handler)
        {
            messageLogHandler += handler;
        }

        public void messageLog(object sender, MessageEvent message)
        {
            if (messageLogHandler != null)
                messageLogHandler(sender, message);
        }



        /// <summary>
        /// Constructors
        /// </summary>
        
        public SnippetServerCommunicator()
        {
            System.Console.WriteLine("Running SnippetServerCommunicator constructor...");
            communicatorStatus = ServerStructures.ServerCommunicatorStatus.Disconnected;
            System.Console.WriteLine("... done running SnippetServerRuntime constructor.");
        }

        /// <summary>
        /// Methods
        /// </summary>
        /// <param name="status"></param>
        public void reachMarshalStatus(ServerStructures.ServerCommunicatorStatus status)
        {
            if (this.communicatorStatus == status) return;


            if (status == ServerStructures.ServerCommunicatorStatus.Connected)
            {
                Thread thread = new Thread(new ThreadStart(startMarshalProc));
                thread.Start();
            }

            if (status == ServerStructures.ServerCommunicatorStatus.Disconnected)
            {
                Thread thread = new Thread(new ThreadStart(stopMarshalProc));
                thread.Start();
            }

            return;
        }

        private void startMarshalProc()
        {
            try
            {
                lock (marshalLock)
                {

                    communicatorStatus = ServerStructures.ServerCommunicatorStatus.Connecting;
                   // updateGUI(this, null);
                    tcpChannel = new TcpChannel(5678);
                    ChannelServices.RegisterChannel(tcpChannel, false);
                    objRef = RemotingServices.Marshal(this, "serverCommunicator");
                    communicatorStatus = ServerStructures.ServerCommunicatorStatus.Connected;
                }
                System.Console.WriteLine("SnippetServerCommunicator Marshalled.");
               // updateGUI(this, null)
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Unable to start Marshal due to exception: " + e.Message + e.StackTrace);
               // messageLog(this, new MessageEvent("Unable to start Marshal due to exception: " + e.Message + e.StackTrace));
               // displayError();
                communicatorStatus = ServerStructures.ServerCommunicatorStatus.Disconnected;
               // updateGUI(this, null);
            }
        }

        private void stopMarshalProc()
        {

            System.Console.WriteLine("Server disconnected is not currently implemented. You can achieve this functionality by restarting the server.");
            return;
          
        }






        public override void nextRunTimeStamp(DateTime timeStamp)
        {

        }


        /// <summary>
        /// Outputs a single timestep.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override bool outputSingleTimestep(SettingsData settings, SingleOutputFrame output)
        {
            return true;
        }


        /// <summary>
        /// Gets the server settings object for this server.
        /// </summary>
        /// <returns></returns>
        public override ServerSettingsInterface getServerSettings()
        {
            return null;
        }


        /// <summary>
        /// Outputs a single gpib group. (evaluated at the beginning of the group.)
        /// </summary>
        /// <param name="gpibGroup"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public override bool outputGPIBGroup(GPIBGroup gpibGroup, SettingsData settings)
        {
            return true;
        }


        /// <summary>
        /// Outputs a single rs232 Group. (evaluated at the beginning of the group.)
        /// </summary>
        /// <param name="rs232Group"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public override bool outputRS232Group(RS232Group rs232Group, SettingsData settings)
        {
            return true;
        }


        /// <summary>
        /// Gets a list of hardware channels attached to server.
        /// </summary>
        /// <returns></returns>
        public override List<HardwareChannel> getHardwareChannels()
        {
            return null;
        }


        /// <summary>
        /// Sets the sequence which is to be run.
        /// </summary>
        /// <param name="sequence"></param>
        public override bool setSequence(SequenceData sequence)
        {
            return true;
        }


        /// <summary>
        /// Sets the settings object.
        /// </summary>
        public override bool setSettings(SettingsData settings)
        {
            return true;
        }




        /// <summary>
        /// Prepares the server for a run. Generates relevant buffers / "tasks". This function is blocking, it wont return until
        /// the tasks are generated. Thus, it should not be run in a UI thread.
        /// </summary>
        /// <param name="listIterationNumber"></param>
        public override BufferGenerationStatus generateBuffers(int listIterationNumber)
        {
            return BufferGenerationStatus.Success;
        }


        /// <summary>
        /// Generates a triggers. This in effect starts the sequence output.
        /// </summary>
        public override bool generateTrigger()
        {
            return true;
        }


        /// <summary>
        /// Starts any Hardware Triggered tasks, or any software triggered tasks which run off an external sample clock.
        /// 
        /// clockID is a randomly-generated-per-shot number that is used to uniquely identify a shared network clock
        /// </summary>
        /// <returns></returns>
        public override bool armTasks(UInt32 clockID)
        {
            return true;
        }



        /// <summary>
        /// Returns true if the most recent run completed sucessfully, false otherwise.
        /// </summary>
        /// <returns></returns>
        public override bool runSuccess()
        {
            return true;
        }



        public override string getServerName()
        {
            return "Snippet Server";
        }


        public override bool ping()
        {
            lock (remoteLockObj)
            {
                Console.WriteLine("Received a PING.");
                return true;
            }
        }


        public override void stop()
        {
            
        }

    }
}
