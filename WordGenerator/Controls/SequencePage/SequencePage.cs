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
    public partial class SequencePage : UserControl
    {

        private bool created = false;

        private bool hideHiddenTimesteps = false;

        private bool horizontalScrollingEventsCalled = false;
        private bool verticalScrollingEventsCalled = false;

        public event EventHandler messageLog;

        public TimestepEditor getTimestepEditor(TimeStep timeStep)
        {
            foreach (Control con in timeStepsFlowPanel.Controls)
            {
                TimestepEditor editor = con as TimestepEditor;
                if (editor != null)
                {
                    if (editor.StepData == timeStep)
                    {
                        return editor;
                    }
                }
            }
            return null;
        }

        public void markAllTimesteps()
        {
            foreach (Control con in timeStepsFlowPanel.Controls)
            {
                TimestepEditor ed = con as TimestepEditor;
                if (ed != null)
                    ed.Marked = true;
            }
        }

        public void unmarkAllTimesteps()
        {
            foreach (Control con in timeStepsFlowPanel.Controls)
            {
                TimestepEditor ed = con as TimestepEditor;
                if (ed != null)
                    ed.Marked = false;
            }
        }

        /// <summary>
        /// True if the timesteps with their "hide" bool set to on should be hidden. False if they should be shown.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool HideHiddenTimesteps
        {
            get { return hideHiddenTimesteps; }
            set { 
                hideHiddenTimesteps = value;
                if (created)
                    layoutTimestepEditors();
            }
        }

        public SequencePage()
        {
            InitializeComponent();
            timeStepsPanel.HorizontalScroll.SmallChange = TimestepEditor.TimestepEditorWidth;
            digitalGrid1.ColWidth = TimestepEditor.TimestepEditorWidth;
            digitalGrid1.RowHeight = 18;
            created = true;

            timeStepsPanel.MouseWheel += new MouseEventHandler(timeStepsPanel_MouseWheel);

            this.digitalChannelLabelsPanel1.rowHeight = 18;

            analogPreviewPane1.colWidth = TimestepEditor.TimestepEditorWidth;
            analogPreviewPane1.rowHeight = 50;
            analogPreviewPane1.enable();


            digitalGrid1.ContainerSize = digitalGridPanel.Size;

          

            this.analogChannelLabelsPanel1.rowHeight = 50;


            toolTip1.SetToolTip(analogPreviewUpdate, "Re-draw the analog previous pane.");

            repairDigitalMargin();
            repairAnalogMargin();

            
        }

        public void insertTimestepEditor(TimestepEditor ed, int index)
        {
            registerTimestepEditorEvents(ed);
            timeStepsFlowPanel.Controls.Add(ed);
            timeStepsFlowPanel.Controls.SetChildIndex(ed, index);
            updateTimestepEditorNumbers();
            updateTimestepEditorsAfterSequenceModeOrTimestepGroupChange();
            layoutTheRest();
        }

        public void moveTimestepEditor(int currentIndex, int destinationIndex)
        {

            Control con = timeStepsFlowPanel.Controls[currentIndex];
            timeStepsFlowPanel.Controls.SetChildIndex(con, destinationIndex);
            updateTimestepEditorNumbers();
            updateTimestepEditorsAfterSequenceModeOrTimestepGroupChange();
            layoutTheRest();

        }

        public void removeTimestepEditor(TimestepEditor editor)
        {
            editor.unRegsiterHotkey();
            timeStepsFlowPanel.Controls.Remove(editor);
            editor.Dispose();
            updateTimestepEditorNumbers();
            updateTimestepEditorsAfterSequenceModeOrTimestepGroupChange();
            layoutTheRest();
        }

        public void updateTimestepEditorNumbers()
        {
            foreach (Control con in timeStepsFlowPanel.Controls)
            {
                TimestepEditor ed = con as TimestepEditor;
                if (ed != null)
                {
                    TimeStep step = ed.StepData;
                    if (Storage.sequenceData.TimeSteps.Contains(step))
                    {
                        int num = Storage.sequenceData.TimeSteps.IndexOf(step) + 1;
                        ed.StepNumber = num;
                    }
                }
            }
        }

        private int timeStepsHorizScrollBackupValue;

        void timeStepsPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (timeStepsHorizScrollBackupValue != timeStepsPanel.HorizontalScroll.Value)
            {
                this.timeStepsPanel_Scroll(this, new ScrollEventArgs(ScrollEventType.ThumbPosition, timeStepsPanel.HorizontalScroll.Value));
            }
        }

        public void updateOverrideCount()
        {
            int analogsOv=0;
            foreach (LogicalChannel lc in Storage.settingsData.logicalChannelManager.Analogs.Values)
                if (lc.overridden)
                    analogsOv++;

            int digitalsOv = 0;
            foreach (LogicalChannel lc in Storage.settingsData.logicalChannelManager.Digitals.Values)
                if (lc.overridden)
                    digitalsOv++;

            if (analogsOv == 0)
            {
                analogOverridesCountLabel.Text = "";
            }
            else {
                analogOverridesCountLabel.Text = analogsOv.ToString() + " analog(s) overridden.";
            }

            if (digitalsOv==0) {
                digitalOverridesCountLabel.Text = "";
            }
            else {
                digitalOverridesCountLabel.Text = digitalsOv.ToString() + " digital(s) overridden.";
            }

        }

        public void updateAllPulseIndicators()
        {
            foreach (Control con in timeStepsFlowPanel.Controls)
            {
                TimestepEditor ed = con as TimestepEditor;
                if (ed != null)
                    ed.updatePulsesIndicator();
            }
        }

        public void layoutAll()
        {

            layoutTimestepEditors();
            layoutSettingsData();
            layoutTheRest();
        }

        public void layoutSettingsData()
        {
            this.digitalChannelLabelsPanel1.layout();
            this.analogChannelLabelsPanel1.layout();
        }

        private bool modeBoxBeingChanged = false;

        public void layoutTheRest()
        {
            this.seqDescBox.Text = Storage.sequenceData.SequenceDescription;
            this.seqNameBox.Text = Storage.sequenceData.SequenceName;

            this.checkBox1.Checked = Storage.sequenceData.stepHidingEnabled;

            if (Storage.sequenceData.TimeSteps.Count == 0)
            {
                this.analogPreviewUpdate.Visible = false;
                this.checkBox1.Visible = false;
                this.analogPreviewAutoUpdate.Visible = false;
            }
            else
            {
                this.checkBox1.Visible = true;
                this.analogPreviewAutoUpdate.Visible = true;
                this.analogPreviewUpdate.Visible = true;
            }

            if (WordGenerator.mainClientForm.instance != null)
                WordGenerator.mainClientForm.instance.cursorWait();

            digitalGrid1.updateSize();
            if (WordGenerator.mainClientForm.instance != null)
                WordGenerator.mainClientForm.instance.cursorWaitRelease();

            repairAllMargins(this, null);

            this.runControl1.layout();

            this.analogPreviewPane1.redrawBuffer();
            this.analogPreviewPane1.Invalidate();

            modeBoxBeingChanged = true;

            modeBox.Items.Clear();
            modeTextBox.Text = "";
            if (Storage.sequenceData.SequenceModes.Count != 0)
            {
                modeBox.Enabled = true;
                modeTextBox.Enabled = true;
                foreach (SequenceMode mode in Storage.sequenceData.SequenceModes)
                {
                    modeBox.Items.Add(mode);
                }
                
                modeBox.SelectedItem = Storage.sequenceData.CurrentMode;
                if (Storage.sequenceData.CurrentMode != null)
                {
                    modeTextBox.Text = Storage.sequenceData.CurrentMode.ModeName;
                }
            }
            else
            {
                modeBox.Enabled = false;
                modeTextBox.Enabled = false;
            }

            modeBoxBeingChanged = false;

            //WordGenerator.mainClientForm.instance.analogGroupEditor1.updateRunOrderPanel();


        }


        public void updateTimestepEditorsAfterSequenceModeOrTimestepGroupChange()
        {
            bool showHideOfTimeStepChanged = false;

            foreach (Control con in timeStepsFlowPanel.Controls)
            {
                TimestepEditor te = con as TimestepEditor;

                if (te != null)
                {
                    te.refreshButtonsAndGroupIndicator();

                    if (hideHiddenTimesteps)
                    {
                        if (te.Visible == te.StepData.StepHidden)
                        {
                            te.Visible = !te.StepData.StepHidden;
                            showHideOfTimeStepChanged = true;
                        }
                    }
                    else
                    {
                        if (te.Visible != true)
                            showHideOfTimeStepChanged = true;
                        te.Visible = true;
                    }
                }

            }
            if (showHideOfTimeStepChanged)
                layoutTheRest();
        }

        private List<TimestepEditor> timestepEditors;

        public List<TimestepEditor> TimestepEditors
        {
            get { return timestepEditors; }
            set { timestepEditors = value; }
        }


        /// <summary>
        /// This function is somewhat time consuming, especially with long sequence files.
        /// A 100-timestep long sequence will take ~8 seconds to run this function.
        /// Avoid calling it unnecessarily.
        /// </summary>
        private void layoutTimestepEditors() 
        {
            this.SuspendLayout();

            timeStepsFlowPanel.Visible = false;

            timeStepsFlowPanel.SuspendLayout();
            // remove old timestep editors


            List<Control> temp = new List<Control>();
            foreach (Control con in timeStepsFlowPanel.Controls)
            {
                temp.Add(con);
            }
            foreach (Control con in temp) {
                TimestepEditor ed = con as TimestepEditor;
                if (ed != null)
                {
                    ed.unRegsiterHotkey();
                    ed.Dispose();
                }
            }

            timeStepsFlowPanel.Controls.Clear();



            int count = 0;

           

            List<TimeStep> timeSteps = Storage.sequenceData.TimeSteps;
            timestepEditors = new List<TimestepEditor>();
            for (int i = 0; i < timeSteps.Count; i++)
            {

                if (timeSteps[i] != null)
                {

                    // show the timestep
                    TimestepEditor editor = new TimestepEditor(timeSteps[i], i + 1);
                    //      editor.Location = new Point(timestepEditorPlaceholder.Location.X + count * timestepEditorPlaceholder.Width,
                    //          timestepEditorPlaceholder.Location.Y);
                    registerTimestepEditorEvents(editor);
                    //this.timeStepsPanel.BufferedControls.Add(editor);

                    editor.registerHotkey();
                    count++;

                    if (hideHiddenTimesteps)
                    {
                        if (timeSteps[i].StepHidden)
                            editor.Visible = false;
                    }

                    timestepEditors.Add(editor);

                    editor.SuspendLayout();

                }

            }

            this.timeStepsFlowPanel.Controls.AddRange(timestepEditors.ToArray());

            this.ResumeLayout();

            foreach (Control con in this.timeStepsFlowPanel.Controls) 
                con.SuspendLayout();

            this.Invalidate();
            timeStepsFlowPanel.ResumeLayout();
            timeStepsPanel.AutoScroll = false;
            timeStepsFlowPanel.AutoSize = false;
            timeStepsFlowPanel.Visible = true; 
            timeStepsFlowPanel.AutoSize = true;
            timeStepsPanel.AutoScroll = true;

            foreach (Control con in this.timeStepsFlowPanel.Controls)
                con.ResumeLayout();

            if (count == 0)
            {
                beginHintLabel.Visible = true;
            }
            else
            {
                beginHintLabel.Visible = false;
            }

        }

        public void registerTimestepEditorEvents(TimestepEditor editor)
        {
            editor.updateGUI += new EventHandler(timeStepEditor_updateGUI);
            editor.messageLog += messageLog;
        }

        void timeStepEditor_updateGUI(object sender, EventArgs e)
        {
            refreshAnalogPreviewIfAutomatic();
        }

        public void refreshAnalogPreviewIfAutomatic()
        {
            if (analogPreviewAutoUpdate.Checked)
            {
                analogPreviewPane1.redrawBuffer();
                analogPreviewPane1.Invalidate();


                //WordGenerator.mainClientForm.instance.analogGroupEditor1.updateRunOrderPanel();
            }
        }

        public void scrollToTimestep(TimeStep step)
        {
            foreach (Control con in timeStepsFlowPanel.Controls)
            {
                TimestepEditor ed = con as TimestepEditor;
                if (ed != null)
                {
                    if (ed.StepData == step)
                    {
                        timeStepsPanel.ScrollControlIntoView(ed);
                        forceUpdateAllScrollbars();
                        return;
                    }
                }
            }
        }



        private delegate void scrollHandler(ScrollEventArgs e);

        /// <summary>
        /// This function is necessary because Microsoft, in all its wisdom, decided that certain scroll-causing
        /// actions do not raise a scroll event, and thus allows the multiple horiz scrollbars to get out of sync. Calling this 
        /// function scrolls all of the horizontal bars to the value of the timesteps panel bar. It also forces the analog and digital
        /// vertical bars back into sync.
        /// </summary>
        public void forceUpdateAllScrollbars()
        {
           
            if (timeStepsPanel != null)
            {
        
                ScrollProperties prop = timeStepsPanel.HorizontalScroll;
                double frac = ((double)(prop.Value - prop.Minimum)) / ((double)(prop.Maximum - prop.Minimum - prop.LargeChange));
                handleHorizScroll(frac);
            }

            if (digitalChannelLabelsPanel1 != null)
            {
                ScrollProperties prop = digitalChannelLabelsPanel1.VerticalScroll;
                double frac = ((double)(prop.Value - prop.Minimum)) / ((double)(prop.Maximum - prop.Minimum - prop.LargeChange));
                handleDigitalVerticalScroll(frac);
            }

            if (analogChannelLabelsPanel1 != null)
            {
                ScrollProperties prop = analogChannelLabelsPanel1.VerticalScroll;
                double frac = ((double)(prop.Value - prop.Minimum)) / ((double)(prop.Maximum - prop.Minimum - prop.LargeChange));
                handleAnalogVerticalScroll(frac);

            }


        }


        private void timeStepsPanel_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                double frac = getScrollFraction(e, timeStepsPanel.HorizontalScroll);
                handleHorizScroll(frac);
            }
            this.timeStepsHorizScrollBackupValue = timeStepsPanel.HorizontalScroll.Value;
        }

        public double getScrollFraction(ScrollEventArgs e, ScrollProperties prop)
        {
            if (e.Type == ScrollEventType.First)
                return 0;
            if (e.Type == ScrollEventType.Last)
                return 1;
            if (prop.Minimum != prop.Maximum)
            {
                return ((double)(e.NewValue - prop.Minimum)) / ((double)(prop.Maximum - prop.Minimum - prop.LargeChange));
            }
            else return 0;
        }



        public void scrollToFrac(double frac, ScrollProperties prop)
        {
            int temp = (int) (prop.Minimum + frac * (double)(prop.Maximum - prop.Minimum - prop.LargeChange));
            // I have no idea why this shit is necessary, but the scrollbar value property is Lazy!. Sometimes its value 
            // doesn't change even when you set it. Kludge solution, attempt to set it 10 times. If it still doesn't work, give up
            // (we don't want to enter an infinite loop)
            for (int i = 0; i < 10; i++)
            {

                prop.Value = temp;
                if (prop.Value == temp)
                    break;
            }

            if (prop.Value != temp)
            {
                messageLog(this, new MessageEvent("Scrollbar issues!!!"));
            }

   //         System.Threading.Thread.Sleep(100);
   //         if (prop.Value != temp)
   //             throw new Exception("Microsoft sucks.");
        }


        private void digitalGrid1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                double frac = getScrollFraction(e, digitalGrid1.HorizontalScroll);
                handleHorizScroll(frac);
            }
            else
            {
                double frac = getScrollFraction(e, digitalGrid1.VerticalScroll);
                handleDigitalVerticalScroll(frac);
            }
        }

        private void handleHorizScroll(double frac)
        {
            
            if (!horizontalScrollingEventsCalled)
            {
                horizontalScrollingEventsCalled = true;
                try
                {
                    scrollToFrac(frac, digitalGridPanel.HorizontalScroll);
                    scrollToFrac(frac, timeStepsPanel.HorizontalScroll);
                    scrollToFrac(frac, analogPreviewPane1.HorizontalScroll);
                }
                catch (Exception ex)
                {
                    messageLog(this, new MessageEvent("Scrollings issues: " + ex.Message));
                }
                horizontalScrollingEventsCalled = false;
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.hideHiddenTimesteps = checkBox1.Checked;
            Storage.sequenceData.stepHidingEnabled = checkBox1.Checked;

            showOrHideHiddenTimestepEditors();

            this.layoutTheRest();
        }

        /// <summary>
        /// Leo C. Stein personally witnessed the creation of this function, and saw it was good.
        /// April 27, 2008.
        /// </summary>
        public void showOrHideHiddenTimestepEditors()
        {
            WordGenerator.mainClientForm.instance.cursorWait();
            timeStepsFlowPanel.SuspendLayout();
            foreach (Control con in timeStepsFlowPanel.Controls)
            {
                TimestepEditor ed = con as TimestepEditor;
                if (ed != null)
                {
                    if (ed.StepData != null)
                    {
                        if (hideHiddenTimesteps)
                        {
                            ed.Visible = !ed.StepData.StepHidden;
                        }
                        else
                        {
                            ed.Visible = true;
                        }
                    }
                }
            }
            timeStepsFlowPanel.ResumeLayout();
            
            WordGenerator.mainClientForm.instance.cursorWaitRelease();
        }

        private void addNewTimestepToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Storage.sequenceData != null)
            {
                Storage.sequenceData.TimeSteps.Add(new TimeStep("New Timestep"));
                WordGenerator.mainClientForm.instance.RefreshSequenceDataToUI(Storage.sequenceData);
            }
        }

        private void analogPreviewPane1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                double frac = getScrollFraction(e, analogPreviewPane1.HorizontalScroll);
                handleHorizScroll(frac);
            }
            else
            {
                double frac = getScrollFraction(e, analogPreviewPane1.VerticalScroll);
                handleAnalogVerticalScroll(frac);
            }
        }

        private const int scrollbarSize = 16;

        private void repairRightMargin()
        {
            if (digitalGridPanel.VerticalScroll.Visible || analogChannelLabelsPanel1.VerticalScroll.Visible)
            {
                if (timeStepsPanel.Margin.Right != scrollbarSize)
                {
                    timeStepsPanel.Margin = new Padding(0, 0, scrollbarSize, 0);
                    timeStepsPanel.Refresh();
                }

                if (!digitalGridPanel.VerticalScroll.Visible)
                {
                    if (digitalGridPanel.Margin.Right != scrollbarSize)
                    {
                        digitalGridPanel.Margin = new Padding(0, 0, scrollbarSize, 0);
                        digitalGridPanel.Refresh();
                    }
                }
                else
                {
                    if (digitalGridPanel.Margin.Right != 0)
                    {
                        digitalGridPanel.Margin = new Padding(0, 0, 0, 0);
                        digitalGridPanel.Refresh();
                    }
                }

                if (!analogPreviewPane1.VerticalScroll.Visible)
                {
                    if (analogPreviewPane1.Margin.Right != scrollbarSize)
                    {
                        analogPreviewPane1.Margin = new Padding(0, 0, scrollbarSize, 0);
                        analogPreviewPane1.Refresh();
                    }
                }
                else
                {
                    if (analogPreviewPane1.Margin.Right != 0)
                    {
                        analogPreviewPane1.Margin = new Padding(0, 0, 0, 0);
                        analogPreviewPane1.Refresh();
                    }
                }
            }
            else
            {
                if (timeStepsPanel.Margin.Right != 0)
                {
                    timeStepsPanel.Margin = new Padding(0, 0,0, 0);
                    timeStepsPanel.Refresh();
                }

                if (analogPreviewPane1.Margin.Right != 0)
                {
                    analogPreviewPane1.Margin = new Padding(0, 0, 0, 0);
                    analogPreviewPane1.Refresh();
                }

                if (digitalGridPanel.Margin.Right != 0)
                {
                    digitalGridPanel.Margin = new Padding(0, 0, 0, 0);
                    digitalGridPanel.Refresh();
                }


            }
        }

        private void repairDigitalMargin()
        {
            if (digitalGridPanel.HorizontalScroll.Visible)
            {
                if (digitalChannelLabelsPanel1.Margin.Bottom != scrollbarSize)
                {
                    digitalChannelLabelsPanel1.Margin = new Padding(0, 0, 0, scrollbarSize);
                    digitalChannelLabelsPanel1.Refresh();
                }
            }
            else
            {
                if (digitalChannelLabelsPanel1.Margin.Bottom != 0)
                {
                    digitalChannelLabelsPanel1.Margin = new Padding(0, 0, 0, 0);
                    digitalChannelLabelsPanel1.Refresh();
                }
            }
        }

        private void repairAnalogMargin()
        {
            if (analogPreviewPane1.HorizontalScroll.Visible)
            {
                if (analogChannelLabelsPanel1.Margin.Bottom != scrollbarSize)
                {
                    analogChannelLabelsPanel1.Margin = new Padding(0, 0, 0, scrollbarSize);
                    analogChannelLabelsPanel1.Refresh();
                }
            }
            else
            {
                if (analogChannelLabelsPanel1.Margin.Bottom != 0)
                {
                    analogChannelLabelsPanel1.Margin = new Padding(0, 0, 0, 0);
                    analogChannelLabelsPanel1.Refresh();
                }
            }
        }

        private void digitalGridPanel_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                double frac = getScrollFraction(e, digitalGridPanel.HorizontalScroll);
                handleHorizScroll(frac);
            }
            else
            {
                double frac = getScrollFraction(e, digitalGridPanel.VerticalScroll);
                handleDigitalVerticalScroll(frac);
            }
        }

        private void analogPreviewUpdate_Click(object sender, EventArgs e)
        {
            this.analogPreviewPane1.redrawBuffer();
            this.analogPreviewPane1.Invalidate();
            //WordGenerator.mainClientForm.instance.analogGroupEditor1.updateRunOrderPanel();
        }


        private void analogChannelLabelsPanel1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                double frac = getScrollFraction(e, analogChannelLabelsPanel1.VerticalScroll);
                handleAnalogVerticalScroll(frac);
            }
        }

        private void handleAnalogVerticalScroll(double frac)
        {

            if (!verticalScrollingEventsCalled)
            {
                verticalScrollingEventsCalled = true;
                try
                {
                    scrollToFrac(frac, analogChannelLabelsPanel1.VerticalScroll);
                    scrollToFrac(frac, analogPreviewPane1.VerticalScroll);
                }
                catch (Exception)
                {
                }
                verticalScrollingEventsCalled = false;
            }

        }

        private void digitalChannelLabelsPanel1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                double frac = getScrollFraction(e, digitalChannelLabelsPanel1.VerticalScroll);
                handleDigitalVerticalScroll(frac);
            }
        }

        private void handleDigitalVerticalScroll(double frac)
        {

            if (!verticalScrollingEventsCalled)
            {

                verticalScrollingEventsCalled = true;
                try
                {
                    scrollToFrac(frac, digitalGridPanel.VerticalScroll);
                    scrollToFrac(frac, digitalChannelLabelsPanel1.VerticalScroll);
                }
                catch (Exception)
                {
                }
                verticalScrollingEventsCalled = false;
            }

        }

        private void SequencePage_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void seqNameBox_TextChanged(object sender, EventArgs e)
        {
            if (Storage.sequenceData != null)
            {
                Storage.sequenceData.SequenceName = seqNameBox.Text;
                if (WordGenerator.mainClientForm.instance!=null)
                    WordGenerator.mainClientForm.instance.updateFormTitle();
            }
        }

        private void seqDescBox_TextChanged(object sender, EventArgs e)
        {
            if (Storage.sequenceData != null)
            {
                Storage.sequenceData.SequenceDescription = seqDescBox.Text;
            }
        }


        private void analogPreviewPane1_Click(object sender, EventArgs e)
        {
            // mousewheel event handled by timestepspanel.
            timeStepsPanel.Focus();
        }

        private void analogChannelLabelsPanel1_Enter(object sender, EventArgs e)
        {
            // these apparently caused the program to freeze
    //        timeStepsPanel.Focus();
        }

        private void digitalChannelLabelsPanel1_Enter(object sender, EventArgs e)
        {
            // apparently caused program to freeze
    //        timeStepsPanel.Focus();
        }

        private void digitalGridPanel_SizeChanged(object sender, EventArgs e)
        {
            repairDigitalMargin();
        }

        private void repairAllMargins(object sender, EventArgs e)
        {
            repairDigitalMargin();
            repairAnalogMargin();
            repairRightMargin();
        }

        private void SequencePage_SizeChanged(object sender, EventArgs e)
        {
            repairAllMargins(this, null);
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            repairAllMargins(sender, e);
        }

        private void SequencePage_Enter(object sender, EventArgs e)
        {
            repairAllMargins(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            repairAllMargins(sender, e);
        }

        private void createMode_Click(object sender, EventArgs e)
        {
            SequenceMode newMode = SequenceMode.createSequenceMode(Storage.sequenceData);
            Storage.sequenceData.SequenceModes.Add(newMode);
            Storage.sequenceData.CurrentMode = newMode;
            newMode.ModeName = "New Mode";
            layoutTheRest();
        }

        private void storeMode_Click(object sender, EventArgs e)
        {
            if (Storage.sequenceData.CurrentMode != null)
            {
                SequenceMode newMode = SequenceMode.createSequenceMode(Storage.sequenceData);
                Storage.sequenceData.CurrentMode.ModeName = modeTextBox.Text;
                Storage.sequenceData.CurrentMode.TimestepEntries = newMode.TimestepEntries;
                layoutTheRest();
            }
        }

        private void destroyMode_Click(object sender, EventArgs e)
        {
            if (Storage.sequenceData.CurrentMode != null)
            {
                if (Storage.sequenceData.SequenceModes.Contains(Storage.sequenceData.CurrentMode))
                {
                    DialogResult res = MessageBox.Show("Delete mode?", "Are you sure you want to delete this mode?", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        Storage.sequenceData.SequenceModes.Remove(Storage.sequenceData.CurrentMode);
                        Storage.sequenceData.CurrentMode = null;
                        layoutTheRest();
                    }
                }
            }
        }

        private delegate void voidMode (SequenceMode mode);

        /// <summary>
        /// Used for thread safe changing of selected mode. Called from RunForm.
        /// </summary>
        /// <param name="mode"></param>
        public void setMode(SequenceMode mode)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new voidMode(setMode), new object[] { mode });
            }
            else
            {
                modeBox.SelectedItem = mode;
            }
        }

        private void modeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler(modeBox_SelectedIndexChanged), new object[] { sender, e });
            }
            else
            {
                if (!modeBoxBeingChanged)
                {
                    Storage.sequenceData.CurrentMode = modeBox.SelectedItem as SequenceMode;
                    if (Storage.sequenceData.CurrentMode != null)
                    {
                        string message = SequenceMode.applySequenceMode(Storage.sequenceData, Storage.sequenceData.CurrentMode);

                        updateTimestepEditorsAfterSequenceModeOrTimestepGroupChange();

                        //layoutTimestepEditors();
                        layoutTheRest();
                        if (message != null)
                        {
                            MessageBox.Show("Note when applying sequence mode. " + message + " To add these missing entries, click the Store button.");
                        }
                    }
                }
            }
        }











    }
}
