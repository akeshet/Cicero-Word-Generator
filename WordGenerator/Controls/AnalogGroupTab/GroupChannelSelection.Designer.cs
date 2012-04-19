namespace WordGenerator.Controls
{
    partial class GroupChannelSelection
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
            this.components = new System.ComponentModel.Container();
            this.channelNameLabel = new System.Windows.Forms.Label();
            this.enabledButton = new System.Windows.Forms.Button();
            this.commonWaveformSelector = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // channelNameLabel
            // 
            this.channelNameLabel.AutoEllipsis = true;
            this.channelNameLabel.Location = new System.Drawing.Point(3, 6);
            this.channelNameLabel.Name = "channelNameLabel";
            this.channelNameLabel.Size = new System.Drawing.Size(61, 13);
            this.channelNameLabel.TabIndex = 0;
            this.channelNameLabel.Text = "label1";
            // 
            // enabledButton
            // 
            this.enabledButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.enabledButton.Location = new System.Drawing.Point(70, 1);
            this.enabledButton.Name = "enabledButton";
            this.enabledButton.Size = new System.Drawing.Size(59, 23);
            this.enabledButton.TabIndex = 1;
            this.enabledButton.Text = "button1";
            this.enabledButton.UseVisualStyleBackColor = false;
            this.enabledButton.Click += new System.EventHandler(this.enabledButton_Click);
            // 
            // commonWaveformSelector
            // 
            this.commonWaveformSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.commonWaveformSelector.FormattingEnabled = true;
            this.commonWaveformSelector.Location = new System.Drawing.Point(135, 3);
            this.commonWaveformSelector.Name = "commonWaveformSelector";
            this.commonWaveformSelector.Size = new System.Drawing.Size(68, 21);
            this.commonWaveformSelector.TabIndex = 2;
            this.commonWaveformSelector.SelectedIndexChanged += new System.EventHandler(this.commonWaveformSelector_SelectedIndexChanged);
            this.commonWaveformSelector.DropDown += new System.EventHandler(this.commonWaveformSelector_DropDown);
            // 
            // GroupChannelSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.commonWaveformSelector);
            this.Controls.Add(this.enabledButton);
            this.Controls.Add(this.channelNameLabel);
            this.Name = "GroupChannelSelection";
            this.Size = new System.Drawing.Size(203, 28);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label channelNameLabel;
        private System.Windows.Forms.Button enabledButton;
        private System.Windows.Forms.ComboBox commonWaveformSelector;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
