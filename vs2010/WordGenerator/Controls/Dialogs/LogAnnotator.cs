using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataStructures;

namespace WordGenerator.Controls.Dialogs
{
    public partial class LogAnnotator : Form
    {
        public RunLogAnnotation annotation;

        public LogAnnotator()
        {
            InitializeComponent();
            annotation = new RunLogAnnotation();
        }
    }
}