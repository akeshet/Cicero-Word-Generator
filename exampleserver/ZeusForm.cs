using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using DataStructures;
using DatabaseHelper;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using HidLibrary;
using System.Linq;

namespace Zeus
{

    public partial class MainExampleServerlForm : Form
    {
        private ExampleServer server;
        private DatabaseHelper.DatabaseHelper dbhelper;
        public List<bool> heartbeatStatuses = new List<bool>();
        public List<Int64> heartbeatMemory = new List<Int64>();
        public List<string> lastBoundVarName = new List<string>();
        private HidDevice[] _deviceList;
        private HidDevice _selectedDevice;


        public MainExampleServerlForm() : this(new ExampleServerSettings())
        {

        }

        public MainExampleServerlForm(ExampleServerSettings settings)
        {
            InitializeComponent();
            if (File.Exists("./ZeusSettings.zsf"))
            {
                settings = DeSerializeObject<ExampleServerSettings>("./ZeusSettings.zsf");
                MessageBox.Show("Settings were loaded from the file 'ZeusSettings.zsf' in the directory with the Zeus executable.");
            }
            else
            {
                MessageBox.Show("Settings were not automatically loaded. Save or place a settings file named 'ZeusSettings.zsf' in the directory containing the Zeus executable in order to enable automatic loading of settings.");
            }

            this.server = new ExampleServer(this, settings);
            this.server.messageLog += addMessageLogText;
            if (!File.Exists("./ZeusSettings.zsf")) //(initialize heartbeats if no settings were autoloaded)
            {
                server.serverSettings.Heartbeats = new List<heartBeat>();
            }
            this.propertyGrid1.SelectedObject = server.serverSettings;
            if (server.serverSettings.Heartbeats != null)
            {
                foreach (heartBeat hb in server.serverSettings.Heartbeats)
                {
                    heartbeatStatuses.Add(false); //Add a list entry that defaults to false; The indices should also now be lined up
                    heartbeatMemory.Add(0); //Add 'old' hb values as zeros to initialize the list
                }
            }

            for (int i = 0; i < 20; i++)
            {
                lastBoundVarName.Add("N/A");
            }
            refreshList();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (server.serverSettings.DatabaseServerIP == null || server.serverSettings.Username == null || server.serverSettings.Password == null || server.serverSettings.DBName == null || server.serverSettings.MemcachedServerIP == null)
            {
                MessageBox.Show("You cannot start Zeus without fully specifying the database and memcached connections.");
            }

            else
            {
                connectButton.Enabled = false;
                server.openConnection();
                dbhelper = new DatabaseHelper.DatabaseHelper(server.serverSettings.MemcachedServerIP, server.serverSettings.DatabaseServerIP, server.serverSettings.Username, server.serverSettings.Password, server.serverSettings.DBName);
                timer1.Enabled = true;
                heartbeatTimer.Enabled = true;
            }
        }


        public void reenableConnectButton()
        {
            Action enableConnectButton = () => this.connectButton.Enabled = true;
            BeginInvoke(enableConnectButton);
        }



        public void addMessageLogText(object sender, MessageEvent e)
        {
            Action addText = () => this.textBox1.AppendText(e.MyTime.ToString() + " " + sender.ToString() + ": " + e.ToString() + "\r\n");
            BeginInvoke(addText);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void connectButton_Click_1(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            server.saveToDatabase = checkBox3.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            server.waitForZeus = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            server.humanOverride = checkBox2.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            //1: Update the variable table
            int rowIndex = variableTable.FirstDisplayedScrollingRowIndex;
            bool[] enableStates = dbhelper.getEnableStatesFromInputValuesTable();
            double[] varValues = dbhelper.getVariableValuesFromInputValuesTable();
            //Clear the table
            variableTable.Rows.Clear();
            //Populate the table
            for (int h = 0; h < 20; h++)
            {

                variableTable.Rows.Add(h + 1, enableStates[h], varValues[h], lastBoundVarName[h]);

            }
            //Deselect the first cell
            variableTable.CurrentCell = null;
            if (rowIndex >= 0) variableTable.FirstDisplayedScrollingRowIndex = rowIndex;
            //2: Update the images table
            rowIndex = imageList.FirstDisplayedScrollingRowIndex;
            List<int> imageIDs = new List<int>();
            List<string> timestamps = new List<string>();
            //Populate the lists
            int[] tempArray = dbhelper.getLastNImageIDs(20);
            for (int i = 0; i < 20; i++)
            {
                imageIDs.Add(tempArray[i]);
            }
            string[] tempArray2 = dbhelper.getLastNImageTimestamps(20);
            for (int i = 0; i < 20; i++)
            {
                timestamps.Add(tempArray2[i]);
            }


            //Clear the table
            imageList.Rows.Clear();
            //Populate the table
            for (int h = 0; h < 20; h++)
            {

                imageList.Rows.Add(imageIDs[h], timestamps[h]);

            }
            //Deselect the first cell
            imageList.CurrentCell = null;
            if (rowIndex >= 0) imageList.FirstDisplayedScrollingRowIndex = rowIndex;

            //3: Update hardware channel table
            hardwareTable.Rows.Clear();
            byte[] data = new byte[128];
            if (_selectedDevice != null)
            {
                _selectedDevice.ReadFeatureData(out data, 0);
            }
                //Populate the table
            foreach (HardwareChannelRule rule in server.serverSettings.Rules)
            {
                hardwareTable.Rows.Add(rule.ChannelName, rule.InputPin, data[rule.InputPin + 1]);
            }


            relockLabel.Text = server.responsesTried.ToString() + "/" + server.serverSettings.NumberOfRelocks.ToString();

        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Process input if the user clicked OK.
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String savepath = saveFileDialog1.FileName;
                SerializeObject(server.serverSettings, savepath);
            }
        }

