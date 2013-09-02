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
    public partial class DigitalChannelLabelsPanel : UserControl
    {
        public List<Label> channelLabels;

        public int rowHeight; 

        public DigitalChannelLabelsPanel()
        {
            InitializeComponent();
            this.BorderStyle = BorderStyle.FixedSingle;
            this.AutoScroll = true;
        }

       

        public void layout()
        {
            this.SuspendLayout();
            if (channelLabels != null)
            {
                foreach (Label lbl in channelLabels)
                {
                    this.Controls.Remove(lbl);
                    lbl.Dispose();
                }

                channelLabels.Clear();
            }
            else
                channelLabels = new List<Label>();

            this.ResumeLayout(true);

            this.SuspendLayout();

            List<int> digitalIDs = new List<int>(Storage.settingsData.logicalChannelManager.ChannelCollections[DataStructures.HardwareChannel.HardwareConstants.ChannelTypes.digital].Channels.Keys);
            digitalIDs.Sort();

            DataStructures.Map <int,int> drawIDs= Storage.settingsData.logicalChannelManager.ChannelCollections[DataStructures.HardwareChannel.HardwareConstants.ChannelTypes.digital].DrawGUIMap;
            digitalIDs.Sort();
            int i = -1000;
            for (int j = 0; j < digitalIDs.Count; j++)
            {
                //**** i is switched to drawID value so that channels are drawn where they should be
                //try-catch is for old settings files which don't have any drawID data stored
                try
                {
                    i = drawIDs.GetBySecondKey(j);
                }
                catch(NullReferenceException e)
                {
                    Storage.settingsData.logicalChannelManager.ChannelCollections[DataStructures.HardwareChannel.HardwareConstants.ChannelTypes.digital].MakeGUIMap();
                    drawIDs = Storage.settingsData.logicalChannelManager.ChannelCollections[DataStructures.HardwareChannel.HardwareConstants.ChannelTypes.digital].DrawGUIMap;
                    i = drawIDs.GetBySecondKey(j);
                }
                //**** 
                Label fillerLbl = new Label();
                fillerLbl.Text = "";
                fillerLbl.Width = 18;
                fillerLbl.Height = rowHeight;
                fillerLbl.Location = new Point(this.Width-20, i * rowHeight);
                fillerLbl.TextAlign = ContentAlignment.MiddleRight;
                fillerLbl.AutoEllipsis = true;
                fillerLbl.AutoSize = false;

                

                int digitalID = digitalIDs[i];
                Label lbl = new Label();
                lbl.Text = Storage.settingsData.logicalChannelManager.ChannelCollections[DataStructures.HardwareChannel.HardwareConstants.ChannelTypes.digital].Channels[digitalID].Name;
                lbl.Width = this.Width - 40;
                lbl.Height = rowHeight;
                lbl.Location = new Point(20, i * rowHeight);
                lbl.TextAlign = ContentAlignment.MiddleRight;
                lbl.AutoEllipsis = true;
                lbl.AutoSize = false;

                this.toolTip1.SetToolTip(lbl, Storage.settingsData.logicalChannelManager.Digitals[digitalID].Description);
                
                

                Label idLbl = new Label();
                idLbl.Width = 20;
                idLbl.Height = rowHeight;
                idLbl.Text = digitalID.ToString();
                idLbl.Location = new Point(0, i * rowHeight);
                idLbl.TextAlign = ContentAlignment.MiddleLeft;

                this.toolTip1.SetToolTip(idLbl, Storage.settingsData.logicalChannelManager.Digitals[digitalID].Description);

                Color bCol = DigitalGrid.ChannelColor(i);

                lbl.BackColor = bCol;
                idLbl.BackColor = bCol;
                fillerLbl.BackColor = bCol;

                lbl.ForeColor = Color.White;
                idLbl.ForeColor = Color.White;

                channelLabels.Add(lbl);
                channelLabels.Add(idLbl);
   //             channelLabels.Add(fillerLbl);

 
            }

            this.Controls.AddRange(channelLabels.ToArray());

            this.ResumeLayout();

        }

    }
}
