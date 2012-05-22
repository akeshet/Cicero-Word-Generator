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

        private WaveformEditor waveformEditor;

        private static readonly int NumSamples = 500;
        /// <summary>
        /// This event gets raised if the waveform graph gets clicked on. It is meant to be caught by the waveform collection.
        /// </summary>
        public event EventHandler gotClicked;

        //***Graphics Related Coords: Stuff in here holds pixel values for drawing in the OnPaint method
        //Margins
        private int g_mar_x_left = 20;
        private int g_mar_x_right = 20;
        private int g_mar_y_top = 50;
        private int g_mar_y_bottom = 20;
        //End margins

        //Waveform in pixels
        private Point [] g_waveform;
        
        //***End Graphics related Coords.
        public WaveformGraph2()
        {
            // Copied, with adaptations, from old WaveformGraph.Designer InitializeComponent
            #region InitializeComponent

            this.waveFormNameLabel = new Label();
            this.channelLabel = new Label();
            
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
            g_waveform = new Point[NumSamples];
        }

        public WaveformGraph2(Waveform waveform, WaveformEditor waveformEditor, bool waveformEditable)
            : this()
        {
            // TODO: Write this function.
            setWaveform(waveform);
            setWaveformEditor(waveformEditor);
            this.editable = waveformEditable;
        }

        public void Activate()
        {
            if (editable)
            {
                this.BackColor = Color.Snow;
            }
        }

        public void Deactivate()
        {
            if (editable)
            {
                this.BackColor = Color.SlateGray;
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

        public void setChannelName(string channelName) {
            this.channelLabel.Text = channelName;
        }

        public void setWaveformEditor(WaveformEditor waveformEditor)
        {
          this.waveformEditor = waveformEditor;
        }

        int count =0;

        public void updateGraph(Object sender, EventArgs e) {
            // TODO !!
            // Part of this code (the part that generates a preview interpolation of the waveform) can
            // be copied from existing WaveformGraph class.
            //
            // This interpolation probably gets stored internal to the object in some way,
            // and then updateGraph should call this.Refresh(), which will request that the operating system repaint the control.
            if (waveform!=null)
                {
                    

                    //This I should probably keep over the two crap lines above
                    //his.waveFormNameLabel.Text = waveform.WaveformName;
                    //this.channelLabel.Text = channelName;

                    //WHY? do you do this? why not just use NumSamples?
                    int nSamples = NumSamples;
                    double[] yValues;
                    try
                    {
                        //yValues are in Volts
                        yValues = waveform.getInterpolation(nSamples, Storage.sequenceData.Variables, Storage.sequenceData.CommonWaveforms);
                        
                        double start = 0;
                        double duration = waveform.WaveformDuration.getBaseValue(); //Duration is in seconds
                        double stepSize = duration / (double)nSamples;
                        double g_active_width = this.Size.Width - g_mar_x_left - g_mar_x_right;
                        double g_active_height = this.Size.Height - g_mar_y_bottom - g_mar_y_top;
                        double g_stepSize = (g_active_width)/(double)nSamples;

                        //These will hold the max/min values of yValues
                        double maxY=-10000;
                        double minY=10000;
                        for (int i = 0; i < nSamples; i++)
                        {

                            //If statements to find min/max values of yValues
                            if (yValues[i]<minY)
                            {
                                minY=yValues[i];
                            }
                            if (yValues[i]>maxY)
                            {
                                maxY=yValues[i];
                            }
                        }

                       
                        waveFormNameLabel.Text = "MaxValue= "+maxY;
                        channelLabel.Text = "Minvalue= "+minY;

                        double yRange=maxY-minY;
                        double g_y_plot_mar=5;
                        double scaleFactor=(g_active_height-g_y_plot_mar)/yRange;
                       
                        if (yRange!=0)
                        {
                            for (int i = 0; i < nSamples; i++)
                            {
                                g_waveform[i].X=g_mar_x_left+(int)(i*g_stepSize);
                                g_waveform[i].Y=(int)((g_active_height-g_y_plot_mar)-(yValues[i]-minY)*scaleFactor+g_mar_y_top);
                                //g_waveform[i].Y = 200;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < nSamples; i++)
                            {
                                g_waveform[i].X = g_mar_x_left + (int)(i * g_stepSize);
                                //g_waveform[i].Y = (int)(g_active_width - (yValues[i] - minY) * scaleFactor - g_mar_y_bottom);
                                g_waveform[i].Y = this.Size.Height-g_mar_y_bottom-(int)(g_active_width/2);
                            }
                        }
                    
                    }
                    catch (InterpolationException exception)
                    {
                        // user changed something that caused an interpolation error. Let's try to undo it.
                        IParameterEditor ipe = sender as IParameterEditor;
                        if (ipe != null)
                        {
                            MessageBox.Show("Invalid data detected, attempting to undo.");
                            ipe.undoLastChange(this, null);
                            this.updateGraph(this, null);
                        }
                        else
                        {
                            MessageBox.Show("Invalid data, unable to undo. " + exception.Message);
                        }
                    }
                }
                else
                {
                 
                    waveFormNameLabel.Text = "I was Null";
                    channelLabel.Text = "is awesome";
                }
            
            this.Refresh();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            // TODO !!
            // Paint the waveform preview in the appropriate region of this control.


            //Draw random line accross paint pane
            // e.Graphics.DrawLine(Pens.Bisque,0,0,this.Size.Width,this.Size.Height);
           
           
           drawAxes(e);
            

            base.OnPaint(e);
        }

        private void drawAxes(PaintEventArgs e)
        {
            //TODO will draw the axes given bounds in the input
            
           
            //Draw x axis
            Pen axisDrawer=new Pen(Color.Black,2);
            e.Graphics.DrawLine(axisDrawer,g_mar_x_left,this.Size.Height-g_mar_y_bottom,this.Size.Width-g_mar_x_right,this.Size.Height-g_mar_y_bottom);
            //Draw y axis
            e.Graphics.DrawLine(axisDrawer, g_mar_x_left, g_mar_y_top, g_mar_x_left, this.Size.Height -g_mar_y_bottom);
            
            Pen plotDrawer=new Pen(Color.Blue, 2);
            //Draw Waveform
            e.Graphics.DrawLines(plotDrawer,g_waveform);
                
        }

    }
}
