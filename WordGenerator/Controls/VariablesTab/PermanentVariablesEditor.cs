using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WordGenerator.Controls
{
    public partial class PermanentVariablesEditor : Form
    {
        public PermanentVariablesEditor()
        {
            /*
                dataGridView1.Rows.Clear();
            */


            InitializeComponent();

            foreach (string name in Storage.settingsData.PermanentVariables.Keys)
            {
                dataGridView1.Rows.Add(new object[] { name, Storage.settingsData.PermanentVariables[name].ToString() });
            }
        }

        private void insertNewPermanentVariableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(new object[] {"Unnamed", "0" });
        }

        private void PermanentVariablesEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dictionary<string, double> tempDict = new Dictionary<string, double>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    if (!tempDict.ContainsKey(row.Cells[0].Value.ToString()))
                    {
                        double outtry = 0;
                        if (row.Cells[1].Value != null)
                        {
                            bool worked = Double.TryParse(row.Cells[1].Value.ToString(), out outtry);
                            if (!worked)
                                outtry = 0;
                        }
                        tempDict.Add(row.Cells[0].Value.ToString(), outtry);
                    }
                }
            }

            Storage.settingsData.PermanentVariables = tempDict;
        }
    }
}