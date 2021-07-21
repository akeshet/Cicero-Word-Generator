namespace WordGenerator.Controls
{
    partial class GpibGroupChannelSelection
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
            this.dataTypeSelector = new System.Windows.Forms.ComboBox();
            this.rawStringTextBox = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.spsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // channelNameLabel
            // 
            this.channelNameLabel.AutoEllipsis = true;
            this.channelNameLabel.Location = new System.Drawing.Point(3, 6);
            this.channelNameLabel.Name = "channelNameLabel";
            this.channelNameLabel.Size = new System.Drawing.Size(65, 13);
            this.channelNameLabel.TabIndex = 0;
            this.channelNameLabel.Text = "label1";
            this.channelNameLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.channelName_Paint);
            // 
            // enabledButton
            // 
            this.enabledButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.enabledButton.Location = new System.Drawing.Point(74, 1);
            this.enabledButton.Name = "enabledButton";
            this.enabledButton.Size = new System.Drawing.Size(63, 23);
            this.enabledButton.TabIndex = 1;
            this.enabledButton.Text = "button1";
            this.enabledButton.UseVisualStyleBackColor = false;
            this.enabledButton.Click += new System.EventHandler(this.enabledButton_Click);
            // 
            // dataTypeSelector
            // 
            this.dataTypeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dataTypeSelector.FormattingEnabled = true;
            this.dataTypeSelector.Location = new System.Drawing.Point(143, 3);
            this.dataTypeSelector.Name = "dataTypeSelector";
            this.dataTypeSelector.Size = new System.Drawing.Size(61, 21);
            this.dataTypeSelector.TabIndex = 2;
            this.dataTypeSelector.SelectedIndexChanged += new System.EventHandler(this.dataTypeSelector_SelectedIndexChanged);
            this.dataTypeSelector.DropDownClosed += new System.EventHandler(this.dataTypeSelector_DropDownClosed);
            // 
            // rawStringTextBox
            // 
            this.rawStringTextBox.Location = new System.Drawing.Point(6, 30);
            this.rawStringTextBox.Name = "rawStringTextBox";
            this.rawStringTextBox.Size = new System.Drawing.Size(198, 20);
            this.rawStringTextBox.TabIndex = 3;
            this.rawStringTextBox.TextChanged += new System.EventHandler(this.rawStringTextBox_TextChanged);
            // 
            // spsFlowLayoutPanel
            // 
            this.spsFlowLayoutPanel.AutoSize = true;
            this.spsFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.spsFlowLayoutPanel.Location = new System.Drawing.Point(4, 31);
            this.spsFlowLayoutPanel.Name = "spsFlowLayoutPanel";
            this.spsFlowLayoutPanel.Size = new System.Drawing.Size(199, 18);
            this.spsFlowLayoutPanel.TabIndex = 4;
            this.spsFlowLayoutPanel.Visible = false;
            // 
            // GpibGroupChannelSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.spsFlowLayoutPanel);
            this.Controls.Add(this.rawStringTextBox);
            this.Controls.Add(this.dataTypeSelector);
            this.Controls.Add(this.enabledButton);
            this.Controls.Add(this.channelNameLabel);
            this.Name = "GpibGroupChannelSelection";
            this.Size = new System.Drawing.Size(207, 58);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label channelNameLabel;
        private System.Windows.Forms.Button enabledButton;
        private System.Windows.Forms.ComboBox dataTypeSelector;
        private System.Windows.Forms.TextBox rawStringTextBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.FlowLayoutPanel spsFlowLayoutPanel;
    }
}
