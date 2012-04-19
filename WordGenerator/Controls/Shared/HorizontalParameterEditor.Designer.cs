namespace WordGenerator.Controls
{
    partial class HorizontalParameterEditor
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
            this.valueSelector = new System.Windows.Forms.NumericUpDown();
            this.unitSelector = new System.Windows.Forms.ComboBox();
            this.variableComboBox1 = new WordGenerator.Controls.VariableComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.valueSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // valueSelector
            // 
            this.valueSelector.DecimalPlaces = 4;
            this.valueSelector.Location = new System.Drawing.Point(0, 1);
            this.valueSelector.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.valueSelector.Minimum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            -2147483648});
            this.valueSelector.Name = "valueSelector";
            this.valueSelector.Size = new System.Drawing.Size(80, 20);
            this.valueSelector.TabIndex = 1;
            this.valueSelector.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.valueSelector.Paint += new System.Windows.Forms.PaintEventHandler(this.valueSelector_Paint);
            this.valueSelector.ValueChanged += new System.EventHandler(this.valueSelector_ValueChanged);
            this.valueSelector.MouseClick += new System.Windows.Forms.MouseEventHandler(this.valueSelector_MouseClick);
            this.valueSelector.MouseDown += new System.Windows.Forms.MouseEventHandler(this.valueSelector_MouseDown);
            this.valueSelector.Enter += new System.EventHandler(this.valueSelector_Enter);
            // 
            // unitSelector
            // 
            this.unitSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitSelector.FormattingEnabled = true;
            this.unitSelector.Location = new System.Drawing.Point(83, 0);
            this.unitSelector.Name = "unitSelector";
            this.unitSelector.Size = new System.Drawing.Size(47, 21);
            this.unitSelector.TabIndex = 3;
            this.unitSelector.TabStop = false;
            this.unitSelector.SelectedIndexChanged += new System.EventHandler(this.unitSelector_SelectedIndexChanged);
            // 
            // variableComboBox1
            // 
            this.variableComboBox1.BackColor = System.Drawing.SystemColors.Window;
            this.variableComboBox1.Enabled = false;
            this.variableComboBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.variableComboBox1.Location = new System.Drawing.Point(0, 0);
            this.variableComboBox1.Margin = new System.Windows.Forms.Padding(0);
            this.variableComboBox1.Name = "variableComboBox1";
            this.variableComboBox1.Size = new System.Drawing.Size(80, 21);
            this.variableComboBox1.TabIndex = 2;
            this.variableComboBox1.Visible = false;
            this.variableComboBox1.RightClick += new System.EventHandler(this.variableComboBox1_RightClick);
            this.variableComboBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.variableComboBox1_Paint);
            this.variableComboBox1.SelectedIndexChanged += new System.EventHandler(this.variableComboBox1_SelectedIndexChanged);
            // 
            // HorizontalParameterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.variableComboBox1);
            this.Controls.Add(this.unitSelector);
            this.Controls.Add(this.valueSelector);
            this.Name = "HorizontalParameterEditor";
            this.Size = new System.Drawing.Size(133, 22);
            this.updateGUI += new System.EventHandler(this.HorizontalParameterEditor_updateGUI);
            this.ParameterChanged += new System.EventHandler(this.HorizontalParameterEditor_ParameterChanged);
            ((System.ComponentModel.ISupportInitialize)(this.valueSelector)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown valueSelector;
        private System.Windows.Forms.ComboBox unitSelector;
        private VariableComboBox variableComboBox1;
    }
}
