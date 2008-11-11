using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataStructures;

namespace Elgin
{
    public partial class RunLogExplorerForm : Form
    {
        private RunLog runLog;

        public RunLogExplorerForm()
        {
            InitializeComponent();
        }

        public RunLogExplorerForm(RunLog log, string fileName)
            : this()
        {
            this.runLog = log;

            this.fileNameLabel.Text = fileName;

            if (log == null)
                return;

            string effectiveName;
            if (log.RunSequence.SequenceName == "" || log.RunSequence.SequenceName == null)
            {
                effectiveName = "Unnamed Sequence";
            }
            else
            {
                effectiveName = log.RunSequence.SequenceName;
            }

            this.Text = effectiveName + " Iteration #" + log.RunSequence.ListIterationNumber + "/" + log.RunSequence.Lists.iterationsCount() + " Time: " + log.RunTime.ToString() + " - Elgin";
            this.seqNameLabel.Text = log.RunSequence.SequenceName;
            this.seqDesc.Text = log.RunSequence.SequenceDescription;

            this.timeLabel.Text = log.RunTime.ToString();
            this.sequenceGrid.SelectedObject = log.RunSequence;
            this.settingsGrid.SelectedObject = log.RunSettings;
        }


        private void loadLogInCicero()
        {
          //  AppDomain dom = new AppDomain();

            AppDomain newDomain = AppDomain.CreateDomain("Sandbox for Cicero");

 

            CiceroLaunchWrapper wrapper = new CiceroLaunchWrapper();
            wrapper.runLog = this.runLog;
            wrapper.parent = this;
            wrapper.ciceroFinished+=new EventHandler(ciceroFinished);
            newDomain.DoCallBack(new CrossAppDomainDelegate(wrapper.runCicero));


        }

        void ciceroFinished(object sender, EventArgs e)
        {
            CiceroLaunchWrapper wrapper = sender as CiceroLaunchWrapper;
            if (wrapper == null)
                return;

            if (wrapper.parent == this)
            {
                button1.Enabled = true;
                button1.Text = "Launch in Cicero.";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Launched.";
            button1.Enabled = false;
            loadLogInCicero();
        }

        [Serializable]
        private class CiceroLaunchWrapper 
        {
            public RunLog runLog;

            public event EventHandler ciceroFinished;

            public RunLogExplorerForm parent;

            public void runCicero()
            {
                WordGenerator.Program.runCicero(runLog);
                if (ciceroFinished != null)
                    ciceroFinished(this, null);
            }
        }

    }
}