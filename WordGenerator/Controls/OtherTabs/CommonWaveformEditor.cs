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
    public partial class CommonWaveformEditor : UserControl
    {

        private List<Waveform> commonWaveforms;

        public void setCommonWaveforms(List<Waveform> commonWaveforms) {
            this.commonWaveforms = commonWaveforms;
            this.waveformGraphCollection1.setWaveforms(commonWaveforms);
        }

        public CommonWaveformEditor()
        {
            InitializeComponent();
            this.waveformGraphCollection1.setWaveformEditor(this.waveformEditor1);
            this.waveformGraphCollection1.updateAllGraphsEachTime = true;

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            this.commonWaveforms.Add(new Waveform("Common " + commonWaveforms.Count));
            this.waveformEditor1.setWaveform(null);
            this.setCommonWaveforms(commonWaveforms);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Waveform current = this.waveformEditor1.getSelectedWaveform();
            Dictionary<Waveform, string> usedWFs = Storage.sequenceData.usedWaveforms();
            if (current != null)
            {
                if (usedWFs.ContainsKey(current))
                {
                    MessageBox.Show("This common waveform is in use: " + usedWFs[current]);
                    return;
                }
                Storage.sequenceData.CommonWaveforms.Remove(current);
                this.waveformEditor1.setWaveform(null);
                this.waveformGraphCollection1.setWaveforms(Storage.sequenceData.CommonWaveforms);
            }
        }
    }
}
