namespace WordGenerator.Controls
{
    partial class RunControl
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
            this.runZeroButton = new System.Windows.Forms.Button();
            this.runCurrentButton = new System.Windows.Forms.Button();
            this.runListButton = new System.Windows.Forms.Button();
            this.continueListButton = new System.Windows.Forms.Button();
            this.iterationSelector = new System.Windows.Forms.NumericUpDown();
            this.repeatCheckBox = new System.Windows.Forms.CheckBox();
            this.setIterButt = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.runRandomList = new System.Windows.Forms.Button();
            this.RunNoSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.iterationSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // runZeroButton
            // 
            this.runZeroButton.Location = new System.Drawing.Point(3, 3);
            this.runZeroButton.Name = "runZeroButton";
            this.runZeroButton.Size = new System.Drawing.Size(110, 40);
            this.runZeroButton.TabIndex = 0;
            this.runZeroButton.Text = "Run Iteration 0 (F9)";
            this.runZeroButton.UseVisualStyleBackColor = true;
            this.runZeroButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // runCurrentButton
            // 
            this.runCurrentButton.Location = new System.Drawing.Point(3, 307);
            this.runCurrentButton.Name = "runCurrentButton";
            this.runCurrentButton.Size = new System.Drawing.Size(110, 39);
            this.runCurrentButton.TabIndex = 4;
            this.runCurrentButton.Text = "Run Current Iteration";
            this.runCurrentButton.UseVisualStyleBackColor = true;
            this.runCurrentButton.Paint += new System.Windows.Forms.PaintEventHandler(this.runCurrentButton_Paint);
            this.runCurrentButton.Click += new System.EventHandler(this.runCurrentButton_Click);
            // 
            // runListButton
            // 
            this.runListButton.Location = new System.Drawing.Point(4, 117);
            this.runListButton.Name = "runListButton";
            this.runListButton.Size = new System.Drawing.Size(110, 38);
            this.runListButton.TabIndex = 6;
            this.runListButton.Text = "Run List (F12)";
            this.runListButton.UseVisualStyleBackColor = true;
            this.runListButton.Click += new System.EventHandler(this.runListButton_Click);
            // 
            // continueListButton
            // 
            this.continueListButton.Location = new System.Drawing.Point(4, 161);
            this.continueListButton.Name = "continueListButton";
            this.continueListButton.Size = new System.Drawing.Size(110, 38);
            this.continueListButton.TabIndex = 7;
            this.continueListButton.Text = "Continue List";
            this.continueListButton.UseVisualStyleBackColor = true;
            this.continueListButton.Click += new System.EventHandler(this.continueListButton_Click);
            // 
            // iterationSelector
            // 
            this.iterationSelector.Location = new System.Drawing.Point(63, 266);
            this.iterationSelector.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.iterationSelector.Name = "iterationSelector";
            this.iterationSelector.Size = new System.Drawing.Size(50, 20);
            this.iterationSelector.TabIndex = 3;
            this.iterationSelector.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.iterationSelector.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // repeatCheckBox
            // 
            this.repeatCheckBox.AutoSize = true;
            this.repeatCheckBox.Location = new System.Drawing.Point(9, 92);
            this.repeatCheckBox.Name = "repeatCheckBox";
            this.repeatCheckBox.Size = new System.Drawing.Size(103, 17);
            this.repeatCheckBox.TabIndex = 5;
            this.repeatCheckBox.Text = "Run Repeatedly";
            this.repeatCheckBox.UseVisualStyleBackColor = true;
            // 
            // setIterButt
            // 
            this.setIterButt.Location = new System.Drawing.Point(3, 249);
            this.setIterButt.Name = "setIterButt";
            this.setIterButt.Size = new System.Drawing.Size(54, 51);
            this.setIterButt.TabIndex = 2;
            this.setIterButt.Text = "Set Iteration";
            this.setIterButt.UseVisualStyleBackColor = true;
            this.setIterButt.Click += new System.EventHandler(this.setIterButt_Click);
            // 
            // runRandomList
            // 
            this.runRandomList.Location = new System.Drawing.Point(4, 205);
            this.runRandomList.Name = "runRandomList";
            this.runRandomList.Size = new System.Drawing.Size(110, 38);
            this.runRandomList.TabIndex = 8;
            this.runRandomList.Text = "Run List in Random Order";
            this.runRandomList.UseVisualStyleBackColor = true;
            this.runRandomList.Click += new System.EventHandler(this.runRandomList_Click);
            // 
            // RunNoSave
            // 
            this.RunNoSave.Location = new System.Drawing.Point(3, 45);
            this.RunNoSave.Name = "RunNoSave";
            this.RunNoSave.Size = new System.Drawing.Size(110, 39);
            this.RunNoSave.TabIndex = 9;
            this.RunNoSave.Text = "Run Without Saving (F10)";
            this.RunNoSave.UseVisualStyleBackColor = true;
            this.RunNoSave.Click += new System.EventHandler(this.RunNoSave_Click);
            // 
            // RunControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RunNoSave);
            this.Controls.Add(this.runRandomList);
            this.Controls.Add(this.setIterButt);
            this.Controls.Add(this.repeatCheckBox);
            this.Controls.Add(this.iterationSelector);
            this.Controls.Add(this.continueListButton);
            this.Controls.Add(this.runListButton);
            this.Controls.Add(this.runCurrentButton);
            this.Controls.Add(this.runZeroButton);
            this.Name = "RunControl";
            this.Size = new System.Drawing.Size(119, 351);
            ((System.ComponentModel.ISupportInitialize)(this.iterationSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button runZeroButton;
        private System.Windows.Forms.Button runCurrentButton;
        private System.Windows.Forms.NumericUpDown iterationSelector;
        private System.Windows.Forms.Button setIterButt;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.CheckBox repeatCheckBox;
        public System.Windows.Forms.Button runListButton;
        public System.Windows.Forms.Button continueListButton;
        public System.Windows.Forms.Button runRandomList;
        public System.Windows.Forms.Button RunNoSave;
    }
}
