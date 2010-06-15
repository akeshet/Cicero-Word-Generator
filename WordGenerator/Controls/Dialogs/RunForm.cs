using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataStructures;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;

namespace WordGenerator
{
    public partial class RunForm : Form
    {
        int repeatCount = 1;

        private delegate bool boolSequenceDelegate(SequenceData seq);
        private delegate bool boolIntSequenceDelegate(int a, SequenceData seq);
        private delegate void MessageEventCallDelegate(object o, MessageEvent e);
        private delegate void setStatusDelegate(RunFormStatus o);

        Thread getConfirmationThread;

        List<Socket> CameraPCsSocketList;
        List<SettingsData.IPAdresses> connectedPCs;

        bool isIdle = false;
        bool isCameraSaving = true;

        DateTime runStartTime;

        private delegate bool boolVoidDelegate();

        // this enum should enumerate the steps that should happen in sequence
        private enum RunFormStatus { Inactive, StartingRun, Running, FinishedRun };
        private RunFormStatus runFormStatus;

        private DateTime formCreationTime;

        private bool hasBeenActivated = false;

        public enum RunType
        {
            /// <summary>
            /// Run iteration #0 only.
            /// </summary>
            Run_Iteration_Zero,
            /// <summary>
            /// Run current iteration # only.
            /// </summary>
            Run_Current_Iteration,
            /// <summary>
            /// Run through the list of iteration #s in order.
            /// </summary>
            Run_Full_List,
            /// <summary>
            /// Run through the remaining list of iteration #s in order, starting with the current iteration #.
            /// </summary>
            Run_Continue_List,
            /// <summary>
            /// Runs the the full list, in random iteration order.
            /// </summary>
            Run_Random_Order_List
        }
        private RunType runType = RunType.Run_Iteration_Zero;

        private Thread runningThread = null;

        private bool runRepeat;

        private bool errorDetected;

        public bool ErrorDetected
        {
            get { return errorDetected; }
            set
            {
                bool temp = (errorDetected != value);
                errorDetected = value;
                if (temp)
                    updateErrorDisplay();
            }
        }


        private delegate void voidVoidDelegate();
        private void updateErrorDisplay()
        {
            if (this.InvokeRequired)
            {
                voidVoidDelegate callback = new voidVoidDelegate(updateErrorDisplay);
                this.Invoke(callback);
            }
            else
            {
                if (errorDetected)
                {
                    textBox1.BackColor = Color.Red;
                }
                else
                {
                    textBox1.BackColor = this.BackColor;
                }
            }
        }

        private void setStatus(RunFormStatus status)
        {
            if (this.InvokeRequired)
            {
                setStatusDelegate callback = new setStatusDelegate(setStatus);
                this.Invoke(callback, new object[] { status });
            }
            else
            {
                runFormStatus = status;

                switch (runFormStatus)
                {
                    case RunFormStatus.Inactive:
                        stopButton.Enabled = false;
                        closeButton.Enabled = false;
                        progressBar.Enabled = false;
                        runAgainButton.Enabled = false;
                        abortAfterThis.Enabled = false;
                        break;

                    case RunFormStatus.StartingRun:
                        stopButton.Enabled = true;
                        closeButton.Enabled = false;
                        progressBar.Enabled = false;
                        runAgainButton.Enabled = false;
                        abortAfterThis.Enabled = true;
                        break;

                    case RunFormStatus.FinishedRun:
                        stopButton.Enabled = false;
                        closeButton.Enabled = true;
                        progressBar.Enabled = false;
                        if ((this.runType == RunType.Run_Iteration_Zero || this.runType == RunType.Run_Current_Iteration) && !isIdle)
                        {
                            runAgainButton.Enabled = true;
                        }
                        abortAfterThis.Enabled = false;
                        break;

                    case RunFormStatus.Running:
                        stopButton.Enabled = true;
                        closeButton.Enabled = false;
                        progressBar.Enabled = true;
                        runAgainButton.Enabled = false;
                        abortAfterThis.Enabled = true;
                        break;
                }
            }
        }

        private Dictionary<int, Button> hotkeyButtons;

