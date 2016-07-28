using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataStructures;
using System.IO;
using WordGenerator;

namespace WordGenerator.Controls.LookUpTableTab
{
    public partial class LookupTableControl : UserControl
    {
        public LookupTableControl()
        {
            InitializeComponent();
            tableDisplay.Rows.Add(1, 2);

        }

        public void setUp() //Run when the screen is refreshed
        {
            
            //Backwards compat. - initializes this property
            if (Storage.settingsData.LookupTables == null)
            {
                Storage.settingsData.LookupTables = new List<LUT>();
            }
            //Clear the controls
            tableDisplay.Rows.Clear();
            LUTSelector.Items.Clear();
            //Populate the dropdown list
            foreach (LUT table in Storage.settingsData.LookupTables)
            {
                LUTSelector.Items.Add(table.Name);
            }
            //If there is at least one LUT
            if (LUTSelector.Items.Count > 0)
            {
                tableDisplay.Enabled = true;
                foreach (double key in Storage.settingsData.LookupTables[0].Table.Keys)
                tableDisplay.Rows.Add(key, Storage.settingsData.LookupTables[0].Table[key]);
                LUTSelector.SelectedIndex = 0;
            }
            //Otherwise disable the data grid view
            else
            {
                tableDisplay.Enabled = false;
             
            }
        }

        private void newLUTButton_Click(object sender, EventArgs e) //When we make a new LUT
        {
            LUT newLUT = new LUT("New Table");
            Storage.settingsData.LookupTables.Add(newLUT);
 
            tableDisplay.Rows.Clear();
            LUTSelector.Items.Clear();
            tableDisplay.Enabled = true;
            foreach (LUT table in Storage.settingsData.LookupTables)
            {
                LUTSelector.Items.Add(table.Name);
            }
            LUTSelector.SelectedIndex = Storage.settingsData.LookupTables.Count() - 1;
            textBox1.Text = "New Table";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableDisplay_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Do it all async
            this.BeginInvoke(new MethodInvoker(() =>
            {
              
           
                //When an edit is made, we save the table to the dictionary
                int editedTableIndex = LUTSelector.SelectedIndex;
                Storage.settingsData.LookupTables[editedTableIndex].Table.Clear();
                foreach (DataGridViewRow row in tableDisplay.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        try
                        {
                            row.Cells[0].ValueType = typeof(double);
                            Storage.settingsData.LookupTables[editedTableIndex].Table.Add(Convert.ToDouble(row.Cells[0].Value), Convert.ToDouble(row.Cells[1].Value));
                        }
                        catch
                        {
                            MessageBox.Show("Generic error. Only ints and floating points allowed. No repeated X values.");
                        }
                    }
                }
                //To auto-sort, now repopulate the table from the dictionary
                tableDisplay.Rows.Clear();
                foreach (double key in Storage.settingsData.LookupTables[LUTSelector.SelectedIndex].Table.Keys)
                    tableDisplay.Rows.Add(key, Storage.settingsData.LookupTables[LUTSelector.SelectedIndex].Table[key]);

                tableDisplay.ClearSelection();
            }));
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tableDisplay_EnabledChanged(object sender, EventArgs e) //Code for graying out the data grid view
        {
            if (tableDisplay.Enabled == false)
            {
                tableDisplay.DefaultCellStyle.BackColor = SystemColors.Control;
                tableDisplay.DefaultCellStyle.ForeColor = SystemColors.GrayText;
                tableDisplay.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                tableDisplay.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.GrayText;
                tableDisplay.EnableHeadersVisualStyles = false;
                deleteButton.Enabled = false;
                loadLUT.Enabled = false;
            }
            else
            {
                tableDisplay.DefaultCellStyle.BackColor = SystemColors.Window;
                tableDisplay.DefaultCellStyle.ForeColor = SystemColors.ControlText;
                tableDisplay.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Window;
                tableDisplay.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                tableDisplay.EnableHeadersVisualStyles = true;
                deleteButton.Enabled = true;
                loadLUT.Enabled = true;

            }
        }

        private void LUTSelector_SelectedIndexChanged(object sender, EventArgs e) 
        {

            tableDisplay.Rows.Clear();
           foreach (double key in Storage.settingsData.LookupTables[LUTSelector.SelectedIndex].Table.Keys)
                tableDisplay.Rows.Add(key, Storage.settingsData.LookupTables[LUTSelector.SelectedIndex].Table[key]);
           textBox1.Text = Storage.settingsData.LookupTables[LUTSelector.SelectedIndex].Name;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int selected = LUTSelector.SelectedIndex;
            //Rename the LUT in Storage first
            Storage.settingsData.LookupTables[selected].Name = textBox1.Text.ToString();
            //Then repopulate the dropdown
            LUTSelector.Items.Clear();
            foreach (LUT table in Storage.settingsData.LookupTables)
            {
                LUTSelector.Items.Add(table.Name);
            }
            LUTSelector.SelectedIndex = selected;
        }



        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this lookup table?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Storage.settingsData.LookupTables.RemoveAt(LUTSelector.SelectedIndex);
                
            }
            
        }

        private void loadLUT_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) //Pick file, get the data
            {
                var reader = new StreamReader(openFileDialog1.FileName);
                List<double> listA = new List<double>();
                List<double> listB = new List<double>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    listA.Add(Convert.ToDouble(values[0]));
                    listB.Add(Convert.ToDouble(values[1]));
                }
                //Now, write it to the grid view and to the dictionary
                try
                {
                    tableDisplay.Rows.Clear();
                    int k;
                    for (k = 0; k < listA.Count(); k++)
                    {
                        tableDisplay.Rows.Add(listA[k], listB[k]);
                    }
                    int editedTableIndex = LUTSelector.SelectedIndex; //Copy and pasted from the event handler for a cell's value changing
                    Storage.settingsData.LookupTables[editedTableIndex].Table.Clear();
                    foreach (DataGridViewRow row in tableDisplay.Rows)
                    {
                        if (row.Cells[0].Value != null)
                        {

                            try
                            {
                                Storage.settingsData.LookupTables[editedTableIndex].Table.Add(Convert.ToDouble(row.Cells[0].Value), Convert.ToDouble(row.Cells[1].Value));
                            }
                            catch
                            {
                                MessageBox.Show("Generic error. Only ints and floating points allowed. No repeated X values.");
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Error. Something's probably wrong with your csv. Make sure it's like x1,y1(new line)x2,y2(new line)x3,y3 and so on");
                }
               
            }

           


           
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void newLUTButton_Click_1(object sender, EventArgs e)
        {

        }

        private void tableDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void varUpdate_Click(object sender, EventArgs e)
        {
            WordGenerator.MainClientForm.instance.variablesEditor.discardAndRefreshAllVariableEditors();
        }
    }
}
