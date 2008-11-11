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
        public ExceptionViewerDialog()
        {
            InitializeComponent();
        }

        public ExceptionViewerDialog(Exception e)
            : this()
        {
            this.Text = "An Exception has occured.";
            this.textBox1.Text = e.ToString() + "\n" + e.Message + e.StackTrace;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}