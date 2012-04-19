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
    public partial class OverridePage : UserControl
    {
        public OverridePage()
        {
            InitializeComponent();
        }

        private List<DigitalOverride> digitalOverrides;
        private List<AnalogOverride> analogOverrides;

        public void setSettings(SettingsData settings)
        {
            if (mainClientForm.instance != null)
                mainClientForm.instance.cursorWait();
            if (digitalOverrides == null)
            {
                digitalOverrides = new List<DigitalOverride>();
            }
            foreach (DigitalOverride dov in digitalOverrides)
            {
                digitalOverridePanel.Controls.Remove(dov);
                dov.unRegsiterHotkey();
                dov.Dispose();
            }
            digitalOverrides.Clear();

            List<int> digitalIDs = new List<int>(settings.logicalChannelManager.Digitals.Keys);
            digitalIDs.Sort();
            int counter = 0;
            foreach (int id in digitalIDs)
            {
                LogicalChannel chan = settings.logicalChannelManager.Digitals[id];
                DigitalOverride dov = new DigitalOverride(chan, id);
                dov.Location = new Point(digitalOverridePlaceholder.Location.X,
                                        digitalOverridePlaceholder.Location.Y + counter * (digitalOverridePlaceholder.Height - 4));
                digitalOverrides.Add(dov);
                dov.registerHotkey();
                dov.BackColor = Storage.settingsData.DigitalGridColors[counter % Storage.settingsData.DigitalGridColors.Count];
                counter++;
            }
            digitalOverridePanel.Controls.AddRange(digitalOverrides.ToArray());

            if (analogOverrides == null) {
                analogOverrides = new List<AnalogOverride>();
            }
            foreach (AnalogOverride ao in analogOverrides) {
                analogOverridePanel.Controls.Remove(ao);
                ao.Dispose();
            }
            analogOverrides.Clear();

            List<int> analogIDs = new List<int>(settings.logicalChannelManager.Analogs.Keys);
            analogIDs.Sort();
            counter = 0;
            foreach (int id in analogIDs)
            {
                LogicalChannel chan = settings.logicalChannelManager.Analogs[id];
                AnalogOverride ao = new AnalogOverride(chan, id);
                ao.Location = new Point(analogOverridePlaceholder.Location.X, analogOverridePlaceholder.Location.Y + counter * analogOverridePlaceholder.Height);
                analogOverrides.Add(ao);
                counter++;
            }
            analogOverridePanel.Controls.AddRange(analogOverrides.ToArray());

            if (mainClientForm.instance != null)
                mainClientForm.instance.cursorWaitRelease();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mainClientForm.instance.reDwell())
                mainClientForm.instance.handleMessageEvent(this, new MessageEvent("Re-output dwelling timestep."));
            else
                mainClientForm.instance.handleMessageEvent(this, new MessageEvent("Re-output of dwelling timestep failed."));
        }
    }
}
