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
            this.pulseEditorsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.createPulse = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cleanPulsesButton = new System.Windows.Forms.Button();
            this.autoNameGlossaryButton = new System.Windows.Forms.Button();
            this.pulseEditorPlaceholder = new WordGenerator.Controls.PulseEditor();
            this.pulseEditorsFlowPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pulseEditorsFlowPanel
            // 
            this.pulseEditorsFlowPanel.AutoScroll = true;
            this.pulseEditorsFlowPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pulseEditorsFlowPanel.Controls.Add(this.pulseEditorPlaceholder);
            this.pulseEditorsFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pulseEditorsFlowPanel.Location = new System.Drawing.Point(183, 3);
            this.pulseEditorsFlowPanel.Name = "pulseEditorsFlowPanel";
            this.pulseEditorsFlowPanel.Size = new System.Drawing.Size(898, 854);
            this.pulseEditorsFlowPanel.TabIndex = 0;
            // 
            // createPulse
            // 
            this.createPulse.Location = new System.Drawing.Point(3, 5);
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
            this.tableLayoutPanel1.Controls.Add(this.pulseEditorsFlowPanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1084, 860);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cleanPulsesButton);
            this.panel1.Controls.Add(this.autoNameGlossaryButton);
            this.panel1.Controls.Add(this.createPulse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 854);
            this.panel1.TabIndex = 1;
            // 
            // cleanPulsesButton
            // 
            this.cleanPulsesButton.Location = new System.Drawing.Point(3, 66);
            this.cleanPulsesButton.Name = "cleanPulsesButton";
            this.cleanPulsesButton.Size = new System.Drawing.Size(124, 46);
            this.cleanPulsesButton.TabIndex = 2;
            this.cleanPulsesButton.Text = "Cleanup Duplicates";
            this.cleanPulsesButton.UseVisualStyleBackColor = true;
            this.cleanPulsesButton.Click += new System.EventHandler(this.cleanPulsesButton_Click);
            // 
            // autoNameGlossaryButton
            // 
            this.autoNameGlossaryButton.Location = new System.Drawing.Point(3, 205);
            this.autoNameGlossaryButton.Name = "autoNameGlossaryButton";
            this.autoNameGlossaryButton.Size = new System.Drawing.Size(124, 46);
            this.autoNameGlossaryButton.TabIndex = 1;
            this.autoNameGlossaryButton.Text = "Autoname Glossary";
            this.autoNameGlossaryButton.UseVisualStyleBackColor = true;
            this.autoNameGlossaryButton.Click += new System.EventHandler(this.openAutoNameGlossary);
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
            // PulsesPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PulsesPage";
            this.Size = new System.Drawing.Size(1084, 860);
            this.pulseEditorsFlowPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pulseEditorsFlowPanel;
        private PulseEditor pulseEditorPlaceholder;
        private System.Windows.Forms.Button createPulse;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cleanPulsesButton;
        private System.Windows.Forms.Button autoNameGlossaryButton;
    }
}
