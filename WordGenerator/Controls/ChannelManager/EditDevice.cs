using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DataStructures;

namespace WordGenerator.ChannelManager
{
    public partial class EditDevice : Form
    {
        ChannelManager cm;
        SelectedDevice sd;
        public EditDevice(SelectedDevice sd, ChannelManager cm)
        {
            InitializeComponent();

            this.cm = cm;
            this.sd = sd;

            // Initialize the fields with relevant information
            this.logicalIDText.Text = sd.logicalID.ToString();
            this.deviceTypeText.Text = sd.channelTypeString;
            this.deviceNameText.Text = sd.lc.name;
            this.deviceDescText.Text = sd.lc.description;

            this.availableHardwareChanCombo.Items.Clear();
            this.availableHardwareChanCombo.Items.Add(HardwareChannel.Unassigned);
            if (sd.lc.hardwareChannel!=null) 
                this.availableHardwareChanCombo.Items.Add(sd.lc.hardwareChannel);
            
            // Fill the availableHardwareChanCombo with relevant items
            foreach (HardwareChannel hc in cm.knownHardwareChannels)
                if (hc.ChannelType == sd.channelType)
                    if (!Storage.settingsData.logicalChannelManager.AssignedHardwareChannels.Contains(hc))
                        this.availableHardwareChanCombo.Items.Add(hc);

            this.availableHardwareChanCombo.SelectedItem = sd.lc.hardwareChannel;

            if (sd.channelType == HardwareChannel.HardwareConstants.ChannelTypes.analog)
            {
                checkBox1.Visible = true;
            }
            else
            {
                checkBox1.Visible = false;
            }

            checkBox1.Checked = sd.lc.analogChannelOutputNowUsesDwellWord;

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            sd.lc.name = this.deviceNameText.Text;
            sd.lc.description = this.deviceDescText.Text;
            sd.lc.analogChannelOutputNowUsesDwellWord = checkBox1.Checked;
            
            if (this.availableHardwareChanCombo.SelectedItem is HardwareChannel)
                sd.lc.hardwareChannel = (HardwareChannel) this.availableHardwareChanCombo.SelectedItem;
            else
                sd.lc.hardwareChannel = HardwareChannel.Unassigned;

            // Visual feedback
            cm.RefreshLogicalDeviceDataGrid();

            this.Close();
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void refreshHardwareButton_Click(object sender, EventArgs e)
        {
            cm.RefreshKnownHardwareChannels();

            this.availableHardwareChanCombo.Items.Clear();
            this.availableHardwareChanCombo.Items.Add(HardwareChannel.Unassigned);

            // Fill the availableHardwareChanCombo with relevant items
            foreach (HardwareChannel hc in cm.knownHardwareChannels)
                if (hc.ChannelType == sd.channelType) 
                    if (!Storage.settingsData.logicalChannelManager.AssignedHardwareChannels.Contains(hc))
                        this.availableHardwareChanCombo.Items.Add(hc);
        }

        private void availableHardwareChanCombo_DropDownClosed(object sender, EventArgs e)
        {
        }

    }
}