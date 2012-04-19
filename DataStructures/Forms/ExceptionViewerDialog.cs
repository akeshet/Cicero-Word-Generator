using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataStructures
{
    public partial class ExceptionViewerDialog : Form
    {

        private Exception exp;

        public ExceptionViewerDialog()
        {
            InitializeComponent();
        }

        public ExceptionViewerDialog(Exception e)
            : this()
        {
            exp = e;

            this.Text = "An Exception has occured.";
            this.textBox1.Text = e.ToString() + "\n" + e.Message + e.StackTrace + e.HelpLink;

            if (e.InnerException != null)
                showInner.Enabled = true;
            else
                showInner.Enabled = false;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showInner_Click(object sender, EventArgs e)
        {
            ExceptionViewerDialog diag = new ExceptionViewerDialog(exp.InnerException);
            diag.ShowDialog();
        }
    }
}