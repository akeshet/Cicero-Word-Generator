namespace WordGenerator.Controls
{
    partial class VariableEditor
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.valueSelector = new System.Windows.Forms.NumericUpDown();
            this.listSelector = new System.Windows.Forms.ComboBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.derivedCheckBox = new System.Windows.Forms.CheckBox();
            this.derivedValueLabel = new System.Windows.Forms.Label();
            this.formulaTextBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.helpOnSupportedOperationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.permanentValueLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.valueSelector)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(25, 1);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // valueSelector
            // 
            this.valueSelector.DecimalPlaces = 3;
            this.valueSelector.Location = new System.Drawing.Point(107, 0);
            this.valueSelector.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.valueSelector.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.valueSelector.Name = "valueSelector";
            this.valueSelector.Size = new System.Drawing.Size(81, 20);
            this.valueSelector.TabIndex = 1;
            this.valueSelector.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            this.valueSelector.MouseClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown1_MouseClick);
            this.valueSelector.MouseDown += new System.Windows.Forms.MouseEventHandler(this.numericUpDown1_MouseClick);
            // 
            // listSelector
            // 
            this.listSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listSelector.FormattingEnabled = true;
            this.listSelector.Location = new System.Drawing.Point(107, 0);
            this.listSelector.Name = "listSelector";
            this.listSelector.Size = new System.Drawing.Size(81, 21);
            this.listSelector.TabIndex = 2;
            this.listSelector.Visible = false;
            this.listSelector.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(1, 1);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(19, 20);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.TabStop = false;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteVariableButtonClick);
            // 
            // derivedCheckBox
            // 
            this.derivedCheckBox.AutoSize = true;
            this.derivedCheckBox.Location = new System.Drawing.Point(194, 4);
            this.derivedCheckBox.Name = "derivedCheckBox";
            this.derivedCheckBox.Size = new System.Drawing.Size(15, 14);
            this.derivedCheckBox.TabIndex = 4;
            this.derivedCheckBox.UseVisualStyleBackColor = true;
            this.derivedCheckBox.CheckedChanged += new System.EventHandler(this.derivedCheckBox_CheckedChanged);
            // 
            // derivedValueLabel
            // 
            this.derivedValueLabel.AutoEllipsis = true;
            this.derivedValueLabel.Location = new System.Drawing.Point(3, 51);
            this.derivedValueLabel.Name = "derivedValueLabel";
            this.derivedValueLabel.Size = new System.Drawing.Size(214, 46);
            this.derivedValueLabel.TabIndex = 7;
            this.derivedValueLabel.Text = "label1";
            this.derivedValueLabel.Visible = false;
            // 
            // formulaTextBox
            // 
            this.formulaTextBox.ContextMenuStrip = this.contextMenuStrip1;
            this.formulaTextBox.Location = new System.Drawing.Point(3, 28);
            this.formulaTextBox.Name = "formulaTextBox";
            this.formulaTextBox.Size = new System.Drawing.Size(206, 20);
            this.formulaTextBox.TabIndex = 8;
            this.formulaTextBox.Visible = false;
            this.formulaTextBox.TextChanged += new System.EventHandler(this.formulaTextBox_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpOnSupportedOperationsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(228, 26);
            // 
            // helpOnSupportedOperationsToolStripMenuItem
            // 
            this.helpOnSupportedOperationsToolStripMenuItem.Name = "helpOnSupportedOperationsToolStripMenuItem";
            this.helpOnSupportedOperationsToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.helpOnSupportedOperationsToolStripMenuItem.Text = "Help on supported operations";
            this.helpOnSupportedOperationsToolStripMenuItem.Click += new System.EventHandler(this.helpOnSupportedOperationsToolStripMenuItem_Click);
            // 
            // permanentValueLabel
            // 
            this.permanentValueLabel.AutoSize = true;
            this.permanentValueLabel.ForeColor = System.Drawing.Color.White;
            this.permanentValueLabel.Location = new System.Drawing.Point(109, 4);
            this.permanentValueLabel.Name = "permanentValueLabel";
            this.permanentValueLabel.Size = new System.Drawing.Size(110, 13);
            this.permanentValueLabel.TabIndex = 9;
            this.permanentValueLabel.Text = "permanentValueLabel";
            this.permanentValueLabel.Visible = false;
            // 
            // VariableEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.permanentValueLabel);
            this.Controls.Add(this.formulaTextBox);
            this.Controls.Add(this.derivedValueLabel);
            this.Controls.Add(this.derivedCheckBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.listSelector);
            this.Controls.Add(this.valueSelector);
            this.Controls.Add(this.textBox1);
            this.Name = "VariableEditor";
            this.Size = new System.Drawing.Size(220, 104);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.VariableEditor_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.valueSelector)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown valueSelector;
        private System.Windows.Forms.ComboBox listSelector;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox derivedCheckBox;
        private System.Windows.Forms.Label derivedValueLabel;
        private System.Windows.Forms.TextBox formulaTextBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpOnSupportedOperationsToolStripMenuItem;
        private System.Windows.Forms.Label permanentValueLabel;
    }
}
