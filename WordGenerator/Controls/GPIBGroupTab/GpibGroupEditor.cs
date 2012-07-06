using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DataStructures;
using wgControlLibrary;

namespace WordGenerator.Controls
{
    public partial class GpibGroupEditor : UserControl
    {
        private GPIBGroup gpibGroup;

        private ChannelCollection gpibChannelCollection;

        private List<GpibGroupChannelSelection> groupChannelSelectors;

        /// <summary>
        /// This bool is used to stop an infinite loop when selecting an analoggroup from a combobox.
        /// </summary>
        private bool gpibGroupBeingChanged = false;

        private void layoutGroupChannelSelectors() 
        {
            this.groupChannelSelectorPanel.Controls.Clear();
            foreach (GpibGroupChannelSelection sel in groupChannelSelectors)
            {
                sel.Dispose();
            }
            groupChannelSelectors.Clear();

            List<int> channelIDs = gpibChannelCollection.getSortedChannelIDList();

            foreach (int id in channelIDs)
            {
                groupChannelSelectors.Add(new GpibGroupChannelSelection(gpibChannelCollection.Channels[id], gpibGroup.getChannelData(id)));
            }

            for (int i = 0; i < groupChannelSelectors.Count; i++)
            {
                groupChannelSelectors[i].Visible = true;
                if (gpibGroup == null)
                    groupChannelSelectors[i].Enabled = false;
                else
                    groupChannelSelectors[i].Enabled = true;
                this.groupChannelSelectorPanel.Controls.Add(groupChannelSelectors[i]);
                groupChannelSelectors[i].Show();
                groupChannelSelectors[i].updateGUI += new EventHandler(groupChannnelSelector_updateGUI);
            }
            this.groupChannelSelectorPanel.Invalidate();
        }

        void groupChannnelSelector_updateGUI(object sender, EventArgs e)
        {
            this.layoutGraphCollection();
        }

        private List<Label> runOrderLabels;
        private Dictionary<Label, GPIBGroup> runOrderLabelGroups;
        public void updateRunOrderPanel()
        {
            if (runOrderLabels != null)
            {
                foreach (Label lab in runOrderLabels)
                {
                    runOrderPanel.Controls.Remove(lab);
                    lab.Dispose();
                }
                runOrderLabels.Clear();
                runOrderLabelGroups.Clear();
            }
            else
            {
                runOrderLabels = new List<Label>();
                runOrderLabelGroups = new Dictionary<Label, GPIBGroup>();
            }

            int xPos = label2.Location.X + label2.Width;
            if (Storage.sequenceData != null)
            {
                if (Storage.sequenceData.TimeSteps != null)
                {
                    foreach (TimeStep step in Storage.sequenceData.enabledTimeSteps())
                    {
                        if (step.GpibGroup != null)
                        {
                            GPIBGroup gg = step.GpibGroup;
                            Label lab = new Label();
                            lab.Text = gg.ToString();
                            lab.BorderStyle = BorderStyle.FixedSingle;
                            lab.AutoSize = false;
                            lab.Width = 80;
                            lab.TextAlign = ContentAlignment.MiddleCenter;
                            lab.AutoEllipsis = true;
                            lab.Location = new Point(xPos, label2.Location.Y);
                            lab.Click += new EventHandler(runOrderLabelClick);
                            runOrderLabelGroups.Add(lab, gg);
                            runOrderLabels.Add(lab);



                            this.toolTip1.SetToolTip(lab, "Timestep: " + step.StepName + ", Duration: " + step.StepDuration.ToString());

                            xPos += lab.Width + 10;
                        }
                    }
                }
            }

            runOrderPanel.Controls.AddRange(runOrderLabels.ToArray());

        }

        void runOrderLabelClick(object sender, EventArgs e)
        {
            Label lab = sender as Label;
            if (runOrderLabelGroups.ContainsKey(lab))
            {
                this.setGpibGroup(runOrderLabelGroups[lab]);
            }
        }


        public void setGpibGroup(GPIBGroup gpibGroup)
        {

            if (gpibGroup == null)
            {
                gpibGroup = new GPIBGroup("Placehold analog group. Do not use.");
                replacementGroupSelector.Enabled = false;
            }
            else 
                replacementGroupSelector.Enabled = true;

            previousObjectBackup = gpibGroup;
            
            this.gpibGroup = gpibGroup;


            this.renameTextBox.Text = gpibGroup.GroupName;
            fillSelectorCombobox();
            gpibGroupBeingChanged = true;
            this.gpibGroupSelector.SelectedItem = gpibGroup;
            gpibGroupBeingChanged = false;
            layoutGroupChannelSelectors();
            layoutGraphCollection();
            waveformEditor1.setWaveform(null);
            descBox.Text = gpibGroup.GroupDescription;

            replacementGroupSelector.SelectedItem = null;

        }

