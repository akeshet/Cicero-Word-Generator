using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DataStructures;
using System.Diagnostics;
using System.IO;

namespace WordGenerator.Controls
{
    public partial class RunControl : UserControl
    {
        private bool isRunNoSaveEnabled;

        public bool IsRunNoSaveEnabled
        {
            get { return isRunNoSaveEnabled; }
            set { 
                isRunNoSaveEnabled = value; 
                this.RunNoSave.Enabled = isRunNoSaveEnabled;
                this.RunNoSave.Visible = isRunNoSaveEnabled;
            }

        }

        public RunControl()
        {
            InitializeComponent();
            toolTip1.SetToolTip(runListButton, "Runs through all the list iterations, starting at iteration 0.");
            toolTip1.SetToolTip(continueListButton, "Runs through the remaining list iterations, beginning with the current iteration.");
            toolTip1.SetToolTip(runRandomList, "Runs through all list iterations in random order.");
            isRunNoSaveEnabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Trying to output to console
            System.Console.WriteLine("Blah");
            RunForm runform = new RunForm(Storage.sequenceData, RunForm.RunType.Run_Iteration_Zero, repeatCheckBox.Checked,true);
            runform.ShowDialog();
            runform.Dispose();
        }

        private void runCurrentButton_Click(object sender, EventArgs e)
        {
            RunForm runform = new RunForm(Storage.sequenceData,  RunForm.RunType.Run_Current_Iteration, repeatCheckBox.Checked, true);
            runform.ShowDialog();
            runform.Dispose();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
          /*  if (Storage.sequenceData != null)
                Storage.sequenceData.ListIterationNumber = (int) iterationSelector.Value;

            if (mainClientForm.instance != null)
                mainClientForm.instance.RefreshSequenceDataToUI(Storage.sequenceData);*/
        }

        public void layout()
        {
            iterationSelector.Value = Storage.sequenceData.ListIterationNumber;
        }

        private void runListButton_Click(object sender, EventArgs e)
        {   
            promptUserRegardingRepeats();
            RunForm runform = new RunForm(Storage.sequenceData, RunForm.RunType.Run_Full_List, repeatCheckBox.Checked, true);
            runform.ShowDialog();
            runform.Dispose();
        }

        private void promptUserRegardingRepeats()
        {
            if (this.repeatCheckBox.Checked)
            {
                DialogResult result = MessageBox.Show("Would you like to turn off Run Repeatedly before doing this list run? You should.", "Turn off repeats?", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.repeatCheckBox.Checked = false;
                }
            }
        }

        private void continueListButton_Click(object sender, EventArgs e)
        {
            promptUserRegardingRepeats();
            RunForm runform = new RunForm(Storage.sequenceData, RunForm.RunType.Run_Continue_List, repeatCheckBox.Checked, true);
            runform.ShowDialog();
            runform.Dispose();
        }

        private void setIterButt_Click(object sender, EventArgs e)
        {
            Storage.sequenceData.ListIterationNumber = (int) this.iterationSelector.Value;
            this.Refresh();
            WordGenerator.MainClientForm.instance.sequencePage.refreshAnalogPreviewIfAutomatic();
            WordGenerator.MainClientForm.instance.variablesEditor.ved_valueChanged(this, null);
            WordGenerator.MainClientForm.instance.sequencePage.updateTimestepEditorsAfterSequenceModeOrTimestepGroupChange();
        }

        private void runCurrentButton_Paint(object sender, PaintEventArgs e)
        {
            if (Storage.sequenceData != null)
                runCurrentButton.Text = "Run Current Iteration (" + Storage.sequenceData.ListIterationNumber + ")";
        }

        private void runRandomList_Click(object sender, EventArgs e)
        {   
			promptUserRegardingRepeats();
            RunForm runform = new RunForm(Storage.sequenceData, RunForm.RunType.Run_Random_Order_List, repeatCheckBox.Checked, true);
            runform.ShowDialog();
        }

        private void RunNoSave_Click(object sender, EventArgs e)
        {
            RunForm runform = new RunForm(Storage.sequenceData, RunForm.RunType.Run_Iteration_Zero, repeatCheckBox.Checked, false);
            runform.ShowDialog();
            runform.Dispose();
        }


        private SequenceData queuedNextSequence = null;

        private void bgRunButton_Click(object sender, EventArgs e)
        {
            if (!Storage.sequenceData.Lists.ListLocked)
            {
                MessageBox.Show("The current sequence does not have its lists locked, and thus cannot be run in the background. Please lock the lists (in the Variables tab).", "Lists not locked, unable to run in background.");
                return;
            }
            SequenceData sequenceCopy = (SequenceData)Common.createDeepCopyBySerialization(Storage.sequenceData);
            if (RunForm.backgroundIsRunning())
            {
                RunForm.bringBackgroundRunFormToFront();
                queuedNextSequence = sequenceCopy;
                RunForm.abortAtEndOfNextBackgroundRun();
            }
            else
            {
                RunForm.beginBackgroundRunAsLoop(sequenceCopy, RunForm.RunType.Run_Iteration_Zero, true, new EventHandler(backgroundRunUpdated));
            }
            backgroundRunUpdated(this, null);
        }

        private void backgroundRunUpdated(object o, EventArgs e)
        {
            if (!RunForm.backgroundIsRunning() && queuedNextSequence != null)
            {
                RunForm.beginBackgroundRunAsLoop(queuedNextSequence, RunForm.RunType.Run_Iteration_Zero, true, new EventHandler(backgroundRunUpdated));
                queuedNextSequence = null;
                backgroundRunUpdated(this, null);
                return;
            }

            if (this.InvokeRequired)
                this.BeginInvoke(new EventHandler(backgroundRunUpdated), new object[] {this, null});
            else
            {
                if (RunForm.backgroundIsRunning())
                {
                    bgRunButton.Text = "Queue as Loop in Background";
                }
                else
                {
                    bgRunButton.Text = "Run as Loop in Background (^F9)";
                }
            }
        }


    }
}
