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
            this.flowPanel1.SuspendLayout();
            foreach (PulseEditor pe in pulseEditors)
            {
                this.flowPanel1.Controls.Remove(pe);
                pe.Dispose();
            }
            pulseEditors.Clear();

            this.flowPanel1.ResumeLayout();
            this.flowPanel1.SuspendLayout();

            int i = 0;
            foreach (Pulse pulse in Storage.sequenceData.DigitalPulses)
            {
                PulseEditor pe = new PulseEditor(pulse);
                pe.Location = new Point( pulseEditorPlaceholder.Location.X,
                    pulseEditorPlaceholder.Location.Y + i * (5+pulseEditorPlaceholder.Height));
                pulseEditors.Add(pe);
                i++;
               
            }
            flowPanel1.Controls.AddRange(pulseEditors.ToArray());
            this.flowPanel1.ResumeLayout();
        }

        private void createPulse_Click(object sender, EventArgs e)
        {
            Storage.sequenceData.DigitalPulses.Add(new DataStructures.Pulse());
            this.layout();
        }

        private void cleanPulsesButton_Click(object sender, EventArgs e)
        {
            WordGenerator.mainClientForm.instance.cursorWait();

            bool replacedPulses = false;

            repeat:
            for (int i = 0; i < Storage.sequenceData.DigitalPulses.Count; i++)
            {
                for (int j = i+1; j < Storage.sequenceData.DigitalPulses.Count; j++)
                {
                    Pulse a, b;
                    a = Storage.sequenceData.DigitalPulses[i];
                    b = Storage.sequenceData.DigitalPulses[j];
                    if (Pulse.Equivalent(a, b))
                    {
                        Storage.sequenceData.replacePulse(b, a);
                        replacedPulses = true;
                        goto repeat;
                    }
                }
            }

            if (replacedPulses)
            {
                WordGenerator.mainClientForm.instance.RefreshSequenceDataToUI(Storage.sequenceData);
            }

            WordGenerator.mainClientForm.instance.cursorWaitRelease();
        }
    }
}
