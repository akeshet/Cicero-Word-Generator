namespace WordGenerator
{
    partial class MainClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainClientForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.settingsFileLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSequence = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.compareSequenceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveMarked = new System.Windows.Forms.ToolStripMenuItem();
            this.insertSequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDefaultSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDefaultSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runIteration0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runCurrentIterationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.continueListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runListInRandomOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runWithoutSavingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timestepGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.placeholderGroupClickerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timestepGroupMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.createNewTimestepGroupButton = new System.Windows.Forms.ToolStripMenuItem();
            this.assignAllMarkedTimestepsToGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assignMarkedStepsToGroupComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.assignToolStripMenuItemAssignButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editLogicalDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editServerManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.populateSequenceWithNewChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableDebugMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sequenceExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveClientStartupSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSequenceDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doNothingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inspectVariableTimebaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateVariableTimebaseAnalogBufferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateVariableTimebaseDigitalBufferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createBufferSnapshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showOnlyWarningsOrErrorsInEventLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openHomePageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openGitRepositoryPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variablesTab = new System.Windows.Forms.TabPage();
            this.variablesEditor = new WordGenerator.Controls.VariablesAndListPage();
            this.commonWaveformTab = new System.Windows.Forms.TabPage();
            this.commonWaveformEditor = new WordGenerator.Controls.CommonWaveformEditor();
            this.gpibTab = new System.Windows.Forms.TabPage();
            this.gpibGroupEditor = new WordGenerator.Controls.GpibGroupEditor();
            this.analogTab = new System.Windows.Forms.TabPage();
            this.analogGroupEditor = new WordGenerator.Controls.AnalogGroupEditor();
            this.sequenceTab = new System.Windows.Forms.TabPage();
            this.waitForReady = new System.Windows.Forms.CheckBox();
            this.lockDigitalCheckBox = new System.Windows.Forms.CheckBox();
            this.rs232GroupsLabel = new System.Windows.Forms.Label();
            this.gpibGroupsLabel = new System.Windows.Forms.Label();
            this.analogGroupsLabel = new System.Windows.Forms.Label();
            this.serverManagerButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.sequencePage = new WordGenerator.Controls.SequencePage();
            this.mainTab = new System.Windows.Forms.TabControl();
            this.overrideTab = new System.Windows.Forms.TabPage();
            this.overridePage = new WordGenerator.OverridePage();
            this.rs232Tab = new System.Windows.Forms.TabPage();
            this.rS232GroupEditor = new WordGenerator.Controls.Temporary.RS232GroupEditor();
            this.pulsesTab = new System.Windows.Forms.TabPage();
            this.pulsesPage = new WordGenerator.Controls.PulsesPage();
            this.eventLogTab = new System.Windows.Forms.TabPage();
            this.messageLogTextBox = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.useNetworkClockCheckBox = new System.Windows.Forms.CheckBox();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.variablesTab.SuspendLayout();
            this.commonWaveformTab.SuspendLayout();
            this.gpibTab.SuspendLayout();
            this.analogTab.SuspendLayout();
            this.sequenceTab.SuspendLayout();
            this.mainTab.SuspendLayout();
            this.overrideTab.SuspendLayout();
            this.rs232Tab.SuspendLayout();
            this.pulsesTab.SuspendLayout();
            this.eventLogTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsFileLabel,
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 860);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1272, 22);
            this.statusStrip.TabIndex = 1;
            // 
            // settingsFileLabel
            // 
            this.settingsFileLabel.Name = "settingsFileLabel";
            this.settingsFileLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(111, 17);
            this.toolStripStatusLabel.Text = "Welcome to Cicero!";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.runToolStripMenuItem,
            this.timestepGroupsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.advancedToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1272, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSequence,
            this.openToolStripMenuItem,
            this.recentFilesToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator3,
            this.compareSequenceMenuItem,
            this.toolStripSeparator2,
            this.saveMarked,
            this.insertSequenceToolStripMenuItem,
            this.toolStripMenuItem1,
            this.loadSettings,
            this.saveSettings,
            this.loadDefaultSettings,
            this.saveDefaultSettings,
            this.toolStripSeparator1,
            this.loadLogToolStripMenuItem,
            this.toolStripMenuItem4,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newSequence
            // 
            this.newSequence.Name = "newSequence";
            this.newSequence.Size = new System.Drawing.Size(204, 22);
            this.newSequence.Text = "&New Sequence";
            this.newSequence.Click += new System.EventHandler(this.newSequence_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.openToolStripMenuItem.Text = "&Load Sequence...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // recentFilesToolStripMenuItem
            // 
            this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.recentFilesToolStripMenuItem.Text = "Recent Sequence Files";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.saveAsToolStripMenuItem.Text = "&Save Sequence As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(201, 6);
            // 
            // compareSequenceMenuItem
            // 
            this.compareSequenceMenuItem.Name = "compareSequenceMenuItem";
            this.compareSequenceMenuItem.Size = new System.Drawing.Size(204, 22);
            this.compareSequenceMenuItem.Text = "Compare Sequences";
            this.compareSequenceMenuItem.Click += new System.EventHandler(this.compareSequenceMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(201, 6);
            // 
            // saveMarked
            // 
            this.saveMarked.Name = "saveMarked";
            this.saveMarked.Size = new System.Drawing.Size(204, 22);
            this.saveMarked.Text = "Save marked timesteps...";
            this.saveMarked.Click += new System.EventHandler(this.saveMarked_Click);
            // 
            // insertSequenceToolStripMenuItem
            // 
            this.insertSequenceToolStripMenuItem.Name = "insertSequenceToolStripMenuItem";
            this.insertSequenceToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.insertSequenceToolStripMenuItem.Text = "Insert Sequence...";
            this.insertSequenceToolStripMenuItem.Click += new System.EventHandler(this.insertSequenceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(201, 6);
            // 
            // loadSettings
            // 
            this.loadSettings.Name = "loadSettings";
            this.loadSettings.Size = new System.Drawing.Size(204, 22);
            this.loadSettings.Text = "Load S&ettings...";
            this.loadSettings.Click += new System.EventHandler(this.loadSettings_Click);
            // 
            // saveSettings
            // 
            this.saveSettings.Name = "saveSettings";
            this.saveSettings.Size = new System.Drawing.Size(204, 22);
            this.saveSettings.Text = "Save Settings As...";
            this.saveSettings.Click += new System.EventHandler(this.saveSettings_Click);
            // 
            // loadDefaultSettings
            // 
            this.loadDefaultSettings.Name = "loadDefaultSettings";
            this.loadDefaultSettings.Size = new System.Drawing.Size(204, 22);
            this.loadDefaultSettings.Text = "Load Default Se&ttings";
            this.loadDefaultSettings.Click += new System.EventHandler(this.loadDefaultSettings_Click);
            // 
            // saveDefaultSettings
            // 
            this.saveDefaultSettings.Name = "saveDefaultSettings";
            this.saveDefaultSettings.Size = new System.Drawing.Size(204, 22);
            this.saveDefaultSettings.Text = "Save Settings as &Default";
            this.saveDefaultSettings.Click += new System.EventHandler(this.saveDefaultSettings_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(201, 6);
            // 
            // loadLogToolStripMenuItem
            // 
            this.loadLogToolStripMenuItem.Name = "loadLogToolStripMenuItem";
            this.loadLogToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.loadLogToolStripMenuItem.Text = "Load Log...";
            this.loadLogToolStripMenuItem.Click += new System.EventHandler(this.loadLogToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(201, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runIteration0ToolStripMenuItem,
            this.runCurrentIterationToolStripMenuItem,
            this.runListToolStripMenuItem,
            this.continueListToolStripMenuItem,
            this.runListInRandomOrderToolStripMenuItem,
            this.runWithoutSavingToolStripMenuItem});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.runToolStripMenuItem.Text = "&Run";
            // 
            // runIteration0ToolStripMenuItem
            // 
            this.runIteration0ToolStripMenuItem.Name = "runIteration0ToolStripMenuItem";
            this.runIteration0ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.runIteration0ToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.runIteration0ToolStripMenuItem.Text = "Run Iteration 0";
            this.runIteration0ToolStripMenuItem.Click += new System.EventHandler(this.runIteration0ToolStripMenuItem_Click);
            // 
            // runCurrentIterationToolStripMenuItem
            // 
            this.runCurrentIterationToolStripMenuItem.Name = "runCurrentIterationToolStripMenuItem";
            this.runCurrentIterationToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.runCurrentIterationToolStripMenuItem.Text = "Run Current Iteration";
            this.runCurrentIterationToolStripMenuItem.Click += new System.EventHandler(this.runCurrentIterationToolStripMenuItem_Click);
            // 
            // runListToolStripMenuItem
            // 
            this.runListToolStripMenuItem.Name = "runListToolStripMenuItem";
            this.runListToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.runListToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.runListToolStripMenuItem.Text = "Run List";
            this.runListToolStripMenuItem.Click += new System.EventHandler(this.runListToolStripMenuItem_Click);
            // 
            // continueListToolStripMenuItem
            // 
            this.continueListToolStripMenuItem.Name = "continueListToolStripMenuItem";
            this.continueListToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.continueListToolStripMenuItem.Text = "Continue List";
            this.continueListToolStripMenuItem.Click += new System.EventHandler(this.continueListToolStripMenuItem_Click);
            // 
            // runListInRandomOrderToolStripMenuItem
            // 
            this.runListInRandomOrderToolStripMenuItem.Name = "runListInRandomOrderToolStripMenuItem";
            this.runListInRandomOrderToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.runListInRandomOrderToolStripMenuItem.Text = "Run List in Random Order";
            this.runListInRandomOrderToolStripMenuItem.Click += new System.EventHandler(this.runListInRandomOrderToolStripMenuItem_Click);
            // 
            // runWithoutSavingToolStripMenuItem
            // 
            this.runWithoutSavingToolStripMenuItem.Name = "runWithoutSavingToolStripMenuItem";
            this.runWithoutSavingToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.runWithoutSavingToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.runWithoutSavingToolStripMenuItem.Text = "Run Without Saving";
            this.runWithoutSavingToolStripMenuItem.Click += new System.EventHandler(this.runWithoutSavingToolStripMenuItem_Click);
            // 
            // timestepGroupsToolStripMenuItem
            // 
            this.timestepGroupsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.placeholderGroupClickerToolStripMenuItem,
            this.timestepGroupMenuSeparator,
            this.createNewTimestepGroupButton,
            this.assignAllMarkedTimestepsToGroupToolStripMenuItem});
            this.timestepGroupsToolStripMenuItem.Name = "timestepGroupsToolStripMenuItem";
            this.timestepGroupsToolStripMenuItem.Size = new System.Drawing.Size(109, 20);
            this.timestepGroupsToolStripMenuItem.Text = "Timestep &Groups";
            this.timestepGroupsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.timestepGroupsToolStripMenuItem_DropDownOpening);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox1.Visible = false;
            // 
            // placeholderGroupClickerToolStripMenuItem
            // 
            this.placeholderGroupClickerToolStripMenuItem.Checked = true;
            this.placeholderGroupClickerToolStripMenuItem.CheckOnClick = true;
            this.placeholderGroupClickerToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.placeholderGroupClickerToolStripMenuItem.Name = "placeholderGroupClickerToolStripMenuItem";
            this.placeholderGroupClickerToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.placeholderGroupClickerToolStripMenuItem.Text = "Placeholder Group Clicker";
            this.placeholderGroupClickerToolStripMenuItem.Visible = false;
            // 
            // timestepGroupMenuSeparator
            // 
            this.timestepGroupMenuSeparator.Name = "timestepGroupMenuSeparator";
            this.timestepGroupMenuSeparator.Size = new System.Drawing.Size(267, 6);
            // 
            // createNewTimestepGroupButton
            // 
            this.createNewTimestepGroupButton.Name = "createNewTimestepGroupButton";
            this.createNewTimestepGroupButton.Size = new System.Drawing.Size(270, 22);
            this.createNewTimestepGroupButton.Text = "Create New Group";
            this.createNewTimestepGroupButton.Click += new System.EventHandler(this.createNewTimestepGroupButton_Click);
            // 
            // assignAllMarkedTimestepsToGroupToolStripMenuItem
            // 
            this.assignAllMarkedTimestepsToGroupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assignMarkedStepsToGroupComboBox,
            this.assignToolStripMenuItemAssignButton});
            this.assignAllMarkedTimestepsToGroupToolStripMenuItem.Name = "assignAllMarkedTimestepsToGroupToolStripMenuItem";
            this.assignAllMarkedTimestepsToGroupToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.assignAllMarkedTimestepsToGroupToolStripMenuItem.Text = "Assign all marked timesteps to group";
            this.assignAllMarkedTimestepsToGroupToolStripMenuItem.DropDownOpening += new System.EventHandler(this.assignAllMarkedTimestepsToGroupToolStripMenuItem_DropDownOpening);
            // 
            // assignMarkedStepsToGroupComboBox
            // 
            this.assignMarkedStepsToGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.assignMarkedStepsToGroupComboBox.Name = "assignMarkedStepsToGroupComboBox";
            this.assignMarkedStepsToGroupComboBox.Size = new System.Drawing.Size(121, 23);
            // 
            // assignToolStripMenuItemAssignButton
            // 
            this.assignToolStripMenuItemAssignButton.Name = "assignToolStripMenuItemAssignButton";
            this.assignToolStripMenuItemAssignButton.Size = new System.Drawing.Size(188, 22);
            this.assignToolStripMenuItemAssignButton.Text = "Click here to confirm.";
            this.assignToolStripMenuItemAssignButton.Click += new System.EventHandler(this.assignToolStripMenuItemAssignButton_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editLogicalDevicesToolStripMenuItem,
            this.editServerManagerToolStripMenuItem,
            this.populateSequenceWithNewChannelsToolStripMenuItem,
            this.enableDebugMenuToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // editLogicalDevicesToolStripMenuItem
            // 
            this.editLogicalDevicesToolStripMenuItem.Name = "editLogicalDevicesToolStripMenuItem";
            this.editLogicalDevicesToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.editLogicalDevicesToolStripMenuItem.Text = "&Channel Manager";
            this.editLogicalDevicesToolStripMenuItem.Click += new System.EventHandler(this.editLogicalDevicesToolStripMenuItem_Click);
            // 
            // editServerManagerToolStripMenuItem
            // 
            this.editServerManagerToolStripMenuItem.Name = "editServerManagerToolStripMenuItem";
            this.editServerManagerToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.editServerManagerToolStripMenuItem.Text = "&Server Manager (F11)";
            this.editServerManagerToolStripMenuItem.Click += new System.EventHandler(this.editServerManagerToolStripMenuItem_Click);
            // 
            // populateSequenceWithNewChannelsToolStripMenuItem
            // 
            this.populateSequenceWithNewChannelsToolStripMenuItem.Name = "populateSequenceWithNewChannelsToolStripMenuItem";
            this.populateSequenceWithNewChannelsToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.populateSequenceWithNewChannelsToolStripMenuItem.Text = "&Populate Sequence";
            this.populateSequenceWithNewChannelsToolStripMenuItem.ToolTipText = "Click here to update SequenceData object to contain all of the channels in the pr" +
                "esent SettingsData. NOTE: Once this is done, Sequence files will no longer be co" +
                "mpatible with old settings files.\r\n";
            this.populateSequenceWithNewChannelsToolStripMenuItem.Click += new System.EventHandler(this.populateSequenceWithNewChannelsToolStripMenuItem_Click);
            // 
            // enableDebugMenuToolStripMenuItem
            // 
            this.enableDebugMenuToolStripMenuItem.Name = "enableDebugMenuToolStripMenuItem";
            this.enableDebugMenuToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.enableDebugMenuToolStripMenuItem.Text = "Enable Debug Menu";
            this.enableDebugMenuToolStripMenuItem.Click += new System.EventHandler(this.enableDebugMenuToolStripMenuItem_Click);
            // 
            // advancedToolStripMenuItem
            // 
            this.advancedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sequenceExplorerToolStripMenuItem,
            this.settingsExplorerToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            this.advancedToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.advancedToolStripMenuItem.Text = "&Advanced";
            // 
            // sequenceExplorerToolStripMenuItem
            // 
            this.sequenceExplorerToolStripMenuItem.Name = "sequenceExplorerToolStripMenuItem";
            this.sequenceExplorerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sequenceExplorerToolStripMenuItem.Text = "&Sequence Explorer";
            this.sequenceExplorerToolStripMenuItem.Click += new System.EventHandler(this.sequenceExplorerToolStripMenuItem_Click);
            // 
            // settingsExplorerToolStripMenuItem
            // 
            this.settingsExplorerToolStripMenuItem.Name = "settingsExplorerToolStripMenuItem";
            this.settingsExplorerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.settingsExplorerToolStripMenuItem.Text = "Se&ttings Explorer";
            this.settingsExplorerToolStripMenuItem.Click += new System.EventHandler(this.settingsExplorerToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAllToolStripMenuItem,
            this.toolStripMenuItem2,
            this.saveClientStartupSettingsToolStripMenuItem,
            this.saveSettingsDataToolStripMenuItem,
            this.saveSequenceDataToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "&Application Settings";
            this.saveToolStripMenuItem.Visible = false;
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.saveAllToolStripMenuItem.Text = "Save &All";
            this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.saveAllToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(209, 6);
            // 
            // saveClientStartupSettingsToolStripMenuItem
            // 
            this.saveClientStartupSettingsToolStripMenuItem.Name = "saveClientStartupSettingsToolStripMenuItem";
            this.saveClientStartupSettingsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.saveClientStartupSettingsToolStripMenuItem.Text = "Save ClientStartupSettings";
            this.saveClientStartupSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveClientStartupSettingsToolStripMenuItem_Click);
            // 
            // saveSettingsDataToolStripMenuItem
            // 
            this.saveSettingsDataToolStripMenuItem.Name = "saveSettingsDataToolStripMenuItem";
            this.saveSettingsDataToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.saveSettingsDataToolStripMenuItem.Text = "Save SettingsData";
            this.saveSettingsDataToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsDataToolStripMenuItem_Click);
            // 
            // saveSequenceDataToolStripMenuItem
            // 
            this.saveSequenceDataToolStripMenuItem.Name = "saveSequenceDataToolStripMenuItem";
            this.saveSequenceDataToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.saveSequenceDataToolStripMenuItem.Text = "Save SequenceData";
            this.saveSequenceDataToolStripMenuItem.Click += new System.EventHandler(this.saveSequenceDataToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.doNothingToolStripMenuItem,
            this.inspectVariableTimebaseToolStripMenuItem,
            this.calculateVariableTimebaseAnalogBufferToolStripMenuItem,
            this.calculateVariableTimebaseDigitalBufferToolStripMenuItem,
            this.stToolStripMenuItem,
            this.createBufferSnapshotToolStripMenuItem,
            this.showOnlyWarningsOrErrorsInEventLogToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Visible = false;
            // 
            // doNothingToolStripMenuItem
            // 
            this.doNothingToolStripMenuItem.Name = "doNothingToolStripMenuItem";
            this.doNothingToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.doNothingToolStripMenuItem.Text = "Do nothing";
            this.doNothingToolStripMenuItem.Click += new System.EventHandler(this.doNothingToolStripMenuItem_Click);
            // 
            // inspectVariableTimebaseToolStripMenuItem
            // 
            this.inspectVariableTimebaseToolStripMenuItem.Name = "inspectVariableTimebaseToolStripMenuItem";
            this.inspectVariableTimebaseToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.inspectVariableTimebaseToolStripMenuItem.Text = "Inspect Variable Timebase";
            this.inspectVariableTimebaseToolStripMenuItem.Click += new System.EventHandler(this.inspectVariableTimebaseToolStripMenuItem_Click);
            // 
            // calculateVariableTimebaseAnalogBufferToolStripMenuItem
            // 
            this.calculateVariableTimebaseAnalogBufferToolStripMenuItem.Name = "calculateVariableTimebaseAnalogBufferToolStripMenuItem";
            this.calculateVariableTimebaseAnalogBufferToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.calculateVariableTimebaseAnalogBufferToolStripMenuItem.Text = "Calculate variable timebase analog buffer";
            this.calculateVariableTimebaseAnalogBufferToolStripMenuItem.Click += new System.EventHandler(this.calculateVariableTimebaseAnalogBufferToolStripMenuItem_Click);
            // 
            // calculateVariableTimebaseDigitalBufferToolStripMenuItem
            // 
            this.calculateVariableTimebaseDigitalBufferToolStripMenuItem.Name = "calculateVariableTimebaseDigitalBufferToolStripMenuItem";
            this.calculateVariableTimebaseDigitalBufferToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.calculateVariableTimebaseDigitalBufferToolStripMenuItem.Text = "Calculate variable timebase digital buffer";
            this.calculateVariableTimebaseDigitalBufferToolStripMenuItem.Click += new System.EventHandler(this.calculateVariableTimebaseDigitalBufferToolStripMenuItem_Click);
            // 
            // stToolStripMenuItem
            // 
            this.stToolStripMenuItem.Name = "stToolStripMenuItem";
            this.stToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.stToolStripMenuItem.Text = "Student Edition";
            this.stToolStripMenuItem.Visible = false;
            this.stToolStripMenuItem.Click += new System.EventHandler(this.stToolStripMenuItem_Click);
            // 
            // createBufferSnapshotToolStripMenuItem
            // 
            this.createBufferSnapshotToolStripMenuItem.Name = "createBufferSnapshotToolStripMenuItem";
            this.createBufferSnapshotToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.createBufferSnapshotToolStripMenuItem.Text = "Create buffer snapshot";
            this.createBufferSnapshotToolStripMenuItem.Click += new System.EventHandler(this.createBufferSnapshotToolStripMenuItem_Click);
            // 
            // showOnlyWarningsOrErrorsInEventLogToolStripMenuItem
            // 
            this.showOnlyWarningsOrErrorsInEventLogToolStripMenuItem.CheckOnClick = true;
            this.showOnlyWarningsOrErrorsInEventLogToolStripMenuItem.Name = "showOnlyWarningsOrErrorsInEventLogToolStripMenuItem";
            this.showOnlyWarningsOrErrorsInEventLogToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.showOnlyWarningsOrErrorsInEventLogToolStripMenuItem.Text = "Show only Warnings or Errors in Event Log";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1,
            this.licenseToolStripMenuItem,
            this.openHomePageToolStripMenuItem,
            this.openGitRepositoryPageToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(209, 22);
            this.aboutToolStripMenuItem1.Text = "Splash Screen";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // licenseToolStripMenuItem
            // 
            this.licenseToolStripMenuItem.Name = "licenseToolStripMenuItem";
            this.licenseToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.licenseToolStripMenuItem.Text = "License";
            this.licenseToolStripMenuItem.Click += new System.EventHandler(this.licenseToolStripMenuItem_Click);
            // 
            // openHomePageToolStripMenuItem
            // 
            this.openHomePageToolStripMenuItem.Name = "openHomePageToolStripMenuItem";
            this.openHomePageToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.openHomePageToolStripMenuItem.Text = "Open Home Page";
            this.openHomePageToolStripMenuItem.Click += new System.EventHandler(this.openHomePageToolStripMenuItem_Click);
            // 
            // openGitRepositoryPageToolStripMenuItem
            // 
            this.openGitRepositoryPageToolStripMenuItem.Name = "openGitRepositoryPageToolStripMenuItem";
            this.openGitRepositoryPageToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.openGitRepositoryPageToolStripMenuItem.Text = "Open Git Repository Page";
            this.openGitRepositoryPageToolStripMenuItem.Click += new System.EventHandler(this.openGitRepositoryPageToolStripMenuItem_Click);
            // 
            // variablesTab
            // 
            this.variablesTab.Controls.Add(this.variablesEditor);
            this.variablesTab.Location = new System.Drawing.Point(4, 22);
            this.variablesTab.Name = "variablesTab";
            this.variablesTab.Padding = new System.Windows.Forms.Padding(3);
            this.variablesTab.Size = new System.Drawing.Size(1264, 810);
            this.variablesTab.TabIndex = 4;
            this.variablesTab.Text = "Variables (F7)";
            this.variablesTab.UseVisualStyleBackColor = true;
            // 
            // variablesEditor
            // 
            this.variablesEditor.Location = new System.Drawing.Point(0, 0);
            this.variablesEditor.Name = "variablesEditor";
            this.variablesEditor.Size = new System.Drawing.Size(1264, 918);
            this.variablesEditor.TabIndex = 0;
            // 
            // commonWaveformTab
            // 
            this.commonWaveformTab.Controls.Add(this.commonWaveformEditor);
            this.commonWaveformTab.Location = new System.Drawing.Point(4, 22);
            this.commonWaveformTab.Name = "commonWaveformTab";
            this.commonWaveformTab.Padding = new System.Windows.Forms.Padding(3);
            this.commonWaveformTab.Size = new System.Drawing.Size(1264, 810);
            this.commonWaveformTab.TabIndex = 3;
            this.commonWaveformTab.Text = "Common Waveform (F6)";
            this.commonWaveformTab.UseVisualStyleBackColor = true;
            // 
            // commonWaveformEditor
            // 
            this.commonWaveformEditor.Location = new System.Drawing.Point(3, 3);
            this.commonWaveformEditor.Name = "commonWaveformEditor";
            this.commonWaveformEditor.Size = new System.Drawing.Size(1252, 844);
            this.commonWaveformEditor.TabIndex = 0;
            // 
            // gpibTab
            // 
            this.gpibTab.Controls.Add(this.gpibGroupEditor);
            this.gpibTab.Location = new System.Drawing.Point(4, 22);
            this.gpibTab.Name = "gpibTab";
            this.gpibTab.Padding = new System.Windows.Forms.Padding(3);
            this.gpibTab.Size = new System.Drawing.Size(1264, 810);
            this.gpibTab.TabIndex = 2;
            this.gpibTab.Text = "GPIB (F4)";
            this.gpibTab.UseVisualStyleBackColor = true;
            // 
            // gpibGroupEditor
            // 
            this.gpibGroupEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpibGroupEditor.Location = new System.Drawing.Point(3, 3);
            this.gpibGroupEditor.Name = "gpibGroupEditor";
            this.gpibGroupEditor.Size = new System.Drawing.Size(1258, 890);
            this.gpibGroupEditor.TabIndex = 0;
            // 
            // analogTab
            // 
            this.analogTab.Controls.Add(this.analogGroupEditor);
            this.analogTab.Location = new System.Drawing.Point(4, 22);
            this.analogTab.Name = "analogTab";
            this.analogTab.Padding = new System.Windows.Forms.Padding(3);
            this.analogTab.Size = new System.Drawing.Size(1264, 810);
            this.analogTab.TabIndex = 1;
            this.analogTab.Text = "Analog (F3)";
            this.analogTab.UseVisualStyleBackColor = true;
            // 
            // analogGroupEditor
            // 
            this.analogGroupEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analogGroupEditor.Location = new System.Drawing.Point(3, 3);
            this.analogGroupEditor.Name = "analogGroupEditor";
            this.analogGroupEditor.Size = new System.Drawing.Size(1258, 890);
            this.analogGroupEditor.TabIndex = 0;
            // 
            // sequenceTab
            // 
            this.sequenceTab.Controls.Add(this.useNetworkClockCheckBox);
            this.sequenceTab.Controls.Add(this.waitForReady);
            this.sequenceTab.Controls.Add(this.lockDigitalCheckBox);
            this.sequenceTab.Controls.Add(this.rs232GroupsLabel);
            this.sequenceTab.Controls.Add(this.gpibGroupsLabel);
            this.sequenceTab.Controls.Add(this.analogGroupsLabel);
            this.sequenceTab.Controls.Add(this.serverManagerButton);
            this.sequenceTab.Controls.Add(this.button2);
            this.sequenceTab.Controls.Add(this.sequencePage);
            this.sequenceTab.Location = new System.Drawing.Point(4, 22);
            this.sequenceTab.Name = "sequenceTab";
            this.sequenceTab.Padding = new System.Windows.Forms.Padding(3);
            this.sequenceTab.Size = new System.Drawing.Size(1264, 810);
            this.sequenceTab.TabIndex = 0;
            this.sequenceTab.Text = "Sequence (F1)";
            this.sequenceTab.ToolTipText = "Testing";
            this.sequenceTab.UseVisualStyleBackColor = true;
            // 
            // waitForReady
            // 
            this.waitForReady.AutoSize = true;
            this.waitForReady.Location = new System.Drawing.Point(7, 747);
            this.waitForReady.Name = "waitForReady";
            this.waitForReady.Size = new System.Drawing.Size(97, 17);
            this.waitForReady.TabIndex = 9;
            this.waitForReady.Text = "Wait for Ready";
            this.waitForReady.UseVisualStyleBackColor = true;
            this.waitForReady.CheckedChanged += new System.EventHandler(this.waitForReady_CheckedChanged);
            // 
            // lockDigitalCheckBox
            // 
            this.lockDigitalCheckBox.AutoSize = true;
            this.lockDigitalCheckBox.Location = new System.Drawing.Point(7, 726);
            this.lockDigitalCheckBox.Name = "lockDigitalCheckBox";
            this.lockDigitalCheckBox.Size = new System.Drawing.Size(112, 17);
            this.lockDigitalCheckBox.TabIndex = 8;
            this.lockDigitalCheckBox.Text = "Lock Digital Panel";
            this.lockDigitalCheckBox.UseVisualStyleBackColor = true;
            // 
            // rs232GroupsLabel
            // 
            this.rs232GroupsLabel.AutoSize = true;
            this.rs232GroupsLabel.Location = new System.Drawing.Point(178, 200);
            this.rs232GroupsLabel.Name = "rs232GroupsLabel";
            this.rs232GroupsLabel.Size = new System.Drawing.Size(75, 13);
            this.rs232GroupsLabel.TabIndex = 7;
            this.rs232GroupsLabel.Text = "RS232 Group:";
            // 
            // gpibGroupsLabel
            // 
            this.gpibGroupsLabel.AutoSize = true;
            this.gpibGroupsLabel.Location = new System.Drawing.Point(186, 174);
            this.gpibGroupsLabel.Name = "gpibGroupsLabel";
            this.gpibGroupsLabel.Size = new System.Drawing.Size(67, 13);
            this.gpibGroupsLabel.TabIndex = 6;
            this.gpibGroupsLabel.Text = "GPIB Group:";
            // 
            // analogGroupsLabel
            // 
            this.analogGroupsLabel.AutoSize = true;
            this.analogGroupsLabel.Location = new System.Drawing.Point(179, 148);
            this.analogGroupsLabel.Name = "analogGroupsLabel";
            this.analogGroupsLabel.Size = new System.Drawing.Size(75, 13);
            this.analogGroupsLabel.TabIndex = 5;
            this.analogGroupsLabel.Text = "Analog Group:";
            // 
            // serverManagerButton
            // 
            this.serverManagerButton.Location = new System.Drawing.Point(3, 685);
            this.serverManagerButton.Name = "serverManagerButton";
            this.serverManagerButton.Size = new System.Drawing.Size(116, 35);
            this.serverManagerButton.TabIndex = 3;
            this.serverManagerButton.Text = "Server Manager (F11)";
            this.serverManagerButton.UseVisualStyleBackColor = true;
            this.serverManagerButton.Click += new System.EventHandler(this.editServerManagerToolStripMenuItem_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 646);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 33);
            this.button2.TabIndex = 2;
            this.button2.Text = "Channel Manager";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.editLogicalDevicesToolStripMenuItem_Click);
            // 
            // sequencePage
            // 
            this.sequencePage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sequencePage.Location = new System.Drawing.Point(3, 3);
            this.sequencePage.Name = "sequencePage";
            this.sequencePage.Size = new System.Drawing.Size(1258, 804);
            this.sequencePage.TabIndex = 0;
            this.sequencePage.messageLog += new System.EventHandler<DataStructures.MessageEvent> (this.handleMessageEvent);
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.sequenceTab);
            this.mainTab.Controls.Add(this.overrideTab);
            this.mainTab.Controls.Add(this.analogTab);
            this.mainTab.Controls.Add(this.gpibTab);
            this.mainTab.Controls.Add(this.rs232Tab);
            this.mainTab.Controls.Add(this.commonWaveformTab);
            this.mainTab.Controls.Add(this.variablesTab);
            this.mainTab.Controls.Add(this.pulsesTab);
            this.mainTab.Controls.Add(this.eventLogTab);
            this.mainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTab.Location = new System.Drawing.Point(0, 24);
            this.mainTab.Name = "mainTab";
            this.mainTab.SelectedIndex = 0;
            this.mainTab.Size = new System.Drawing.Size(1272, 836);
            this.mainTab.TabIndex = 2;
            // 
            // overrideTab
            // 
            this.overrideTab.Controls.Add(this.overridePage);
            this.overrideTab.Location = new System.Drawing.Point(4, 22);
            this.overrideTab.Name = "overrideTab";
            this.overrideTab.Size = new System.Drawing.Size(1264, 810);
            this.overrideTab.TabIndex = 7;
            this.overrideTab.Text = "Override (F2)";
            this.overrideTab.UseVisualStyleBackColor = true;
            // 
            // overridePage
            // 
            this.overridePage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.overridePage.Location = new System.Drawing.Point(0, 0);
            this.overridePage.Name = "overridePage";
            this.overridePage.Size = new System.Drawing.Size(1264, 896);
            this.overridePage.TabIndex = 0;
            // 
            // rs232Tab
            // 
            this.rs232Tab.Controls.Add(this.rS232GroupEditor);
            this.rs232Tab.Location = new System.Drawing.Point(4, 22);
            this.rs232Tab.Name = "rs232Tab";
            this.rs232Tab.Size = new System.Drawing.Size(1264, 810);
            this.rs232Tab.TabIndex = 5;
            this.rs232Tab.Text = "RS232 (F5)";
            this.rs232Tab.UseVisualStyleBackColor = true;
            // 
            // rS232GroupEditor
            // 
            this.rS232GroupEditor.Location = new System.Drawing.Point(3, 3);
            this.rS232GroupEditor.Name = "rS232GroupEditor";
            this.rS232GroupEditor.Size = new System.Drawing.Size(1264, 918);
            this.rS232GroupEditor.TabIndex = 0;
            // 
            // pulsesTab
            // 
            this.pulsesTab.Controls.Add(this.pulsesPage);
            this.pulsesTab.Location = new System.Drawing.Point(4, 22);
            this.pulsesTab.Name = "pulsesTab";
            this.pulsesTab.Size = new System.Drawing.Size(1264, 810);
            this.pulsesTab.TabIndex = 8;
            this.pulsesTab.Text = "Pulses (F8)";
            this.pulsesTab.UseVisualStyleBackColor = true;
            // 
            // pulsesPage
            // 
            this.pulsesPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pulsesPage.Location = new System.Drawing.Point(0, 0);
            this.pulsesPage.Name = "pulsesPage";
            this.pulsesPage.Size = new System.Drawing.Size(1264, 896);
            this.pulsesPage.TabIndex = 0;
            // 
            // eventLogTab
            // 
            this.eventLogTab.Controls.Add(this.messageLogTextBox);
            this.eventLogTab.Location = new System.Drawing.Point(4, 22);
            this.eventLogTab.Name = "eventLogTab";
            this.eventLogTab.Size = new System.Drawing.Size(1264, 810);
            this.eventLogTab.TabIndex = 6;
            this.eventLogTab.Text = "Event Log";
            this.eventLogTab.UseVisualStyleBackColor = true;
            // 
            // messageLogTextBox
            // 
            this.messageLogTextBox.Location = new System.Drawing.Point(12, 11);
            this.messageLogTextBox.Multiline = true;
            this.messageLogTextBox.Name = "messageLogTextBox";
            this.messageLogTextBox.ReadOnly = true;
            this.messageLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageLogTextBox.Size = new System.Drawing.Size(1244, 885);
            this.messageLogTextBox.TabIndex = 0;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 0;
            this.toolTip1.UseAnimation = false;
            this.toolTip1.UseFading = false;
            // 
            // useNetworkClockCheckBox
            // 
            this.useNetworkClockCheckBox.AutoSize = true;
            this.useNetworkClockCheckBox.Location = new System.Drawing.Point(7, 768);
            this.useNetworkClockCheckBox.Name = "useNetworkClockCheckBox";
            this.useNetworkClockCheckBox.Size = new System.Drawing.Size(118, 17);
            this.useNetworkClockCheckBox.TabIndex = 10;
            this.useNetworkClockCheckBox.Text = "Use Network Clock";
            this.useNetworkClockCheckBox.UseVisualStyleBackColor = true;
            this.useNetworkClockCheckBox.CheckedChanged += new System.EventHandler(this.useNetworkClockCheckbox_CheckedChanged);
            // 
            // MainClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 882);
            this.Controls.Add(this.mainTab);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(10000, 10000);
            this.Name = "MainClientForm";
            this.Text = "Cicero Word Generator 1.0 Beta";
            this.Activated += new System.EventHandler(this.mainClientForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainClientForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainClientForm_FormClosed);
            this.Load += new System.EventHandler(this.mainClientForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.variablesTab.ResumeLayout(false);
            this.commonWaveformTab.ResumeLayout(false);
            this.gpibTab.ResumeLayout(false);
            this.analogTab.ResumeLayout(false);
            this.sequenceTab.ResumeLayout(false);
            this.sequenceTab.PerformLayout();
            this.mainTab.ResumeLayout(false);
            this.overrideTab.ResumeLayout(false);
            this.rs232Tab.ResumeLayout(false);
            this.pulsesTab.ResumeLayout(false);
            this.eventLogTab.ResumeLayout(false);
            this.eventLogTab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem saveClientStartupSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSequenceDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editLogicalDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sequenceExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem populateSequenceWithNewChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editServerManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSettings;
        private System.Windows.Forms.ToolStripMenuItem saveSettings;
        private System.Windows.Forms.ToolStripMenuItem loadDefaultSettings;
        private System.Windows.Forms.ToolStripMenuItem saveDefaultSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabPage variablesTab;
        private System.Windows.Forms.TabPage commonWaveformTab;
        private System.Windows.Forms.TabPage gpibTab;
        private WordGenerator.Controls.GpibGroupEditor gpibGroupEditor;
        private System.Windows.Forms.TabPage analogTab;
        private System.Windows.Forms.TabPage sequenceTab;
        public WordGenerator.Controls.SequencePage sequencePage;
        private System.Windows.Forms.TabControl mainTab;
        private System.Windows.Forms.TabPage rs232Tab;
        private WordGenerator.Controls.Temporary.RS232GroupEditor rS232GroupEditor;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.TabPage eventLogTab;
        private System.Windows.Forms.TextBox messageLogTextBox;
        private System.Windows.Forms.ToolStripMenuItem newSequence;
        private System.Windows.Forms.Button serverManagerButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabPage overrideTab;
        private OverridePage overridePage;
        private System.Windows.Forms.ToolStripMenuItem inspectVariableTimebaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculateVariableTimebaseAnalogBufferToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculateVariableTimebaseDigitalBufferToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runIteration0ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runCurrentIterationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem continueListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runListInRandomOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel settingsFileLabel;
        private System.Windows.Forms.TabPage pulsesTab;
        private System.Windows.Forms.Label rs232GroupsLabel;
        private System.Windows.Forms.Label gpibGroupsLabel;
        private System.Windows.Forms.Label analogGroupsLabel;
        public WordGenerator.Controls.AnalogGroupEditor analogGroupEditor;
        public WordGenerator.Controls.PulsesPage pulsesPage;
        public WordGenerator.Controls.VariablesAndListPage variablesEditor;
        public WordGenerator.Controls.CommonWaveformEditor commonWaveformEditor;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem licenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveMarked;
        private System.Windows.Forms.ToolStripMenuItem insertSequenceToolStripMenuItem;
        public System.Windows.Forms.CheckBox lockDigitalCheckBox;
        private System.Windows.Forms.CheckBox waitForReady;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem compareSequenceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem runWithoutSavingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timestepGroupsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem placeholderGroupClickerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator timestepGroupMenuSeparator;
        private System.Windows.Forms.ToolStripMenuItem createNewTimestepGroupButton;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem assignAllMarkedTimestepsToGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox assignMarkedStepsToGroupComboBox;
        private System.Windows.Forms.ToolStripMenuItem assignToolStripMenuItemAssignButton;
        private System.Windows.Forms.ToolStripMenuItem stToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doNothingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createBufferSnapshotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableDebugMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openHomePageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openGitRepositoryPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showOnlyWarningsOrErrorsInEventLogToolStripMenuItem;
        private System.Windows.Forms.CheckBox useNetworkClockCheckBox;

    }
}

