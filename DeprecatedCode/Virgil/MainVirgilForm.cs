using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataStructures;

namespace Virgil
{
    public partial class MainVirgilForm : Form
    {
        private VirgilServer server;


        public MainVirgilForm() : this(new VirgilServerSettings())
        {
        }

        public MainVirgilForm(VirgilServerSettings settings) {
            InitializeComponent();
            this.server = new VirgilServer(this, settings);
            this.server.messageLog += addMessageLogText;
            this.propertyGrid1.SelectedObject = settings;

            try
            {
                VirgilH5Exporter.testForHDFLibrary();
            }
            catch (Exception e)
            {
                MessageBox.Show("You do not appear to have a working HDF5 installation. For detailed information, re-run Virgil from a Console");
                System.Console.WriteLine("Exception when testing for HDF library: " );
                System.Console.WriteLine(e.Message + e.StackTrace);
                System.Console.WriteLine("If this is a FileNotFound exception, this may indicate that you are missing hdf5dll.dll, szlibdll.dll, or some other dependency of HDF5DotNet.dll. Use Depends.exe to explore the dependencies of HDF5DotNet.dll");
            }

        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            connectButton.Enabled = false;
            server.openConnection();


        }

        public delegate void voidVoidDelegate();

        public void reenableConnectButton()
        {
            if (this.InvokeRequired)
            {

                this.BeginInvoke(new voidVoidDelegate(reenableConnectButton));
            }
            else
                this.connectButton.Enabled = true;
        }



        public delegate void MessageEventCallDelegate(object sender, MessageEvent e);
        public void addMessageLogText(object sender, MessageEvent e)
        {
            if (this.InvokeRequired)
            {
                MessageEventCallDelegate ev = new MessageEventCallDelegate(addMessageLogText);
                this.BeginInvoke(ev, new object[] { sender, e });
            }
            else
            {
                this.textBox1.AppendText(e.myTime.ToString() + " " + sender.ToString() + ": " + e.ToString() + "\r\n");
            }
        }


    }
}