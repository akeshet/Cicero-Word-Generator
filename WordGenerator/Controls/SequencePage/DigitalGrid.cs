using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using wgControlLibrary;
using DataStructures;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace WordGenerator.Controls
{
    public class DigitalGrid : UserControl
    {
        private Bitmap buffer;


        private Point clickStartPoint;
        private Point clickEndPoint;
        /// <summary>
        /// True between the time of MouseDown and MouseUp.
        /// </summary>
        bool mouseClicking = false;


        private ComboBox pulseSelector;
        private DigitalDataPoint pulseSelectorTarget;
        private Point pulseSelectorPoint;

        /// <summary>
        /// Handles only right mouse clicks.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!WordGenerator.mainClientForm.instance.lockDigitalCheckBox.Checked)
            {

                //         base.OnMouseClick(e);

                if (e.Button == MouseButtons.Right)
                {
                    if (Storage.sequenceData.DigitalPulses.Count != 0)
                    {
                        Point clickPoint = clickPixelToCellPoint(e.X, e.Y);

                        DigitalDataPoint dp = this.cellPointToDigitalDataPoint(clickPoint);
                        if (dp != null)
                        {
                            ComboBox pulseSelectBox = new ComboBox();
                            pulseSelectBox.DropDownStyle = ComboBoxStyle.DropDownList;
                            pulseSelectBox.Items.Clear();

                            pulseSelectBox.Items.Add("None.");
                            foreach (Pulse pulse in Storage.sequenceData.DigitalPulses)
                            {
                                pulseSelectBox.Items.Add(pulse);
                            }
                            pulseSelectBox.Width = colWidth;
                            pulseSelectBox.Height = rowHeight;
                            pulseSelectBox.Location = cellPointToClickPixel(clickPoint.X, clickPoint.Y);

                            pulseSelector = pulseSelectBox;
                            pulseSelectorTarget = dp;
                            pulseSelectorPoint = clickPoint;

                            pulseSelector.Visible = true;

                            this.Controls.Add(pulseSelector);

                            pulseSelector.DropDownClosed += new EventHandler(pulseSelector_DropDownClosed);
                            pulseSelector.DroppedDown = true;

                        }
                    }

                }
            }
        }



        void pulseSelector_DropDownClosed(object sender, EventArgs e)
        {
            if (pulseSelectorTarget != null)
            {
                Pulse val = pulseSelector.SelectedItem as Pulse;
                pulseSelectorTarget.DigitalPulse = val;
            }
            this.Controls.Remove(pulseSelector);

            refreshCell(Graphics.FromImage(buffer), pulseSelectorPoint);

            pulseSelector.Dispose();
            pulseSelector = null;
            pulseSelectorTarget = null;

            this.Invalidate();
            WordGenerator.mainClientForm.instance.sequencePage1.updateAllPulseIndicators();

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!WordGenerator.mainClientForm.instance.lockDigitalCheckBox.Checked)
            {
                //        base.OnMouseDown(e);

                if (e.Button == MouseButtons.Left)
                {
                    if (!mouseClicking)
                    {
                        clickStartPoint = clickPixelToCellPoint(e.X, e.Y);
                        clickEndPoint = clickStartPoint;
                        mouseClicking = true;
                        drawDrag();
                    }
                }
            }
        }

       
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!WordGenerator.mainClientForm.instance.lockDigitalCheckBox.Checked)
            {

                //         base.OnMouseMove(e);

                if (mouseClicking)
                {
                    Point oldEnd = clickEndPoint;
                    if (mouseClicking)
                    {
                        clickEndPoint = clickPixelToCellPoint(e.X, e.Y);
                    }
                    if (oldEnd != clickEndPoint)
                    {
                        drawDrag();
                    }
                }
                else
                {
                    Point temp = clickPixelToCellPoint(e.X, e.Y);
                    if (temp != oldHoverPoint)
                    {

                        if (buffer == null)
                        {
                            createAndPaintNewBuffer();

                        }



                        Graphics g = Graphics.FromImage(buffer);

                        if (oldHoverPoint.X >= 0 && oldHoverPoint.Y >= 0)
                        {
                            uncolorHover(g, oldHoverPoint);

                        }



                        colorHover(temp, g);
                        oldHoverPoint = temp;

                        g.Dispose();
                        this.Refresh();


                    }

                }
            }
        }


        private void uncolorHover(Graphics g, Point p)
        {
            if (!invalidated)
            {
                Pen blackPen = Pens.Black;

              //  g.DrawLine(blackPen, 0, p.Y * rowHeight, Width, p.Y * rowHeight);
              //  g.DrawLine(blackPen, 0, (p.Y + 1) * rowHeight, Width, (p.Y + 1) * rowHeight);
                g.DrawLine(blackPen, p.X * ColWidth, 0, p.X * ColWidth, Height);
                g.DrawLine(blackPen, (p.X + 1) * ColWidth, 0, (p.X + 1) * ColWidth, Height);

                refreshCell(g, p);



             /*  TimestepEditor te = WordGenerator.mainClientForm.instance.sequencePage1.getTimestepEditor(
                   Storage.sequenceData.getNthDisplayedTimeStep(p.X));

                te.BorderStyle = BorderStyle.None;
                */
                /*if (preHoverColor!=null)
                    te.BackColor = preHoverColor;
                else 
                    te.BackColor = Color.Transparent;
                */

            }
        }

        TimestepEditor oldTe;
        Label oldChanLab;

        private void colorHover(Point temp, Graphics g)
        {
            if (!invalidated)
            {
                g.FillRectangle(trueBrush(temp.Y), temp.X * colWidth + 10, temp.Y * rowHeight + 3, colWidth - 20, rowHeight - 6);

                Pen whitePen = Pens.White;

               // g.DrawLine(whitePen, 0, temp.Y * rowHeight, Width, temp.Y  * rowHeight);
               // g.DrawLine(whitePen, 0, (temp.Y +1)* rowHeight, Width, (temp.Y+1) * rowHeight);
                g.DrawLine(whitePen, temp.X * ColWidth, 0, temp.X * ColWidth, Height);
                g.DrawLine(whitePen, (temp.X + 1) * ColWidth, 0, (temp.X + 1) * ColWidth, Height);

                DigitalDataPoint dp = cellPointToDigitalDataPoint(temp);
                if (dp != null)
                {
                    if (dp.DigitalPulse != null)
                    {
                        paintPulse(g, temp, dp);
                    }
                }

                TimestepEditor te = 
                WordGenerator.mainClientForm.instance.sequencePage1.getTimestepEditor(
                   Storage.sequenceData.getNthDisplayedTimeStep(temp.X));

             /*   Label chanLab = WordGenerator.mainClientForm.instance.sequencePage1.digitalChannelLabelsPanel1.channelLabels[temp.Y*2];

                if (oldChanLab != chanLab)
                {
                    if (oldChanLab != null)
                    {
                        oldChanLab.BorderStyle = BorderStyle.None;
                    }
                    chanLab.BorderStyle = BorderStyle.FixedSingle;
                    
                    oldChanLab = chanLab;
                }
                */

                if (oldTe != te)
                {
                    if (oldTe != null)
                    {
                        oldTe.BorderStyle = BorderStyle.None;
                    }
                    te.BorderStyle = BorderStyle.FixedSingle;
                    oldTe = te;
                }


               /* preHoverColor = te.BackColor;

                te.BackColor = Color.Tan;
                */
            }
        }

        Point oldHoverPoint = new Point(-1, -1);

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!WordGenerator.mainClientForm.instance.lockDigitalCheckBox.Checked)
            {

                //        base.OnMouseLeave(e);

                if (mouseClicking)
                {
                    mouseClicking = false;
                    drawDrag();
                }
                else
                {
                    if (buffer == null)
                    {
                        createBuffer();
                        createAndPaintNewBuffer();
                    }

                    Graphics g = Graphics.FromImage(buffer);

                    uncolorHover(g, oldHoverPoint);

                    if (oldTe != null)
                    {
                        oldTe.BorderStyle = BorderStyle.None;
                        oldTe = null;
                    }

                    if (oldChanLab != null)
                    {
                        oldChanLab.BorderStyle = BorderStyle.None;
                        oldChanLab = null;
                    }

                    oldHoverPoint = new Point(-1, -1);

                    g.Dispose();
                    this.Refresh();

                }
            }
        }


        [DllImport("user32.dll")]
        static extern bool GetKeyboardState(byte[] lpKeyState);

        private bool CTRL_is_pressed()
        {
            byte[] answerBuffer = new byte[256];
            GetKeyboardState(answerBuffer);
            return (answerBuffer[0x11]&128)!=0; // 0x11 is the code for VK_CONTROL
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!WordGenerator.mainClientForm.instance.lockDigitalCheckBox.Checked)
            {
                //          base.OnMouseUp(e);

                

                if (e.Button == MouseButtons.Left)
                {
                    bool useContinue = false;

                    if (CTRL_is_pressed())
                    {
                        useContinue = true;
                    }

                    if (mouseClicking)
                    {
                        mouseClicking = false;

                        int start = Math.Min(clickStartPoint.X, clickEndPoint.X);
                        int end = Math.Max(clickStartPoint.X, clickEndPoint.X);


                        for (int col = start; col <= end; col++)
                        {
                            DigitalDataPoint dp = cellPointToDigitalDataPoint(new Point(col, clickStartPoint.Y));
                            if (dp != null)
                            {
                                if (dp.variable == null)
                                {
                                    if (useContinue)
                                    {
                                        dp.DigitalContinue = true;
                                    }
                                    else
                                    {
                                        if (dp.DigitalContinue)
                                        {
                                            dp.DigitalContinue = false;
                                            dp.ManualValue = false;
                                        }
                                        else
                                        {
                                            dp.ManualValue = !dp.ManualValue;
                                        }
                                    }
                                }
                            }
                        }




                        drawDrag();

                        clickStartPoint = new Point(-1, -1);
                        clickEndPoint = new Point(-1, -1);
                    }
                }
            }
        }

        

        private void drawDrag()
        {


            if (buffer == null)
            {
                createBuffer();
                createAndPaintNewBuffer();
            }

            if (!invalidated)
            {



                int row = clickStartPoint.Y;
                Graphics g = Graphics.FromImage(buffer);

                // redraw the whole row of the starting point
                for (int col = 0; col < Storage.sequenceData.getNDisplayedTimeSteps(); col++)
                {
                    refreshCell(g, new Point(col, row));
                }

                if (mouseClicking)
                {

                    // now draw drag brushes
                    int start = Math.Min(clickStartPoint.X, clickEndPoint.X);
                    int end = Math.Max(clickStartPoint.X, clickEndPoint.X);
                    for (int col = start; col <= end; col++)
                    {
                        paintCell(g, new Point(col, row), draggingBrush, normalOutlinePen);
                    }

                }

                g.Dispose();
                this.Refresh();
            }
        }

        protected override void OnMouseCaptureChanged(EventArgs e)
        {
            //base.OnMouseCaptureChanged(e);
        }



        private int colWidth = 0;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ColWidth
        {
            get { return colWidth; }
            set {
                colWidth = value; 
            }
        }
        private int rowHeight = 0;

        public int RowHeight
        {
            get { return rowHeight; }
            set { rowHeight = value; }
        }
        
        public DigitalGrid() : base()
        {


            this.DoubleBuffered = true;


        }



        /// <summary>
        /// Array of brushes used to fill in "true" cells. Also, array of corresponding colors used in digitalchannellabelspanel.
        /// </summary>


        public static List<Color> TrueBrushColors
        {
            get {
                return Storage.settingsData.DigitalGridColors;
            }
        } 


        
        private Brush trueBrush(int row)
        {
            return new SolidBrush(TrueBrushColors[row % TrueBrushColors.Count]);
        }

        private Brush continueBrush(int row)
        {
            return new HatchBrush(HatchStyle.NarrowHorizontal, TrueBrushColors[row % TrueBrushColors.Count], Color.Tan);
           // return new HatchBrush(HatchStyle.DarkUpwardDiagonal, TrueBrushColors[row % TrueBrushColors.Count], Color.Tan);
        }

        private static readonly Brush falseBrush = Brushes.Tan;

        private static readonly Pen normalOutlinePen = Pens.Black;
        private static readonly Pen pulseOutlintPen = Pens.White;

        private static readonly Brush draggingBrush = new HatchBrush(HatchStyle.Percent20, Color.Black, Color.Transparent);

        private static readonly Brush variableBrush = Brushes.Thistle;

        private static readonly Brush nullBrush = Brushes.Gray;

        private static readonly Font variableFont = new Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        /// <summary>
        /// Converts a pixel position to a grid cell position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Point pixelToCellPoint(int x, int y)
        {
            if (colWidth == 0 || rowHeight == 0) return new Point(0, 0);

            int pointx;
            int pointy;
            pointx = x / colWidth;
            pointy = y / rowHeight;
            return new Point(pointx, pointy);
        }

        private Point cellPointToClickPixel(int x, int y)
        {
            Point ans = new Point();
            ans.X = x * colWidth ;
            ans.Y = y * rowHeight ;
            return ans;
        }

        private Point clickPixelToCellPoint(int x, int y)
        {
            return pixelToCellPoint(x , y );
        }


        private DigitalDataPoint cellPointToDigitalDataPoint(Point p)
        {
            if (Storage.sequenceData != null)
            {
                TimeStep step = Storage.sequenceData.getNthDisplayedTimeStep(p.X);
                if (step == null) return null;

                List<int> channelIDs = Storage.settingsData.logicalChannelManager.ChannelCollections[HardwareChannel.HardwareConstants.ChannelTypes.digital].getSortedChannelIDList();
                if (channelIDs.Count <= p.Y)
                    return null;

                int channelID = channelIDs[p.Y];

                if (step.DigitalData.ContainsKey(channelID))
                    return step.DigitalData[channelID];
                else
                    return null;
            }
            else return null;
        }

        public void GetScrollEvent(ScrollEventArgs e)
        {
            this.OnScroll(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }

        #region Painting

        /// <summary>
        /// Fills specified cell with specified brush.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        /// <param name="brush"></param>
        private void paintCell(Graphics g, Point p, Brush fill, Pen outline)
        {
            g.FillRectangle(fill, p.X * colWidth, p.Y * rowHeight, colWidth, rowHeight);
            g.DrawRectangle(outline, p.X * colWidth, p.Y * rowHeight, colWidth, rowHeight);
        }

        private void painCellRectInternal(Graphics g, Point p, Pen pen)
        {
            g.DrawRectangle(pen, p.X * colWidth + 1, p.Y * rowHeight + 1, colWidth - 2, rowHeight - 2);
        }

        private void drawCellTopAndBottomInternalLines(Graphics g, Point p, Pen pen)
        {
            g.DrawLine(pen, p.X * colWidth + 1, p.Y * rowHeight + 1, (p.X + 1) * colWidth - 1, p.Y * rowHeight + 1);
            g.DrawLine(pen, p.X * colWidth + 1, (p.Y+1) * rowHeight - 1, (p.X + 1) * colWidth - 1, (p.Y+1) * rowHeight -1);
        }

        public void updateSize()
        {
            //int height = rowHeight * Storage.settingsData.logicalChannelManager.ChannelCollections[HardwareChannel.HardwareConstants.ChannelTypes.digital].Count;
            //int width = Storage.sequenceData.getNDisplayedTimeSteps() * colWidth;

            Size gSize = GridDisplaySize();

            this.Width = gSize.Width;
            this.Height = gSize.Height;

            SuspendLayout();

            // This stupid shit is designed to carefully force the horizontal scrollbar to appear, if necessary, but not force the vertical.

/*            if (gSize.Width <= containerSize.Width)
            {
                if (gSize.Height <= containerSize.Height)
                { // no vertical scrollbars
                    this.Width = containerSize.Width + 1000;
                }
                else
                {
                    this.Width = containerSize.Width + 1 - scrollbarSize;
                }
            }
            else
            {
                this.Width = Math.Max(containerSize.Width+1, gSize.Width);
            }
            this.Height = Math.Max(containerSize.Height, gSize.Height);*/

            destroyBuffer();

            ResumeLayout();
            this.Invalidate();

        }

        private Size GridDisplaySize()
        {
            if ((Storage.sequenceData != null) && (Storage.settingsData != null))
            {
                return new Size(Storage.sequenceData.getNDisplayedTimeSteps() * colWidth, 
                    rowHeight * Storage.settingsData.logicalChannelManager.ChannelCollections[HardwareChannel.HardwareConstants.ChannelTypes.digital].Count);
            }
            else return new Size(1, 1);
        }

        private Size containerSize;

        public Size ContainerSize
        {
            get { return containerSize; }
            set { containerSize = value;
          //  updateSize();
            }
        }

        /// <summary>
        /// Gets set to true when grid is invalidated, and set to false after its been redrawn. This is to avoid having other parts of the
        /// code draw onto the buffer before it gets fully re-drawn, during events that should cause redraws.
        /// </summary>
        private bool invalidated;

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);
            invalidated = true;
        }

        private void destroyBuffer()
        {
            if (buffer != null)
                buffer.Dispose();
            buffer = null;

            this.Invalidate();
        }

        /// <summary>
        /// Fills a cell with appropriate brush based on its data, and writes variable names if under variable control.
        /// </summary>
        private void refreshCell(Graphics g, Point p)
        {
            DigitalDataPoint dp = cellPointToDigitalDataPoint(p);

            Pen outlinePen = normalOutlinePen ;
           /* if (dp != null)
            {
                if (dp.usesPulse())
                {
                    outlinePen = pulseOutlintPen;
                }
                else
                {
                    outlinePen = normalOutlinePen;
                }
            }*/
         

            if (dp == null)
            {
                paintCell(g, p, nullBrush, outlinePen);
                return;
            }

            if (dp.variable == null)
            {
                Brush br;
                if (dp.DigitalContinue)
                {
                    br = continueBrush(p.Y);
                }
                else if (dp.ManualValue)
                {
                    br = trueBrush(p.Y);
                    // paintCell(g, p, trueBrush(p.Y), outlinePen);
                }
                else
                {
                    br = falseBrush;
                    // paintCell(g, p, falseBrush, outlinePen);
                    //painCellRectInternal(g, p, new Pen(trueBrush(p.Y)));
                    //drawCellTopAndBottomInternalLines(g, p, new Pen(trueBrush(p.Y)));
                }
                paintCell(g, p, br, outlinePen);

            }
            else
            {
                // this will probably never get used. Though there is functionality for having a variable value for a digital
                // this does not get set anywhere. Probably this is superseeded by pulse capability
                paintCell(g, p, variableBrush, outlinePen);
                g.DrawString(dp.variable.ToString(), variableFont, Brushes.Black, new RectangleF(p.X * colWidth, p.Y * rowHeight, colWidth, rowHeight));
            }

            if (dp.DigitalPulse != null)
            {
                p = paintPulse(g, p, dp);
            }         
        }

        private Point paintPulse(Graphics g, Point p, DigitalDataPoint dp)
        {
            painCellRectInternal(g, p, pulseOutlintPen);
            Brush textBrush = Brushes.White;
            if (!dp.getValue() && !dp.DigitalContinue)
            {
                textBrush = Brushes.Black;
            }
            g.DrawString(dp.DigitalPulse.PulseName, variableFont, textBrush, new Rectangle(p.X * colWidth + 2, p.Y * rowHeight + 2, colWidth - 4, rowHeight - 4));
            return p;
        }



        protected override void OnPaint(PaintEventArgs e)
        {

            if (buffer == null)
            {
                createAndPaintNewBuffer();
            }


         // e.Graphics.DrawImageUnscaled(buffer, new Point(-HorizontalScroll.Value, -VerticalScroll.Value));
            
         
       //     e.Graphics.DrawImageUnscaledAndClipped(buffer, new Point(-HorizontalScroll.Value, -VerticalScroll.Value), e.ClipRectangle);

            Rectangle sourceRectangle = new Rectangle(e.ClipRectangle.X , e.ClipRectangle.Y , e.ClipRectangle.Width, e.ClipRectangle.Height);

            e.Graphics.DrawImage(buffer, e.ClipRectangle, sourceRectangle, GraphicsUnit.Pixel);

            base.OnPaint(e);

            invalidated = false;

        }

        private void createAndPaintNewBuffer()
        {

                createBuffer();
                PaintEventArgs args = new PaintEventArgs(Graphics.FromImage(buffer), new Rectangle(0, 0, Width, Height));
                repaintBuffer(args);
            
        }

        private void createBuffer()
        {
            if (Width == 0 || Height == 0)
            {
                if (buffer != null)
                    buffer.Dispose();
                buffer = null;
                buffer = new Bitmap(1, 1);
            }
            else
            {
                if (buffer != null)
                {
                    buffer.Dispose();
                }
                buffer = null;
                buffer = new Bitmap(Width, Height);
            }
        }

        private void repaintBuffer(PaintEventArgs e)
        {
            int currentCol = 0;

            while (true)
            {
                if (Storage.sequenceData == null || Storage.settingsData == null) return;
                TimeStep currentStep = Storage.sequenceData.getNthDisplayedTimeStep(currentCol);
                if (currentStep == null) break;

                for (int row = 0;
                    row < Storage.settingsData.logicalChannelManager.ChannelCollections[HardwareChannel.HardwareConstants.ChannelTypes.digital].Count;
                    row++)
                {
                    refreshCell(e.Graphics, new Point(currentCol, row));
                }
                currentCol++;
            }
        }



        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DigitalGrid
            // 
            this.Name = "DigitalGrid";
            this.ResumeLayout(false);

        }




    }
}
