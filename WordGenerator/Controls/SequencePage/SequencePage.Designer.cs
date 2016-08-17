namespace WordGenerator.Controls
{
    partial class SequencePage
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
            this.hideHiddenTimestepsCheckbox = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewTimestepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analogPreviewAutoUpdate = new System.Windows.Forms.CheckBox();
            this.analogPreviewUpdate = new System.Windows.Forms.Button();
            this.timeStepsPanel = new System.Windows.Forms.Panel();
            this.beginHintLabel = new System.Windows.Forms.Label();
            this.timeStepsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.seqNameBox = new System.Windows.Forms.TextBox();
            this.seqDescBox = new System.Windows.Forms.TextBox();
            this.sequenceNameLabel = new System.Windows.Forms.Label();
            this.sequenceDescriptionLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.createModeButton = new System.Windows.Forms.Button();
            this.destroyModeButton = new System.Windows.Forms.Button();
            this.storeMode = new System.Windows.Forms.Button();
            this.digitalOverridesCountLabel = new System.Windows.Forms.Label();
            this.analogOverridesCountLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.leftColumnPanel = new System.Windows.Forms.Panel();
            this.sequenceViewPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.upperRowPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.upperCornerPanel = new System.Windows.Forms.Panel();
            this.modeTextBox = new System.Windows.Forms.TextBox();
            this.modeBox = new System.Windows.Forms.ComboBox();
            this.digitalAnalogSplitContainer = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.digitalGridPanel = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.runControl1 = new WordGenerator.Controls.RunControl();
            this.analogChannelLabelsPanel = new WordGenerator.Controls.AnalogChannelLabelsPanel();
            this.analogPreviewPane = new WordGenerator.Controls.AnalogPreviewPane();
            this.digitalChannelLabelsPanel = new WordGenerator.Controls.DigitalChannelLabelsPanel();
            this.digitalGrid = new WordGenerator.Controls.DigitalGrid();
            this.contextMenuStrip1.SuspendLayout();
            this.timeStepsPanel.SuspendLayout();
            this.timeStepsFlowPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.leftColumnPanel.SuspendLayout();
            this.sequenceViewPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.upperRowPanel.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.upperCornerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.digitalAnalogSplitContainer)).BeginInit();
            this.digitalAnalogSplitContainer.Panel1.SuspendLayout();
            this.digitalAnalogSplitContainer.Panel2.SuspendLayout();
            this.digitalAnalogSplitContainer.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.digitalGridPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // hideHiddenTimestepsCheckbox
            // 
            this.hideHiddenTimestepsCheckbox.AutoSize = true;
            this.hideHiddenTimestepsCheckbox.ForeColor = System.Drawing.Color.White;
            this.hideHiddenTimestepsCheckbox.Location = new System.Drawing.Point(59, 58);
            this.hideHiddenTimestepsCheckbox.Name = "hideHiddenTimestepsCheckbox";
            this.hideHiddenTimestepsCheckbox.Size = new System.Drawing.Size(54, 17);
            this.hideHiddenTimestepsCheckbox.TabIndex = 5;
            this.hideHiddenTimestepsCheckbox.TabStop = false;
            this.hideHiddenTimestepsCheckbox.Text = "Hide?";
            this.hideHiddenTimestepsCheckbox.UseVisualStyleBackColor = true;
            this.hideHiddenTimestepsCheckbox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewTimestepToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(176, 26);
            // 
            // addNewTimestepToolStripMenuItem
            // 
            this.addNewTimestepToolStripMenuItem.Name = "addNewTimestepToolStripMenuItem";
            this.addNewTimestepToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.addNewTimestepToolStripMenuItem.Text = "Add New Timestep";
            this.addNewTimestepToolStripMenuItem.Click += new System.EventHandler(this.addNewTimestepToolStripMenuItem_Click);
            // 
            // analogPreviewAutoUpdate
            // 
            this.analogPreviewAutoUpdate.AutoSize = true;
            this.analogPreviewAutoUpdate.Checked = true;
            this.analogPreviewAutoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.analogPreviewAutoUpdate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.analogPreviewAutoUpdate.ForeColor = System.Drawing.Color.White;
            this.analogPreviewAutoUpdate.Location = new System.Drawing.Point(65, 216);
            this.analogPreviewAutoUpdate.Name = "analogPreviewAutoUpdate";
            this.analogPreviewAutoUpdate.Size = new System.Drawing.Size(48, 17);
            this.analogPreviewAutoUpdate.TabIndex = 5;
            this.analogPreviewAutoUpdate.Text = "Auto";
            this.analogPreviewAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // analogPreviewUpdate
            // 
            this.analogPreviewUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.analogPreviewUpdate.Location = new System.Drawing.Point(2, 212);
            this.analogPreviewUpdate.Name = "analogPreviewUpdate";
            this.analogPreviewUpdate.Size = new System.Drawing.Size(57, 23);
            this.analogPreviewUpdate.TabIndex = 4;
            this.analogPreviewUpdate.Text = "Update";
            this.analogPreviewUpdate.UseVisualStyleBackColor = true;
            this.analogPreviewUpdate.Click += new System.EventHandler(this.analogPreviewUpdate_Click);
            // 
            // timeStepsPanel
            // 
            this.timeStepsPanel.AutoScroll = true;
            this.timeStepsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.timeStepsPanel.ContextMenuStrip = this.contextMenuStrip1;
            this.timeStepsPanel.Controls.Add(this.timeStepsFlowPanel);
            this.timeStepsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeStepsPanel.Location = new System.Drawing.Point(130, 0);
            this.timeStepsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.timeStepsPanel.Name = "timeStepsPanel";
            this.timeStepsPanel.Size = new System.Drawing.Size(545, 240);
            this.timeStepsPanel.TabIndex = 2;
            this.timeStepsPanel.Text = "smoothScrollingControl1";
            this.timeStepsPanel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.timeStepsPanel_Scroll);
            // 
            // beginHintLabel
            // 
            this.beginHintLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.beginHintLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.beginHintLabel.ForeColor = System.Drawing.Color.White;
            this.beginHintLabel.Location = new System.Drawing.Point(3, 0);
            this.beginHintLabel.Name = "beginHintLabel";
            this.beginHintLabel.Size = new System.Drawing.Size(188, 175);
            this.beginHintLabel.TabIndex = 0;
            this.beginHintLabel.Text = "To begin, create a new Timestep by right clicking here.";
            this.beginHintLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timeStepsFlowPanel
            // 
            this.timeStepsFlowPanel.AutoSize = true;
            this.timeStepsFlowPanel.Controls.Add(this.beginHintLabel);
            this.timeStepsFlowPanel.Location = new System.Drawing.Point(0, 0);
            this.timeStepsFlowPanel.Margin = new System.Windows.Forms.Padding(0);
            this.timeStepsFlowPanel.Name = "timeStepsFlowPanel";
            this.timeStepsFlowPanel.Size = new System.Drawing.Size(200, 181);
            this.timeStepsFlowPanel.TabIndex = 0;
            this.timeStepsFlowPanel.SizeChanged += new System.EventHandler(this.repairAllMargins);
            // 
            // seqNameBox
            // 
            this.seqNameBox.BackColor = System.Drawing.Color.Gray;
            this.seqNameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.seqNameBox.ForeColor = System.Drawing.Color.White;
            this.seqNameBox.Location = new System.Drawing.Point(2, 373);
            this.seqNameBox.Name = "seqNameBox";
            this.seqNameBox.Size = new System.Drawing.Size(112, 20);
            this.seqNameBox.TabIndex = 2;
            this.seqNameBox.TextChanged += new System.EventHandler(this.seqNameBox_TextChanged);
            // 
            // seqDescBox
            // 
            this.seqDescBox.BackColor = System.Drawing.Color.Gray;
            this.seqDescBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.seqDescBox.ForeColor = System.Drawing.Color.White;
            this.seqDescBox.Location = new System.Drawing.Point(3, 416);
            this.seqDescBox.Multiline = true;
            this.seqDescBox.Name = "seqDescBox";
            this.seqDescBox.Size = new System.Drawing.Size(112, 191);
            this.seqDescBox.TabIndex = 3;
            this.seqDescBox.TextChanged += new System.EventHandler(this.seqDescBox_TextChanged);
            // 
            // sequenceNameLabel
            // 
            this.sequenceNameLabel.AutoSize = true;
            this.sequenceNameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sequenceNameLabel.ForeColor = System.Drawing.Color.White;
            this.sequenceNameLabel.Location = new System.Drawing.Point(13, 357);
            this.sequenceNameLabel.Name = "sequenceNameLabel";
            this.sequenceNameLabel.Size = new System.Drawing.Size(90, 13);
            this.sequenceNameLabel.TabIndex = 13;
            this.sequenceNameLabel.Text = "Sequence Name:";
            // 
            // sequenceDescriptionLabel
            // 
            this.sequenceDescriptionLabel.AutoSize = true;
            this.sequenceDescriptionLabel.ForeColor = System.Drawing.Color.White;
            this.sequenceDescriptionLabel.Location = new System.Drawing.Point(2, 400);
            this.sequenceDescriptionLabel.Name = "sequenceDescriptionLabel";
            this.sequenceDescriptionLabel.Size = new System.Drawing.Size(115, 13);
            this.sequenceDescriptionLabel.TabIndex = 14;
            this.sequenceDescriptionLabel.Text = "Sequence Description:";
            // 
            // createModeButton
            // 
            this.createModeButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.createModeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.createModeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.createModeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createModeButton.ForeColor = System.Drawing.Color.White;
            this.createModeButton.Location = new System.Drawing.Point(80, 82);
            this.createModeButton.Name = "createModeButton";
            this.createModeButton.Size = new System.Drawing.Size(20, 20);
            this.createModeButton.TabIndex = 18;
            this.createModeButton.Text = "+";
            this.toolTip1.SetToolTip(this.createModeButton, "Create new Mode.");
            this.createModeButton.UseVisualStyleBackColor = true;
            this.createModeButton.Click += new System.EventHandler(this.createMode_Click);
            // 
            // destroyModeButton
            // 
            this.destroyModeButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.destroyModeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.destroyModeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.destroyModeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.destroyModeButton.ForeColor = System.Drawing.Color.White;
            this.destroyModeButton.Location = new System.Drawing.Point(106, 82);
            this.destroyModeButton.Name = "destroyModeButton";
            this.destroyModeButton.Size = new System.Drawing.Size(20, 20);
            this.destroyModeButton.TabIndex = 19;
            this.destroyModeButton.Text = "-";
            this.toolTip1.SetToolTip(this.destroyModeButton, "Delete currently selected Mode.");
            this.destroyModeButton.UseVisualStyleBackColor = true;
            this.destroyModeButton.Click += new System.EventHandler(this.destroyMode_Click);
            // 
            // storeMode
            // 
            this.storeMode.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.storeMode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.storeMode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.storeMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.storeMode.ForeColor = System.Drawing.Color.White;
            this.storeMode.Location = new System.Drawing.Point(80, 107);
            this.storeMode.Name = "storeMode";
            this.storeMode.Size = new System.Drawing.Size(47, 33);
            this.storeMode.TabIndex = 21;
            this.storeMode.Text = "Store";
            this.toolTip1.SetToolTip(this.storeMode, "Store current Mode.");
            this.storeMode.UseVisualStyleBackColor = true;
            this.storeMode.Click += new System.EventHandler(this.storeMode_Click);
            // 
            // digitalOverridesCountLabel
            // 
            this.digitalOverridesCountLabel.AutoSize = true;
            this.digitalOverridesCountLabel.ForeColor = System.Drawing.Color.White;
            this.digitalOverridesCountLabel.Location = new System.Drawing.Point(3, 36);
            this.digitalOverridesCountLabel.Name = "digitalOverridesCountLabel";
            this.digitalOverridesCountLabel.Size = new System.Drawing.Size(35, 13);
            this.digitalOverridesCountLabel.TabIndex = 15;
            this.digitalOverridesCountLabel.Text = "label3";
            // 
            // analogOverridesCountLabel
            // 
            this.analogOverridesCountLabel.AutoSize = true;
            this.analogOverridesCountLabel.ForeColor = System.Drawing.Color.White;
            this.analogOverridesCountLabel.Location = new System.Drawing.Point(3, 13);
            this.analogOverridesCountLabel.Name = "analogOverridesCountLabel";
            this.analogOverridesCountLabel.Size = new System.Drawing.Size(35, 13);
            this.analogOverridesCountLabel.TabIndex = 16;
            this.analogOverridesCountLabel.Text = "label4";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.leftColumnPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.sequenceViewPanel, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // leftColumnPanel
            // 
            this.leftColumnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.leftColumnPanel.Controls.Add(this.runControl1);
            this.leftColumnPanel.Controls.Add(this.sequenceNameLabel);
            this.leftColumnPanel.Controls.Add(this.seqNameBox);
            this.leftColumnPanel.Controls.Add(this.sequenceDescriptionLabel);
            this.leftColumnPanel.Controls.Add(this.seqDescBox);
            this.leftColumnPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftColumnPanel.Location = new System.Drawing.Point(3, 3);
            this.leftColumnPanel.Name = "leftColumnPanel";
            this.leftColumnPanel.Size = new System.Drawing.Size(119, 594);
            this.leftColumnPanel.TabIndex = 0;
            // 
            // sequenceViewPanel
            // 
            this.sequenceViewPanel.Controls.Add(this.tableLayoutPanel2);
            this.sequenceViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sequenceViewPanel.Location = new System.Drawing.Point(125, 0);
            this.sequenceViewPanel.Margin = new System.Windows.Forms.Padding(0);
            this.sequenceViewPanel.Name = "sequenceViewPanel";
            this.sequenceViewPanel.Size = new System.Drawing.Size(675, 600);
            this.sequenceViewPanel.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.upperRowPanel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.digitalAnalogSplitContainer, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(675, 600);
            this.tableLayoutPanel2.TabIndex = 17;
            // 
            // upperRowPanel
            // 
            this.upperRowPanel.Controls.Add(this.tableLayoutPanel4);
            this.upperRowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.upperRowPanel.Location = new System.Drawing.Point(0, 0);
            this.upperRowPanel.Margin = new System.Windows.Forms.Padding(0);
            this.upperRowPanel.Name = "upperRowPanel";
            this.upperRowPanel.Size = new System.Drawing.Size(675, 240);
            this.upperRowPanel.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.timeStepsPanel, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.upperCornerPanel, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(675, 240);
            this.tableLayoutPanel4.TabIndex = 17;
            // 
            // upperCornerPanel
            // 
            this.upperCornerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.upperCornerPanel.Controls.Add(this.storeMode);
            this.upperCornerPanel.Controls.Add(this.modeTextBox);
            this.upperCornerPanel.Controls.Add(this.destroyModeButton);
            this.upperCornerPanel.Controls.Add(this.createModeButton);
            this.upperCornerPanel.Controls.Add(this.modeBox);
            this.upperCornerPanel.Controls.Add(this.digitalOverridesCountLabel);
            this.upperCornerPanel.Controls.Add(this.analogPreviewAutoUpdate);
            this.upperCornerPanel.Controls.Add(this.analogOverridesCountLabel);
            this.upperCornerPanel.Controls.Add(this.analogPreviewUpdate);
            this.upperCornerPanel.Controls.Add(this.hideHiddenTimestepsCheckbox);
            this.upperCornerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.upperCornerPanel.Location = new System.Drawing.Point(0, 0);
            this.upperCornerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.upperCornerPanel.Name = "upperCornerPanel";
            this.upperCornerPanel.Size = new System.Drawing.Size(130, 240);
            this.upperCornerPanel.TabIndex = 3;
            // 
            // modeTextBox
            // 
            this.modeTextBox.BackColor = System.Drawing.Color.Gray;
            this.modeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.modeTextBox.ForeColor = System.Drawing.Color.White;
            this.modeTextBox.Location = new System.Drawing.Point(5, 108);
            this.modeTextBox.Name = "modeTextBox";
            this.modeTextBox.Size = new System.Drawing.Size(73, 20);
            this.modeTextBox.TabIndex = 20;
            // 
            // modeBox
            // 
            this.modeBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.modeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.modeBox.ForeColor = System.Drawing.Color.White;
            this.modeBox.FormattingEnabled = true;
            this.modeBox.Location = new System.Drawing.Point(5, 81);
            this.modeBox.Name = "modeBox";
            this.modeBox.Size = new System.Drawing.Size(73, 21);
            this.modeBox.TabIndex = 17;
            this.modeBox.SelectedIndexChanged += new System.EventHandler(this.modeBox_SelectedIndexChanged);
            // 
            // digitalAnalogSplitContainer
            // 
            this.digitalAnalogSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.digitalAnalogSplitContainer.Location = new System.Drawing.Point(3, 243);
            this.digitalAnalogSplitContainer.Name = "digitalAnalogSplitContainer";
            this.digitalAnalogSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // digitalAnalogSplitContainer.Panel1
            // 
            this.digitalAnalogSplitContainer.Panel1.Controls.Add(this.tableLayoutPanel3);
            // 
            // digitalAnalogSplitContainer.Panel2
            // 
            this.digitalAnalogSplitContainer.Panel2.Controls.Add(this.tableLayoutPanel5);
            this.digitalAnalogSplitContainer.Size = new System.Drawing.Size(669, 354);
            this.digitalAnalogSplitContainer.SplitterDistance = 147;
            this.digitalAnalogSplitContainer.TabIndex = 1;
            this.digitalAnalogSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.analogChannelLabelsPanel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.analogPreviewPane, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 147F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(669, 147);
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.digitalChannelLabelsPanel, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.digitalGridPanel, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(669, 203);
            this.tableLayoutPanel5.TabIndex = 8;
            // 
            // digitalGridPanel
            // 
            this.digitalGridPanel.AutoScroll = true;
            this.digitalGridPanel.Controls.Add(this.digitalGrid);
            this.digitalGridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.digitalGridPanel.Location = new System.Drawing.Point(130, 0);
            this.digitalGridPanel.Margin = new System.Windows.Forms.Padding(0);
            this.digitalGridPanel.Name = "digitalGridPanel";
            this.digitalGridPanel.Size = new System.Drawing.Size(539, 203);
            this.digitalGridPanel.TabIndex = 8;
            this.digitalGridPanel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.digitalGridPanel_Scroll);
            this.digitalGridPanel.SizeChanged += new System.EventHandler(this.digitalGridPanel_SizeChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // runControl1
            // 
            this.runControl1.IsRunNoSaveEnabled = true;
            this.runControl1.Location = new System.Drawing.Point(2, 3);
            this.runControl1.Name = "runControl1";
            this.runControl1.Size = new System.Drawing.Size(119, 351);
            this.runControl1.TabIndex = 1;
            // 
            // analogChannelLabelsPanel
            // 
            this.analogChannelLabelsPanel.AutoScroll = true;
            this.analogChannelLabelsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.analogChannelLabelsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.analogChannelLabelsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analogChannelLabelsPanel.Location = new System.Drawing.Point(0, 0);
            this.analogChannelLabelsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.analogChannelLabelsPanel.Name = "analogChannelLabelsPanel";
            this.analogChannelLabelsPanel.Size = new System.Drawing.Size(130, 147);
            this.analogChannelLabelsPanel.TabIndex = 6;
            this.analogChannelLabelsPanel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.analogChannelLabelsPanel1_Scroll);
            this.analogChannelLabelsPanel.Enter += new System.EventHandler(this.analogChannelLabelsPanel1_Enter);
            // 
            // analogPreviewPane
            // 
            this.analogPreviewPane.AutoScroll = true;
            this.analogPreviewPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analogPreviewPane.Location = new System.Drawing.Point(130, 0);
            this.analogPreviewPane.Margin = new System.Windows.Forms.Padding(0);
            this.analogPreviewPane.Name = "analogPreviewPane";
            this.analogPreviewPane.Size = new System.Drawing.Size(539, 147);
            this.analogPreviewPane.TabIndex = 6;
            this.analogPreviewPane.TabStop = false;
            this.analogPreviewPane.Load += new System.EventHandler(this.analogPreviewPane_Load);
            this.analogPreviewPane.Scroll += new System.Windows.Forms.ScrollEventHandler(this.analogPreviewPane1_Scroll);
            this.analogPreviewPane.Click += new System.EventHandler(this.analogPreviewPane1_Click);
            // 
            // digitalChannelLabelsPanel
            // 
            this.digitalChannelLabelsPanel.AutoScroll = true;
            this.digitalChannelLabelsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.digitalChannelLabelsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.digitalChannelLabelsPanel.Location = new System.Drawing.Point(0, 0);
            this.digitalChannelLabelsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.digitalChannelLabelsPanel.Name = "digitalChannelLabelsPanel";
            this.digitalChannelLabelsPanel.Size = new System.Drawing.Size(130, 203);
            this.digitalChannelLabelsPanel.TabIndex = 7;
            this.digitalChannelLabelsPanel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.digitalChannelLabelsPanel1_Scroll);
            this.digitalChannelLabelsPanel.Enter += new System.EventHandler(this.digitalChannelLabelsPanel1_Enter);
            // 
            // digitalGrid
            // 
            this.digitalGrid.AutoScroll = true;
            this.digitalGrid.BackColor = System.Drawing.Color.Gray;
            this.digitalGrid.ContainerSize = new System.Drawing.Size(0, 0);
            this.digitalGrid.Location = new System.Drawing.Point(0, 0);
            this.digitalGrid.Margin = new System.Windows.Forms.Padding(0);
            this.digitalGrid.Name = "digitalGrid";
            this.digitalGrid.RowHeight = 0;
            this.digitalGrid.Size = new System.Drawing.Size(539, 203);
            this.digitalGrid.TabIndex = 3;
            this.digitalGrid.TabStop = false;
            this.digitalGrid.Load += new System.EventHandler(this.digitalGrid_Load);
            // 
            // SequencePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SequencePage";
            this.Size = new System.Drawing.Size(800, 600);
            this.SizeChanged += new System.EventHandler(this.SequencePage_SizeChanged);
            this.Click += new System.EventHandler(this.SequencePage_Click);
            this.Enter += new System.EventHandler(this.SequencePage_Enter);
            this.contextMenuStrip1.ResumeLayout(false);
            this.timeStepsPanel.ResumeLayout(false);
            this.timeStepsPanel.PerformLayout();
            this.timeStepsFlowPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.leftColumnPanel.ResumeLayout(false);
            this.leftColumnPanel.PerformLayout();
            this.sequenceViewPanel.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.upperRowPanel.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.upperCornerPanel.ResumeLayout(false);
            this.upperCornerPanel.PerformLayout();
            this.digitalAnalogSplitContainer.Panel1.ResumeLayout(false);
            this.digitalAnalogSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.digitalAnalogSplitContainer)).EndInit();
            this.digitalAnalogSplitContainer.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.digitalGridPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public RunControl runControl1;
        private System.Windows.Forms.CheckBox hideHiddenTimestepsCheckbox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addNewTimestepToolStripMenuItem;
        private System.Windows.Forms.CheckBox analogPreviewAutoUpdate;
        private System.Windows.Forms.Button analogPreviewUpdate;
        private System.Windows.Forms.TextBox seqNameBox;
        private System.Windows.Forms.TextBox seqDescBox;
        private System.Windows.Forms.Label sequenceNameLabel;
        private System.Windows.Forms.Label sequenceDescriptionLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Panel timeStepsPanel;
        public DigitalGrid digitalGrid;
        public AnalogPreviewPane analogPreviewPane;
        public DigitalChannelLabelsPanel digitalChannelLabelsPanel;
        public AnalogChannelLabelsPanel analogChannelLabelsPanel;
        private System.Windows.Forms.Label digitalOverridesCountLabel;
        private System.Windows.Forms.Label analogOverridesCountLabel;
        public System.Windows.Forms.FlowLayoutPanel timeStepsFlowPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel leftColumnPanel;
        private System.Windows.Forms.Panel sequenceViewPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel upperRowPanel;
        private System.Windows.Forms.SplitContainer digitalAnalogSplitContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel upperCornerPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel digitalGridPanel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button createModeButton;
        private System.Windows.Forms.Button destroyModeButton;
        private System.Windows.Forms.Button storeMode;
        private System.Windows.Forms.TextBox modeTextBox;
        public System.Windows.Forms.ComboBox modeBox;
        private System.Windows.Forms.Label beginHintLabel;
    }
}
