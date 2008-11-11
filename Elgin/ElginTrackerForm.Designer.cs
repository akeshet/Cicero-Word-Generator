namespace Elgin
{
    partial class ElginTrackerForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.variableColumnSettingsStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.varNameSelector = new System.Windows.Forms.ToolStripComboBox();
            this.assignVariableByVariableIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.varNumSelector = new System.Windows.Forms.ToolStripComboBox();
            this.unassignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.columnVisibility = new System.Windows.Forms.CheckedListBox();
            this.CloseButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.SeqName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IterNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RunTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ListStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SequenceDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalibrationShot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.variableColumnSettingsStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CloseButton,
            this.SeqName,
            this.IterNum,
            this.RunTime,
            this.ListStartTime,
            this.SequenceDuration,
            this.CalibrationShot,
            this.VarA,
            this.VarB,
            this.VarC,
            this.VarD,
            this.VarE,
            this.VarF,
            this.VarG});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(134, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(575, 618);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseEnter);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // variableColumnSettingsStrip
            // 
            this.variableColumnSettingsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideToolStripMenuItem,
            this.assignVariableByVariableIDToolStripMenuItem,
            this.unassignToolStripMenuItem});
            this.variableColumnSettingsStrip.Name = "variableColumnSettingsStrip";
            this.variableColumnSettingsStrip.Size = new System.Drawing.Size(236, 70);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.varNameSelector});
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.hideToolStripMenuItem.Text = "Assign Variable by Name";
            // 
            // varNameSelector
            // 
            this.varNameSelector.Name = "varNameSelector";
            this.varNameSelector.Size = new System.Drawing.Size(121, 21);
            this.varNameSelector.DropDown += new System.EventHandler(this.varNameSelector_DropDown);
            this.varNameSelector.DropDownClosed += new System.EventHandler(this.varNameSelector_DropDownClosed);
            // 
            // assignVariableByVariableIDToolStripMenuItem
            // 
            this.assignVariableByVariableIDToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.varNumSelector});
            this.assignVariableByVariableIDToolStripMenuItem.Name = "assignVariableByVariableIDToolStripMenuItem";
            this.assignVariableByVariableIDToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.assignVariableByVariableIDToolStripMenuItem.Text = "Assign Variable by Variable ID#";
            // 
            // varNumSelector
            // 
            this.varNumSelector.Name = "varNumSelector";
            this.varNumSelector.Size = new System.Drawing.Size(121, 21);
            this.varNumSelector.DropDown += new System.EventHandler(this.varNumSelector_DropDown);
            this.varNumSelector.DropDownClosed += new System.EventHandler(this.varNumSelector_DropDownClosed);
            // 
            // unassignToolStripMenuItem
            // 
            this.unassignToolStripMenuItem.Name = "unassignToolStripMenuItem";
            this.unassignToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.unassignToolStripMenuItem.Text = "Unassign";
            this.unassignToolStripMenuItem.Click += new System.EventHandler(this.unassignToolStripMenuItem_Click);
            // 
            // columnVisibility
            // 
            this.columnVisibility.CheckOnClick = true;
            this.columnVisibility.Dock = System.Windows.Forms.DockStyle.Left;
            this.columnVisibility.FormattingEnabled = true;
            this.columnVisibility.Location = new System.Drawing.Point(0, 0);
            this.columnVisibility.Name = "columnVisibility";
            this.columnVisibility.Size = new System.Drawing.Size(134, 604);
            this.columnVisibility.TabIndex = 4;
            this.columnVisibility.SelectedIndexChanged += new System.EventHandler(this.columnVisibility_SelectedIndexChanged);
            this.columnVisibility.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.columnVisibility_ItemCheck);
            // 
            // CloseButton
            // 
            this.CloseButton.HeaderText = "Close";
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.ReadOnly = true;
            this.CloseButton.Width = 40;
            // 
            // SeqName
            // 
            this.SeqName.HeaderText = "Sequence Name";
            this.SeqName.Name = "SeqName";
            this.SeqName.ReadOnly = true;
            this.SeqName.Width = 80;
            // 
            // IterNum
            // 
            this.IterNum.HeaderText = "Iteration Number";
            this.IterNum.Name = "IterNum";
            this.IterNum.ReadOnly = true;
            this.IterNum.Width = 80;
            // 
            // RunTime
            // 
            this.RunTime.HeaderText = "Run Time";
            this.RunTime.Name = "RunTime";
            this.RunTime.ReadOnly = true;
            this.RunTime.Width = 80;
            // 
            // ListStartTime
            // 
            this.ListStartTime.HeaderText = "List Start Time";
            this.ListStartTime.Name = "ListStartTime";
            this.ListStartTime.ReadOnly = true;
            this.ListStartTime.Width = 80;
            // 
            // SequenceDuration
            // 
            this.SequenceDuration.HeaderText = "Sequence Duration";
            this.SequenceDuration.Name = "SequenceDuration";
            this.SequenceDuration.ReadOnly = true;
            this.SequenceDuration.Width = 80;
            // 
            // CalibrationShot
            // 
            this.CalibrationShot.HeaderText = "Calib. Shot?";
            this.CalibrationShot.Name = "CalibrationShot";
            this.CalibrationShot.ReadOnly = true;
            // 
            // VarA
            // 
            this.VarA.ContextMenuStrip = this.variableColumnSettingsStrip;
            this.VarA.HeaderText = "Unassigned Var.";
            this.VarA.Name = "VarA";
            this.VarA.ReadOnly = true;
            this.VarA.Visible = false;
            // 
            // VarB
            // 
            this.VarB.ContextMenuStrip = this.variableColumnSettingsStrip;
            this.VarB.HeaderText = "Unassigned Var.";
            this.VarB.Name = "VarB";
            this.VarB.ReadOnly = true;
            this.VarB.Visible = false;
            // 
            // VarC
            // 
            this.VarC.ContextMenuStrip = this.variableColumnSettingsStrip;
            this.VarC.HeaderText = "Unassigned Var.";
            this.VarC.Name = "VarC";
            this.VarC.ReadOnly = true;
            this.VarC.Visible = false;
            // 
            // VarD
            // 
            this.VarD.ContextMenuStrip = this.variableColumnSettingsStrip;
            this.VarD.HeaderText = "Unassigned Var.";
            this.VarD.Name = "VarD";
            this.VarD.ReadOnly = true;
            this.VarD.Visible = false;
            // 
            // VarE
            // 
            this.VarE.ContextMenuStrip = this.variableColumnSettingsStrip;
            this.VarE.HeaderText = "Unassigned Var.";
            this.VarE.Name = "VarE";
            this.VarE.ReadOnly = true;
            this.VarE.Visible = false;
            // 
            // VarF
            // 
            this.VarF.ContextMenuStrip = this.variableColumnSettingsStrip;
            this.VarF.HeaderText = "Unassigned Var.";
            this.VarF.Name = "VarF";
            this.VarF.ReadOnly = true;
            this.VarF.Visible = false;
            // 
            // VarG
            // 
            this.VarG.ContextMenuStrip = this.variableColumnSettingsStrip;
            this.VarG.HeaderText = "Unassigned Var.";
            this.VarG.Name = "VarG";
            this.VarG.ReadOnly = true;
            this.VarG.Visible = false;
            // 
            // ElginTrackerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 618);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.columnVisibility);
            this.Name = "ElginTrackerForm";
            this.ShowIcon = false;
            this.Text = "Open Logs";
            this.Load += new System.EventHandler(this.ElginMainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.variableColumnSettingsStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip variableColumnSettingsStrip;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.CheckedListBox columnVisibility;
        private System.Windows.Forms.ToolStripComboBox varNameSelector;
        private System.Windows.Forms.ToolStripMenuItem assignVariableByVariableIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox varNumSelector;
        private System.Windows.Forms.ToolStripMenuItem unassignToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewButtonColumn CloseButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn SeqName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IterNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn RunTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ListStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn SequenceDuration;
        private System.Windows.Forms.DataGridViewTextBoxColumn CalibrationShot;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarA;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarB;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarC;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarD;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarF;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarG;
    }
}

