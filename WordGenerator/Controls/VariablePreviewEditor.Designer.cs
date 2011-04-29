namespace WordGenerator.Controls
{
    partial class VariablePreviewEditor
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
            this.variableName = new System.Windows.Forms.Label();
            this.currentValue = new System.Windows.Forms.Label();
            this.nextValue = new System.Windows.Forms.NumericUpDown();
            this.clickToChangeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nextValue)).BeginInit();
            this.SuspendLayout();
            // 
            // variableName
            // 
            this.variableName.AutoSize = true;
            this.variableName.Location = new System.Drawing.Point(3, 3);
            this.variableName.Name = "variableName";
            this.variableName.Size = new System.Drawing.Size(72, 13);
            this.variableName.TabIndex = 0;
            this.variableName.Text = "variableName";
            // 
            // currentValue
            // 
            this.currentValue.AutoSize = true;
            this.currentValue.Location = new System.Drawing.Point(122, 3);
            this.currentValue.Name = "currentValue";
            this.currentValue.Size = new System.Drawing.Size(67, 13);
            this.currentValue.TabIndex = 1;
            this.currentValue.Text = "currentValue";
            // 
            // nextValue
            // 
            this.nextValue.DecimalPlaces = 3;
            this.nextValue.Location = new System.Drawing.Point(231, 0);
            this.nextValue.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nextValue.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.nextValue.Name = "nextValue";
            this.nextValue.Size = new System.Drawing.Size(107, 20);
            this.nextValue.TabIndex = 2;
            // 
            // clickToChangeButton
            // 
            this.clickToChangeButton.Location = new System.Drawing.Point(345, -1);
            this.clickToChangeButton.Name = "clickToChangeButton";
            this.clickToChangeButton.Size = new System.Drawing.Size(26, 21);
            this.clickToChangeButton.TabIndex = 3;
            this.clickToChangeButton.UseVisualStyleBackColor = true;
            this.clickToChangeButton.Click += new System.EventHandler(this.clickToChangeButton_Click);
            // 
            // VariablePreviewEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.clickToChangeButton);
            this.Controls.Add(this.nextValue);
            this.Controls.Add(this.currentValue);
            this.Controls.Add(this.variableName);
            this.Name = "VariablePreviewEditor";
            this.Size = new System.Drawing.Size(396, 27);
            ((System.ComponentModel.ISupportInitialize)(this.nextValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label variableName;
        private System.Windows.Forms.Label currentValue;
        private System.Windows.Forms.NumericUpDown nextValue;
        private System.Windows.Forms.Button clickToChangeButton;
    }
}
