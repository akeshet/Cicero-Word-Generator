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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewTimestepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analogPreviewAutoUpdate = new System.Windows.Forms.CheckBox();
            this.analogPreviewUpdate = new System.Windows.Forms.Button();
            this.timeStepsPanel = new System.Windows.Forms.Panel();
            this.timeStepsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.seqNameBox = new System.Windows.Forms.TextBox();
            this.seqDescBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.createMode = new System.Windows.Forms.Button();
            this.destroyMode = new System.Windows.Forms.Button();
            this.storeMode = new System.Windows.Forms.Button();
            this.digitalOverridesCountLabel = new System.Windows.Forms.Label();
            this.analogOverridesCountLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.modeTextBox = new System.Windows.Forms.TextBox();
            this.modeBox = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.digitalGridPanel = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.beginHintLabel = new System.Windows.Forms.Label();
            this.runControl1 = new WordGenerator.Controls.RunControl();
            this.analogChannelLabelsPanel1 = new WordGenerator.Controls.AnalogChannelLabelsPanel();
            this.analogPreviewPane1 = new WordGenerator.Controls.AnalogPreviewPane();
            this.digitalChannelLabelsPanel1 = new WordGenerator.Controls.DigitalChannelLabelsPanel();
            this.digitalGrid1 = new WordGenerator.Controls.DigitalGrid();
            this.contextMenuStrip1.SuspendLayout();
            this.timeStepsPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel4.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.digitalGridPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(59, 58);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(54, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.TabStop = false;
            this.checkBox1.Text = "Hide?";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
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
            this.analogPreviewAutoUpdate.Location = new System.Drawing.Point(65, 216);
            this.analogPreviewAutoUpdate.Name = "analogPreviewAutoUpdate";
            this.analogPreviewAutoUpdate.Size = new System.Drawing.Size(48, 17);
            this.analogPreviewAutoUpdate.TabIndex = 5;
            this.analogPreviewAutoUpdate.Text = "Auto";
            this.analogPreviewAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // analogPreviewUpdate
            // 
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
            this.timeStepsPanel.ContextMenuStrip = this.contextMenuStrip1;
            this.timeStepsPanel.Controls.Add(this.beginHintLabel);
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
            // timeStepsFlowPanel
            // 
            this.timeStepsFlowPanel.AutoSize = true;
            this.timeStepsFlowPanel.Location = new System.Drawing.Point(0, 0);
            this.timeStepsFlowPanel.Margin = new System.Windows.Forms.Padding(0);
            this.timeStepsFlowPanel.Name = "timeStepsFlowPanel";
            this.timeStepsFlowPanel.Size = new System.Drawing.Size(200, 181);
            this.timeStepsFlowPanel.TabIndex = 0;
            this.timeStepsFlowPanel.SizeChanged += new System.EventHandler(this.repairAllMargins);
            // 
            // seqNameBox
            // 
            this.seqNameBox.Location = new System.Drawing.Point(2, 373);
            this.seqNameBox.Name = "seqNameBox";
            this.seqNameBox.Size = new System.Drawing.Size(112, 20);
            this.seqNameBox.TabIndex = 2;
            this.seqNameBox.TextChanged += new System.EventHandler(this.seqNameBox_TextChanged);
            // 
            // seqDescBox
            // 
            this.seqDescBox.Location = new System.Drawing.Point(3, 416);
            this.seqDescBox.Multiline = true;
            this.seqDescBox.Name = "seqDescBox";
            this.seqDescBox.Size = new System.Drawing.Size(112, 191);
            this.seqDescBox.TabIndex = 3;
            this.seqDescBox.TextChanged += new System.EventHandler(this.seqDescBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 357);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Sequence Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 400);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Sequence Description:";
            // 
            // createMode
            // 
            this.createMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.createMode.Location = new System.Drawing.Point(81, 90);
            this.createMode.Name = "createMode";
            this.createMode.Size = new System.Drawing.Size(20, 20);
            this.createMode.TabIndex = 18;
            this.createMode.Text = "+";
            this.toolTip1.SetToolTip(this.createMode, "Create new Mode.");
            this.createMode.UseVisualStyleBackColor = true;
            this.createMode.Click += new System.EventHandler(this.createMode_Click);
            // 
            // destroyMode
            // 
            this.destroyMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.destroyMode.Location = new System.Drawing.Point(106, 90);
            this.destroyMode.Name = "destroyMode";
            this.destroyMode.Size = new System.Drawing.Size(20, 20);
            this.destroyMode.TabIndex = 19;
            this.destroyMode.Text = "-";
            this.toolTip1.SetToolTip(this.destroyMode, "Delete currently selected Mode.");
            this.destroyMode.UseVisualStyleBackColor = true;
            this.destroyMode.Click += new System.EventHandler(this.destroyMode_Click);
            // 
            // storeMode
            // 
            this.storeMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.storeMode.Location = new System.Drawing.Point(80, 114);
            this.storeMode.Name = "storeMode";
            this.storeMode.Size = new System.Drawing.Size(47, 20);
            this.storeMode.TabIndex = 21;
            this.storeMode.Text = "Store";
            this.toolTip1.SetToolTip(this.storeMode, "Store current Mode.");
            this.storeMode.UseVisualStyleBackColor = true;
            this.storeMode.Click += new System.EventHandler(this.storeMode_Click);
            // 
            // digitalOverridesCountLabel
            // 
            this.digitalOverridesCountLabel.AutoSize = true;
            this.digitalOverridesCountLabel.Location = new System.Drawing.Point(3, 36);
            this.digitalOverridesCountLabel.Name = "digitalOverridesCountLabel";
            this.digitalOverridesCountLabel.Size = new System.Drawing.Size(35, 13);
            this.digitalOverridesCountLabel.TabIndex = 15;
            this.digitalOverridesCountLabel.Text = "label3";
            // 
            // analogOverridesCountLabel
            // 
            this.analogOverridesCountLabel.AutoSize = true;
            this.analogOverridesCountLabel.Location = new System.Drawing.Point(3, 13);
            this.analogOverridesCountLabel.Name = "analogOverridesCountLabel";
            this.analogOverridesCountLabel.Size = new System.Drawing.Size(35, 13);
            this.analogOverridesCountLabel.TabIndex = 16;
            this.analogOverridesCountLabel.Text = "label4";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.runControl1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.seqNameBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.seqDescBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(119, 594);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(125, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(675, 600);
            this.panel2.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(675, 600);
            this.tableLayoutPanel2.TabIndex = 17;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(675, 240);
            this.panel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.timeStepsPanel, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(675, 240);
            this.tableLayoutPanel4.TabIndex = 17;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.storeMode);
            this.panel4.Controls.Add(this.modeTextBox);
            this.panel4.Controls.Add(this.destroyMode);
            this.panel4.Controls.Add(this.createMode);
            this.panel4.Controls.Add(this.modeBox);
            this.panel4.Controls.Add(this.digitalOverridesCountLabel);
            this.panel4.Controls.Add(this.analogPreviewAutoUpdate);
            this.panel4.Controls.Add(this.analogOverridesCountLabel);
            this.panel4.Controls.Add(this.analogPreviewUpdate);
            this.panel4.Controls.Add(this.checkBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(130, 240);
            this.panel4.TabIndex = 3;
            // 
            // modeTextBox
            // 
            this.modeTextBox.Location = new System.Drawing.Point(5, 114);
            this.modeTextBox.Name = "modeTextBox";
            this.modeTextBox.Size = new System.Drawing.Size(73, 20);
            this.modeTextBox.TabIndex = 20;
            // 
            // modeBox
            // 
            this.modeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeBox.FormattingEnabled = true;
            this.modeBox.Location = new System.Drawing.Point(5, 90);
            this.modeBox.Name = "modeBox";
            this.modeBox.Size = new System.Drawing.Size(73, 21);
            this.modeBox.TabIndex = 17;
            this.modeBox.SelectedIndexChanged += new System.EventHandler(this.modeBox_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 243);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel5);
            this.splitContainer1.Size = new System.Drawing.Size(669, 354);
            this.splitContainer1.SplitterDistance = 147;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.analogChannelLabelsPanel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.analogPreviewPane1, 1, 0);
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
            this.tableLayoutPanel5.Controls.Add(this.digitalChannelLabelsPanel1, 0, 0);
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
            this.digitalGridPanel.Controls.Add(this.digitalGrid1);
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
            // beginHintLabel
            // 
            this.beginHintLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.beginHintLabel.Location = new System.Drawing.Point(0, 0);
            this.beginHintLabel.Name = "beginHintLabel";
            this.beginHintLabel.Size = new System.Drawing.Size(188, 175);
            this.beginHintLabel.TabIndex = 0;
            this.beginHintLabel.Text = "To begin, create a new Timestep by right clicking here.";
            this.beginHintLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // runControl1
            // 
            this.runControl1.IsRunNoSaveEnabled = true;
            this.runControl1.Location = new System.Drawing.Point(2, 3);
            this.runControl1.Name = "runControl1";
            this.runControl1.Size = new System.Drawing.Size(119, 351);
            this.runControl1.TabIndex = 1;
            // 
            // analogChannelLabelsPanel1
            // 
            this.analogChannelLabelsPanel1.AutoScroll = true;
            this.analogChannelLabelsPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.analogChannelLabelsPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analogChannelLabelsPanel1.Location = new System.Drawing.Point(0, 0);
            this.analogChannelLabelsPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.analogChannelLabelsPanel1.Name = "analogChannelLabelsPanel1";
            this.analogChannelLabelsPanel1.Size = new System.Drawing.Size(130, 147);
            this.analogChannelLabelsPanel1.TabIndex = 6;
            this.analogChannelLabelsPanel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.analogChannelLabelsPanel1_Scroll);
            this.analogChannelLabelsPanel1.Enter += new System.EventHandler(this.analogChannelLabelsPanel1_Enter);
            // 
            // analogPreviewPane1
            // 
            this.analogPreviewPane1.AutoScroll = true;
            this.analogPreviewPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analogPreviewPane1.Location = new System.Drawing.Point(130, 0);
            this.analogPreviewPane1.Margin = new System.Windows.Forms.Padding(0);
            this.analogPreviewPane1.Name = "analogPreviewPane1";
            this.analogPreviewPane1.Size = new System.Drawing.Size(539, 147);
            this.analogPreviewPane1.TabIndex = 6;
            this.analogPreviewPane1.TabStop = false;
            this.analogPreviewPane1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.analogPreviewPane1_Scroll);
            this.analogPreviewPane1.Click += new System.EventHandler(this.analogPreviewPane1_Click);
            // 
            // digitalChannelLabelsPanel1
            // 
            this.digitalChannelLabelsPanel1.AutoScroll = true;
            this.digitalChannelLabelsPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.digitalChannelLabelsPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.digitalChannelLabelsPanel1.Location = new System.Drawing.Point(0, 0);
            this.digitalChannelLabelsPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.digitalChannelLabelsPanel1.Name = "digitalChannelLabelsPanel1";
            this.digitalChannelLabelsPanel1.Size = new System.Drawing.Size(130, 203);
            this.digitalChannelLabelsPanel1.TabIndex = 7;
            this.digitalChannelLabelsPanel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.digitalChannelLabelsPanel1_Scroll);
            this.digitalChannelLabelsPanel1.Enter += new System.EventHandler(this.digitalChannelLabelsPanel1_Enter);
            // 
            // digitalGrid1
            // 
            this.digitalGrid1.AutoScroll = true;
            this.digitalGrid1.ContainerSize = new System.Drawing.Size(0, 0);
            this.digitalGrid1.Location = new System.Drawing.Point(0, 0);
            this.digitalGrid1.Margin = new System.Windows.Forms.Padding(0);
            this.digitalGrid1.Name = "digitalGrid1";
            this.digitalGrid1.RowHeight = 0;
            this.digitalGrid1.Size = new System.Drawing.Size(539, 203);
            this.digitalGrid1.TabIndex = 3;
            this.digitalGrid1.TabStop = false;
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
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.digitalGridPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public RunControl runControl1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addNewTimestepToolStripMenuItem;
        private System.Windows.Forms.CheckBox analogPreviewAutoUpdate;
        private System.Windows.Forms.Button analogPreviewUpdate;
        private System.Windows.Forms.TextBox seqNameBox;
        private System.Windows.Forms.TextBox seqDescBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Panel timeStepsPanel;
        public DigitalGrid digitalGrid1;
        public AnalogPreviewPane analogPreviewPane1;
        public DigitalChannelLabelsPanel digitalChannelLabelsPanel1;
        public AnalogChannelLabelsPanel analogChannelLabelsPanel1;
        private System.Windows.Forms.Label digitalOverridesCountLabel;
        private System.Windows.Forms.Label analogOverridesCountLabel;
        public System.Windows.Forms.FlowLayoutPanel timeStepsFlowPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel digitalGridPanel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button createMode;
        private System.Windows.Forms.Button destroyMode;
        private System.Windows.Forms.Button storeMode;
        private System.Windows.Forms.TextBox modeTextBox;
        public System.Windows.Forms.ComboBox modeBox;
        private System.Windows.Forms.Label beginHintLabel;
    }
}