        private void layoutGraphCollection()
        {
            waveformEditor1.setWaveform(null);

            if (WordGenerator.MainClientForm.instance!=null)
                WordGenerator.MainClientForm.instance.cursorWait();
            try
            {
                List<Waveform> waveformsToDisplay = new List<Waveform>();
                List<string> channelNamesToDisplay = new List<string>();


                // figure out what to display in the waveform graph
                if (gpibGroup != null)
                {
                    List<int> usedChannelIDs = gpibGroup.getChannelIDs();
                    for (int id = 0; id < usedChannelIDs.Count; id++)
                    {
                        int gpibID = usedChannelIDs[id];

                        if (Storage.settingsData.logicalChannelManager.ChannelCollections[HardwareChannel.HardwareConstants.ChannelTypes.gpib].Channels.ContainsKey(gpibID))
                        {

                            GPIBGroupChannelData channelData = gpibGroup.ChannelDatas[gpibID];
                            if (channelData.Enabled)
                            {
                                if (channelData.DataType == GPIBGroupChannelData.GpibChannelDataType.voltage_frequency_waveform)
                                {
                                    waveformsToDisplay.Add(channelData.volts);
                                    channelNamesToDisplay.Add(gpibChannelCollection.Channels[gpibID].Name + " Vpp");
                                    waveformsToDisplay.Add(channelData.frequency);
                                    channelNamesToDisplay.Add(gpibChannelCollection.Channels[gpibID].Name + " Hz");
                                }
                            }
                        }
                    }
                }


                waveformGraphCollection1.deactivateAllGraphs();

                waveformGraphCollection1.setWaveforms(waveformsToDisplay);
                waveformGraphCollection1.setChannelNames(channelNamesToDisplay);
                waveformGraphCollection1.setWaveformEditor(waveformEditor1);
            }
            finally
            {
                if (WordGenerator.MainClientForm.instance != null)
                    WordGenerator.MainClientForm.instance.cursorWaitRelease();
            }

        }

        public void setChannelCollection(ChannelCollection gpibChannelCollection)
        {
            this.gpibChannelCollection = gpibChannelCollection;
            this.layoutGroupChannelSelectors();
        }

        public GpibGroupEditor()
        {
            InitializeComponent();
            groupChannelSelectors = new List<GpibGroupChannelSelection>();
            this.setChannelCollection(new ChannelCollection());
            this.setGpibGroup(new GPIBGroup("Placehold gpib group. Do not use."));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GPIBGroup gg = new GPIBGroup("Gpib Group " + (Storage.sequenceData.GpibGroups.Count+1));
            Storage.sequenceData.GpibGroups.Add(gg);
            setGpibGroup(gg);
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            fillSelectorCombobox();
        }

        private void fillSelectorCombobox()
        {
            gpibGroupSelector.Items.Clear();
            if (Storage.sequenceData != null)
            {
                foreach (GPIBGroup gg in Storage.sequenceData.GpibGroups)
                    gpibGroupSelector.Items.Add(gg);
            }

        }

