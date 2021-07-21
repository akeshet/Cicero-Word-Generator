using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace WordGenerator.Controls
{
    public class AnalogChannelLabelsPanel : UserControl
    {

        private List<Label> channelLabels;
        private ToolTip toolTip1;
        private System.ComponentModel.IContainer components;

        public int rowHeight;

        public AnalogChannelLabelsPanel()
            : base()
        {
            InitializeComponent();
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

            List<int> analogIDs = new List<int>(Storage.settingsData.logicalChannelManager.ChannelCollections[DataStructures.HardwareChannel.HardwareConstants.ChannelTypes.analog].Channels.Keys);
            analogIDs.Sort();

            for (int i = 0; i < analogIDs.Count; i++)
            {
                int analogID = analogIDs[i];
                Label lbl = new Label();
                lbl.Location = new Point(20, i * rowHeight + 10);
                lbl.TextAlign = ContentAlignment.MiddleRight;
                lbl.Width = this.Width - 50;
                lbl.Height = rowHeight;
                lbl.AutoEllipsis = true;
                lbl.AutoSize = false;
                lbl.Text = Storage.settingsData.logicalChannelManager.ChannelCollections[DataStructures.HardwareChannel.HardwareConstants.ChannelTypes.analog].Channels[analogID].Name;


                toolTip1.SetToolTip(lbl, Storage.settingsData.logicalChannelManager.Analogs[analogID].Description);

                Label idLbl = new Label();
                idLbl.Text = analogID.ToString();
                idLbl.Location = new Point(0, i * rowHeight + 10);
                idLbl.TextAlign = ContentAlignment.MiddleLeft;
                idLbl.Width = 20;
                idLbl.Height = rowHeight;

                toolTip1.SetToolTip(idLbl, Storage.settingsData.logicalChannelManager.Analogs[analogID].Description);


                channelLabels.Add(lbl);
                channelLabels.Add(idLbl);
                channelLabels[2 * i + 1].Click += new EventHandler(AnalogChannelLabelsPanel_Click); //For ID
            }

            this.Controls.AddRange(channelLabels.ToArray());
            this.ResumeLayout();

        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 100;
            this.toolTip1.AutoPopDelay = 15000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 20;
            // 
            // AnalogChannelLabelsPanel
            // 
            this.AutoScroll = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "AnalogChannelLabelsPanel";
            this.Size = new System.Drawing.Size(148, 148);
            this.ResumeLayout(false);

        }

        private void AnalogChannelLabelsPanel_Load(object sender, EventArgs e)
        {

        }


        private void AnalogChannelLabelsPanel_Click(object sender, EventArgs e)
        {
            //From stack overflow... can cast the sender to get some info about what we clicked on!
            Label temp = (Label)sender;
            int selectedLogicalID = Convert.ToInt32(temp.Text);
            AnalogQuickEdit quick = new AnalogQuickEdit(selectedLogicalID);
            quick.ShowDialog();
            quick.Dispose();
            MainClientForm.instance.RefreshSequenceDataToUI();
            MainClientForm.instance.RefreshSettingsDataToUI();
        }
    }
}
