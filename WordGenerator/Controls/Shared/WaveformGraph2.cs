using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DataStructures;

namespace WordGenerator.Controls
{

    /// <summary>
    /// A new version of the WaveformGraph UI widget, rewritten from the ground up to be
    /// cleaner and to not use NationalInstruments UI librares (thus removing our dependance on them).
    /// 
    /// Some placeholder code included here, indicating what parts of the object need to get written.
    /// </summary>
    public class WaveformGraph2 : UserControl
    {
        private System.Windows.Forms.Label waveFormNameLabel;
        private System.Windows.Forms.Label channelLabel;
        private bool editable;

        /// <summary>
        /// This event gets raised if the waveform graph gets clicked on. It is meant to be caught by the waveform collection.
        /// </summary>
        public event EventHandler gotClicked;

        public WaveformGraph2()
        {
            // Copied, with adaptations, from old WaveformGraph.Designer InitializeComponent
            #region InitializeComponent
            
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
            this.waveFormNameLabel.Click += new EventHandler(clicked);
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
            this.channelLabel.Click += new EventHandler(clicked);
            
            // 
            // WaveformGraph2
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.channelLabel);
            this.Controls.Add(this.waveFormNameLabel);
            this.Name = "WaveformGraph";
            this.Size = new System.Drawing.Size(248, 248);
            this.Click += new EventHandler(clicked);
            this.ResumeLayout(false);
            this.PerformLayout();

            #endregion
        }

        public WaveformGraph2(Waveform waveform, WaveformEditor waveformEditor, bool waveformEditable)
            : this()
        {
            // TODO: Write this function.
        }

        public void Activate()
        {
            if (editable)
            {
                this.BackColor = Color.Olive;
            }
        }

        public void Deactivate()
        {
            if (editable)
            {
                this.BackColor = Color.Snow;
            }
        }

        void clicked(object sender, EventArgs e)
        {
            if (gotClicked != null)
            {
                gotClicked(sender, e);
            }
        }

        private Waveform waveform;

        public void setWaveform(Waveform waveform)
        {
            this.waveform = waveform;
            this.updateGraph(this, null);
        }

        public Waveform getWaveform()
        {
            return waveform;
        }



        public void updateGraph(Object sender, EventArgs e) {
            // TODO !!
            // Part of this code (the part that generates a preview interpolation of the waveform) can
            // be copied from existing WaveformGraph class.
            //
            // This interpolation probably gets stored internal to the object in some way,
            // and then updateGraph should call this.Refresh(), which will request that the operating system repaint the control.
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            // TODO !!
            // Paint the waveform preview in the appropriate region of this control.

            base.OnPaint(e);
        }

    }
}
