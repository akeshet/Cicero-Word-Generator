namespace WordGenerator.Controls
{
    partial class AnalogGroupEditor
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
            this.groupChannelSelectorPanel = new System.Windows.Forms.Panel();
            this.groupChannelSelectorPlaceholder = new WordGenerator.Controls.GroupChannelSelection();
            this.newGroupButton = new System.Windows.Forms.Button();
            this.analogGroupSelector = new System.Windows.Forms.ComboBox();
            this.renameTextBox = new System.Windows.Forms.TextBox();
            this.renameButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.timeResolutionLabel = new System.Windows.Forms.Label();
            this.descBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.plus = new System.Windows.Forms.Button();
            this.minus = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.runOrderPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.replaceGroupButton = new System.Windows.Forms.Button();
            this.replacementGroupSelector = new System.Windows.Forms.ComboBox();
            this.timeResolutionEditor = new WordGenerator.Controls.HorizontalParameterEditor();
            this.waveformEditor1 = new WordGenerator.Controls.WaveformEditor();
            this.waveformGraphCollection1 = new WordGenerator.Controls.WaveformGraphCollection();
            this.button2 = new System.Windows.Forms.Button();
            this.groupChannelSelectorPanel.SuspendLayout();
            this.runOrderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupChannelSelectorPanel
            // 
            this.groupChannelSelectorPanel.AutoScroll = true;
            this.groupChannelSelectorPanel.Controls.Add(this.groupChannelSelectorPlaceholder);
            this.groupChannelSelectorPanel.Location = new System.Drawing.Point(3, 313);
            this.groupChannelSelectorPanel.Name = "groupChannelSelectorPanel";
            this.groupChannelSelectorPanel.Size = new System.Drawing.Size(220, 477);
            this.groupChannelSelectorPanel.TabIndex = 0;
            // 
            // groupChannelSelectorPlaceholder
            // 
            this.groupChannelSelectorPlaceholder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.groupChannelSelectorPlaceholder.Location = new System.Drawing.Point(0, 3);
            this.groupChannelSelectorPlaceholder.Name = "groupChannelSelectorPlaceholder";
            this.groupChannelSelectorPlaceholder.Size = new System.Drawing.Size(203, 28);
            this.groupChannelSelectorPlaceholder.TabIndex = 0;
            this.groupChannelSelectorPlaceholder.Visible = false;
            // 
            // newGroupButton
            // 
            this.newGroupButton.Location = new System.Drawing.Point(3, 56);
            this.newGroupButton.Name = "newGroupButton";
            this.newGroupButton.Size = new System.Drawing.Size(110, 26);
            this.newGroupButton.TabIndex = 3;
            this.newGroupButton.Text = "Create New Group";
            this.newGroupButton.UseVisualStyleBackColor = true;
            this.newGroupButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // analogGroupSelector
            // 
            this.analogGroupSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.analogGroupSelector.FormattingEnabled = true;
            this.analogGroupSelector.Location = new System.Drawing.Point(3, 3);
            this.analogGroupSelector.Name = "analogGroupSelector";
            this.analogGroupSelector.Size = new System.Drawing.Size(110, 21);
            this.analogGroupSelector.TabIndex = 6;
            this.analogGroupSelector.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.analogGroupSelector.DropDownClosed += new System.EventHandler(this.analogGroupSelector_DropDownClosed);
            this.analogGroupSelector.DropDown += new System.EventHandler(this.comboBox1_DropDown);
            // 
            // renameTextBox
            // 
            this.renameTextBox.Location = new System.Drawing.Point(3, 30);
            this.renameTextBox.Name = "renameTextBox";
            this.renameTextBox.Size = new System.Drawing.Size(92, 20);
            this.renameTextBox.TabIndex = 7;
            this.renameTextBox.TextChanged += new System.EventHandler(this.renameTextBox_TextChanged);
            // 
            // renameButton
            // 
            this.renameButton.Location = new System.Drawing.Point(101, 29);
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(66, 21);
            this.renameButton.TabIndex = 8;
            this.renameButton.Text = "Rename";
            this.renameButton.UseVisualStyleBackColor = true;
            this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 27);
            this.button1.TabIndex = 9;
            this.button1.Text = "Delete This Group";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // timeResolutionLabel
            // 
            this.timeResolutionLabel.AutoSize = true;
            this.timeResolutionLabel.Location = new System.Drawing.Point(3, 118);
            this.timeResolutionLabel.Name = "timeResolutionLabel";
            this.timeResolutionLabel.Size = new System.Drawing.Size(86, 13);
            this.timeResolutionLabel.TabIndex = 11;
            this.timeResolutionLabel.Text = "Time Resolution:";
            // 
            // descBox
            // 
            this.descBox.Location = new System.Drawing.Point(3, 177);
            this.descBox.Multiline = true;
            this.descBox.Name = "descBox";
            this.descBox.Size = new System.Drawing.Size(164, 110);
            this.descBox.TabIndex = 13;
            this.descBox.TextChanged += new System.EventHandler(this.descBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Description:";
            // 
            // plus
            // 
            this.plus.Location = new System.Drawing.Point(119, 3);
            this.plus.Name = "plus";
            this.plus.Size = new System.Drawing.Size(22, 22);
            this.plus.TabIndex = 15;
            this.plus.Text = "+";
            this.plus.UseVisualStyleBackColor = true;
            this.plus.Click += new System.EventHandler(this.plus_Click);
            // 
            // minus
            // 
            this.minus.Location = new System.Drawing.Point(147, 3);
            this.minus.Name = "minus";
            this.minus.Size = new System.Drawing.Size(22, 22);
            this.minus.TabIndex = 16;
            this.minus.Text = "-";
            this.minus.UseVisualStyleBackColor = true;
            this.minus.Click += new System.EventHandler(this.minus_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 100;
            // 
            // runOrderPanel
            // 
            this.runOrderPanel.AutoScroll = true;
            this.runOrderPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.runOrderPanel.Controls.Add(this.label3);
            this.runOrderPanel.Controls.Add(this.label1);
            this.runOrderPanel.Location = new System.Drawing.Point(6, 796);
            this.runOrderPanel.Name = "runOrderPanel";
            this.runOrderPanel.Size = new System.Drawing.Size(1234, 73);
            this.runOrderPanel.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "(click to be transported)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Analog Group Run Order:";
            // 
            // replaceGroupButton
            // 
            this.replaceGroupButton.Enabled = false;
            this.replaceGroupButton.Location = new System.Drawing.Point(3, 289);
            this.replaceGroupButton.Name = "replaceGroupButton";
            this.replaceGroupButton.Size = new System.Drawing.Size(92, 22);
            this.replaceGroupButton.TabIndex = 19;
            this.replaceGroupButton.Text = "Replace Group";
            this.replaceGroupButton.UseVisualStyleBackColor = true;
            this.replaceGroupButton.Click += new System.EventHandler(this.replaceGroupButton_Click);
            // 
            // replacementGroupSelector
            // 
            this.replacementGroupSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.replacementGroupSelector.Enabled = false;
            this.replacementGroupSelector.FormattingEnabled = true;
            this.replacementGroupSelector.Location = new System.Drawing.Point(101, 289);
            this.replacementGroupSelector.MaxDropDownItems = 100;
            this.replacementGroupSelector.Name = "replacementGroupSelector";
            this.replacementGroupSelector.Size = new System.Drawing.Size(122, 21);
            this.replacementGroupSelector.TabIndex = 20;
            this.replacementGroupSelector.SelectedValueChanged += new System.EventHandler(this.replacementGroupSelector_SelectedValueChanged);
            this.replacementGroupSelector.DropDown += new System.EventHandler(this.replacementSelector_DropDown_1);
            // 
            // timeResolutionEditor
            // 
            this.timeResolutionEditor.Location = new System.Drawing.Point(6, 136);
            this.timeResolutionEditor.Name = "timeResolutionEditor";
            this.timeResolutionEditor.Size = new System.Drawing.Size(150, 22);
            this.timeResolutionEditor.TabIndex = 17;
            this.timeResolutionEditor.UnitSelectorVisibility = true;
            // 
            // waveformEditor1
            // 
            this.waveformEditor1.AutoScroll = true;
            this.waveformEditor1.Enabled = false;
            this.waveformEditor1.Location = new System.Drawing.Point(224, 0);
            this.waveformEditor1.Name = "waveformEditor1";
            this.waveformEditor1.Size = new System.Drawing.Size(269, 790);
            this.waveformEditor1.TabIndex = 2;
            this.waveformEditor1.copyDuration += new System.EventHandler(this.waveformEditor1_copyDuration);
            // 
            // waveformGraphCollection1
            // 
            this.waveformGraphCollection1.AutoScroll = true;
            this.waveformGraphCollection1.Location = new System.Drawing.Point(493, 1);
            this.waveformGraphCollection1.Name = "waveformGraphCollection1";
            this.waveformGraphCollection1.Size = new System.Drawing.Size(785, 750);
            this.waveformGraphCollection1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(119, 56);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 34);
            this.button2.TabIndex = 21;
            this.button2.Text = "Delete Unused Groups";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AnalogGroupEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.replacementGroupSelector);
            this.Controls.Add(this.replaceGroupButton);
            this.Controls.Add(this.runOrderPanel);
            this.Controls.Add(this.timeResolutionEditor);
            this.Controls.Add(this.minus);
            this.Controls.Add(this.plus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.descBox);
            this.Controls.Add(this.timeResolutionLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.renameButton);
            this.Controls.Add(this.renameTextBox);
            this.Controls.Add(this.analogGroupSelector);
            this.Controls.Add(this.newGroupButton);
            this.Controls.Add(this.waveformEditor1);
            this.Controls.Add(this.waveformGraphCollection1);
            this.Controls.Add(this.groupChannelSelectorPanel);
            this.Name = "AnalogGroupEditor";
            this.Size = new System.Drawing.Size(1320, 918);
            this.VisibleChanged += new System.EventHandler(this.AnalogGroupEditor_VisibleChanged);
            this.groupChannelSelectorPanel.ResumeLayout(false);
            this.runOrderPanel.ResumeLayout(false);
            this.runOrderPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel groupChannelSelectorPanel;
        private WordGenerator.Controls.GroupChannelSelection groupChannelSelectorPlaceholder;
        private WordGenerator.Controls.WaveformGraphCollection waveformGraphCollection1;
        private WordGenerator.Controls.WaveformEditor waveformEditor1;
        private System.Windows.Forms.Button newGroupButton;
        private System.Windows.Forms.ComboBox analogGroupSelector;
        private System.Windows.Forms.TextBox renameTextBox;
        private System.Windows.Forms.Button renameButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label timeResolutionLabel;
        private System.Windows.Forms.TextBox descBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button plus;
        private System.Windows.Forms.Button minus;
        private HorizontalParameterEditor timeResolutionEditor;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel runOrderPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button replaceGroupButton;
        private System.Windows.Forms.ComboBox replacementGroupSelector;
        private System.Windows.Forms.Button button2;

    }
}
