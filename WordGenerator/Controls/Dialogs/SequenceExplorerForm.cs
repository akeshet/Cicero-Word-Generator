using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WordGenerator.Controls
{
    public partial class SequenceExplorerForm : Form
    {
        public SequenceExplorerForm()
        {
            InitializeComponent();
            this.propertyGrid1.SelectedObject = Storage.sequenceData;
        }

        private void SequenceExplorerForm_Load(object sender, EventArgs e)
        {
//            WordGenerator.mainClientForm.instance.Enabled = false;
        }

        private void SequenceExplorerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WordGenerator.mainClientForm.instance.RefreshSequenceDataToUI(Storage.sequenceData);
 //           WordGenerator.mainClientForm.instance.Enabled = true;
        }
    }
}