        public RunForm()
        {
            if (WordGenerator.mainClientForm.instance.studentEdition)
            {
                MessageBox.Show("Your Cicero Professional Edition (C) License expired on March 31. You are now running a temporary 24 hour STUDENT EDITION license. Please see http://web.mit.edu/~akeshet/www/Cicero/apr1.html for license renewal information.", "License expired -- temporary STUDENT EDITION license.");
            }

            IPAddress lclhst = null;
            IPEndPoint ipe = null;
            CameraPCsSocketList = new List<Socket>();
            connectedPCs = new List<SettingsData.IPAdresses>();
            bool errorOccured = false;
            foreach (SettingsData.IPAdresses ipAdress in Storage.settingsData.CameraPCs)
            {
                errorOccured = false;
                if (ipAdress.inUse)
                {
                    try
                    {

                        lclhst = Dns.GetHostEntry(ipAdress.pcAddress).AddressList[0];
                        ipe = new IPEndPoint(lclhst, ipAdress.Port);
                        CameraPCsSocketList.Add(new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp));

                        CameraPCsSocketList[CameraPCsSocketList.Count - 1].Blocking = false;
                        CameraPCsSocketList[CameraPCsSocketList.Count - 1].SendTimeout = 100;
                        CameraPCsSocketList[CameraPCsSocketList.Count - 1].ReceiveTimeout = 100;

                    }
                    catch
                    {
                        errorOccured = true;
                    }
                    if (errorOccured)
                        continue;
                    else
                    {
                        connectedPCs.Add(ipAdress);
                        try
                        {
                            CameraPCsSocketList[CameraPCsSocketList.Count - 1].Connect(ipe);
                        }
                        catch { }
                    }
                }
            }

            if (Storage.settingsData.CameraPCs.Count != 0)
            {
                getConfirmationThread = new Thread(new ThreadStart(getConfirmationEntryPoint));
                getConfirmationThread.Start();
            }
            // Supress hotkeys in main form when this form is runnings. This will be cleared when the run form closes.
            WordGenerator.mainClientForm.instance.suppressHotkeys = true;

            InitializeComponent();
            runFormStatus = RunFormStatus.Inactive;
            formCreationTime = DateTime.Now;

            // hotkey registration
            hotkeyButtons = new Dictionary<int, Button>();


            // fortune cookie
            if (WordGenerator.mainClientForm.instance.fortunes != null)
            {
                List<string> forts = WordGenerator.mainClientForm.instance.fortunes;
                Random rand = new Random();
                fortuneCookieLabel.Text = forts[rand.Next(forts.Count - 1)];
            }



        }

        private void registerAllHotkeys()
        {
            // Abort button
            RegisterHotKey(Handle, hotkeyButtons.Count, KeyModifiers.None, Keys.A);
            hotkeyButtons.Add(hotkeyButtons.Count, stopButton);

            RegisterHotKey(Handle, hotkeyButtons.Count, KeyModifiers.None, Keys.Escape);
            hotkeyButtons.Add(hotkeyButtons.Count, stopButton);

            // Run button (2 hotkeys)

            RegisterHotKey(Handle, hotkeyButtons.Count, KeyModifiers.None, Keys.R);
            hotkeyButtons.Add(hotkeyButtons.Count, runAgainButton);

            RegisterHotKey(Handle, hotkeyButtons.Count, KeyModifiers.None, Keys.F9);
            hotkeyButtons.Add(hotkeyButtons.Count, runAgainButton);

            // Close button

            RegisterHotKey(Handle, hotkeyButtons.Count, KeyModifiers.None, Keys.C);
            hotkeyButtons.Add(hotkeyButtons.Count, closeButton);

            RegisterHotKey(Handle, hotkeyButtons.Count, KeyModifiers.None, Keys.Space);
            hotkeyButtons.Add(hotkeyButtons.Count, closeButton);
        }

        private void unregisterAllHotkeys()
        {
            foreach (int id in hotkeyButtons.Keys)
            {
                UnregisterHotKey(Handle, id);
            }
            hotkeyButtons.Clear();
        }

        public RunForm(RunType runType, bool runRepeat)
            : this()
        {
            this.runType = runType;
            this.runRepeat = runRepeat;
            if (runType == RunType.Run_Full_List || runType == RunType.Run_Continue_List)
                runRepeat = false;

            if (runType != RunType.Run_Current_Iteration && runType != RunType.Run_Iteration_Zero)
                abortAfterThis.Visible = true;


            if (runRepeat)
                abortAfterThis.Visible = true;
        }

        public RunForm(RunType runType, bool runRepeat, bool isCameraSaving)
            : this()
        {
            this.runType = runType;
            this.isCameraSaving = isCameraSaving;
            this.savingWarning.Visible = !isCameraSaving;
            Storage.sequenceData.AISaved = isCameraSaving;

            this.runRepeat = runRepeat;

            if (runType == RunType.Run_Full_List || runType == RunType.Run_Continue_List)
                runRepeat = false;

            if (runType != RunType.Run_Current_Iteration && runType != RunType.Run_Iteration_Zero)
                abortAfterThis.Visible = true;


            if (runRepeat)
                abortAfterThis.Visible = true;
        }

