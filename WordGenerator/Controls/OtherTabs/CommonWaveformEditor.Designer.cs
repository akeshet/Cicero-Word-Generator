namespace WordGenerator.Controls
{
    partial class CommonWaveformEditor
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
            this.addButton = new System.Windows.Forms.Button();
            this.waveformGraphCollection1 = new WordGenerator.Controls.WaveformGraphCollection();
            this.waveformEditor1 = new WordGenerator.Controls.WaveformEditor();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(32, 102);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(145, 70);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "Add Common Waveform";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // waveformGraphCollection1
            // 
            this.waveformGraphCollection1.AutoScroll = true;
            this.waveformGraphCollection1.Location = new System.Drawing.Point(493, 1);
            this.waveformGraphCollection1.Name = "waveformGraphCollection1";
            this.waveformGraphCollection1.Size = new System.Drawing.Size(767, 750);
            this.waveformGraphCollection1.TabIndex = 1;
            // 
            // waveformEditor1
            // 
            this.waveformEditor1.AutoScroll = true;
            this.waveformEditor1.Enabled = false;
            this.waveformEditor1.Location = new System.Drawing.Point(224, 0);
            this.waveformEditor1.Name = "waveformEditor1";
            this.waveformEditor1.Size = new System.Drawing.Size(269, 790);
            this.waveformEditor1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 192);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 67);
            this.button1.TabIndex = 3;
            this.button1.Text = "Delete Selected Waveform";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CommonWaveformEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.waveformGraphCollection1);
            this.Controls.Add(this.waveformEditor1);
            this.Name = "CommonWaveformEditor";
            this.Size = new System.Drawing.Size(1264, 918);
            this.ResumeLayout(false);

        }

        #endregion

        private WordGenerator.Controls.WaveformEditor waveformEditor1;
        private WordGenerator.Controls.WaveformGraphCollection waveformGraphCollection1;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button button1;
    }
}
