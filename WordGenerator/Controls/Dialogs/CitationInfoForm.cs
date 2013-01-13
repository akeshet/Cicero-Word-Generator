using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WordGenerator.Controls
{
    public partial class CitationInfoForm : Form
    {
        public CitationInfoForm() : this (true)
        {
            
        }

        public CitationInfoForm(bool firstTime)
        {
            InitializeComponent();
            if (!firstTime)
            {
                checkBox1.Visible = false;
                button1.Visible = true;
                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Visible = checkBox1.Checked;
            button1.Enabled = checkBox1.Checked;
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel.Text);
        }


    }
}
