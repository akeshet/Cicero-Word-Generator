using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataStructures;

namespace WordGenerator.Controls
{
    public partial class AnalogQuickEdit : Form
    {
        private int incomingID;

        public AnalogQuickEdit(int selectedID)
        {
            InitializeComponent();
            IDText.Text = selectedID.ToString();
            incomingID = selectedID;

        }



        private void IDText_TextChanged(object sender, EventArgs e)
        {

        }


        private void SubmitButton_Click(object sender, EventArgs e)
        {
            int newKey;
            if (int.TryParse(IDText.Text, out newKey))
            {
                //Set up the datastructures
                ChannelCollection selectedChannelCollection;
                HardwareChannel.HardwareConstants.ChannelTypes selectedChannelType;



                //Bring in all the data, and call the new SwapChannels function
                string selectedTypeString = "analog";
                selectedChannelType = HardwareChannel.HardwareConstants.ParseChannelTypeFromString(selectedTypeString);
                selectedChannelCollection = Storage.settingsData.logicalChannelManager.GetDeviceCollection(selectedChannelType);

                //need to reshuffle the digital data and pulse data in the timesteps....
                AnalogGroupChannelData tempDic = new AnalogGroupChannelData();
                int numberOfSteps = Storage.sequenceData.TimeSteps.Count();
                int numberOfSwaps = newKey - incomingID; //The sign of this int tells us which way we "climb" through the dictionary
                                                         //Simple case of an unoccupied destination key:
                if (!Storage.settingsData.logicalChannelManager.Analogs.ContainsKey(newKey))
                {
                    for (int w = 0; w < numberOfSteps; w++)
                    {
                        if (Storage.sequenceData.TimeSteps[w].AnalogGroup != null) //  "Continue" shows up as a "null" analog group
                        {
                            Storage.sequenceData.TimeSteps[w].AnalogGroup.ChannelDatas[newKey] = Storage.sequenceData.TimeSteps[w].AnalogGroup.ChannelDatas[incomingID];
                            Storage.sequenceData.TimeSteps[w].AnalogGroup.ChannelDatas[incomingID] = new AnalogGroupChannelData();
                        }
                    }
                    selectedChannelCollection.SwapChannels(incomingID, newKey);
                }
                else
                {
                    //Complicated case of an occupied destination key:
                    for (int w = 0; w < numberOfSteps; w++)
                    {
                        #region Pairwise swaps to move the digital data along
                        if (numberOfSwaps > 0)
                        {
                            for (int j = incomingID; j < newKey; j++)
                            {
                                while (!Storage.settingsData.logicalChannelManager.Analogs.ContainsKey(j)) //Sometimes there are gaps in the key sequence because users can choose ANY ID
                                { j++; }

                                if (j >= newKey)
                                {
                                    break;
                                }

                                int nextJ = j + 1;
                                while (!Storage.settingsData.logicalChannelManager.Analogs.ContainsKey(nextJ)) //Sometimes there are gaps in the key sequence because users can choose ANY ID
                                { nextJ++; }

                                if (Storage.sequenceData.TimeSteps[w].AnalogGroup != null) //  "Continue" shows up as a "null" analog group
                                {
                                    tempDic = Storage.sequenceData.TimeSteps[w].AnalogGroup.ChannelDatas[j];
                                }

                                #region Find if a channel already has the next desired ID
                                int conflictFlag = 0;
                                Dictionary<int, LogicalChannel>.KeyCollection keys = Storage.settingsData.logicalChannelManager.ChannelCollections[DataStructures.HardwareChannel.HardwareConstants.ChannelTypes.analog].Channels.Keys;
                                foreach (int i in keys)
                                    if (i == newKey)
                                        conflictFlag = 1;
                                #endregion
                                //If so, we need to delete THAT channel and re-add it with the old ID
                                if (conflictFlag == 1)
                                {
                                    if (Storage.sequenceData.TimeSteps[w].AnalogGroup != null) //  "Continue" shows up as a "null" analog group
                                    {
                                        Storage.sequenceData.TimeSteps[w].AnalogGroup.ChannelDatas[j] = Storage.sequenceData.TimeSteps[w].AnalogGroup.ChannelDatas[nextJ];
                                        Storage.sequenceData.TimeSteps[w].AnalogGroup.ChannelDatas[nextJ] = tempDic;
                                    }

                                }
                                //If not, we just add the new channel at the new ID   
                                else
                                {
                                    if (Storage.sequenceData.TimeSteps[w].AnalogGroup != null) //  "Continue" shows up as a "null" analog group
                                    {
                                        Storage.sequenceData.TimeSteps[w].AnalogGroup.ChannelDatas[j] = new AnalogGroupChannelData();
                                        Storage.sequenceData.TimeSteps[w].AnalogGroup.ChannelDatas[nextJ] = tempDic;
                                    }
                                }


                            }
                        }
                        else if (numberOfSwaps < 0)
                        {
                            for (int j = incomingID; j > newKey; j--)
                            {
                                while (!Storage.settingsData.logicalChannelManager.Analogs.ContainsKey(j)) //Sometimes there are gaps in the key sequence because users can choose ANY ID
                                { j--; }

                                if (j <= newKey)
                                {
                                    break;
                                }

                                int nextJ = j - 1;
                                while (!Storage.settingsData.logicalChannelManager.Analogs.ContainsKey(nextJ)) //Sometimes there are gaps in the key sequence because users can choose ANY ID

                                { nextJ--; }
                                if (Storage.sequenceData.TimeSteps[w].AnalogGroup != null) //  "Continue" shows up as a "null" analog group
                                {
                                    tempDic = Storage.sequenceData.TimeSteps[w].AnalogGroup.ChannelDatas[j];
                                }

                                #region Find if a channel already has the next desired ID
                                int conflictFlag = 0;
                                Dictionary<int, LogicalChannel>.KeyCollection keys = Storage.settingsData.logicalChannelManager.ChannelCollections[DataStructures.HardwareChannel.HardwareConstants.ChannelTypes.analog].Channels.Keys;
                                foreach (int i in keys)
                                    if (i == newKey)
                                        conflictFlag = 1;
                                #endregion
                                //If so, we need to delete THAT channel and re-add it with the old ID
                                if (conflictFlag == 1)
                                {
                                    if (Storage.sequenceData.TimeSteps[w].AnalogGroup != null) //  "Continue" shows up as a "null" analog group
                                    {
                                        Storage.sequenceData.TimeSteps[w].AnalogGroup.ChannelDatas[j] = Storage.sequenceData.TimeSteps[w].AnalogGroup.ChannelDatas[nextJ];
                                        Storage.sequenceData.TimeSteps[w].AnalogGroup.ChannelDatas[nextJ] = tempDic;
                                    }

                                }
                                //If not, we just add the new channel at the new ID   
                                else
                                {
                                    if (Storage.sequenceData.TimeSteps[w].AnalogGroup != null) //  "Continue" shows up as a "null" analog group
                                    {
                                        Storage.sequenceData.TimeSteps[w].AnalogGroup.ChannelDatas[j] = new AnalogGroupChannelData();
                                        Storage.sequenceData.TimeSteps[w].AnalogGroup.ChannelDatas[nextJ] = tempDic;
                                    }
                                }


                            }

                        }
                        #endregion
                    }
                    #region Pairwise swaps to move the analog settings
                    if (numberOfSwaps > 0)
                    {
                        for (int j = incomingID; j < newKey; j++)
                        {
                            while (!Storage.settingsData.logicalChannelManager.Analogs.ContainsKey(j)) //Sometimes there are gaps in the key sequence because users can choose ANY ID
                            { j++; }

                            if (j >= newKey)
                            {
                                break;
                            }


                            int nextJ = j + 1;
                            while (!Storage.settingsData.logicalChannelManager.Analogs.ContainsKey(nextJ)) //Sometimes there are gaps in the key sequence because users can choose ANY ID
                            { nextJ++; }
                            selectedChannelCollection.SwapChannels(j, nextJ);
                        }
                    }
                    else if (numberOfSwaps < 0)
                    {
                        for (int j = incomingID; j > newKey; j--)
                        {
                            while (!Storage.settingsData.logicalChannelManager.Analogs.ContainsKey(j)) //Sometimes there are gaps in the key sequence because users can choose ANY ID
                            { j--; }
                            if (j <= newKey)
                            {
                                break;
                            }
                            int nextJ = j - 1;
                            while (!Storage.settingsData.logicalChannelManager.Analogs.ContainsKey(nextJ)) //Sometimes there are gaps in the key sequence because users can choose ANY ID
                            { nextJ--; }
                            selectedChannelCollection.SwapChannels(j, nextJ);
                        }

                    }
                    #endregion
                }



                //Kill this form
                this.Close();
            }
        }

        private void IDText_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
