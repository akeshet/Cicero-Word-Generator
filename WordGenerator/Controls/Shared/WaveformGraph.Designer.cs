namespace WordGenerator.Controls
{
    partial class WaveformGraph
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
            this.waveformGraph1 = new NationalInstruments.UI.WindowsForms.WaveformGraph();
            this.waveformPlot1 = new NationalInstruments.UI.WaveformPlot();
            this.xAxis1 = new NationalInstruments.UI.XAxis();
            this.yAxis1 = new NationalInstruments.UI.YAxis();
            this.waveFormNameLabel = new System.Windows.Forms.Label();
            this.channelLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.waveformGraph1)).BeginInit();
            this.SuspendLayout();
            // 
            // waveformGraph1
            // 
            this.waveformGraph1.Location = new System.Drawing.Point(0, 47);
            this.waveformGraph1.Name = "waveformGraph1";
            this.waveformGraph1.Plots.AddRange(new NationalInstruments.UI.WaveformPlot[] {
            this.waveformPlot1});
            this.waveformGraph1.Size = new System.Drawing.Size(250, 203);
            this.waveformGraph1.TabIndex = 0;
            this.waveformGraph1.XAxes.AddRange(new NationalInstruments.UI.XAxis[] {
            this.xAxis1});
            this.waveformGraph1.YAxes.AddRange(new NationalInstruments.UI.YAxis[] {
            this.yAxis1});
            this.waveformGraph1.Click += new System.EventHandler(this.WaveformGraph_Click);
            // 
            // waveformPlot1
            // 
            this.waveformPlot1.XAxis = this.xAxis1;
            this.waveformPlot1.YAxis = this.yAxis1;
            // 
            // waveFormNameLabel
            // 
            this.waveFormNameLabel.AutoSize = true;
            this.waveFormNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.waveFormNameLabel.Location = new System.Drawing.Point(3, 20);
            this.waveFormNameLabel.Name = "waveFormNameLabel";
            this.waveFormNameLabel.Size = new System.Drawing.Size(127, 20);
            this.waveFormNameLabel.TabIndex = 1;
            this.waveFormNameLabel.Text = "Waveform Name";
            this.waveFormNameLabel.Click += new System.EventHandler(this.WaveformGraph_Click);
            // 
            // channelLabel
            // 
            this.channelLabel.AutoSize = true;
            this.channelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.channelLabel.Location = new System.Drawing.Point(3, 0);
            this.channelLabel.Name = "channelLabel";
            this.channelLabel.Size = new System.Drawing.Size(104, 20);
            this.channelLabel.TabIndex = 2;
            this.channelLabel.Text = "channelLabel";
            this.channelLabel.Click += new System.EventHandler(this.WaveformGraph_Click);
            // 
            // WaveformGraph
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.channelLabel);
            this.Controls.Add(this.waveFormNameLabel);
            this.Controls.Add(this.waveformGraph1);
            this.Name = "WaveformGraph";
            this.Size = new System.Drawing.Size(248, 248);
            this.Click += new System.EventHandler(this.WaveformGraph_Click);
            ((System.ComponentModel.ISupportInitialize)(this.waveformGraph1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NationalInstruments.UI.WindowsForms.WaveformGraph waveformGraph1;
        private NationalInstruments.UI.WaveformPlot waveformPlot1;
        private NationalInstruments.UI.XAxis xAxis1;
        private NationalInstruments.UI.YAxis yAxis1;
        private System.Windows.Forms.Label waveFormNameLabel;
        private System.Windows.Forms.Label channelLabel;


    }
}
