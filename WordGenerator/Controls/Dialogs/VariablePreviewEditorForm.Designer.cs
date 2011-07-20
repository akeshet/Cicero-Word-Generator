namespace WordGenerator.Controls
{
    partial class VariablePreviewEditorForm
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
            this.variablePreviewEditorPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.variablePreviewEditor1 = new WordGenerator.Controls.VariablePreviewEditor();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.variablePreviewEditorPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // variablePreviewEditorPanel
            // 
            this.variablePreviewEditorPanel.AutoSize = true;
            this.variablePreviewEditorPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.variablePreviewEditorPanel.Controls.Add(this.variablePreviewEditor1);
            this.variablePreviewEditorPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.variablePreviewEditorPanel.Location = new System.Drawing.Point(0, 0);
            this.variablePreviewEditorPanel.Name = "variablePreviewEditorPanel";
            this.variablePreviewEditorPanel.Size = new System.Drawing.Size(401, 33);
            this.variablePreviewEditorPanel.TabIndex = 0;
            // 
            // variablePreviewEditor1
            // 
            this.variablePreviewEditor1.Location = new System.Drawing.Point(3, 3);
            this.variablePreviewEditor1.Name = "variablePreviewEditor1";
            this.variablePreviewEditor1.Size = new System.Drawing.Size(395, 27);
            this.variablePreviewEditor1.TabIndex = 0;
            this.variablePreviewEditor1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Variable Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Current Value";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(248, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Next Value";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(355, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Click to change";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.variablePreviewEditorPanel);
            this.panel1.Location = new System.Drawing.Point(15, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(432, 421);
            this.panel1.TabIndex = 5;
            // 
            // VariablePreviewEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 492);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(470, 520);
            this.MinimumSize = new System.Drawing.Size(470, 520);
            this.Name = "VariablePreviewEditorForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Variable Previews";
            this.variablePreviewEditorPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel variablePreviewEditorPanel;
        private VariablePreviewEditor variablePreviewEditor1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
    }
}