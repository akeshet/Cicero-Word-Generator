using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataStructures;

namespace WordGenerator.Controls
{
    class ToolStripNumericOrVariableEditor : ToolStripControlHost
    {
        HorizontalParameterEditor myHorizontalParameterEditor;

        public HorizontalParameterEditor MyHorizontalParameterEditor
        {
            get { return this.Control as HorizontalParameterEditor; }
        }
        public ToolStripNumericOrVariableEditor(DimensionedParameter param, bool showUnitSelector)
            : base(new HorizontalParameterEditor(param, showUnitSelector))
        {
        }
    }
}