        public void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
        }

        /// <summary>
        /// Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                string attributeXml = string.Empty;

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }

            return objectOut;
        }

        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Process input if the user clicked OK.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String loadpath = openFileDialog1.FileName;
                server.serverSettings = DeSerializeObject<ExampleServerSettings>(loadpath);
            }
            propertyGrid1.SelectedObject = server.serverSettings;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //1: First see if number of heartbeats in settings has just changed
            if (heartbeatStatuses.Count != server.serverSettings.Heartbeats.Count)
            {
                heartbeatStatuses.Clear();
                heartbeatMemory.Clear();

                foreach (heartBeat hb in server.serverSettings.Heartbeats)
                {
                    heartbeatStatuses.Add(false);
                    heartbeatMemory.Add(0);
                }
            }

            //2: Run the heartbeat checks

            for (int i = 0; i < heartbeatStatuses.Count; i++)
            {
                Int64 newhb = dbhelper.getHeartbeat(server.serverSettings.Heartbeats[i].name);
                heartbeatStatuses[i] = (newhb != heartbeatMemory[i]);
                heartbeatMemory[i] = newhb;
            }

            //3: Update heartbeat table
            heartbeatTable.Rows.Clear();
            variableTable.CurrentCell = null;
            for (int i = 0; i < heartbeatStatuses.Count; i++)
            {
                heartbeatTable.Rows.Add(server.serverSettings.Heartbeats[i].name, heartbeatStatuses[i], server.serverSettings.Heartbeats[i].group);
            }

            heartbeatTable.ClearSelection();

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            server.waitForVariableUpdates = checkBox4.Checked;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Begin functions related to USB peripeheral

        private void refreshList()
        {
            _deviceList = HidDevices.Enumerate(5824).ToArray();
            listBox1.DisplayMember = "Description";
            listBox1.DataSource = _deviceList;
            if (_deviceList.Length > 0)
            {
                _selectedDevice = _deviceList[0];
                listBox1.Enabled = true;
                connectToHID.Enabled = true;
            }
            else
            {
                listBox1.Enabled = false;
                connectToHID.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            refreshList();
        }

        private void connectToHID_Click(object sender, EventArgs e)
        {
            if ((_selectedDevice != null)) _selectedDevice.CloseDevice();
            _selectedDevice = _deviceList[listBox1.SelectedIndex];
            _selectedDevice.OpenDevice();
            _selectedDevice.MonitorDeviceEvents = true;
            _selectedDevice.Inserted += Device_Inserted;
            _selectedDevice.Removed += Device_Removed;
            button2.Enabled = false;
            connectToHID.Enabled = false;
            server.connectedHID = _selectedDevice;
            checkBox5.Enabled = true;
            checkBox6.Enabled = true;
            resetRelock.Enabled = true;
        }

        private void Device_Inserted()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(Device_Inserted));
                return;
            }
            addMessageLogText(this, new MessageEvent("USB Lab Monitor connected."));
            listBox1.Enabled = true;
        }

        private void Device_Removed()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(Device_Removed));
                return;
            }
            addMessageLogText(this, new MessageEvent("USB Lab Monitor disconnected."));
            listBox1.Enabled = false;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            server.checkHardwareChannels = checkBox5.Checked;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            server.useResponsePulses = checkBox6.Checked;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("USB Help: Input pins 0 through 5 are {D5,B0,B1,C0,C1,D4}. Output pulse pins 0 through 2 are {C2,C3,C4}.");
        }

        private void resetRelock_Click(object sender, EventArgs e)
        {
            server.responsesTried = 0;
        }

        private void MainExampleServerlForm_Load(object sender, EventArgs e)
        {

        }
    }
}