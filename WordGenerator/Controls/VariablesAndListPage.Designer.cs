namespace WordGenerator.Controls
{
    partial class VariablesAndListPage
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
            this.variablesPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.variableEditorPlaceholder = new WordGenerator.Controls.VariableEditor();
            this.addButton = new System.Windows.Forms.Button();
            this.lockButton = new System.Windows.Forms.Button();
            this.LockMessage = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.loadCalSequence = new System.Windows.Forms.Button();
            this.unloadCalSequence = new System.Windows.Forms.Button();
            this.calShotSequenceLabel = new System.Windows.Forms.Label();
            this.calShotSeqLabInfo = new System.Windows.Forms.Label();
            this.runEveryNCheck = new System.Windows.Forms.CheckBox();
            this.runCalN = new System.Windows.Forms.NumericUpDown();
            this.runEveryLabel = new System.Windows.Forms.Label();
            this.runCalLastCheck = new System.Windows.Forms.CheckBox();
            this.runCalFirstCheck = new System.Windows.Forms.CheckBox();
            this.calibEnabled = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listEditorPanelPlaceholder = new WordGenerator.Controls.ListEditorPanel();
            this.runControl1 = new WordGenerator.Controls.RunControl();
            this.equationHelpButton = new System.Windows.Forms.Button();
            this.permanentVariablesButton = new System.Windows.Forms.Button();
            this.variablesPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.runCalN)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // variablesPanel
            // 
            this.variablesPanel.AutoSize = true;
            this.variablesPanel.Controls.Add(this.tableLayoutPanel1);
            this.variablesPanel.Controls.Add(this.variableEditorPlaceholder);
            this.variablesPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.variablesPanel.Location = new System.Drawing.Point(3, 0);
            this.variablesPanel.Name = "variablesPanel";
            this.variablesPanel.Size = new System.Drawing.Size(235, 701);
            this.variablesPanel.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.71028F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.28972F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.nameLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(229, 19);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(115, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Del";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(39, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(35, 13);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(168, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Equation?";
            // 
            // variableEditorPlaceholder
            // 
            this.variableEditorPlaceholder.ListLocked = false;
            this.variableEditorPlaceholder.Location = new System.Drawing.Point(3, 28);
            this.variableEditorPlaceholder.Name = "variableEditorPlaceholder";
            this.variableEditorPlaceholder.Size = new System.Drawing.Size(220, 22);
            this.variableEditorPlaceholder.TabIndex = 2;
            this.variableEditorPlaceholder.Visible = false;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(24, 74);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(80, 34);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add Variable";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // lockButton
            // 
            this.lockButton.Location = new System.Drawing.Point(852, 765);
            this.lockButton.Name = "lockButton";
            this.lockButton.Size = new System.Drawing.Size(157, 72);
            this.lockButton.TabIndex = 3;
            this.lockButton.Text = "Lock Lists";
            this.lockButton.UseVisualStyleBackColor = true;
            this.lockButton.Click += new System.EventHandler(this.lockButton_Click);
            // 
            // LockMessage
            // 
            this.LockMessage.AutoSize = true;
            this.LockMessage.Location = new System.Drawing.Point(833, 845);
            this.LockMessage.Name = "LockMessage";
            this.LockMessage.Size = new System.Drawing.Size(0, 13);
            this.LockMessage.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.loadCalSequence);
            this.groupBox1.Controls.Add(this.unloadCalSequence);
            this.groupBox1.Controls.Add(this.calShotSequenceLabel);
            this.groupBox1.Controls.Add(this.calShotSeqLabInfo);
            this.groupBox1.Controls.Add(this.runEveryNCheck);
            this.groupBox1.Controls.Add(this.runCalN);
            this.groupBox1.Controls.Add(this.runEveryLabel);
            this.groupBox1.Controls.Add(this.runCalLastCheck);
            this.groupBox1.Controls.Add(this.runCalFirstCheck);
            this.groupBox1.Controls.Add(this.calibEnabled);
            this.groupBox1.Location = new System.Drawing.Point(312, 755);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 135);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Calibration Shots";
            // 
            // loadCalSequence
            // 
            this.loadCalSequence.Location = new System.Drawing.Point(225, 103);
            this.loadCalSequence.Name = "loadCalSequence";
            this.loadCalSequence.Size = new System.Drawing.Size(122, 26);
            this.loadCalSequence.TabIndex = 9;
            this.loadCalSequence.Text = "Load Sequence";
            this.loadCalSequence.UseVisualStyleBackColor = true;
            this.loadCalSequence.Click += new System.EventHandler(this.loadCalSequence_Click);
            // 
            // unloadCalSequence
            // 
            this.unloadCalSequence.Location = new System.Drawing.Point(14, 103);
            this.unloadCalSequence.Name = "unloadCalSequence";
            this.unloadCalSequence.Size = new System.Drawing.Size(118, 26);
            this.unloadCalSequence.TabIndex = 8;
            this.unloadCalSequence.Text = "Unload Sequence";
            this.unloadCalSequence.UseVisualStyleBackColor = true;
            this.unloadCalSequence.Click += new System.EventHandler(this.unloadCalSequence_Click);
            // 
            // calShotSequenceLabel
            // 
            this.calShotSequenceLabel.AutoEllipsis = true;
            this.calShotSequenceLabel.Location = new System.Drawing.Point(153, 69);
            this.calShotSequenceLabel.Name = "calShotSequenceLabel";
            this.calShotSequenceLabel.Size = new System.Drawing.Size(194, 13);
            this.calShotSequenceLabel.TabIndex = 7;
            this.calShotSequenceLabel.Text = "label4";
            // 
            // calShotSeqLabInfo
            // 
            this.calShotSeqLabInfo.AutoSize = true;
            this.calShotSeqLabInfo.Location = new System.Drawing.Point(11, 69);
            this.calShotSeqLabInfo.Name = "calShotSeqLabInfo";
            this.calShotSeqLabInfo.Size = new System.Drawing.Size(136, 13);
            this.calShotSeqLabInfo.TabIndex = 6;
            this.calShotSeqLabInfo.Text = "Calibration Shot Sequence:";
            // 
            // runEveryNCheck
            // 
            this.runEveryNCheck.AutoSize = true;
            this.runEveryNCheck.Location = new System.Drawing.Point(311, 40);
            this.runEveryNCheck.Name = "runEveryNCheck";
            this.runEveryNCheck.Size = new System.Drawing.Size(15, 14);
            this.runEveryNCheck.TabIndex = 5;
            this.runEveryNCheck.UseVisualStyleBackColor = true;
            this.runEveryNCheck.CheckedChanged += new System.EventHandler(this.runEveryNCheck_CheckedChanged);
            // 
            // runCalN
            // 
            this.runCalN.Location = new System.Drawing.Point(263, 38);
            this.runCalN.Name = "runCalN";
            this.runCalN.Size = new System.Drawing.Size(42, 20);
            this.runCalN.TabIndex = 4;
            this.runCalN.ValueChanged += new System.EventHandler(this.runCalN_ValueChanged);
            // 
            // runEveryLabel
            // 
            this.runEveryLabel.AutoSize = true;
            this.runEveryLabel.Location = new System.Drawing.Point(198, 40);
            this.runEveryLabel.Name = "runEveryLabel";
            this.runEveryLabel.Size = new System.Drawing.Size(59, 13);
            this.runEveryLabel.TabIndex = 3;
            this.runEveryLabel.Text = "Run every:";
            // 
            // runCalLastCheck
            // 
            this.runCalLastCheck.AutoSize = true;
            this.runCalLastCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.runCalLastCheck.Location = new System.Drawing.Point(102, 39);
            this.runCalLastCheck.Name = "runCalLastCheck";
            this.runCalLastCheck.Size = new System.Drawing.Size(75, 17);
            this.runCalLastCheck.TabIndex = 2;
            this.runCalLastCheck.Text = "Run Last?";
            this.runCalLastCheck.UseVisualStyleBackColor = true;
            this.runCalLastCheck.CheckedChanged += new System.EventHandler(this.runCalLastCheck_CheckedChanged);
            // 
            // runCalFirstCheck
            // 
            this.runCalFirstCheck.AutoSize = true;
            this.runCalFirstCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.runCalFirstCheck.Location = new System.Drawing.Point(11, 39);
            this.runCalFirstCheck.Name = "runCalFirstCheck";
            this.runCalFirstCheck.Size = new System.Drawing.Size(74, 17);
            this.runCalFirstCheck.TabIndex = 1;
            this.runCalFirstCheck.Text = "Run First?";
            this.runCalFirstCheck.UseVisualStyleBackColor = true;
            this.runCalFirstCheck.CheckedChanged += new System.EventHandler(this.runCalFirstCheck_CheckedChanged);
            // 
            // calibEnabled
            // 
            this.calibEnabled.AutoSize = true;
            this.calibEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.calibEnabled.Location = new System.Drawing.Point(14, 19);
            this.calibEnabled.Name = "calibEnabled";
            this.calibEnabled.Size = new System.Drawing.Size(71, 17);
            this.calibEnabled.TabIndex = 0;
            this.calibEnabled.Text = "Enabled?";
            this.calibEnabled.UseVisualStyleBackColor = true;
            this.calibEnabled.CheckedChanged += new System.EventHandler(this.calibEnabled_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.variablesPanel);
            this.panel1.Location = new System.Drawing.Point(24, 132);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 741);
            this.panel1.TabIndex = 7;
            // 
            // listEditorPanelPlaceholder
            // 
            this.listEditorPanelPlaceholder.Location = new System.Drawing.Point(326, 3);
            this.listEditorPanelPlaceholder.Name = "listEditorPanelPlaceholder";
            this.listEditorPanelPlaceholder.Size = new System.Drawing.Size(97, 774);
            this.listEditorPanelPlaceholder.TabIndex = 2;
            this.listEditorPanelPlaceholder.Visible = false;
            // 
            // runControl1
            // 
            this.runControl1.Location = new System.Drawing.Point(1092, 768);
            this.runControl1.Name = "runControl1";
            this.runControl1.Size = new System.Drawing.Size(119, 105);
            this.runControl1.TabIndex = 5;
            // 
            // equationHelpButton
            // 
            this.equationHelpButton.Location = new System.Drawing.Point(203, 66);
            this.equationHelpButton.Name = "equationHelpButton";
            this.equationHelpButton.Size = new System.Drawing.Size(66, 51);
            this.equationHelpButton.TabIndex = 8;
            this.equationHelpButton.Text = "Equation Help";
            this.equationHelpButton.UseVisualStyleBackColor = true;
            this.equationHelpButton.Click += new System.EventHandler(this.equationHelpButton_Click);
            // 
            // permanentVariablesButton
            // 
            this.permanentVariablesButton.Location = new System.Drawing.Point(112, 74);
            this.permanentVariablesButton.Name = "permanentVariablesButton";
            this.permanentVariablesButton.Size = new System.Drawing.Size(85, 34);
            this.permanentVariablesButton.TabIndex = 9;
            this.permanentVariablesButton.Text = "Permanent Variables";
            this.permanentVariablesButton.UseVisualStyleBackColor = true;
            this.permanentVariablesButton.Click += new System.EventHandler(this.permanentVariablesButton_Click);
            // 
            // VariablesAndListPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.permanentVariablesButton);
            this.Controls.Add(this.equationHelpButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LockMessage);
            this.Controls.Add(this.lockButton);
            this.Controls.Add(this.listEditorPanelPlaceholder);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.runControl1);
            this.Name = "VariablesAndListPage";
            this.Size = new System.Drawing.Size(1264, 918);
            this.variablesPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.runCalN)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel variablesPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label nameLabel;
        private VariableEditor variableEditorPlaceholder;
        private ListEditorPanel listEditorPanelPlaceholder;
        private System.Windows.Forms.Button lockButton;
        private System.Windows.Forms.Label LockMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private RunControl runControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox calibEnabled;
        private System.Windows.Forms.CheckBox runEveryNCheck;
        private System.Windows.Forms.NumericUpDown runCalN;
        private System.Windows.Forms.Label runEveryLabel;
        private System.Windows.Forms.CheckBox runCalLastCheck;
        private System.Windows.Forms.CheckBox runCalFirstCheck;
        private System.Windows.Forms.Label calShotSequenceLabel;
        private System.Windows.Forms.Label calShotSeqLabInfo;
        private System.Windows.Forms.Button loadCalSequence;
        private System.Windows.Forms.Button unloadCalSequence;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button equationHelpButton;
        private System.Windows.Forms.Button permanentVariablesButton;
    }
}
