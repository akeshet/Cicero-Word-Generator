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
    public partial class QuickEdit : Form
    {
        private int incomingID;

        public QuickEdit(int selectedID)
        {
            InitializeComponent();
            IDText.Text = selectedID.ToString();
            incomingID = selectedID;
            if (Storage.settingsData.logicalChannelManager.ChannelCollections[DataStructures.HardwareChannel.HardwareConstants.ChannelTypes.digital].Channels[incomingID].ChannelColor != System.Drawing.Color.Empty)
            {
                colorSwatch.BackColor = Storage.settingsData.logicalChannelManager.ChannelCollections[DataStructures.HardwareChannel.HardwareConstants.ChannelTypes.digital].Channels[incomingID].ChannelColor;
            }
            else
            {
                colorSwatch.BackColor = Color.RoyalBlue;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           int newKey;
           if (int.TryParse(IDText.Text, out newKey))
           {
               //Set up the datastructures
               ChannelCollection selectedChannelCollection;
               HardwareChannel.HardwareConstants.ChannelTypes selectedChannelType;

               //Now update the color
               Storage.settingsData.logicalChannelManager.ChannelCollections[DataStructures.HardwareChannel.HardwareConstants.ChannelTypes.digital].Channels[incomingID].ChannelColor = this.colorSwatch.BackColor;

               //Bring in all the data, and call the new SwapChannels function
               string selectedTypeString = "digital";
               selectedChannelType = HardwareChannel.HardwareConstants.ParseChannelTypeFromString(selectedTypeString);
               selectedChannelCollection = Storage.settingsData.logicalChannelManager.GetDeviceCollection(selectedChannelType);
               
               //need to reshuffle the digital data and pulse data in the timesteps....
               DigitalDataPoint tempDic;
               int numberOfSteps = Storage.sequenceData.TimeSteps.Count();
               int numberOfSwaps = newKey - incomingID; //The sign of this int tells us which way we "climb" through the dictionary
               //Simple case of an unoccupied destination key:
               if (!Storage.settingsData.logicalChannelManager.Digitals.ContainsKey(newKey))
               {
                   for (int w = 0; w < numberOfSteps; w++)
                   {
                       Storage.sequenceData.TimeSteps[w].DigitalData[newKey] = Storage.sequenceData.TimeSteps[w].DigitalData[incomingID];
                       Storage.sequenceData.TimeSteps[w].DigitalData[incomingID] = new DigitalDataPoint();
                   }
                   selectedChannelCollection.SwapChannels(incomingID,newKey);
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
                               while (!Storage.settingsData.logicalChannelManager.Digitals.ContainsKey(j)) //Sometimes there are gaps in the key sequence because users can choose ANY ID
                               { j++; }

                               if (j >= newKey)
                               {
                                   break;
                               }

                               int nextJ = j + 1;
                               while (!Storage.settingsData.logicalChannelManager.Digitals.ContainsKey(nextJ)) //Sometimes there are gaps in the key sequence because users can choose ANY ID
                               { nextJ++; }

                               tempDic = Storage.sequenceData.TimeSteps[w].DigitalData[j];

                               #region Find if a channel already has the next desired ID
                               int conflictFlag = 0;
                               Dictionary<int, LogicalChannel>.KeyCollection keys = Storage.settingsData.logicalChannelManager.ChannelCollections[DataStructures.HardwareChannel.HardwareConstants.ChannelTypes.digital].Channels.Keys;
                               foreach (int i in keys)
                                   if (i == newKey)
                                       conflictFlag = 1;
                               #endregion
                               //If so, we need to delete THAT channel and re-add it with the old ID
                               if (conflictFlag == 1)
                               {
                                   Storage.sequenceData.TimeSteps[w].DigitalData[j] = Storage.sequenceData.TimeSteps[w].DigitalData[nextJ];
                                   Storage.sequenceData.TimeSteps[w].DigitalData[nextJ] = tempDic;

                               }
                               //If not, we just add the new channel at the new ID   
                               else
                               {
                                   Storage.sequenceData.TimeSteps[w].DigitalData[j] = new DigitalDataPoint();
                                   Storage.sequenceData.TimeSteps[w].DigitalData[nextJ] = tempDic;
                               }


                           }
                       }
                       else if (numberOfSwaps < 0)
                       {
                           for (int j = incomingID; j > newKey; j--)
                           {
                               while (!Storage.settingsData.logicalChannelManager.Digitals.ContainsKey(j)) //Sometimes there are gaps in the key sequence because users can choose ANY ID
                               { j--; }

                               if (j <= newKey)
                               {
                                   break;
                               }

                               int nextJ = j - 1;
                               while (!Storage.settingsData.logicalChannelManager.Digitals.ContainsKey(nextJ)) //Sometimes there are gaps in the key sequence because users can choose ANY ID
                               { nextJ--; }

                               tempDic = Storage.sequenceData.TimeSteps[w].DigitalData[j];

                               #region Find if a channel already has the next desired ID
                               int conflictFlag = 0;
                               Dictionary<int, LogicalChannel>.KeyCollection keys = Storage.settingsData.logicalChannelManager.ChannelCollections[DataStructures.HardwareChannel.HardwareConstants.ChannelTypes.digital].Channels.Keys;
                               foreach (int i in keys)
                                   if (i == newKey)
                                       conflictFlag = 1;
                               #endregion
                               //If so, we need to delete THAT channel and re-add it with the old ID
                               if (conflictFlag == 1)
                               {
                                   Storage.sequenceData.TimeSteps[w].DigitalData[j] = Storage.sequenceData.TimeSteps[w].DigitalData[nextJ];
                                   Storage.sequenceData.TimeSteps[w].DigitalData[nextJ] = tempDic;

                               }
                               //If not, we just add the new channel at the new ID   
                               else
                               {
                                   Storage.sequenceData.TimeSteps[w].DigitalData[j] = new DigitalDataPoint();
                                   Storage.sequenceData.TimeSteps[w].DigitalData[nextJ] = tempDic;
                               }


                           }

                       }
                       #endregion
                   }
                   #region Pairwise swaps to move the digital settings
                   if (numberOfSwaps > 0)
                   {
                       for (int j = incomingID; j < newKey; j++)
                       {
                           while (!Storage.settingsData.logicalChannelManager.Digitals.ContainsKey(j)) //Sometimes there are gaps in the key sequence because users can choose ANY ID
                           { j++; }

                           if (j >= newKey)
                           {
                               break;
                           }
                          

                           int nextJ = j + 1;
                           while (!Storage.settingsData.logicalChannelManager.Digitals.ContainsKey(nextJ)) //Sometimes there are gaps in the key sequence because users can choose ANY ID
                           { nextJ++; }
                           selectedChannelCollection.SwapChannels(j, nextJ);
                       }
                   }
                   else if (numberOfSwaps < 0)
                   {
                       for (int j = incomingID; j > newKey; j--)
                       {
                           while (!Storage.settingsData.logicalChannelManager.Digitals.ContainsKey(j)) //Sometimes there are gaps in the key sequence because users can choose ANY ID
                           { j--; }
                           if (j <= newKey)
                           {
                               break;
                           }
                           int nextJ = j - 1;
                           while (!Storage.settingsData.logicalChannelManager.Digitals.ContainsKey(nextJ)) //Sometimes there are gaps in the key sequence because users can choose ANY ID
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void IDText_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void QuickEdit_Load(object sender, EventArgs e)
        {

        }

        private void colorSwatch_Click(object sender, EventArgs e)
        {
            this.colorDialog1.ShowDialog();
            colorSwatch.BackColor = colorDialog1.Color;
        }

        private void colorSwatch_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
