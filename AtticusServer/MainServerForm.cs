using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DataStructures;
using System.Runtime.InteropServices;

namespace AtticusServer
{
    public partial class MainServerForm : Form
    {

        private bool displayError;

        public bool DisplayError
        {
            get { return displayError; }
            set
            {
                if (value != displayError)
                {
                    displayError = value;
                    updateDisplayError();
                }
            }
        }

        private void updateDisplayError()
        {
            Action displayErrorAction = () =>
            {
                if (displayError)
                {
                    eventLogTextBox.BackColor = Color.Red;
                }
                else
                {
                    eventLogTextBox.BackColor = this.BackColor;
                }
            };

            BeginInvoke(displayErrorAction);            
        }


        public static MainServerForm instance;

        int cursorWaitCount = 0;



        public void updateHardwareChannelCount()
        {
            this.HardwareChannelCount.Text = AtticusServer.server.MyHardwareChannels.Count + " Hardware Channels:\n";
            foreach (HardwareChannel.HardwareConstants.ChannelTypes channelType in HardwareChannel.HardwareConstants.allChannelTypes)
            {
                int count = 0;
                foreach (HardwareChannel hc in AtticusServer.server.MyHardwareChannels)
                {
                    if (hc.ChannelType == channelType)
                        count++;
                }
                this.HardwareChannelCount.Text += count + " " + channelType.ToString() + "\n";
            }


        }

        public void cursorWait()
        {
            if (cursorWaitCount == 0)
                Cursor.Current = Cursors.WaitCursor;
            cursorWaitCount++;
        }

        public void cursorWaitRelease()
        {
            cursorWaitCount--;
            if (cursorWaitCount <= 0)
                Cursor.Current = Cursors.Default;
            if (cursorWaitCount < 0)
                cursorWaitCount = 0;
        }


        public MainServerForm()
        {
            if (instance != null)
            {
                throw new Exception("Cannot create more than one instance of MainServerForm.");
            }
            instance = this;

            InitializeComponent();

            this.Text = "Atticus Server " + DataStructures.Information.VersionString;

            AtticusSplashForm splash = new AtticusSplashForm();
            splash.Show();

            this.serverNameTextBox.Text = AtticusServer.server.serverSettings.ServerName;
            this.listBox1.Items.Clear();

            foreach (DeviceSettings ds in AtticusServer.server.serverSettings.myDevicesSettings.Values)
            {
                this.listBox1.Items.Add(ds);
            }

            hcList.Items.Clear();
            foreach (HardwareChannel hc in AtticusServer.server.MyHardwareChannels)
            {
                hcList.Items.Add(hc);
            }

            serverSettingsPropertyGrid.SelectedObject = AtticusServer.server.serverSettings;

            updateHardwareChannelCount();

        }

        public void updateGUI(object sender, EventArgs e)
        {
            // This roundabout way of calling this.Invalidate is a thread safe way to have other threads call
            // form controls. I copied it from .net documentation.
            if (this.InvokeRequired)
            {
                EventHandler<MessageEvent> ev = new EventHandler<MessageEvent>(updateGUI);
                this.Invoke(ev, new object[] { sender, e });
            }
            else
            {
                this.Invalidate(true);
            }
        }


        public void addMessageLogText(object sender, MessageEvent e)
        {
            if (e.Verbosity > 0 && !this.softwareTimedTaskLogTextCheckbox.Checked)
                return;

            if (!formLoaded)
                return;

            if (showWarningsErrorsOnly.Checked && e.MessageType != MessageEvent.MessageTypes.Error && e.MessageType != MessageEvent.MessageTypes.Warning)
                return;

            /// This is a closure. Fancy functional makes the code cleaner.
            Action printMessage = () => eventLogTextBox.AppendText(e.MyTime.ToString() + " " + sender.ToString() + ": " + e.ToString() + "\r\n");
            BeginInvoke(printMessage);
            
        }

        

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (AtticusServer.server.CommunicatorStatus == ServerStructures.ServerCommunicatorStatus.Connecting) return;
            if (AtticusServer.server.CommunicatorStatus == ServerStructures.ServerCommunicatorStatus.Null) return;
            if (AtticusServer.server.CommunicatorStatus == ServerStructures.ServerCommunicatorStatus.Connected)
                AtticusServer.server.reachMarshalStatus(ServerStructures.ServerCommunicatorStatus.Disconnected);
            if (AtticusServer.server.CommunicatorStatus == ServerStructures.ServerCommunicatorStatus.Disconnected)
                AtticusServer.server.reachMarshalStatus(ServerStructures.ServerCommunicatorStatus.Connected);
        }

        private void connectionStatusLabel_Paint(object sender, PaintEventArgs e)
        {
            connectionStatusLabel.Text = AtticusServer.server.CommunicatorStatus.ToString();
        }