        Object previousObjectBackup;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!gpibGroupBeingChanged)
            {
                previousObjectBackup = gpibGroupSelector.SelectedItem;
                GPIBGroup gg = gpibGroupSelector.SelectedItem as GPIBGroup;
                setGpibGroup(gg);
            }
        }

        private void renameTextBox_TextChanged(object sender, EventArgs e)
        {
            //gpibGroup.GroupName = renameTextBox.Text;
        }

        private void renameButton_Click(object sender, EventArgs e)
        {
            GPIBGroup temp = this.gpibGroup;
            if (gpibGroup != null)
            {
                gpibGroup.GroupName = renameTextBox.Text;
                this.gpibGroupSelector.SelectedItem = null;
                this.gpibGroupSelector.SelectedItem = temp;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Storage.sequenceData != null)
            {
                if (Storage.sequenceData.GpibGroups.Contains(this.gpibGroup))
                {
                    foreach (TimeStep step in Storage.sequenceData.TimeSteps)
                    {
                        if (step.GpibGroup == this.gpibGroup)
                        {
                            MessageBox.Show("Cannot delete this group, it is used in timestep " + step.ToString());
                            return;
                        }
                    }
                    Storage.sequenceData.GpibGroups.Remove(this.gpibGroup);
                    this.gpibGroupSelector.SelectedItem = null;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.gpibGroup == null)
            {
                MessageBox.Show("Cannot output null group.");
                return;
            }

            if (!Storage.sequenceData.Lists.ListLocked)
            {
                MessageBox.Show("Lists not locked, unable to output.");
                return;
            }

            if (Storage.settingsData.unconnectedRequiredServers().Count != 0)
            {
                string missingServers = ServerManager.convertListOfServersToOneString(Storage.settingsData.unconnectedRequiredServers());
                MessageBox.Show("Unable to output group, the following required servers are missing: " + missingServers);
                return;
            }

            ServerManager.ServerActionStatus status = Storage.settingsData.serverManager.outputGPIBGroupOnConnectedServers(this.gpibGroup, Storage.settingsData, WordGenerator.MainClientForm.instance.handleMessageEvent);
            if (status != ServerManager.ServerActionStatus.Success)
            {
                MessageBox.Show("Failed due to server error or disconnection.");
                return;
            }

            MessageBox.Show("Group " + gpibGroup.ToString() + " output successfully.");
        }

        private void descBox_TextChanged(object sender, EventArgs e)
        {
            if (this.gpibGroup != null)
            {
                gpibGroup.GroupDescription = descBox.Text;
            }
        }

        private void plus_Click(object sender, EventArgs e)
        {
            if (gpibGroupSelector.SelectedIndex < gpibGroupSelector.Items.Count - 1)
                gpibGroupSelector.SelectedIndex++;
        }

        private void minus_Click(object sender, EventArgs e)
        {
            if (gpibGroupSelector.SelectedIndex > 0)
                gpibGroupSelector.SelectedIndex--;
        }

        private void gpibGroupSelector_DropDownClosed(object sender, EventArgs e)
        {
            if (gpibGroupSelector.SelectedItem == null)
                gpibGroupSelector.SelectedItem = previousObjectBackup as GPIBGroup;
        }

        private void GpibGroupEditor_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                updateRunOrderPanel();
            }
        }

        private void waveformEditor1_copyDuration(object sender, EventArgs e)
        {
            if (waveformEditor1.CurrentWaveform != null)
            {
                DimensionedParameter par = waveformEditor1.CurrentWaveform.WaveformDuration;

                foreach (int chanID in Storage.settingsData.logicalChannelManager.GPIBs.Keys)
                {
                    if (this.gpibGroup != null)
                    {
                        if (this.gpibGroup.channelEnabled(chanID))
                        {
                            if (this.gpibGroup.ChannelDatas[chanID].DataType == GPIBGroupChannelData.GpibChannelDataType.voltage_frequency_waveform)
                            {
                                GPIBGroupChannelData dat = this.gpibGroup.ChannelDatas[chanID];
                                if (dat.frequency != null)
                                    dat.frequency.WaveformDuration = new DimensionedParameter(par);
                                if (dat.volts != null)
                                    dat.volts.WaveformDuration = new DimensionedParameter(par);
                            }

                        }
                    }
                }

                this.waveformGraphCollection1.redrawAllGraphs();
            }
        }

        private void replacementSelector_DropDown_1(object sender, EventArgs e)
        {
            replacementGroupSelector.Items.Clear();
            replacementGroupSelector.Items.AddRange(Storage.sequenceData.GpibGroups.ToArray());
        }

        private void replaceGroupButton_Click(object sender, EventArgs e)
        {
            GPIBGroup replacementGroup = replacementGroupSelector.SelectedItem as GPIBGroup;
            if (replacementGroup != null)
            {
                if (replacementGroup != this.gpibGroup)
                {
                    DialogResult result = MessageBox.Show("This will permanently replace all occurences of the currently edited group with the group selected near the Replace button. Are you sure you want to proceed?", "Replace analog group?", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Storage.sequenceData.replaceGPIBGroup(gpibGroup, replacementGroup);
                        WordGenerator.MainClientForm.instance.RefreshSequenceDataToUI();
                    }
                }
            }
        }

        private void replacementGroupSelector_SelectedValueChanged(object sender, EventArgs e)
        {
            if (replacementGroupSelector.SelectedItem as GPIBGroup == null)
            {
                replaceGroupButton.Enabled = false;
            }
            else
            {
                replaceGroupButton.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("This action will delete all unused gpib groups (ie groups that are not activated anywhere in the sequence. Are you sure you want to continue?", "Delete unused groups?", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                List<GPIBGroup> usedGroups = new List<GPIBGroup>();
                foreach (TimeStep step in Storage.sequenceData.TimeSteps)
                {
                    if (step.GpibGroup != null)
                        if (!usedGroups.Contains(step.GpibGroup))
                            usedGroups.Add(step.GpibGroup);
                }

                Storage.sequenceData.GpibGroups = usedGroups;
                WordGenerator.MainClientForm.instance.RefreshSequenceDataToUI();
            }

        }


    }
}
