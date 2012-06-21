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
    public partial class AutoNameGlossaryDialog : Form
    {
        public AutoNameGlossaryDialog()
        {
            InitializeComponent();
        }


        private void closeDialog(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
