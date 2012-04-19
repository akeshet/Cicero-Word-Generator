namespace WordGenerator.ChannelManager
{
    partial class AddDevice
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblHardwareChan = new System.Windows.Forms.Label();
            this.deviceNameText = new System.Windows.Forms.TextBox();
            this.deviceDescText = new System.Windows.Forms.TextBox();
            this.availableHardwareChanCombo = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.lblLogicalID = new System.Windows.Forms.Label();
            this.deviceTypeCombo = new System.Windows.Forms.ComboBox();
            this.logicalIDText = new System.Windows.Forms.TextBox();
            this.refreshHardwareButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.togglingCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 53);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 78);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Description:";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 18);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Type:";
            // 
            // lblHardwareChan
            // 
            this.lblHardwareChan.AutoSize = true;
            this.lblHardwareChan.Location = new System.Drawing.Point(12, 103);
            this.lblHardwareChan.Name = "lblHardwareChan";
            this.lblHardwareChan.Size = new System.Drawing.Size(98, 13);
            this.lblHardwareChan.TabIndex = 3;
            this.lblHardwareChan.Text = "Hardware Channel:";
            // 
            // deviceNameText
            // 
            this.deviceNameText.Enabled = false;
            this.deviceNameText.Location = new System.Drawing.Point(120, 50);
            this.deviceNameText.Name = "deviceNameText";
            this.deviceNameText.Size = new System.Drawing.Size(201, 20);
            this.deviceNameText.TabIndex = 4;
            // 
            // deviceDescText
            // 
            this.deviceDescText.Enabled = false;
            this.deviceDescText.Location = new System.Drawing.Point(120, 75);
            this.deviceDescText.Name = "deviceDescText";
            this.deviceDescText.Size = new System.Drawing.Size(201, 20);
            this.deviceDescText.TabIndex = 5;
            // 
            // availableHardwareChanCombo
            // 
            this.availableHardwareChanCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.availableHardwareChanCombo.Enabled = false;
            this.availableHardwareChanCombo.FormattingEnabled = true;
            this.availableHardwareChanCombo.Location = new System.Drawing.Point(120, 100);
            this.availableHardwareChanCombo.Name = "availableHardwareChanCombo";
            this.availableHardwareChanCombo.Size = new System.Drawing.Size(201, 21);
            this.availableHardwareChanCombo.TabIndex = 6;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(170, 179);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(246, 179);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // lblLogicalID
            // 
            this.lblLogicalID.AutoSize = true;
            this.lblLogicalID.Location = new System.Drawing.Point(199, 18);
            this.lblLogicalID.Name = "lblLogicalID";
            this.lblLogicalID.Size = new System.Drawing.Size(58, 13);
            this.lblLogicalID.TabIndex = 9;
            this.lblLogicalID.Text = "Logical ID:";
            // 
            // deviceTypeCombo
            // 
            this.deviceTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deviceTypeCombo.FormattingEnabled = true;
            this.deviceTypeCombo.Location = new System.Drawing.Point(52, 15);
            this.deviceTypeCombo.Name = "deviceTypeCombo";
            this.deviceTypeCombo.Size = new System.Drawing.Size(121, 21);
            this.deviceTypeCombo.TabIndex = 10;
            this.deviceTypeCombo.SelectedIndexChanged += new System.EventHandler(this.deviceTypeCombo_SelectedIndexChanged);
            // 
            // logicalIDText
            // 
            this.logicalIDText.Location = new System.Drawing.Point(261, 15);
            this.logicalIDText.Name = "logicalIDText";
            this.logicalIDText.ReadOnly = true;
            this.logicalIDText.Size = new System.Drawing.Size(60, 20);
            this.logicalIDText.TabIndex = 11;
            // 
            // refreshHardwareButton
            // 
            this.refreshHardwareButton.Location = new System.Drawing.Point(15, 179);
            this.refreshHardwareButton.Name = "refreshHardwareButton";
            this.refreshHardwareButton.Size = new System.Drawing.Size(105, 23);
            this.refreshHardwareButton.TabIndex = 12;
            this.refreshHardwareButton.Text = "Refresh Hardware";
            this.refreshHardwareButton.UseVisualStyleBackColor = true;
            this.refreshHardwareButton.Click += new System.EventHandler(this.refreshHardwareButton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Location = new System.Drawing.Point(13, 126);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(241, 17);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Analog \"Output Now\" uses Dwell Word value";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // togglingCheck
            // 
            this.togglingCheck.AutoSize = true;
            this.togglingCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.togglingCheck.Location = new System.Drawing.Point(15, 149);
            this.togglingCheck.Name = "togglingCheck";
            this.togglingCheck.Size = new System.Drawing.Size(297, 17);
            this.togglingCheck.TabIndex = 14;
            this.togglingCheck.Text = "Toggling channel (for use with FPGA Mistrigger detection)";
            this.togglingCheck.UseVisualStyleBackColor = true;
            this.togglingCheck.Visible = false;
            this.togglingCheck.CheckedChanged += new System.EventHandler(this.togglingCheck_CheckedChanged);
            // 
            // AddDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 233);
            this.Controls.Add(this.togglingCheck);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.refreshHardwareButton);
            this.Controls.Add(this.logicalIDText);
            this.Controls.Add(this.deviceTypeCombo);
            this.Controls.Add(this.lblLogicalID);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.availableHardwareChanCombo);
            this.Controls.Add(this.deviceDescText);
            this.Controls.Add(this.deviceNameText);
            this.Controls.Add(this.lblHardwareChan);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblName);
            this.Name = "AddDevice";
            this.Text = "Add new logical device";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblHardwareChan;
        private System.Windows.Forms.TextBox deviceNameText;
        private System.Windows.Forms.TextBox deviceDescText;
        private System.Windows.Forms.ComboBox availableHardwareChanCombo;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label lblLogicalID;
        private System.Windows.Forms.ComboBox deviceTypeCombo;
        private System.Windows.Forms.TextBox logicalIDText;
        private System.Windows.Forms.Button refreshHardwareButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox togglingCheck;
    }
}