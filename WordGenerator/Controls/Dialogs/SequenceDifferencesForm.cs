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
    public partial class SequenceDifferencesForm : Form
    {
        public SequenceDifferencesForm()
        {
            InitializeComponent();
        }

        public SequenceDifferencesForm(List<SequenceComparer.SequenceDifference> differences) : this()
        {
            textBox1.Text += "Difference report\r\n--------------------\r\n";
            if (differences.Count != 0)
            {
                foreach (SequenceComparer.SequenceDifference diff in differences)
                {
                    textBox1.Text += "\r\n" + diff.Description;
                }
            }
            else
            {
                textBox1.Text += "No differences to report!";
            }
        }
    }
}