namespace WordGenerator.Controls
{
    partial class PulsesPage
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
            this.pulseEditorPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.pulseEditorPlaceholder = new WordGenerator.Controls.PulseEditor();
            this.createPulse = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pulseEditorPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pulseEditorPanel
            // 
            this.pulseEditorPanel.AutoScroll = true;
            this.pulseEditorPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pulseEditorPanel.Controls.Add(this.pulseEditorPlaceholder);
            this.pulseEditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pulseEditorPanel.Location = new System.Drawing.Point(183, 3);
            this.pulseEditorPanel.Name = "pulseEditorPanel";
            this.pulseEditorPanel.Size = new System.Drawing.Size(898, 854);
            this.pulseEditorPanel.TabIndex = 0;
            // 
            // pulseEditorPlaceholder
            // 
            this.pulseEditorPlaceholder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pulseEditorPlaceholder.Location = new System.Drawing.Point(3, 3);
            this.pulseEditorPlaceholder.Name = "pulseEditorPlaceholder";
            this.pulseEditorPlaceholder.Size = new System.Drawing.Size(597, 267);
            this.pulseEditorPlaceholder.TabIndex = 0;
            this.pulseEditorPlaceholder.Visible = false;
            // 
            // createPulse
            // 
            this.createPulse.Location = new System.Drawing.Point(3, 3);
            this.createPulse.Name = "createPulse";
            this.createPulse.Size = new System.Drawing.Size(124, 42);
            this.createPulse.TabIndex = 1;
            this.createPulse.Text = "Create Pulse";
            this.createPulse.UseVisualStyleBackColor = true;
            this.createPulse.Click += new System.EventHandler(this.createPulse_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.createPulse, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pulseEditorPanel, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1084, 860);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // PulsesPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PulsesPage";
            this.Size = new System.Drawing.Size(1084, 860);
            this.pulseEditorPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pulseEditorPanel;
        private PulseEditor pulseEditorPlaceholder;
        private System.Windows.Forms.Button createPulse;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
