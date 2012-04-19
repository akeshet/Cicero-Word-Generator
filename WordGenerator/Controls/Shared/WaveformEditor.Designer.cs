namespace WordGenerator.Controls
{
    partial class WaveformEditor
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
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.interpolationTypeComboBox = new System.Windows.Forms.ComboBox();
            this.durationLabel = new System.Windows.Forms.Label();
            this.specialParametersBox = new System.Windows.Forms.GroupBox();
            this.equationHelpText = new System.Windows.Forms.Label();
            this.equationStatusLabel = new System.Windows.Forms.Label();
            this.equationTextBox = new System.Windows.Forms.TextBox();
            this.specializedLabelStart = new System.Windows.Forms.Label();
            this.specialParametersStartPoint = new WordGenerator.Controls.HorizontalParameterEditor();
            this.downButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.waveformCombosStart = new System.Windows.Forms.ComboBox();
            this.XLabel = new System.Windows.Forms.Label();
            this.YLabel = new System.Windows.Forms.Label();
            this.sortButton = new System.Windows.Forms.Button();
            this.scaleButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToCommonWaveformsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteFromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileLoadCheckBox = new System.Windows.Forms.CheckBox();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.fileBrowseButton = new System.Windows.Forms.Button();
            this.fileLoadButton = new System.Windows.Forms.Button();
            this.fileLoadGroupBox = new System.Windows.Forms.GroupBox();
            this.XLabel1 = new System.Windows.Forms.Label();
            this.YLabel1 = new System.Windows.Forms.Label();
            this.durationParameterEditor = new WordGenerator.Controls.HorizontalParameterEditor();
            this.XYParametersStart2 = new WordGenerator.Controls.HorizontalParameterEditor();
            this.XYParametersStart1 = new WordGenerator.Controls.HorizontalParameterEditor();
            this.specialParametersBox.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.fileLoadGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(15, 3);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(38, 13);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(81, 0);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(150, 20);
            this.nameTextBox.TabIndex = 1;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(14, 29);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(34, 13);
            this.typeLabel.TabIndex = 2;
            this.typeLabel.Text = "Type:";
            // 
            // interpolationTypeComboBox
            // 
            this.interpolationTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.interpolationTypeComboBox.FormattingEnabled = true;
            this.interpolationTypeComboBox.Location = new System.Drawing.Point(81, 26);
            this.interpolationTypeComboBox.Name = "interpolationTypeComboBox";
            this.interpolationTypeComboBox.Size = new System.Drawing.Size(150, 21);
            this.interpolationTypeComboBox.TabIndex = 3;
            this.interpolationTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.interpolationTypeComboBox_SelectedIndexChanged);
            // 
            // durationLabel
            // 
            this.durationLabel.AutoSize = true;
            this.durationLabel.Location = new System.Drawing.Point(14, 56);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(50, 13);
            this.durationLabel.TabIndex = 4;
            this.durationLabel.Text = "Duration:";
            // 
            // specialParametersBox
            // 
            this.specialParametersBox.Controls.Add(this.equationHelpText);
            this.specialParametersBox.Controls.Add(this.equationStatusLabel);
            this.specialParametersBox.Controls.Add(this.equationTextBox);
            this.specialParametersBox.Controls.Add(this.specializedLabelStart);
            this.specialParametersBox.Controls.Add(this.specialParametersStartPoint);
            this.specialParametersBox.Location = new System.Drawing.Point(24, 81);
            this.specialParametersBox.Name = "specialParametersBox";
            this.specialParametersBox.Size = new System.Drawing.Size(217, 157);
            this.specialParametersBox.TabIndex = 6;
            this.specialParametersBox.TabStop = false;
            this.specialParametersBox.Text = "Specialized Parameters";
            // 
            // equationHelpText
            // 
            this.equationHelpText.Location = new System.Drawing.Point(10, 89);
            this.equationHelpText.Name = "equationHelpText";
            this.equationHelpText.Size = new System.Drawing.Size(188, 67);
            this.equationHelpText.TabIndex = 4;
            this.equationHelpText.Text = "Enter equation. You may use any of the existing variables or Common Waveforms, an" +
                "d any of the functions described in the Variables tab. Use \"t\" for time (in seco" +
                "nds).";
            this.equationHelpText.Visible = false;
            this.equationHelpText.Click += new System.EventHandler(this.equationHelpText_Click);
            // 
            // equationStatusLabel
            // 
            this.equationStatusLabel.Location = new System.Drawing.Point(8, 44);
            this.equationStatusLabel.Name = "equationStatusLabel";
            this.equationStatusLabel.Size = new System.Drawing.Size(199, 41);
            this.equationStatusLabel.TabIndex = 3;
            this.equationStatusLabel.Text = "label1";
            // 
            // equationTextBox
            // 
            this.equationTextBox.Enabled = false;
            this.equationTextBox.Location = new System.Drawing.Point(8, 20);
            this.equationTextBox.Name = "equationTextBox";
            this.equationTextBox.Size = new System.Drawing.Size(199, 20);
            this.equationTextBox.TabIndex = 2;
            this.equationTextBox.Visible = false;
            this.equationTextBox.TextChanged += new System.EventHandler(this.equationTextBox_TextChanged);
            // 
            // specializedLabelStart
            // 
            this.specializedLabelStart.AutoSize = true;
            this.specializedLabelStart.Enabled = false;
            this.specializedLabelStart.Location = new System.Drawing.Point(4, 23);
            this.specializedLabelStart.Name = "specializedLabelStart";
            this.specializedLabelStart.Size = new System.Drawing.Size(35, 13);
            this.specializedLabelStart.TabIndex = 1;
            this.specializedLabelStart.Text = "label1";
            this.specializedLabelStart.Visible = false;
            // 
            // specialParametersStartPoint
            // 
            this.specialParametersStartPoint.Enabled = false;
            this.specialParametersStartPoint.Location = new System.Drawing.Point(59, 19);
            this.specialParametersStartPoint.Name = "specialParametersStartPoint";
            this.specialParametersStartPoint.Size = new System.Drawing.Size(157, 25);
            this.specialParametersStartPoint.TabIndex = 0;
            this.specialParametersStartPoint.UnitSelectorVisibility = false;
            this.specialParametersStartPoint.Visible = false;
            // 
            // downButton
            // 
            this.downButton.Enabled = false;
            this.downButton.Location = new System.Drawing.Point(92, 372);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(37, 22);
            this.downButton.TabIndex = 9;
            this.downButton.Text = "\\/";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Visible = false;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // upButton
            // 
            this.upButton.Enabled = false;
            this.upButton.Location = new System.Drawing.Point(135, 372);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(37, 22);
            this.upButton.TabIndex = 10;
            this.upButton.Text = "/\\";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Visible = false;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // waveformCombosStart
            // 
            this.waveformCombosStart.Enabled = false;
            this.waveformCombosStart.FormattingEnabled = true;
            this.waveformCombosStart.Location = new System.Drawing.Point(69, 289);
            this.waveformCombosStart.Name = "waveformCombosStart";
            this.waveformCombosStart.Size = new System.Drawing.Size(150, 21);
            this.waveformCombosStart.TabIndex = 11;
            this.waveformCombosStart.Visible = false;
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Location = new System.Drawing.Point(34, 292);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(14, 13);
            this.XLabel.TabIndex = 12;
            this.XLabel.Text = "X";
            this.XLabel.Visible = false;
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Location = new System.Drawing.Point(166, 292);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(14, 13);
            this.YLabel.TabIndex = 13;
            this.YLabel.Text = "Y";
            this.YLabel.Visible = false;
            // 
            // sortButton
            // 
            this.sortButton.Location = new System.Drawing.Point(3, 372);
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(35, 22);
            this.sortButton.TabIndex = 14;
            this.sortButton.Text = "Sort";
            this.sortButton.UseVisualStyleBackColor = true;
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // scaleButton
            // 
            this.scaleButton.Location = new System.Drawing.Point(44, 372);
            this.scaleButton.Name = "scaleButton";
            this.scaleButton.Size = new System.Drawing.Size(42, 22);
            this.scaleButton.TabIndex = 15;
            this.scaleButton.Text = "Scale";
            this.scaleButton.UseVisualStyleBackColor = true;
            this.scaleButton.Click += new System.EventHandler(this.scaleButton_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 100;
            this.toolTip1.AutoPopDelay = 15000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 20;
            this.toolTip1.ShowAlways = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(221, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 22);
            this.button1.TabIndex = 23;
            this.button1.Text = "Copy";
            this.toolTip1.SetToolTip(this.button1, "Copies this duration to the other enable waveforms in this analog or gpib group.");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToCommonWaveformsToolStripMenuItem,
            this.copyToClipboardToolStripMenuItem,
            this.pasteFromClipboardToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(226, 70);
            // 
            // copyToCommonWaveformsToolStripMenuItem
            // 
            this.copyToCommonWaveformsToolStripMenuItem.Name = "copyToCommonWaveformsToolStripMenuItem";
            this.copyToCommonWaveformsToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.copyToCommonWaveformsToolStripMenuItem.Text = "Copy to Common Waveforms";
            this.copyToCommonWaveformsToolStripMenuItem.Click += new System.EventHandler(this.copyToCommonWaveformsToolStripMenuItem_Click);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to Clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // pasteFromClipboardToolStripMenuItem
            // 
            this.pasteFromClipboardToolStripMenuItem.Name = "pasteFromClipboardToolStripMenuItem";
            this.pasteFromClipboardToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.pasteFromClipboardToolStripMenuItem.Text = "Paste from Clipboard";
            this.pasteFromClipboardToolStripMenuItem.Click += new System.EventHandler(this.pasteFromClipboardToolStripMenuItem_Click);
            // 
            // fileLoadCheckBox
            // 
            this.fileLoadCheckBox.AutoSize = true;
            this.fileLoadCheckBox.Enabled = false;
            this.fileLoadCheckBox.Location = new System.Drawing.Point(6, 15);
            this.fileLoadCheckBox.Name = "fileLoadCheckBox";
            this.fileLoadCheckBox.Size = new System.Drawing.Size(95, 17);
            this.fileLoadCheckBox.TabIndex = 16;
            this.fileLoadCheckBox.Text = "Load From File";
            this.fileLoadCheckBox.UseVisualStyleBackColor = true;
            this.fileLoadCheckBox.Click += new System.EventHandler(this.fileLoadCheckBox_Click);
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Enabled = false;
            this.filePathTextBox.Location = new System.Drawing.Point(67, 38);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(141, 20);
            this.filePathTextBox.TabIndex = 17;
            this.filePathTextBox.Visible = false;
            // 
            // fileBrowseButton
            // 
            this.fileBrowseButton.Enabled = false;
            this.fileBrowseButton.Location = new System.Drawing.Point(6, 38);
            this.fileBrowseButton.Name = "fileBrowseButton";
            this.fileBrowseButton.Size = new System.Drawing.Size(57, 20);
            this.fileBrowseButton.TabIndex = 18;
            this.fileBrowseButton.Text = "Browse";
            this.fileBrowseButton.UseVisualStyleBackColor = true;
            this.fileBrowseButton.Visible = false;
            this.fileBrowseButton.Click += new System.EventHandler(this.fileBrowseButton_Click);
            // 
            // fileLoadButton
            // 
            this.fileLoadButton.Enabled = false;
            this.fileLoadButton.Location = new System.Drawing.Point(107, 12);
            this.fileLoadButton.Name = "fileLoadButton";
            this.fileLoadButton.Size = new System.Drawing.Size(96, 20);
            this.fileLoadButton.TabIndex = 19;
            this.fileLoadButton.Text = "Load";
            this.fileLoadButton.UseVisualStyleBackColor = true;
            this.fileLoadButton.Visible = false;
            this.fileLoadButton.Click += new System.EventHandler(this.fileLoadButton_Click);
            // 
            // fileLoadGroupBox
            // 
            this.fileLoadGroupBox.Controls.Add(this.fileLoadCheckBox);
            this.fileLoadGroupBox.Controls.Add(this.fileLoadButton);
            this.fileLoadGroupBox.Controls.Add(this.fileBrowseButton);
            this.fileLoadGroupBox.Controls.Add(this.filePathTextBox);
            this.fileLoadGroupBox.Location = new System.Drawing.Point(24, 244);
            this.fileLoadGroupBox.Name = "fileLoadGroupBox";
            this.fileLoadGroupBox.Size = new System.Drawing.Size(217, 39);
            this.fileLoadGroupBox.TabIndex = 20;
            this.fileLoadGroupBox.TabStop = false;
            // 
            // XLabel1
            // 
            this.XLabel1.AutoSize = true;
            this.XLabel1.Enabled = false;
            this.XLabel1.Location = new System.Drawing.Point(34, 292);
            this.XLabel1.Name = "XLabel1";
            this.XLabel1.Size = new System.Drawing.Size(14, 13);
            this.XLabel1.TabIndex = 21;
            this.XLabel1.Text = "X";
            this.XLabel1.Visible = false;
            // 
            // YLabel1
            // 
            this.YLabel1.AutoSize = true;
            this.YLabel1.Enabled = false;
            this.YLabel1.Location = new System.Drawing.Point(166, 292);
            this.YLabel1.Name = "YLabel1";
            this.YLabel1.Size = new System.Drawing.Size(14, 13);
            this.YLabel1.TabIndex = 22;
            this.YLabel1.Text = "Y";
            this.YLabel1.Visible = false;
            // 
            // durationParameterEditor
            // 
            this.durationParameterEditor.Location = new System.Drawing.Point(81, 53);
            this.durationParameterEditor.Name = "durationParameterEditor";
            this.durationParameterEditor.Size = new System.Drawing.Size(150, 22);
            this.durationParameterEditor.TabIndex = 5;
            this.durationParameterEditor.UnitSelectorVisibility = true;
            this.durationParameterEditor.updateGUI += new System.EventHandler(this.updateGUI);
            // 
            // XYParametersStart2
            // 
            this.XYParametersStart2.Enabled = false;
            this.XYParametersStart2.Location = new System.Drawing.Point(135, 316);
            this.XYParametersStart2.Name = "XYParametersStart2";
            this.XYParametersStart2.Size = new System.Drawing.Size(131, 22);
            this.XYParametersStart2.TabIndex = 8;
            this.XYParametersStart2.UnitSelectorVisibility = true;
            this.XYParametersStart2.Visible = false;
            // 
            // XYParametersStart1
            // 
            this.XYParametersStart1.Enabled = false;
            this.XYParametersStart1.Location = new System.Drawing.Point(3, 316);
            this.XYParametersStart1.Name = "XYParametersStart1";
            this.XYParametersStart1.Size = new System.Drawing.Size(133, 22);
            this.XYParametersStart1.TabIndex = 7;
            this.XYParametersStart1.UnitSelectorVisibility = true;
            this.XYParametersStart1.Visible = false;
            // 
            // WaveformEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.YLabel1);
            this.Controls.Add(this.fileLoadGroupBox);
            this.Controls.Add(this.specialParametersBox);
            this.Controls.Add(this.scaleButton);
            this.Controls.Add(this.sortButton);
            this.Controls.Add(this.YLabel);
            this.Controls.Add(this.durationParameterEditor);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.XYParametersStart2);
            this.Controls.Add(this.XYParametersStart1);
            this.Controls.Add(this.durationLabel);
            this.Controls.Add(this.interpolationTypeComboBox);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.waveformCombosStart);
            this.Controls.Add(this.XLabel1);
            this.Controls.Add(this.XLabel);
            this.Name = "WaveformEditor";
            this.Size = new System.Drawing.Size(269, 790);
            this.specialParametersBox.ResumeLayout(false);
            this.specialParametersBox.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.fileLoadGroupBox.ResumeLayout(false);
            this.fileLoadGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.ComboBox interpolationTypeComboBox;
        private System.Windows.Forms.Label durationLabel;
        private HorizontalParameterEditor durationParameterEditor;
        private System.Windows.Forms.GroupBox specialParametersBox;
        private HorizontalParameterEditor specialParametersStartPoint;
        private HorizontalParameterEditor XYParametersStart1;
        private HorizontalParameterEditor XYParametersStart2;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.ComboBox waveformCombosStart;
        private System.Windows.Forms.Label specializedLabelStart;
        private System.Windows.Forms.Label XLabel;
        private System.Windows.Forms.Label YLabel;
        private System.Windows.Forms.Button sortButton;
        private System.Windows.Forms.Button scaleButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToCommonWaveformsToolStripMenuItem;
        private System.Windows.Forms.CheckBox fileLoadCheckBox;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.Button fileBrowseButton;
        private System.Windows.Forms.Button fileLoadButton;
        private System.Windows.Forms.GroupBox fileLoadGroupBox;
        private System.Windows.Forms.Label XLabel1;
        private System.Windows.Forms.Label YLabel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label equationStatusLabel;
        private System.Windows.Forms.TextBox equationTextBox;
        private System.Windows.Forms.Label equationHelpText;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteFromClipboardToolStripMenuItem;
    }
}
