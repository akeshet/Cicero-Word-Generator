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
            this.deviceNameText.Text = sd.lc.Name;
            this.deviceDescText.Text = sd.lc.Description;
            this.absoluteCheck.Checked = sd.lc.AbsoluteValueChannel;



            this.SignForChannelCombo.Items.Clear();
            this.SignForChannelCombo.Items.Add(new KeyValuePair<int, string>(-1, "none"));

            this.SignForChannelCombo.DisplayMember = "Value";
            this.SignForChannelCombo.ValueMember = "Key";

            foreach (int aid in Storage.settingsData.logicalChannelManager.Analogs.Keys)
            {
                string aname = Storage.settingsData.logicalChannelManager.Analogs[aid].Name;
                this.SignForChannelCombo.Items.Add(new KeyValuePair<int, string>(aid, aname));
            }

            if (Storage.settingsData.logicalChannelManager.Analogs.ContainsKey(sd.lc.SignChannelFor))
            {
                string aname = Storage.settingsData.logicalChannelManager.Analogs[sd.lc.SignChannelFor].Name;
                this.SignForChannelCombo.SelectedItem = new KeyValuePair<int, string>(sd.lc.SignChannelFor, aname);
            }
            else
            {
                this.SignForChannelCombo.SelectedIndex = 0;
            }


            this.availableHardwareChanCombo.Items.Clear();
            this.availableHardwareChanCombo.Items.Add(HardwareChannel.Unassigned);
            if (sd.lc.HardwareChannel!=null) 
                this.availableHardwareChanCombo.Items.Add(sd.lc.HardwareChannel);
            
            // Fill the availableHardwareChanCombo with relevant items
            foreach (HardwareChannel hc in cm.knownHardwareChannels)
                if (hc.ChannelType == sd.channelType)
                    if (!Storage.settingsData.logicalChannelManager.AssignedHardwareChannels.Contains(hc))
                        this.availableHardwareChanCombo.Items.Add(hc);

            this.availableHardwareChanCombo.SelectedItem = sd.lc.HardwareChannel;

            togglingCheck.Checked = sd.lc.TogglingChannel;

            if (sd.channelType == HardwareChannel.HardwareConstants.ChannelTypes.analog)
            {
                checkBox1.Visible = true;
                absoluteCheck.Visible = true;
            }
            else
            {
                absoluteCheck.Visible = false;
                checkBox1.Visible = false;
            }

            if (sd.channelType == HardwareChannel.HardwareConstants.ChannelTypes.digital)
            {
                SignForChannelCombo.Visible = true;
                SignLabel.Visible = true;
            }
            else
            {
                SignForChannelCombo.Visible = false;
                SignLabel.Visible = false;
            }

            if (sd.channelType == HardwareChannel.HardwareConstants.ChannelTypes.analog ||
                sd.channelType == HardwareChannel.HardwareConstants.ChannelTypes.digital)
            {
                togglingCheck.Visible = true;
            }
            else
            {
                togglingCheck.Visible = false;
            }

            checkBox1.Checked = sd.lc.AnalogChannelOutputNowUsesDwellWord;

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            sd.lc.Name = this.deviceNameText.Text;
            sd.lc.Description = this.deviceDescText.Text;
            sd.lc.AnalogChannelOutputNowUsesDwellWord = checkBox1.Checked;
            
            if (this.availableHardwareChanCombo.SelectedItem is HardwareChannel)
                sd.lc.HardwareChannel = (HardwareChannel) this.availableHardwareChanCombo.SelectedItem;
            else
                sd.lc.HardwareChannel = HardwareChannel.Unassigned;

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

        private void togglingCheck_CheckedChanged(object sender, EventArgs e)
        {
            sd.lc.TogglingChannel = togglingCheck.Checked;
        }

        private void absoluteCheck_CheckedChanged(object sender, EventArgs e)
        {
            sd.lc.AbsoluteValueChannel = absoluteCheck.Checked;
        }

        private void SignForChannelCombo_ValueChanged(object sender, EventArgs e)
        {
            if(SignForChannelCombo.SelectedItem != null)
                sd.lc.SignChannelFor = ((KeyValuePair<int,string>) SignForChannelCombo.SelectedItem).Key;
        }

    }
}