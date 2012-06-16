using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DataStructures;
using System.Drawing.Drawing2D;

namespace WordGenerator.Controls
{
    public partial class AnalogPreviewPane : UserControl
    {
        /// <summary>
        /// a kludge to make autoscroll recognize the full size of the active area. 
        /// </summary>
        private Panel placeholderPanel;


        private bool enabled = false;
        public void enable()
        {
            this.enabled = true;
        }

        private Bitmap buffer;

        private Point cursorPoint;
        private bool drawCursor;
        private double value;


        private Dictionary<int, double> maxValues;
        private Dictionary<int, double> minValues;



        public AnalogPreviewPane()
        {
            InitializeComponent();
            placeholderPanel = new Panel();
            placeholderPanel.Location = new Point(0, 0);
            placeholderPanel.Width = 0;
            placeholderPanel.Height = 0;
            placeholderPanel.BackColor = Color.Transparent;

            this.Controls.Add(placeholderPanel);


            placeholderPanel.MouseClick += new MouseEventHandler(placeholderPanel_MouseClick);


            this.AutoScroll = true;

        }

        void placeholderPanel_MouseClick(object sender, MouseEventArgs e)
        {
            drawCursor = false;

            int chan = e.Y / rowHeight;
            int visStepId = e.X / colWidth;

            List<int> analogIDs = new List<int>(Storage.settingsData.logicalChannelManager.ChannelCollections[DataStructures.HardwareChannel.HardwareConstants.ChannelTypes.analog].Channels.Keys);
            analogIDs.Sort();

            int channelID;
            if (analogIDs.Count > chan)
            {
                channelID = analogIDs[chan];
            }
            else
            {
                Invalidate();
                return;
            }

            int stepNum = Storage.sequenceData.getNthDisplayedTimeStepID(visStepId);
            
            double lagtime = 0;
            int extraXPixels = e.X - visStepId * colWidth;
            double extraTime = Storage.sequenceData.TimeSteps[stepNum].StepDuration.getBaseValue() * ((double)extraXPixels / (double)colWidth);

            Waveform wf = Storage.sequenceData.getChannelWaveformAtTimestep(channelID, 
                stepNum, ref lagtime);

            double val;

            if (wf == null)
            {
                val = 0;
            }
            else
            {
                val = wf.getValueAtTime(lagtime + extraTime, Storage.sequenceData.Variables, Storage.sequenceData.CommonWaveforms);
            }

            if (!maxValues.ContainsKey(channelID))
                return;
            if (!minValues.ContainsKey(channelID))
                return;
            double maxValue = maxValues[channelID];
            double minValue = minValues[channelID];

            double scale;

            int offs = 0;

            if (maxValue == minValue)
            {
                scale = 0;
                offs = -rowHeight / 2;
            }
            else
                scale = -(double)(rowHeight - 2) / (maxValue - minValue);


            int y = offs + (int)(scale * (val - minValue) + (chan+1) * rowHeight)-1;



            drawCursor = true;
            cursorPoint = new Point(e.X , y );
            value = val;
            this.Invalidate();
        }

        public int colWidth;
        public int rowHeight;

        protected override void OnPaint(PaintEventArgs e)
        {
            
            if (enabled)
            {
                if (buffer != null)
                {

                    Rectangle sourceRectangle = new Rectangle(e.ClipRectangle.X + HorizontalScroll.Value, e.ClipRectangle.Y + VerticalScroll.Value, e.ClipRectangle.Width, e.ClipRectangle.Height);

                    e.Graphics.DrawImage(buffer, e.ClipRectangle, sourceRectangle, GraphicsUnit.Pixel);

                    if (drawCursor)
                    {
                        Point center = new Point(cursorPoint.X - HorizontalScroll.Value, cursorPoint.Y - VerticalScroll.Value);

                        e.Graphics.DrawLine(new Pen(Color.Fuchsia), center.X - 4, center.Y, center.X + 4, center.Y);
                        e.Graphics.DrawLine(new Pen(Color.Fuchsia), center.X, center.Y - 4, center.X, center.Y + 4);

                        e.Graphics.DrawString(value.ToString("0.####"), this.Font, Brushes.Fuchsia, center.X + 6, center.Y);
                    }
                }
            }
 
        }


