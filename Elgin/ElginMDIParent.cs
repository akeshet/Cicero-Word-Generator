using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Elgin
{
    public partial class ElginMDIParent : Form
    {
        public ElginMDIParent()
        {
            InitializeComponent();
            ElginSplash splash = new ElginSplash();
            splash.Show();
            this.Text = "Elgin Log Explorer " + DataStructures.Information.VersionString;
        }

        private ElginTrackerForm trackerForm;

        private void ElginMDIParent_Load(object sender, EventArgs e)
        {
            trackerForm = new ElginTrackerForm();
            trackerForm.MdiParent = this;
            trackerForm.Show();
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trackerForm.CloseAllBrowsers();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.trackerForm.MinimizeAll();
        }

        private void loadLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trackerForm.openFile(sender, e);
        }

        private void ElginMDIParent_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
           
        }

        private void ElginMDIParent_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                trackerForm.loadFiles(files);
            }
        }

        private void ElginMDIParent_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                // do nothing
            }
            else
            {
                e.Effect = DragDropEffects.All;
            }
        }


        private void splashScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ElginSplash spl = new ElginSplash(false);
            spl.ShowDialog();
        }

        private void licenseInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataStructures.LicenseInfoForm form = new DataStructures.LicenseInfoForm();
            form.ShowDialog();
        }




    }
}