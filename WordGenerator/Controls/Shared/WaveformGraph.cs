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
    public class WaveformGraph : UserControl
    {
        private System.Windows.Forms.Label waveFormNameLabel;
        private System.Windows.Forms.Label channelLabel;
        private bool editable;

        private WaveformEditor waveformEditor;

       
        /// <summary>
        /// This event gets raised if the waveform graph gets clicked on. It is meant to be caught by the waveform collection.
        /// </summary>
        public event EventHandler gotClicked;

        //***Graphics Related Coords: Stuff in here holds pixel values for drawing in the OnPaint method
        //Margins
        private static readonly int g_mar_x_left = 30;
        private static readonly int g_mar_x_right = 20;
        private static readonly int g_mar_y_top = 50;
        private static readonly int g_mar_y_bottom = 30;
        private static readonly int g_y_plot_mar = 0;
        private static readonly int g_x_plot_mar=1;
        private static readonly int g_control_size=248;
        //WHY? can I make these static somehow?
        private int g_active_width;
        private int g_active_height;
        //End margins

        //Scale tick locations and spacings
        //These are all doubles which are type-cast into int when they are used to draw. The reason for this is to
        //minimize roundoff error in drawing.
        private double g_y_tick_bottom;
        private double g_y_tick_top;
        private double g_x_tick_spacing;
        private double g_y_tick_spacing;

        private double x_label_spacing;
        private double y_label_spacing;
        private double y_label_min;
        private double y_label_max;
        private double x_label_min;
        private double x_label_max;


        private static readonly int g_ticksize=5;
        //Waveform in pixels
        private Point [] g_waveform;
        
        //***End Graphics related Coords.

        private static readonly int NumSamples =2*(g_control_size - g_mar_x_right - g_mar_x_left);
        

        public WaveformGraph()
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
            this.channelLabel.Text = "";
            this.channelLabel.Click += new EventHandler(clicked);
            
            // 
            // WaveformGraph2
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.channelLabel);
            this.Controls.Add(this.waveFormNameLabel);
            this.Name = "WaveformGraph";
            this.Size = new System.Drawing.Size(g_control_size, g_control_size);
            this.Click += new EventHandler(clicked);
            this.ResumeLayout(false);
            this.PerformLayout();

            #endregion
            
            //WHY? related to making these static
            g_active_width = this.Size.Width - g_mar_x_left - g_mar_x_right;
            g_active_height = this.Size.Height - g_mar_y_bottom - g_mar_y_top;
            g_waveform = new Point[NumSamples];

            g_y_tick_bottom=this.Size.Height-g_mar_y_bottom-g_y_plot_mar;
            g_y_tick_top=g_mar_y_top;
            g_y_tick_spacing=g_active_height/((double)5);
            y_label_spacing=1;//Start off with 1V divisions on the y scale
            y_label_min=0;
            y_label_max=0;
            x_label_max=0;
            x_label_min=0;
        }

        public WaveformGraph(Waveform waveform, WaveformEditor waveformEditor, bool waveformEditable)
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
                this.BackColor = Color.Tan;
                this.channelLabel.BackColor = Color.Tomato;
                this.waveFormNameLabel.BackColor = Color.Tomato;
            }
        }

        public void Deactivate()
        {
            if (editable)
            {
                this.BackColor = Color.White;
                this.channelLabel.BackColor = Color.White;
                this.waveFormNameLabel.BackColor = Color.White;
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

   

        public void updateGraph(Object sender, EventArgs e) {
            // TODO !!
            // Part of this code (the part that generates a preview interpolation of the waveform) can
            // be copied from existing WaveformGraph class.
            //
            // This interpolation probably gets stored internal to the object in some way,
            // and then updateGraph should call this.Refresh(), which will request that the operating system repaint the control.
            if (waveform!=null)
                {
                    this.waveFormNameLabel.Text = waveform.WaveformName;
                    //this.channelLabel.Text = "placeholder";

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
                        double duration = waveform.WaveformDuration.getBaseValue(); //Duration is in seconds
                        double stepSize = duration / (double)nSamples;
                        double g_stepSize = (g_active_width-g_x_plot_mar)/(double)nSamples;

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

                       
                        

                        if ((maxY-minY) <= 1e-7)
                        {//If we have essential a flat-line waveform, make the surrounding yScale +/-1V of the Waveform value
                            maxY = maxY+1;
                            minY = minY-1;
                            
                        }
                        if (duration<1e-10)//Less than 1ns durations are ridiculous
                        {
                            duration=1e-9;//clamp it to a nanosecond? WHY?
                        }

                        NiceScale yScale = new NiceScale(minY, maxY);
                        NiceScale xScale = new NiceScale(0, duration);


                        y_label_min=yScale.getNiceMin();
                        y_label_max=yScale.getNiceMax();

                        x_label_min=xScale.getNiceMin();
                        x_label_max=xScale.getNiceMax();


                        double yRange = y_label_max - y_label_min;
                        double xRange= x_label_max-x_label_min;
                        

                        double yScaleFactor=(g_active_height-g_y_plot_mar)/yRange;
                        double xScaleFactor=(g_active_width-g_x_plot_mar)/xRange;
                        x_label_spacing=xScale.getTickSpacing();
                        g_x_tick_spacing=x_label_spacing*xScaleFactor;

                        y_label_spacing=yScale.getTickSpacing();
                        g_y_tick_spacing=y_label_spacing*yScaleFactor;
                       // if (yRange>1e-7)//Use 100nV as resolution cutoff beyong which the axes will just be static
                       // {
                            for (int i = 0; i < nSamples; i++)
                            {
                                g_waveform[i].X = g_mar_x_left + (int)(i * g_stepSize) + g_x_plot_mar;//We add a single pixel offset to the X-coordinate so that the 
                                //waveform doesn't overlap with the y-axis. This is purely for aesthetics.
                                g_waveform[i].Y=(int)((g_active_height-g_y_plot_mar)-(yValues[i]-y_label_min)*yScaleFactor+g_mar_y_top);                 
                            }
                      //  }
                      //  else
                      /*  {
                            
                            for (int i = 0; i < nSamples; i++)
                            {
                                g_waveform[i].X = g_mar_x_left + (int)(i * g_stepSize);
                                g_waveform[i].Y = this.Size.Height-g_mar_y_bottom-(int)(g_active_width/2);
                            }
                        }*/
                    
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
                    channelLabel.Text = "D:";
                }
            
            this.Refresh();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            // TODO !!
            // Paint the waveform preview in the appropriate region of this control.


            //Draw random line accross paint pane
            // e.Graphics.DrawLine(Pens.Bisque,0,0,this.Size.Width,this.Size.Height);
           
           e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
           drawBorder(e);
           drawAxes(e);
           drawWaveform(e); 
           base.OnPaint(e);
        }

        private void drawAxes(PaintEventArgs e)
        {
            //TODO will draw the axes given bounds in the input
            
           
            //Draw x axis
            Pen axisDrawer=new Pen(Color.Black,1.4F);
            Pen tickDrawer=new Pen(Color.White, 1.5F);
            e.Graphics.DrawLine(axisDrawer,g_mar_x_left,this.Size.Height-g_mar_y_bottom,this.Size.Width-g_mar_x_right,this.Size.Height-g_mar_y_bottom);
            //Draw y axis
            e.Graphics.DrawLine(axisDrawer, g_mar_x_left, g_mar_y_top, g_mar_x_left, this.Size.Height -g_mar_y_bottom);

            //Draw y ticks:
            int bottom_tick=this.Size.Height-g_mar_y_bottom-g_y_plot_mar;
            int tick_mark=bottom_tick;

            //The following loop is probably more elegantly done with a "for" loop, but I got confused as to when the for loop
            //checks the termination condition (leading to an extra drawn tick), so I just used a while loop
            int i=0;
            while(true)
            {
                 tick_mark = (int)(bottom_tick - i * g_y_tick_spacing);
                 if (tick_mark<g_mar_y_top)
                    break;

                 e.Graphics.DrawLine(tickDrawer, g_mar_x_left + g_x_plot_mar, tick_mark, g_mar_x_left + g_x_plot_mar + g_ticksize, tick_mark);
                Font scaleFont = new Font("Arial", 10);
                SolidBrush scaleBrush= new SolidBrush(Color.Black);

                //Draw the axes tick label. The -25 and -7 in the coordinates are just to get the Font spacing correct.
                e.Graphics.DrawString(Convert.ToString(y_label_min+i*y_label_spacing),scaleFont, scaleBrush, g_mar_x_left-25, tick_mark-7);
      
                i++;
            }


            //Draw x ticks:
            int left_tick = g_mar_x_left+g_x_plot_mar;
            tick_mark = left_tick;

            //The following loop is probably more elegantly done with a "for" loop, but I got confused as to when the for loop
            //checks the termination condition (leading to an extra drawn tick), so I just used a while loop
            i = 0;
            while (true)
            {
                tick_mark = (int)(left_tick +i * g_x_tick_spacing);
                if (tick_mark > this.Size.Width-g_mar_x_right)
                    break;

                e.Graphics.DrawLine(tickDrawer, tick_mark, this.Size.Height - g_mar_y_bottom, tick_mark, this.Size.Height - g_mar_y_bottom - g_ticksize);
                Font scaleFont = new Font("Arial", 10);
                SolidBrush scaleBrush = new SolidBrush(Color.Black);

                //Draw the axes tick label. The -5 and +2 in the coordinates are just to get the Font spacing correct.
                e.Graphics.DrawString(Convert.ToString(x_label_min + i * x_label_spacing), scaleFont, scaleBrush, tick_mark - 5, this.Size.Height - g_mar_y_bottom+5);

                i++;
            }
      
        }

        private void drawWaveform(PaintEventArgs e)
        {
             
            Pen plotDrawer = new Pen(Color.Chartreuse,1.5F);
            //Draw Waveform
            e.Graphics.DrawLines(plotDrawer, g_waveform);
        }

        private void drawBorder(PaintEventArgs e)
        {

            
            //Draws the black rectangle for the plot
            System.Drawing.SolidBrush boxDrawer = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            e.Graphics.FillRectangle(boxDrawer, g_mar_x_left, g_mar_y_top, g_active_width, g_active_height);
            boxDrawer.Dispose();
        }
    }
}
