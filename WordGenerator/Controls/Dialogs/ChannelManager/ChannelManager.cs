using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
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
        private string chan1_string;
        private string chan2_string;

        private bool chan1_analog;
        private bool chan2_analog;

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

            //So I want to output the channel list always sorted. The Channel collection is a dictionary, which
            //does not have any inherent order, though it usually comes out sorted "by accident". However, after
            //implementing the channel swap feature, they key collection of the dictionary is not longer accidentally
            //sorted, so here we have to sort it manually:
            var keyList = selectedDeviceDict.getSortedChannelIDList();


            foreach (int logicalID in keyList)
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

        private void txtBoxChan2_TextChanged(object sender, EventArgs e)
        {
            chan2_string = txtBoxChan2.Text;
        }

        private void txtBoxChan1_TextChanged(object sender, EventArgs e)
        {
            chan1_string = txtBoxChan1.Text;
        }

        private void Ch1AnalogCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            chan1_analog = Ch1AnalogCheckBox.Checked;
        }

        private void Ch2AnalogCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            chan2_analog = Ch2AnalogCheckBox.Checked;
        }

        private void moveAnalogChannels(int key, int target)
        {
            ChannelCollection channelSet = Storage.settingsData.logicalChannelManager.GetDeviceCollection(HardwareChannel.HardwareConstants.ChannelTypes.analog);

            List<int> keyList = channelSet.getSortedChannelIDList();

            //checking that the move is valid, i.e. the key and target are valid selections
            if (key < keyList[0])
                key = keyList[0];
            else if (key > keyList[keyList.Count - 1])
                key = keyList[keyList.Count - 1];

            if (target < keyList[0])
                target = keyList[0];
            else if (target > keyList[keyList.Count - 1])
                target = keyList[keyList.Count - 1];


            if (key == target)
                return;

            List<AnalogGroupChannelData> toInsertAnalogLine = Storage.sequenceData.getAnalogSequenceLine(key);

            //hold on to logical channel that will be inserted
            LogicalChannel toInsertIntoSettings = channelSet.Channels[key];

            //remove the logical channel that we will late re-insert
            channelSet.RemoveChannel(key);


            //dump dictionary to a list (only possible because of System.Linq, I think)
            List<KeyValuePair<int, LogicalChannel>> tempList = channelSet.Channels.ToList();

            //sort the list by keys to preserve old ordering when repopulating
            tempList.Sort(
                delegate(KeyValuePair<int, LogicalChannel> p1, KeyValuePair<int, LogicalChannel> p2)
                {
                    return p1.Key.CompareTo(p2.Key);
                });

            int newKey = 0;
            channelSet.Channels.Clear();

            Dictionary<int, List<AnalogGroupChannelData>> rearrangedDigitals = new Dictionary<int, List<AnalogGroupChannelData>>();
            bool insertSuccessfull = false;
            foreach (KeyValuePair<int, LogicalChannel> i in tempList)
            {
                if (newKey == target)
                {
                    channelSet.Channels.Add(newKey, toInsertIntoSettings);
                    rearrangedDigitals.Add(newKey, toInsertAnalogLine);
                    newKey++;
                    channelSet.Channels.Add(newKey, i.Value);
                    rearrangedDigitals.Add(newKey, Storage.sequenceData.getAnalogSequenceLine(i.Key));
                    insertSuccessfull = true;
                }
                else
                {
                    rearrangedDigitals.Add(newKey, Storage.sequenceData.getAnalogSequenceLine(i.Key));
                    channelSet.Channels.Add(newKey, i.Value);
                }
                newKey++;
            }

            if (!insertSuccessfull)
            {
                channelSet.Channels.Add(newKey, toInsertIntoSettings);
                rearrangedDigitals.Add(newKey, toInsertAnalogLine);
            }

            foreach (KeyValuePair<int, List<AnalogGroupChannelData>> i in rearrangedDigitals)
            {
                Storage.sequenceData.setAnalogSequenceLine(i.Key, i.Value);
            }
        }

        private void moveDigitalChannels(int key, int target)
        {


            ChannelCollection channelSet = Storage.settingsData.logicalChannelManager.GetDeviceCollection(HardwareChannel.HardwareConstants.ChannelTypes.digital);
            List<int> keyList = channelSet.getSortedChannelIDList();
            //checking that the move is valid, i.e. the key and target are valid selections
            if (key < keyList[0])
                key = keyList[0];
            else if (key > keyList[keyList.Count-1])
                key = keyList[keyList.Count-1];

            if (target < keyList[0])
                target = keyList[0];
            else if (target > keyList[keyList.Count-1])
                target = keyList[keyList.Count-1];


            if (key == target)
                return;
           
           
            List<DigitalDataPoint> toInsertDigitalLine = Storage.sequenceData.getDigitalSequenceLine(key);

            //hold on to logical channel that will be inserted
            LogicalChannel toInsertIntoSettings = channelSet.Channels[key];
            
            //remove the logical channel that we will late re-insert
            channelSet.RemoveChannel(key);


            //dump dictionary to a list (only possible because of System.Linq, I think)
            List<KeyValuePair<int, LogicalChannel>> tempList = channelSet.Channels.ToList();

            //sort the list by keys to preserve old ordering when repopulating
            tempList.Sort(
                delegate(KeyValuePair<int, LogicalChannel> p1, KeyValuePair<int, LogicalChannel> p2)
                {
                    return p1.Key.CompareTo(p2.Key);
                });

            int newKey = 0;
            channelSet.Channels.Clear();

            Dictionary<int,List<DigitalDataPoint>> rearrangedDigitals = new Dictionary<int,List<DigitalDataPoint>>();
            bool insertSuccessfull = false;
            foreach (KeyValuePair<int, LogicalChannel> i in tempList)
            {
                if (newKey == target)
                {
                    channelSet.Channels.Add(newKey, toInsertIntoSettings);
                    rearrangedDigitals.Add(newKey,toInsertDigitalLine);
                    newKey++;
                    channelSet.Channels.Add(newKey, i.Value);
                    rearrangedDigitals.Add(newKey,Storage.sequenceData.getDigitalSequenceLine(i.Key));
                    insertSuccessfull = true;
                }
                else
                {
                    rearrangedDigitals.Add(newKey,Storage.sequenceData.getDigitalSequenceLine(i.Key));
                    channelSet.Channels.Add(newKey, i.Value);
                }
                newKey++;
            }

            //sloppy way to handle an edge case. It turns out because I removed the toInsert element from the channelSet,
            //if the element is to be inserted with the highest key (i.e. at the end of the dictionary), then the above
            //loop doesn't actually ever get there. So this little if-statement will take care of that.
            if (!insertSuccessfull)
            {
                channelSet.Channels.Add(newKey, toInsertIntoSettings);
                rearrangedDigitals.Add(newKey, toInsertDigitalLine);
            }
            foreach (KeyValuePair<int, List<DigitalDataPoint>> i in rearrangedDigitals)
            {
                Storage.sequenceData.setDigitalSequenceLine(i.Key, i.Value);
            }
            //repopulate list, inserting the moved logical channel when we are at the right key value
          
        }

        private void swapChannelsButton_Click(object sender, EventArgs e)
        {
            //First, check if the swap input is valid
            if (chan1_analog != chan2_analog)
                return;
            int chan1_ID;
            int chan2_ID;

            if (!Int32.TryParse(chan1_string, out chan1_ID) || !Int32.TryParse(chan2_string, out chan2_ID))
                return;

            //If we made it this far without returning, then we have a valid swap to perform:
            HardwareChannel.HardwareConstants.ChannelTypes selectedType;
            if (chan1_analog)
                moveAnalogChannels(chan1_ID, chan2_ID);
            else
                moveDigitalChannels(chan1_ID, chan2_ID);

            //get the two logical channels to swap


           // ChannelCollection selectedChannelCollection = Storage.settingsData.logicalChannelManager.GetDeviceCollection(selectedType);

            //selectedChannelCollection.MoveValue(chan1_ID, chan2_ID);
            
           // DigitalDataPoint temp = Storage.sequenceData.TimeSteps[0].DigitalData[0];
           // Storage.sequenceData.TimeSteps[0].DigitalData.Remove(0);
           // Storage.sequenceData.TimeSteps[0].DigitalData.Add(0,temp);
           // Storage.sequenceData.TimeSteps[0].DigitalData[1] = temp;

           // Storage.sequenceData.TimeSteps.
          


        

//   ChannelCollection selectedDeviceCollection = Storage.settingsData.logicalChannelManager.GetDeviceCollection(
  //                                                              selectedDevice.channelType);
    //            selectedDeviceCollection.RemoveChannel(selectedDevice.logicalID);
      //      asdsa
            RefreshLogicalDeviceDataGrid();
            

        }
 
    }
}