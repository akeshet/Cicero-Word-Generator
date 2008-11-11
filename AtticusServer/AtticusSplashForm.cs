using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AtticusServer
{
    public partial class AtticusSplashForm : Form
    {
        public AtticusSplashForm()
        {
            InitializeComponent();
            label1.Text = Properties.Resources.VersionString + "          " + Properties.Resources.BuildDate + "          \n" + Properties.Resources.AuthorString + "\nContributors: " + Properties.Resources.Contribs;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        public AtticusSplashForm(bool runTimer) : this()
        {
            this.timer1.Enabled = runTimer;
        }
    }
}