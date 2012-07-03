namespace WordGenerator
{
    partial class RunForm
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.timeLabel = new System.Windows.Forms.Label();
            this.stepLabel = new System.Windows.Forms.Label();
            this.durationLabel = new System.Windows.Forms.Label();
            this.runAgainButton = new System.Windows.Forms.Button();
            this.fortuneCookieLabel = new System.Windows.Forms.Label();
            this.abortAfterThis = new System.Windows.Forms.CheckBox();
            this.savingWarning = new System.Windows.Forms.Label();
            this.showVariablePreviewCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(48, 340);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(477, 23);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 21);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(546, 313);
            this.textBox1.TabIndex = 1;
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(51, 403);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(90, 44);
            this.stopButton.TabIndex = 2;
            this.stopButton.Text = "Abort  (A, ESC)";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(427, 403);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(98, 44);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close  (C, Space)";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(45, 372);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(0, 13);
            this.timeLabel.TabIndex = 4;
            // 
            // stepLabel
            // 
            this.stepLabel.AutoSize = true;
            this.stepLabel.Location = new System.Drawing.Point(271, 372);
            this.stepLabel.Name = "stepLabel";
            this.stepLabel.Size = new System.Drawing.Size(0, 13);
            this.stepLabel.TabIndex = 5;
            // 
            // durationLabel
            // 
            this.durationLabel.AutoSize = true;
            this.durationLabel.Location = new System.Drawing.Point(497, 372);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(0, 13);
            this.durationLabel.TabIndex = 6;
            // 
            // runAgainButton
            // 
            this.runAgainButton.Location = new System.Drawing.Point(221, 404);
            this.runAgainButton.Name = "runAgainButton";
            this.runAgainButton.Size = new System.Drawing.Size(104, 43);
            this.runAgainButton.TabIndex = 7;
            this.runAgainButton.Text = "Run Again  (R, F9)";
            this.runAgainButton.UseVisualStyleBackColor = true;
            this.runAgainButton.Click += new System.EventHandler(this.runAgainButton_Click);
            // 
            // fortuneCookieLabel
            // 
            this.fortuneCookieLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fortuneCookieLabel.Location = new System.Drawing.Point(48, 466);
            this.fortuneCookieLabel.Name = "fortuneCookieLabel";
            this.fortuneCookieLabel.Size = new System.Drawing.Size(477, 121);
            this.fortuneCookieLabel.TabIndex = 8;
            // 
            // abortAfterThis
            // 
            this.abortAfterThis.AutoSize = true;
            this.abortAfterThis.Location = new System.Drawing.Point(144, 411);
            this.abortAfterThis.Name = "abortAfterThis";
            this.abortAfterThis.Size = new System.Drawing.Size(78, 30);
            this.abortAfterThis.TabIndex = 9;
            this.abortAfterThis.Text = "Abort after \r\nthis run";
            this.abortAfterThis.UseVisualStyleBackColor = true;
            this.abortAfterThis.Visible = false;
            // 
            // savingWarning
            // 
            this.savingWarning.AutoSize = true;
            this.savingWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savingWarning.ForeColor = System.Drawing.Color.Red;
            this.savingWarning.Location = new System.Drawing.Point(216, 600);
            this.savingWarning.Name = "savingWarning";
            this.savingWarning.Size = new System.Drawing.Size(150, 25);
            this.savingWarning.TabIndex = 10;
            this.savingWarning.Text = "NOT SAVING";
            this.savingWarning.Visible = false;
            // 
            // showVariablePreviewCheckbox
            // 
            this.showVariablePreviewCheckbox.Location = new System.Drawing.Point(336, 394);
            this.showVariablePreviewCheckbox.Name = "showVariablePreviewCheckbox";
            this.showVariablePreviewCheckbox.Size = new System.Drawing.Size(90, 66);
            this.showVariablePreviewCheckbox.TabIndex = 11;
            this.showVariablePreviewCheckbox.Text = "Variable preview";
            this.showVariablePreviewCheckbox.UseVisualStyleBackColor = true;
            this.showVariablePreviewCheckbox.CheckedChanged += new System.EventHandler(this.showVariablePreviewCheckbox_CheckedChanged);
            // 
            // RunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 634);
            this.Controls.Add(this.showVariablePreviewCheckbox);
            this.Controls.Add(this.savingWarning);
            this.Controls.Add(this.abortAfterThis);
            this.Controls.Add(this.fortuneCookieLabel);
            this.Controls.Add(this.runAgainButton);
            this.Controls.Add(this.durationLabel);
            this.Controls.Add(this.stepLabel);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(576, 662);
            this.MinimumSize = new System.Drawing.Size(576, 662);
            this.Name = "RunForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Run Controller";
            this.Activated += new System.EventHandler(this.RunForm_Activated);
            this.Deactivate += new System.EventHandler(this.RunForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RunForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label stepLabel;
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.Button runAgainButton;
        private System.Windows.Forms.Label fortuneCookieLabel;
        private System.Windows.Forms.CheckBox abortAfterThis;
        private System.Windows.Forms.Label savingWarning;
        private System.Windows.Forms.CheckBox showVariablePreviewCheckbox;
    }
}
