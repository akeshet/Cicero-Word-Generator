using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WordGenerator.Controls
{
    public partial class RunControl : UserControl
    {

        public RunControl()
        {
            InitializeComponent();
            toolTip1.SetToolTip(runListButton, "Runs through all the list iterations, starting at iteration 0.");
            toolTip1.SetToolTip(continueListButton, "Runs through the remaining list iterations, beginning with the current iteration.");
            toolTip1.SetToolTip(runRandomList, "Runs through all list iterations in random order.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RunForm runform = new RunForm(RunForm.RunType.Run_Iteration_Zero, repeatCheckBox.Checked);
            runform.ShowDialog();
            runform.Dispose();
        }

        private void runCurrentButton_Click(object sender, EventArgs e)
        {
            RunForm runform = new RunForm(RunForm.RunType.Run_Current_Iteration, repeatCheckBox.Checked);
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
            RunForm runform = new RunForm(RunForm.RunType.Run_Full_List, repeatCheckBox.Checked);
            runform.ShowDialog();
            runform.Dispose();
        }

        private void continueListButton_Click(object sender, EventArgs e)
        {
            RunForm runform = new RunForm(RunForm.RunType.Run_Continue_List, repeatCheckBox.Checked);
            runform.ShowDialog();
            runform.Dispose();
        }

        private void setIterButt_Click(object sender, EventArgs e)
        {
            Storage.sequenceData.ListIterationNumber = (int) this.iterationSelector.Value;
            this.Refresh();
            WordGenerator.mainClientForm.instance.sequencePage1.refreshAnalogPreviewIfAutomatic();
            WordGenerator.mainClientForm.instance.variablesEditor1.ved_valueChanged(this, null);
        }

        private void runCurrentButton_Paint(object sender, PaintEventArgs e)
        {
            if (Storage.sequenceData != null)
                runCurrentButton.Text = "Run Current Iteration (" + Storage.sequenceData.ListIterationNumber + ")";
        }

        private void runRandomList_Click(object sender, EventArgs e)
        {
            RunForm runform = new RunForm(RunForm.RunType.Run_Random_Order_List, repeatCheckBox.Checked);
            runform.ShowDialog();
        }


    }
}
