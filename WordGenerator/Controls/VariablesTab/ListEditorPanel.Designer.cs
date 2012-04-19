namespace WordGenerator.Controls
{
    partial class ListEditorPanel
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listName = new System.Windows.Forms.Label();
            this.timesButton = new System.Windows.Forms.Button();
            this.enabledBox = new System.Windows.Forms.CheckBox();
            this.lineCount = new System.Windows.Forms.Label();
            this.shuffleButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 54);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(55, 644);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // listName
            // 
            this.listName.AutoSize = true;
            this.listName.Location = new System.Drawing.Point(5, 29);
            this.listName.Name = "listName";
            this.listName.Size = new System.Drawing.Size(35, 13);
            this.listName.TabIndex = 1;
            this.listName.Text = "label1";
            // 
            // timesButton
            // 
            this.timesButton.Location = new System.Drawing.Point(62, 357);
            this.timesButton.Name = "timesButton";
            this.timesButton.Size = new System.Drawing.Size(30, 26);
            this.timesButton.TabIndex = 2;
            this.timesButton.Text = "button1";
            this.timesButton.UseVisualStyleBackColor = true;
            this.timesButton.Click += new System.EventHandler(this.timesButton_Click);
            // 
            // enabledBox
            // 
            this.enabledBox.AutoSize = true;
            this.enabledBox.Location = new System.Drawing.Point(8, 12);
            this.enabledBox.Name = "enabledBox";
            this.enabledBox.Size = new System.Drawing.Size(15, 14);
            this.enabledBox.TabIndex = 3;
            this.enabledBox.UseVisualStyleBackColor = true;
            this.enabledBox.CheckedChanged += new System.EventHandler(this.enabledBox_CheckedChanged);
            // 
            // lineCount
            // 
            this.lineCount.AutoSize = true;
            this.lineCount.Location = new System.Drawing.Point(3, 727);
            this.lineCount.Name = "lineCount";
            this.lineCount.Size = new System.Drawing.Size(35, 13);
            this.lineCount.TabIndex = 4;
            this.lineCount.Text = "label1";
            // 
            // shuffleButton
            // 
            this.shuffleButton.Location = new System.Drawing.Point(3, 701);
            this.shuffleButton.Name = "shuffleButton";
            this.shuffleButton.Size = new System.Drawing.Size(55, 23);
            this.shuffleButton.TabIndex = 5;
            this.shuffleButton.Text = "Shuffle";
            this.shuffleButton.UseVisualStyleBackColor = true;
            this.shuffleButton.Click += new System.EventHandler(this.shuffleButton_Click);
            // 
            // ListEditorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.shuffleButton);
            this.Controls.Add(this.lineCount);
            this.Controls.Add(this.enabledBox);
            this.Controls.Add(this.timesButton);
            this.Controls.Add(this.listName);
            this.Controls.Add(this.textBox1);
            this.Name = "ListEditorPanel";
            this.Size = new System.Drawing.Size(96, 774);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label listName;
        private System.Windows.Forms.Button timesButton;
        private System.Windows.Forms.CheckBox enabledBox;
        private System.Windows.Forms.Label lineCount;
        private System.Windows.Forms.Button shuffleButton;
    }
}
