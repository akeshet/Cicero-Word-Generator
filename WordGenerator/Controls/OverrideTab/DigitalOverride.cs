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
    public partial class DigitalOverride : UserControl
    {
        private LogicalChannel channel;

        public DigitalOverride()
        {
            InitializeComponent();
        }

        public DigitalOverride(LogicalChannel channel, int channelID) : this()
        {
            this.channel = channel;
            this.overrideCheck.Checked = channel.overridden;
            this.valueBox.Checked = channel.digitalOverrideValue;
            if (this.overrideCheck.Checked)
            {
                this.valueBox.Enabled = true;
            }
            else
            {
                this.valueBox.Enabled = false;
            }

            this.label1.Text =  channelID.ToString() + " " + channel.Name;
            toolTip1.SetToolTip(label1, channel.Description);
            if (channel.hotkeyChar != 0)
            {
                this.hotkeyLabel.Text = "{" + channel.hotkeyChar + "}";
            }

            if (channel.overrideHotkeyChar != 0)
            {
                this.overrideHotkeyLabel.Text = "{" + channel.overrideHotkeyChar + "}";
            }
        }

        private void overrideCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (channel != null)
            {
                channel.overridden = overrideCheck.Checked;
                if (overrideCheck.Checked)
                {
                    valueBox.Enabled = true;
                }
                else
                {
                    valueBox.Enabled = false;
                }
                updateOverride();
            }
        }

        private void valueBox_CheckedChanged(object sender, EventArgs e)
        {
            if (channel != null)
            {
                channel.digitalOverrideValue = valueBox.Checked;
                updateOverride();
            }
        }

        private void updateOverride()
        {
            WordGenerator.MainClientForm.instance.sequencePage.updateOverrideCount();

            if (WordGenerator.MainClientForm.instance != null)
            {
                if (WordGenerator.MainClientForm.instance.reDwell())
                {
                    WordGenerator.MainClientForm.instance.handleMessageEvent(this, new MessageEvent("Re-output current timestep due to change in override value."));
                }
            }
        }

        private void hotkeyTextbox_TextChanged(object sender, EventArgs e)
        {
            if (hotkeyTextbox.Text.Length>0)
            {
                char hotkeyChar = hotkeyTextbox.Text[0];
                if (char.IsLetter(hotkeyChar))
                {
                    setHotkeyChar(hotkeyChar);
                    contextMenuStrip1.Close();
                }
                else
                {
                    hotkeyTextbox.Text = "";
                }
            }
        }

         private void setHotkeyChar(char hChar)
        {
            if (hChar != 0)
            {
                foreach (LogicalChannel chan in  Storage.settingsData.logicalChannelManager.Digitals.Values)
                {
                    if ((channel != chan) && ((chan.hotkeyChar == hChar) || chan.overrideHotkeyChar == hChar))
                    {
                        MessageBox.Show("That hotkey is already in use.");
                        return;
                    }
                }

                if (channel.hotkeyChar != 0)
                    unRegsiterHotkey();
                channel.hotkeyChar = hChar;

                WordGenerator.MainClientForm.instance.RefreshSettingsDataToUI();
            }
        }


        private void setOverrideHotkeyChar(char hChar)
        {
            if (hChar != 0)
            {
                foreach (LogicalChannel chan in Storage.settingsData.logicalChannelManager.Digitals.Values)
                {
                    if ((channel != chan) && ((chan.hotkeyChar == hChar) || chan.overrideHotkeyChar == hChar))
                    {
                        MessageBox.Show("That hotkey is already in use.");
                        return;
                    }
                }

                if (channel.overrideHotkeyChar != 0)
                    unRegsiterHotkey();
                channel.overrideHotkeyChar = hChar;

                WordGenerator.MainClientForm.instance.RefreshSettingsDataToUI();
            }
        }



        private void clearHotkeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (channel.hotkeyChar != 0)
            {
                unRegsiterHotkey();
                channel.hotkeyChar = (char) 0;
                WordGenerator.MainClientForm.instance.RefreshSettingsDataToUI();
            }
        }


        public void unRegsiterHotkey()
        {
            if ( channel.hotkeyChar != 0)
            {
                WordGenerator.MainClientForm.instance.unregisterHotkey(valueBox);
            }

            if (channel.overrideHotkeyChar != 0)
            {
                WordGenerator.MainClientForm.instance.unregisterHotkey(overrideCheck);
            }
        }

        public void registerHotkey()
        {
            if (channel.hotkeyChar != 0)
            {
                WordGenerator.MainClientForm.instance.registerToggleHotkey(channel.hotkeyChar, valueBox);
            }

            if (channel.overrideHotkeyChar != 0)
            {
                WordGenerator.MainClientForm.instance.registerToggleHotkey(channel.overrideHotkeyChar, overrideCheck);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void overrideHotkeyTextbox_TextChanged(object sender, EventArgs e)
        {
            if (overrideHotkeyTextbox.Text.Length > 0)
            {
                char overrideHotkeyChar = overrideHotkeyTextbox.Text[0];
                if (char.IsLetter(overrideHotkeyChar))
                {
                    setOverrideHotkeyChar(overrideHotkeyChar);
                    contextMenuStrip1.Close();
                }
                else
                {
                    overrideHotkeyTextbox.Text = "";
                }
            }

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (channel.overrideHotkeyChar != 0)
            {
                unRegsiterHotkey();
                channel.overrideHotkeyChar = (char)0;
                WordGenerator.MainClientForm.instance.RefreshSettingsDataToUI();
            }
        }


    }
}
