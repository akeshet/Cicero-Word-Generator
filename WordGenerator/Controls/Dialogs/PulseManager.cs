using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WordGenerator.Controls.Dialogs
{
    public partial class PulseManager : Form
    {
        public PulseManager()
        {
            InitializeComponent();
        }
        
        private void closeWindow(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
