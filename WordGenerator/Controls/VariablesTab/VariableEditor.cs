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
    public partial class VariableEditor : UserControl
    {
        private Variable variable;

        private bool listLocked;

        public event EventHandler variableDeleted;

        public event EventHandler valueChanged;

        private decimal backupValue=0;

        public bool ListLocked
        {
            get { 
                return listLocked; 
            }
            set { 
                listLocked = value;

                if (ListLocked)
                {
                    this.listSelector.Enabled = false;
                }
                else
                {
                    this.listSelector.Enabled = true;
                }

                this.Invalidate();
            }
        }

        public VariableEditor()
        {
            InitializeComponent();
            this.listSelector.Items.Clear();
            this.listSelector.Items.Add("Manual");
            for (int i = 0; i < ListData.NLists; i++)
            {
                this.listSelector.Items.Add("List " + (i + 1));
            }

            this.toolTip1.SetToolTip(this.deleteButton, "Delete this variable.");

            updateLayout();

        }

        public void setVariable(Variable var)
        {
            if (this.variable == var)
                return; // if the variable is already set appropriately,
                        // do nothing and return immediately

            this.variable = var;
            if (this.variable.ListDriven) {
                this.listSelector.Visible = true;
                this.listSelector.SelectedIndex = this.variable.ListNumber;
            }
            else
                this.listSelector.Visible = false;

            if (!variable.DerivedVariable && !variable.PermanentVariable)
            {
                this.valueSelector.Value = (decimal)this.variable.VariableValue;
            }

            this.textBox1.Text = variable.VariableName;

            this.derivedCheckBox.Checked = var.DerivedVariable;
            this.formulaTextBox.Text = var.VariableFormula;

            updateLayout();
             
        }

        private void VariableEditor_Paint(object sender, PaintEventArgs e)
        {
            if (variable != null)
            {
                this.textBox1.Text = variable.VariableName;
                if (!variable.DerivedVariable && !variable.PermanentVariable)
                {
                    this.valueSelector.Value = (decimal)variable.VariableValue;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool oldPermanent = this.variable.PermanentVariable;

            if (variable != null)
            {
                this.variable.VariableName = textBox1.Text;
                if (this.variable.VariableName == "SeqMode")
                {
                    this.BackColor = Color.Salmon;
                    this.variable.PermanentVariable = false;
                }
                else
                {
                    detectIfThisIsPermanentVariable();
                }
            }

            if (oldPermanent != this.variable.PermanentVariable)
                updateLayout();
        }

        private void detectIfThisIsPermanentVariable()
        {
            if (Storage.settingsData.PermanentVariables.ContainsKey(this.variable.VariableName))
            {
                this.BackColor = Color.BlueViolet;
                this.variable.PermanentVariable = true;
                this.variable.PermanentValue = Storage.settingsData.PermanentVariables[this.variable.VariableName];
            }
            else
            {
                this.BackColor = Color.Transparent;
                this.variable.PermanentVariable = false;
            }
        }


        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listSelector.SelectedIndex == 0)
            {
                variable.ListDriven = false;
                listSelector.Visible = false;
                this.valueSelector.Value = backupValue;
            }
            else
            {
                this.backupValue = valueSelector.Value;
                int listNum = listSelector.SelectedIndex;
                this.variable.ListDriven = true;
                this.variable.ListNumber = listNum;
            }

            if (valueChanged != null)
                valueChanged(this, null);
        }

        private void numericUpDown1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (!listLocked)
                {
                    this.listSelector.Visible = true;
                    this.listSelector.Enabled = true;
                    this.listSelector.Focus();
                    this.listSelector.DroppedDown = true;
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (variable != null)
            {
                if (variable.ListDriven==false)
                    this.variable.VariableValue = (double) valueSelector.Value;
            }
            if (valueChanged != null)
            {
                valueChanged(this, null);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void deleteVariableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            attemptDeleteVariable();
            
        }

        private void attemptDeleteVariable()
        {
            Dictionary<Variable, string> usedVariables = Storage.sequenceData.usedVariables();
            if (usedVariables.ContainsKey(this.variable))
            {
                MessageBox.Show("Cannot delete this variable, it is in use. " + usedVariables[this.variable]);
            }
            else
            {
                Storage.sequenceData.Variables.Remove(this.variable);
                if (variableDeleted != null)
                    variableDeleted(this, null);
            }
        }

        private void deleteVariableButtonClick(object sender, EventArgs e)
        {
            attemptDeleteVariable();
        }

        private void derivedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.variable.DerivedVariable = derivedCheckBox.Checked;
            updateLayout();
            if (valueChanged != null)
            {
                valueChanged(this, null);
            }
        }

        private List<HorizontalParameterEditor> combinedValues;
        private List<ComboBox> combineOperators;
        private Dictionary<ComboBox, int> combineOperatorsIndexMapping;
        public void updateLayout()
        {
            if (variable == null)
                return;

            if (combinedValues == null)
            {
                combinedValues = new List<HorizontalParameterEditor>();
            }

            if (combineOperators == null)
            {
                combineOperators = new List<ComboBox>();
            }

            if (combineOperatorsIndexMapping == null)
            {
                combineOperatorsIndexMapping = new Dictionary<ComboBox, int>();
            }

            // remove all derived variable related elements, if any
            foreach (HorizontalParameterEditor hpe in combinedValues)
            {
                this.Controls.Remove(hpe);
            }
            foreach (ComboBox cb in combineOperators)
            {
                this.Controls.Remove(cb);
            }

            combinedValues.Clear();
            combineOperators.Clear();
            combineOperatorsIndexMapping.Clear();

            detectIfThisIsPermanentVariable();
            if (variable.PermanentVariable)
            {
                permanentValueLabel.Visible = true;
                permanentValueLabel.Text = Storage.settingsData.PermanentVariables[this.variable.VariableName].ToString();
                valueSelector.Visible = false;
                derivedValueLabel.Visible = false;
                formulaTextBox.Visible = false;
                derivedCheckBox.Visible = false;
                this.Size = new Size(220, 22);
            }
            else
            {
                permanentValueLabel.Visible = false;

                if (!variable.DerivedVariable)
                {

                    this.valueSelector.Visible = true;
                    this.formulaTextBox.Visible = false;


                    derivedValueLabel.Visible = false;



                    this.BorderStyle = BorderStyle.None;

                    this.Size = new Size(220, 22);
                }
                else
                {
                    this.valueSelector.Visible = false;
                    listSelector.SelectedIndex = 0;
                    listSelector.Visible = false;
                    this.formulaTextBox.Visible = true;
                    derivedValueLabel.Visible = true;

                    this.Size = new Size(220, 104);
                    this.BorderStyle = BorderStyle.FixedSingle;

                    updateDerivedValue();


                }
            }
        }

        void hpe_updateGUI(object sender, EventArgs e)
        {
            updateDerivedValue();
            if (valueChanged!=null) {
                valueChanged(this, null);
            }
        }

      /*  void box_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = sender as ComboBox;
            if (box==null) 
                return;
            if (!combineOperatorsIndexMapping.ContainsKey(box))
            {
                return;
            }

            variable.Combiners[combineOperatorsIndexMapping[box]] = (string) box.SelectedItem;
            updateDerivedValue();

            if (valueChanged != null)
            {
                valueChanged(this, null);
            }
        }*/

        public void updateDerivedValue()
        {

            if (variable != null && variable.DerivedVariable)
            {
                if (variable.DerivedVariable)
                {
                    string sanity = variable.parseVariableFormula(Storage.sequenceData.Variables);
                    if (sanity != null)
                    {
                        derivedValueLabel.BackColor = Color.Red;
                        derivedValueLabel.Text = sanity;
                    }
                    else
                    {
                        derivedValueLabel.BackColor = Color.Transparent;
                        derivedValueLabel.Text = "Value: " + variable.VariableValue;
                    }
                }
            }
        }

        private void formulaTextBox_TextChanged(object sender, EventArgs e)
        {
            this.variable.VariableFormula = formulaTextBox.Text;
            updateDerivedValue();
        }

        private void helpOnSupportedOperationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormulaHelpDialog dial = new FormulaHelpDialog();
            dial.ShowDialog();
        }

    /*    private void downButton_Click(object sender, EventArgs e)
        {
            if (variable != null)
            {
                if (variable.DerivedVariable)
                {
                    variable.Combiners.Add(Variable.plus);
                    variable.CombinedValues.Add(new DimensionedParameter(Units.Dimension.unity));
                    updateLayout();
                }
            }
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            if (variable != null)
            {
                if (variable.DerivedVariable)
                {
                    if (variable.CombinedValues.Count > 1)
                    {
                        variable.CombinedValues.RemoveAt(variable.CombinedValues.Count - 1);
                        variable.Combiners.RemoveAt(variable.Combiners.Count - 1);
                        updateLayout();
                    }
                }
            }
        }*/
    }
}
