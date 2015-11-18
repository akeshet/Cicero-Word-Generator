using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataStructures;

namespace WordGenerator.Controls
{
    public partial class varSelector : Form
    {
        private Variable boundOutputVar;

        public varSelector(Variable varIn)
        {
            InitializeComponent();
            foreach (Variable var in Storage.sequenceData.Variables)
            {
                comboBox1.Items.Add(var.VariableName);
                boundOutputVar = varIn;
                comboBox1.SelectedIndex = 0;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            Variable tempVar = Storage.sequenceData.Variables.Find(x => x.VariableName.Equals(comboBox1.SelectedItem.ToString()));
            Storage.sequenceData.Variables.Find(x => x.VariableName.Equals(boundOutputVar.VariableName)).LUTInput = tempVar;
            this.Close();
        }
    }
}
