namespace WordGenerator.ChannelManager
{
    partial class ChannelManager
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
            this.logicalDevSplitContainer = new System.Windows.Forms.SplitContainer();
            this.logicalDevicesDataGridView = new System.Windows.Forms.DataGridView();
            this.deviceTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deviceIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deviceNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deviceDescColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deviceHardwareChanColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ch2AnalogCheckBox = new System.Windows.Forms.CheckBox();
            this.Ch1AnalogCheckBox = new System.Windows.Forms.CheckBox();
            this.swapChannelsButton = new System.Windows.Forms.Button();
            this.lblChID2 = new System.Windows.Forms.Label();
            this.lblChID1 = new System.Windows.Forms.Label();
            this.txtBoxChan2 = new System.Windows.Forms.TextBox();
            this.txtBoxChan1 = new System.Windows.Forms.TextBox();
            this.lblSwapChannels = new System.Windows.Forms.Label();
            this.deleteDeviceButton = new System.Windows.Forms.Button();
            this.editDeviceButton = new System.Windows.Forms.Button();
            this.addDeviceButton = new System.Windows.Forms.Button();
            this.deviceTypeCombo = new System.Windows.Forms.ComboBox();
            this.lblShowDevice = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logicalDevSplitContainer)).BeginInit();
            this.logicalDevSplitContainer.Panel1.SuspendLayout();
            this.logicalDevSplitContainer.Panel2.SuspendLayout();
            this.logicalDevSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logicalDevicesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // logicalDevSplitContainer
            // 
            this.logicalDevSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logicalDevSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.logicalDevSplitContainer.Name = "logicalDevSplitContainer";
            // 
            // logicalDevSplitContainer.Panel1
            // 
            this.logicalDevSplitContainer.Panel1.Controls.Add(this.logicalDevicesDataGridView);
            // 
            // logicalDevSplitContainer.Panel2
            // 
            this.logicalDevSplitContainer.Panel2.Controls.Add(this.Ch2AnalogCheckBox);
            this.logicalDevSplitContainer.Panel2.Controls.Add(this.Ch1AnalogCheckBox);
            this.logicalDevSplitContainer.Panel2.Controls.Add(this.swapChannelsButton);
            this.logicalDevSplitContainer.Panel2.Controls.Add(this.lblChID2);
            this.logicalDevSplitContainer.Panel2.Controls.Add(this.lblChID1);
            this.logicalDevSplitContainer.Panel2.Controls.Add(this.txtBoxChan2);
            this.logicalDevSplitContainer.Panel2.Controls.Add(this.txtBoxChan1);
            this.logicalDevSplitContainer.Panel2.Controls.Add(this.lblSwapChannels);
            this.logicalDevSplitContainer.Panel2.Controls.Add(this.deleteDeviceButton);
            this.logicalDevSplitContainer.Panel2.Controls.Add(this.editDeviceButton);
            this.logicalDevSplitContainer.Panel2.Controls.Add(this.addDeviceButton);
            this.logicalDevSplitContainer.Panel2.Controls.Add(this.deviceTypeCombo);
            this.logicalDevSplitContainer.Panel2.Controls.Add(this.lblShowDevice);
            this.logicalDevSplitContainer.Size = new System.Drawing.Size(842, 573);
            this.logicalDevSplitContainer.SplitterDistance = 695;
            this.logicalDevSplitContainer.TabIndex = 1;
            // 
            // logicalDevicesDataGridView
            // 
            this.logicalDevicesDataGridView.AllowUserToAddRows = false;
            this.logicalDevicesDataGridView.AllowUserToDeleteRows = false;
            this.logicalDevicesDataGridView.AllowUserToOrderColumns = true;
            this.logicalDevicesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.logicalDevicesDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.logicalDevicesDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.logicalDevicesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logicalDevicesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.deviceTypeColumn,
            this.deviceIDColumn,
            this.deviceNameColumn,
            this.deviceDescColumn,
            this.deviceHardwareChanColumn});
            this.logicalDevicesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logicalDevicesDataGridView.GridColor = System.Drawing.Color.Black;
            this.logicalDevicesDataGridView.Location = new System.Drawing.Point(0, 0);
            this.logicalDevicesDataGridView.MultiSelect = false;
            this.logicalDevicesDataGridView.Name = "logicalDevicesDataGridView";
            this.logicalDevicesDataGridView.ReadOnly = true;
            this.logicalDevicesDataGridView.RowHeadersVisible = false;
            this.logicalDevicesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.logicalDevicesDataGridView.Size = new System.Drawing.Size(695, 573);
            this.logicalDevicesDataGridView.TabIndex = 0;
            // 
            // deviceTypeColumn
            // 
            this.deviceTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.deviceTypeColumn.FillWeight = 126.9036F;
            this.deviceTypeColumn.HeaderText = "Type";
            this.deviceTypeColumn.Name = "deviceTypeColumn";
            this.deviceTypeColumn.ReadOnly = true;
            this.deviceTypeColumn.Width = 56;
            // 
            // deviceIDColumn
            // 
            this.deviceIDColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.deviceIDColumn.FillWeight = 93.27411F;
            this.deviceIDColumn.HeaderText = "Logical ID";
            this.deviceIDColumn.Name = "deviceIDColumn";
            this.deviceIDColumn.ReadOnly = true;
            this.deviceIDColumn.Width = 80;
            // 
            // deviceNameColumn
            // 
            this.deviceNameColumn.FillWeight = 93.27411F;
            this.deviceNameColumn.HeaderText = "Name";
            this.deviceNameColumn.Name = "deviceNameColumn";
            this.deviceNameColumn.ReadOnly = true;
            // 
            // deviceDescColumn
            // 
            this.deviceDescColumn.FillWeight = 93.27411F;
            this.deviceDescColumn.HeaderText = "Description";
            this.deviceDescColumn.Name = "deviceDescColumn";
            this.deviceDescColumn.ReadOnly = true;
            // 
            // deviceHardwareChanColumn
            // 
            this.deviceHardwareChanColumn.FillWeight = 93.27411F;
            this.deviceHardwareChanColumn.HeaderText = "Hardware Channel";
            this.deviceHardwareChanColumn.Name = "deviceHardwareChanColumn";
            this.deviceHardwareChanColumn.ReadOnly = true;
            // 
            // Ch2AnalogCheckBox
            // 
            this.Ch2AnalogCheckBox.AutoSize = true;
            this.Ch2AnalogCheckBox.Location = new System.Drawing.Point(83, 442);
            this.Ch2AnalogCheckBox.Name = "Ch2AnalogCheckBox";
            this.Ch2AnalogCheckBox.Size = new System.Drawing.Size(53, 17);
            this.Ch2AnalogCheckBox.TabIndex = 12;
            this.Ch2AnalogCheckBox.Text = "Anlg?";
            this.Ch2AnalogCheckBox.UseVisualStyleBackColor = true;
            this.Ch2AnalogCheckBox.CheckedChanged += new System.EventHandler(this.Ch2AnalogCheckBox_CheckedChanged);
            // 
            // Ch1AnalogCheckBox
            // 
            this.Ch1AnalogCheckBox.AutoSize = true;
            this.Ch1AnalogCheckBox.Location = new System.Drawing.Point(84, 371);
            this.Ch1AnalogCheckBox.Name = "Ch1AnalogCheckBox";
            this.Ch1AnalogCheckBox.Size = new System.Drawing.Size(53, 17);
            this.Ch1AnalogCheckBox.TabIndex = 11;
            this.Ch1AnalogCheckBox.Text = "Anlg?";
            this.Ch1AnalogCheckBox.UseVisualStyleBackColor = true;
            this.Ch1AnalogCheckBox.CheckedChanged += new System.EventHandler(this.Ch1AnalogCheckBox_CheckedChanged);
            // 
            // swapChannelsButton
            // 
            this.swapChannelsButton.Location = new System.Drawing.Point(30, 402);
            this.swapChannelsButton.Name = "swapChannelsButton";
            this.swapChannelsButton.Size = new System.Drawing.Size(75, 23);
            this.swapChannelsButton.TabIndex = 10;
            this.swapChannelsButton.Text = "Move To";
            this.swapChannelsButton.UseVisualStyleBackColor = true;
            this.swapChannelsButton.Click += new System.EventHandler(this.swapChannelsButton_Click);
            // 
            // lblChID2
            // 
            this.lblChID2.AutoSize = true;
            this.lblChID2.Location = new System.Drawing.Point(5, 442);
            this.lblChID2.Name = "lblChID2";
            this.lblChID2.Size = new System.Drawing.Size(38, 13);
            this.lblChID2.TabIndex = 9;
            this.lblChID2.Text = "Target";
            // 
            // lblChID1
            // 
            this.lblChID1.AutoSize = true;
            this.lblChID1.Location = new System.Drawing.Point(8, 371);
            this.lblChID1.Name = "lblChID1";
            this.lblChID1.Size = new System.Drawing.Size(31, 13);
            this.lblChID1.TabIndex = 8;
            this.lblChID1.Text = "ChID";
            // 
            // txtBoxChan2
            // 
            this.txtBoxChan2.Location = new System.Drawing.Point(47, 440);
            this.txtBoxChan2.Name = "txtBoxChan2";
            this.txtBoxChan2.Size = new System.Drawing.Size(31, 20);
            this.txtBoxChan2.TabIndex = 7;
            this.txtBoxChan2.TextChanged += new System.EventHandler(this.txtBoxChan2_TextChanged);
            // 
            // txtBoxChan1
            // 
            this.txtBoxChan1.Location = new System.Drawing.Point(47, 369);
            this.txtBoxChan1.Name = "txtBoxChan1";
            this.txtBoxChan1.Size = new System.Drawing.Size(31, 20);
            this.txtBoxChan1.TabIndex = 6;
            this.txtBoxChan1.TextChanged += new System.EventHandler(this.txtBoxChan1_TextChanged);
            // 
            // lblSwapChannels
            // 
            this.lblSwapChannels.AutoSize = true;
            this.lblSwapChannels.Location = new System.Drawing.Point(5, 335);
            this.lblSwapChannels.Name = "lblSwapChannels";
            this.lblSwapChannels.Size = new System.Drawing.Size(131, 13);
            this.lblSwapChannels.TabIndex = 5;
            this.lblSwapChannels.Text = "Channel Position Swapper";
            // 
            // deleteDeviceButton
            // 
            this.deleteDeviceButton.Location = new System.Drawing.Point(8, 122);
            this.deleteDeviceButton.Name = "deleteDeviceButton";
            this.deleteDeviceButton.Size = new System.Drawing.Size(121, 23);
            this.deleteDeviceButton.TabIndex = 4;
            this.deleteDeviceButton.Text = "Delete";
            this.deleteDeviceButton.UseVisualStyleBackColor = true;
            this.deleteDeviceButton.Click += new System.EventHandler(this.deleteDeviceButton_Click);
            // 
            // editDeviceButton
            // 
            this.editDeviceButton.Location = new System.Drawing.Point(8, 92);
            this.editDeviceButton.Name = "editDeviceButton";
            this.editDeviceButton.Size = new System.Drawing.Size(121, 23);
            this.editDeviceButton.TabIndex = 3;
            this.editDeviceButton.Text = "Edit";
            this.editDeviceButton.UseVisualStyleBackColor = true;
            this.editDeviceButton.Click += new System.EventHandler(this.editDeviceButton_Click);
            // 
            // addDeviceButton
            // 
            this.addDeviceButton.Location = new System.Drawing.Point(8, 62);
            this.addDeviceButton.Name = "addDeviceButton";
            this.addDeviceButton.Size = new System.Drawing.Size(121, 23);
            this.addDeviceButton.TabIndex = 2;
            this.addDeviceButton.Text = "Add Logical Device";
            this.addDeviceButton.UseVisualStyleBackColor = true;
            this.addDeviceButton.Click += new System.EventHandler(this.addDeviceButton_Click);
            // 
            // deviceTypeCombo
            // 
            this.deviceTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deviceTypeCombo.FormattingEnabled = true;
            this.deviceTypeCombo.Location = new System.Drawing.Point(8, 23);
            this.deviceTypeCombo.Name = "deviceTypeCombo";
            this.deviceTypeCombo.Size = new System.Drawing.Size(121, 21);
            this.deviceTypeCombo.TabIndex = 1;
            this.deviceTypeCombo.SelectedIndexChanged += new System.EventHandler(this.deviceTypeCombo_SelectedIndexChanged);
            // 
            // lblShowDevice
            // 
            this.lblShowDevice.AutoSize = true;
            this.lblShowDevice.Location = new System.Drawing.Point(5, 7);
            this.lblShowDevice.Name = "lblShowDevice";
            this.lblShowDevice.Size = new System.Drawing.Size(95, 13);
            this.lblShowDevice.TabIndex = 0;
            this.lblShowDevice.Text = "Show device type:";
            // 
            // ChannelManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 573);
            this.Controls.Add(this.logicalDevSplitContainer);
            this.Name = "ChannelManager";
            this.Text = "ChannelManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChannelManager_FormClosing);
            this.Load += new System.EventHandler(this.ChannelManager_Load);
            this.logicalDevSplitContainer.Panel1.ResumeLayout(false);
            this.logicalDevSplitContainer.Panel2.ResumeLayout(false);
            this.logicalDevSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logicalDevSplitContainer)).EndInit();
            this.logicalDevSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logicalDevicesDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer logicalDevSplitContainer;
        private System.Windows.Forms.DataGridView logicalDevicesDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn deviceTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deviceIDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deviceNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deviceDescColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deviceHardwareChanColumn;
        private System.Windows.Forms.Button deleteDeviceButton;
        private System.Windows.Forms.Button editDeviceButton;
        private System.Windows.Forms.Button addDeviceButton;
        private System.Windows.Forms.ComboBox deviceTypeCombo;
        private System.Windows.Forms.Label lblShowDevice;
        private System.Windows.Forms.Label lblSwapChannels;
        private System.Windows.Forms.Label lblChID1;
        private System.Windows.Forms.TextBox txtBoxChan2;
        private System.Windows.Forms.TextBox txtBoxChan1;
        private System.Windows.Forms.Label lblChID2;
        private System.Windows.Forms.CheckBox Ch2AnalogCheckBox;
        private System.Windows.Forms.CheckBox Ch1AnalogCheckBox;
        private System.Windows.Forms.Button swapChannelsButton;

    }
}