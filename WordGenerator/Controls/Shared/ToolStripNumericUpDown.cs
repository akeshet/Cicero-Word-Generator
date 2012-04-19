using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WordGenerator.Controls
{
    public class ToolStripNumericUpDown : ToolStripControlHost
    {

        public NumericUpDown NumericUpDownControl
        {
            get { return this.Control as NumericUpDown; }
        }

        public ToolStripNumericUpDown()
            : base(new NumericUpDown())
        {
        }
    }
}
