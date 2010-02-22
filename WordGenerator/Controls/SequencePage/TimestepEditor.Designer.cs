namespace WordGenerator.Controls
{
    partial class TimestepEditor
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
            this.timeStepNumber = new System.Windows.Forms.Label();
            this.timestepName = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.outputNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.waitForRetriggerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.insertTimestepBeforeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertTimestepAfterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveTimestepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToTimestepCombobox = new System.Windows.Forms.ToolStripComboBox();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.setTimestepHotkeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hotkeyEntryTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.removeTimestepHotkeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.viewDescMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDescMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.descriptionTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.separator = new System.Windows.Forms.ToolStripSeparator();
            this.mark = new System.Windows.Forms.ToolStripMenuItem();
            this.unmark = new System.Windows.Forms.ToolStripMenuItem();
            this.markall = new System.Windows.Forms.ToolStripMenuItem();
            this.unmarkall = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.timestepGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timestepGroupComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.enabledButton = new System.Windows.Forms.Button();
            this.analogSelector = new System.Windows.Forms.ComboBox();
            this.gpibSelector = new System.Windows.Forms.ComboBox();
            this.rs232Selector = new System.Windows.Forms.ComboBox();
            this.showHideButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pulseIndicator = new System.Windows.Forms.Label();
            this.waitLabel = new System.Windows.Forms.Label();
            this.timestepGroupIndicatorLabel = new System.Windows.Forms.Label();
            this.analogPictureBox = new System.Windows.Forms.PictureBox();
            this.gpibPictureBox = new System.Windows.Forms.PictureBox();
            this.rs232PictureBox = new System.Windows.Forms.PictureBox();
            this.durationEditor = new WordGenerator.Controls.VerticalParameterEditor();
            this.setDigitalsToContinue = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.analogPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpibPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rs232PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // timeStepNumber
            // 
            this.timeStepNumber.AutoSize = true;
            this.timeStepNumber.Location = new System.Drawing.Point(0, 0);
            this.timeStepNumber.Name = "timeStepNumber";
            this.timeStepNumber.Size = new System.Drawing.Size(14, 13);
            this.timeStepNumber.TabIndex = 0;
            this.timeStepNumber.Text = "#";
            this.timeStepNumber.Visible = false;
            // 
            // timestepName
            // 
            this.timestepName.ContextMenuStrip = this.contextMenuStrip1;
            this.timestepName.Location = new System.Drawing.Point(0, 16);
            this.timestepName.Name = "timestepName";
            this.timestepName.Size = new System.Drawing.Size(84, 20);
            this.timestepName.TabIndex = 1;
            this.timestepName.Text = "Name";
            this.timestepName.Visible = false;
            this.timestepName.TextChanged += new System.EventHandler(this.timestepName_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.outputNowToolStripMenuItem,
            this.toolStripSeparator4,
            this.waitForRetriggerMenuItem,
            this.toolStripSeparator2,
            this.insertTimestepBeforeToolStripMenuItem,
            this.insertTimestepAfterToolStripMenuItem,
            this.duplicateToolStripMenuItem,
            this.moveTimestepToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.setTimestepHotkeyToolStripMenuItem,
            this.removeTimestepHotkeyToolStripMenuItem,
            this.toolStripSeparator3,
            this.viewDescMenuItem,
            this.setDescMenuItem,
            this.separator,
            this.mark,
            this.unmark,
            this.markall,
            this.unmarkall,
            this.toolStripSeparator6,
            this.setDigitalsToContinue,
            this.toolStripSeparator5,
            this.timestepGroupToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(250, 442);
            // 
            // outputNowToolStripMenuItem
            // 
            this.outputNowToolStripMenuItem.Name = "outputNowToolStripMenuItem";
            this.outputNowToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.outputNowToolStripMenuItem.Text = "<Output Now>";
            this.outputNowToolStripMenuItem.Click += new System.EventHandler(this.outputNowToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(246, 6);
            // 
            // waitForRetriggerMenuItem
            // 
            this.waitForRetriggerMenuItem.Name = "waitForRetriggerMenuItem";
            this.waitForRetriggerMenuItem.Size = new System.Drawing.Size(249, 22);
            this.waitForRetriggerMenuItem.Text = "Enable Wait-for-retrigger";
            this.waitForRetriggerMenuItem.Click += new System.EventHandler(this.waitForRetriggerMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(246, 6);
            // 
            // insertTimestepBeforeToolStripMenuItem
            // 
            this.insertTimestepBeforeToolStripMenuItem.Name = "insertTimestepBeforeToolStripMenuItem";
            this.insertTimestepBeforeToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.insertTimestepBeforeToolStripMenuItem.Text = "Insert Timestep Before";
            this.insertTimestepBeforeToolStripMenuItem.Click += new System.EventHandler(this.insertTimestepBeforeToolStripMenuItem_Click);
            // 
            // insertTimestepAfterToolStripMenuItem
            // 
            this.insertTimestepAfterToolStripMenuItem.Name = "insertTimestepAfterToolStripMenuItem";
            this.insertTimestepAfterToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.insertTimestepAfterToolStripMenuItem.Text = "Insert Timestep After";
            this.insertTimestepAfterToolStripMenuItem.Click += new System.EventHandler(this.insertTimestepAfterToolStripMenuItem_Click);
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.duplicateToolStripMenuItem.Text = "Duplicate Timestep";
            this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.duplicateToolStripMenuItem_Click);
            // 
            // moveTimestepToolStripMenuItem
            // 
            this.moveTimestepToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.moveToTimestepCombobox});
            this.moveTimestepToolStripMenuItem.Name = "moveTimestepToolStripMenuItem";
            this.moveTimestepToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.moveTimestepToolStripMenuItem.Text = "Move timestep";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(206, 22);
            this.toolStripMenuItem2.Text = "Select Destination Index:";
            // 
            // moveToTimestepCombobox
            // 
            this.moveToTimestepCombobox.Name = "moveToTimestepCombobox";
            this.moveToTimestepCombobox.Size = new System.Drawing.Size(121, 21);
            this.moveToTimestepCombobox.DropDown += new System.EventHandler(this.moveToTimestepCombobox_DropDown);
            this.moveToTimestepCombobox.DropDownClosed += new System.EventHandler(this.moveToTimestepCombobox_DropDownClosed);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(246, 6);
            // 
            // setTimestepHotkeyToolStripMenuItem
            // 
            this.setTimestepHotkeyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.hotkeyEntryTextBox});
            this.setTimestepHotkeyToolStripMenuItem.Name = "setTimestepHotkeyToolStripMenuItem";
            this.setTimestepHotkeyToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.setTimestepHotkeyToolStripMenuItem.Text = "Set timestep hotkey";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem1.Text = "CTRL + ";
            // 
            // hotkeyEntryTextBox
            // 
            this.hotkeyEntryTextBox.Name = "hotkeyEntryTextBox";
            this.hotkeyEntryTextBox.Size = new System.Drawing.Size(100, 21);
            this.hotkeyEntryTextBox.TextChanged += new System.EventHandler(this.hotkeyEntryTextBox_TextChanged);
            // 
            // removeTimestepHotkeyToolStripMenuItem
            // 
            this.removeTimestepHotkeyToolStripMenuItem.Name = "removeTimestepHotkeyToolStripMenuItem";
            this.removeTimestepHotkeyToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.removeTimestepHotkeyToolStripMenuItem.Text = "Remove timestep hotkey";
            this.removeTimestepHotkeyToolStripMenuItem.Click += new System.EventHandler(this.removeTimestepHotkeyToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(246, 6);
            // 
            // viewDescMenuItem
            // 
            this.viewDescMenuItem.Name = "viewDescMenuItem";
            this.viewDescMenuItem.Size = new System.Drawing.Size(249, 22);
            this.viewDescMenuItem.Text = "View Timestep Description";
            this.viewDescMenuItem.Click += new System.EventHandler(this.viewDescMenuItem_Click);
            // 
            // setDescMenuItem
            // 
            this.setDescMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.descriptionTextBox});
            this.setDescMenuItem.Name = "setDescMenuItem";
            this.setDescMenuItem.Size = new System.Drawing.Size(249, 22);
            this.setDescMenuItem.Text = "Set Timestep Description";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItem5.Text = "Enter description:";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(100, 21);
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.descriptionTextChanged);
            // 
            // separator
            // 
            this.separator.Name = "separator";
            this.separator.Size = new System.Drawing.Size(246, 6);
            // 
            // mark
            // 
            this.mark.Name = "mark";
            this.mark.Size = new System.Drawing.Size(249, 22);
            this.mark.Text = "Mark";
            this.mark.Click += new System.EventHandler(this.mark_Click);
            // 
            // unmark
            // 
            this.unmark.Name = "unmark";
            this.unmark.Size = new System.Drawing.Size(249, 22);
            this.unmark.Text = "Unmark";
            this.unmark.Click += new System.EventHandler(this.unmark_Click);
            // 
            // markall
            // 
            this.markall.Name = "markall";
            this.markall.Size = new System.Drawing.Size(249, 22);
            this.markall.Text = "Mark All";
            this.markall.Click += new System.EventHandler(this.markall_Click);
            // 
            // unmarkall
            // 
            this.unmarkall.Name = "unmarkall";
            this.unmarkall.Size = new System.Drawing.Size(249, 22);
            this.unmarkall.Text = "Unmark All";
            this.unmarkall.Click += new System.EventHandler(this.unmarkall_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(246, 6);
            // 
            // timestepGroupToolStripMenuItem
            // 
            this.timestepGroupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timestepGroupComboBox});
            this.timestepGroupToolStripMenuItem.Name = "timestepGroupToolStripMenuItem";
            this.timestepGroupToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.timestepGroupToolStripMenuItem.Text = "Timestep Group";
            this.timestepGroupToolStripMenuItem.DropDownOpening += new System.EventHandler(this.timestepGroupToolStripMenuItem_DropDownOpening);
            // 
            // timestepGroupComboBox
            // 
            this.timestepGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timestepGroupComboBox.Name = "timestepGroupComboBox";
            this.timestepGroupComboBox.Size = new System.Drawing.Size(121, 21);
            this.timestepGroupComboBox.SelectedIndexChanged += new System.EventHandler(this.timestepGroupComboBox_SelectedIndexChanged);
            // 
            // enabledButton
            // 
            this.enabledButton.BackColor = System.Drawing.Color.Green;
            this.enabledButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.enabledButton.Location = new System.Drawing.Point(0, 38);
            this.enabledButton.Name = "enabledButton";
            this.enabledButton.Size = new System.Drawing.Size(84, 22);
            this.enabledButton.TabIndex = 2;
            this.enabledButton.Text = "Enabled";
            this.enabledButton.UseVisualStyleBackColor = false;
            this.enabledButton.Visible = false;
            this.enabledButton.Click += new System.EventHandler(this.enabledButton_Click);
            // 
            // analogSelector
            // 
            this.analogSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.analogSelector.FormattingEnabled = true;
            this.analogSelector.Location = new System.Drawing.Point(0, 142);
            this.analogSelector.Name = "analogSelector";
            this.analogSelector.Size = new System.Drawing.Size(84, 21);
            this.analogSelector.TabIndex = 5;
            this.analogSelector.Visible = false;
            this.analogSelector.SelectedValueChanged += new System.EventHandler(this.analogSelector_SelectedValueChanged);
            this.analogSelector.DropDownClosed += new System.EventHandler(this.analogSelector_DropDownClosed);
            this.analogSelector.DropDown += new System.EventHandler(this.analogSelector_DropDown);
            // 
            // gpibSelector
            // 
            this.gpibSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gpibSelector.FormattingEnabled = true;
            this.gpibSelector.Location = new System.Drawing.Point(0, 168);
            this.gpibSelector.Name = "gpibSelector";
            this.gpibSelector.Size = new System.Drawing.Size(84, 21);
            this.gpibSelector.TabIndex = 6;
            this.gpibSelector.Visible = false;
            this.gpibSelector.SelectedValueChanged += new System.EventHandler(this.gpibSelector_SelectedValueChanged);
            this.gpibSelector.DropDownClosed += new System.EventHandler(this.gpibSelector_DropDownClosed);
            this.gpibSelector.DropDown += new System.EventHandler(this.gpibSelector_DropDown);
            // 
            // rs232Selector
            // 
            this.rs232Selector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rs232Selector.FormattingEnabled = true;
            this.rs232Selector.Location = new System.Drawing.Point(0, 194);
            this.rs232Selector.Name = "rs232Selector";
            this.rs232Selector.Size = new System.Drawing.Size(84, 21);
            this.rs232Selector.TabIndex = 7;
            this.rs232Selector.Visible = false;
            this.rs232Selector.SelectedValueChanged += new System.EventHandler(this.rs232Selector_SelectedValueChanged);
            this.rs232Selector.DropDownClosed += new System.EventHandler(this.rs232Selector_DropDownClosed);
            this.rs232Selector.DropDown += new System.EventHandler(this.rs232Selector_DropDown);
            // 
            // showHideButton
            // 
            this.showHideButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showHideButton.Location = new System.Drawing.Point(0, 63);
            this.showHideButton.Name = "showHideButton";
            this.showHideButton.Size = new System.Drawing.Size(84, 22);
            this.showHideButton.TabIndex = 3;
            this.showHideButton.Text = "Hide";
            this.showHideButton.UseVisualStyleBackColor = true;
            this.showHideButton.Visible = false;
            this.showHideButton.Click += new System.EventHandler(this.showHideButton_Click);
            // 
            // pulseIndicator
            // 
            this.pulseIndicator.AutoSize = true;
            this.pulseIndicator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pulseIndicator.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pulseIndicator.ForeColor = System.Drawing.Color.Purple;
            this.pulseIndicator.Location = new System.Drawing.Point(73, 0);
            this.pulseIndicator.Name = "pulseIndicator";
            this.pulseIndicator.Size = new System.Drawing.Size(11, 12);
            this.pulseIndicator.TabIndex = 9;
            this.pulseIndicator.Text = "P";
            this.pulseIndicator.Visible = false;
            // 
            // waitLabel
            // 
            this.waitLabel.AutoSize = true;
            this.waitLabel.BackColor = System.Drawing.Color.Red;
            this.waitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.waitLabel.ForeColor = System.Drawing.Color.Purple;
            this.waitLabel.Location = new System.Drawing.Point(59, 0);
            this.waitLabel.Name = "waitLabel";
            this.waitLabel.Size = new System.Drawing.Size(14, 12);
            this.waitLabel.TabIndex = 10;
            this.waitLabel.Text = "W";
            // 
            // timestepGroupIndicatorLabel
            // 
            this.timestepGroupIndicatorLabel.AutoSize = true;
            this.timestepGroupIndicatorLabel.BackColor = System.Drawing.Color.Blue;
            this.timestepGroupIndicatorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.timestepGroupIndicatorLabel.ForeColor = System.Drawing.Color.White;
            this.timestepGroupIndicatorLabel.Location = new System.Drawing.Point(47, 0);
            this.timestepGroupIndicatorLabel.Name = "timestepGroupIndicatorLabel";
            this.timestepGroupIndicatorLabel.Size = new System.Drawing.Size(12, 12);
            this.timestepGroupIndicatorLabel.TabIndex = 11;
            this.timestepGroupIndicatorLabel.Text = "G";
            // 
            // analogPictureBox
            // 
            this.analogPictureBox.Image = global::WordGenerator.Properties.Resources.continueImage;
            this.analogPictureBox.Location = new System.Drawing.Point(0, 142);
            this.analogPictureBox.Name = "analogPictureBox";
            this.analogPictureBox.Size = new System.Drawing.Size(84, 21);
            this.analogPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.analogPictureBox.TabIndex = 12;
            this.analogPictureBox.TabStop = false;
            this.analogPictureBox.Visible = false;
            this.analogPictureBox.MouseEnter += new System.EventHandler(this.analogPictureBox_MouseEnter);
            // 
            // gpibPictureBox
            // 
            this.gpibPictureBox.Image = global::WordGenerator.Properties.Resources.continueImage;
            this.gpibPictureBox.Location = new System.Drawing.Point(0, 168);
            this.gpibPictureBox.Name = "gpibPictureBox";
            this.gpibPictureBox.Size = new System.Drawing.Size(84, 21);
            this.gpibPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.gpibPictureBox.TabIndex = 13;
            this.gpibPictureBox.TabStop = false;
            this.gpibPictureBox.Visible = false;
            this.gpibPictureBox.MouseEnter += new System.EventHandler(this.gpibPictureBox_MouseEnter);
            // 
            // rs232PictureBox
            // 
            this.rs232PictureBox.Image = global::WordGenerator.Properties.Resources.continueImage;
            this.rs232PictureBox.Location = new System.Drawing.Point(0, 194);
            this.rs232PictureBox.Name = "rs232PictureBox";
            this.rs232PictureBox.Size = new System.Drawing.Size(84, 21);
            this.rs232PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.rs232PictureBox.TabIndex = 14;
            this.rs232PictureBox.TabStop = false;
            this.rs232PictureBox.Visible = false;
            this.rs232PictureBox.MouseEnter += new System.EventHandler(this.rs232PictureBox_MouseEnter);
            // 
            // durationEditor
            // 
            this.durationEditor.Location = new System.Drawing.Point(3, 90);
            this.durationEditor.Name = "durationEditor";
            this.durationEditor.Size = new System.Drawing.Size(81, 49);
            this.durationEditor.TabIndex = 4;
            this.durationEditor.UnitSelectorVisibility = true;
            this.durationEditor.Visible = false;
            this.durationEditor.updateGUI += new System.EventHandler(this.durationEditor_updateGUI);
            // 
            // setDigitalsToContinue
            // 
            this.setDigitalsToContinue.Name = "setDigitalsToContinue";
            this.setDigitalsToContinue.Size = new System.Drawing.Size(249, 22);
            this.setDigitalsToContinue.Text = "Set all digital channels to Continue";
            this.setDigitalsToContinue.Click += new System.EventHandler(this.setDigitalsToContinue_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(246, 6);
            // 
            // TimestepEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.timestepGroupIndicatorLabel);
            this.Controls.Add(this.waitLabel);
            this.Controls.Add(this.pulseIndicator);
            this.Controls.Add(this.showHideButton);
            this.Controls.Add(this.analogSelector);
            this.Controls.Add(this.rs232Selector);
            this.Controls.Add(this.gpibSelector);
            this.Controls.Add(this.timeStepNumber);
            this.Controls.Add(this.durationEditor);
            this.Controls.Add(this.enabledButton);
            this.Controls.Add(this.timestepName);
            this.Controls.Add(this.analogPictureBox);
            this.Controls.Add(this.gpibPictureBox);
            this.Controls.Add(this.rs232PictureBox);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TimestepEditor";
            this.Size = new System.Drawing.Size(86, 219);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.TimestepEditor_Layout);
            this.Enter += new System.EventHandler(this.TimestepEditor_Enter);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.analogPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpibPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rs232PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label timeStepNumber;
        private System.Windows.Forms.TextBox timestepName;
        private System.Windows.Forms.Button enabledButton;
        private WordGenerator.Controls.VerticalParameterEditor durationEditor;
        private System.Windows.Forms.ComboBox analogSelector;
        private System.Windows.Forms.ComboBox gpibSelector;
        private System.Windows.Forms.ComboBox rs232Selector;
        private System.Windows.Forms.Button showHideButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem outputNowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertTimestepBeforeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertTimestepAfterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveTimestepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setTimestepHotkeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeTimestepHotkeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox hotkeyEntryTextBox;
        private System.Windows.Forms.ToolStripComboBox moveToTimestepCombobox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem viewDescMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDescMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripTextBox descriptionTextBox;
        private System.Windows.Forms.Label pulseIndicator;
        private System.Windows.Forms.ToolStripSeparator separator;
        private System.Windows.Forms.ToolStripMenuItem mark;
        private System.Windows.Forms.ToolStripMenuItem unmark;
        private System.Windows.Forms.ToolStripMenuItem markall;
        private System.Windows.Forms.ToolStripMenuItem unmarkall;
        private System.Windows.Forms.Label waitLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem waitForRetriggerMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem timestepGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox timestepGroupComboBox;
        private System.Windows.Forms.Label timestepGroupIndicatorLabel;
        private System.Windows.Forms.PictureBox analogPictureBox;
        private System.Windows.Forms.PictureBox gpibPictureBox;
        private System.Windows.Forms.PictureBox rs232PictureBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem setDigitalsToContinue;
    }
}
