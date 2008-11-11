using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AtticusServer
{
    public partial class DaqmxBrowser : Form
    {
        public DaqmxBrowser()
        {
            InitializeComponent();
            this.propertyGrid1.SelectedObject = NationalInstruments.DAQmx.DaqSystem.Local;
        }
    }
}