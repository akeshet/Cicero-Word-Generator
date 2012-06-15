using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DataStructures;

namespace WordGenerator.ChannelManager
{
    public partial class ChannelManager : Form
    {
        /// <summary>
        /// In the ChannelManager form, we want the ability to correlate logical channels of the application to the physical
        /// channels provided by the various servers. In order to do this, we need to have a list on hand of the available 
        /// physical channels.
        /// </summary>
        public List<HardwareChannel> knownHardwareChannels;


        public ChannelManager()
        {
            InitializeComponent();

            // Obtain the list of available hardware channels
            knownHardwareChannels = new List<HardwareChannel>();
            RefreshKnownHardwareChannels();

            // Add entries to the deviceTypeCombo
            this.deviceTypeCombo.Items.Add("Show all");
            foreach (HardwareChannel.HardwareConstants.ChannelTypes ct in HardwareChannel.HardwareConstants.allChannelTypes)
                this.deviceTypeCombo.Items.Add(ct.ToString());
            this.deviceTypeCombo.SelectedIndex = 0; // Set the default state to "Show all"
        }

        /// <summary>
        /// This method queries the available (ie connected) servers and updates the available hardware channels list accordingly.
        /// </summary>
        public void RefreshKnownHardwareChannels()
        {
            knownHardwareChannels = Storage.settingsData.serverManager.getAllHardwareChannels();

        }

        

        /// <summary>
        /// The following method is responsible for appropriately updating the main logicalDevicesDataGridView with the entries
        /// that correspond to the current deviceTypeCombo selection.
        /// </summary>
        public void RefreshLogicalDeviceDataGrid()
        {
            // Clear the DataGridView
            logicalDevicesDataGridView.Rows.Clear();

            // Treat the "Show all" selection separately from the others
            if (this.deviceTypeCombo.SelectedIndex != 0) // Not "Show all"
            {
                string selectedTypeString = this.deviceTypeCombo.SelectedItem.ToString();
                HardwareChannel.HardwareConstants.ChannelTypes selectedChannelType = HardwareChannel.HardwareConstants.ParseChannelTypeFromString(selectedTypeString);

                EmitLogicalDeviceDictToGrid(selectedChannelType);
            }
            else // We are in the "Show all" case
            {
                // Emit all devices to the grid
                foreach (HardwareChannel.HardwareConstants.ChannelTypes ct in HardwareChannel.HardwareConstants.allChannelTypes)
                    EmitLogicalDeviceDictToGrid(ct);
            }
        }

        /// <summary>
        /// Emits all devices in Storage.settingsData.logicalChannelManager that have the particular ChannelType ct
        /// to the GridView in the "Logical devices" tab of the ChannelManager form. It makes use of EmitLogicalDeviceToGrid
        /// method which is responsible for emitting a single logical device to the grid.
        /// </summary>
        private void EmitLogicalDeviceDictToGrid(HardwareChannel.HardwareConstants.ChannelTypes ct)
        {
            ChannelCollection selectedDeviceDict =
                Storage.settingsData.logicalChannelManager.GetDeviceCollection(ct);

            foreach (int logicalID in selectedDeviceDict.Channels.Keys)
                EmitLogicalDeviceToGrid(ct, logicalID, selectedDeviceDict.Channels[logicalID]);
        }
        private void EmitLogicalDeviceToGrid(HardwareChannel.HardwareConstants.ChannelTypes ct, int logicalID, LogicalChannel lc)
        {
            string[] row = { ct.ToString(),
                             logicalID.ToString(), 
                             lc.Name,
                             lc.Description,
                             lc.HardwareChannel.ToString() };
            logicalDevicesDataGridView.Rows.Add(row);
        }

        /// <summary>
        /// Refresh the logicalDevicesDataGridView
        /// </summary>
        private void deviceTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshLogicalDeviceDataGrid();
        }

        /// <summary>
        /// Launch the AddDevice form
        /// </summary>
        private void addDeviceButton_Click(object sender, EventArgs e)
        {
            AddDevice addDevice = new AddDevice(this);
            addDevice.ShowDialog();
            addDevice.Dispose();
        }

        /// <summary>
        /// Launch the EditDevice form
        /// </summary>
        private void logicalDevicesDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Determine the device that is being edited
            SelectedDevice selectedDevice = DetermineSelectedLogicalChannelFromGrid();

            if (selectedDevice == null) // Abort if nothing is selected
                return;

            EditDevice editDevice = new EditDevice(selectedDevice, this);
            editDevice.ShowDialog();
            editDevice.Dispose();
        }

        /// <summary>
        /// Based on the current state of the logicalDevicesDataGridView, determines the appropriate LogicalChannel
        /// object that is in "focus". This result is wrapped in the SelectedDevice class.
        /// 
        /// If there is nothing selected in the logicalDevicesDataGrid, then we return null.
        /// </summary>
        private SelectedDevice DetermineSelectedLogicalChannelFromGrid()
        {
            if (logicalDevicesDataGridView.SelectedRows.Count == 0) // Do we actually have something selected?
                return null;

            string selectedTypeString = logicalDevicesDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            HardwareChannel.HardwareConstants.ChannelTypes selectedType = HardwareChannel.HardwareConstants.ParseChannelTypeFromString(selectedTypeString);
            
            int selectedLogicalID = int.Parse(logicalDevicesDataGridView.SelectedRows[0].Cells[1].Value.ToString());

            LogicalChannel lc = Storage.settingsData.logicalChannelManager.GetDeviceCollection(selectedType).Channels[selectedLogicalID];

            return new SelectedDevice(selectedTypeString, selectedType, selectedLogicalID, lc);

        }

        private void editDeviceButton_Click(object sender, EventArgs e)
        {
            // Determine the device that is being edited
            SelectedDevice selectedDevice = DetermineSelectedLogicalChannelFromGrid();

            if (selectedDevice == null) // Abort if nothing is selected
                return;

            EditDevice editDevice = new EditDevice(selectedDevice, this);
            editDevice.ShowDialog();
            editDevice.Dispose();
        }

        private void deleteDeviceButton_Click(object sender, EventArgs e)
        {
            // Determine the device that is being edited
            SelectedDevice selectedDevice = DetermineSelectedLogicalChannelFromGrid();

            if (selectedDevice != null) // Abort if nothing is selected
            {

                ChannelCollection selectedDeviceCollection = Storage.settingsData.logicalChannelManager.GetDeviceCollection(
                                                                selectedDevice.channelType);
                selectedDeviceCollection.RemoveChannel(selectedDevice.logicalID);

                // For visual feedback
                RefreshLogicalDeviceDataGrid();
            }
        }

        private void ChannelManager_Load(object sender, EventArgs e)
        {
           // mainClientForm.instance.Enabled = false;
        }

        private void ChannelManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainClientForm.instance.RefreshSettingsDataToUI();
            MainClientForm.instance.RefreshSequenceDataToUI();
          //  mainClientForm.instance.Enabled = true;
        }
 
    }
}