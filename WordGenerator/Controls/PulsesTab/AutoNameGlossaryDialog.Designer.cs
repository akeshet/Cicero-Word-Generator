namespace WordGenerator.Controls
{
    partial class AutoNameGlossaryDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoNameGlossaryDialog));
            this.closeButton = new System.Windows.Forms.Button();
            this.glossaryText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(141, 312);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeDialog);
            // 
            // glossaryText
            // 
            this.glossaryText.AutoSize = true;
            this.glossaryText.Location = new System.Drawing.Point(57, 97);
            this.glossaryText.Name = "glossaryText";
            this.glossaryText.Size = new System.Drawing.Size(241, 104);
            this.glossaryText.TabIndex = 1;
            this.glossaryText.Text = resources.GetString("glossaryText.Text");
            // 
            // AutoNameGlossaryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 347);
            this.Controls.Add(this.glossaryText);
            this.Controls.Add(this.closeButton);
            this.Name = "AutoNameGlossaryDialog";
            this.Text = "Autoname Glossary";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label glossaryText;
    }
}