        public void addMessageLogText(object sender, EventArgs e)
        {


            if (this.InvokeRequired)
            {
                MessageEventCallDelegate ev = new MessageEventCallDelegate(addMessageLogText);
                this.BeginInvoke(ev, new object[] { sender, e });
            }
            else
            {

                WordGenerator.mainClientForm.instance.handleMessageEvent(sender, e);
                MessageEvent message = (MessageEvent)e;
                this.textBox1.AppendText(message.myTime.ToString() + " " + message.ToString() + "\r\n");
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            /// bugfix: Before using hasBeenActivated, runs would get repeated if you switched to a different
            /// window and then switched back to the run window
            if (!hasBeenActivated)
            {
                hasBeenActivated = true;
                startRun();
            }


        }

        private delegate void voidBoolDelegate(bool calibrationShot);

        public void updateTitleBar(bool calibrationShot)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new voidBoolDelegate(updateTitleBar), new object[] { calibrationShot });
            }
            else
            {
                if (!calibrationShot)
                {
                    string title = "Running iteration ";
                    switch (runType)
                    {
                        case RunType.Run_Iteration_Zero:
                            title += "0.";
                            break;
                        case RunType.Run_Current_Iteration:
                            title += Storage.sequenceData.ListIterationNumber.ToString() + ".";
                            break;
                        case RunType.Run_Full_List:
                            title += Storage.sequenceData.ListIterationNumber.ToString() + "/" + (Storage.sequenceData.Lists.iterationsCount() - 1) + ".";
                            break;
                        case RunType.Run_Continue_List:
                            title += Storage.sequenceData.ListIterationNumber.ToString() + "/" + (Storage.sequenceData.Lists.iterationsCount() - 1) + ".";
                            break;
                        case RunType.Run_Random_Order_List:
                            title += Storage.sequenceData.ListIterationNumber.ToString() + "/" + (Storage.sequenceData.Lists.iterationsCount() - 1) + " (random run #" + random_order_run_iteration_number + ").";
                            break;
                    }

                    if (runRepeat)
                        title += " Repeat #" + repeatCount;
                    this.Text = title;

                }
                else
                {
                    this.Text = "Running calibration shot.";
                }
            }
        }

        private void startRun()
        {
            // start run! woo hoo!
            // do it async so as not to block the UI thread.
            bool listCouldBeLocked = true;
            if (!Storage.sequenceData.Lists.ListLocked)
            {
                addMessageLogText(this, new MessageEvent("Lists not locked, attempting to lock them..."));

                WordGenerator.mainClientForm.instance.variablesEditor1.tryLockLists();

                if (!Storage.sequenceData.Lists.ListLocked)
                {
                    addMessageLogText(this, new MessageEvent("Unable to lock lists. Aborting run. See the Variables tab."));

                    setStatus(RunFormStatus.FinishedRun);
                    listCouldBeLocked = false;
                }
                addMessageLogText(this, new MessageEvent("Lists locked successfully."));
            }

            if (listCouldBeLocked)
            {
                switch (runType)
                {
                    case RunType.Run_Iteration_Zero:
                        boolIntSequenceDelegate runZero = new boolIntSequenceDelegate(do_run);
                        runZero.BeginInvoke(0, Storage.sequenceData, null, null);
                        break;
                    case RunType.Run_Current_Iteration:
                        boolSequenceDelegate runCurrent = new boolSequenceDelegate(do_run);
                        runCurrent.BeginInvoke(Storage.sequenceData, null, null);
                        break;
                    case RunType.Run_Full_List:
                        boolVoidDelegate runList = new boolVoidDelegate(do_list_run);
                        runList.BeginInvoke(null, null);
                        break;
                    case RunType.Run_Continue_List:
                        boolVoidDelegate runContinueList = new boolVoidDelegate(do_continue_list_run);
                        runContinueList.BeginInvoke(null, null);
                        break;
                    case RunType.Run_Random_Order_List:
                        boolVoidDelegate runRandomList = new boolVoidDelegate(do_random_order_list_run);
                        runRandomList.BeginInvoke(null, null);
                        break;
                }
            }
        }

        private delegate void progressBarInitDelegate(double dur);
        private delegate void progressBarUpdateDelegate(int msec, SequenceData sequence);



        public bool do_continue_list_run()
        {
            addMessageLogText(this, new MessageEvent("Starting continue list run. " + Storage.sequenceData.Lists.iterationsCount() + " total iterations. Starting at iteration #" + Storage.sequenceData.ListIterationNumber));

            int i = Storage.sequenceData.ListIterationNumber;

            calibrationShot(i);


            for (; i < Storage.sequenceData.Lists.iterationsCount(); i++)
            {

                addMessageLogText(this, new MessageEvent("Iteration #" + i));
                bool temp = do_run(i, Storage.sequenceData);
                if (!temp)
                {
                    addMessageLogText(this, new MessageEvent("Aborting list run at iteration #" + i));
                    return false;
                }
                if (i != 0)
                {
                    calibrationShot(i);
                }
            }
            addMessageLogText(this, new MessageEvent("Continue list run successful."));
            return true;
        }

        private void calibrationShot(int i)
        {
            if (Storage.sequenceData.CalibrationShotsInfo.calibrationShotRequiredOnThisRun(i,
                Storage.sequenceData.Lists.iterationsCount()))
            {
                addMessageLogText(this, new MessageEvent("Taking a calibration shot."));
                bool temp = do_run(0, Storage.sequenceData.calibrationShotsInfo.CalibrationShotSequence, true);
                if (!temp)
                {
                    addMessageLogText(this, new MessageEvent("Calibration shot failed. Attempting to continue list run."));
                    this.ErrorDetected = true;
                }
            }
        }

        private int random_order_run_iteration_number;

        public bool do_random_order_list_run()
        {
            addMessageLogText(this, new MessageEvent("Starting random-order list run, " + Storage.sequenceData.Lists.iterationsCount() + " iterations."));
            List<int> iterationsRemaining = new List<int>();
            for (int i = 0; i < Storage.sequenceData.Lists.iterationsCount(); i++)
            {
                iterationsRemaining.Add(i);
            }

            random_order_run_iteration_number = 0;

            calibrationShot(random_order_run_iteration_number);
            while (iterationsRemaining.Count != 0)
            {
                Random rand = new Random();
                int selectedIterationIndex = rand.Next(iterationsRemaining.Count);
                int selectedIteration = iterationsRemaining[selectedIterationIndex];

                addMessageLogText(this, new MessageEvent("Iteration # " + selectedIteration + " (randomly selected)."));
                bool temp = do_run(selectedIteration, Storage.sequenceData);
                if (!temp)
                {
                    addMessageLogText(this, new MessageEvent("Aborting randomized list run after " + random_order_run_iteration_number + " randomly selected iterations."));
                    return false;
                }
                random_order_run_iteration_number++;

                calibrationShot(random_order_run_iteration_number);

                iterationsRemaining.Remove(selectedIteration);
            }
            addMessageLogText(this, new MessageEvent("Randomized list run successful."));
            return true;
        }

        public bool do_list_run()
        {
            addMessageLogText(this, new MessageEvent("Starting list run, " + Storage.sequenceData.Lists.iterationsCount() + " iterations."));

            int i = 0;
            calibrationShot(i);
            for (; i < Storage.sequenceData.Lists.iterationsCount(); i++)
            {
                addMessageLogText(this, new MessageEvent("Iteration #" + i));

                bool temp = do_run(i, Storage.sequenceData);
                if (!temp)
                {
                    addMessageLogText(this, new MessageEvent("Aborting list run at iteration #" + i));
                    return false;
                }
                if (i != 0)
                {
                    calibrationShot(i);
                }
            }
            addMessageLogText(this, new MessageEvent("List run successful."));
            return true;
        }

        public bool do_run(SequenceData sequence)
        {
            return do_run(Storage.sequenceData.ListIterationNumber, sequence);
        }

        public bool do_run(int iterationNumber, SequenceData sequence)
        {
            return do_run(iterationNumber, sequence, false);
        }

        public bool do_run(int iterationNumber, SequenceData sequence, bool calibrationShot)
        {
            this.runningThread = Thread.CurrentThread;
            bool keepGoing = true;
            while (keepGoing)
            {
                mainClientForm.instance.CurrentlyOutputtingTimestep = null;



                setStatus(RunFormStatus.StartingRun);

                lic_chk();


                addMessageLogText(this, new MessageEvent("Starting Run."));



                updateTitleBar(calibrationShot);

                bool wrongSavePath = false;
                try
                {
                    if (Storage.settingsData.SavePath != "")
                        System.IO.Directory.GetFiles(Storage.settingsData.SavePath);

                }
                catch
                {
                    wrongSavePath = true;
                }

                if (wrongSavePath)
                {
                    addMessageLogText(this, new MessageEvent("Unable to locate save path. Aborting run. See the SavePath setting (under Advanced->Settings Explorer)."));

                    setStatus(RunFormStatus.FinishedRun);
                    return false;
                }

                if (!sequence.Lists.ListLocked)
                {
                    addMessageLogText(this, new MessageEvent("Lists not locked, attempting to lock them..."));

                    WordGenerator.mainClientForm.instance.variablesEditor1.tryLockLists();

                    if (!sequence.Lists.ListLocked)
                    {
                        addMessageLogText(this, new MessageEvent("Unable to lock lists. Aborting run. See the Variables tab."));
                        ErrorDetected = true;

                        setStatus(RunFormStatus.FinishedRun);
                        return false;
                    }
                    addMessageLogText(this, new MessageEvent("Lists locked successfully."));
                }

                sequence.ListIterationNumber = iterationNumber;

                string listBoundVariableValues = "";

                foreach (Variable var in sequence.Variables)
                {
                    if (Storage.settingsData.PermanentVariables.ContainsKey(var.VariableName))
                    {
                        var.PermanentVariable = true;
                        var.PermanentValue = Storage.settingsData.PermanentVariables[var.VariableName];
                    }
                    else
                    {
                        var.PermanentVariable = false;
                    }
                }

                foreach (Variable var in sequence.Variables)
                {

                    if (var.ListDriven && !var.PermanentVariable)
                    {
                        if (listBoundVariableValues == "")
                        {
                            listBoundVariableValues = "List bound variable values: ";
                        }
                        listBoundVariableValues += var.VariableName + " = " + var.VariableValue.ToString() + ", ";
                    }
                }

                if (listBoundVariableValues != "")
                {
                    addMessageLogText(this, new MessageEvent(listBoundVariableValues));
                }


                foreach (Variable var in sequence.Variables)
                {
                    if (var.DerivedVariable)
                    {
                        if (var.parseVariableFormula(sequence.Variables) != null)
                        {
                            addMessageLogText(this, new MessageEvent("Warning! Derived variable " + var.ToString() + " has an an error. Will default to 0 for this run."));
                            ErrorDetected = true;
                        }
                    }
                }
                if (!calibrationShot)
                {
                    foreach (Variable var in sequence.Variables)
                    {
                        if (var.VariableName == "SeqMode")
                        {
                            addMessageLogText(this, new MessageEvent("Detected a variable with special name SeqMode. Nearest integer value " + (int)var.VariableValue + "."));
                            int i = (int)var.VariableValue;
                            if (i >= 0 && i < Storage.sequenceData.SequenceModes.Count)
                            {
                                SequenceMode mode = Storage.sequenceData.SequenceModes[i];
                                addMessageLogText(this, new MessageEvent("Settings sequence to sequence mode " + mode.ModeName + "."));
                                WordGenerator.mainClientForm.instance.sequencePage1.setMode(mode);
                            }
                            else
                            {
                                addMessageLogText(this, new MessageEvent("Warning! Invalid sequence mode index. Ignoring the SeqMode variable."));
                                ErrorDetected = true;
                            }
                        }
                    }
                }


                // Create timestep "loop copies" if there are timestep loops in use
                bool useLoops = false;
                foreach (TimestepGroup tsg in sequence.TimestepGroups)
                {
                    if (tsg.LoopTimestepGroup && sequence.TimestepGroupIsLoopable(tsg) && tsg.LoopCountInt>1)
                    {
                        useLoops = true;
                    }
                }
                if (useLoops)
                {
                    addMessageLogText(this, new MessageEvent("This sequence makes use of looping timestep groups. Creating temporary loop copies..."));
                    sequence.createLoopCopies();
                    addMessageLogText(this, new MessageEvent("...done"));
                }


                List<string> missingServers = Storage.settingsData.unconnectedRequiredServers();

                if (missingServers.Count != 0)
                {

                    string missingServerList = ServerManager.convertListOfServersToOneString(missingServers);

                    addMessageLogText(this, new MessageEvent("Unable to start run. The following required servers are not connected: " + missingServerList + "."));
                    ErrorDetected = true;
                    setStatus(RunFormStatus.FinishedRun);
                    return false;
                }


                List<LogicalChannel> overriddenDigitals = new List<LogicalChannel>();
                List<LogicalChannel> overriddenAnalogs = new List<LogicalChannel>();

                foreach (LogicalChannel lc in Storage.settingsData.logicalChannelManager.Digitals.Values)
                {
                    if (lc.overridden)
                        overriddenDigitals.Add(lc);
                }

                foreach (LogicalChannel lc in Storage.settingsData.logicalChannelManager.Analogs.Values)
                {
                    if (lc.overridden)
                        overriddenAnalogs.Add(lc);
                }

                if (overriddenDigitals.Count != 0)
                {
                    string list = "";
                    foreach (LogicalChannel lc in overriddenDigitals)
                    {
                        string actingName;
                        if (lc.Name != "" & lc.Name != null)
                        {
                            actingName = lc.Name;
                        }
                        else
                        {
                            actingName = "[Unnamed]";
                        }
                        list += actingName + ", ";
                    }
                    list = list.Remove(list.Length - 2);
                    list += ".";
                    addMessageLogText(this, new MessageEvent("Reminder. The following " + overriddenDigitals.Count + " digital channel(s) are being overridden: " + list));
                }

                if (overriddenAnalogs.Count != 0)
                {
                    string list = "";
                    foreach (LogicalChannel lc in overriddenAnalogs)
                    {
                        string actingName;
                        if (lc.Name != "" & lc.Name != null)
                        {
                            actingName = lc.Name;
                        }
                        else
                        {
                            actingName = "[Unnamed]";
                        }

                        list += actingName + ", ";
                    }
                    list = list.Remove(list.Length - 2);
                    list += ".";
                    addMessageLogText(this, new MessageEvent("Reminder. The following " + overriddenAnalogs.Count + " analog channel(s) are being overridden: " + list));
                }


                runStartTime = DateTime.Now;

                #region Sending camera instructions
                if (Storage.settingsData.UseCameras)
                {

                    byte[] msg;// = Encoding.ASCII.GetBytes(get_fileStamp(sequence));
                    string shot_name = NamingFunctions.get_fileStamp(sequence, Storage.settingsData, runStartTime);
                    string sequenceTime = sequence.SequenceDuration.ToString();
                    string FCamera;
                    string UCamera;

                    foreach (Socket theSocket in CameraPCsSocketList)
                    {
                        try
                        {
                            int index = CameraPCsSocketList.IndexOf(theSocket);
                            FCamera = connectedPCs[index].useFWCamera.ToString();
                            UCamera = connectedPCs[index].useUSBCamera.ToString();
                            msg = Encoding.ASCII.GetBytes(shot_name + "@" + sequenceTime + "@" + FCamera + "@" + UCamera + "@" + isCameraSaving.ToString() + "@\0");
                            theSocket.Send(msg, 0, msg.Length, SocketFlags.None);
                        }
                        catch { }
                    }
                }
                #endregion

                ServerManager.ServerActionStatus actionStatus;

                // send start timestamp
                addMessageLogText(this, new MessageEvent("Sending run start timestamp."));
                actionStatus = Storage.settingsData.serverManager.setNextRunTimestampOnConnectedServers(runStartTime, addMessageLogText);
                if (actionStatus != ServerManager.ServerActionStatus.Success)
                {
                    addMessageLogText(this, new MessageEvent("Unable to set start timestamp. " + actionStatus.ToString()));
                    ErrorDetected = true;
                    setStatus(RunFormStatus.FinishedRun);
                    return false;
                }



                // send settings data.
                addMessageLogText(this, new MessageEvent("Sending settings data."));
                actionStatus = Storage.settingsData.serverManager.setSettingsOnConnectedServers(Storage.settingsData, addMessageLogText);
                if (actionStatus != ServerManager.ServerActionStatus.Success)
                {
                    addMessageLogText(this, new MessageEvent("Unable to send settings data. " + actionStatus.ToString()));
                    ErrorDetected = true;
                    setStatus(RunFormStatus.FinishedRun);
                    return false;
                }

                // send sequence data.
                addMessageLogText(this, new MessageEvent("Sending sequence data."));
                actionStatus = Storage.settingsData.serverManager.setSequenceOnConnectedServers(sequence, addMessageLogText);
                if (actionStatus != ServerManager.ServerActionStatus.Success)
                {
                    addMessageLogText(this, new MessageEvent("Unable to send sequence data. " + actionStatus.ToString()));
                    ErrorDetected = true;
                    setStatus(RunFormStatus.FinishedRun);
                    return false;
                }

                // generate buffers. 
                addMessageLogText(this, new MessageEvent("Generating buffers."));
                actionStatus = Storage.settingsData.serverManager.generateBuffersOnConnectedServers(iterationNumber, addMessageLogText);
                if (actionStatus != ServerManager.ServerActionStatus.Success)
                {
                    addMessageLogText(this, new MessageEvent("Unable to generate buffers. " + actionStatus.ToString()));
                    ErrorDetected = true;
                    setStatus(RunFormStatus.FinishedRun);
                    return false;
                }


                // arm tasks.

                addMessageLogText(this, new MessageEvent("Arming tasks."));
                actionStatus = Storage.settingsData.serverManager.armTasksOnConnectedServers(addMessageLogText);
                if (actionStatus != ServerManager.ServerActionStatus.Success)
                {
                    addMessageLogText(this, new MessageEvent("Unable to arm tasks. " + actionStatus.ToString()));
                    ErrorDetected = true;
                    setStatus(RunFormStatus.FinishedRun);
                    return false;
                }

                // generate triggers

                addMessageLogText(this, new MessageEvent("Generating triggers."));
                actionStatus = Storage.settingsData.serverManager.generateTriggersOnConnectedServers(addMessageLogText);
                if (actionStatus != ServerManager.ServerActionStatus.Success)
                {
                    addMessageLogText(this, new MessageEvent("Unable to generate triggers. " + actionStatus.ToString()));
                    ErrorDetected = true;
                    setStatus(RunFormStatus.FinishedRun);
                    return false;
                }

                setStatus(RunFormStatus.Running);

                double duration = sequence.SequenceDuration;
                addMessageLogText(this, new MessageEvent("Sequence duration " + duration + " s. Running."));

                // async call to progress bar initialization

                progressBarInitDelegate pbid = new progressBarInitDelegate(initializeProgressBar);
                progressBarUpdateDelegate pbud = new progressBarUpdateDelegate(updateProgressBar);

                IAsyncResult res = BeginInvoke(pbid, new object[] { duration });
                EndInvoke(res);

                // Update the progress bar.
                long startTicks = DateTime.Now.Ticks;
                while (true)
                {
                    int elapsed_milliseconds = (int)((DateTime.Now.Ticks - startTicks) / 10000);
                    // progressBarUpdateDelegate pbud = new progressBarUpdateDelegate(updateProgressBar);
                    //res = BeginInvoke(pbud, new object[] { elapsed_milliseconds });



                    this.Invoke(pbud, new object[] { elapsed_milliseconds, sequence });


                    if (elapsed_milliseconds >= (200 + duration * 1000.0))
                        break;
                    Thread.Sleep(100);
                    //EndInvoke(res);
                }

                mainClientForm.instance.CurrentlyOutputtingTimestep = sequence.dwellWord();



                actionStatus = Storage.settingsData.serverManager.getRunSuccessOnConnectedServers(addMessageLogText);
                if (actionStatus != ServerManager.ServerActionStatus.Success)
                {
                    addMessageLogText(this, new MessageEvent("Run failed, possibly due to a buffer underrun. Please check the server event logs."));
                    ErrorDetected = true;
                    setStatus(RunFormStatus.FinishedRun);
                    return false;
                }


                if (useLoops)
                    sequence.cleanupLoopCopies();


                addMessageLogText(this, new MessageEvent("Finished run. Writing log file..."));
                RunLog runLog = new RunLog(runStartTime, formCreationTime, sequence, Storage.settingsData, WordGenerator.mainClientForm.instance.OpenSequenceFileName, WordGenerator.mainClientForm.instance.OpenSettingsFileName);
                string fileName = runLog.WriteLogFile();

                if (fileName != null)
                {
                    addMessageLogText(this, new MessageEvent("Log written to " + fileName));
                }
                else
                {
                    addMessageLogText(this, new MessageEvent("Log not written! Perhaps a file with this name already exists?"));
                    ErrorDetected = true;
                }
                setStatus(RunFormStatus.FinishedRun);




                if (runRepeat)
                    keepGoing = true;
                else
                    keepGoing = false;

                repeatCount++;

                if (abortAfterThis.Checked)
                    return false;
            }


            return true;
        }

        private void lic_chk()
        {
            if (WordGenerator.mainClientForm.instance.studentEdition)
                addMessageLogText(this, new MessageEvent("Your Cicero Professional Edition (C) License expired on March 31. You are now running a temporary 24 hour STUDENT EDITION license. Please see http://web.mit.edu/~akeshet/www/Cicero/apr1.html for license renewal information."));
        }

        private void updateProgressBar(int elapsed_milliseconds, SequenceData sequence)
        {
            try
            {
                TimeStep step = sequence.getTimeStepAtTime((double)elapsed_milliseconds / 1000.0);

                if (step != null)
                {
                    if (!step.LoopCopy)
                        mainClientForm.instance.CurrentlyOutputtingTimestep = step;
                    else
                        mainClientForm.instance.CurrentlyOutputtingTimestep = step.loopOriginalCopy;
                }
                string stepName = "";
                if (step != null)
                    stepName = step.StepName;

                stepLabel.Text = stepName;

                if (elapsed_milliseconds >= progressBar.Maximum)
                {
                    progressBar.Value = progressBar.Maximum;
                    timeLabel.Text = (progressBar.Maximum / 1000.0) + " s";
                }
                else if (elapsed_milliseconds <= 0)
                {
                    progressBar.Value = 0;
                    timeLabel.Text = "0 s";
                }
                else
                {
                    progressBar.Value = elapsed_milliseconds;
                    timeLabel.Text = (elapsed_milliseconds / 1000.0) + " s";
                }
            }
            catch (Exception e)
            {
                addMessageLogText(this, new MessageEvent("Except caught while updating progress bar: " + e.Message + e.StackTrace));
            }
        }

        private void initializeProgressBar(double duration)
        {
            progressBar.Value = 0;
            progressBar.Maximum = (int)(duration * 1000.0);
            durationLabel.Text = duration + " s";
            timeLabel.Text = "0 s";
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            WordGenerator.mainClientForm.instance.suppressHotkeys = false;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            foreach (Socket theSocket in CameraPCsSocketList)
            {
                try
                {

                    theSocket.Send(Encoding.ASCII.GetBytes("Abort"), 0, Encoding.ASCII.GetBytes("Abort").Length, SocketFlags.None);
                }
                catch { }
            }

            //Give time for the dwell word to be sent
            isIdle = true;
            this.runAgainButton.Enabled = false;
            System.Timers.Timer idleTimer = new System.Timers.Timer(1000);
            idleTimer.SynchronizingObject = this;
            idleTimer.Elapsed += new System.Timers.ElapsedEventHandler(idleTimer_Elapsed);
            idleTimer.Start();

            if (this.runningThread != null)
                runningThread.Abort();
            this.setStatus(RunFormStatus.FinishedRun);
            addMessageLogText(this, new MessageEvent("Run aborting."));
            Storage.settingsData.serverManager.stopAllServers(addMessageLogText);
            addMessageLogText(this, new MessageEvent("Run aborted."));
            TimeStep step = Storage.sequenceData.dwellWord();
            if (step != null)
            {
                addMessageLogText(this, new MessageEvent("Attempting to output the dwell timestep."));
                WordGenerator.Controls.TimestepEditor editor = WordGenerator.mainClientForm.instance.sequencePage1.getTimestepEditor(step);
                bool success = false;
                if (editor != null)
                {
                    success = editor.outputTimestepNow(false, false);
                }

                if (success)
                {
                    addMessageLogText(this, new MessageEvent("Dwell output successfull."));
                }
                else
                {
                    addMessageLogText(this, new MessageEvent("Dwell output unsuccessfull."));
                    ErrorDetected = true;
                }
            }
        }

        private void runAgainButton_Click(object sender, EventArgs e)
        {
            startRun();
        }



        private void RunForm_Load(object sender, EventArgs e)
        {

        }


        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd, // handle to window    
            int id, // hot key identifier    
            KeyModifiers fsModifiers, // key-modifier options    
            Keys vk    // virtual-key code    
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,
            int id
            );


        public enum KeyModifiers        //enum to call 3rd parameter of RegisterHotKey easily
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }


        const int WM_HOTKEY = 0x0312;

        protected override void WndProc(ref Message m)
        {

            switch (m.Msg)
            {
                // handle hotkey, based on the type of object bound to it
                case WM_HOTKEY:
                    {


                        int id = (int)m.WParam;
                        if (hotkeyButtons.ContainsKey(id))
                        {
                            Button bt = hotkeyButtons[id];
                            if (bt != null)
                                bt.PerformClick();
                        }


                    }
                    break;
            }


            base.WndProc(ref m);

        }

        private void RunForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Storage.settingsData.CameraPCs.Count != 0)
            {
                getConfirmationThread.Abort();
            }

            foreach (Socket theSocket in CameraPCsSocketList)
            {
                try
                {

                    theSocket.Send(Encoding.ASCII.GetBytes("Closing"), 0, Encoding.ASCII.GetBytes("Closing").Length, SocketFlags.None);
                }
                catch { }
            }
            if (!closeButton.Enabled)
            {
                e.Cancel = true;
            }

            Storage.sequenceData.cleanupLoopCopies();
        }


        private void RunForm_Activated(object sender, EventArgs e)
        {
            registerAllHotkeys();
        }

        private void RunForm_Deactivate(object sender, EventArgs e)
        {
            unregisterAllHotkeys();
        }

        //Receives log messages from the camera software. Only runs if a camera is listed
        private void getConfirmationEntryPoint()
        {
            string[] conf;
            byte[] bconf;
            while (true)
            {
                Thread.Sleep(1);
                foreach (Socket theSocket in CameraPCsSocketList)
                {
                    try
                    {
                        if (theSocket.Available > 0)
                        {
                            bconf = new byte[theSocket.Available];
                            theSocket.Receive(bconf, 0, bconf.Length, SocketFlags.None);
                            conf = (Encoding.ASCII.GetString(bconf)).Split('.');
                            for (int i = 0; conf.Length > i; i++)
                            {
                                if (conf[i] != "")
                                    addMessageLogText(this, new MessageEvent(conf[i] + "."));
                            }
                        }
                    }
                    catch { }
                }
            }
        }

        //ReEnables the Run Again Button
        private void idleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.runAgainButton.Enabled = true;
            isIdle = false;
            (sender as System.Timers.Timer).Stop();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.ErrorDetected = false;
        }
    }
}