        private void connectButton_Paint(object sender, PaintEventArgs e)
        {
            if (AtticusServer.server.CommunicatorStatus == ServerStructures.ServerCommunicatorStatus.Connected) {
                connectButton.Text = "Disconnect";
                connectButton.Enabled = true;
            }
            else if (AtticusServer.server.CommunicatorStatus == ServerStructures.ServerCommunicatorStatus.Disconnected) {
                connectButton.Text = "Connect";
                connectButton.Enabled = true;
            }
            else {
                connectButton.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DaqmxBrowser browser = new DaqmxBrowser();
            browser.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refreshHardwareChannels();
        }

        private void refreshHardwareChannels()
        {
            cursorWait();
            AtticusServer.server.refreshHardwareLists();
            this.listBox1.Items.Clear();

            foreach (DeviceSettings ds in AtticusServer.server.serverSettings.myDevicesSettings.Values)
            {
                this.listBox1.Items.Add(ds);
            }

            deviceSettingsPropertyGrid.SelectedObject = null;

            hcList.Items.Clear();

            foreach (HardwareChannel hc in AtticusServer.server.MyHardwareChannels)
            {
                hcList.Items.Add(hc);
            }

            hcGrid.SelectedObject = null;

            updateHardwareChannelCount();

            cursorWaitRelease();
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            if (AtticusServer.server.CommunicatorStatus != ServerStructures.ServerCommunicatorStatus.Disconnected)
            {
                //button1.Enabled = false;
            }
            else
              refreshHardwareButton.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("This will permanently delete all your device settings. Are you sure you want to proceed?", "Just take it easy, man!", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                AtticusServer.server.serverSettings.myDevicesSettings.Clear();
                this.deviceSettingsPropertyGrid.SelectedObject = null;
                this.listBox1.Items.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HardwareChannelsBrowser browser = new HardwareChannelsBrowser();
            browser.ShowDialog();
        }

        private void serverNameTextBox_TextChanged(object sender, EventArgs e)
        {
            AtticusServer.server.serverSettings.ServerName = serverNameTextBox.Text;
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            this.deviceSettingsPropertyGrid.SelectedObject = listBox1.SelectedItem;
        }

        private bool formLoaded = false;
        private void MainServerForm_Load(object sender, EventArgs e)
        {

            formLoaded = true;
            // marshal the serverCommunicator if the start up settings say to do so.
            if (AtticusServer.server.serverSettings.ConnectOnStartup)
                AtticusServer.server.reachMarshalStatus(ServerStructures.ServerCommunicatorStatus.Connected);


            //Entry point to now-disabled mystery code.
            //See NiSync.cs for details.
            //NiSync.onLoad();
                
        }

        private void MainServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AtticusServer.server.shutDown();

            //Entry point to now-disabled myster code.
            //See NiSync.cs for details.
            //NiSync.onClose();
        }

        private void resetDevicesButtonClick(object sender, EventArgs e)
        {
            AtticusServer.server.resetAllDevices();
        }


        private void hcList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (hcList.SelectedItems.Count == 1)
            {
                hcGrid.SelectedObject = hcList.SelectedItem;
            }
            else
            {
                hcGrid.SelectedObject = null;
            }
        }

        private void excludeChannelsButtonClick(object sender, EventArgs e)
        {
            if (hcList.SelectedItems.Count > 0)
            {
                foreach (object obj in hcList.SelectedItems)
                {
                    if (obj is HardwareChannel)
                    {
                        AtticusServer.server.serverSettings.ExcludedChannels.Add((HardwareChannel)obj);
                    }
                }
                refreshHardwareChannels();
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Server settings cannot be loaded while server is running. To specify a settings file, replace the file " + FileNameStrings.DefaultServerSettingsDataFile + " in Atticus's home directory with the desired settings file.");
        }

        private void saveAsDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AtticusServer.saveServerSettings(AppDomain.CurrentDomain.BaseDirectory +  FileNameStrings.DefaultServerSettingsDataFile, AtticusServer.server.serverSettings);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();

            sf.DefaultExt = "set";

  

            sf.Filter =  " Settings files (*.set) |*.set|All files (*.*)|*.*";
            sf.FilterIndex = 1;
            sf.AddExtension = true;

            sf.Title = "Save settings";
            DialogResult dr = sf.ShowDialog();
            if (dr == DialogResult.OK)
            {
                AtticusServer.saveServerSettings(sf.FileName, AtticusServer.server.serverSettings);
            }

        }


        private void splashScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AtticusSplashForm spl = new AtticusSplashForm(false);
            spl.ShowDialog();
        }

        private void licenseInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataStructures.LicenseInfoForm form = new LicenseInfoForm();
            form.ShowDialog();
        }

        private void deleteSelectedDeviceButtonClick(object sender, EventArgs e)
        {
            if (AtticusServer.server.serverSettings.myDevicesSettings.ContainsValue(listBox1.SelectedItem as DeviceSettings)) {
                string str = "";
                foreach (string st in AtticusServer.server.serverSettings.myDevicesSettings.Keys)
                {
                    if (AtticusServer.server.serverSettings.myDevicesSettings[st] == listBox1.SelectedItem as DeviceSettings)
                    {
                        str = st;
                        break;
                    }
                }
                if (str != "")
                {
                    AtticusServer.server.serverSettings.myDevicesSettings.Remove(str);
                    listBox1.Items.Remove(listBox1.SelectedItem);
                }
            }
        }

        private void eventLogTextBox_Click(object sender, EventArgs e)
        {
            this.DisplayError = false;
        }

        private void openHomePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://akeshet.github.com/Cicero-Word-Generator/");
        }

        private void openGitRepositoryPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/akeshet/Cicero-Word-Generator");
        }

        private void MainServerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            formLoaded = false;
        }

        private void resetNetworkClocksButton_Click(object sender, EventArgs e)
        {
            AtticusServer.server.resetNetworkClocks();
        }

        private void showWarningsErrorsOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (showWarningsErrorsOnly.Checked)
            {
                softwareTimedTaskLogTextCheckbox.Checked = false;
                softwareTimedTaskLogTextCheckbox.Enabled = false;
            }
            else
            {
                softwareTimedTaskLogTextCheckbox.Enabled = true;
            }
        }




    }
}
