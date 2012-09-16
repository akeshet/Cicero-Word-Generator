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
            this.pulseEditorsFlowPanel.SuspendLayout();

            if (Storage.sequenceData == null || Storage.sequenceData.DigitalPulses == null)
                discardAndRefreshAllPulseEditors(); // the slow way
            else
            { // the fast way
                if (pulseEditors.Count < Storage.sequenceData.DigitalPulses.Count)
                {
                    int extras = Storage.sequenceData.DigitalPulses.Count - pulseEditors.Count;
                    for (int i = 0; i < extras; i++)
                    {
                        
                        pulseEditorsFlowPanel.Controls.Add(createAndRegisterPulseEditor(null));
                    }
                }
                else if (pulseEditors.Count > Storage.sequenceData.DigitalPulses.Count)
                {
                    int extras = pulseEditors.Count - Storage.sequenceData.DigitalPulses.Count;
                    for (int i = 0; i < extras; i++)
                    {
                        pulseEditorsFlowPanel.Controls.Remove(pulseEditors[0]);
                        pulseEditors[0].Dispose();
                        pulseEditors.RemoveAt(0);
                    }
                }

                for (int i = 0; i < pulseEditors.Count; i++)
                {
                    pulseEditors[i].setPulse(Storage.sequenceData.DigitalPulses[i]);
                }

                arrangePulseEditorLocations();
            }

            this.pulseEditorsFlowPanel.ResumeLayout();
        }

        private void arrangePulseEditorLocations()
        {
            for (int i = 0; i < pulseEditors.Count; i++)
            {
                pulseEditors[i].Location = new Point(pulseEditorPlaceholder.Location.X,
                    pulseEditorPlaceholder.Location.Y + i * (5 + pulseEditorPlaceholder.Height));
            }
        }

        private void discardAndRefreshAllPulseEditors()
        {
            foreach (PulseEditor pe in pulseEditors)
            {
                this.pulseEditorsFlowPanel.Controls.Remove(pe);
                pe.Dispose();
            }
            pulseEditors.Clear();

            this.pulseEditorsFlowPanel.ResumeLayout();
            this.pulseEditorsFlowPanel.SuspendLayout();

          
            foreach (Pulse pulse in Storage.sequenceData.DigitalPulses)
            {
                createAndRegisterPulseEditor(pulse);  
            }

            arrangePulseEditorLocations();

            pulseEditorsFlowPanel.Controls.AddRange(pulseEditors.ToArray());
        }

        private PulseEditor createAndRegisterPulseEditor(Pulse pulse)
        {
            PulseEditor pe = new PulseEditor(pulse);
            pulseEditors.Add(pe);
            pe.pulseDeleted += new EventHandler(pe_pulseDeleted);
            return pe;
        }

        void pe_pulseDeleted(object sender, EventArgs e)
        {
            if (sender is PulseEditor)
            {
                PulseEditor pe = sender as PulseEditor;
                pulseEditors.Remove(pe);
                pulseEditorsFlowPanel.Controls.Remove(pe);
                pe.Dispose();
            }
        }

        private void createPulse_Click(object sender, EventArgs e)
        {
            Storage.sequenceData.DigitalPulses.Add(new DataStructures.Pulse());
            this.layout();
        }

        private void cleanPulsesButton_Click(object sender, EventArgs e)
        {
            WordGenerator.MainClientForm.instance.cursorWait();
            try
            {
                bool replacedPulses = false;

            repeat:
                for (int i = 0; i < Storage.sequenceData.DigitalPulses.Count; i++)
                {
                    for (int j = i + 1; j < Storage.sequenceData.DigitalPulses.Count; j++)
                    {
                        Pulse a, b;
                        a = Storage.sequenceData.DigitalPulses[i];
                        b = Storage.sequenceData.DigitalPulses[j];
                        if (Pulse.Equivalent(a, b))
                        {
                            Storage.sequenceData.replacePulse(b, a);
                            replacedPulses = true;
                            goto repeat;            // YOU HAVE FOUND THE ONE AND ONLY "goto" statement in Cicero
                            // Congrats!
                            // Call Apogee and say Aardwolf.
                        }
                    }
                }

                if (replacedPulses)
                {
                    WordGenerator.MainClientForm.instance.RefreshSequenceDataToUI();
                }
            }
            finally
            {
                WordGenerator.MainClientForm.instance.cursorWaitRelease();
            }
        }

        public void openAutoNameGlossary(object sender, EventArgs e)
        {
           AutoNameGlossaryDialog autoNameBox = new AutoNameGlossaryDialog();
           autoNameBox.Show();
        }
    }
}
