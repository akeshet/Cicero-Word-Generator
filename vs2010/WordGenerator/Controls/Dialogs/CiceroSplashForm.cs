using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WordGenerator.Controls
{
    public partial class CiceroSplashForm : Form
    {
        public CiceroSplashForm()
        {
            InitializeComponent();

            label1.Text = DataStructures.Information.VersionString + "          " + DataStructures.Information.BuildDateString + "          \n" + DataStructures.Information.AuthorString + "\nContributors: " + DataStructures.Information.ContribString;

            if (WordGenerator.mainClientForm.instance.studentEdition)
            {
                WordGenerator.mainClientForm.instance.studentEdition = false;
                WordGenerator.mainClientForm.instance.studentEditionDisabled = true;
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        public CiceroSplashForm(bool runTimer)
            : this()
        {
            this.timer1.Enabled = runTimer;
        }


    }
}