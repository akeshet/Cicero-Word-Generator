namespace WordGenerator
{
    partial class DigitalOverride
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
            this.label1 = new System.Windows.Forms.Label();
            this.overrideCheck = new System.Windows.Forms.CheckBox();
            this.valueBox = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.hotkeyLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.hotkeyTextbox = new System.Windows.Forms.ToolStripTextBox();
            this.clearHotkeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlAltToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overrideHotkeyTextbox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.overrideHotkeyLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // overrideCheck
            // 
            this.overrideCheck.AutoSize = true;
            this.overrideCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.overrideCheck.ForeColor = System.Drawing.Color.White;
            this.overrideCheck.Location = new System.Drawing.Point(107, 4);
            this.overrideCheck.Name = "overrideCheck";
            this.overrideCheck.Size = new System.Drawing.Size(72, 17);
            this.overrideCheck.TabIndex = 1;
            this.overrideCheck.Text = "Override?";
            this.overrideCheck.UseVisualStyleBackColor = true;
            this.overrideCheck.CheckedChanged += new System.EventHandler(this.overrideCheck_CheckedChanged);
            // 
            // valueBox
            // 
            this.valueBox.AutoSize = true;
            this.valueBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.valueBox.ForeColor = System.Drawing.Color.White;
            this.valueBox.Location = new System.Drawing.Point(273, 4);
            this.valueBox.Name = "valueBox";
            this.valueBox.Size = new System.Drawing.Size(56, 17);
            this.valueBox.TabIndex = 2;
            this.valueBox.Text = "Value:";
            this.valueBox.UseVisualStyleBackColor = true;
            this.valueBox.CheckedChanged += new System.EventHandler(this.valueBox_CheckedChanged);
            // 
            // hotkeyLabel
            // 
            this.hotkeyLabel.AutoSize = true;
            this.hotkeyLabel.ForeColor = System.Drawing.Color.White;
            this.hotkeyLabel.Location = new System.Drawing.Point(335, 6);
            this.hotkeyLabel.Name = "hotkeyLabel";
            this.hotkeyLabel.Size = new System.Drawing.Size(0, 13);
            this.hotkeyLabel.TabIndex = 3;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.clearHotkeyToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(190, 98);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.hotkeyTextbox});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem1.Text = "Set toggle hotkey";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem2.Text = "Ctrl + Alt +";
            // 
            // hotkeyTextbox
            // 
            this.hotkeyTextbox.Name = "hotkeyTextbox";
            this.hotkeyTextbox.Size = new System.Drawing.Size(100, 21);
            this.hotkeyTextbox.TextChanged += new System.EventHandler(this.hotkeyTextbox_TextChanged);
            // 
            // clearHotkeyToolStripMenuItem
            // 
            this.clearHotkeyToolStripMenuItem.Name = "clearHotkeyToolStripMenuItem";
            this.clearHotkeyToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.clearHotkeyToolStripMenuItem.Text = "Clear toggle hotkey";
            this.clearHotkeyToolStripMenuItem.Click += new System.EventHandler(this.clearHotkeyToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrlAltToolStripMenuItem,
            this.overrideHotkeyTextbox});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem3.Text = "Set override hotkey";
            // 
            // ctrlAltToolStripMenuItem
            // 
            this.ctrlAltToolStripMenuItem.Name = "ctrlAltToolStripMenuItem";
            this.ctrlAltToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.ctrlAltToolStripMenuItem.Text = "Ctrl + Alt + ";
            // 
            // overrideHotkeyTextbox
            // 
            this.overrideHotkeyTextbox.Name = "overrideHotkeyTextbox";
            this.overrideHotkeyTextbox.Size = new System.Drawing.Size(100, 21);
            this.overrideHotkeyTextbox.TextChanged += new System.EventHandler(this.overrideHotkeyTextbox_TextChanged);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem4.Text = "Clear override hotkey";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // overrideHotkeyLabel
            // 
            this.overrideHotkeyLabel.AutoSize = true;
            this.overrideHotkeyLabel.ForeColor = System.Drawing.Color.White;
            this.overrideHotkeyLabel.Location = new System.Drawing.Point(186, 4);
            this.overrideHotkeyLabel.Name = "overrideHotkeyLabel";
            this.overrideHotkeyLabel.Size = new System.Drawing.Size(0, 13);
            this.overrideHotkeyLabel.TabIndex = 4;
            // 
            // DigitalOverride
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.overrideHotkeyLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.overrideCheck);
            this.Controls.Add(this.hotkeyLabel);
            this.Controls.Add(this.valueBox);
            this.Name = "DigitalOverride";
            this.Size = new System.Drawing.Size(365, 27);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox overrideCheck;
        private System.Windows.Forms.CheckBox valueBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label hotkeyLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripTextBox hotkeyTextbox;
        private System.Windows.Forms.ToolStripMenuItem clearHotkeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem ctrlAltToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox overrideHotkeyTextbox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.Label overrideHotkeyLabel;
    }
}
