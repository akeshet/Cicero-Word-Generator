namespace WordGenerator
{
    partial class OverridePage
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
            this.digitalOverridePanel = new System.Windows.Forms.Panel();
            this.digitalOverridePlaceholder = new WordGenerator.DigitalOverride();
            this.analogOverridePanel = new System.Windows.Forms.Panel();
            this.analogOverridePlaceholder = new WordGenerator.AnalogOverride();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.digitalOverridePanel.SuspendLayout();
            this.analogOverridePanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // digitalOverridePanel
            // 
            this.digitalOverridePanel.AutoScroll = true;
            this.digitalOverridePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.digitalOverridePanel.Controls.Add(this.digitalOverridePlaceholder);
            this.digitalOverridePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.digitalOverridePanel.Location = new System.Drawing.Point(3, 63);
            this.digitalOverridePanel.Name = "digitalOverridePanel";
            this.digitalOverridePanel.Size = new System.Drawing.Size(626, 849);
            this.digitalOverridePanel.TabIndex = 0;
            // 
            // digitalOverridePlaceholder
            // 
            this.digitalOverridePlaceholder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.digitalOverridePlaceholder.Location = new System.Drawing.Point(3, 3);
            this.digitalOverridePlaceholder.Name = "digitalOverridePlaceholder";
            this.digitalOverridePlaceholder.Size = new System.Drawing.Size(299, 30);
            this.digitalOverridePlaceholder.TabIndex = 0;
            this.digitalOverridePlaceholder.Visible = false;
            // 
            // analogOverridePanel
            // 
            this.analogOverridePanel.AutoScroll = true;
            this.analogOverridePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.analogOverridePanel.Controls.Add(this.analogOverridePlaceholder);
            this.analogOverridePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analogOverridePanel.Location = new System.Drawing.Point(635, 63);
            this.analogOverridePanel.Name = "analogOverridePanel";
            this.analogOverridePanel.Size = new System.Drawing.Size(626, 849);
            this.analogOverridePanel.TabIndex = 1;
            // 
            // analogOverridePlaceholder
            // 
            this.analogOverridePlaceholder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.analogOverridePlaceholder.Location = new System.Drawing.Point(4, 4);
            this.analogOverridePlaceholder.Name = "analogOverridePlaceholder";
            this.analogOverridePlaceholder.Size = new System.Drawing.Size(354, 30);
            this.analogOverridePlaceholder.TabIndex = 0;
            this.analogOverridePlaceholder.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Digital Overrides:                (re-outputs automatically)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Analog Overrides:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(100, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 41);
            this.button1.TabIndex = 4;
            this.button1.Text = "Re-&Output (Alt-O)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(241, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Right-click to set an override value toggle hotkey.";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.analogOverridePanel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.digitalOverridePanel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1264, 915);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(626, 54);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(635, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(626, 54);
            this.panel2.TabIndex = 1;
            // 
            // OverridePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "OverridePage";
            this.Size = new System.Drawing.Size(1264, 915);
            this.digitalOverridePanel.ResumeLayout(false);
            this.analogOverridePanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel digitalOverridePanel;
        private DigitalOverride digitalOverridePlaceholder;
        private System.Windows.Forms.Panel analogOverridePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private AnalogOverride analogOverridePlaceholder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
