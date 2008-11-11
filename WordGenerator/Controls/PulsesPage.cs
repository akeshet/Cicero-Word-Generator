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
    public partial class PulsesPage : UserControl
    {
        private List<PulseEditor> pulseEditors;

        public PulsesPage()
        {
            InitializeComponent();
            pulseEditors = new List<PulseEditor>();
        }

        public void layout()
        {
            this.pulseEditorPanel.SuspendLayout();
            foreach (PulseEditor pe in pulseEditors)
            {
                this.pulseEditorPanel.Controls.Remove(pe);
                pe.Dispose();
            }
            pulseEditors.Clear();

            this.pulseEditorPanel.ResumeLayout();
            this.pulseEditorPanel.SuspendLayout();

            int i = 0;
            foreach (Pulse pulse in Storage.sequenceData.DigitalPulses)
            {
                PulseEditor pe = new PulseEditor(pulse);
                pe.Location = new Point( pulseEditorPlaceholder.Location.X,
                    pulseEditorPlaceholder.Location.Y + i * (5+pulseEditorPlaceholder.Height));
                pulseEditors.Add(pe);
                i++;
               
            }
            pulseEditorPanel.Controls.AddRange(pulseEditors.ToArray());
            this.pulseEditorPanel.ResumeLayout();
        }

        private void createPulse_Click(object sender, EventArgs e)
        {
            Storage.sequenceData.DigitalPulses.Add(new DataStructures.Pulse());
            this.layout();
        }
    }
}
