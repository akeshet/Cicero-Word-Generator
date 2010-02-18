using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using WordGenerator.Controls;
using DataStructures;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WordGenerator
{
    public partial class mainClientForm : Form
    {
        public static mainClientForm instance;

        /// <summary>
        /// This is the timestep that is currently being output. This applies only during "dwell" times, not during run times.
        /// After the end of a run, this gets set to the dwell timestep. If there is no currently outputting timestep, this gets set to 
        /// null.
        /// 
        /// 
        /// </summary>
        private TimeStep currentlyOutputtingTimestep;

        private object lockObj = new object();

        private string openSequenceFileName;

        public string OpenSequenceFileName
        {
            get { return openSequenceFileName; }
            set
            {
                openSequenceFileName = value;
                Storage.clientStartupSettings.sequenceDataFileName = value;
                updateFormTitle();
                RefreshRecentFiles();
            }
        }

        public List<string> fortunes;

        private string openSettingsFileName;

        public string OpenSettingsFileName
        {
            get { return openSettingsFileName; }
            set
            {
                openSettingsFileName = value;

                Storage.clientStartupSettings.settingsDataFileName = value;

                if (openSequenceFileName == null)
                    openSequenceFileName = "";

                updateFormTitle();
            }
        }


        public TimeStep CurrentlyOutputtingTimestep
        {
            get
            {
                return currentlyOutputtingTimestep;
            }
            set
            {
                lock (lockObj)
                {
                    if (currentlyOutputtingTimestep != value)
                    {
                        currentlyOutputtingTimestep = value;
                        if (instance != null)
                        {
                            setTimestepEditorBackgrounds();
                        }
                    }
                }
            }
        }

        private delegate void voidVoidDelegate();

        private void setTimestepEditorBackgrounds()
        {
            if (this.InvokeRequired)
            {

                this.BeginInvoke(new voidVoidDelegate(setTimestepEditorBackgrounds));
            }
            else
            {

                if (this.sequencePage1 != null)
                {
                    if (this.sequencePage1.timeStepsFlowPanel != null)
                    {
                        foreach (Control con in sequencePage1.timeStepsFlowPanel.Controls)
                        {
                            TimestepEditor te = con as TimestepEditor;
                            if (te != null)
                            {
                                te.updateBackColor(te.StepData == currentlyOutputtingTimestep);
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// To be called by the value-override controls, when their override value changes and they want to request
        /// that the currently outputting word be re-output.
        /// </summary>
        public bool reDwell()
        {
            if (currentlyOutputtingTimestep != null)
            {
                // this isn't maybe the most elegant way to do this, but it re-uses the dwell code that I already 
                // wrote in timestepeditor, so that's how it will be
                foreach (Control con in sequencePage1.timeStepsFlowPanel.Controls)
                {
                    TimestepEditor te = con as TimestepEditor;
                    if (te != null)
                    {
                        if (te.StepData == currentlyOutputtingTimestep)
                        {
                            return te.outputTimestepNow(false, true);
                        }
                    }
                }
            }
            return false;
        }

        private string clientStartupSettingsFile;

        private int cursorWaitCount = 0;

        public void cursorWait()
        {
            if (cursorWaitCount == 0)
                Cursor.Current = Cursors.WaitCursor;
            cursorWaitCount++;
        }


        public void activateAnalogGroupEditor(AnalogGroup ag)
        {
            this.analogGroupEditor1.setAnalogGroup(ag);
            this.mainTab.SelectedIndex = 1;


        }

        public void activateGPIBGroupEditor(GPIBGroup gg)
        {
            this.gpibGroupEditor1.setGpibGroup(gg);
            this.mainTab.SelectedIndex = 2;
        }

        public void activateRS232GroupEditor(RS232Group rg)
        {
            this.rS232GroupEditor1.setRS232Group(rg);
            this.mainTab.SelectedIndex = 3;
        }

        public void updateFormTitle()
        {
            string textPreamble;

            if (Storage.sequenceData == null || Storage.sequenceData.SequenceName == "")
            {
                textPreamble = "Cicero " + WordGenerator.Properties.Resources.VersionString;
            }
            else
            {
                textPreamble = Storage.sequenceData.SequenceName + " - Cicero " + WordGenerator.Properties.Resources.VersionString;
            }

            if (this.OpenSequenceFileName != "" && this.openSequenceFileName != null)
            {
                textPreamble += " | " + this.OpenSequenceFileName;
            }

            this.Text = textPreamble;


            if (this.settingsFileLabel != null)
            {
                this.settingsFileLabel.Text = this.OpenSettingsFileName;
            }
        }

        public void cursorWaitRelease()
        {
            cursorWaitCount--;
            if (cursorWaitCount == 0)
                Cursor.Current = Cursors.Default;
            if (cursorWaitCount < 0)
                cursorWaitCount = 0;
        }

        /// <summary>
        /// runLog will be null in general, when running Cicero as a standalone application. However, when Cicero is launched through Elgin,
        /// runLog will contain the log that we intent to browse.
        /// </summary>
        /// <param name="runLog"></param>
        public mainClientForm(RunLog runLog)
        {
            #region Singleton
            if (instance != null)
                throw new Exception("Word Generator is already running!");
            instance = this;
            #endregion

            try
            {
                string[] forts = WordGenerator.Properties.Resources.Fortunes.Split('\n');
                fortunes = new List<string>(forts);
            }
            catch { }


            if (runLog == null)
            {
                // Identify the clientStartupSettingsFile
                clientStartupSettingsFile = DefaultNames.ClientStartupSettingsFile;

                // Load all necessary data into Storage
                Storage.SaveAndLoad.LoadAllSubclasses(clientStartupSettingsFile);

            }
            else
            {
                Storage.clientStartupSettings = new ClientStartupSettings();
                Storage.sequenceData = runLog.RunSequence;
                Storage.settingsData = runLog.RunSettings;
            }


            InitializeComponent();

            CiceroSplashForm splash = new CiceroSplashForm();

            splash.Show();

            // bind F9 hotkey to run button:
            if (hotKeyBindings == null)
                hotKeyBindings = new List<object>();


            /*
            RegisterHotKey(Handle, hotKeyBindings.Count, KeyModifiers.None, Keys.F9);
            hotKeyBindings.Add(sequencePage1.runControl1.runZeroButton);
            */

            // bind F11 hotkey to server manager:
            RegisterHotKey(Handle, hotKeyBindings.Count, KeyModifiers.None, Keys.F11);
            hotKeyBindings.Add(this.serverManagerButton);


            // bind F1 to F8 to appropriate tab pages

            RegisterHotKey(Handle, hotKeyBindings.Count, KeyModifiers.None, Keys.F1);
            hotKeyBindings.Add(this.sequencePage);

            RegisterHotKey(Handle, hotKeyBindings.Count, KeyModifiers.None, Keys.F2);
            hotKeyBindings.Add(this.overrideTab);

            RegisterHotKey(Handle, hotKeyBindings.Count, KeyModifiers.None, Keys.F3);
            hotKeyBindings.Add(this.analogPage);

            RegisterHotKey(Handle, hotKeyBindings.Count, KeyModifiers.None, Keys.F4);
            hotKeyBindings.Add(this.gpibPage);

            RegisterHotKey(Handle, hotKeyBindings.Count, KeyModifiers.None, Keys.F5);
            hotKeyBindings.Add(this.rs232Tab);

            RegisterHotKey(Handle, hotKeyBindings.Count, KeyModifiers.None, Keys.F6);
            hotKeyBindings.Add(this.commonWaveformPage);

            RegisterHotKey(Handle, hotKeyBindings.Count, KeyModifiers.None, Keys.F7);
            hotKeyBindings.Add(this.variablesPage);

            RegisterHotKey(Handle, hotKeyBindings.Count, KeyModifiers.None, Keys.F8);
            hotKeyBindings.Add(this.pulsesTab);




            RefreshRecentFiles();
            this.RefreshSettingsDataToUI(Storage.settingsData);
            this.RefreshSequenceDataToUI(Storage.sequenceData);
        }



        public mainClientForm()
            : this(null)
        {

        }

        /// <summary>
        /// Fills the 'Recent Files' menu tab under File menu with relevant data.
        /// </summary>
        private void RefreshRecentFiles()
        {
            if (Storage.clientStartupSettings != null)
            {
                if (recentFilesToolStripMenuItem != null)
                {
                    // Recent files feature
                    int nRecentFiles = Storage.clientStartupSettings.recentFiles.Count;
                    if (nRecentFiles == 0)
                    {
                        ToolStripMenuItem tsmi = new ToolStripMenuItem("No recent files");
                        tsmi.Enabled = false;
                        recentFilesToolStripMenuItem.DropDownItems.Add(tsmi);
                    }
                    else
                    {
                        recentFilesToolStripMenuItem.DropDownItems.Clear();
                        ToolStripSeparator tss = new ToolStripSeparator();
                        for (int i = 0; i < System.Math.Min(nRecentFiles, GUIConstants.nMaxRecentFiles); i++)
                        {
                            string recentFileName = Storage.clientStartupSettings.recentFiles[i];
                            ToolStripMenuItem tsmi = new ToolStripMenuItem(recentFileName);
                            if (Storage.clientStartupSettings.RecentFilesLastUsageDates.ContainsKey(recentFileName))
                            {
                                tsmi.ToolTipText = "Last open on " + Storage.clientStartupSettings.RecentFilesLastUsageDates[recentFileName].Date.ToShortDateString();

                            }
                            else
                            {
                                tsmi.ToolTipText = "Last open unknown.";
                            }
                            tsmi.Click += new EventHandler(recentFileClick);
                            recentFilesToolStripMenuItem.DropDownItems.Add(tsmi);
                        }
                    }
                }
            }
        }

        void recentFileClick(object sender, EventArgs e)
        {
            ToolStripMenuItem ts = sender as ToolStripMenuItem;
            if (ts == null)
                return;

            if (Storage.SaveAndLoad.LoadSequenceDataToStorage(ts.Text))
            {
                RefreshSequenceDataToUI(Storage.sequenceData);
                this.handleMessageEvent(this, new MessageEvent("Loaded sequence file " + this.openSequenceFileName));
            }
        }

        /// <summary>
        /// This function should be called after SequenceData is loaded, or if it is changed by a non-UI element.
        /// This is a slow function, it effectively causes all of the controls which lay themselves out based on SequenceData
        /// to be re-drawn. Thus, it should not be called unnecessarily.
        /// </summary>
        public void RefreshSequenceDataToUI(SequenceData sequenceData)
        {
            
                WordGenerator.mainClientForm.instance.cursorWait();

                
                this.commonWaveformEditor1.setCommonWaveforms(Storage.sequenceData.CommonWaveforms);

                if (sequenceData.AnalogGroups.Count != 0)
                    this.analogGroupEditor1.setAnalogGroup(sequenceData.AnalogGroups[0]);
                else
                    this.analogGroupEditor1.setAnalogGroup(null);

                
                this.sequencePage1.layoutAll();

                this.variablesEditor1.layout();

                if (sequenceData.GpibGroups.Count != 0)
                    this.gpibGroupEditor1.setGpibGroup(sequenceData.GpibGroups[0]);
                else
                    this.gpibGroupEditor1.setGpibGroup(null);

                if (sequenceData.RS232Groups.Count != 0)
                    this.rS232GroupEditor1.setRS232Group(sequenceData.RS232Groups[0]);
                else
                    this.rS232GroupEditor1.setRS232Group(null);

                this.analogGroupEditor1.updateRunOrderPanel();
                this.gpibGroupEditor1.updateRunOrderPanel();

                updateFormTitle();

                if (!Storage.sequenceData.TimeSteps.Contains(CurrentlyOutputtingTimestep))
                {
                    CurrentlyOutputtingTimestep = null;
                }

                pulsesPage1.layout();

                sequencePage1.forceUpdateAllScrollbars();

                setTimestepEditorBackgrounds();

                waitForReady.Checked = Storage.sequenceData.WaitForReady;

                WordGenerator.mainClientForm.instance.cursorWaitRelease();

        }

        public void RefreshSettingsDataToUI(SettingsData settingsData)
        {
            WordGenerator.mainClientForm.instance.cursorWait();

            this.analogGroupEditor1.setChannelCollection(settingsData.logicalChannelManager.ChannelCollections[HardwareChannel.HardwareConstants.ChannelTypes.analog]);
            this.gpibGroupEditor1.setChannelCollection(settingsData.logicalChannelManager.ChannelCollections[HardwareChannel.HardwareConstants.ChannelTypes.gpib]);
            this.rS232GroupEditor1.setChannelCollection(settingsData.logicalChannelManager.ChannelCollections[HardwareChannel.HardwareConstants.ChannelTypes.rs232]);
            this.overridePage1.setSettings(Storage.settingsData);
            this.sequencePage1.layoutSettingsData();
            this.sequencePage1.updateOverrideCount();

            setTimestepEditorBackgrounds();

            WordGenerator.mainClientForm.instance.cursorWaitRelease();
        }


        #region Self-explanatory event handlers
        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Storage.SaveAndLoad.SaveAllSubclasses();
        }
        private void saveClientStartupSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Storage.SaveAndLoad.SaveClientStartupSettings();
        }
        private void saveSettingsDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Storage.SaveAndLoad.SaveSettingsData(AppDomain.CurrentDomain.BaseDirectory + Storage.clientStartupSettings.settingsDataFileName);
        }
        private void saveSequenceDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Storage.SaveAndLoad.SaveSequenceData(AppDomain.CurrentDomain.BaseDirectory + Storage.clientStartupSettings.sequenceDataFileName);
        }

        private void editLogicalDevicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChannelManager.ChannelManager logicalChannelManager =
                new ChannelManager.ChannelManager();
            logicalChannelManager.ShowDialog();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void sequenceExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SequenceExplorerForm sef = new SequenceExplorerForm();

            sef.ShowDialog();
        }

        private void settingsExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsExplorerForm sef = new SettingsExplorerForm();
            sef.ShowDialog();
        }

        private void populateSequenceWithNewChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Storage.sequenceData.populateWithChannels(Storage.settingsData);
            this.RefreshSequenceDataToUI(Storage.sequenceData);

        }

        private void editServerManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerManagerForm manager = new ServerManagerForm();
            manager.ShowDialog();
        }

        private void tssFile_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Storage.SaveAndLoad.LoadSequenceDataToStorage(null))
            {
                RefreshRecentFiles();
                this.RefreshSequenceDataToUI(Storage.sequenceData);
                this.handleMessageEvent(this, new MessageEvent("Loaded sequence file " + this.openSequenceFileName));
            }

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Storage.SaveAndLoad.SaveSequenceData(null);
            RefreshRecentFiles();
        }

        private void loadSettings_Click(object sender, EventArgs e)
        {
            if (Storage.SaveAndLoad.LoadSettingsData(null))
            {
                RefreshSettingsDataToUI(Storage.settingsData);
                this.handleMessageEvent(this, new MessageEvent("Loaded settings file " + this.openSettingsFileName));
            }
        }

        private void saveSettings_Click(object sender, EventArgs e)
        {
            Storage.SaveAndLoad.SaveSettingsData(null);
        }

        private void loadDefaultSettings_Click(object sender, EventArgs e)
        {
            if (Storage.SaveAndLoad.LoadSettingsData(DefaultNames.FileNames.SettingsData))
            {


                RefreshSettingsDataToUI(Storage.settingsData);
                this.handleMessageEvent(this, new MessageEvent("Loaded default settings from " + this.openSettingsFileName));
            }
        }

        private void saveDefaultSettings_Click(object sender, EventArgs e)
        {
            Storage.SaveAndLoad.SaveSettingsData(AppDomain.CurrentDomain.BaseDirectory + DefaultNames.FileNames.SettingsData);
        }

        private void mainClientForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            /* CiceroSplashForm splash = new CiceroSplashForm();
             splash.Show();*/
        }

        protected override bool IsInputChar(char charCode)
        {
            return true;
            //return base.IsInputChar(charCode);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);


            if (!e.Handled)
            {
                System.Console.WriteLine("blah");
            }

        }



        public void addStatusText(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(addStatusText), new object[] { sender, e });
            }
            else
            {
                toolStripStatusLabel1.Text = e.ToString();
            }
        }

        public void handleMessageEvent(object sender, EventArgs e)
        {
            addStatusText(sender, e);
            addMessageLogText(sender, e);
        }

        public void addMessageLogText(object sender, EventArgs e)
        {

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(addMessageLogText), new object[] { sender, e });
            }
            else
            {
                if (e is MessageEvent)
                {
                    MessageEvent message = (MessageEvent)e;
                    this.messageLogTextBox.AppendText(message.myTime.ToString() + " " + message.ToString() + "\r\n");
                }
                else
                {
                    this.messageLogTextBox.AppendText(e.ToString());
                }
            }
        }


        public enum KeyModifiers        //enum to call 3rd parameter of RegisterHotKey easily
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }


        // Hotkey handling requires some external non-c# library stuff:

        //API Imports
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd, // handle to window    
            int id, // hot key identifier    
            KeyModifiers fsModifiers, // key-modifier options    
            Keys vk    // virtual-key code    
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd, // handle to window    
        int id      // hot key identifier    
            );


        const int WM_HOTKEY = 0x0312;

        /// <summary>
        /// This list is used to keep track of what hotkey ID corresponds to what action.
        /// </summary>
        List<object> hotKeyBindings;

        public bool suppressHotkeys = false;

        // We are overriding WndProc because we need to handle hotkeys here:
        protected override void WndProc(ref Message m)
        {

            switch (m.Msg)
            {
                // handle hotkey, based on the type of object bound to it
                case WM_HOTKEY:
                    {
                        if (!suppressHotkeys)
                        {

                            int id = (int)m.WParam;
                            object hotkeyObj = hotKeyBindings[id];
                            if (hotkeyObj is TimestepEditor)
                            {
                                TimestepEditor te = (TimestepEditor)hotkeyObj;
                                te.outputTimestepNow();
                            }
                            else if (hotkeyObj is Button)
                            {
                                Button b = (Button)hotkeyObj;
                                b.PerformClick();
                            }
                            else if (hotkeyObj is CheckBox)
                            {
                                CheckBox c = (CheckBox)hotkeyObj;
                                c.Checked = !c.Checked;
                            }
                            else if (hotkeyObj is TabPage)
                            {
                                TabPage t = (TabPage)hotkeyObj;
                                mainTab.SelectedTab = t;
                            }
                        }
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// used to register timestep hotkeys. Ctrl + key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hotkeyObject"></param>
        public void registerTimestepHotkey(char key, TimestepEditor hotkeyObject)
        {
            if (hotKeyBindings == null)
                hotKeyBindings = new List<object>();

            // convert the character to a Keys enum element. This is weird but necessary.
            string keyStr = "" + char.ToUpper(key);

            Keys myKey = (Keys)Enum.Parse(typeof(Keys), keyStr);

            RegisterHotKey(Handle, hotKeyBindings.Count, KeyModifiers.Control, myKey);
            hotKeyBindings.Add(hotkeyObject);
        }

        public void unregisterHotkey(char key, object hotkeyObject)
        {
            if (hotKeyBindings == null)
                hotKeyBindings = new List<object>();

            if (hotKeyBindings.Contains(hotkeyObject))
            {
                int id = hotKeyBindings.IndexOf(hotkeyObject);
                UnregisterHotKey(Handle, id);
                hotKeyBindings[id] = null;
            }
        }

        /// <summary>
        /// used to register hotkeys for toggling override values. Ctrl + Alt + key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hotkeyObject"></param>
        public void registerToggleHotkey(char key, CheckBox hotkeyObject)
        {
            if (hotKeyBindings == null)
                hotKeyBindings = new List<object>();

            // convert the character to a Keys enum element. This is weird but necessary.
            string keyStr = "" + char.ToUpper(key);

            Keys myKey = (Keys)Enum.Parse(typeof(Keys), keyStr);

            RegisterHotKey(Handle, hotKeyBindings.Count, KeyModifiers.Control | KeyModifiers.Alt, myKey);
            hotKeyBindings.Add(hotkeyObject);
        }



        private void newSequence_Click(object sender, EventArgs e)
        {
            Storage.sequenceData = new SequenceData();
            WordGenerator.mainClientForm.instance.OpenSequenceFileName = null;
            RefreshSequenceDataToUI(Storage.sequenceData);
        }






        private void inspectVariableTimebaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertyGridForm dialog = new PropertyGridForm(Storage.sequenceData.generateVariableTimebaseSegments(SequenceData.VariableTimebaseTypes.AnalogGroupControlledVariableFrequencyClock, .000001));
            dialog.ShowDialog();
        }

        private void calculateVariableTimebaseAnalogBufferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimestepTimebaseSegmentCollection timebaseSegments = Storage.sequenceData.generateVariableTimebaseSegments(SequenceData.VariableTimebaseTypes.AnalogGroupControlledVariableFrequencyClock, .000001);
            int nSamples = 1 + timebaseSegments.nSegmentSamples();

            double[] ans = new double[nSamples];
            Storage.sequenceData.computeAnalogBuffer(0, .000001, ans, timebaseSegments);
        }

        private void runIteration0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!suppressHotkeys)
            {
                RunForm rf = new RunForm(RunForm.RunType.Run_Iteration_Zero, sequencePage1.runControl1.repeatCheckBox.Checked,true);
                rf.ShowDialog();
            }

        }

        private void runCurrentIterationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!suppressHotkeys)
            {
                RunForm rf = new RunForm(RunForm.RunType.Run_Current_Iteration, sequencePage1.runControl1.repeatCheckBox.Checked,true);
                rf.ShowDialog();
            }
        }

        private void runListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!suppressHotkeys)
            {
                RunForm rf = new RunForm(RunForm.RunType.Run_Full_List, sequencePage1.runControl1.repeatCheckBox.Checked, true);
                rf.ShowDialog();
            }
        }

        private void continueListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!suppressHotkeys)
            {
                RunForm rf = new RunForm(RunForm.RunType.Run_Continue_List, sequencePage1.runControl1.repeatCheckBox.Checked, true);
                rf.ShowDialog();
            }
        }

        private void runListInRandomOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!suppressHotkeys)
            {
                RunForm rf = new RunForm(RunForm.RunType.Run_Random_Order_List, sequencePage1.runControl1.repeatCheckBox.Checked, true);
                rf.ShowDialog();
            }
        }

        private void runWithoutSavingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!suppressHotkeys)
            {
                RunForm rf = new RunForm(RunForm.RunType.Run_Iteration_Zero, sequencePage1.runControl1.repeatCheckBox.Checked, false);
                rf.ShowDialog();
            }
        }

        private void mainClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Storage.SaveAndLoad.SaveClientStartupSettings();
        }

        private void calculateVariableTimebaseDigitalBufferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SequenceData sequence = Storage.sequenceData;

            TimestepTimebaseSegmentCollection timebaseSegments = sequence.generateVariableTimebaseSegments(SequenceData.VariableTimebaseTypes.AnalogGroupControlledVariableFrequencyClock,
                .001);

            int nVTSamples = 1;
            foreach (TimeStep st in timebaseSegments.Keys)
            {
                foreach (SequenceData.VariableTimebaseSegment seg in timebaseSegments[st])
                {
                    nVTSamples += seg.NSegmentSamples;
                }

            }

            bool[] ans1;
            bool[] ans2 = new bool[nVTSamples];
            bool[] ans3;

            ans1 = sequence.computeDigitalBuffer(0, .001);
            sequence.computeDigitalBuffer(0, .001, ans2, timebaseSegments);

            ans3 = sequence.getDigitalBufferClockSharedWithVariableTimebaseClock(timebaseSegments, 0, .001);



        }


        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataStructures.LicenseInfoForm form = new LicenseInfoForm();
            form.ShowDialog();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CiceroSplashForm spl = new CiceroSplashForm(false);
            spl.ShowDialog();
        }

        private void mainClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string title = "Is this your homework Larry?";
            if (DateTime.Now.Millisecond % 2 == 0)
            {
                title = "This was a valued rug.";
            }
            DialogResult result = MessageBox.Show("Cicero is closing. Do you want to save the open Sequence or Settings file first?", title, MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            if (result == DialogResult.Yes)
            {
                DialogResult res2 = MessageBox.Show("Save sequence?", "", MessageBoxButtons.YesNo);
                if (res2 == DialogResult.Yes)
                {
                    Storage.SaveAndLoad.SaveSequenceData(null);
                }

                DialogResult res3 = MessageBox.Show("Save settings?", "", MessageBoxButtons.YesNo);
                if (res3 == DialogResult.Yes)
                {
                    Storage.SaveAndLoad.SaveSettingsData(null);
                }
            }

        }

        private void saveMarked_Click(object sender, EventArgs e)
        {
            List<TimeStep> markedSteps = markedTimesteps();

            if (markedSteps.Count == 0)
            {
                MessageBox.Show("Unable to save marked timesteps, as no timesteps are marked.");
            }

            SequenceData saveMe = new SequenceData(markedSteps);

            Storage.SaveAndLoad.SaveSequenceData(null, saveMe);
        }

        private List<TimeStep> markedTimesteps()
        {
            List<TimeStep> markedSteps = new List<TimeStep>();
            foreach (Control con in sequencePage1.timeStepsFlowPanel.Controls)
            {
                TimestepEditor ed = con as TimestepEditor;
                if (ed != null)
                {
                    if (ed.Marked)
                    {
                        markedSteps.Add(ed.StepData);
                    }
                }
            }
            return markedSteps;
        }

        private void insertSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<TimeStep> markedSteps = markedTimesteps();

            DialogResult result;

            if (markedSteps.Count == 0)
            {
                result = MessageBox.Show("This operation will insert a sequence at the beginning of the currently open sequence. To insert a sequence elsewhere in the open sequence, mark the timestep that you would like to insertion to come after.", "Insert sequence.", MessageBoxButtons.OKCancel);
            }
            else if (markedSteps.Count == 1)
            {
                result = MessageBox.Show("This operation will insert a sequence into the currently open sequence, after the marked timestep.", "Insert Sequence.", MessageBoxButtons.OKCancel);
            }
            else
            {
                MessageBox.Show("There are " + markedSteps.Count + " marked timesteps. Please mark at most 1 timestep when using this feature.");
                return;
            }

            if (result == DialogResult.Cancel)
            {
                return;
            }

            SequenceData insertMe = Storage.SaveAndLoad.LoadSequenceWithFileDialog();
            if (insertMe != null)
            {

                bool insertAsTimestepGroup = false;

                insertAsTimestepGroup = (MessageBox.Show("Would you like a new Timestep Group to be created, and for all the inserted timesteps to be placed in that group?", "Insert as group?", MessageBoxButtons.YesNo) == DialogResult.Yes);
                TimestepGroup newGroup;
                if (insertAsTimestepGroup)
                {
                    MessageBox.Show("The inserted subsequence will be assigned to a new timestep group named Inserted Sequence.", "Inserting as a group.");
                    newGroup = new TimestepGroup("Inserted Sequence.");
                    Storage.sequenceData.TimestepGroups.Add(newGroup);
                    foreach (TimeStep step in insertMe.TimeSteps)
                    {
                        step.MyTimestepGroup = newGroup;
                    }
                }

                string response;

                bool dupAnalogs = false ;
                bool dupPulses = false;
                bool dupVariables=false;
                bool dupgpibs=false;
                bool duprs232s=false;
                bool dupTsGs = false;

                SequenceData seq = Storage.sequenceData;

                foreach (AnalogGroup ag in seq.AnalogGroups)
                {
                    foreach (AnalogGroup ag2 in insertMe.AnalogGroups)
                    {
                        if (ag.GroupName == ag2.GroupName)
                            dupAnalogs = true;
                    }
                }

                foreach (Pulse p1 in seq.DigitalPulses)
                    foreach (Pulse p2 in insertMe.DigitalPulses)
                        if (p1.PulseName == p2.PulseName)
                            dupPulses = true;

                foreach (Variable v1 in seq.Variables)
                    foreach (Variable v2 in insertMe.Variables)
                        if (!v1.IsSpecialVariable)
                            if (!v2.IsSpecialVariable)
                                if (v1.VariableName == v2.VariableName)
                                    dupVariables = true;

                foreach (GPIBGroup g1 in seq.GpibGroups)
                    foreach (GPIBGroup g2 in insertMe.GpibGroups)
                        if (g1.GroupName == g2.GroupName)
                            dupgpibs = true;

                foreach (RS232Group g1 in seq.RS232Groups)
                    foreach (RS232Group g2 in insertMe.RS232Groups)
                        if (g1.GroupName == g2.GroupName)
                            duprs232s = true;

                foreach (TimestepGroup tg1 in seq.TimestepGroups)
                    foreach (TimestepGroup tg2 in insertMe.TimestepGroups)
                        if (tg1.TimestepGroupName == tg2.TimestepGroupName)
                            dupTsGs = true;

                if (markedSteps.Count == 0)
                {
                    response = Storage.sequenceData.insertSequence(insertMe, 0);
                }
                else
                {
                    response = Storage.sequenceData.insertSequence(insertMe, markedSteps[0]);
                }

                if (response != null)
                {
                    MessageBox.Show(response);
                }

                if (dupAnalogs)
                    MessageBox.Show("The insertion operation has created duplicate Analog groups with identical names. To reduce confusion, it is recommended that the user now cleans these up.", "Analog Group duplicates detected.");
                if (dupgpibs)
                    MessageBox.Show("The insertion operation has created duplicate GPIB groups with identical names. To reduce confusion, it is recommended that the user now cleans these up.", "GPIB Group duplicates detected.");
                if (dupPulses)
                    MessageBox.Show("The insertion operation has created duplicate Pulses with identical names. To reduce confusion, it is recommended that the user now cleans these up.", "Pulse duplicates detected.");
                if (duprs232s)
                    MessageBox.Show("The insertion operation has created duplicate RS232 groups with identical names. To reduce confusion, it is recommended that the user now cleans these up.", "RS232 Group duplicates detected.");
                if (dupVariables)
                    MessageBox.Show("The insertion operation has created duplicate Variables with identical names. To reduce confusion, it is recommended that the user now cleans these up.", "Variable duplicates detected.");
                if (dupTsGs)
                    MessageBox.Show("The insertion operation has created duplicate Timestep Groups with identical names. To reduce confusion, it is recommended that the user now cleans these up.", "Timestep Group duplicates detected.");







                RefreshSequenceDataToUI(Storage.sequenceData);
            }

        }

        private void waitForReady_CheckedChanged(object sender, EventArgs e)
        {
            if (Storage.sequenceData != null)
            {
                Storage.sequenceData.WaitForReady = waitForReady.Checked;
            }
        }

        private void loadLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Title = "Open Log File";
            fileDialog.Filter = "RunLog files (*.clg)|*.clg|All files (*.*)|*.*";
            fileDialog.FilterIndex = 1;
            fileDialog.Multiselect = false;

            DialogResult result = fileDialog.ShowDialog();

            string fileName = fileDialog.FileName;

            if (result == DialogResult.OK)
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                BinaryFormatter bf = new BinaryFormatter();
                RunLog log = (RunLog)bf.Deserialize(fs);
                fs.Close();

                Storage.settingsData = log.RunSettings;
                WordGenerator.mainClientForm.instance.OpenSettingsFileName = fileName;
                Storage.sequenceData = log.RunSequence;                
                WordGenerator.mainClientForm.instance.OpenSequenceFileName = fileName;

                this.RefreshSequenceDataToUI(Storage.sequenceData);
                this.handleMessageEvent(this, new MessageEvent("Loaded sequence file " + fileName));

            }

        }

        private void mainClientForm_Activated(object sender, EventArgs e)
        {
            sequencePage1.runControl1.IsRunNoSaveEnabled=(Storage.settingsData.CameraPCs.Count != 0);
            runWithoutSavingToolStripMenuItem.Enabled=(Storage.settingsData.CameraPCs.Count != 0);
        } 

	 	private void compareSequenceMenuItem_Click(object sender, EventArgs e)
	        {
	            try
	            {
	                SequenceData other = Storage.SaveAndLoad.LoadSequenceWithFileDialog();
	                List<SequenceComparer.SequenceDifference> differences = SequenceComparer.CompareSequences(
	                    Storage.sequenceData, other);
	                SequenceDifferencesForm frm = new SequenceDifferencesForm(differences);
	                frm.ShowDialog();
	            }
	            catch (Exception ex)
	            {
	                MessageBox.Show("Caught an exception when attempting to compare the sequences. Sequence comparison is still in its early stages. Please copy the following exception information and send it to the author. Don't worry, Cicero is not about to crash.");
	                ExceptionViewerDialog view = new ExceptionViewerDialog(ex);
	                view.ShowDialog();
	            }

	        }

        private void timestepGroupsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            timestepGroupsToolStripMenuItem.DropDownItems.Clear();
            foreach (TimestepGroup tsg in Storage.sequenceData.TimestepGroups)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                
                item.Tag = tsg;
                item.Checked = tsg.GroupEnabled;
                item.CheckOnClick = true;
                item.Text = tsg.TimestepGroupName;
                item.CheckedChanged += new EventHandler(timestepGroupItemCheckChanged);

                ToolStripTextBox nameBox = new ToolStripTextBox();
                nameBox.Tag = item;
                nameBox.Text = tsg.TimestepGroupName;
                nameBox.TextChanged += new EventHandler(nameEditBox_TextChanged);

                ToolStripMenuItem deleteGroup = new ToolStripMenuItem();
                deleteGroup.Text = "Delete group.";
                deleteGroup.Tag = tsg;
                deleteGroup.Click += new EventHandler(deleteTimestepGroupClick);

                ToolStripSeparator sep = new ToolStripSeparator();

                ToolStripMenuItem usageCount = new ToolStripMenuItem();
                int i=0;
                foreach (TimeStep step in Storage.sequenceData.TimeSteps)
                {
                    if (step.MyTimestepGroup == tsg)
                        i++;
                }
                usageCount.Text = i.ToString() +  " timestep(s) assigned to this group.";

                ToolStripMenuItem deleteTimestepsAndGroup = new ToolStripMenuItem();
                if (i == 0)
                    deleteTimestepsAndGroup.Enabled = false;
                deleteTimestepsAndGroup.Tag = tsg;
                deleteTimestepsAndGroup.Text = "Delete group and member timesteps.";
                deleteTimestepsAndGroup.Click += new EventHandler(deleteTimestepsAndGroup_Click);
                

                item.DropDownItems.Add(nameBox);
                item.DropDownItems.Add(deleteGroup);
                item.DropDownItems.Add(sep);
                item.DropDownItems.Add(usageCount);
                item.DropDownItems.Add(deleteTimestepsAndGroup);

                timestepGroupsToolStripMenuItem.DropDownItems.Add(item);
            }
            timestepGroupsToolStripMenuItem.DropDownItems.Add(timestepGroupMenuSeparator);
            timestepGroupsToolStripMenuItem.DropDownItems.Add(createNewTimestepGroupButton);
            timestepGroupsToolStripMenuItem.DropDownItems.Add(assignAllMarkedTimestepsToGroupToolStripMenuItem);
            if (markedTimesteps().Count == 0)
            {
                assignAllMarkedTimestepsToGroupToolStripMenuItem.Enabled = false;
                assignMarkedStepsToGroupComboBox.Enabled = false;
                assignToolStripMenuItemAssignButton.Enabled = false;
            }
            else
            {
                assignAllMarkedTimestepsToGroupToolStripMenuItem.Enabled = true;
                assignMarkedStepsToGroupComboBox.Enabled = true;
                assignToolStripMenuItemAssignButton.Enabled = true;
            }
            
        }

        void deleteTimestepsAndGroup_Click(object sender, EventArgs e)
        {
            bool proceed = (MessageBox.Show("This operation will delete both the selected timestep group, and all timesteps that are assigned to it. Are you sure you want to proceed?", "Delete group and its members?", MessageBoxButtons.YesNo)==DialogResult.Yes);
            if (proceed)
            {
                ToolStripMenuItem tsm = sender as ToolStripMenuItem;
                TimestepGroup tsg = tsm.Tag as TimestepGroup;
                if (tsg != null)
                {
                    List<TimeStep> removeList = new List<TimeStep>();
                    foreach (TimeStep step in Storage.sequenceData.TimeSteps)
                    {
                        if (step.MyTimestepGroup == tsg)
                        {
                            removeList.Add(step);
                        }
                    }

                    foreach (TimeStep step in removeList)
                    {
                        Storage.sequenceData.TimeSteps.Remove(step);
                    }

                    Storage.sequenceData.TimestepGroups.Remove(tsg);
                    RefreshSequenceDataToUI(Storage.sequenceData);
                    MessageBox.Show("Timestep group and member timesteps deleted.");
                }
                else
                {
                    MessageBox.Show("Error: null timestep group. Operation cancelled.");
                }
            }
            else
            {
                MessageBox.Show("Operation cancelled.");
            }

        }

        void deleteTimestepGroupClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this timestep group?", "Delete timestep group?", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            TimestepGroup tsg = tsmi.Tag as TimestepGroup;

            bool groupUsed = false;
            bool delAnyway = false;
            foreach (TimeStep step in Storage.sequenceData.TimeSteps)
            {
                if (step.MyTimestepGroup == tsg)
                {
                    groupUsed = true;
                    break;
                }
            }

            if (groupUsed)
            {
                delAnyway = (MessageBox.Show("The group you are about to delete is in use. Are you sure you want to delete it?", "Delete used timestep group?", MessageBoxButtons.YesNo) == DialogResult.Yes);
            }

            if ((!groupUsed) || delAnyway)
            {
                foreach (TimeStep step in Storage.sequenceData.TimeSteps)
                {
                    if (step.MyTimestepGroup == tsg)
                    {
                        step.MyTimestepGroup = null;
                    }
                }
                Storage.sequenceData.TimestepGroups.Remove(tsg);

                MessageBox.Show("Timestep group deleted.");
                sequencePage1.updateTimestepEditorsAfterSequenceModeOrTimestepGroupChange();
            }

        }

        void nameEditBox_TextChanged(object sender, EventArgs e)
        {
            ToolStripTextBox tstb = sender as ToolStripTextBox;
            ToolStripMenuItem tsmi = tstb.Tag as ToolStripMenuItem;
            TimestepGroup tsg = tsmi.Tag as TimestepGroup;
            tsg.TimestepGroupName = tstb.Text;
            tsmi.Text = tstb.Text;
        }

        void timestepGroupItemCheckChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = sender as ToolStripMenuItem;
            TimestepGroup tsg = tsm.Tag as TimestepGroup;
            tsg.GroupEnabled = tsm.Checked;
            this.sequencePage1.updateTimestepEditorsAfterSequenceModeOrTimestepGroupChange();
            
        }

        private void createNewTimestepGroupButton_Click(object sender, EventArgs e)
        {
            Storage.sequenceData.TimestepGroups.Add(new TimestepGroup("New timestep group"));
        }

        private void assignAllMarkedTimestepsToGroupToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            assignMarkedStepsToGroupComboBox.Items.Clear();
            assignMarkedStepsToGroupComboBox.Items.Add("None.");
            assignMarkedStepsToGroupComboBox.Items.AddRange(Storage.sequenceData.TimestepGroups.ToArray());
            assignMarkedStepsToGroupComboBox.SelectedItem = "None.";
        }

        private void assignToolStripMenuItemAssignButton_Click(object sender, EventArgs e)
        {
            TimestepGroup tsg = assignMarkedStepsToGroupComboBox.SelectedItem as TimestepGroup;
            List<TimeStep> markedsts = markedTimesteps();

            int i = 0;
            foreach (TimeStep step in markedsts)
            {
                step.MyTimestepGroup = tsg;
                i++;
            }
            if (tsg != null)
            {
                MessageBox.Show("Timestep group " + tsg.TimestepGroupName + " assigned to " + i + " marked timestep(s).");
            }
            else
            {
                MessageBox.Show("No Timestep group assigned to " + i + " marked timestep(s).");
            }
            sequencePage1.updateTimestepEditorsAfterSequenceModeOrTimestepGroupChange();
        }
    }
}
