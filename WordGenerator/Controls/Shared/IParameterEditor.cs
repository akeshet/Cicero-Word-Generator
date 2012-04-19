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
    public partial class IParameterEditor : UserControl
    {

        public event EventHandler ParameterChanged;

        protected DimensionedParameter dimensionedParameter;
        protected DimensionedParameter backupDimensionedParameter;
        public event EventHandler updateGUI;



        public void undoLastChange(Object sender, EventArgs e)
        {
            dimensionedParameter.parameter = backupDimensionedParameter.parameter;
            dimensionedParameter.units = backupDimensionedParameter.units;
            if (updateGUI != null)
                updateGUI(sender, e);
        }

        /// <summary>
        /// informGUI should be set to true if it is necessary for this change to be propagated back to the GUI.
        /// informGUI should be set to false if the changeParameter is being called BY the gui.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="informGUI"></param>
        protected void changeParameter(double value)
        {
            backupDimensionedParameter.parameter = dimensionedParameter.parameter;
            backupDimensionedParameter.units = dimensionedParameter.units;
            dimensionedParameter.parameter.ManualValue = value;

                if (updateGUI != null)
                    updateGUI(this, null);
        }

        protected void changeUnits(Units units)
        {
            backupDimensionedParameter.parameter = dimensionedParameter.parameter;
            backupDimensionedParameter.units = dimensionedParameter.units;
            dimensionedParameter.units = units;

                if (updateGUI != null)
                    updateGUI(this, null);
        }

        protected void changeVariable(Variable variable)
        {
            backupDimensionedParameter.parameter.variable = dimensionedParameter.parameter.variable;
            dimensionedParameter.parameter.variable = variable;
            if (updateGUI != null)
                updateGUI(this, null);
        }

        protected void setParameter(DimensionedParameter parameter)
        {
            dimensionedParameter = parameter;
            backupDimensionedParameter = new DimensionedParameter(parameter);
            if (ParameterChanged!=null)
                ParameterChanged(this, null);
        }

        public IParameterEditor()
        {
            this.setParameter(new DimensionedParameter(Units.Dimension.unity));
            InitializeComponent();
        }



    }
}
