namespace WordGenerator.ChannelManager
{
    partial class EditDevice
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
            this.logicalIDText = new System.Windows.Forms.TextBox();
            this.lblLogicalID = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.availableHardwareChanCombo = new System.Windows.Forms.ComboBox();
            this.deviceDescText = new System.Windows.Forms.TextBox();
            this.deviceNameText = new System.Windows.Forms.TextBox();
            this.lblHardwareChan = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.deviceTypeText = new System.Windows.Forms.TextBox();
            this.refreshHardwareButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.togglingCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // logicalIDText
            // 
            this.logicalIDText.ForeColor = System.Drawing.SystemColors.WindowText;
            this.logicalIDText.Location = new System.Drawing.Point(261, 12);
            this.logicalIDText.Name = "logicalIDText";
            this.logicalIDText.Size = new System.Drawing.Size(60, 20);
            this.logicalIDText.TabIndex = 24;
            this.logicalIDText.TextChanged += new System.EventHandler(this.logicalIDText_TextChanged);
            // 
            // lblLogicalID
            // 
            this.lblLogicalID.AutoSize = true;
            this.lblLogicalID.Location = new System.Drawing.Point(199, 15);
            this.lblLogicalID.Name = "lblLogicalID";
            this.lblLogicalID.Size = new System.Drawing.Size(58, 13);
            this.lblLogicalID.TabIndex = 22;
            this.lblLogicalID.Text = "Logical ID:";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(246, 169);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 21;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(165, 169);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 20;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // availableHardwareChanCombo
            // 
            this.availableHardwareChanCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.availableHardwareChanCombo.FormattingEnabled = true;
            this.availableHardwareChanCombo.Location = new System.Drawing.Point(120, 97);
            this.availableHardwareChanCombo.Name = "availableHardwareChanCombo";
            this.availableHardwareChanCombo.Size = new System.Drawing.Size(201, 21);
            this.availableHardwareChanCombo.TabIndex = 19;
            this.availableHardwareChanCombo.DropDownClosed += new System.EventHandler(this.availableHardwareChanCombo_DropDownClosed);
            // 
            // deviceDescText
            // 
            this.deviceDescText.Location = new System.Drawing.Point(120, 72);
            this.deviceDescText.Name = "deviceDescText";
            this.deviceDescText.Size = new System.Drawing.Size(201, 20);
            this.deviceDescText.TabIndex = 18;
            // 
            // deviceNameText
            // 
            this.deviceNameText.Location = new System.Drawing.Point(120, 47);
            this.deviceNameText.Name = "deviceNameText";
            this.deviceNameText.Size = new System.Drawing.Size(201, 20);
            this.deviceNameText.TabIndex = 17;
            // 
            // lblHardwareChan
            // 
            this.lblHardwareChan.AutoSize = true;
            this.lblHardwareChan.Location = new System.Drawing.Point(12, 100);
            this.lblHardwareChan.Name = "lblHardwareChan";
            this.lblHardwareChan.Size = new System.Drawing.Size(98, 13);
            this.lblHardwareChan.TabIndex = 16;
            this.lblHardwareChan.Text = "Hardware Channel:";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 15);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 15;
            this.lblType.Text = "Type:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 75);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 14;
            this.lblDescription.Text = "Description:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 50);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 13;
            this.lblName.Text = "Name:";
            // 
            // deviceTypeText
            // 
            this.deviceTypeText.Location = new System.Drawing.Point(52, 12);
            this.deviceTypeText.Name = "deviceTypeText";
            this.deviceTypeText.ReadOnly = true;
            this.deviceTypeText.Size = new System.Drawing.Size(121, 20);
            this.deviceTypeText.TabIndex = 26;
            this.deviceTypeText.TextChanged += new System.EventHandler(this.deviceTypeText_TextChanged);
            // 
            // refreshHardwareButton
            // 
            this.refreshHardwareButton.Location = new System.Drawing.Point(15, 169);
            this.refreshHardwareButton.Name = "refreshHardwareButton";
            this.refreshHardwareButton.Size = new System.Drawing.Size(105, 23);
            this.refreshHardwareButton.TabIndex = 27;
            this.refreshHardwareButton.Text = "Refresh Hardware";
            this.refreshHardwareButton.UseVisualStyleBackColor = true;
            this.refreshHardwareButton.Click += new System.EventHandler(this.refreshHardwareButton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Location = new System.Drawing.Point(16, 129);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(241, 17);
            this.checkBox1.TabIndex = 28;
            this.checkBox1.Text = "Analog \"Output Now\" uses Dwell Word value";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // togglingCheck
            // 
            this.togglingCheck.AutoSize = true;
            this.togglingCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.togglingCheck.Location = new System.Drawing.Point(15, 146);
            this.togglingCheck.Name = "togglingCheck";
            this.togglingCheck.Size = new System.Drawing.Size(300, 17);
            this.togglingCheck.TabIndex = 29;
            this.togglingCheck.Text = "Toggling Channel (for use with FPGA Mistrigger Detection)";
            this.togglingCheck.UseVisualStyleBackColor = true;
            this.togglingCheck.CheckedChanged += new System.EventHandler(this.togglingCheck_CheckedChanged);
            // 
            // EditDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 209);
            this.Controls.Add(this.togglingCheck);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.refreshHardwareButton);
            this.Controls.Add(this.deviceTypeText);
            this.Controls.Add(this.logicalIDText);
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
            this.Name = "EditDevice";
            this.Text = "Edit logical device";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox logicalIDText;
        private System.Windows.Forms.Label lblLogicalID;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ComboBox availableHardwareChanCombo;
        private System.Windows.Forms.TextBox deviceDescText;
        private System.Windows.Forms.TextBox deviceNameText;
        private System.Windows.Forms.Label lblHardwareChan;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox deviceTypeText;
        private System.Windows.Forms.Button refreshHardwareButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox togglingCheck;
    }
}