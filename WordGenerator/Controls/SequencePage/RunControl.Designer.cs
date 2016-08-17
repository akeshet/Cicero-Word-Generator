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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bgRunButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.iterationSelector)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // runZeroButton
            // 
            this.runZeroButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.runZeroButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.runZeroButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.runZeroButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runZeroButton.ForeColor = System.Drawing.Color.White;
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
            this.runCurrentButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.runCurrentButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.runCurrentButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.runCurrentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runCurrentButton.ForeColor = System.Drawing.Color.White;
            this.runCurrentButton.Location = new System.Drawing.Point(3, 311);
            this.runCurrentButton.Name = "runCurrentButton";
            this.runCurrentButton.Size = new System.Drawing.Size(110, 39);
            this.runCurrentButton.TabIndex = 4;
            this.runCurrentButton.Text = "Run Current Iteration";
            this.runCurrentButton.UseVisualStyleBackColor = true;
            this.runCurrentButton.Click += new System.EventHandler(this.runCurrentButton_Click);
            this.runCurrentButton.Paint += new System.Windows.Forms.PaintEventHandler(this.runCurrentButton_Paint);
            // 
            // runListButton
            // 
            this.runListButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.runListButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.runListButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.runListButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runListButton.ForeColor = System.Drawing.Color.White;
            this.runListButton.Location = new System.Drawing.Point(3, 117);
            this.runListButton.Name = "runListButton";
            this.runListButton.Size = new System.Drawing.Size(110, 38);
            this.runListButton.TabIndex = 6;
            this.runListButton.Text = "Run List (F12)";
            this.runListButton.UseVisualStyleBackColor = true;
            this.runListButton.Click += new System.EventHandler(this.runListButton_Click);
            // 
            // continueListButton
            // 
            this.continueListButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.continueListButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.continueListButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.continueListButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueListButton.ForeColor = System.Drawing.Color.White;
            this.continueListButton.Location = new System.Drawing.Point(3, 161);
            this.continueListButton.Name = "continueListButton";
            this.continueListButton.Size = new System.Drawing.Size(110, 38);
            this.continueListButton.TabIndex = 7;
            this.continueListButton.Text = "Continue List";
            this.continueListButton.UseVisualStyleBackColor = true;
            this.continueListButton.Click += new System.EventHandler(this.continueListButton_Click);
            // 
            // iterationSelector
            // 
            this.iterationSelector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.iterationSelector.ForeColor = System.Drawing.Color.White;
            this.iterationSelector.Location = new System.Drawing.Point(69, 18);
            this.iterationSelector.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.iterationSelector.Name = "iterationSelector";
            this.iterationSelector.Size = new System.Drawing.Size(39, 20);
            this.iterationSelector.TabIndex = 3;
            this.iterationSelector.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.iterationSelector.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // repeatCheckBox
            // 
            this.repeatCheckBox.AutoSize = true;
            this.repeatCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.repeatCheckBox.ForeColor = System.Drawing.Color.White;
            this.repeatCheckBox.Location = new System.Drawing.Point(3, 94);
            this.repeatCheckBox.Name = "repeatCheckBox";
            this.repeatCheckBox.Size = new System.Drawing.Size(100, 17);
            this.repeatCheckBox.TabIndex = 5;
            this.repeatCheckBox.Text = "Run Repeatedly";
            this.repeatCheckBox.UseVisualStyleBackColor = true;
            // 
            // setIterButt
            // 
            this.setIterButt.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.setIterButt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.setIterButt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.setIterButt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setIterButt.ForeColor = System.Drawing.Color.White;
            this.setIterButt.Location = new System.Drawing.Point(0, 3);
            this.setIterButt.Name = "setIterButt";
            this.setIterButt.Size = new System.Drawing.Size(63, 51);
            this.setIterButt.TabIndex = 2;
            this.setIterButt.Text = "Set Iteration";
            this.setIterButt.UseVisualStyleBackColor = true;
            this.setIterButt.Click += new System.EventHandler(this.setIterButt_Click);
            // 
            // runRandomList
            // 
            this.runRandomList.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.runRandomList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.runRandomList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.runRandomList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runRandomList.ForeColor = System.Drawing.Color.White;
            this.runRandomList.Location = new System.Drawing.Point(3, 205);
            this.runRandomList.Name = "runRandomList";
            this.runRandomList.Size = new System.Drawing.Size(110, 38);
            this.runRandomList.TabIndex = 8;
            this.runRandomList.Text = "Run List in Random Order";
            this.runRandomList.UseVisualStyleBackColor = true;
            this.runRandomList.Click += new System.EventHandler(this.runRandomList_Click);
            // 
            // RunNoSave
            // 
            this.RunNoSave.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.RunNoSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.RunNoSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.RunNoSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RunNoSave.ForeColor = System.Drawing.Color.White;
            this.RunNoSave.Location = new System.Drawing.Point(3, 49);
            this.RunNoSave.Name = "RunNoSave";
            this.RunNoSave.Size = new System.Drawing.Size(110, 39);
            this.RunNoSave.TabIndex = 9;
            this.RunNoSave.Text = "Run Without Saving (F10)";
            this.RunNoSave.UseVisualStyleBackColor = true;
            this.RunNoSave.Click += new System.EventHandler(this.RunNoSave_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.flowLayoutPanel1.Controls.Add(this.runZeroButton);
            this.flowLayoutPanel1.Controls.Add(this.RunNoSave);
            this.flowLayoutPanel1.Controls.Add(this.repeatCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.runListButton);
            this.flowLayoutPanel1.Controls.Add(this.continueListButton);
            this.flowLayoutPanel1.Controls.Add(this.runRandomList);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.runCurrentButton);
            this.flowLayoutPanel1.Controls.Add(this.bgRunButton);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(119, 400);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.setIterButt);
            this.panel1.Controls.Add(this.iterationSelector);
            this.panel1.Location = new System.Drawing.Point(3, 249);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(115, 56);
            this.panel1.TabIndex = 11;
            // 
            // bgRunButton
            // 
            this.bgRunButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bgRunButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.bgRunButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.bgRunButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bgRunButton.ForeColor = System.Drawing.Color.White;
            this.bgRunButton.Location = new System.Drawing.Point(3, 356);
            this.bgRunButton.Name = "bgRunButton";
            this.bgRunButton.Size = new System.Drawing.Size(110, 39);
            this.bgRunButton.TabIndex = 12;
            this.bgRunButton.Text = "Run as Loop in Background (^F9)";
            this.bgRunButton.UseVisualStyleBackColor = true;
            this.bgRunButton.Click += new System.EventHandler(this.bgRunButton_Click);
            // 
            // RunControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "RunControl";
            this.Size = new System.Drawing.Size(119, 402);
            ((System.ComponentModel.ISupportInitialize)(this.iterationSelector)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button bgRunButton;
    }
}
