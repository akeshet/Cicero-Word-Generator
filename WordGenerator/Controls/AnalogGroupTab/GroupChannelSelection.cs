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
    public partial class GroupChannelSelection : UserControl
    {
        public AnalogGroupChannelData groupChannelData;
        public LogicalChannel logicalChannel;

        public event EventHandler updateGUI;

        public void setChannelData(AnalogGroupChannelData channelData)
        {
            this.groupChannelData = channelData;

            layout();
        }


        public GroupChannelSelection()
        {
            InitializeComponent();
            groupChannelData = new AnalogGroupChannelData(null, false, false);
            logicalChannel = new LogicalChannel();
            logicalChannel.Name = "Placeholder Channel";
            this.commonWaveformSelector.Items.Add("Manual");
            this.commonWaveformSelector.SelectedItem = "Manual";
            this.setButtonAppearance();
        }


        public GroupChannelSelection(LogicalChannel logicalChannel, AnalogGroupChannelData groupChannelData, int channelID) : this()
        {
            this.groupChannelData = groupChannelData;
            setCommonWaveforms();
            layout();


            this.logicalChannel = logicalChannel;
            this.toolTip1.SetToolTip(channelNameLabel, logicalChannel.Description);


            channelNameLabel.Text = channelID.ToString() + " " + logicalChannel.Name;
        }

        private void layout()
        {
            if (groupChannelData.ChannelWaveformIsCommon)
                commonWaveformSelector.SelectedItem = groupChannelData.waveform;
            else
                commonWaveformSelector.SelectedItem = "Manual";
            this.setButtonAppearance();
        }
        

        private void enabledButton_Click(object sender, EventArgs e)
        {
            groupChannelData.ChannelEnabled = !groupChannelData.ChannelEnabled;
            this.setButtonAppearance();
            if (updateGUI != null)
                updateGUI(this, e);
        }

        private void setButtonAppearance()
        {
			if (groupChannelData.ChannelEnabled)
		    {
			    enabledButton.BackColor = Color.Green;
			    enabledButton.Text = "Enabled";
                commonWaveformSelector.Enabled = true;
		    }
		    else
		    {
		    	enabledButton.BackColor = Color.Red;
		    	enabledButton.Text = "Continue";
                commonWaveformSelector.Enabled = false;
		    }
            enabledButton.Invalidate();
        }


        private void commonWaveformSelector_DropDown(object sender, EventArgs e)
        {
            setCommonWaveforms();
        }

        private void setCommonWaveforms()
        {
            this.commonWaveformSelector.Items.Clear();
            this.commonWaveformSelector.Items.Add("Manual");
            foreach (Waveform wf in WordGenerator.Storage.sequenceData.CommonWaveforms)
                this.commonWaveformSelector.Items.Add(wf);
        }

        private void commonWaveformSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            Waveform wf = commonWaveformSelector.SelectedItem as Waveform;
            if (wf == null)
            {
                if (groupChannelData.ChannelWaveformIsCommon)
                    groupChannelData.waveform = new Waveform();

                groupChannelData.ChannelWaveformIsCommon = false;
     
            }
            else
            {
                groupChannelData.ChannelWaveformIsCommon = true;
                groupChannelData.waveform = wf;
            }

            if (updateGUI != null)
                updateGUI(this, e);
        }




    }
}
