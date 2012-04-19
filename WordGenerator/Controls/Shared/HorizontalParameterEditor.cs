using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataStructures;

namespace WordGenerator.Controls
{
    public partial class HorizontalParameterEditor : WordGenerator.Controls.IParameterEditor
    {

        public HorizontalParameterEditor(DimensionedParameter parameter, bool showUnitSelector) : this(parameter)
        {
            UnitSelectorVisibility = showUnitSelector;
        }

        public HorizontalParameterEditor()
        {
            InitializeComponent();

            this.unitSelector.DropDownClosed += new EventHandler(unitSelector_DropDownClosed);

            // gets rid of a pesky context menu that interferes with right-clicking
            this.variableComboBox1.ContextMenuStrip = null;
            this.variableComboBox1.ContextMenu = null;

            for (int i = 0; i < dimensionedParameter.units.dimension.commonlyUsedMultipliers.GetLength(0); i++)
            {
                unitSelector.Items.Add(dimensionedParameter.units.dimension.commonlyUsedMultipliers[i].ToString());
            }

        }

        void unitSelector_DropDownClosed(object sender, EventArgs e)
        {
            if (unitSelector.SelectedItem == null)
                if (this.dimensionedParameter != null)
                    unitSelector.SelectedItem = this.dimensionedParameter.units;
        }

        private bool unitSelectorVisibility;

        public bool UnitSelectorVisibility
        {
            get { return unitSelectorVisibility; }
            set { 
                unitSelectorVisibility = value;
                setUnitSelectorVisibility(value);
            }
        }

        public void setUnitSelectorVisibility(bool visible)
        {
            this.unitSelector.Visible = visible;
        }

        public void setUnitSelectorWidth(int width)
        {
            this.unitSelector.Width = width;
        }
         
        public HorizontalParameterEditor(DimensionedParameter parameter)
            : this()
        {
            this.setParameter(parameter);
        }

        public void setMinimumAllowableManualValue(decimal value)
        {
            this.valueSelector.Minimum = value;
        }

        public void setParameterData(DimensionedParameter parameter)
        {
            this.setParameter(parameter);
        }

        private void valueSelector_ValueChanged(object sender, EventArgs e)
        {


                this.changeParameter((double)valueSelector.Value);


        }

        private void valueSelector_Paint(object sender, PaintEventArgs e)
        {
            valueSelector.Value = (decimal) dimensionedParameter.parameter.ManualValue;
        }

        private void unitSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.changeUnits(new Units(unitSelector.Text));
        }

        private void valueSelector_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                showVariableSelector();
            }
            /*
            if (e.Button == MouseButtons.Left)
            {
                valueSelector.fo
            }*/
        }

        /// <summary>
        /// This protected method converts the horizontally laid out parameter editor to a vertical one. 
        /// This is called from the VerticalParameterEditor constructore. Kinda dirty OO wise, but it works better
        /// than re-writing all of this class again.
        /// </summary>
        protected void convertToVertical()
        {
            this.unitSelector.Location = new Point(this.valueSelector.Location.X + this.valueSelector.Width - this.unitSelector.Width
                , this.valueSelector.Location.Y + this.valueSelector.Height + 5);
            this.Size = new Size(this.valueSelector.Location.X + this.valueSelector.Width + 3, this.unitSelector.Location.Y + this.unitSelector.Height + 3);
            this.Invalidate();
        }

        private void showVariableSelector()
        {
            this.variableComboBox1.Visible = true;
            this.variableComboBox1.Enabled = true;
            this.variableComboBox1.DropDown();
        }

        private void variableComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.changeVariable(this.variableComboBox1.SelectedVariable);
            if (this.dimensionedParameter.parameter.variable==null)
            {
                this.variableComboBox1.Visible = false;
                this.variableComboBox1.Enabled = false;
                this.variableComboBox1.Focus();
                this.Invalidate();
            }
        }

        private void variableComboBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void variableComboBox1_RightClick(object sender, EventArgs e)
        {
            this.showVariableSelector();
        }

        private void valueSelector_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                this.showVariableSelector();
        }

        private void HorizontalParameterEditor_ParameterChanged(object sender, EventArgs e)
        {
            unitSelector.Items.Clear();
            // Make the units list match the correct dimension type.
            for (int i = 0; i < dimensionedParameter.units.dimension.commonlyUsedMultipliers.GetLength(0); i++)
            {
                Units unit = new Units();
                unit.dimension = dimensionedParameter.units.dimension;
                unit.multiplier = dimensionedParameter.units.dimension.commonlyUsedMultipliers[i];
                unitSelector.Items.Add(unit);
                

            }

            // insurance policy, in case we somehow get access to really really small numbers.
            if (!unitSelector.Items.Contains(dimensionedParameter.units))
            {
                unitSelector.Items.Add(dimensionedParameter.units);
            }

            unitSelector.SelectedItem = dimensionedParameter.units;

            variableComboBox1.updateListItems();

            if (dimensionedParameter.parameter.variable != null)
            {
                variableComboBox1.Enabled = true;
                variableComboBox1.Visible = true;
                variableComboBox1.SelectedVariable = dimensionedParameter.parameter.variable;
            }
            else
            {
                variableComboBox1.Enabled = false;
                variableComboBox1.Visible = false;
                variableComboBox1.SelectedVariable = null;
            }

        }

        private void HorizontalParameterEditor_updateGUI(object sender, EventArgs e)
        {
            // force change of the UI elements:
            if (dimensionedParameter != null)
            {
                this.valueSelector.Value = (decimal) dimensionedParameter.parameter.ManualValue;
                if (!unitSelector.Items.Contains(dimensionedParameter.units))
                {
                    unitSelector.Items.Add(dimensionedParameter.units);
                }
                this.unitSelector.SelectedItem = dimensionedParameter.units;
                this.variableComboBox1.SelectedVariable = dimensionedParameter.parameter.variable;
            }
        }

        private void valueSelector_Enter(object sender, EventArgs e)
        {
            valueSelector.Select(0, 100);
        }
    }
}

