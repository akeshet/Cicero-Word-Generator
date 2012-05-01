using System;
using System.Collections.Generic;
using System.Text;
using DataStructures;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Windows.Forms;
using NationalInstruments.DAQmx;
using System.ComponentModel;
using System.IO.Ports;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using DataStructures.UtilityClasses;



namespace AtticusServer
{

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class AtticusServerCommunicator : ServerCommunicator
    {

        private void displayError()
        {
            if (MainServerForm.instance != null)
            {
                MainServerForm.instance.DisplayError = true;
            }
        }
        AnalogMultiChannelReader reader_analog_S7; //AnalogMultiChannelReader object for analog in
        List<String> analog_in_names; //List of the custom names fo the analog innputs
        Task analogS7ReadTask; // Task for analog input acquisition
        DateTime runTime; //Timestamp used to name files
        bool analogInCardDetected = false; // boolean detecting the existence of a 6259 card with analog inputs activated


        /// <summary>
        /// This is a lock object, used to make sure that only one instance of any remotely-called method is running at a time.
        /// The remotely called methods all lock this object.
        /// </summary>
        private object remoteLockObj = new object();

        private List<HardwareChannel> myHardwareChannels;

        [Description("A list of the hardware channels on this server.")]
        public List<HardwareChannel> MyHardwareChannels
        {
            get { return myHardwareChannels; }
            //       set { myHardwareChannels = value; }
        }

        /// <summary>
        /// List of strings giving the device id strings for all the devices connected this this server.
        /// Populated in refreshHardwareLists(). 
        /// </summary>
        private List<string> detectedDevices;

        /// <summary>
        /// List of opal kelly devices in use.
        /// </summary>
        private List<com.opalkelly.frontpanel.okCFrontPanel> opalKellyDevices;

        private List<string> opalKellyDeviceNames;

        /// <summary>
        /// To be called when the atticus server UI needs to be updated.
        /// </summary>
        public EventHandler updateGUI;

        /// <summary>
        /// To be called to add messages to the server message log. Eventually may support logging to a file
        /// as well as to the screen.
        /// </summary>
        public MainServerForm.MessageEventCallDelegate messageLog;

        // The next three objects are used in marshalling / unmarshalling this class
        // (ie sharing it over .NET remoting)
        private Object marshalLock = new Object();
        private ObjRef objRef;
        private TcpChannel tcpChannel;

        /// <summary>
        /// A list to keep track of which timing connections have been made, so that connections can be made 
        /// and unmade without needing to constantly reset the DAQMx devices.
        /// </summary>
        private List<TerminalPair> madeConnections;



        private ServerSettings myServerSettings;

        [Description("Settings object of this server.")]
        public ServerSettings serverSettings
        {
            get { return myServerSettings; }
            set { myServerSettings = value; }
        }


        private ServerStructures.ServerCommunicatorStatus communicatorStatus;

        [Description("Communication status of the server.")]
        public ServerStructures.ServerCommunicatorStatus CommunicatorStatus
        {
            get { return communicatorStatus; }
            //set { communicatorStatus = value; }
        }

        /// <summary>
        /// The sequence data that will next be run. This is sent from the client via the setSequence method.
        /// </summary>
        private SequenceData sequence;

        /// <summary>
        /// Settings data for the next run. This is sent from the client via the setSettings method.
        /// </summary>
        private SettingsData settings;

        /// <summary>
        /// A mapping from string device names to NI DAQmx Task object, for the DAQmx tasks that are to be run.
        /// Populated in generateBuffers method.
        /// </summary>
        private Dictionary<string, Task> daqMxTasks;

        /// <summary>
        /// A mapping from device name to Fpga Timebase Task for all the FPGA tasks that are to be run.
        /// </summary>
        private Dictionary<string, FpgaTimebaseTask> fpgaTasks;

        /// <summary>
        /// A Mapping from hardware channels to corresponding gpib tasks that are to be run. Note that there is one task
        /// per connected gpib device (ie scope, function generator, etc.), not one per gpib output card.
        /// Populated in generateBuffers.
        /// </summary>
        private Dictionary<HardwareChannel, GpibTask> gpibTasks;

        /// <summary>
        /// Ditto for rfsg tasks.
        /// </summary>
        private Dictionary<HardwareChannel, RfsgTask> rfsgTasks;

        /// <summary>
        /// Mapping from hardware channels to corresponding rs232 tasks that are to be run.
        /// </summary>
        private Dictionary<HardwareChannel, RS232Task> rs232Tasks;


        /// <summary>
        /// Contains one entry for each logical ID - HardwareChannel pair which was found in settings data and which
        /// resides on this server. Populated in findMyChannels.
        /// </summary>
        private Dictionary<int, HardwareChannel> usedDigitalChannels;

        /// <summary>
        /// Contains one entry for each logical ID - HardwareChannel pair which was found in settings data and which
        /// resides on this server. Populated in findMyChannels.
        /// </summary>
        private Dictionary<int, HardwareChannel> usedAnalogChannels;

        /// <summary>
        /// List of the string identifiers of the DaqMx devices that are used in the next run. Populated in findMyChannels.
        /// </summary>
        private List<string> usedDaqMxDevices;

        /// <summary>
        /// Contains one entry for each logical ID - HardwareChannel pair which was found in settings data and which
        /// resides on this server. Populated in findMyChannels.
        /// </summary>
        private Dictionary<int, HardwareChannel> usedGpibChannels;

        /// <summary>
        /// Contains one entry for each logical ID - HardwareChannel pair which was found in settings data, which is
        /// an rs232 channel, and which resides on this server.
        /// </summary>
        private Dictionary<int, HardwareChannel> usedRS232Channels;

        #region Constructors
        public AtticusServerCommunicator(ServerSettings settings)
        {

            System.Console.WriteLine("Running AtticusServerRuntime constructor...");

            myServerSettings = settings;

            refreshHardwareLists();

            communicatorStatus = ServerStructures.ServerCommunicatorStatus.Disconnected;

            // marshal the serverCommunicator if the start up settings say to do so.
            if (settings.ConnectOnStartup)
                reachMarshalStatus(ServerStructures.ServerCommunicatorStatus.Connected);



            System.Console.WriteLine("... done running AtticusServerRuntime constructor.");

        }
        #endregion


        #region Implementation of ServerCommunicator interface

        public override bool ping()
        {
            lock (remoteLockObj)
            {
                messageLog(this, new MessageEvent("Received a PING."));
                return true;
            }
        }

        public override string getServerName()
        {
            lock (remoteLockObj)
            {
                return myServerSettings.ServerName;
            }
        }

        public override List<HardwareChannel> getHardwareChannels()
        {
            lock (remoteLockObj)
            {
                messageLog(this, new MessageEvent("Received getHardwareChannels() request."));
                return myHardwareChannels;
            }
        }

        /// <summary>
        /// Outputs a single output frame to analog and digital cards. All other cards / outputs are unaffected.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override bool outputSingleTimestep(SettingsData settings, SingleOutputFrame output)
        {
            lock (remoteLockObj)
            {
                try
                {
                    messageLog(this, new MessageEvent("Received an output single timestep request."));


                    if (output == null)
                    {
                        messageLog(this, new MessageEvent("Received a null output object. Unable to comply."));
                        return false;
                    }

                    if (!stopAndCleanupTasks())
                        return false;

                    if (!setSettings(settings))
                        return false;

                    foreach (string dev in usedDaqMxDevices)
                    {

                        DeviceSettings deviceSettings = myServerSettings.myDevicesSettings[dev];
                        if (deviceSettings.DeviceEnabled)
                        {
                            messageLog(this, new MessageEvent("Generating single output for " + dev));
                            Task task = DaqMxTaskGenerator.createDaqMxTaskAndOutputNow(dev,
                                deviceSettings,
                                output,
                                settings,
                                usedDigitalChannels,
                                usedAnalogChannels);

                            daqMxTasks.Add(dev, task);
                            messageLog(this, new MessageEvent("Success."));
                        }
                        else
                        {
                            messageLog(this, new MessageEvent("Skipped buffer generation for disabled device " + dev));
                        }
                    }

                    return true;
                }
                catch (Exception e)
                {
                    messageLog(this, new MessageEvent("Caught exception when attempting output of single timestep: " + e.Message + e.StackTrace));
                    displayError();
                    return false;
                }
            }
        }


        public override ServerSettingsInterface getServerSettings()
        {
            lock (remoteLockObj)
            {
                return this.serverSettings;
            }
        }

        public override void nextRunTimeStamp(DateTime timeStamp)
        {


            runTime = timeStamp;
            lock (remoteLockObj)
            {
                return;
            }
        }


        private delegate void voidObjDel(object o);

        private Task variableTimebaseClockTask;

        /// <summary>
        /// This method should be run after setSettings and setSequence to ensure sane data.
        /// </summary>
        /// <param name="listIterationNumber"></param>
        /// <returns></returns>
        public override BufferGenerationStatus generateBuffers(int listIterationNumber)
        {
            lock (remoteLockObj)
            {
                clientFinishedRun = false;
                if (taskFinishTimeClicks == null)
                    taskFinishTimeClicks = new List<long>();
                lock (taskFinishTimeClicks)
                {
                    taskFinishTimeClicks.Clear();
                }

                expectedNumberOfSamplesGenerated = new Dictionary<string, long>();


                try
                {

                    messageLog(this, new MessageEvent("Stopping and cleaning up old tasks."));
                    if (!stopAndCleanupTasks())
                        return BufferGenerationStatus.Failed_Buffer_Underrun;






                    messageLog(this, new MessageEvent("Generating buffers."));
                    if (settings == null)
                    {
                        messageLog(this, new MessageEvent("Unable to generate buffers. Null settings."));
                        displayError();
                        return BufferGenerationStatus.Failed_Settings_Null;
                    }
                    if (sequence == null)
                    {
                        messageLog(this, new MessageEvent("Unable to generate buffers. Null sequence."));
                        displayError();
                        return BufferGenerationStatus.Failed_Sequence_Null;
                    }

                    // This is redundant.
                    sequence.ListIterationNumber = listIterationNumber;

                    #region Generate variable timebase FPGA task

                    if (serverSettings.UseOpalKellyFPGA)
                    {
                        fpgaTasks = new Dictionary<string, FpgaTimebaseTask>();
                        foreach (DeviceSettings fsettings in myServerSettings.myDevicesSettings.Values)
                        {
                            if (fsettings.IsFPGADevice)
                            {
                                if (fsettings.DeviceEnabled)
                                {
                                    if (fsettings.UsingVariableTimebase)
                                    {
                                        if (opalKellyDeviceNames.Contains(fsettings.DeviceName))
                                        {
                                            messageLog(this, new MessageEvent("Creating Variable Timebase Task on fpga device " + fsettings.DeviceName + "."));
                                            int nSegs = 0;
                                            FpgaTimebaseTask ftask = new FpgaTimebaseTask(fsettings,
                                                opalKellyDevices[opalKellyDeviceNames.IndexOf(fsettings.DeviceName)],
                                                sequence,
                                                ((double)1) / ((double)(fsettings.SampleClockRate)),
                                                out nSegs,
                                                myServerSettings.UseFpgaRfModulatedClockOutput,
                                                myServerSettings.UseFpgaAssymetricDutyCycleClocking);
                                            fpgaTasks.Add(fsettings.DeviceName, ftask);
                                            messageLog(this, new MessageEvent("...Done (" + nSegs + " segments total)"));
                                        }
                                        else
                                        {
                                            messageLog(this, new MessageEvent("FPGA device " + fsettings.DeviceName + " is not programmed. Please press the refresh hardware button to program it. Or, if variable timebase on this device is not desired, set DeviceEnabled to false in this device's settings."));
                                            messageLog(this, new MessageEvent("Unable to create variable timebase output on FPGA device. Aborting buffer generation."));
                                            displayError();
                                            return BufferGenerationStatus.Failed_Invalid_Data;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion


                    #region Generate variable timebase clock output task

                    // If a variable timebase is to be generated by a NI digital output, 
                    // then generate the variable timebase output task.
                    if (serverSettings.VariableTimebaseOutputChannel != null && serverSettings.VariableTimebaseOutputChannel != "")
                    {
                        messageLog(this, new MessageEvent("Generating Variable Timebase output clock buffer."));

                        bool otherChannelsOnVariableTimebaseDeviceAlsoUsed = false;
                        foreach (HardwareChannel hc in usedDigitalChannels.Values)
                        {
                            // if the digital channel for the variable timebase is also bound to a logical channel,
                            // complain
                            if (hc.physicalChannelName().ToUpper() == serverSettings.VariableTimebaseOutputChannel)
                            {
                                messageLog(this, new MessageEvent("The variable timebase clock output channel also has a sequence-specified channel bound to it. This is not allowed."));
                                displayError();
                                return BufferGenerationStatus.Failed_Invalid_Data;
                            }

                            // detect if the variable timebase output channel is on a device which also has other
                            // used digital channels.
                            if (hc.DeviceName.ToUpper() == HardwareChannel.parseDeviceNameStringFromPhysicalChannelString(serverSettings.VariableTimebaseOutputChannel).ToUpper())
                            {
                                otherChannelsOnVariableTimebaseDeviceAlsoUsed = true;
                            }
                        }

                        // if the variable timebase output is on the same
                        // device as other digital output channels,
                        // then create the variable timebase task
                        // with the fancy function that can create a shared buffer
                        // for the variable timebase and for the digital output.
                        // Otherwise, use the simpler function which cannot.
                        // NOTE: The above comment is currently incorrect. The variable timebase output
                        // cannot currently exist on the same task as other used channels. Atticus will 
                        // complain in a descriptive way if this is attempted.
                        if (otherChannelsOnVariableTimebaseDeviceAlsoUsed)
                        {

                            string variableTimebaseOutputDevice = HardwareChannel.parseDeviceNameStringFromPhysicalChannelString(serverSettings.VariableTimebaseOutputChannel);

                            if (!variableTimebaseOutputDevice.Contains("Dev"))
                            {
                                messageLog(this, new MessageEvent("******* You are using a NI device named " + variableTimebaseOutputDevice + ". This does not follow the convention of naming your devices Dev1, Dev2, Dev3, etc. Unpredictable results are possible! Not recommended! *******"));
                                displayError();
                            }

                            // NOTE! This call will modify useDigitalChannels. Those digital channels which
                            // get their buffers generated within this task will be removed. This is useful,
                            // because we may later in generateBuffers() do another
                            // call to generate another task on this device, but 
                            // we want that call to only generate buffers for the remaining digital channels.

                            int nDigitals = usedDigitalChannels.Count;

                            if (myServerSettings.myDevicesSettings.ContainsKey(variableTimebaseOutputDevice))
                            {
                                messageLog(this, new MessageEvent("Variable timebase output device [" + variableTimebaseOutputDevice + "]"));

                                variableTimebaseClockTask = DaqMxTaskGenerator.createDaqMxDigitalOutputAndVariableTimebaseSource(
                                    serverSettings.VariableTimebaseOutputChannel,
                                    null,
                                    serverSettings.VariableTimebaseMasterFrequency,
                                    sequence,
                                    serverSettings.VariableTimebaseType,
                                    variableTimebaseOutputDevice,
                                    serverSettings.myDevicesSettings[variableTimebaseOutputDevice],
                                    settings,
                                    usedDigitalChannels,
                                    serverSettings);
                            }
                            else
                            {
                                messageLog(this, new MessageEvent("***** Server does not contain device named " + variableTimebaseOutputDevice));
                                displayError();
                                return BufferGenerationStatus.Failed_Invalid_Data;
                            }

                            variableTimebaseClockTask.Done += new TaskDoneEventHandler(aTaskFinished);

                            int consumedChannels = nDigitals - usedDigitalChannels.Count;
                            messageLog(this, new MessageEvent(consumedChannels.ToString() + " output buffers also generated. Note, buffer generation on device " + variableTimebaseOutputDevice + " may skip due to no additional channels."));
                        }
                        else
                        {

                            string variableTimebaseOutputDevice = HardwareChannel.parseDeviceNameStringFromPhysicalChannelString(serverSettings.VariableTimebaseOutputChannel);

                            if (myServerSettings.myDevicesSettings.ContainsKey(variableTimebaseOutputDevice))
                            {
                                messageLog(this, new MessageEvent("Variable timebase output device [" + variableTimebaseOutputDevice + "]"));

                                variableTimebaseClockTask = DaqMxTaskGenerator.createDaqMxVariableTimebaseSource(
                                    serverSettings.VariableTimebaseOutputChannel,
                                    serverSettings.VariableTimebaseMasterFrequency,
                                    sequence,
                                    serverSettings.VariableTimebaseType,
                                    serverSettings,
                                    serverSettings.myDevicesSettings[variableTimebaseOutputDevice]);
                            }
                            else
                            {
                                messageLog(this, new MessageEvent("***** Server does not contain device named " + variableTimebaseOutputDevice));
                                displayError();
                                return BufferGenerationStatus.Failed_Invalid_Data;
                            }


                            variableTimebaseClockTask.Done += new TaskDoneEventHandler(aTaskFinished);
                        }

                        try
                        {

                            messageLog(this, new MessageEvent("Variable timebase output clock buffer generated successfully. " + variableTimebaseClockTask.Stream.Buffer.OutputBufferSize + " samples per channel. On board buffer size: " + variableTimebaseClockTask.Stream.Buffer.OutputOnBoardBufferSize + " samples per channel."));
                        }
                        catch (Exception)
                        {
                            messageLog(this, new MessageEvent("Unable to poll task for buffer information. This is probably not a problem."));
                        }
                    }
                    else
                    {
                        variableTimebaseClockTask = null;
                    }

                    #endregion

                    #region Analog and Digital NI device generation (daqMx)

                    /// Is multi-threaded buffer generation enabled?
                    if (!serverSettings.UseMultiThreadedDaqmxBufferGeneration)
                    {
                        // if not, generate each buffer sequentially, in this thread.
                        foreach (string dev in usedDaqMxDevices)
                        {
                            if (!dev.Contains("Dev"))
                            {
                                messageLog(this, new MessageEvent("******* You are using a NI device named " + dev + ". This does not follow the convention of naming your devices Dev1, Dev2, Dev3, etc. Unpredictable results are possible! Not recommended! *******"));
                                displayError();
                            }

                            generateDaqMxTaskOnDevice(dev);
                        }
                    }
                    else
                    {
                        // if yes, generate each buffer in a parallel thread.

                        List<Thread> generateThreads = new List<Thread>();
                        try
                        {

                            messageLog(this, new MessageEvent("Generating buffers in parallel..."));
                            foreach (string dev in usedDaqMxDevices)
                            {
                                if (!dev.Contains("Dev"))
                                {
                                    messageLog(this, new MessageEvent("******* You are using a NI device named " + dev + ". This does not follow the convention of naming your devices Dev1, Dev2, Dev3, etc. Unpredictable results are possible! Not recommended! *******"));
                                    displayError();
                                }
                                Thread thread = new Thread(generateDaqMxTaskOnDevice);
                                generateThreads.Add(thread);

                                thread.Start(dev);
                            }
                            // wait for threads to all complete.

                            foreach (Thread thread in generateThreads)
                            {
                                thread.Join();
                            }
                            messageLog(this, new MessageEvent("...done."));
                        }
                        finally
                        {
                            foreach (Thread thread in generateThreads)
                            {
                                thread.Abort();
                            }
                        }

                    }

                    #endregion

                    // This is where the analog input task is defined
                    #region Analog In Task
                    
					//First we determine if an analog in task should be created
                    foreach (DeviceSettings ds in AtticusServer.server.serverSettings.myDevicesSettings.Values)
                    {
                        analogInCardDetected |= ds.DeviceDescription.Contains("6259") && ds.AnalogInEnabled;
                    }

                    if (analogInCardDetected)
                    {
                        // Creates the analog in task
                        analogS7ReadTask = new Task();
                        analogS7ReadTask.SynchronizeCallbacks = false;
                        analog_in_names = new List<string>();
                        bool isUsed;


                       // Obtains a list of the analog in channels to be included in the task, and their name
                        AnalogInChannels the_channels = AtticusServer.server.serverSettings.AIChannels[0];
                        AnalogInNames the_names = AtticusServer.server.serverSettings.AINames[0];

                       // We use part of the channels as single ended, and part as differential. He channels are added here. The task is created on Slot 7. This shouldn't be hard coded and will be changed.
                        #region Single ended
                        for (int analog_index = 0; analog_index < 10; analog_index++)
                        {
                            PropertyInfo the_channel = the_channels.GetType().GetProperty("AS" + analog_index.ToString());
                            isUsed = (bool)the_channel.GetValue(the_channels, null);
                            if (isUsed)
                            {
                                if (analog_index < 5)
                                {
                                    analogS7ReadTask.AIChannels.CreateVoltageChannel("PXI1Slot7/ai" + analog_index.ToString(), "",
                                        AITerminalConfiguration.Nrse, -10, 10, AIVoltageUnits.Volts);
                                }
                                else
                                {
                                    analogS7ReadTask.AIChannels.CreateVoltageChannel("PXI1Slot7/ai" + (analog_index + 3).ToString(), "",
                                        AITerminalConfiguration.Nrse, -10, 10, AIVoltageUnits.Volts);
                                }
                                string theName = (string)(the_names.GetType().GetProperty("AS" + analog_index.ToString()).GetValue(the_names, null));
                                if (theName == "")
                                    analog_in_names.Add("AIS" + analog_index.ToString());
                                else
                                    analog_in_names.Add(theName);
                            }
                        }
                        #endregion

                        #region Differential
                        for (int analog_index = 0; analog_index < 11; analog_index++)
                        {
                            PropertyInfo the_channel = the_channels.GetType().GetProperty("AD" + NamingFunctions.number_to_string(analog_index, 2));
                            isUsed = (bool)the_channel.GetValue(the_channels, null);

                            if (isUsed)
                            {
                                if (analog_index < 3)
                                {
                                    analogS7ReadTask.AIChannels.CreateVoltageChannel("PXI1Slot7/ai" + (analog_index + 5).ToString(), "",
                                        AITerminalConfiguration.Differential, -10, 10, AIVoltageUnits.Volts);
                                }
                                else
                                {
                                    analogS7ReadTask.AIChannels.CreateVoltageChannel("PXI1Slot7/ai" + (analog_index + 13).ToString(), "",
                                        AITerminalConfiguration.Differential, -10, 10, AIVoltageUnits.Volts);
                                }
                                string theName = (string)(the_names.GetType().GetProperty("AD" + NamingFunctions.number_to_string(analog_index, 2)).GetValue(the_names, null));
                                if (theName == "")
                                    analog_in_names.Add("AID" + analog_index.ToString());
                                else
                                    analog_in_names.Add(theName);
                            }
                        }
                        #endregion
                        
                        // Configure timing specs of the analog In task. Again these things shouldn't be hard coded and will be changed    
                        analogS7ReadTask.Timing.ConfigureSampleClock("", (double)(AtticusServer.server.serverSettings.AIFrequency), SampleClockActiveEdge.Rising,
                            SampleQuantityMode.FiniteSamples, sequence.nSamples(1 / ((double)(AtticusServer.server.serverSettings.AIFrequency))));

                        analogS7ReadTask.Timing.ReferenceClockSource = "/PXI1Slot7/PXI_Trig7";
                        analogS7ReadTask.Timing.ReferenceClockRate = 20000000;
                        analogS7ReadTask.Stream.Timeout = Convert.ToInt32(sequence.SequenceDuration * 1000) + 10000;

                        reader_analog_S7 = new AnalogMultiChannelReader(analogS7ReadTask.Stream);

                    }
                    #endregion

                    #region Watchdog Timer stuff -- NOT FINISHED YET, THUS COMMENTED OUT


                    if (serverSettings.UseWatchdogTimerMonitoringTask)
                    {
                        messageLog(this, new MessageEvent("Watchdog Timer tasks no longer supported. Ignoring serverSettings.UseWatchdogTimerMonitoringTask"));
                        /*
                        if (daqMxTasks.ContainsKey(serverSettings.DeviceToSyncSoftwareTimedTasksTo))
                        {
                            messageLog(this, new MessageEvent("Creating watchdog timer monitoring task"));
                            WatchdogTimerTask wtask = new WatchdogTimerTask(sequence, myServerSettings.myDevicesSettings[serverSettings.DeviceToSyncSoftwareTimedTasksTo].SampleClockRate, daqMxTasks[serverSettings.DeviceToSyncSoftwareTimedTasksTo], .2);
                        }
                        else
                        {
                            messageLog(this, new MessageEvent("Unable to create watchdog timer monitoring task, since the hardware-timed device it was to be synched to is not being used. Continuing without watchdog."));
                        }*/
                    }



                    #endregion




                    #region GPIB device and masquerading gpib channels (like rfsg channels)

                    foreach (int gpibID in usedGpibChannels.Keys)
                    {
                        HardwareChannel gpibChannel = usedGpibChannels[gpibID];
                        if (!gpibChannel.gpibMasquerade)
                        {
                            messageLog(this, new MessageEvent("Generating gpib buffer for gpib ID " + gpibID));

                            NationalInstruments.NI4882.Device gpibDevice = new NationalInstruments.NI4882.Device(
                                gpibChannel.gpibBoardNumber(), gpibChannel.GpibAddress);
                            GpibTask gpibTask = new GpibTask(gpibDevice);
                            gpibTask.generateBuffer(sequence, myServerSettings.myDevicesSettings[gpibChannel.DeviceName],
                                gpibChannel, gpibID, myServerSettings.GpibRampConverters);
                            gpibTask.Done += new TaskDoneEventHandler(aTaskFinished);
                            gpibTasks.Add(gpibChannel, gpibTask);
                            messageLog(this, new MessageEvent("Done."));
                        }
                        else
                        {
                            switch (gpibChannel.myGpibMasqueradeType)
                            {
                                case HardwareChannel.GpibMasqueradeType.NONE:
                                    messageLog(this, new MessageEvent("********** Error. GPIB channel with ID " + gpibID + " has its masquerading bit set to true, but has its masquerading type set to NONE. **********"));
                                    displayError();
                                    break;
                                case HardwareChannel.GpibMasqueradeType.RFSG:
                                    messageLog(this, new MessageEvent("Generating RFSG buffer for gpib ID " + gpibID));
                                    RfsgTask rftask = new RfsgTask(sequence, settings, gpibID, gpibChannel.DeviceName, serverSettings.myDevicesSettings[gpibChannel.DeviceName]);
                                    rftask.Done += new TaskDoneEventHandler(aTaskFinished);
                                    rfsgTasks.Add(gpibChannel, rftask);
                                    messageLog(this, new MessageEvent("Done."));
                                    break;
                            }
                        }
                    }

                    #endregion

                    #region RS 232 Devices
                    foreach (int rs232ID in usedRS232Channels.Keys)
                    {
                        if (sequence.rs232ChannelUsed(rs232ID))
                        {
                            messageLog(this, new MessageEvent("Generating rs232 buffer for rs232 ID " + rs232ID));
                            HardwareChannel hc = usedRS232Channels[rs232ID];
                            //NationalInstruments.VisaNS.SerialSession device = new NationalInstruments.VisaNS.SerialSession(hc.ChannelName);

                            NationalInstruments.VisaNS.SerialSession device;

                            try
                            {
                                device = getSerialSession(hc);
                            }
                            catch (Exception e)
                            {
                                messageLog(this, new MessageEvent("Caught exception when attempting to open serial device for rs232 channel #" + rs232ID + ". Exception: " + e.Message + e.StackTrace));
                                return BufferGenerationStatus.Failed_Out_Of_Memory;
                            }

                            //                NationalInstruments.VisaNS.RegisterBasedSession device = (NationalInstruments.VisaNS.RegisterBasedSession) NationalInstruments.VisaNS.ResourceManager.GetLocalManager().Open(hc.ChannelName);

                            RS232Task rs232task = new RS232Task(device);
                            rs232task.generateBuffer(sequence, myServerSettings.myDevicesSettings["Serial"], hc, rs232ID);
                            rs232task.Done += new TaskDoneEventHandler(aTaskFinished);
                            rs232Tasks.Add(hc, rs232task);
                            messageLog(this, new MessageEvent("Done."));
                        }
                        else
                        {
                            messageLog(this, new MessageEvent("Skipping rs232 channel ID " + rs232ID + ", that channel is not used in this sequence."));
                        }
                    }



                    #endregion


                    makeTerminalConnections();


                    // Try to clean up as much memory as possible so that there wont be any garbage collection
                    // during the run. Suspect that GCs during the run may be the cause of sporadic buffer underruns.
                    // Note: This is likely wrong. Most of these buffer underruns were eventually fixed with the 
                    // data transfer mechanism tweaks described in the user manual. However, no harm in doing some
                    // GC here.
                    System.GC.Collect();
                    System.GC.Collect();
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();

                    messageLog(this, new MessageEvent("Buffers generated succesfully."));


                    return BufferGenerationStatus.Success;

                }
                catch (Exception e)
                {
                    messageLog(this, new MessageEvent("Failed buffer generation due to exception: " + e.Message + "\n" + e.StackTrace));
                    displayError();
                    return BufferGenerationStatus.Failed_Invalid_Data;
                }
            }

        }

        /// <summary>
        /// A flag as to weather any of the tasks have thrown an error while running. This flag gets set by aTaskFinished eventhandler, and
        /// gets cleared by stopAndCleanupTasks()
        /// </summary>
        private bool taskErrorsDetected = false;

        public List<long> taskFinishTimeClicks;

        /// <summary>
        /// Event handler that gets called (by a task) whenever a task finishes. If there is an error in the task, then it will get reported here.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void aTaskFinished(object sender, TaskDoneEventArgs e)
        {
            long eventTime = DateTime.Now.Ticks;

            if (e.Error != null)
            {
                messageLog(this, new MessageEvent("A task ended prematurely due to an error: " + e.Error.Message + e.Error.StackTrace));
                displayError();
                taskErrorsDetected = true;
            }
            else
            {
                Task daqTask = sender as Task;
                if (daqTask != null)
                {
                    if (daqTask.Devices != null && daqTask.Devices.Length != 0)
                    {
                        messageLog(this, new MessageEvent("daqMx Task on device " + daqTask.Devices[0] + " finished."));
                    }
                    else
                    {
                        messageLog(this, new MessageEvent("daqMx Task on unknown device finished."));
                    }

                    if (!clientFinishedRun)
                    {
                        lock (taskFinishTimeClicks)
                        {
                            taskFinishTimeClicks.Add(eventTime);
                        }
                    }
                }
                else
                {
                    messageLog(this, new MessageEvent("non-daqMx Task " + sender.ToString() + " finished."));
                }
            }
        }

        private void callback_S7(IAsyncResult ar) //This is what happens when the analog in task completes
        {   
            try
            {
				//Read the available data from the channels 
                double[,] data_ana_in_S7 = new double[analogS7ReadTask.AIChannels.Count, sequence.nSamples(1 / (double)(AtticusServer.server.serverSettings.AIFrequency))];
                data_ana_in_S7 = reader_analog_S7.EndReadMultiSample(ar);
                DataTable dataTable = new DataTable();
                //Creates a new datatable to store data
				DataTableHelper.InitializeDataTable(dataTable,sequence,AtticusServer.server.serverSettings,analog_in_names);
                analogS7ReadTask.Dispose();
                //Only if the sequence needs to save analog in, the data is exported to CSV
                if (sequence.AISaved)
                {
                    DataTableHelper.dataToDataTable(data_ana_in_S7, dataTable,AtticusServer.server.serverSettings.AIFrequency);

                    string day_folder = NamingFunctions.get_fileDirectory(settings);
                    if (!System.IO.Directory.Exists(day_folder + @"\Analog in"))
                        System.IO.Directory.CreateDirectory(day_folder + @"\Analog in");


                    string shot_name = NamingFunctions.get_fileStamp(sequence,settings,runTime);


                    System.IO.StreamWriter a = System.IO.File.CreateText(day_folder + @"\Analog in\" + shot_name + ".csv");
                    DataTableHelper.ProduceCSV(dataTable, a, true);
                    a.Close();
                }
            }
            catch { }

        }

        public Dictionary<string, long> expectedNumberOfSamplesGenerated;

        public bool clientFinishedRun;

        public override bool runSuccess()
        {
            long eventTime = DateTime.Now.Ticks;

            lock (remoteLockObj)
            {
                clientFinishedRun = true;
                messageLog(this, new MessageEvent("Client thinks the run is finished. Buffer statuses:"));
                if (daqMxTasks.Count == 0)
                    messageLog(this, new MessageEvent("No buffers to report"));

                bool samplesGeneratedMismatchDetected = false;

                foreach (string str in daqMxTasks.Keys)
                {
                    Task task = daqMxTasks[str];
                    try
                    {
                        long samplesGenerated = task.Stream.TotalSamplesGeneratedPerChannel;
                        messageLog(this, new MessageEvent(str + " " + samplesGenerated + "/" + task.Stream.Buffer.OutputBufferSize));
                        if (expectedNumberOfSamplesGenerated != null)
                        {
                            if (expectedNumberOfSamplesGenerated.ContainsKey(str))
                            {
                                if (myServerSettings.myDevicesSettings.ContainsKey(str))
                                {
                                    if (myServerSettings.myDevicesSettings[str].MySampleClockSource == DeviceSettings.SampleClockSource.External)
                                    {
                                        long samplesExpected = expectedNumberOfSamplesGenerated[str];
                                        if (samplesGenerated != samplesExpected)
                                        {
                                            // The number of samples is not equal to the expected number. Should we announce this as an error?
                                            // That depends on the error check sensitivity.
                                            
                                            bool announceError=false;
                                            
                                            if (myServerSettings.myDevicesSettings[str].SamplesGeneratedCheckSensitivity == DeviceSettings.DeviceErrorCheckSamplesGeneratedSensitivity.Strict)
                                            {
                                                announceError = true;
                                            }
                                            if (myServerSettings.myDevicesSettings[str].SamplesGeneratedCheckSensitivity == DeviceSettings.DeviceErrorCheckSamplesGeneratedSensitivity.Within4)
                                            {
                                                if (Math.Abs(samplesExpected - samplesGenerated) >= 4)
                                                    announceError = true;
                                            }
                                            if (myServerSettings.myDevicesSettings[str].SamplesGeneratedCheckSensitivity == DeviceSettings.DeviceErrorCheckSamplesGeneratedSensitivity.Disabled)
                                            {
                                                announceError = false;
                                            }


                                            if (announceError)
                                            {
                                                messageLog(this, new MessageEvent("*** Expected generation of " + samplesExpected + " samples for this task."));
                                                displayError();
                                                samplesGeneratedMismatchDetected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        messageLog(this, new MessageEvent("Unable to give buffer status of task " + str + ": " + e.Message));
                    }
                }

                bool earlyFinishDetected = false;
                if (taskFinishTimeClicks != null)
                {
                    lock (taskFinishTimeClicks)
                    {
                        foreach (long time in taskFinishTimeClicks)
                        {
                            if ((eventTime - time) > 10000000)
                            {
                                earlyFinishDetected = true;
                            }
                        }
                    }
                }

                if (earlyFinishDetected)
                {
                    messageLog(this, new MessageEvent("***At least 1 daqMx task finished at least 1 second before the client notified the server that it thought the run was finished. This may indicate corruption of the timing signal driving this daqMx task. Reporting to client as error.***"));
                    displayError();
                    taskErrorsDetected = true;
                }

                if (samplesGeneratedMismatchDetected)
                {
                    messageLog(this, new MessageEvent("***At least 1 daqMx task generated a different number of samples from the expected number. Reporting to client as an error."));
                    displayError();
                    taskErrorsDetected = true;
                }

                if (myServerSettings.UseFpgaMistriggerDetection)
                {
                    foreach (FpgaTimebaseTask ft in fpgaTasks.Values)
                    {
                        int ans = ft.getMistriggerStatus();
                        if (ans != 0)
                        {
                            int errorSamp = ans - 1;
                            taskErrorsDetected = true;
                            messageLog(this, new MessageEvent("FPGA Mistrigger detection detected a mistrigger at (or after) sample number " + errorSamp));
                            displayError();
                            if (serverSettings.DeviceToSyncSoftwareTimedTasksTo != null && serverSettings.DeviceToSyncSoftwareTimedTasksTo != "")
                            {
                                int masterFreq = myServerSettings.myDevicesSettings[serverSettings.DeviceToSyncSoftwareTimedTasksTo].SampleClockRate;
                                TimestepTimebaseSegmentCollection segments = sequence.generateVariableTimebaseSegments(SequenceData.VariableTimebaseTypes.AnalogGroupControlledVariableFrequencyClock, (1.0 / ((double)masterFreq)));
                                int masterSamp = sequence.getMasterSampleFromDerivedSample(errorSamp, segments);
                                double seqTime = ((double)masterSamp) / ((double)masterFreq);
                                TimeStep step = sequence.getTimeStepAtTime(seqTime);
                                messageLog(this, new MessageEvent("This mistrigger was at master sample " + masterSamp + ", sequence time " + seqTime + ", Timestep [" + step.StepName + "] (based on timing info from " + myServerSettings.DeviceToSyncSoftwareTimedTasksTo + ")"));

                                double remTime = seqTime - sequence.getTimeAtTimestep(step);
                                messageLog(this, new MessageEvent("This mistrigger occured " + remTime + " s after the start of the stated timestep."));
                            }
                        }
                    }
                }

                return (!taskErrorsDetected);
            }
        }

        private Dictionary<HardwareChannel, NationalInstruments.VisaNS.SerialSession> serialSessions;

        private NationalInstruments.VisaNS.SerialSession getSerialSession(HardwareChannel hc)
        {

            if (serialSessions == null)
                serialSessions = new Dictionary<HardwareChannel, NationalInstruments.VisaNS.SerialSession>();

            if (!serialSessions.ContainsKey(hc))
            {
                NationalInstruments.VisaNS.SerialSession device = (NationalInstruments.VisaNS.SerialSession)NationalInstruments.VisaNS.ResourceManager.GetLocalManager().Open(hc.ChannelName, NationalInstruments.VisaNS.AccessModes.LoadConfig, 100);
                serialSessions.Add(hc, device);
            }

            return serialSessions[hc];


            // figure out if this device needs its settings modified...

            //NationalInstruments.VisaNS.ResourceManager.GetLocalManager().Open(

            /*            bool customized = false;
                        foreach (SerialPortSettings sps in serverSettings.myDevicesSettings["Serial"].SerialSettings)
                        {

                            if (sps.PortName == hc.ChannelName)
                            {
                                customized = true;
                                messageLog(this, new MessageEvent("Applying custom serial settings to " + hc.ChannelName));
                                device.BaudRate = sps.BaudRate;
                                device.DataBits = sps.DataBits;
                                device.Parity = sps.parity;
                                device.StopBits = sps.StopBits;
                                device.FlowControl = sps.FlowControl;
                            }
                        }
                        if (!customized)
                        {
                            messageLog(this, new MessageEvent("Using default serial settings for " + hc.ChannelName));
                        }*/
        }

        private void clearOpenSerialSessions()
        {
            if (serialSessions != null)
            {
                serialSessions.Clear();
            }
        }


        /// <summary>
        /// Returns true if usedDigitalChannels or usedAnalogChannels has at least one entry
        /// on the specified device. False otherwise.
        /// </summary>
        /// <param name="dev"></param>
        /// <returns></returns>
        bool deviceHasUsedChannels(string dev)
        {
            foreach (HardwareChannel hc in usedDigitalChannels.Values)
            {
                if (hc.DeviceName.ToUpper() == dev.ToUpper())
                    return true;
            }

            foreach (HardwareChannel hc in usedAnalogChannels.Values)
            {
                if (hc.DeviceName.ToUpper() == dev.ToUpper())
                    return true;
            }

            return false;
        }

        private void generateDaqMxTaskOnDevice(string dev)
        {
            this.generateDaqMxTaskOnDevice((object)dev);
        }


        private void generateDaqMxTaskOnDevice(object devObj)
        {
            string dev = devObj.ToString();



            DeviceSettings deviceSettings = myServerSettings.myDevicesSettings[dev];

            if (deviceSettings.DeviceEnabled)
            {
                if (deviceHasUsedChannels(dev))
                {
                    long expectedGenerated = 0;

                    messageLog(this, new MessageEvent("Generating buffer for " + dev));
                    Task task = DaqMxTaskGenerator.createDaqMxTask(dev,
                        deviceSettings,
                        sequence,
                        settings,
                        usedDigitalChannels,
                        usedAnalogChannels,
                        serverSettings,
                        out expectedGenerated);

                    task.Done += new TaskDoneEventHandler(aTaskFinished);

                    daqMxTasks.Add(dev, task);
                    messageLog(this, new MessageEvent("Buffer for " + dev + " generated. " + task.Stream.Buffer.OutputBufferSize + " samples per channel. On board buffer size " + task.Stream.Buffer.OutputOnBoardBufferSize + " samples per channel."));

                    if (expectedGenerated != 0)
                    {
                        expectedNumberOfSamplesGenerated.Add(dev, expectedGenerated);
                    }
                }
                else
                {
                    messageLog(this, new MessageEvent("Skipped buffer generation for " + dev + ". No used channels."));
                }
            }
            else
            {
                messageLog(this, new MessageEvent("Skipped buffer generation for disabled device " + dev));
            }

        }

        private void makeTerminalConnections()
        {

            if (madeConnections == null)
                madeConnections = new List<TerminalPair>();

            // First, removing all connections which exist but which are no longer on the serverSettings.Connections list.

            // copying madeConnections to a temporary array so that I remove elements from madeconnections while
            // iterating through its elements
            TerminalPair[] tempArray = madeConnections.ToArray();

            foreach (TerminalPair pair in tempArray)
            {
                if (!serverSettings.Connections.Contains(pair))
                {
                    DaqSystem.Local.DisconnectTerminals(pair.SourceTerminal, pair.DestinationTerminal);
                    madeConnections.Remove(pair);
                }
            }

            // Now adding connections.

            foreach (TerminalPair pair in serverSettings.Connections)
            {
                if (!madeConnections.Contains(pair))
                {
                    DaqSystem.Local.ConnectTerminals(pair.SourceTerminal, pair.DestinationTerminal);
                    madeConnections.Add(pair);
                }
            }
        }


        private void unmakeTerminalConnections()
        {
            if (madeConnections == null)
                return;
            foreach (TerminalPair pair in madeConnections)
            {
                DaqSystem.Local.DisconnectTerminals(pair.SourceTerminal, pair.DestinationTerminal);
            }
            madeConnections.Clear();
        }



        public override bool outputGPIBGroup(GPIBGroup gpibGroup, SettingsData settings)
        {
            lock (remoteLockObj)
            {
                try
                {
                    messageLog(this, new MessageEvent("Received an output gpib group request."));

                    if (gpibGroup == null)
                    {
                        messageLog(this, new MessageEvent("Received a null object, unable to comply."));
                        displayError();
                        return false;
                    }

                    if (!stopAndCleanupTasks())
                        return false;

                    if (!setSettings(settings))
                        return false;

                    foreach (int channelID in usedGpibChannels.Keys)
                    {
                        if (gpibGroup.channelEnabled(channelID))
                        {
                            HardwareChannel hc = usedGpibChannels[channelID];
                            GPIBGroupChannelData channelData = gpibGroup.ChannelDatas[channelID];
                            if (channelData.DataType == GPIBGroupChannelData.GpibChannelDataType.raw_string)
                            {
                                NationalInstruments.NI4882.Device gpibDevice = new NationalInstruments.NI4882.Device(hc.gpibBoardNumber(), hc.GpibAddress);
                                gpibDevice.Write(
                                    GpibTask.AddNewlineCharacters(channelData.RawString));
                                messageLog(this, new MessageEvent("Wrote GPIB data : " + channelData.RawString));
                            }
                            else if (channelData.DataType == GPIBGroupChannelData.GpibChannelDataType.string_param_string)
                            {
                                NationalInstruments.NI4882.Device gpibDevice = new NationalInstruments.NI4882.Device(hc.gpibBoardNumber(), hc.GpibAddress);
                                if (channelData.StringParameterStrings != null)
                                {
                                    foreach (StringParameterString sps in channelData.StringParameterStrings)
                                    {
                                        gpibDevice.Write(
                                            GpibTask.AddNewlineCharacters(sps.ToString()));
                                        messageLog(this, new MessageEvent("Wrote GPIB data : " + sps.ToString()));
                                    }
                                }
                            }
                            else
                            {
                                messageLog(this, new MessageEvent("Skipping channel " + channelID + ", unsupported data type for an Output Now request: " + channelData.DataType.ToString()));
                                displayError();
                            }
                        }
                    }
                    return true;
                }
                catch (Exception e)
                {
                    messageLog(this, new MessageEvent("Caught exception when attempting to output gpib group: " + e.Message + e.StackTrace));
                    displayError();
                    return false;
                }
            }
        }

        public override bool outputRS232Group(RS232Group rs232Group, SettingsData settings)
        {
            lock (remoteLockObj)
            {

                try
                {
                    messageLog(this, new MessageEvent("Received an output rs232 group request."));


                    if (rs232Group == null)
                    {
                        messageLog(this, new MessageEvent("Received a null output object. Unable to comply."));
                        displayError();
                        return false;
                    }

                    if (!stopAndCleanupTasks())
                        return false;

                    if (!setSettings(settings))
                        return false;

                    foreach (int channelID in usedRS232Channels.Keys)
                    {
                        if (rs232Group.channelEnabled(channelID))
                        {
                            HardwareChannel hc = usedRS232Channels[channelID];
                            RS232GroupChannelData channelData = rs232Group.ChannelDatas[channelID];
                            if (channelData.DataType == RS232GroupChannelData.RS232DataType.Raw)
                            {
                                NationalInstruments.VisaNS.SerialSession ss = getSerialSession(hc);
                                ss.Write(RS232Task.AddNewlineCharacters(channelData.RawString));
                                messageLog(this, new MessageEvent("Wrote rs232 command " + channelData.RawString));
                            }
                            else if (channelData.DataType == RS232GroupChannelData.RS232DataType.Parameter)
                            {
                                if (channelData.StringParameterStrings != null)
                                {
                                    foreach (StringParameterString sps in channelData.StringParameterStrings)
                                    {
                                        NationalInstruments.VisaNS.SerialSession ss = getSerialSession(hc);
                                        string rawCommand = sps.ToString();
                                        string command = RS232Task.AddNewlineCharacters(rawCommand);
                                        ss.Write(command);
                                        messageLog(this, new MessageEvent("Wrote rs232 command " + rawCommand));
                                    }
                                }
                            }
                            else
                            {
                                messageLog(this, new MessageEvent("Skipping output on channel " + channelID + ", output now not enabled for data of type " + channelData.DataType.ToString()));
                            }
                        }
                    }



                    return true;
                }
                catch (Exception e)
                {
                    messageLog(this, new MessageEvent("Caught exception when attempting output of single rs232 group: " + e.Message + e.StackTrace));
                    displayError();
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// This object gets locked whenever softwareTriggeringTask gets accessed or modified. This hopefully
        /// makes the software triggering task stuff watertight threadsafe.
        /// </summary>
        /// 
        private object softwareTriggeringTaskLock = new object();

        /// <summary>
        /// Start hardware triggered tasks, and software triggerred tasks which run off an external sample clock. This function also
        /// sets up the software-timed task triggering mechanism, if one is in use.
        /// </summary>
        /// <returns></returns>
        public override bool armTasks()
        {
            lock (remoteLockObj)
            {
                try
                {
                    messageLog(this, new MessageEvent("Arming tasks"));
                    int armedTasks = 0;

                    lock (softwareTriggeringTaskLock)
                    {
                        softwareTriggeringTask = null;
                    }



                    softwareTimedTasksTriggered = false;
                    softwareTimedTriggerCount = 0;

                    foreach (string dev in myServerSettings.myDevicesSettings.Keys)
                    {
                        if (daqMxTasks.ContainsKey(dev))
                        {
                            DeviceSettings ds = myServerSettings.myDevicesSettings[dev];

                            // Start the task if it is hardware triggered, or if it is software triggered but with an external sample clock.
                            if ((ds.StartTriggerType == DeviceSettings.TriggerType.TriggerIn) ||
                                 ((ds.StartTriggerType == DeviceSettings.TriggerType.SoftwareTrigger) && (ds.MySampleClockSource == DeviceSettings.SampleClockSource.External)))
                            {
                                Task task = daqMxTasks[dev];

                                if (dev == serverSettings.DeviceToSyncSoftwareTimedTasksTo)
                                {
                                    if (serverSettings.SoftwareTaskTriggerMethod == ServerSettings.SoftwareTaskTriggerType.SampleClockEvent)
                                    {
                                        messageLog(this, new MessageEvent("***** You are using SampleClockEvent as your SoftwareTaskTriggerMethod (in your ServerSettings). This is not recommended and no longer supported. Use PollBufferPosition instead. *****"));
                                        displayError();
                                        /*
                                        lock (softwareTriggeringTaskLock)
                                        {
                                            softwareTriggeringTask = task;
                                        }
                                        task.SampleClock += triggerSoftwareTimedTasks;
                                    
                                         */
                                        return false;
                                    }
                                }

                                task.Start();

                                if (dev == serverSettings.DeviceToSyncSoftwareTimedTasksTo)
                                {
                                    if (serverSettings.SoftwareTaskTriggerMethod == ServerSettings.SoftwareTaskTriggerType.PollBufferPosition)
                                    {
                                        if (softwareTaskTriggerPollingThread != null)
                                        {
                                            if (softwareTaskTriggerPollingThread.ThreadState == ThreadState.Running)
                                            {
                                                softwareTaskTriggerPollingThread.Abort();
                                                if (softwareTaskTriggerPollingThread.ThreadState != ThreadState.Aborted)
                                                {
                                                    throw new Exception("Unable to abort an already-running software-task triggering polling thread.");
                                                }

                                            }
                                        }
                                        lock (softwareTriggeringTaskLock)
                                        {
                                            softwareTriggeringTask = task;
                                        }
                                        softwareTaskTriggerPollingFunctionInitialPosition = task.Stream.TotalSamplesGeneratedPerChannel;
                                        softwareTaskTriggerPollingThread = new Thread(new ThreadStart(softwareTaskTriggerPollingFunction));
                                        softwareTaskTriggerPollingThread.Start();

                                    }
                                }

                                armedTasks++;

                            }
                        }
                    }

                    messageLog(this, new MessageEvent(armedTasks.ToString() + " tasks armed."));

                    if (variableTimebaseClockTask != null)
                    {
                        variableTimebaseClockTask.Control(TaskAction.Commit);
                    }

					//if needed, the analog input task is launched here
                    if (analogInCardDetected)
                        reader_analog_S7.BeginReadMultiSample(sequence.nSamples(1 / (double)(AtticusServer.server.serverSettings.AIFrequency)), new AsyncCallback(callback_S7), null);

                    return true;
                }
                catch (Exception e)
                {
                    messageLog(this, new MessageEvent("Unable to arm tasks due to exception: " + e.Message + e.StackTrace));
                    displayError();
                    return false;
                }
            }
        }

        private Object softTrigLock = new object();
        private Task softwareTriggeringTask;
        private bool softwareTimedTasksTriggered;
        private int softwareTimedTriggerCount;
        private Thread softwareTaskTriggerPollingThread;
        private long softwareTaskTriggerPollingFunctionInitialPosition;

        /// <summary>
        /// This function continuously polls the output position of a task. When that output position changes from its initial values,,
        /// it triggers the software timed tasks.
        /// 
        /// This is a mechanism for better syncronization of software timed tasks with hardware timed ones.
        /// </summary>
        private void softwareTaskTriggerPollingFunction()
        {
            try
            {
                bool entered = Monitor.TryEnter(softTrigLock, 100);
                if (!entered)
                {
                    messageLog(this, new MessageEvent("******* Unable to run software task triggering polling thread, as another such thread is already running. Software timed tasks may not be triggered. *******"));
                    displayError();
                    return;
                }
                try
                {
                    messageLog(this, new MessageEvent("Started a polling thread to trigger software timed tasks. Initial buffer write position being monitored: " + softwareTaskTriggerPollingFunctionInitialPosition));
                    lock (softwareTriggeringTaskLock)
                    {
                        while (true)
                        {
                            if (softwareTriggeringTask.Stream.TotalSamplesGeneratedPerChannel != softwareTaskTriggerPollingFunctionInitialPosition)
                            {
                                foreach (GpibTask gp in gpibTasks.Values)
                                {
                                    gp.Start();
                                }
                                foreach (RS232Task rs in rs232Tasks.Values)
                                {
                                    rs.Start();
                                }
                                foreach (RfsgTask rf in rfsgTasks.Values)
                                {
                                    rf.Start();
                                }

                                messageLog(this, new MessageEvent("Software timed tasks triggered."));
                                softwareTaskTriggerPollingThread = null;
                                Monitor.Exit(softTrigLock);
                                return;
                            }

                            Thread.Sleep(1);
                        }
                    }
                }
                catch (ThreadAbortException)
                {
                    Monitor.Exit(softTrigLock);
                }
            }
            catch (Exception e)
            {
                messageLog(this, new MessageEvent("Caught exception during software trigger polling thread: " + e.Message + e.StackTrace));
                displayError();
                try
                {
                    Monitor.Exit(softTrigLock);
                }
                catch (Exception)
                {
                    messageLog(this, new MessageEvent("Also, caught exception when attempting to release software polling thread lock. This is probably not important."));
                }
            }

        }

        bool readyReaderLoopRunning = false;
        bool readyReaderLoopAbort = false;

        /// <summary>
        /// This event handler will be called when a task on this server receives a sample clock. 
        /// We only want to consume the first such event. It will be used to start and software timed
        /// operations on this server.
        /// 
        /// NOTE: This method never worked well, and is not no longer in use. Polling the buffer position with a monitoring
        /// thread ended up working much better. See softwareTaskTriggerPollingFunction()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void triggerSoftwareTimedTasks(object sender, SampleClockEventArgs e)
        {
            messageLog(this, new MessageEvent("******** triggerSoftwareTimedTasks(...) was called. This function is no longer supported. How did you get here? ********"));
            displayError();
            /*
            lock (softTrigLock)
            {
                if (!softwareTimedTasksTriggered)
                {
                    lock (softwareTriggeringTaskLock)
                    {
                        // first order of business is to remove this event handler so it doesn't get overwhelmed
                        softwareTriggeringTask.SampleClock -= triggerSoftwareTimedTasks;
                    }

                    softwareTimedTasksTriggered = true;

                    //ok. Now trigger the tasks.
                    foreach (GpibTask gp in gpibTasks.Values)
                    {
                        gp.Start();
                    }
                    foreach (RS232Task rs in rs232Tasks.Values)
                    {
                        rs.Start();
                    }
                    foreach (RfsgTask rf in rfsgTasks.Values)
                    {
                        rf.Start();
                    }
                    messageLog(this, new MessageEvent("Software timed tasks triggered by " + serverSettings.DeviceToSyncSoftwareTimedTasksTo));
                }
                else
                {
                    softwareTimedTriggerCount++;
                    if (softwareTimedTriggerCount % 10 == 0)
                    {
                        messageLog(this, new MessageEvent("Ignored " + softwareTimedTriggerCount + " duplicate software-timed task triggers."));
                    }
                }
            }*/
        }

        public void shutDown()
        {
            messageLog(this, new MessageEvent("Shutting down..."));


            messageLog(this, new MessageEvent("Stopping all tasks."));
            this.stopAndCleanupTasks();

            // Reset devices on exit. This is so that if you close this and open the old word generator,
            // it doesn't complain on startup.
            resetAllDevices();

            messageLog(this, new MessageEvent("Shutdown complete."));
        }


        /// <summary>
        /// Start internally clocked software triggered tasks, software timed tasks (like gpib tasks), and tasks which act as clocks for other tasks.
        /// </summary>
        /// <returns></returns>
        public override bool generateTrigger()
        {
            lock (remoteLockObj)
            {
                try
                {

                    messageLog(this, new MessageEvent("Generating triggers."));

                    Dictionary<string, DeviceSettings> devicesSettings = myServerSettings.myDevicesSettings;
                    List<string> devicesToSoftTrigger = new List<string>();
                    List<Task> tasksToSoftTrigger = new List<Task>();
                    List<Task> tasksToSoftTriggerLast = new List<Task>();
                    List<GpibTask> gpibTasksToTrigger = new List<GpibTask>();
                    List<RS232Task> rs232TasksToTrigger = new List<RS232Task>();
                    List<RfsgTask> rfsgTasksToTrigger = new List<RfsgTask>();

                    // This loop adds the NIDAQ analog and digital tasks that require soft trigger to the appropriate list.
                    // These are software triggered tasks which do NOT use an external sample clock (those that do are started in armTasks)
                    foreach (string dev in devicesSettings.Keys)
                    {
                        if ((devicesSettings[dev].StartTriggerType == DeviceSettings.TriggerType.SoftwareTrigger)
                            && (devicesSettings[dev].MySampleClockSource != DeviceSettings.SampleClockSource.External))
                        {
                            devicesToSoftTrigger.Add(dev);

                            if (daqMxTasks.ContainsKey(dev))
                            {

                                if (daqMxTasks[dev] != null)
                                {
                                    if (devicesSettings[dev].SoftTriggerLast)
                                    {
                                        tasksToSoftTriggerLast.Add(daqMxTasks[dev]);
                                    }
                                    else
                                    {
                                        tasksToSoftTrigger.Add(daqMxTasks[dev]);
                                    }
                                }
                            }
                        }

                    }

                    // add all the gpib tasks to the soft trigger list
                    foreach (GpibTask gpTask in gpibTasks.Values)
                    {
                        gpibTasksToTrigger.Add(gpTask);
                    }

                    foreach (RS232Task task in rs232Tasks.Values)
                    {
                        rs232TasksToTrigger.Add(task);
                    }
                    foreach (RfsgTask task in rfsgTasks.Values)
                    {
                        rfsgTasksToTrigger.Add(task);
                    }


                    // If there is an additional "wait for ready" input, then wait for it.
                    if (serverSettings.ReadyInput != null)
                    {
                        if (serverSettings.ReadyInput != "")
                        {
                            if (sequence.WaitForReady)
                            {
                                messageLog(this, new MessageEvent("Waiting for ready input."));
                                Task readyReaderTask = new Task("ReadyInput");
                                readyReaderTask.DIChannels.CreateChannel(serverSettings.ReadyInput, "", ChannelLineGrouping.OneChannelForEachLine);
                                DigitalSingleChannelReader reader = new DigitalSingleChannelReader(readyReaderTask.Stream);

                                readyReaderLoopAbort = false;
                                readyReaderLoopRunning = true;
                                long startTicks = DateTime.Now.Ticks;
                                while (!reader.ReadSingleSampleSingleLine() && !readyReaderLoopAbort)
                                {
                                    if (serverSettings.ReadyTimeout > 0)
                                    {
                                        long durationTicks = DateTime.Now.Ticks - startTicks;
                                        if (durationTicks / 10000 > serverSettings.ReadyTimeout)
                                        {
                                            messageLog(this, new MessageEvent("Timeout waiting for ready input, more than " + serverSettings.ReadyTimeout + "ms elapsed."));
                                            if (serverSettings.ReadyTimeoutRunAnyway)
                                            {
                                                messageLog(this, new MessageEvent("Running sequence anyway..."));
                                                break;
                                            }
                                            else
                                            {
                                                readyReaderTask.Dispose();
                                                messageLog(this, new MessageEvent("Aborting run."));
                                                readyReaderLoopRunning = false;
                                                return false;
                                            }
                                        }
                                    }
                                }
                                readyReaderLoopRunning = false;

                                if (readyReaderLoopAbort)
                                {
                                    messageLog(this, new MessageEvent("Received an abort request while running the ready input polling loop. Aborting."));
                                    readyReaderTask.Dispose();
                                    return false;
                                }

                                messageLog(this, new MessageEvent("Done waiting for ready input. Running sequence."));
                                readyReaderTask.Dispose();

                            }
                        }
                    }
                    // ok, done waiting for the ready input (or never stopped to wait)

                    // Hardware trigger outputs. This is not a recommended way to synchronize things.
                    if (serverSettings.TriggerOutputChannel != "" && serverSettings.TriggerOutputChannel != null)
                    {
                        messageLog(this, new MessageEvent("******* This server is configured to use a trigger output channel. This is not recommended. Instead, either use a variable timebase sample clock, or derive your start trigger from the StartTrigger channel of a software triggered task. *********"));
                        displayError();

                        string triggerChannel = serverSettings.TriggerOutputChannel;
                        // Create trigger tasks
                        /*List<Task> triggerTasks = new List<Task>();
                        List<DigitalSingleChannelWriter> triggerWriters = new List<DigitalSingleChannelWriter>();
                        foreach (string triggerChannel in serverSettings.TriggerOutputChannels)
                        {*/
                        Task triggerTask = new Task();
                        triggerTask.DOChannels.CreateChannel(triggerChannel, "", ChannelLineGrouping.OneChannelForEachLine);
                        DigitalSingleChannelWriter writer = new DigitalSingleChannelWriter(triggerTask.Stream);
                        writer.WriteSingleSampleSingleLine(true, false);
                        // }

                        // wait for the trigger lines to go low.
                        Thread.Sleep(1);


                        // now pounce!

                        //foreach (DigitalSingleChannelWriter writer in triggerWriters)
                        writer.WriteSingleSampleSingleLine(true, true);
                    }



                    // Software triggering for daqMx tasks.
                    foreach (Task task in tasksToSoftTrigger)
                    {
                        task.Start();
                    }



                    if (!myServerSettings.TriggerSoftwareTasksAfterTimebaseTask)
                    {
                        // Trigger the software timed operations, but only if these operations are not going to be
                        // triggered through a triggering task. (see the armTasks function for more info).
                        if (softwareTriggeringTask == null)
                        {
                            int softTrigs = 0;
                            // software triggering for gpib tasks
                            foreach (GpibTask gpTask in gpibTasksToTrigger)
                            {
                                gpTask.Start();
                                softTrigs++;
                            }

                            // softward triggering for rs232 tasks
                            foreach (RS232Task task in rs232TasksToTrigger)
                            {
                                task.Start();
                                softTrigs++;
                            }

                            foreach (RfsgTask rftask in rfsgTasksToTrigger)
                            {
                                rftask.Start();
                                softTrigs++;
                            }
                            messageLog(this, new MessageEvent("Triggered " + softTrigs + " software-timed task(s) (without sync to a hardware timed task)."));
                        }
                    }


                    foreach (Task task in tasksToSoftTriggerLast)
                    {
                        task.Start();
                    }


                    // finally, if there is a variable timebase output task, we start it.

                    if (variableTimebaseClockTask != null)
                    {
                        long before = DateTime.Now.Ticks;
                        variableTimebaseClockTask.Start();
                        long after = DateTime.Now.Ticks;
                        long elapsed = after - before;

                        int ms = (int)(elapsed / 10000);
                        messageLog(this, new MessageEvent("Triggered variable timebase clock task. Time elapsed waiting for task to start: " + ms + " ms."));

                        if (!myServerSettings.SilenceSoftwareTimebaseDelayError)
                        {
                            // Detect possible long delay between timebase and software task trigger.
                            if (ms > 50)
                            {
                                if (gpibTasks.Count + rs232Tasks.Count + rfsgTasks.Count > 0)
                                {
                                    if (softwareTriggeringTask == null)
                                    {
                                        if (!myServerSettings.TriggerSoftwareTasksAfterTimebaseTask)
                                        {
                                            messageLog(this, new MessageEvent("**** NOTE: There is a delay of ~" + ms + " ms between the start of your software-timed tasks and the start of your variable timebase. To reduce this, either use the DeviceToSyncSoftwareTasksTo sync method, or set TriggerSoftwareTasksAfterTimebaseTask to true. To silence this error, set SilenceSoftwareTimebaseDelayError to true.***"));
                                            displayError();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (myServerSettings.TriggerSoftwareTasksAfterTimebaseTask)
                    {
                        // Trigger the software timed operations, but only if these operations are not going to be
                        // triggered through a triggering task. (see the armTasks function for more info).
                        if (softwareTriggeringTask == null)
                        {
                            int softTrigs = 0;
                            // software triggering for gpib tasks
                            foreach (GpibTask gpTask in gpibTasksToTrigger)
                            {
                                gpTask.Start();
                                softTrigs++;
                            }

                            // softward triggering for rs232 tasks
                            foreach (RS232Task task in rs232TasksToTrigger)
                            {
                                task.Start();
                                softTrigs++;
                            }

                            foreach (RfsgTask rftask in rfsgTasksToTrigger)
                            {
                                rftask.Start();
                                softTrigs++;
                            }
                              messageLog(this, new MessageEvent("Triggered " + softTrigs + " software-timed task(s) (without sync to a hardware timed task)."));                        
                        }

                    }

                    // TO DO. Insert code that waits for external triggers to occur, before returning. This
                    // Will allow client UI to stay synced with external triggers, if such things are being provided.

                    if (fpgaTasks != null)
                    {
                        foreach (FpgaTimebaseTask ft in fpgaTasks.Values)
                        {
                            ft.Start();
                        }
                    }

                    messageLog(this, new MessageEvent("Triggers generated. Sequence running."));

                    return true;
                }
                catch (Exception e)
                {
                    messageLog(this, new MessageEvent("Unable to generate triggers due to exception. " + e.Message + e.StackTrace));
                    displayError();
                    return false;
                }
            }

        }


        public override bool setSequence(SequenceData sequence)
        {
            lock (remoteLockObj)
            {
                messageLog(this, new MessageEvent("Received sequence."));
                this.sequence = sequence;
                return true;
            }
        }

        public override bool setSettings(SettingsData settings)
        {
            lock (remoteLockObj)
            {
                try
                {
                    messageLog(this, new MessageEvent("Received settings."));
                    this.settings = settings;

                    findMyChannels();
                    return true;
                }
                catch (Exception e)
                {
                    messageLog(this, new MessageEvent("Caught exception while attempting to verify settings. " + e.Message + e.StackTrace));
                    displayError();
                    return false;
                }
            }
        }

        public override void stop()
        {
            if (readyReaderLoopRunning)
            {
                readyReaderLoopAbort = true;
            }

            lock (remoteLockObj)
            {
                try
                {
                    messageLog(this, new MessageEvent("Received a STOP signal. Stopping any currently executing runs."));
                    // Analog in task needs to be stopped
                    if (analogInCardDetected)
                    {
                        #region Stop Analog In
                        try
                        {

                            analogS7ReadTask.Control(TaskAction.Abort);
                            analogS7ReadTask.Stop();
                            analogS7ReadTask.Dispose();
                        }
                        catch
                        {
                        }


                        #endregion Stop Analog In
                    }
                    stopAndCleanupTasks();
                }
                catch (Exception e)
                {
                    messageLog(this, new MessageEvent("Caught exception while attempting to STOP: " + e.Message + e.StackTrace));
                    displayError();
                }
            }
        }

        #endregion

        private bool stopAndCleanupTasks()
        {
            try
            {
                bool ans = true;

                if (fpgaTasks != null)
                {
                    foreach (FpgaTimebaseTask ftask in fpgaTasks.Values)
                    {
                        ftask.Stop();
                    }
                }

                if (variableTimebaseClockTask != null)
                {
                    try
                    {
                        variableTimebaseClockTask.Stop();
                    }
                    catch (Exception e)
                    {
                        messageLog(this, new MessageEvent("Caught exception when trying to stop the variable timebase output task. This may indicate that the variable timebase clock suffered a buffer underrun in the previous run. Exception follows: " + e.Message + e.StackTrace));
                        displayError();
                        ans = false;
                    }

                    try
                    {
                        variableTimebaseClockTask.Dispose();
                    }
                    catch (Exception e)
                    {
                        messageLog(this, new MessageEvent("Caught exception when trying to dispose of the variable timebase output task. This may indicate that the variable timebase clock suffered a buffer underrun in the previous run. Exception follows: " + e.Message + e.StackTrace));
                        displayError();
                        ans = false;
                    }

                    variableTimebaseClockTask = null;
                }

                if (daqMxTasks == null)
                {
                    daqMxTasks = new Dictionary<string, Task>();
                }
                else
                {
                    List<string> stopMe = new List<string>(daqMxTasks.Keys);

                    foreach (string dev in stopMe)
                    {
                        try
                        {
                            daqMxTasks[dev].Stop();
                        }
                        catch (Exception e)
                        {
                            messageLog(this, new MessageEvent("Caught exception when trying to stop task on device " + dev + ". This may indicate that the previous run suffered from a buffer underrun. Exception follows: " + e.Message + e.StackTrace));
                            displayError();
                            ans = false;
                        }

                        try
                        {
                            daqMxTasks[dev].Dispose();
                        }
                        catch (Exception e)
                        {
                            messageLog(this, new MessageEvent("Caught exception when trying to dispose of task on device " + dev + ". This may indicated that the previous run suffered from a buffer underrun. Exception follows: " + e.Message + e.StackTrace));
                            displayError();
                            ans = false;
                        }

                        daqMxTasks.Remove(dev);
                    }
                    GC.Collect();
                    GC.Collect();
                }

                if (gpibTasks == null)
                {
                    gpibTasks = new Dictionary<HardwareChannel, GpibTask>();
                }
                else
                {
                    // stop gpib tasks.

                    foreach (GpibTask gp in gpibTasks.Values)
                        gp.stop();

                    gpibTasks.Clear();
                }

                if (rs232Tasks == null)
                {
                    rs232Tasks = new Dictionary<HardwareChannel, RS232Task>();
                }
                else
                {
                    foreach (RS232Task rt in rs232Tasks.Values)
                    {
                        rt.stop();
                    }
                    rs232Tasks.Clear();
                }

                if (rfsgTasks == null)
                {
                    rfsgTasks = new Dictionary<HardwareChannel, RfsgTask>();
                }
                else
                {
                    foreach (RfsgTask rf in rfsgTasks.Values)
                    {
                        rf.stop();
                    }
                    rfsgTasks.Clear();
                }

                //   resetAllDevices();

                System.GC.Collect();
                System.GC.Collect();

                taskErrorsDetected = false;

                return ans;
            }
            catch (Exception e)
            {
                messageLog(this, new MessageEvent("Caught exception when attempting to stop tasks. " + e.Message + e.StackTrace));
                displayError();
                return false;
            }

        }

        public void resetAllDevices()
        {
            messageLog(this, new MessageEvent("Resetting all devices..."));
            // Reset all of the devices. This removes all of the clocking and triggering connections, so that
            // we don't have to keep track of them and manually add and remove them all of the time.
            foreach (string dev in DaqSystem.Local.Devices)
            {
                messageLog(this, new MessageEvent("Resetting " + dev));

                Device device = DaqSystem.Local.LoadDevice(dev);
                device.Reset();
                messageLog(this, new MessageEvent("Reset of " + dev + " finished."));
            }
            if (this.madeConnections != null)
                this.madeConnections.Clear();

            clearOpenSerialSessions();
        }

        /// <summary>
        /// Determines the logical channel IDs and Hardware Channel objects for all channels in settings data
        /// which reside on this server.
        /// </summary>
        private void findMyChannels()
        {
            this.usedAnalogChannels = new Dictionary<int, HardwareChannel>();
            this.usedDigitalChannels = new Dictionary<int, HardwareChannel>();
            this.usedGpibChannels = new Dictionary<int, HardwareChannel>();
            this.usedRS232Channels = new Dictionary<int, HardwareChannel>();

            LogicalChannel errorChan;
            //analog
            errorChan = findChannels(settings.logicalChannelManager.ChannelCollections[HardwareChannel.HardwareConstants.ChannelTypes.analog],
                usedAnalogChannels);

            if (errorChan != null)
            {
                throw new Exception("Invalid settings data. Analog logical channel named " + errorChan.Name + " is bound to a hardware channel on this server which is disabled or does not exist.");
            }

            //digital
            errorChan = findChannels(settings.logicalChannelManager.ChannelCollections[HardwareChannel.HardwareConstants.ChannelTypes.digital],
                usedDigitalChannels);

            if (errorChan != null)
            {
                throw new Exception("Invalid settings data. Digital logical channel named " + errorChan.Name + " is bound to a hardware channel on this server which is disabled or does not exist.");
            }

            // gpib
            errorChan = findChannels(settings.logicalChannelManager.ChannelCollections[HardwareChannel.HardwareConstants.ChannelTypes.gpib],
                usedGpibChannels);

            if (errorChan != null)
            {
                throw new Exception("Invalid settings data. GPIB logical channel named " + errorChan.Name + " is bound to a hardware channel on this server which is disabled or does not exist.");
            }

            // rs232
            errorChan = findChannels(settings.logicalChannelManager.ChannelCollections[HardwareChannel.HardwareConstants.ChannelTypes.rs232],
                usedRS232Channels);

            if (errorChan != null)
            {
                throw new Exception("Invalid settings data. RS232 logical channel named " + errorChan.Name + " is bound to a hardware channel on this server which is disabled or does not exist.");
            }

            // populate the list of used daqmx devices
            this.usedDaqMxDevices = new List<string>();
            foreach (int analogID in usedAnalogChannels.Keys)
            {
                HardwareChannel hc = settings.logicalChannelManager.ChannelCollections[HardwareChannel.HardwareConstants.ChannelTypes.analog].Channels[analogID].hardwareChannel;

                if (!usedDaqMxDevices.Contains(hc.DeviceName))
                    usedDaqMxDevices.Add(hc.DeviceName);
            }
            foreach (int digitalID in usedDigitalChannels.Keys)
            {
                HardwareChannel hc = settings.logicalChannelManager.ChannelCollections[HardwareChannel.HardwareConstants.ChannelTypes.digital].Channels[digitalID].hardwareChannel;
                if (!usedDaqMxDevices.Contains(hc.DeviceName))
                    usedDaqMxDevices.Add(hc.DeviceName);
            }
        }

        /// <summary>
        /// Helper function for findMyChannels. If a channel is found that is not contained in the current list of exportable channels,
        /// then that channel is returned as an error.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="channelMap"></param>
        private LogicalChannel findChannels(ChannelCollection collection, Dictionary<int, HardwareChannel> channelMap)
        {
            foreach (int id in collection.Channels.Keys)
            {
                LogicalChannel logical = collection.Channels[id];
                if (logical.hardwareChannel != null)
                {
                    if (!logical.hardwareChannel.isUnAssigned)
                    {
                        if (logical.hardwareChannel.ServerName == this.myServerSettings.ServerName)
                        {
                            if (this.MyHardwareChannels.Contains(logical.hardwareChannel))
                            {
                                channelMap.Add(id, logical.hardwareChannel);
                            }
                            else
                            {
                                return logical;
                            }
                        }
                    }
                }
            }

            return null;
        }


        #region Threaded methods

        /// <summary>
        /// This method start .NET remoting sharing of the server. Intended for use in a non UI thread.
        /// </summary>
        private void startMarshalProc()
        {
            try
            {
                lock (marshalLock)
                {

                    communicatorStatus = ServerStructures.ServerCommunicatorStatus.Connecting;
                    updateGUI(this, null);
                    tcpChannel = new TcpChannel(5678);
                    ChannelServices.RegisterChannel(tcpChannel, false);
                    objRef = RemotingServices.Marshal(this, "serverCommunicator");
                    communicatorStatus = ServerStructures.ServerCommunicatorStatus.Connected;
                }
                messageLog(this, new MessageEvent("serverCommunicator Marshalled."));
                updateGUI(this, null);
            }
            catch (Exception e)
            {
                messageLog(this, new MessageEvent("Unable to start Marshal due to exception: " + e.Message + e.StackTrace));
                displayError();
                communicatorStatus = ServerStructures.ServerCommunicatorStatus.Disconnected;
                updateGUI(this, null);
            }
        }

        /// <summary>
        /// This method stop .NET remoting of the server. Intended for use in a non UI thread.
        /// </summary>
        private void stopMarshalProc()
        {

            messageLog(this, new MessageEvent("Server disconnected is not currently implemented. You can achieve this functionality by restarting the server."));
            return;


            /*
            try
            {
                lock (marshalLock)
                {
                    communicatorStatus = ServerStructures.ServerCommunicatorStatus.Connecting;
                    updateGUI(this, null);
                    RemotingServices.Unmarshal(objRef);
                    ChannelServices.UnregisterChannel(tcpChannel);
                    communicatorStatus = ServerStructures.ServerCommunicatorStatus.Disconnected;
                }
                messageLog(this, new MessageEvent("serverCommunicator Unmarshalled."));
                updateGUI(this, null);
            }
            catch (Exception e)
            {
                messageLog(this, new MessageEvent("Unable to stop Marshal due to exception: " + e.Message + e.StackTrace));
                communicatorStatus = ServerStructures.ServerCommunicatorStatus.Disconnected;
                updateGUI(this, null);
            }*/

        }

        #endregion


        private delegate void gpibWriteDelegate(string s);

        /// <summary>
        /// This method is to be called on the server side, and is not open to use via remoting. Its purpose
        /// is to update the servers internal list of hardware channels by querying the National Instruments drivers.
        /// </summary>
        public void refreshHardwareLists()
        {

            System.Console.WriteLine("Running refreshHardwareLists()...");

            try
            {


                // Set all the devices to disconnected. They will be set back to connected if they are detecter later in this method.
                foreach (DeviceSettings ds in serverSettings.myDevicesSettings.Values)
                    ds.deviceConnected = false;



                myHardwareChannels = new List<HardwareChannel>();

                //List of string identifiers for all devices detected in the process.
                detectedDevices = new List<string>();

                Dictionary<string, string> myDeviceDescriptions = new Dictionary<string, string>();

                // Detect National Instruments analog and digital channels.

                #region detect NI analog and digital  (daqMx)


                System.Console.WriteLine("Accessing DaqSystem devices list...");

                DaqSystem daqSystem = DaqSystem.Local;
                string[] devices = daqSystem.Devices;


                System.Console.WriteLine("...done.");

                System.Console.WriteLine("Found " + devices.Length.ToString() + " devices.");

                bool niDaqDevicesExistThatAreNotNamedDevSomething = false;

                for (int i = 0; i < devices.Length; i++)
                {
                    if (!devices[i].ToUpper().Contains("DEV"))
                    {
                        niDaqDevicesExistThatAreNotNamedDevSomething = true;
                    }

                    System.Console.WriteLine("Querying device " + i + "...");

                    detectedDevices.Add(devices[i]);

                    Device device = daqSystem.LoadDevice(devices[i]);


                    myDeviceDescriptions.Add(devices[i], device.ProductType);
                    string[] analogs = device.AOPhysicalChannels;
                    string[] digitalLines = device.DOLines;

                    if (!serverSettings.myDevicesSettings.ContainsKey(devices[i]))
                    {
                        serverSettings.myDevicesSettings.Add(devices[i], new DeviceSettings(devices[i], myDeviceDescriptions[devices[i]]));
                    }

                    // Special case: 6259 cards use 32-bit wide ports instead of 16 bit wide.
                    if (myDeviceDescriptions[devices[i]].Contains("6259"))
                    {
                        serverSettings.myDevicesSettings[devices[i]].use32BitDigitalPorts = true;
                    }


                    // Add all the analog channels, but only if the device settings say this card is enabled

                    if (serverSettings.myDevicesSettings.ContainsKey(devices[i]) && serverSettings.myDevicesSettings[devices[i]].DeviceEnabled)
                    {

                        if (serverSettings.myDevicesSettings[devices[i]].AnalogChannelsEnabled)
                        {
                            for (int j = 0; j < analogs.Length; j++)
                            {
                                string channelName = justTheChannelName(analogs[j], devices[i]);
                                HardwareChannel hc = new HardwareChannel(this.myServerSettings.ServerName, devices[i], channelName, HardwareChannel.HardwareConstants.ChannelTypes.analog);
                                if (!serverSettings.ExcludedChannels.Contains(hc))
                                {
                                    myHardwareChannels.Add(hc);
                                }
                            }
                        }

                        if (serverSettings.myDevicesSettings[devices[i]].DigitalChannelsEnabled)
                        {
                            for (int j = 0; j < digitalLines.Length; j++)
                            {
                                string channelName = justTheChannelName(digitalLines[j], devices[i]);
                                HardwareChannel hc = new HardwareChannel(this.myServerSettings.ServerName, devices[i], channelName, HardwareChannel.HardwareConstants.ChannelTypes.digital);
                                if (!serverSettings.ExcludedChannels.Contains(hc))
                                {
                                    myHardwareChannels.Add(hc);
                                }
                            }
                        }
                    }

                    System.Console.WriteLine("...done.");

                }

                #endregion

                #region "detect" RFSG cards

                foreach (ServerSettings.rfsgDeviceName rfsgDevName in serverSettings.RfsgDeviceNames)
                {

                    System.Console.WriteLine("Querying RFSG devices...");

                    string devName = rfsgDevName.DeviceName;
                    HardwareChannel hc = new HardwareChannel(serverSettings.ServerName, devName, "rf_out", HardwareChannel.HardwareConstants.ChannelTypes.gpib);
                    hc.gpibMasquerade = true;
                    hc.myGpibMasqueradeType = HardwareChannel.GpibMasqueradeType.RFSG;

                    myHardwareChannels.Add(hc);

                    if (!serverSettings.myDevicesSettings.ContainsKey(devName))
                    {
                        DeviceSettings devSettings = new DeviceSettings(devName, "RFSG driver library signal generator");
                        serverSettings.myDevicesSettings.Add(devName, devSettings);
                    }

                    System.Console.WriteLine("...done.");

                }

                #endregion

                #region detect NI GBIB

                // try a maxumimum of 10 GPIB boards... this is a totally arbitrary number.
                for (int i = 0; i < 10; i++)
                {

                    System.Console.WriteLine("Querying or detecting GPIB Board Number " + i + "...");

                    try
                    {
                        NationalInstruments.NI4882.Board board = new NationalInstruments.NI4882.Board(i);
                        board.SendInterfaceClear();
                        //              board.BecomeActiveController(false);
                        NationalInstruments.NI4882.AddressCollection listeners = board.FindListeners();


                        if (listeners.Count != 0)
                        {
                            foreach (NationalInstruments.NI4882.Address address in listeners)
                            {


                                int wait_delay = 100;

                                try
                                {
                                    NationalInstruments.NI4882.Device dev = new NationalInstruments.NI4882.Device(i, address);
                                    dev.Clear();

                                    // ask the device for its identity
                                    gpibWriteDelegate writeDelegate = new gpibWriteDelegate(dev.Write);
                                    IAsyncResult result = writeDelegate.BeginInvoke("*IDN?\n", null, null);
                                    result.AsyncWaitHandle.WaitOne(wait_delay, true);

                                    if (!result.IsCompleted)
                                    {
                                        messageLog(this, new MessageEvent("GPIB device took longer than " + wait_delay + " ms to respond to id request. Aborting."));
                                        dev.AbortAsynchronousIO();
                                        continue;
                                    }

                                    string deviceDescription = dev.ReadString();

                                    string deviceName = "GPIB" + i + "/" + HardwareChannel.gpibAddressToShortString(address);
                                    detectedDevices.Add(deviceName);

                                    myDeviceDescriptions.Add(deviceName, deviceDescription);


                                    HardwareChannel.HardwareConstants.GPIBDeviceType gpibDeviceType = new HardwareChannel.HardwareConstants.GPIBDeviceType();

                                    // VERY IMPORTANT!!!!!!!!!!
                                    // *******************  THIS IS WHERE YOU ADD DEVICE-DETECTION CODE FOR NEW GPIB DEVICES *********************/
                                    // detect the gpib device type
                                    if (deviceDescription.Contains("ESG-4000B"))
                                    {
                                        gpibDeviceType = HardwareChannel.HardwareConstants.GPIBDeviceType.Agilent_ESG_SIG_Generator;
                                    }
                                    // place any other device type detection code here as else ifs.
                                    else if (deviceDescription.Contains("N5181"))
                                    {
                                        gpibDeviceType = HardwareChannel.HardwareConstants.GPIBDeviceType.Agilent_ESG_SIG_Generator;
                                    }
                                    else
                                    {
                                        gpibDeviceType = HardwareChannel.HardwareConstants.GPIBDeviceType.Unknown;
                                    }


                                    HardwareChannel hc = new HardwareChannel(this.myServerSettings.ServerName,
                                        deviceName,
                                        HardwareChannel.gpibAddressToShortString(address),
                                        deviceDescription,
                                        HardwareChannel.HardwareConstants.ChannelTypes.gpib, address, gpibDeviceType);
                                    if (!serverSettings.ExcludedChannels.Contains(hc))
                                    {
                                        myHardwareChannels.Add(hc);
                                    }
                                }
                                catch (Exception e)
                                {
                                    messageLog(this, new MessageEvent("Exception when attempting to communicate with GPIB device " + "GPIB" + i + "/" + HardwareChannel.gpibAddressToShortString(address) + ". " + e.Message + "\n" + e.StackTrace));
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // throw e;
                    }

                    System.Console.WriteLine("...done.");

                }

                /*
                NationalInstruments.NI4882.Board board = new NationalInstruments.NI4882.Board();
                board.FindListeners(
                */

                #endregion

                #region Detect NI RS232 ports

                System.Console.WriteLine("Querying NI VisaNS Serial Resources...");

                string[] resourceNames = null;
                NationalInstruments.VisaNS.ResourceManager VisaRescources = null;

                System.Console.WriteLine("...done.");



                try
                {
                    VisaRescources = NationalInstruments.VisaNS.ResourceManager.GetLocalManager();
                    resourceNames = VisaRescources.FindResources("/?*");
                }
                catch (Exception e)
                {
                    if (messageLog != null)
                    {
                        messageLog(this, new MessageEvent("Caught exception when attempting to detect serial ports: " + e.Message + e.StackTrace));
                    }
                    else
                    {
                        MessageBox.Show("Caught exception when attempting to detect serial ports: " + e.Message + e.StackTrace);
                    }
                }


                if (resourceNames != null)
                {



                    foreach (string s in resourceNames)
                    {

                        try
                        {

                            System.Console.WriteLine("Querying Resource " + s);

                            NationalInstruments.VisaNS.HardwareInterfaceType hType;
                            short chanNum;
                            VisaRescources.ParseResource(s, out hType, out chanNum);
                            if (hType == NationalInstruments.VisaNS.HardwareInterfaceType.Serial)
                            {
                                NationalInstruments.VisaNS.SerialSession ss = (NationalInstruments.VisaNS.SerialSession)NationalInstruments.VisaNS.ResourceManager.GetLocalManager().Open(s);




                                string description = ss.HardwareInterfaceName;

                                HardwareChannel hc = new HardwareChannel(this.serverSettings.ServerName, "Serial", s, description, HardwareChannel.HardwareConstants.ChannelTypes.rs232);
                                if (!serverSettings.ExcludedChannels.Contains(hc))
                                {
                                    MyHardwareChannels.Add(hc);
                                }
                                if (!detectedDevices.Contains("Serial"))
                                {
                                    detectedDevices.Add("Serial");
                                    myDeviceDescriptions.Add("Serial", "All local RS232 devices.");
                                }

                                ss.Dispose();

                            }
                        }
                        catch (Exception e)
                        {
                            System.Console.WriteLine("Caught exception when attempting to query serial resource named " + s + ". Displaying exception and then skipping this device.");
                            ExceptionViewerDialog ev = new ExceptionViewerDialog(e);
                            ev.ShowDialog();

                        }

                        System.Console.WriteLine("...done.");
                    }
                }



                #endregion

                // If necessary, add DeviceSettings entries for devices.
                foreach (string device in detectedDevices)
                {
                    // If this device does not already have a settings object...
                    if (!myServerSettings.myDevicesSettings.ContainsKey(device))
                    {
                        myServerSettings.myDevicesSettings.Add(device,
                            new DeviceSettings(device, myDeviceDescriptions[device]));
                    }
                    else
                    {
                        myServerSettings.myDevicesSettings[device].deviceConnected = true;
                    }
                }

                #region FPGA Detection and configuration

                if (this.serverSettings.UseOpalKellyFPGA)
                {
                    try
                    {
                        System.Console.WriteLine("Scanning for Opal Kelly FPGA devices...");

                        opalKellyDevices = new List<com.opalkelly.frontpanel.okCFrontPanel>();
                        opalKellyDeviceNames = new List<string>();

                        com.opalkelly.frontpanel.okCFrontPanel ok = new com.opalkelly.frontpanel.okCFrontPanel();
                        int fpgaDeviceCount = ok.GetDeviceCount();

                        System.Console.WriteLine("Found " + fpgaDeviceCount + " fpga device(s).");

                        for (int i = 0; i < fpgaDeviceCount; i++)
                        {
                            string name = ok.GetDeviceListSerial(i);
                            com.opalkelly.frontpanel.okCFrontPanel fpgaDevice = new com.opalkelly.frontpanel.okCFrontPanel();

                            com.opalkelly.frontpanel.okCFrontPanel.ErrorCode errorCode;

                            errorCode = fpgaDevice.OpenBySerial(name);

                            if (errorCode != com.opalkelly.frontpanel.okCFrontPanel.ErrorCode.NoError)
                            {
                                System.Console.WriteLine("Unable to open FPGA device " + name + " due to error code " + errorCode.ToString());
                                continue;
                            }

                            if (!this.detectedDevices.Contains(name))
                            {
                                this.detectedDevices.Add(name);
                            }

                            if (!myServerSettings.myDevicesSettings.ContainsKey(name))
                            {
                                DeviceSettings newSettings = new DeviceSettings(name, "Opal Kelly FPGA Device");
                                newSettings.deviceConnected = true;
                                newSettings.isFPGADevice = true;
                                myServerSettings.myDevicesSettings.Add(name, newSettings);
                            }
                            else
                            {
                                myServerSettings.myDevicesSettings[name].deviceConnected = true;
                            }

                            bool deviceProgrammed = false;

                            if (myServerSettings.myDevicesSettings[name].DeviceEnabled)
                            {
                                if (myServerSettings.myDevicesSettings[name].UsingVariableTimebase)
                                {
                                    deviceProgrammed = true;
                                    if (myServerSettings.myDevicesSettings[name].MySampleClockSource == DeviceSettings.SampleClockSource.DerivedFromMaster)
                                    {
                                        errorCode = fpgaDevice.ConfigureFPGA("variable_timebase_fpga_internal.bit");
                                    }
                                    else
                                    {
                                        errorCode = fpgaDevice.ConfigureFPGA("variable_timebase_fpga_external.bit");
                                    }

                                    if (errorCode == com.opalkelly.frontpanel.okCFrontPanel.ErrorCode.NoError)
                                    {

                                        System.Console.WriteLine("Programmed fpga device " + name + ". Used fpga code version based on sample clock source " + myServerSettings.myDevicesSettings[name].MySampleClockSource.ToString());

                                        opalKellyDeviceNames.Add(name);
                                        opalKellyDevices.Add(fpgaDevice);
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("Error when attempting to program fpga device, error code " + errorCode.ToString());
                                    }
                                }
                            }
                            if (!deviceProgrammed)
                                System.Console.WriteLine("Fpga device " + name + " not programmed, as it is not enable or varible timebase on that device is not enabled.");


                        }
                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null && e.InnerException.InnerException != null)
                        {
                            Exception innerInner = e.InnerException.InnerException;
                            if (innerInner is System.BadImageFormatException)
                            {
                                MessageBox.Show("You are probably running the wrong build of Atticus. As of version 1.56 Atticus now comes in a 64-bit or 32-bit versions (in the bin\\Release-x64 and bin\\Release-x86 subdirectories respectively). These builds make use of different versions of the Opal Kelly Frontpanel libraries. Please ensure you are using the correct Atticus build. The exception which gave rise to this error will now be displayed.", "Wrong build of Atticus?");
                                DataStructures.ExceptionViewerDialog dial = new ExceptionViewerDialog(e);
                                dial.ShowDialog();
                            }
                            else if (innerInner is System.DllNotFoundException)
                            {
                                MessageBox.Show("As of version 1.56, Atticus is now built to use the 4.0.8 version of the Opal Kelly Frontpanel libraries/drivers. Perhaps you have the wrong version of the libraries installed? You can find a driver-only installer for 4.0.8 in the \\Opal Kelly\\Opal Kelly 4.0.8\\ directory of the Cicero distribution. Please install the newer drivers from there, or contact Opal Kelly to receive a CD with the newest full Frontpanel distribution (or go to the download section of github page for the Cicero Word generator, and download and install Opal-Kelly-CD-4.0.8.zip. You may also need to plug an Opal Kelly device into this computer during installation of the drivers, to insure that the WINUSB windows subsystem gets installed. The exception which gave rise to this error will now be displayed.", "Wrong version of FrontPanel drivers installed?");
                                DataStructures.ExceptionViewerDialog dial = new ExceptionViewerDialog(e);
                                dial.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("An unrecognized exception was encountered when scanning for Opal Kelly FPGA devices. The exception will now be displayed.", "Unrecognized exception.");
                                DataStructures.ExceptionViewerDialog dial = new ExceptionViewerDialog(e);
                                dial.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("An unrecognized exception was encountered when scanning for Opal Kelly FPGA devices. The exception will now be displayed.", "Unrecognized exception.");
                            DataStructures.ExceptionViewerDialog dial = new ExceptionViewerDialog(e);
                            dial.ShowDialog();
                        }

                        System.Console.WriteLine("Caught exceptions when attempting to scan for Opal Kelly FPGA devices. Aborted FPGA scan.");
                        opalKellyDevices = null;

                    }
                }
                else
                {
                    System.Console.WriteLine("Opal Kelly FPGA not enabled, so not scanning for them.");
                    opalKellyDevices = null;
                }


                #endregion

                System.Console.WriteLine("...done running refreshHardwareLists().");

                if (niDaqDevicesExistThatAreNotNamedDevSomething)
                {
                    System.Console.WriteLine("!! NOTE !! Some NI devices were detected whose name does not follow the Dev1, Dev2, Dev3, naming convention. This will cause problems.");
                    MessageBox.Show("National Instruments cards were detected whose names do not corresponding to the Dev1, Dev2, Dev3, etc. naming convention. This convention is relied upon by Atticus. Not following this convention will cause problems when attempting to run sequences on these devices. Please use MAX (the NI-supplied program) to rename your NI devices to follow the convention.", "Invalid niDaq Device Names");
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Unhandled exception when scanning connected hardware. Displaying exception, and then continuing to attempt to start Atticus.");
                ExceptionViewerDialog ev = new ExceptionViewerDialog(e);
                ev.ShowDialog();
                MessageBox.Show("Atticus encountered an unhandled exception when scanning for connected hardware. Atticus will continue to run. It is unlikely to function correctly for driving outputs, but will allow you to edit your settings to disable whatever is causing the unhandled exception.");
            }
        }




        /// <summary>
        /// Starts a new thread which attempts to achieve the given thread marshal status.
        /// Use this method to start or stop marshalling of communicator.
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



        #region  Trivial Helper methods

        private string ReplaceCommonEscapeSequences(string s)
        {
            return s.Replace("\\n", "\n").Replace("\\r", "\r");
        }

        private string InsertCommonEscapeSequences(string s)
        {
            return s.Replace("\n", "\\n").Replace("\r", "\\r");
        }

        private string justTheChannelName(string fullname, string devicename)
        {
            return fullname.Substring(devicename.Length + 1);
        }



        #endregion
    }
}
