namespace WordGenerator.Controls
{
    partial class GpibGroupEditor
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
            this.groupChannelSelectorPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.newGroupButton = new System.Windows.Forms.Button();
            this.gpibGroupSelector = new System.Windows.Forms.ComboBox();
            this.renameTextBox = new System.Windows.Forms.TextBox();
            this.renameButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.descBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.plus = new System.Windows.Forms.Button();
            this.minus = new System.Windows.Forms.Button();
            this.runOrderPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.replacementGroupSelector = new System.Windows.Forms.ComboBox();
            this.replaceGroupButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.waveformEditor1 = new WordGenerator.Controls.WaveformEditor();
            this.waveformGraphCollection1 = new WordGenerator.Controls.WaveformGraphCollection();
            this.groupChannelSelectorPlaceholder = new WordGenerator.Controls.GpibGroupChannelSelection();
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
            this.groupChannelSelectorPanel.Size = new System.Drawing.Size(227, 477);
            this.groupChannelSelectorPanel.TabIndex = 0;
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
            // gpibGroupSelector
            // 
            this.gpibGroupSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gpibGroupSelector.FormattingEnabled = true;
            this.gpibGroupSelector.Location = new System.Drawing.Point(3, 3);
            this.gpibGroupSelector.Name = "gpibGroupSelector";
            this.gpibGroupSelector.Size = new System.Drawing.Size(110, 21);
            this.gpibGroupSelector.TabIndex = 6;
            this.gpibGroupSelector.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.gpibGroupSelector.DropDownClosed += new System.EventHandler(this.gpibGroupSelector_DropDownClosed);
            this.gpibGroupSelector.DropDown += new System.EventHandler(this.comboBox1_DropDown);
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
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 121);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 27);
            this.button2.TabIndex = 10;
            this.button2.Text = "Output Now";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // descBox
            // 
            this.descBox.Location = new System.Drawing.Point(3, 177);
            this.descBox.Multiline = true;
            this.descBox.Name = "descBox";
            this.descBox.Size = new System.Drawing.Size(164, 110);
            this.descBox.TabIndex = 11;
            this.descBox.TextChanged += new System.EventHandler(this.descBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Description:";
            // 
            // plus
            // 
            this.plus.Location = new System.Drawing.Point(119, 3);
            this.plus.Name = "plus";
            this.plus.Size = new System.Drawing.Size(22, 22);
            this.plus.TabIndex = 13;
            this.plus.Text = "+";
            this.plus.UseVisualStyleBackColor = true;
            this.plus.Click += new System.EventHandler(this.plus_Click);
            // 
            // minus
            // 
            this.minus.Location = new System.Drawing.Point(147, 3);
            this.minus.Name = "minus";
            this.minus.Size = new System.Drawing.Size(22, 22);
            this.minus.TabIndex = 14;
            this.minus.Text = "-";
            this.minus.UseVisualStyleBackColor = true;
            this.minus.Click += new System.EventHandler(this.minus_Click);
            // 
            // runOrderPanel
            // 
            this.runOrderPanel.AutoScroll = true;
            this.runOrderPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.runOrderPanel.Controls.Add(this.label3);
            this.runOrderPanel.Controls.Add(this.label2);
            this.runOrderPanel.Location = new System.Drawing.Point(6, 796);
            this.runOrderPanel.Name = "runOrderPanel";
            this.runOrderPanel.Size = new System.Drawing.Size(1234, 73);
            this.runOrderPanel.TabIndex = 19;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "GPIB Group Run Order:";
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 100;
            this.toolTip1.AutoPopDelay = 15000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 20;
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
            this.replacementGroupSelector.TabIndex = 22;
            this.replacementGroupSelector.SelectedValueChanged += new System.EventHandler(this.replacementGroupSelector_SelectedValueChanged);
            this.replacementGroupSelector.DropDown += new System.EventHandler(this.replacementSelector_DropDown_1);
            // 
            // replaceGroupButton
            // 
            this.replaceGroupButton.Enabled = false;
            this.replaceGroupButton.Location = new System.Drawing.Point(3, 289);
            this.replaceGroupButton.Name = "replaceGroupButton";
            this.replaceGroupButton.Size = new System.Drawing.Size(92, 22);
            this.replaceGroupButton.TabIndex = 21;
            this.replaceGroupButton.Text = "Replace Group";
            this.replaceGroupButton.UseVisualStyleBackColor = true;
            this.replaceGroupButton.Click += new System.EventHandler(this.replaceGroupButton_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(119, 56);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(76, 34);
            this.button3.TabIndex = 23;
            this.button3.Text = "Delete Unused Groups";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
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
            this.waveformGraphCollection1.Size = new System.Drawing.Size(767, 750);
            this.waveformGraphCollection1.TabIndex = 1;
            // 
            // groupChannelSelectorPlaceholder
            // 
            this.groupChannelSelectorPlaceholder.AutoSize = true;
            this.groupChannelSelectorPlaceholder.Location = new System.Drawing.Point(3, 3);
            this.groupChannelSelectorPlaceholder.Name = "groupChannelSelectorPlaceholder";
            this.groupChannelSelectorPlaceholder.Size = new System.Drawing.Size(207, 53);
            this.groupChannelSelectorPlaceholder.TabIndex = 0;
            this.groupChannelSelectorPlaceholder.Visible = false;
            // 
            // GpibGroupEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.replacementGroupSelector);
            this.Controls.Add(this.replaceGroupButton);
            this.Controls.Add(this.runOrderPanel);
            this.Controls.Add(this.minus);
            this.Controls.Add(this.plus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.descBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.renameButton);
            this.Controls.Add(this.renameTextBox);
            this.Controls.Add(this.gpibGroupSelector);
            this.Controls.Add(this.newGroupButton);
            this.Controls.Add(this.waveformEditor1);
            this.Controls.Add(this.waveformGraphCollection1);
            this.Controls.Add(this.groupChannelSelectorPanel);
            this.Name = "GpibGroupEditor";
            this.Size = new System.Drawing.Size(1264, 918);
            this.VisibleChanged += new System.EventHandler(this.GpibGroupEditor_VisibleChanged);
            this.groupChannelSelectorPanel.ResumeLayout(false);
            this.groupChannelSelectorPanel.PerformLayout();
            this.runOrderPanel.ResumeLayout(false);
            this.runOrderPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel groupChannelSelectorPanel;
        private WordGenerator.Controls.GpibGroupChannelSelection groupChannelSelectorPlaceholder;
        private WordGenerator.Controls.WaveformGraphCollection waveformGraphCollection1;
        private WordGenerator.Controls.WaveformEditor waveformEditor1;
        private System.Windows.Forms.Button newGroupButton;
        private System.Windows.Forms.ComboBox gpibGroupSelector;
        private System.Windows.Forms.TextBox renameTextBox;
        private System.Windows.Forms.Button renameButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox descBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button plus;
        private System.Windows.Forms.Button minus;
        private System.Windows.Forms.Panel runOrderPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox replacementGroupSelector;
        private System.Windows.Forms.Button replaceGroupButton;
        private System.Windows.Forms.Button button3;

    }
}
