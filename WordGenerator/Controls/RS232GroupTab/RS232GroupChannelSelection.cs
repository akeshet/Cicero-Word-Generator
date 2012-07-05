using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DataStructures;
using WordGenerator.Controls;

namespace wgControlLibrary
{
    public partial class RS232GroupChannelSelection : UserControl
    {
        public RS232GroupChannelData groupChannelData;
        public LogicalChannel logicalChannel;

        public event EventHandler updateGUI;




        public RS232GroupChannelSelection()
        {
            InitializeComponent();
            groupChannelData = new RS232GroupChannelData();
            logicalChannel = new LogicalChannel();
            this.layout();
        
            foreach (RS232GroupChannelData.RS232DataType type in RS232GroupChannelData.allDataTypes)
            {
                this.dataTypeSelector.Items.Add(type);
            }


        }


        public RS232GroupChannelSelection(LogicalChannel logicalChannel, RS232GroupChannelData groupChannelData) : this()
        {
            this.groupChannelData = groupChannelData;
            this.logicalChannel = logicalChannel;

            this.toolTip1.SetToolTip(channelNameLabel, logicalChannel.Description);

            this.layout();

            this.dataTypeSelector.Items.Clear();
            foreach (RS232GroupChannelData.RS232DataType type in RS232GroupChannelData.allDataTypes) {
                this.dataTypeSelector.Items.Add(type);
            }
            //this.dataTypeSelector.Items.AddRange(GPIBGroupChannelData.GpibChannelDataType.allTypes);
            this.dataTypeSelector.SelectedItem = groupChannelData.DataType;
            this.rawStringTextBox.Text = groupChannelData.RawString;
        }
        

        private void enabledButton_Click(object sender, EventArgs e)
        {
            groupChannelData.Enabled = !groupChannelData.Enabled;
            this.layout();
            if (updateGUI != null)
                updateGUI(this, e);
        }

        private void layout()
        {
			if (groupChannelData.Enabled)
		    {
			    enabledButton.BackColor = Color.Green;
			    enabledButton.Text = "Enabled";
                dataTypeSelector.Enabled = true;
                if (groupChannelData.DataType == RS232GroupChannelData.RS232DataType.Raw)
                {
                    rawStringTextBox.Enabled = true;
                    rawStringTextBox.Visible = true;
                    spsFlowPanel.Visible = false;
                }
                else if (groupChannelData.DataType == RS232GroupChannelData.RS232DataType.Parameter)
                {
                    rawStringTextBox.Enabled = false;
                    rawStringTextBox.Visible = false;
                    spsFlowPanel.Visible = true;
                    if (groupChannelData.StringParameterStrings == null)
                    {
                        groupChannelData.StringParameterStrings = new List<StringParameterString>();
                    }
                    if (groupChannelData.StringParameterStrings.Count == 0)
                    {
                        groupChannelData.StringParameterStrings.Add(new StringParameterString());
                    }

                    spsFlowPanel.Controls.Clear();

                    foreach (StringParameterString sps in groupChannelData.StringParameterStrings)
                    {
                        StringParameterStringEditor spse = new StringParameterStringEditor(sps);
                        spsFlowPanel.Controls.Add(spse);

                        spse.delete += new Action<StringParameterString>(spse_delete);
                        spse.insertAbove += new Action<StringParameterString>(spse_insertAbove);
                        spse.insertBelow += new Action<StringParameterString>(spse_insertBelow);
                    }
                     
                }
                
		    }
		    else
		    {
		    	enabledButton.BackColor = Color.Red;
		    	enabledButton.Text = "Continue";
                dataTypeSelector.Enabled = false;
                rawStringTextBox.Enabled = false;
		    }
            enabledButton.Invalidate();
        }

        void spse_insertBelow(StringParameterString sps)
        {
            if (groupChannelData.StringParameterStrings != null)
            {
                if (groupChannelData.StringParameterStrings.Contains(sps))
                {
                    groupChannelData.StringParameterStrings.Insert(
                        groupChannelData.StringParameterStrings.IndexOf(sps) +1, new StringParameterString());
                    this.layout();
                }
            }
        }

        void spse_insertAbove(StringParameterString sps)
        {
            if (groupChannelData.StringParameterStrings != null)
            {
                if (groupChannelData.StringParameterStrings.Contains(sps))
                {
                    groupChannelData.StringParameterStrings.Insert(
                        groupChannelData.StringParameterStrings.IndexOf(sps) , new StringParameterString());
                    this.layout();
                }
            }
        }

        void spse_delete(StringParameterString sps)
        {
            if (groupChannelData.StringParameterStrings != null)
            {
                if (groupChannelData.StringParameterStrings.Contains(sps))
                {
                    groupChannelData.StringParameterStrings.Remove(sps);
                    this.layout();
                }
            }
        }

        private void channelName_Paint(object sender, PaintEventArgs e)
        {
            if (logicalChannel.Name != null)
                channelNameLabel.Text = logicalChannel.Name;
            else
                channelNameLabel.Text = "null";
        }

        private object backupSelectedType;

        private void dataTypeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataTypeSelector.SelectedItem is RS232GroupChannelData.RS232DataType)
            {

                    groupChannelData.DataType = (RS232GroupChannelData.RS232DataType)dataTypeSelector.SelectedItem;
            }
            layout();
            if (updateGUI != null)
                updateGUI(this, null);

            backupSelectedType = dataTypeSelector.SelectedItem;
        }

        private void rawStringTextBox_TextChanged(object sender, EventArgs e)
        {
            groupChannelData.RawString = rawStringTextBox.Text;
        }

        private void dataTypeSelector_DropDownClosed(object sender, EventArgs e)
        {
            if (dataTypeSelector.SelectedItem == null)
                dataTypeSelector.SelectedItem = backupSelectedType;
        }
    }
}
