using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AtticusServer
{
    public partial class HardwareChannelsBrowser : Form
    {
        public HardwareChannelsBrowser()
        {
            InitializeComponent();
            this.propertyGrid1.SelectedObject = AtticusServer.server;
        }
    }
}