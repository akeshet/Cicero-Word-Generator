namespace WordGenerator.Controls
{
    partial class StringParameterStringEditor
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
            this.prefixTextbox = new System.Windows.Forms.TextBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insBef = new System.Windows.Forms.ToolStripMenuItem();
            this.insAft = new System.Windows.Forms.ToolStripMenuItem();
            this.del = new System.Windows.Forms.ToolStripMenuItem();
            this.postfixTextbox = new System.Windows.Forms.TextBox();
            this.horizontalParameterEditor1 = new WordGenerator.Controls.HorizontalParameterEditor();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // prefixTextbox
            // 
            this.prefixTextbox.ContextMenuStrip = this.contextMenu;
            this.prefixTextbox.Location = new System.Drawing.Point(0, 0);
            this.prefixTextbox.Name = "prefixTextbox";
            this.prefixTextbox.Size = new System.Drawing.Size(54, 20);
            this.prefixTextbox.TabIndex = 0;
            this.prefixTextbox.TextChanged += new System.EventHandler(this.prefixTextbox_TextChanged);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insBef,
            this.insAft,
            this.del});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(150, 70);
            // 
            // insBef
            // 
            this.insBef.Name = "insBef";
            this.insBef.Size = new System.Drawing.Size(149, 22);
            this.insBef.Text = "Insert Before";
            // 
            // insAft
            // 
            this.insAft.Name = "insAft";
            this.insAft.Size = new System.Drawing.Size(149, 22);
            this.insAft.Text = "Insert After";
            // 
            // del
            // 
            this.del.Name = "del";
            this.del.Size = new System.Drawing.Size(149, 22);
            this.del.Text = "Delete";
            // 
            // postfixTextbox
            // 
            this.postfixTextbox.ContextMenuStrip = this.contextMenu;
            this.postfixTextbox.Location = new System.Drawing.Point(136, 0);
            this.postfixTextbox.Name = "postfixTextbox";
            this.postfixTextbox.Size = new System.Drawing.Size(59, 20);
            this.postfixTextbox.TabIndex = 2;
            this.postfixTextbox.TextChanged += new System.EventHandler(this.postfixTextbox_TextChanged);
            // 
            // horizontalParameterEditor1
            // 
            this.horizontalParameterEditor1.Location = new System.Drawing.Point(55, -1);
            this.horizontalParameterEditor1.Name = "horizontalParameterEditor1";
            this.horizontalParameterEditor1.Size = new System.Drawing.Size(83, 22);
            this.horizontalParameterEditor1.TabIndex = 1;
            this.horizontalParameterEditor1.UnitSelectorVisibility = false;
            // 
            // StringParameterStringEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenu;
            this.Controls.Add(this.postfixTextbox);
            this.Controls.Add(this.horizontalParameterEditor1);
            this.Controls.Add(this.prefixTextbox);
            this.Name = "StringParameterStringEditor";
            this.Size = new System.Drawing.Size(198, 20);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox prefixTextbox;
        private HorizontalParameterEditor horizontalParameterEditor1;
        private System.Windows.Forms.TextBox postfixTextbox;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem insBef;
        private System.Windows.Forms.ToolStripMenuItem insAft;
        private System.Windows.Forms.ToolStripMenuItem del;
    }
}
