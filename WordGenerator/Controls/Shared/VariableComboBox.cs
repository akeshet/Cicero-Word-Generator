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
            if (WordGenerator.Storage.sequenceData != null)
            {
                foreach (Variable var in WordGenerator.Storage.sequenceData.Variables)
                {
                    comboBox1.Items.Add(var);
                }
            }

            this.comboBox1.SelectedItem = "Manual";

            /// If using Windows 7, we need to change the combo box
            /// to a style that supports background colors.
            if (WordGenerator.GlobalInfo.usingWindows7)
            {
                comboBox1.FlatStyle = FlatStyle.Popup;
            }
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
            this.Width = 200;
            comboBox1.Width = 200;
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
            this.Width = 200;
            comboBox1.Width = 200;
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

            this.Width = 80;
            this.comboBox1.Width = 80;
        }



        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
          if (this.SelectedVariable != null)
            {

                if (comboBox1.SelectedText != "Manual" && toolTip1.GetToolTip(comboBox1) != this.SelectedVariable.VariableName + " = " + this.SelectedVariable.VariableValue.ToString())
                    toolTip1.SetToolTip(comboBox1, this.SelectedVariable.VariableName + " = " + this.SelectedVariable.VariableValue.ToString());

            }
        }

        private void comboBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Active = true;
            toolTip1_Popup(null, null);
        }

        private void comboBox1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Active = false;
        }

        private void comboBox1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Active = true;
            toolTip1_Popup(null, null);
        }

        private void VariableComboBox_Load(object sender, EventArgs e)
        {
          /*  comboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox1.DrawItem += new DrawItemEventHandler(comboBox1_DrawItem);*/
        }

        void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            /*string text = this.comboBox1.GetItemText(e.Index);
            e.DrawBackground();
            using (SolidBrush br = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(text, e.Font, br, e.Bounds);
            }

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                this.toolTip1.Show(text, comboBox1, e.Bounds.Right, e.Bounds.Bottom);
            }
            else
            {
                this.toolTip1.Hide(comboBox1);
            }

            e.DrawFocusRectangle();*/
        }
       
    }
}
