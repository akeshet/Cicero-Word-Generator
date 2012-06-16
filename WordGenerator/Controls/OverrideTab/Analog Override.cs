using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DataStructures;

namespace WordGenerator
{
    public partial class AnalogOverride : UserControl
    {
        private LogicalChannel channel;

        public AnalogOverride()
        {
            InitializeComponent();
        }

        public AnalogOverride(LogicalChannel channel, int channelID)
            : this()
        {
            this.channel = channel;
            this.checkBox1.Checked = channel.overridden;
            this.numericUpDown1.Value = (decimal) channel.analogOverrideValue;
            this.numericUpDown1.Enabled = this.checkBox1.Checked;
            toolTip1.SetToolTip(label1, channel.Description);
            label1.Text = channelID.ToString() + " " + channel.Name;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            channel.overridden = checkBox1.Checked;
            this.numericUpDown1.Enabled = checkBox1.Checked;
            WordGenerator.MainClientForm.instance.sequencePage.updateOverrideCount();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            channel.analogOverrideValue = (double) numericUpDown1.Value;
        }
    }
}
