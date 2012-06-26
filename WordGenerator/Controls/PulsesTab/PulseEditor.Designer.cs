namespace WordGenerator.Controls
{
    partial class PulseEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pulseNameTextBox = new System.Windows.Forms.TextBox();
            this.pulseDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.startDelayEnabled = new System.Windows.Forms.CheckBox();
            this.startDelayed = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.startCondition = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.endDelayEnabled = new System.Windows.Forms.CheckBox();
            this.endDelayed = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.endCondition = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pulseValue = new System.Windows.Forms.CheckBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.validityLabel = new System.Windows.Forms.Label();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.duplicateButton = new System.Windows.Forms.Button();
            this.getValueFromVariableCheckBox = new System.Windows.Forms.CheckBox();
            this.valueVariableComboBox = new System.Windows.Forms.ComboBox();
            this.autoNameCheckBox = new System.Windows.Forms.CheckBox();
            this.pulseDuration = new WordGenerator.Controls.HorizontalParameterEditor();
            this.endDelayTime = new WordGenerator.Controls.HorizontalParameterEditor();
            this.startDelayTime = new WordGenerator.Controls.HorizontalParameterEditor();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pulseNameTextBox
            // 
            this.pulseNameTextBox.Location = new System.Drawing.Point(70, 3);
            this.pulseNameTextBox.Name = "pulseNameTextBox";
            this.pulseNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.pulseNameTextBox.TabIndex = 0;
            this.pulseNameTextBox.TextChanged += new System.EventHandler(this.pulseNameTextBox_TextChanged);
            // 
            // pulseDescriptionTextBox
            // 
            this.pulseDescriptionTextBox.Location = new System.Drawing.Point(70, 30);
            this.pulseDescriptionTextBox.Name = "pulseDescriptionTextBox";
            this.pulseDescriptionTextBox.Size = new System.Drawing.Size(511, 20);
            this.pulseDescriptionTextBox.TabIndex = 1;
            this.pulseDescriptionTextBox.TextChanged += new System.EventHandler(this.pulseDescriptionTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.startDelayEnabled);
            this.groupBox1.Controls.Add(this.startDelayed);
            this.groupBox1.Controls.Add(this.startDelayTime);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.startCondition);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(14, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 121);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Start Condition";
            // 
            // startDelayEnabled
            // 
            this.startDelayEnabled.AutoSize = true;
            this.startDelayEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.startDelayEnabled.Location = new System.Drawing.Point(5, 42);
            this.startDelayEnabled.Name = "startDelayEnabled";
            this.startDelayEnabled.Size = new System.Drawing.Size(136, 17);
            this.startDelayEnabled.TabIndex = 5;
            this.startDelayEnabled.Text = "Pretrig/Delay Enabled?";
            this.startDelayEnabled.UseVisualStyleBackColor = true;
            this.startDelayEnabled.CheckedChanged += new System.EventHandler(this.startDelayEnabled_CheckedChanged);
            // 
            // startDelayed
            // 
            this.startDelayed.AutoSize = true;
            this.startDelayed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.startDelayed.Location = new System.Drawing.Point(5, 94);
            this.startDelayed.Name = "startDelayed";
            this.startDelayed.Size = new System.Drawing.Size(59, 17);
            this.startDelayed.TabIndex = 4;
            this.startDelayed.Text = "Delay?";
            this.startDelayed.UseVisualStyleBackColor = true;
            this.startDelayed.CheckedChanged += new System.EventHandler(this.startDelayed_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Pretrig/Delay time:";
            // 
            // startCondition
            // 
            this.startCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.startCondition.FormattingEnabled = true;
            this.startCondition.Location = new System.Drawing.Point(163, 16);
            this.startCondition.Name = "startCondition";
            this.startCondition.Size = new System.Drawing.Size(106, 21);
            this.startCondition.TabIndex = 1;
            this.startCondition.SelectedIndexChanged += new System.EventHandler(this.startCondition_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Condition:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.endDelayEnabled);
            this.groupBox2.Controls.Add(this.endDelayed);
            this.groupBox2.Controls.Add(this.endDelayTime);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.endCondition);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(306, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 121);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "End Condition";
            // 
            // endDelayEnabled
            // 
            this.endDelayEnabled.AutoSize = true;
            this.endDelayEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.endDelayEnabled.Location = new System.Drawing.Point(5, 42);
            this.endDelayEnabled.Name = "endDelayEnabled";
            this.endDelayEnabled.Size = new System.Drawing.Size(136, 17);
            this.endDelayEnabled.TabIndex = 5;
            this.endDelayEnabled.Text = "Pretrig/Delay Enabled?";
            this.endDelayEnabled.UseVisualStyleBackColor = true;
            this.endDelayEnabled.CheckedChanged += new System.EventHandler(this.endDelayEnabled_CheckedChanged);
            // 
            // endDelayed
            // 
            this.endDelayed.AutoSize = true;
            this.endDelayed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.endDelayed.Location = new System.Drawing.Point(5, 94);
            this.endDelayed.Name = "endDelayed";
            this.endDelayed.Size = new System.Drawing.Size(59, 17);
            this.endDelayed.TabIndex = 4;
            this.endDelayed.Text = "Delay?";
            this.endDelayed.UseVisualStyleBackColor = true;
            this.endDelayed.CheckedChanged += new System.EventHandler(this.endDelayed_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Pretrig/Delay time:";
            // 
            // endCondition
            // 
            this.endCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.endCondition.FormattingEnabled = true;
            this.endCondition.Location = new System.Drawing.Point(163, 16);
            this.endCondition.Name = "endCondition";
            this.endCondition.Size = new System.Drawing.Size(106, 21);
            this.endCondition.TabIndex = 1;
            this.endCondition.SelectedIndexChanged += new System.EventHandler(this.endCondition_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Condition:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 206);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Pulse Duration:";
            // 
            // pulseValue
            // 
            this.pulseValue.AutoSize = true;
            this.pulseValue.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pulseValue.Location = new System.Drawing.Point(23, 239);
            this.pulseValue.Name = "pulseValue";
            this.pulseValue.Size = new System.Drawing.Size(82, 17);
            this.pulseValue.TabIndex = 9;
            this.pulseValue.Text = "Pulse Value";
            this.pulseValue.UseVisualStyleBackColor = true;
            this.pulseValue.CheckedChanged += new System.EventHandler(this.pulseValue_CheckedChanged);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(494, 240);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(99, 23);
            this.deleteButton.TabIndex = 10;
            this.deleteButton.Text = "Delete Pulse";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deletebutton_Click);
            // 
            // validityLabel
            // 
            this.validityLabel.AutoSize = true;
            this.validityLabel.Location = new System.Drawing.Point(312, 224);
            this.validityLabel.Name = "validityLabel";
            this.validityLabel.Size = new System.Drawing.Size(67, 13);
            this.validityLabel.TabIndex = 11;
            this.validityLabel.Text = "Data Invalid!";
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(505, 1);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(32, 23);
            this.upButton.TabIndex = 12;
            this.upButton.Text = "/\\";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(537, 1);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(30, 23);
            this.downButton.TabIndex = 13;
            this.downButton.Text = "\\/";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // duplicateButton
            // 
            this.duplicateButton.Location = new System.Drawing.Point(494, 214);
            this.duplicateButton.Name = "duplicateButton";
            this.duplicateButton.Size = new System.Drawing.Size(99, 23);
            this.duplicateButton.TabIndex = 14;
            this.duplicateButton.Text = "Duplicate Pulse";
            this.duplicateButton.UseVisualStyleBackColor = true;
            this.duplicateButton.Click += new System.EventHandler(this.duplicateButton_Click);
            // 
            // getValueFromVariableCheckBox
            // 
            this.getValueFromVariableCheckBox.AutoSize = true;
            this.getValueFromVariableCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.getValueFromVariableCheckBox.Location = new System.Drawing.Point(147, 239);
            this.getValueFromVariableCheckBox.Name = "getValueFromVariableCheckBox";
            this.getValueFromVariableCheckBox.Size = new System.Drawing.Size(136, 17);
            this.getValueFromVariableCheckBox.TabIndex = 15;
            this.getValueFromVariableCheckBox.Text = "Get value from Variable";
            this.getValueFromVariableCheckBox.UseVisualStyleBackColor = true;
            this.getValueFromVariableCheckBox.CheckedChanged += new System.EventHandler(this.getValueFromVariableCheckBox_CheckedChanged);
            // 
            // valueVariableComboBox
            // 
            this.valueVariableComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.valueVariableComboBox.FormattingEnabled = true;
            this.valueVariableComboBox.Location = new System.Drawing.Point(16, 235);
            this.valueVariableComboBox.Name = "valueVariableComboBox";
            this.valueVariableComboBox.Size = new System.Drawing.Size(110, 21);
            this.valueVariableComboBox.TabIndex = 16;
            this.valueVariableComboBox.Visible = false;
            this.valueVariableComboBox.DropDown += new System.EventHandler(this.valueVariableComboBox_DropDown);
            this.valueVariableComboBox.SelectedIndexChanged += new System.EventHandler(this.valueVariableComboBox_SelectedIndexChanged);
            // 
            // autoNameCheckBox
            // 
            this.autoNameCheckBox.AutoSize = true;
            this.autoNameCheckBox.Location = new System.Drawing.Point(177, 5);
            this.autoNameCheckBox.Name = "autoNameCheckBox";
            this.autoNameCheckBox.Size = new System.Drawing.Size(77, 17);
            this.autoNameCheckBox.TabIndex = 17;
            this.autoNameCheckBox.Text = "Auto-name";
            this.autoNameCheckBox.UseVisualStyleBackColor = true;
            this.autoNameCheckBox.CheckedChanged += new System.EventHandler(this.autoNameCheckBox_CheckedChanged);
            // 
            // pulseDuration
            // 
            this.pulseDuration.Location = new System.Drawing.Point(108, 201);
            this.pulseDuration.Name = "pulseDuration";
            this.pulseDuration.Size = new System.Drawing.Size(150, 22);
            this.pulseDuration.TabIndex = 7;
            this.pulseDuration.UnitSelectorVisibility = true;
            this.pulseDuration.updateGUI += new System.EventHandler(this.updateAutoName);
            // 
            // endDelayTime
            // 
            this.endDelayTime.Location = new System.Drawing.Point(119, 65);
            this.endDelayTime.Name = "endDelayTime";
            this.endDelayTime.Size = new System.Drawing.Size(150, 22);
            this.endDelayTime.TabIndex = 3;
            this.endDelayTime.UnitSelectorVisibility = true;
            this.endDelayTime.updateGUI += new System.EventHandler(this.updateAutoName);
            // 
            // startDelayTime
            // 
            this.startDelayTime.Location = new System.Drawing.Point(119, 65);
            this.startDelayTime.Name = "startDelayTime";
            this.startDelayTime.Size = new System.Drawing.Size(150, 22);
            this.startDelayTime.TabIndex = 3;
            this.startDelayTime.UnitSelectorVisibility = true;
            this.startDelayTime.updateGUI += new System.EventHandler(this.updateAutoName);
            // 
            // PulseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.autoNameCheckBox);
            this.Controls.Add(this.valueVariableComboBox);
            this.Controls.Add(this.getValueFromVariableCheckBox);
            this.Controls.Add(this.duplicateButton);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.validityLabel);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.pulseValue);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pulseDuration);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pulseDescriptionTextBox);
            this.Controls.Add(this.pulseNameTextBox);
            this.Name = "PulseEditor";
            this.Size = new System.Drawing.Size(597, 267);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.TextBox pulseNameTextBox;
        private System.Windows.Forms.TextBox pulseDescriptionTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox startDelayed;
        private HorizontalParameterEditor startDelayTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox startCondition;
        private System.Windows.Forms.CheckBox startDelayEnabled;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox endDelayEnabled;
        private System.Windows.Forms.CheckBox endDelayed;
        private HorizontalParameterEditor endDelayTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox endCondition;
        private System.Windows.Forms.Label label6;
        private HorizontalParameterEditor pulseDuration;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox pulseValue;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label validityLabel;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button duplicateButton;
        private System.Windows.Forms.CheckBox getValueFromVariableCheckBox;
        private System.Windows.Forms.ComboBox valueVariableComboBox;
        private System.Windows.Forms.CheckBox autoNameCheckBox;
    }
}
