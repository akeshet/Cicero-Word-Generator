using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WordGenerator
{
    public partial class SettingsExplorerForm : Form
    {
        public SettingsExplorerForm()
        {
            InitializeComponent();
            this.propertyGrid1.SelectedObject = Storage.settingsData;
        }

        private void SettingsExplorerForm_Load(object sender, EventArgs e)
        {
//            WordGenerator.mainClientForm.instance.Enabled = false;
        }

        private void SettingsExplorerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Insert a call to refresh mainclientform's sequence data.
  //          WordGenerator.mainClientForm.instance.Enabled = true;
            MainClientForm.instance.RefreshSettingsDataToUI();
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }


    }
}