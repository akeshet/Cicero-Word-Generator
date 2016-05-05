namespace WordGenerator
{
    partial class RunLogManager
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
            this.okayButton = new System.Windows.Forms.Button();
            this.simpleVariableOutputCheckBox = new System.Windows.Forms.CheckBox();
            this.outputFileTextBox = new System.Windows.Forms.TextBox();
            this.outputFileLabel = new System.Windows.Forms.Label();
            this.txtLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.setPathButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // okayButton
            // 
            this.okayButton.Location = new System.Drawing.Point(54, 227);
            this.okayButton.Name = "okayButton";
            this.okayButton.Size = new System.Drawing.Size(75, 23);
            this.okayButton.TabIndex = 0;
            this.okayButton.Text = "Okay";
            this.okayButton.UseVisualStyleBackColor = true;
            this.okayButton.Click += new System.EventHandler(this.okayButton_Click);
            // 
            // simpleVariableOutputCheckBox
            // 
            this.simpleVariableOutputCheckBox.AutoSize = true;
            this.simpleVariableOutputCheckBox.Location = new System.Drawing.Point(76, 29);
            this.simpleVariableOutputCheckBox.Name = "simpleVariableOutputCheckBox";
            this.simpleVariableOutputCheckBox.Size = new System.Drawing.Size(141, 17);
            this.simpleVariableOutputCheckBox.TabIndex = 1;
            this.simpleVariableOutputCheckBox.Text = "Output variable list to file";
            this.simpleVariableOutputCheckBox.UseVisualStyleBackColor = true;
            this.simpleVariableOutputCheckBox.CheckedChanged += new System.EventHandler(this.simpleVariableOutputCheckBox_CheckedChanged);
            // 
            // outputFileTextBox
            // 
            this.outputFileTextBox.Location = new System.Drawing.Point(126, 58);
            this.outputFileTextBox.Name = "outputFileTextBox";
            this.outputFileTextBox.ReadOnly = true;
            this.outputFileTextBox.Size = new System.Drawing.Size(100, 20);
            this.outputFileTextBox.TabIndex = 2;
            this.outputFileTextBox.LostFocus += new System.EventHandler(this.outputFileTextBox_LostFocus);
            // 
            // outputFileLabel
            // 
            this.outputFileLabel.AutoSize = true;
            this.outputFileLabel.Location = new System.Drawing.Point(34, 60);
            this.outputFileLabel.Name = "outputFileLabel";
            this.outputFileLabel.Size = new System.Drawing.Size(95, 13);
            this.outputFileLabel.TabIndex = 3;
            this.outputFileLabel.Text = "Output file name: \"";
            // 
            // txtLabel
            // 
            this.txtLabel.AutoSize = true;
            this.txtLabel.Location = new System.Drawing.Point(225, 61);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(26, 13);
            this.txtLabel.TabIndex = 4;
            this.txtLabel.Text = ".txt\"";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(151, 227);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 5;
            this.closeButton.Text = "Cancel";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // setPathButton
            // 
            this.setPathButton.Location = new System.Drawing.Point(103, 96);
            this.setPathButton.Name = "setPathButton";
            this.setPathButton.Size = new System.Drawing.Size(75, 23);
            this.setPathButton.TabIndex = 7;
            this.setPathButton.Text = "Set Path";
            this.setPathButton.UseVisualStyleBackColor = true;
            this.setPathButton.Click += new System.EventHandler(this.setPathButton_Click);
            // 
            // RunLogManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.setPathButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.txtLabel);
            this.Controls.Add(this.outputFileLabel);
            this.Controls.Add(this.outputFileTextBox);
            this.Controls.Add(this.simpleVariableOutputCheckBox);
            this.Controls.Add(this.okayButton);
            this.Name = "RunLogManager";
            this.Text = "Run Log Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okayButton;
        private System.Windows.Forms.CheckBox simpleVariableOutputCheckBox;
        private System.Windows.Forms.TextBox outputFileTextBox;
        private System.Windows.Forms.Label outputFileLabel;
        private System.Windows.Forms.Label txtLabel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button setPathButton;


    }
}