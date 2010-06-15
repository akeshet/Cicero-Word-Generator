namespace WordGenerator
{
    partial class mainClientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainClientForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.settingsFileLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sequenceExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveClientStartupSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSequenceDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inspectVariableTimebaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateVariableTimebaseAnalogBufferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateVariableTimebaseDigitalBufferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variablesPage = new System.Windows.Forms.TabPage();
            this.variablesEditor1 = new WordGenerator.Controls.VariablesAndListPage();
            this.commonWaveformPage = new System.Windows.Forms.TabPage();
            this.commonWaveformEditor1 = new WordGenerator.Controls.CommonWaveformEditor();
            this.gpibPage = new System.Windows.Forms.TabPage();
            this.gpibGroupEditor1 = new WordGenerator.Controls.GpibGroupEditor();
            this.analogPage = new System.Windows.Forms.TabPage();
            this.analogGroupEditor1 = new WordGenerator.Controls.AnalogGroupEditor();
            this.sequencePage = new System.Windows.Forms.TabPage();
            this.waitForReady = new System.Windows.Forms.CheckBox();
            this.lockDigitalCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.serverManagerButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.sequencePage1 = new WordGenerator.Controls.SequencePage();
            this.mainTab = new System.Windows.Forms.TabControl();
            this.overrideTab = new System.Windows.Forms.TabPage();
            this.overridePage1 = new WordGenerator.OverridePage();
            this.rs232Tab = new System.Windows.Forms.TabPage();
            this.rS232GroupEditor1 = new WordGenerator.Controls.Temporary.RS232GroupEditor();
            this.pulsesTab = new System.Windows.Forms.TabPage();
            this.pulsesPage1 = new WordGenerator.Controls.PulsesPage();
            this.eventLogPage = new System.Windows.Forms.TabPage();
            this.messageLogTextBox = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.variablesPage.SuspendLayout();
            this.commonWaveformPage.SuspendLayout();
            this.gpibPage.SuspendLayout();
            this.analogPage.SuspendLayout();
            this.sequencePage.SuspendLayout();
            this.mainTab.SuspendLayout();
            this.overrideTab.SuspendLayout();
            this.rs232Tab.SuspendLayout();
            this.pulsesTab.SuspendLayout();
            this.eventLogPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsFileLabel,
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 946);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1272, 22);
            this.statusStrip.TabIndex = 1;
            // 
            // settingsFileLabel
            // 
            this.settingsFileLabel.Name = "settingsFileLabel";
            this.settingsFileLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(100, 17);
            this.toolStripStatusLabel1.Text = "Welcome to Cicero!";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.runToolStripMenuItem,
            this.timestepGroupsToolStripMenuItem,
            this.toolsToolStripMenuItem,
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newSequence
            // 
            this.newSequence.Name = "newSequence";
            this.newSequence.Size = new System.Drawing.Size(208, 22);
            this.newSequence.Text = "&New Sequence";
            this.newSequence.Click += new System.EventHandler(this.newSequence_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.openToolStripMenuItem.Text = "&Load Sequence...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // recentFilesToolStripMenuItem
            // 
            this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.recentFilesToolStripMenuItem.Text = "Recent Sequence Files";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.saveAsToolStripMenuItem.Text = "&Save Sequence As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(205, 6);
            // 
            // compareSequenceMenuItem
            // 
            this.compareSequenceMenuItem.Name = "compareSequenceMenuItem";
            this.compareSequenceMenuItem.Size = new System.Drawing.Size(208, 22);
            this.compareSequenceMenuItem.Text = "Compare Sequences";
            this.compareSequenceMenuItem.Click += new System.EventHandler(this.compareSequenceMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(205, 6);
            // 
            // saveMarked
            // 
            this.saveMarked.Name = "saveMarked";
            this.saveMarked.Size = new System.Drawing.Size(208, 22);
            this.saveMarked.Text = "Save marked timesteps...";
            this.saveMarked.Click += new System.EventHandler(this.saveMarked_Click);
            // 
            // insertSequenceToolStripMenuItem
            // 
            this.insertSequenceToolStripMenuItem.Name = "insertSequenceToolStripMenuItem";
            this.insertSequenceToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.insertSequenceToolStripMenuItem.Text = "Insert Sequence...";
            this.insertSequenceToolStripMenuItem.Click += new System.EventHandler(this.insertSequenceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(205, 6);
            // 
            // loadSettings
            // 
            this.loadSettings.Name = "loadSettings";
            this.loadSettings.Size = new System.Drawing.Size(208, 22);
            this.loadSettings.Text = "Load S&ettings...";
            this.loadSettings.Click += new System.EventHandler(this.loadSettings_Click);
            // 
            // saveSettings
            // 
            this.saveSettings.Name = "saveSettings";
            this.saveSettings.Size = new System.Drawing.Size(208, 22);
            this.saveSettings.Text = "Save Settings As...";
            this.saveSettings.Click += new System.EventHandler(this.saveSettings_Click);
            // 
            // loadDefaultSettings
            // 
            this.loadDefaultSettings.Name = "loadDefaultSettings";
            this.loadDefaultSettings.Size = new System.Drawing.Size(208, 22);
            this.loadDefaultSettings.Text = "Load Default Se&ttings";
            this.loadDefaultSettings.Click += new System.EventHandler(this.loadDefaultSettings_Click);
            // 
            // saveDefaultSettings
            // 
            this.saveDefaultSettings.Name = "saveDefaultSettings";
            this.saveDefaultSettings.Size = new System.Drawing.Size(208, 22);
            this.saveDefaultSettings.Text = "Save Settings as &Default";
            this.saveDefaultSettings.Click += new System.EventHandler(this.saveDefaultSettings_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
            // 
            // loadLogToolStripMenuItem
            // 
            this.loadLogToolStripMenuItem.Name = "loadLogToolStripMenuItem";
            this.loadLogToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.loadLogToolStripMenuItem.Text = "Load Log...";
            this.loadLogToolStripMenuItem.Click += new System.EventHandler(this.loadLogToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(205, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
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
            this.runToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.runToolStripMenuItem.Text = "&Run";
            // 
            // runIteration0ToolStripMenuItem
            // 
            this.runIteration0ToolStripMenuItem.Name = "runIteration0ToolStripMenuItem";
            this.runIteration0ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.runIteration0ToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.runIteration0ToolStripMenuItem.Text = "Run Iteration 0";
            this.runIteration0ToolStripMenuItem.Click += new System.EventHandler(this.runIteration0ToolStripMenuItem_Click);
            // 
            // runCurrentIterationToolStripMenuItem
            // 
            this.runCurrentIterationToolStripMenuItem.Name = "runCurrentIterationToolStripMenuItem";
            this.runCurrentIterationToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.runCurrentIterationToolStripMenuItem.Text = "Run Current Iteration";
            this.runCurrentIterationToolStripMenuItem.Click += new System.EventHandler(this.runCurrentIterationToolStripMenuItem_Click);
            // 
            // runListToolStripMenuItem
            // 
            this.runListToolStripMenuItem.Name = "runListToolStripMenuItem";
            this.runListToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.runListToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.runListToolStripMenuItem.Text = "Run List";
            this.runListToolStripMenuItem.Click += new System.EventHandler(this.runListToolStripMenuItem_Click);
            // 
            // continueListToolStripMenuItem
            // 
            this.continueListToolStripMenuItem.Name = "continueListToolStripMenuItem";
            this.continueListToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.continueListToolStripMenuItem.Text = "Continue List";
            this.continueListToolStripMenuItem.Click += new System.EventHandler(this.continueListToolStripMenuItem_Click);
            // 
            // runListInRandomOrderToolStripMenuItem
            // 
            this.runListInRandomOrderToolStripMenuItem.Name = "runListInRandomOrderToolStripMenuItem";
            this.runListInRandomOrderToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.runListInRandomOrderToolStripMenuItem.Text = "Run List in Random Order";
            this.runListInRandomOrderToolStripMenuItem.Click += new System.EventHandler(this.runListInRandomOrderToolStripMenuItem_Click);
            // 
            // runWithoutSavingToolStripMenuItem
            // 
            this.runWithoutSavingToolStripMenuItem.Name = "runWithoutSavingToolStripMenuItem";
            this.runWithoutSavingToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.runWithoutSavingToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
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
            this.timestepGroupsToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.timestepGroupsToolStripMenuItem.Text = "Timestep &Groups";
            this.timestepGroupsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.timestepGroupsToolStripMenuItem_DropDownOpening);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 21);
            this.toolStripTextBox1.Visible = false;
            // 
            // placeholderGroupClickerToolStripMenuItem
            // 
            this.placeholderGroupClickerToolStripMenuItem.Checked = true;
            this.placeholderGroupClickerToolStripMenuItem.CheckOnClick = true;
            this.placeholderGroupClickerToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.placeholderGroupClickerToolStripMenuItem.Name = "placeholderGroupClickerToolStripMenuItem";
            this.placeholderGroupClickerToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.placeholderGroupClickerToolStripMenuItem.Text = "Placeholder Group Clicker";
            this.placeholderGroupClickerToolStripMenuItem.Visible = false;
            this.placeholderGroupClickerToolStripMenuItem.Click += new System.EventHandler(this.placeholderGroupClickerToolStripMenuItem_Click);
            // 
            // timestepGroupMenuSeparator
            // 
            this.timestepGroupMenuSeparator.Name = "timestepGroupMenuSeparator";
            this.timestepGroupMenuSeparator.Size = new System.Drawing.Size(257, 6);
            // 
            // createNewTimestepGroupButton
            // 
            this.createNewTimestepGroupButton.Name = "createNewTimestepGroupButton";
            this.createNewTimestepGroupButton.Size = new System.Drawing.Size(260, 22);
            this.createNewTimestepGroupButton.Text = "Create New Group";
            this.createNewTimestepGroupButton.Click += new System.EventHandler(this.createNewTimestepGroupButton_Click);
            // 
            // assignAllMarkedTimestepsToGroupToolStripMenuItem
            // 
            this.assignAllMarkedTimestepsToGroupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assignMarkedStepsToGroupComboBox,
            this.assignToolStripMenuItemAssignButton});
            this.assignAllMarkedTimestepsToGroupToolStripMenuItem.Name = "assignAllMarkedTimestepsToGroupToolStripMenuItem";
            this.assignAllMarkedTimestepsToGroupToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.assignAllMarkedTimestepsToGroupToolStripMenuItem.Text = "Assign all marked timesteps to group";
            this.assignAllMarkedTimestepsToGroupToolStripMenuItem.DropDownOpening += new System.EventHandler(this.assignAllMarkedTimestepsToGroupToolStripMenuItem_DropDownOpening);
            // 
            // assignMarkedStepsToGroupComboBox
            // 
            this.assignMarkedStepsToGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.assignMarkedStepsToGroupComboBox.Name = "assignMarkedStepsToGroupComboBox";
            this.assignMarkedStepsToGroupComboBox.Size = new System.Drawing.Size(121, 21);
            // 
            // assignToolStripMenuItemAssignButton
            // 
            this.assignToolStripMenuItemAssignButton.Name = "assignToolStripMenuItemAssignButton";
            this.assignToolStripMenuItemAssignButton.Size = new System.Drawing.Size(186, 22);
            this.assignToolStripMenuItemAssignButton.Text = "Click here to confirm.";
            this.assignToolStripMenuItemAssignButton.Click += new System.EventHandler(this.assignToolStripMenuItemAssignButton_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editLogicalDevicesToolStripMenuItem,
            this.editServerManagerToolStripMenuItem,
            this.populateSequenceWithNewChannelsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // editLogicalDevicesToolStripMenuItem
            // 
            this.editLogicalDevicesToolStripMenuItem.Name = "editLogicalDevicesToolStripMenuItem";
            this.editLogicalDevicesToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.editLogicalDevicesToolStripMenuItem.Text = "&Channel Manager";
            this.editLogicalDevicesToolStripMenuItem.Click += new System.EventHandler(this.editLogicalDevicesToolStripMenuItem_Click);
            // 
            // editServerManagerToolStripMenuItem
            // 
            this.editServerManagerToolStripMenuItem.Name = "editServerManagerToolStripMenuItem";
            this.editServerManagerToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.editServerManagerToolStripMenuItem.Text = "&Server Manager (F11)";
            this.editServerManagerToolStripMenuItem.Click += new System.EventHandler(this.editServerManagerToolStripMenuItem_Click);
            // 
            // populateSequenceWithNewChannelsToolStripMenuItem
            // 
            this.populateSequenceWithNewChannelsToolStripMenuItem.Name = "populateSequenceWithNewChannelsToolStripMenuItem";
            this.populateSequenceWithNewChannelsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.populateSequenceWithNewChannelsToolStripMenuItem.Text = "&Populate Sequence";
            this.populateSequenceWithNewChannelsToolStripMenuItem.ToolTipText = "Click here to update SequenceData object to contain all of the channels in the pr" +
                "esent SettingsData. NOTE: Once this is done, Sequence files will no longer be co" +
                "mpatible with old settings files.\r\n";
            this.populateSequenceWithNewChannelsToolStripMenuItem.Click += new System.EventHandler(this.populateSequenceWithNewChannelsToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sequenceExplorerToolStripMenuItem,
            this.settingsExplorerToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.inspectVariableTimebaseToolStripMenuItem,
            this.calculateVariableTimebaseAnalogBufferToolStripMenuItem,
            this.calculateVariableTimebaseDigitalBufferToolStripMenuItem,
            this.stToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.debugToolStripMenuItem.Text = "&Advanced";
            // 
            // sequenceExplorerToolStripMenuItem
            // 
            this.sequenceExplorerToolStripMenuItem.Name = "sequenceExplorerToolStripMenuItem";
            this.sequenceExplorerToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.sequenceExplorerToolStripMenuItem.Text = "&Sequence Explorer";
            this.sequenceExplorerToolStripMenuItem.Click += new System.EventHandler(this.sequenceExplorerToolStripMenuItem_Click);
            // 
            // settingsExplorerToolStripMenuItem
            // 
            this.settingsExplorerToolStripMenuItem.Name = "settingsExplorerToolStripMenuItem";
            this.settingsExplorerToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
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
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.saveToolStripMenuItem.Text = "&Application Settings";
            this.saveToolStripMenuItem.Visible = false;
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.saveAllToolStripMenuItem.Text = "Save &All";
            this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.saveAllToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(211, 6);
            // 
            // saveClientStartupSettingsToolStripMenuItem
            // 
            this.saveClientStartupSettingsToolStripMenuItem.Name = "saveClientStartupSettingsToolStripMenuItem";
            this.saveClientStartupSettingsToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.saveClientStartupSettingsToolStripMenuItem.Text = "Save ClientStartupSettings";
            this.saveClientStartupSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveClientStartupSettingsToolStripMenuItem_Click);
            // 
            // saveSettingsDataToolStripMenuItem
            // 
            this.saveSettingsDataToolStripMenuItem.Name = "saveSettingsDataToolStripMenuItem";
            this.saveSettingsDataToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.saveSettingsDataToolStripMenuItem.Text = "Save SettingsData";
            this.saveSettingsDataToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsDataToolStripMenuItem_Click);
            // 
            // saveSequenceDataToolStripMenuItem
            // 
            this.saveSequenceDataToolStripMenuItem.Name = "saveSequenceDataToolStripMenuItem";
            this.saveSequenceDataToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.saveSequenceDataToolStripMenuItem.Text = "Save SequenceData";
            this.saveSequenceDataToolStripMenuItem.Click += new System.EventHandler(this.saveSequenceDataToolStripMenuItem_Click);
            // 
            // inspectVariableTimebaseToolStripMenuItem
            // 
            this.inspectVariableTimebaseToolStripMenuItem.Name = "inspectVariableTimebaseToolStripMenuItem";
            this.inspectVariableTimebaseToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.inspectVariableTimebaseToolStripMenuItem.Text = "Inspect Variable Timebase";
            this.inspectVariableTimebaseToolStripMenuItem.Click += new System.EventHandler(this.inspectVariableTimebaseToolStripMenuItem_Click);
            // 
            // calculateVariableTimebaseAnalogBufferToolStripMenuItem
            // 
            this.calculateVariableTimebaseAnalogBufferToolStripMenuItem.Name = "calculateVariableTimebaseAnalogBufferToolStripMenuItem";
            this.calculateVariableTimebaseAnalogBufferToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.calculateVariableTimebaseAnalogBufferToolStripMenuItem.Text = "Calculate variable timebase analog buffer";
            this.calculateVariableTimebaseAnalogBufferToolStripMenuItem.Click += new System.EventHandler(this.calculateVariableTimebaseAnalogBufferToolStripMenuItem_Click);
            // 
            // calculateVariableTimebaseDigitalBufferToolStripMenuItem
            // 
            this.calculateVariableTimebaseDigitalBufferToolStripMenuItem.Name = "calculateVariableTimebaseDigitalBufferToolStripMenuItem";
            this.calculateVariableTimebaseDigitalBufferToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.calculateVariableTimebaseDigitalBufferToolStripMenuItem.Text = "Calculate variable timebase digital buffer";
            this.calculateVariableTimebaseDigitalBufferToolStripMenuItem.Click += new System.EventHandler(this.calculateVariableTimebaseDigitalBufferToolStripMenuItem_Click);
            // 
            // stToolStripMenuItem
            // 
            this.stToolStripMenuItem.Name = "stToolStripMenuItem";
            this.stToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.stToolStripMenuItem.Text = "Student Edition";
            this.stToolStripMenuItem.Visible = false;
            this.stToolStripMenuItem.Click += new System.EventHandler(this.stToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1,
            this.licenseToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem1.Text = "Splash Screen";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // licenseToolStripMenuItem
            // 
            this.licenseToolStripMenuItem.Name = "licenseToolStripMenuItem";
            this.licenseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.licenseToolStripMenuItem.Text = "License";
            this.licenseToolStripMenuItem.Click += new System.EventHandler(this.licenseToolStripMenuItem_Click);
            // 
            // variablesPage
            // 
            this.variablesPage.Controls.Add(this.variablesEditor1);
            this.variablesPage.Location = new System.Drawing.Point(4, 22);
            this.variablesPage.Name = "variablesPage";
            this.variablesPage.Padding = new System.Windows.Forms.Padding(3);
            this.variablesPage.Size = new System.Drawing.Size(1264, 896);
            this.variablesPage.TabIndex = 4;
            this.variablesPage.Text = "Variables (F7)";
            this.variablesPage.UseVisualStyleBackColor = true;
            // 
            // variablesEditor1
            // 
            this.variablesEditor1.Location = new System.Drawing.Point(0, 0);
            this.variablesEditor1.Name = "variablesEditor1";
            this.variablesEditor1.Size = new System.Drawing.Size(1264, 918);
            this.variablesEditor1.TabIndex = 0;
            // 
            // commonWaveformPage
            // 
            this.commonWaveformPage.Controls.Add(this.commonWaveformEditor1);
            this.commonWaveformPage.Location = new System.Drawing.Point(4, 22);
            this.commonWaveformPage.Name = "commonWaveformPage";
            this.commonWaveformPage.Padding = new System.Windows.Forms.Padding(3);
            this.commonWaveformPage.Size = new System.Drawing.Size(1264, 896);
            this.commonWaveformPage.TabIndex = 3;
            this.commonWaveformPage.Text = "Common Waveform (F6)";
            this.commonWaveformPage.UseVisualStyleBackColor = true;
            // 
            // commonWaveformEditor1
            // 
            this.commonWaveformEditor1.Location = new System.Drawing.Point(3, 3);
            this.commonWaveformEditor1.Name = "commonWaveformEditor1";
            this.commonWaveformEditor1.Size = new System.Drawing.Size(1252, 844);
            this.commonWaveformEditor1.TabIndex = 0;
            // 
            // gpibPage
            // 
            this.gpibPage.Controls.Add(this.gpibGroupEditor1);
            this.gpibPage.Location = new System.Drawing.Point(4, 22);
            this.gpibPage.Name = "gpibPage";
            this.gpibPage.Padding = new System.Windows.Forms.Padding(3);
            this.gpibPage.Size = new System.Drawing.Size(1264, 896);
            this.gpibPage.TabIndex = 2;
            this.gpibPage.Text = "GPIB (F4)";
            this.gpibPage.UseVisualStyleBackColor = true;
            // 
            // gpibGroupEditor1
            // 
            this.gpibGroupEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpibGroupEditor1.Location = new System.Drawing.Point(3, 3);
            this.gpibGroupEditor1.Name = "gpibGroupEditor1";
            this.gpibGroupEditor1.Size = new System.Drawing.Size(1258, 890);
            this.gpibGroupEditor1.TabIndex = 0;
            // 
            // analogPage
            // 
            this.analogPage.Controls.Add(this.analogGroupEditor1);
            this.analogPage.Location = new System.Drawing.Point(4, 22);
            this.analogPage.Name = "analogPage";
            this.analogPage.Padding = new System.Windows.Forms.Padding(3);
            this.analogPage.Size = new System.Drawing.Size(1264, 896);
            this.analogPage.TabIndex = 1;
            this.analogPage.Text = "Analog (F3)";
            this.analogPage.UseVisualStyleBackColor = true;
            // 
            // analogGroupEditor1
            // 
            this.analogGroupEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analogGroupEditor1.Location = new System.Drawing.Point(3, 3);
            this.analogGroupEditor1.Name = "analogGroupEditor1";
            this.analogGroupEditor1.Size = new System.Drawing.Size(1258, 890);
            this.analogGroupEditor1.TabIndex = 0;
            // 
            // sequencePage
            // 
            this.sequencePage.Controls.Add(this.waitForReady);
            this.sequencePage.Controls.Add(this.lockDigitalCheckBox);
            this.sequencePage.Controls.Add(this.label3);
            this.sequencePage.Controls.Add(this.label2);
            this.sequencePage.Controls.Add(this.label1);
            this.sequencePage.Controls.Add(this.serverManagerButton);
            this.sequencePage.Controls.Add(this.button2);
            this.sequencePage.Controls.Add(this.button1);
            this.sequencePage.Controls.Add(this.sequencePage1);
            this.sequencePage.Location = new System.Drawing.Point(4, 22);
            this.sequencePage.Name = "sequencePage";
            this.sequencePage.Padding = new System.Windows.Forms.Padding(3);
            this.sequencePage.Size = new System.Drawing.Size(1264, 896);
            this.sequencePage.TabIndex = 0;
            this.sequencePage.Text = "Sequence (F1)";
            this.sequencePage.ToolTipText = "Testing";
            this.sequencePage.UseVisualStyleBackColor = true;
            // 
            // waitForReady
            // 
            this.waitForReady.AutoSize = true;
            this.waitForReady.Location = new System.Drawing.Point(7, 791);
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
            this.lockDigitalCheckBox.Location = new System.Drawing.Point(7, 770);
            this.lockDigitalCheckBox.Name = "lockDigitalCheckBox";
            this.lockDigitalCheckBox.Size = new System.Drawing.Size(112, 17);
            this.lockDigitalCheckBox.TabIndex = 8;
            this.lockDigitalCheckBox.Text = "Lock Digital Panel";
            this.lockDigitalCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "RS232 Group:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(186, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "GPIB Group:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(179, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Analog Group:";
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 726);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 38);
            this.button1.TabIndex = 4;
            this.button1.Text = "Populate Sequence";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.populateSequenceWithNewChannelsToolStripMenuItem_Click);
            // 
            // sequencePage1
            // 
            this.sequencePage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sequencePage1.Location = new System.Drawing.Point(3, 3);
            this.sequencePage1.Name = "sequencePage1";
            this.sequencePage1.Size = new System.Drawing.Size(1258, 890);
            this.sequencePage1.TabIndex = 0;
            this.sequencePage1.Load += new System.EventHandler(this.sequencePage1_Load);
            this.sequencePage1.messageLog += new System.EventHandler(this.handleMessageEvent);
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.sequencePage);
            this.mainTab.Controls.Add(this.overrideTab);
            this.mainTab.Controls.Add(this.analogPage);
            this.mainTab.Controls.Add(this.gpibPage);
            this.mainTab.Controls.Add(this.rs232Tab);
            this.mainTab.Controls.Add(this.commonWaveformPage);
            this.mainTab.Controls.Add(this.variablesPage);
            this.mainTab.Controls.Add(this.pulsesTab);
            this.mainTab.Controls.Add(this.eventLogPage);
            this.mainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTab.Location = new System.Drawing.Point(0, 24);
            this.mainTab.Name = "mainTab";
            this.mainTab.SelectedIndex = 0;
            this.mainTab.Size = new System.Drawing.Size(1272, 922);
            this.mainTab.TabIndex = 2;
            // 
            // overrideTab
            // 
            this.overrideTab.Controls.Add(this.overridePage1);
            this.overrideTab.Location = new System.Drawing.Point(4, 22);
            this.overrideTab.Name = "overrideTab";
            this.overrideTab.Size = new System.Drawing.Size(1264, 896);
            this.overrideTab.TabIndex = 7;
            this.overrideTab.Text = "Override (F2)";
            this.overrideTab.UseVisualStyleBackColor = true;
            // 
            // overridePage1
            // 
            this.overridePage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.overridePage1.Location = new System.Drawing.Point(0, 0);
            this.overridePage1.Name = "overridePage1";
            this.overridePage1.Size = new System.Drawing.Size(1264, 896);
            this.overridePage1.TabIndex = 0;
            // 
            // rs232Tab
            // 
            this.rs232Tab.Controls.Add(this.rS232GroupEditor1);
            this.rs232Tab.Location = new System.Drawing.Point(4, 22);
            this.rs232Tab.Name = "rs232Tab";
            this.rs232Tab.Size = new System.Drawing.Size(1264, 896);
            this.rs232Tab.TabIndex = 5;
            this.rs232Tab.Text = "RS232 (F5)";
            this.rs232Tab.UseVisualStyleBackColor = true;
            // 
            // rS232GroupEditor1
            // 
            this.rS232GroupEditor1.Location = new System.Drawing.Point(3, 3);
            this.rS232GroupEditor1.Name = "rS232GroupEditor1";
            this.rS232GroupEditor1.Size = new System.Drawing.Size(1264, 918);
            this.rS232GroupEditor1.TabIndex = 0;
            // 
            // pulsesTab
            // 
            this.pulsesTab.Controls.Add(this.pulsesPage1);
            this.pulsesTab.Location = new System.Drawing.Point(4, 22);
            this.pulsesTab.Name = "pulsesTab";
            this.pulsesTab.Size = new System.Drawing.Size(1264, 896);
            this.pulsesTab.TabIndex = 8;
            this.pulsesTab.Text = "Pulses (F8)";
            this.pulsesTab.UseVisualStyleBackColor = true;
            // 
            // pulsesPage1
            // 
            this.pulsesPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pulsesPage1.Location = new System.Drawing.Point(0, 0);
            this.pulsesPage1.Name = "pulsesPage1";
            this.pulsesPage1.Size = new System.Drawing.Size(1264, 896);
            this.pulsesPage1.TabIndex = 0;
            // 
            // eventLogPage
            // 
            this.eventLogPage.Controls.Add(this.messageLogTextBox);
            this.eventLogPage.Location = new System.Drawing.Point(4, 22);
            this.eventLogPage.Name = "eventLogPage";
            this.eventLogPage.Size = new System.Drawing.Size(1264, 896);
            this.eventLogPage.TabIndex = 6;
            this.eventLogPage.Text = "Event Log";
            this.eventLogPage.UseVisualStyleBackColor = true;
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
            // mainClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 968);
            this.Controls.Add(this.mainTab);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(10000, 10000);
            this.Name = "mainClientForm";
            this.Text = "Cicero Word Generator 1.0 Beta";
            this.Load += new System.EventHandler(this.mainClientForm_Load);
            this.Activated += new System.EventHandler(this.mainClientForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainClientForm_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainClientForm_FormClosing);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.variablesPage.ResumeLayout(false);
            this.commonWaveformPage.ResumeLayout(false);
            this.gpibPage.ResumeLayout(false);
            this.analogPage.ResumeLayout(false);
            this.sequencePage.ResumeLayout(false);
            this.sequencePage.PerformLayout();
            this.mainTab.ResumeLayout(false);
            this.overrideTab.ResumeLayout(false);
            this.rs232Tab.ResumeLayout(false);
            this.pulsesTab.ResumeLayout(false);
            this.eventLogPage.ResumeLayout(false);
            this.eventLogPage.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
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
        private System.Windows.Forms.TabPage variablesPage;
        private System.Windows.Forms.TabPage commonWaveformPage;
        private System.Windows.Forms.TabPage gpibPage;
        private WordGenerator.Controls.GpibGroupEditor gpibGroupEditor1;
        private System.Windows.Forms.TabPage analogPage;
        private System.Windows.Forms.TabPage sequencePage;
        public WordGenerator.Controls.SequencePage sequencePage1;
        private System.Windows.Forms.TabControl mainTab;
        private System.Windows.Forms.TabPage rs232Tab;
        private WordGenerator.Controls.Temporary.RS232GroupEditor rS232GroupEditor1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TabPage eventLogPage;
        private System.Windows.Forms.TextBox messageLogTextBox;
        private System.Windows.Forms.ToolStripMenuItem newSequence;
        private System.Windows.Forms.Button serverManagerButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage overrideTab;
        private OverridePage overridePage1;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public WordGenerator.Controls.AnalogGroupEditor analogGroupEditor1;
        public WordGenerator.Controls.PulsesPage pulsesPage1;
        public WordGenerator.Controls.VariablesAndListPage variablesEditor1;
        public WordGenerator.Controls.CommonWaveformEditor commonWaveformEditor1;
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

    }
}

