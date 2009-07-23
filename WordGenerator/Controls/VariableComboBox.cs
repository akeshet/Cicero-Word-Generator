using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DataStructures;

namespace WordGenerator.Controls
{
    public partial class VariableComboBox : UserControl
    {


        public event EventHandler SelectedIndexChanged;
        public event EventHandler RightClick;

        private object backupSelectedItem;

        public VariableComboBox()
        {
            InitializeComponent();
            this.comboBox1.Items.Add("Manual");
            toolTip1.ShowAlways = true;
            if (WordGenerator.Storage.sequenceData != null)
            {
                foreach (Variable var in WordGenerator.Storage.sequenceData.Variables)
                {
                    comboBox1.Items.Add(var);
                }
            }

            this.comboBox1.SelectedItem = "Manual";
        }

       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged!=null)
              SelectedIndexChanged(sender, e);
          backupSelectedItem = this.comboBox1.SelectedItem;
        }


        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Variable SelectedVariable
        {
            get
            {
                return comboBox1.SelectedItem as Variable;
            }
            set
            {

                if (value == null)
                    comboBox1.SelectedItem = "Manual";
                else
                    comboBox1.SelectedItem = value;
            }
        }

        public void DropDown()
        {
            comboBox1.Focus();
            comboBox1.Show();
            comboBox1.DroppedDown = true;
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                RightClick(sender, e);
        }

        private void VariableComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            RightClick(sender, e);
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            updateListItems();
        }

        public void updateListItems()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Manual");
            if (Storage.sequenceData != null)
            {
                foreach (Variable var in WordGenerator.Storage.sequenceData.Variables)
                {
                    comboBox1.Items.Add(var);
                }
            }
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                comboBox1.SelectedItem = this.backupSelectedItem;
        }

        

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            if (comboBox1.SelectedText != "Manual" && toolTip1.GetToolTip(comboBox1) != this.SelectedVariable.VariableName + " = " + this.SelectedVariable.VariableValue.ToString())
                toolTip1.SetToolTip(comboBox1, this.SelectedVariable.VariableName + " = " + this.SelectedVariable.VariableValue.ToString());
        }

        

       
    }
}
