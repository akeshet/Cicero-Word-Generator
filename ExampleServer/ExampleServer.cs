using System;
using System.Collections.Generic;
using System.Text;
using DataStructures;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Virgil
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


        #endregion


        #region Methods called by MainVirgilForm

        public void openConnection()
        {
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
