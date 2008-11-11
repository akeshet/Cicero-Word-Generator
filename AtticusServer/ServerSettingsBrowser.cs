using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AtticusServer
{
    public partial class ServerSettingsBrowser : Form
    {
        public ServerSettingsBrowser()
        {
            InitializeComponent();
            propertyGrid1.SelectedObject = AtticusServer.server.serverSettings;
        }
    }
}