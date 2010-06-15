using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Elgin
{
    public partial class ElginSplash : Form
    {
        public ElginSplash()
        {
            InitializeComponent();
            label1.Text = DataStructures.Information.VersionString + "          " + DataStructures.Information.BuildDateString + "          \n" + DataStructures.Information.AuthorString + "\nContributors: " + DataStructures.Information.ContribString;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public ElginSplash(bool runTimer)
            : this()
        {
            
        }
    }
}