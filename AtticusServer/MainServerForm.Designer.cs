namespace AtticusServer
{
    partial class MainServerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainServerForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deleteSelectedDeviceButton = new System.Windows.Forms.Button();
            this.excludeChannelsButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.hcGrid = new System.Windows.Forms.PropertyGrid();
            this.hcList = new System.Windows.Forms.ListBox();
            this.resetDevicesButton = new System.Windows.Forms.Button();
            this.HardwareChannelCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.deviceSettingsPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.clearDeviceSettingsButton = new System.Windows.Forms.Button();
            this.refreshHardwareButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.resetNetworkClocksButton = new System.Windows.Forms.Button();
            this.serverSettingsPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.connectButton = new System.Windows.Forms.Button();
            this.connectionStatusLabel = new System.Windows.Forms.Label();
            this.serverNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.eventLogTextBox = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.softwareTimedTaskLogTextCheckbox = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.serverSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splashScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openHomePageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openGitRepositoryPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showWarningsErrorsOnly = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.deleteSelectedDeviceButton);
            this.groupBox1.Controls.Add(this.excludeChannelsButton);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.hcGrid);
            this.groupBox1.Controls.Add(this.hcList);
            this.groupBox1.Controls.Add(this.resetDevicesButton);
            this.groupBox1.Controls.Add(this.HardwareChannelCount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.deviceSettingsPropertyGrid);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.clearDeviceSettingsButton);
            this.groupBox1.Controls.Add(this.refreshHardwareButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(509, 524);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hardware Settings";
            // 
            // deleteSelectedDeviceButton
            // 
            this.deleteSelectedDeviceButton.Location = new System.Drawing.Point(16, 69);
            this.deleteSelectedDeviceButton.Name = "deleteSelectedDeviceButton";
            this.deleteSelectedDeviceButton.Size = new System.Drawing.Size(93, 37);
            this.deleteSelectedDeviceButton.TabIndex = 13;
            this.deleteSelectedDeviceButton.Text = "Delete Selected Device";
            this.deleteSelectedDeviceButton.UseVisualStyleBackColor = true;
            this.deleteSelectedDeviceButton.Click += new System.EventHandler(this.deleteSelectedDeviceButtonClick);
            // 
            // excludeChannelsButton
            // 
            this.excludeChannelsButton.Location = new System.Drawing.Point(115, 302);
            this.excludeChannelsButton.Name = "excludeChannelsButton";
            this.excludeChannelsButton.Size = new System.Drawing.Size(105, 32);
            this.excludeChannelsButton.TabIndex = 12;
            this.excludeChannelsButton.Text = "Exclude channel(s)";
            this.excludeChannelsButton.UseVisualStyleBackColor = true;
            this.excludeChannelsButton.Click += new System.EventHandler(this.excludeChannelsButtonClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 312);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Channels:";
            // 
            // hcGrid
            // 
            this.hcGrid.Location = new System.Drawing.Point(226, 328);
            this.hcGrid.Name = "hcGrid";
            this.hcGrid.Size = new System.Drawing.Size(277, 185);
            this.hcGrid.TabIndex = 10;
            // 
            // hcList
            // 
            this.hcList.FormattingEnabled = true;
            this.hcList.Location = new System.Drawing.Point(18, 340);
            this.hcList.Name = "hcList";
            this.hcList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.hcList.Size = new System.Drawing.Size(202, 173);
            this.hcList.TabIndex = 9;
            this.hcList.SelectedValueChanged += new System.EventHandler(this.hcList_SelectedValueChanged);
            // 
            // resetDevicesButton
            // 
            this.resetDevicesButton.Location = new System.Drawing.Point(18, 158);
            this.resetDevicesButton.Name = "resetDevicesButton";
            this.resetDevicesButton.Size = new System.Drawing.Size(88, 34);
            this.resetDevicesButton.TabIndex = 8;
            this.resetDevicesButton.Text = "Reset Devices";
            this.resetDevicesButton.UseVisualStyleBackColor = true;
            this.resetDevicesButton.Click += new System.EventHandler(this.resetDevicesButtonClick);
            // 
            // HardwareChannelCount
            // 
            this.HardwareChannelCount.AutoSize = true;
            this.HardwareChannelCount.Location = new System.Drawing.Point(15, 207);
            this.HardwareChannelCount.Name = "HardwareChannelCount";
            this.HardwareChannelCount.Size = new System.Drawing.Size(35, 13);
            this.HardwareChannelCount.TabIndex = 7;
            this.HardwareChannelCount.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Devices:";
            // 
            // deviceSettingsPropertyGrid
            // 
            this.deviceSettingsPropertyGrid.Location = new System.Drawing.Point(226, 11);
            this.deviceSettingsPropertyGrid.Name = "deviceSettingsPropertyGrid";
            this.deviceSettingsPropertyGrid.Size = new System.Drawing.Size(277, 274);
            this.deviceSettingsPropertyGrid.TabIndex = 5;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(115, 44);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(105, 108);
            this.listBox1.TabIndex = 4;
            this.listBox1.SelectedValueChanged += new System.EventHandler(this.listBox1_SelectedValueChanged);
            // 
            // clearDeviceSettingsButton
            // 
            this.clearDeviceSettingsButton.Location = new System.Drawing.Point(16, 112);
            this.clearDeviceSettingsButton.Name = "clearDeviceSettingsButton";
            this.clearDeviceSettingsButton.Size = new System.Drawing.Size(93, 40);
            this.clearDeviceSettingsButton.TabIndex = 2;
            this.clearDeviceSettingsButton.Text = "Clear Device Settings";
            this.clearDeviceSettingsButton.UseVisualStyleBackColor = true;
            this.clearDeviceSettingsButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // refreshHardwareButton
            // 
            this.refreshHardwareButton.Location = new System.Drawing.Point(16, 28);
            this.refreshHardwareButton.Name = "refreshHardwareButton";
            this.refreshHardwareButton.Size = new System.Drawing.Size(93, 35);
            this.refreshHardwareButton.TabIndex = 0;
            this.refreshHardwareButton.Text = "Refresh Hardware";
            this.refreshHardwareButton.UseVisualStyleBackColor = true;
            this.refreshHardwareButton.Click += new System.EventHandler(this.button1_Click);
            this.refreshHardwareButton.Paint += new System.Windows.Forms.PaintEventHandler(this.button1_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.resetNetworkClocksButton);
            this.groupBox2.Controls.Add(this.serverSettingsPropertyGrid);
            this.groupBox2.Controls.Add(this.connectButton);
            this.groupBox2.Controls.Add(this.connectionStatusLabel);
            this.groupBox2.Controls.Add(this.serverNameTextBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(527, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(459, 394);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server Settings";
            // 
            // resetNetworkClocksButton
            // 
            this.resetNetworkClocksButton.Location = new System.Drawing.Point(236, 71);
            this.resetNetworkClocksButton.Name = "resetNetworkClocksButton";
            this.resetNetworkClocksButton.Size = new System.Drawing.Size(198, 35);
            this.resetNetworkClocksButton.TabIndex = 6;
            this.resetNetworkClocksButton.Text = "Reset Network Clock Provider";
            this.resetNetworkClocksButton.UseVisualStyleBackColor = true;
            this.resetNetworkClocksButton.Click += new System.EventHandler(this.resetNetworkClocksButton_Click);
            // 
            // serverSettingsPropertyGrid
            // 
            this.serverSettingsPropertyGrid.Location = new System.Drawing.Point(21, 99);
            this.serverSettingsPropertyGrid.Name = "serverSettingsPropertyGrid";
            this.serverSettingsPropertyGrid.Size = new System.Drawing.Size(413, 280);
            this.serverSettingsPropertyGrid.TabIndex = 6;
            // 
            // connectButton
            // 
            this.connectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectButton.Location = new System.Drawing.Point(236, 15);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(99, 47);
            this.connectButton.TabIndex = 5;
            this.connectButton.Text = "DummyText";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            this.connectButton.Paint += new System.Windows.Forms.PaintEventHandler(this.connectButton_Paint);
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.AutoSize = true;
            this.connectionStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectionStatusLabel.Location = new System.Drawing.Point(341, 24);
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(93, 20);
            this.connectionStatusLabel.TabIndex = 4;
            this.connectionStatusLabel.Text = "DummyText";
            this.connectionStatusLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.connectionStatusLabel_Paint);
            // 
            // serverNameTextBox
            // 
            this.serverNameTextBox.Location = new System.Drawing.Point(96, 30);
            this.serverNameTextBox.Name = "serverNameTextBox";
            this.serverNameTextBox.Size = new System.Drawing.Size(99, 20);
            this.serverNameTextBox.TabIndex = 1;
            this.serverNameTextBox.TextChanged += new System.EventHandler(this.serverNameTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Name:";
            // 
            // eventLogTextBox
            // 
            this.eventLogTextBox.Location = new System.Drawing.Point(6, 19);
            this.eventLogTextBox.Multiline = true;
            this.eventLogTextBox.Name = "eventLogTextBox";
            this.eventLogTextBox.ReadOnly = true;
            this.eventLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.eventLogTextBox.Size = new System.Drawing.Size(968, 166);
            this.eventLogTextBox.TabIndex = 0;
            this.eventLogTextBox.Click += new System.EventHandler(this.eventLogTextBox_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.eventLogTextBox);
            this.groupBox4.Location = new System.Drawing.Point(12, 552);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(980, 191);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Event Log";
            // 
            // verboseCheckBox
            // 
            this.softwareTimedTaskLogTextCheckbox.AutoSize = true;
            this.softwareTimedTaskLogTextCheckbox.Location = new System.Drawing.Point(793, 529);
            this.softwareTimedTaskLogTextCheckbox.Name = "verboseCheckBox";
            this.softwareTimedTaskLogTextCheckbox.Size = new System.Drawing.Size(193, 17);
            this.softwareTimedTaskLogTextCheckbox.TabIndex = 4;
            this.softwareTimedTaskLogTextCheckbox.Text = "Show GPIB and RS232 Commands";
            this.softwareTimedTaskLogTextCheckbox.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverSettingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(996, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // serverSettingsToolStripMenuItem
            // 
            this.serverSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveAsDefaultToolStripMenuItem,
            this.toolStripSeparator1,
            this.loadToolStripMenuItem});
            this.serverSettingsToolStripMenuItem.Name = "serverSettingsToolStripMenuItem";
            this.serverSettingsToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.serverSettingsToolStripMenuItem.Text = "Server Settings";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveToolStripMenuItem.Text = "Save...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsDefaultToolStripMenuItem
            // 
            this.saveAsDefaultToolStripMenuItem.Name = "saveAsDefaultToolStripMenuItem";
            this.saveAsDefaultToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveAsDefaultToolStripMenuItem.Text = "Save As Default";
            this.saveAsDefaultToolStripMenuItem.Click += new System.EventHandler(this.saveAsDefaultToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.loadToolStripMenuItem.Text = "Load...";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.splashScreenToolStripMenuItem,
            this.licenseInformationToolStripMenuItem,
            this.openHomePageToolStripMenuItem,
            this.openGitRepositoryPageToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // splashScreenToolStripMenuItem
            // 
            this.splashScreenToolStripMenuItem.Name = "splashScreenToolStripMenuItem";
            this.splashScreenToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.splashScreenToolStripMenuItem.Text = "Splash Screen";
            this.splashScreenToolStripMenuItem.Click += new System.EventHandler(this.splashScreenToolStripMenuItem_Click);
            // 
            // licenseInformationToolStripMenuItem
            // 
            this.licenseInformationToolStripMenuItem.Name = "licenseInformationToolStripMenuItem";
            this.licenseInformationToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.licenseInformationToolStripMenuItem.Text = "License Information";
            this.licenseInformationToolStripMenuItem.Click += new System.EventHandler(this.licenseInformationToolStripMenuItem_Click);
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
            // showWarningsErrorsOnly
            // 
            this.showWarningsErrorsOnly.AutoSize = true;
            this.showWarningsErrorsOnly.Location = new System.Drawing.Point(623, 529);
            this.showWarningsErrorsOnly.Name = "showWarningsErrorsOnly";
            this.showWarningsErrorsOnly.Size = new System.Drawing.Size(161, 17);
            this.showWarningsErrorsOnly.TabIndex = 6;
            this.showWarningsErrorsOnly.Text = "Show only Warnings / Errors";
            this.showWarningsErrorsOnly.UseVisualStyleBackColor = true;
            this.showWarningsErrorsOnly.CheckedChanged += new System.EventHandler(this.showWarningsErrorsOnly_CheckedChanged);
            // 
            // MainServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 742);
            this.Controls.Add(this.showWarningsErrorsOnly);
            this.Controls.Add(this.softwareTimedTaskLogTextCheckbox);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1012, 780);
            this.MinimumSize = new System.Drawing.Size(1012, 780);
            this.Name = "MainServerForm";
            this.Text = "Atticus Server 1.0 Beta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainServerForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainServerForm_FormClosed);
            this.Load += new System.EventHandler(this.MainServerForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox serverNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label connectionStatusLabel;
        private System.Windows.Forms.TextBox eventLogTextBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button refreshHardwareButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button clearDeviceSettingsButton;
        private System.Windows.Forms.PropertyGrid deviceSettingsPropertyGrid;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label HardwareChannelCount;
        private System.Windows.Forms.Button resetDevicesButton;
        private System.Windows.Forms.PropertyGrid serverSettingsPropertyGrid;
        private System.Windows.Forms.PropertyGrid hcGrid;
        private System.Windows.Forms.ListBox hcList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button excludeChannelsButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem serverSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsDefaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem splashScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem licenseInformationToolStripMenuItem;
        private System.Windows.Forms.Button deleteSelectedDeviceButton;
        private System.Windows.Forms.ToolStripMenuItem openHomePageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openGitRepositoryPageToolStripMenuItem;
        private System.Windows.Forms.CheckBox softwareTimedTaskLogTextCheckbox;
        private System.Windows.Forms.Button resetNetworkClocksButton;
        private System.Windows.Forms.CheckBox showWarningsErrorsOnly;

    }
}

