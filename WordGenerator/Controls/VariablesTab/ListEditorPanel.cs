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
    public partial class ListEditorPanel : UserControl
    {


        private int listID;

        public ListEditorPanel()
        {
            InitializeComponent();
            this.textBox1.Text = "";
        }

        public void setListID(int listID)
        {

            this.listID = listID;

            UI_Updating = true;
            this.textBox1.Text = "";
            UI_Updating = false;

            if (listID > ListData.NLists)
                return;
            this.listName.Text = "List " + listID;

            if (listID == ListData.NLists)
                timesButton.Visible = false;

            if (listID<ListData.NLists)
                setTimesButtonText();

            if (Storage.sequenceData == null)
            {
                this.Enabled = false;
                return;
            }

            if (Storage.sequenceData.Lists == null)
            {
                this.Enabled = false;
                return;
            }

            if (Storage.sequenceData.Lists.Lists == null)
            {
                this.Enabled = false;
                return;
            }

            if (!(Storage.sequenceData.Lists.Lists.Length > listID-1))
            {
                this.Enabled = false;
                return;
            }

            if (Storage.sequenceData.Lists.Lists[listID - 1] == null)
            {
                this.Enabled = false;
                return;
            }


            this.Enabled = true;

            this.enabledBox.Checked = Storage.sequenceData.Lists.ListEnabled[listID - 1];

            updateUIToReflectedEnabled();


            UI_Updating = true;
            for (int i=0; i<Storage.sequenceData.Lists.Lists[listID-1].Count; i++) {
                this.textBox1.AppendText(Storage.sequenceData.Lists.Lists[listID - 1][i].ToString());
                if (i != Storage.sequenceData.Lists.Lists[listID - 1].Count - 1)
                    this.textBox1.AppendText("\r\n");
            }
            this.textBox1_TextChanged(null, null);

            UI_Updating = false;

        }

        private void updateUIToReflectedEnabled()
        {
            if (!this.enabledBox.Checked)
            {
                this.textBox1.Enabled = false;
                this.timesButton.Enabled = false;
                shuffleButton.Enabled = false;
            }
            else
            {
                this.textBox1.Enabled = true;
                this.timesButton.Enabled = true;
                shuffleButton.Enabled = true;
            }
        }

        private void setTimesButtonText()
        {
            if (Storage.sequenceData == null) return;
            if (Storage.sequenceData.Lists == null) return;
            if (Storage.sequenceData.Lists.Cross[listID - 1])
                timesButton.Text = "X";
            else
                timesButton.Text = ",";
        }

        public int NLines()
        {
            return textBox1.Lines.Length;
        }

        public bool isListParsable()
        {
            if (textBox1.Lines.Length == 0) return true;
            for (int i = 0; i < textBox1.Lines.Length; i++)
            {
                double junk;
                if (!Double.TryParse(textBox1.Lines[i], out junk))
                    return false;
            }
            return true;
        }

        public void setData(List<double> data)
        {
            UI_Updating = true;

            textBox1.Text = "";
            for (int i = 0; i < data.Count - 1; i++)
            {
                textBox1.AppendText(data[i] + "\r\n");
            }
            if (data.Count > 0)
                textBox1.AppendText(data[data.Count - 1].ToString());

            UI_Updating = false;
        }

        public List<double> parseList()
        {
            if (!isListParsable())
                return null;

            List<double> ans = new List<double>();

            if (!enabledBox.Checked)
                return ans;

            for (int i = 0; i < textBox1.Lines.Length; i++)
            {
                ans.Add(Double.Parse(textBox1.Lines[i]));
            }
            return ans;
        }

        public void setEnabledBox(bool enabled)
        {
            this.enabledBox.Checked = enabled;
        }

        private bool UI_Updating;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            if (this.isListParsable())
            {
                this.listName.BackColor = Color.Green;
                if (!UI_Updating)
                {
                    if (Storage.sequenceData != null)
                    {
                        if (Storage.sequenceData.Lists != null)
                        {
                            if (Storage.sequenceData.Lists.Lists != null)
                            {
                                if (Storage.sequenceData.Lists.Lists.Length > listID - 1)
                                {
                                    Storage.sequenceData.Lists.Lists[listID - 1] = parseList();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                this.listName.BackColor = Color.Red;
            }
            this.lineCount.Text = this.textBox1.Lines.Length + " lines.";
        }
            

        private void timesButton_Click(object sender, EventArgs e)
        {
            if (listID < ListData.NLists)
                Storage.sequenceData.Lists.Cross[listID - 1] = !Storage.sequenceData.Lists.Cross[listID - 1];
            this.setTimesButtonText();
        }

        private void enabledBox_CheckedChanged(object sender, EventArgs e)
        {
            this.updateUIToReflectedEnabled();
            if (Storage.sequenceData == null)
                return;
            if (Storage.sequenceData.Lists == null)
                return;
            Storage.sequenceData.Lists.ListEnabled[listID - 1] = enabledBox.Checked;
        }

        private void shuffleButton_Click(object sender, EventArgs e)
        {
            string[] preShuffle = this.textBox1.Lines;
            Random rand = new Random();
            List<string> remaining = new List<string>();
            if (preShuffle != null)
            {
                remaining.AddRange(preShuffle);
            }
            List<string> newOrder = new List<string>();

            while (remaining.Count != 0)
            {
                int item = rand.Next(remaining.Count);
                newOrder.Add(remaining[item]);
                remaining.RemoveAt(item);
            }

            textBox1.Lines = newOrder.ToArray();

        }

    }
}
