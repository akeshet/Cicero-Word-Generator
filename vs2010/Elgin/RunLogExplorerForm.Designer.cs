namespace Elgin
{
    partial class RunLogExplorerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.seqDesc = new System.Windows.Forms.TextBox();
            this.sequenceGrid = new System.Windows.Forms.PropertyGrid();
            this.settingsGrid = new System.Windows.Forms.PropertyGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.fileNameLabel = new System.Windows.Forms.TextBox();
            this.timeLabel = new System.Windows.Forms.TextBox();
            this.seqNameLabel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Run Time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sequence Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Sequence Description:";
            // 
            // seqDesc
            // 
            this.seqDesc.Location = new System.Drawing.Point(15, 144);
            this.seqDesc.Multiline = true;
            this.seqDesc.Name = "seqDesc";
            this.seqDesc.ReadOnly = true;
            this.seqDesc.Size = new System.Drawing.Size(157, 99);
            this.seqDesc.TabIndex = 5;
            // 
            // sequenceGrid
            // 
            this.sequenceGrid.Location = new System.Drawing.Point(12, 249);
            this.sequenceGrid.Name = "sequenceGrid";
            this.sequenceGrid.Size = new System.Drawing.Size(305, 389);
            this.sequenceGrid.TabIndex = 6;
            // 
            // settingsGrid
            // 
            this.settingsGrid.Location = new System.Drawing.Point(422, 249);
            this.settingsGrid.Name = "settingsGrid";
            this.settingsGrid.Size = new System.Drawing.Size(331, 389);
            this.settingsGrid.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(419, 230);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Settings:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Filename:";
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.Location = new System.Drawing.Point(67, 6);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.ReadOnly = true;
            this.fileNameLabel.Size = new System.Drawing.Size(690, 20);
            this.fileNameLabel.TabIndex = 10;
            // 
            // timeLabel
            // 
            this.timeLabel.Location = new System.Drawing.Point(111, 65);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.ReadOnly = true;
            this.timeLabel.Size = new System.Drawing.Size(172, 20);
            this.timeLabel.TabIndex = 11;
            // 
            // seqNameLabel
            // 
            this.seqNameLabel.Location = new System.Drawing.Point(111, 36);
            this.seqNameLabel.Name = "seqNameLabel";
            this.seqNameLabel.ReadOnly = true;
            this.seqNameLabel.Size = new System.Drawing.Size(172, 20);
            this.seqNameLabel.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "label6";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(289, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 49);
            this.button1.TabIndex = 14;
            this.button1.Text = "Launch in Cicero";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RunLogExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 650);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.seqNameLabel);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.settingsGrid);
            this.Controls.Add(this.sequenceGrid);
            this.Controls.Add(this.seqDesc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "RunLogExplorerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox seqDesc;
        private System.Windows.Forms.PropertyGrid sequenceGrid;
        private System.Windows.Forms.PropertyGrid settingsGrid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox fileNameLabel;
        private System.Windows.Forms.TextBox timeLabel;
        private System.Windows.Forms.TextBox seqNameLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
    }
}