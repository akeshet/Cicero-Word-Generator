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
    public partial class GpibGroupChannelSelection : UserControl
    {
        public GPIBGroupChannelData groupChannelData;
        public LogicalChannel logicalChannel;

        public event EventHandler updateGUI;




        public GpibGroupChannelSelection()
        {
            InitializeComponent();
            groupChannelData = new GPIBGroupChannelData();
            logicalChannel = new LogicalChannel();
            this.layout();
        
            foreach (GPIBGroupChannelData.GpibChannelDataType type in GPIBGroupChannelData.GpibChannelDataType.allTypes)
            {
                this.dataTypeSelector.Items.Add(type);
            }


        }


        public GpibGroupChannelSelection(LogicalChannel logicalChannel, GPIBGroupChannelData groupChannelData) : this()
        {
            this.groupChannelData = groupChannelData;
            this.logicalChannel = logicalChannel;

            this.toolTip1.SetToolTip(channelNameLabel, logicalChannel.Description);

            this.layout();

            this.dataTypeSelector.Items.Clear();
            foreach (GPIBGroupChannelData.GpibChannelDataType type in GPIBGroupChannelData.GpibChannelDataType.allTypes) {
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
            spsFlowLayoutPanel.Controls.Clear();
			if (groupChannelData.Enabled)
		    {
			    enabledButton.BackColor = Color.Green;
			    enabledButton.Text = "Enabled";
                dataTypeSelector.Enabled = true;

                if (groupChannelData.DataType == GPIBGroupChannelData.GpibChannelDataType.raw_string)
                {
                    rawStringTextBox.Enabled = true;
                    rawStringTextBox.Visible = true;
                    spsFlowLayoutPanel.Visible = false;
                    spsFlowLayoutPanel.Controls.Clear();

                }
                else if (groupChannelData.DataType == GPIBGroupChannelData.GpibChannelDataType.voltage_frequency_waveform)
                {
                    rawStringTextBox.Enabled = false;
                    rawStringTextBox.Visible = false;
                    spsFlowLayoutPanel.Visible = false;
                    spsFlowLayoutPanel.Controls.Clear();

                }
                else if (groupChannelData.DataType == GPIBGroupChannelData.GpibChannelDataType.string_param_string)
                {
                    rawStringTextBox.Enabled = false;
                    rawStringTextBox.Visible = false;
                    spsFlowLayoutPanel.Visible = true;

                    if (groupChannelData.StringParameterStrings == null)
                    {
                        groupChannelData.StringParameterStrings = new List<StringParameterString>();
                        groupChannelData.StringParameterStrings.Add(new StringParameterString());
                    }

                    foreach (StringParameterString sps in groupChannelData.StringParameterStrings)
                    {
                        StringParameterStringEditor spse = new StringParameterStringEditor(sps);

                        spse.insertAbove += new Action<StringParameterString>(spse_insertAbove);
                        spse.insertBelow += new Action<StringParameterString>(spse_insertBelow);
                        spse.delete += new Action<StringParameterString>(spse_delete);

                        spsFlowLayoutPanel.Controls.Add(spse);
                    }

                }
		    }
		    else
		    {
		    	enabledButton.BackColor = Color.Red;
		    	enabledButton.Text = "Continue";
                dataTypeSelector.Enabled = false;
                rawStringTextBox.Enabled = false;
                rawStringTextBox.Visible = true;

		    }
            enabledButton.Invalidate();
        }

        void spse_delete(StringParameterString sps)
        {
            if (sps != null)
            {
                if (groupChannelData.StringParameterStrings.Contains(sps))
                {
                    if (groupChannelData.StringParameterStrings.Count > 1)
                    {
                        groupChannelData.StringParameterStrings.Remove(sps);
                        layout();
                    }
                }
            }
        }

        void spse_insertBelow(StringParameterString sps)
        {
            if (sps != null)
            {
                if (groupChannelData.StringParameterStrings.Contains(sps))
                {
                    int idx = groupChannelData.StringParameterStrings.IndexOf(sps);
                    groupChannelData.StringParameterStrings.Insert(idx + 1, new StringParameterString());
                    layout();
                }
            }
        }

        void spse_insertAbove(StringParameterString sps)
        {
            if (sps != null)
            {
                if (groupChannelData.StringParameterStrings.Contains(sps))
                {
                    int idx = groupChannelData.StringParameterStrings.IndexOf(sps);
                    groupChannelData.StringParameterStrings.Insert(idx, new StringParameterString());
                    layout();
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
            if (dataTypeSelector.SelectedItem is GPIBGroupChannelData.GpibChannelDataType)
            {
                groupChannelData.DataType = (GPIBGroupChannelData.GpibChannelDataType)dataTypeSelector.SelectedItem;
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
