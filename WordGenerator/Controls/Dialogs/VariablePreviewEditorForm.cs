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
    public partial class VariablePreviewEditorForm : Form
    {
        private List<VariablePreviewEditor> eds;

        public VariablePreviewEditorForm()
        {
            InitializeComponent();
        }

        public VariablePreviewEditorForm(List<Variable> variables) : this()
        {
            eds = new List<VariablePreviewEditor>();

            foreach (Variable var in variables)
            {
                VariablePreviewEditor ed = new VariablePreviewEditor(var);
                eds.Add(ed);
                this.variablePreviewEditorPanel.Controls.Add(ed);
            }
        }

        private delegate int intSeqDelegate(SequenceData s);

        public int refresh(SequenceData sequence)
        {
            if (this.InvokeRequired)
            {
                intSeqDelegate myDelegate = new intSeqDelegate(this.refresh);
                IAsyncResult result = this.BeginInvoke(myDelegate, new object [] {sequence});
                return (int) this.EndInvoke(result);
            }
            else
            {
                int ans = 0;
                foreach (VariablePreviewEditor ed in eds)
                {
                    if (ed.refresh())
                        ans++;
                }

                // Force the sequence to re-calculate all equation based derived variables, with this
                // line of code that ends up calling setIterationNumber in sequence data
                sequence.ListIterationNumber = sequence.ListIterationNumber;


                foreach (VariablePreviewEditor ed in eds)
                {
                    ed.refresh();
                }

                return ans;
            }
        }
    }
}
