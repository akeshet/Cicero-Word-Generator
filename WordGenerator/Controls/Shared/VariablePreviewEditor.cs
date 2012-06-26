using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataStructures;

namespace WordGenerator.Controls
{
    public partial class VariablePreviewEditor : UserControl
    {
        private Variable variable;

        public VariablePreviewEditor()
        {
            InitializeComponent();
        }

        private bool changeValueOnNextRefresh = false;

        public VariablePreviewEditor(Variable variable) : this()
        {
            this.variable = variable;
            this.variableName.Text = variable.VariableName;
            refreshCurrentValue(variable);
            this.nextValue.Value = (decimal) variable.VariableValue;
            if ((variable.passThroughVariable!=null) || variable.IsSpecialVariable || variable.ListDriven || variable.DerivedVariable)
            {
                this.nextValue.Enabled = false;
                this.nextValue.Visible = false;
            }

            this.nextValue.ValueChanged += new EventHandler(nextValue_ValueChanged);

            this.clickToChangeButton.Visible = false;
        }

        void nextValue_ValueChanged(object sender, EventArgs e)
        {
            if (((double)nextValue.Value) != variable.VariableValue)
            {
                this.clickToChangeButton.Visible = true;
                this.changeValueOnNextRefresh = false;
                updateButtonColor();
            }
            else
            {
                clickToChangeButton.Visible = false;
            }
        }

        private void updateButtonColor()
        {
            if (changeValueOnNextRefresh)
                clickToChangeButton.BackColor = Color.Green;
            else
                this.clickToChangeButton.BackColor = Color.Red;
        }

        private void refreshCurrentValue(Variable variable)
        {
            this.currentValue.Text = variable.VariableValue.ToString();
        }

        private void clickToChangeButton_Click(object sender, EventArgs e)
        {
            this.changeValueOnNextRefresh = !this.changeValueOnNextRefresh;
            updateButtonColor();
        }

        /// <summary>
        /// Returns true if value was changed by this refresh.
        /// </summary>
        /// <returns></returns>
        public bool refresh()
        {
            bool ans = false;
            if (changeValueOnNextRefresh)
            {
                if (variable.VariableValue != (double)nextValue.Value)
                {
                    variable.VariableValue = (double)nextValue.Value;
                    ans = true;
                    clickToChangeButton.Visible = false;
                }
            }
            changeValueOnNextRefresh = false;
            currentValue.Text = variable.VariableValue.ToString();
            nextValue.Value = (decimal)variable.VariableValue;
            return ans;
        }
    }
}