        public void redrawBuffer()
        {
            try
            {
                drawCursor = false;
                if (WordGenerator.MainClientForm.instance != null)
                    WordGenerator.MainClientForm.instance.cursorWait();


                if (Storage.sequenceData == null)
                {
                    if (WordGenerator.MainClientForm.instance != null)
                        WordGenerator.MainClientForm.instance.cursorWaitRelease();
                    return;
                }

                int nDisplayedSteps = Storage.sequenceData.getNDisplayedTimeSteps();

                /// higher numbers here mean that each pixel actually gets more than one sample, to reduce aliasing.
                int samples_per_pixel = 1;

                int xSize = nDisplayedSteps * colWidth;
                int ySize = rowHeight * Storage.settingsData.logicalChannelManager.ChannelCollections[DataStructures.HardwareChannel.HardwareConstants.ChannelTypes.analog].Channels.Count;

                /*if ((xSize == 0) || (ySize == 0))
                {
                    WordGenerator.MainClientForm.instance.cursorWaitRelease();
                    return;
                }*/
                if (xSize == 0)
                    xSize = 1;
                if (ySize == 0)
                    ySize = 1;

                Graphics gc;

                if (placeholderPanel.Width != xSize || placeholderPanel.Height != ySize)
                {
                    placeholderPanel.Width = xSize;
                    placeholderPanel.Height = ySize;
                }


                if (buffer != null)
                {
                    if (buffer.Width != xSize || buffer.Height != ySize)
                    {
                        buffer.Dispose();
                        buffer = new Bitmap(xSize, ySize);
                        gc = Graphics.FromImage(buffer);

                    }
                    else
                    {
                        gc = Graphics.FromImage(buffer);
                    }
                }
                else
                {
                    buffer = new Bitmap(xSize, ySize);
                    gc = Graphics.FromImage(buffer);
                }

                gc.FillRectangle(Brushes.Black, 0, 0, xSize, ySize);



                List<int> analogIDs = new List<int>(Storage.settingsData.logicalChannelManager.Analogs.Keys);
                analogIDs.Sort();


                Brush continueBrush = new HatchBrush(HatchStyle.Percent20, Color.Green);
                Brush disabledBrush = new HatchBrush(HatchStyle.Percent20, Color.Red);

                double[] channelValues = new double[xSize * samples_per_pixel];
                for (int i = 0; i < analogIDs.Count; i++)
                {
                    int analogID = analogIDs[i];
                    for (int index = 0; index < xSize * samples_per_pixel; index++)
                    {
                        channelValues[index] = 0;
                    }

                    for (int j = 0; j < nDisplayedSteps; j++)
                    {
                        int stepID =
                            Storage.sequenceData.getNthDisplayedTimeStepID(j);

                        if (Storage.sequenceData.TimeSteps[stepID].StepEnabled)
                        {
                            try
                            {
                                Storage.sequenceData.getTimestepInterpolation(stepID, analogID, channelValues, colWidth * samples_per_pixel, j * colWidth * samples_per_pixel);
                            }
                            catch (InterpolationException)
                            {
                                WordGenerator.MainClientForm.instance.cursorWaitRelease();
                                return;
                            }
                        }
                        else
                        {
                            double fillValue;
                            if (j == 0)
                            {
                                fillValue = 0;
                            }
                            else
                            {
                                fillValue = channelValues[colWidth * j * samples_per_pixel - 1];
                            }
                            for (int k = 0; k < colWidth * samples_per_pixel; k++)
                            {
                                channelValues[k + j * colWidth * samples_per_pixel] = fillValue;
                            }
                        }

                    }
                    double maxValue = double.MinValue;
                    double minValue = double.MaxValue;
                    foreach (double samp in channelValues)
                    {
                        if (samp > maxValue)
                            maxValue = samp;
                        if (samp < minValue)
                            minValue = samp;
                    }

                    // we want to map each double value to a pixel point. y = m (samp + b)


                    // draw backgrounds

                    int y_offset = rowHeight * (i + 1);

                    for (int j = 0; j < Storage.sequenceData.getNDisplayedTimeSteps(); j++)
                    {
                        TimeStep step = Storage.sequenceData.getNthDisplayedTimeStep(j);
                        if (!step.StepEnabled)
                            gc.FillRectangle(disabledBrush, j * colWidth, y_offset - rowHeight, colWidth, rowHeight);
                        else if (step.AnalogGroup == null || !step.AnalogGroup.channelEnabled(analogID))
                        {

                            gc.FillRectangle(continueBrush, j * colWidth, y_offset - rowHeight, colWidth, rowHeight);

                        }
                    }

                    // draw divider lines.
                    Pen dividerPen = new Pen(Brushes.Gray);

                    gc.DrawLine(dividerPen, 0, i * rowHeight, xSize, i * rowHeight);

                    double scale;
                    int offs = 0;
                    if (maxValue == minValue)
                    {
                        scale = 0;
                        offs = -rowHeight / 2;
                    }

                    else
                        scale = -(double)(rowHeight - 2) / (maxValue - minValue);




                    int oldY = offs + (int)(scale * (channelValues[0] - minValue) + y_offset - 1);
                    int newY;

                    Pen linePen = new Pen(Color.White);

                    for (int x = 1; x < xSize * samples_per_pixel; x++)
                    {
                        newY = offs + (int)(scale * (channelValues[x] - minValue) + y_offset - 1);
                        gc.DrawLine(linePen, (x - 1) / samples_per_pixel, oldY, x / samples_per_pixel, newY);
                        oldY = newY;
                    }

                    if (maxValues == null)
                        maxValues = new Dictionary<int, double>();
                    if (minValues == null)
                        minValues = new Dictionary<int, double>();


                    if (!maxValues.ContainsKey(analogID))
                    {
                        maxValues.Add(analogID, 0);
                    }
                    if (!minValues.ContainsKey(analogID))
                    {
                        minValues.Add(analogID, 0);
                    }
                    maxValues[analogID] = maxValue;
                    minValues[analogID] = minValue;
                }
                if (WordGenerator.MainClientForm.instance != null)
                    WordGenerator.MainClientForm.instance.cursorWaitRelease();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Caught exception when trying to redraw analog preview buffer: " + ex.Message);
            }
        }

        private void AnalogPreviewPane_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

        }
    

    }
}
