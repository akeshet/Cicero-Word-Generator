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
    public partial class MainExampleServerlForm : Form
    {
        private ExampleServer server;


        public MainExampleServerlForm() : this(new ExampleServerSettings())
        {
        }

        public MainExampleServerlForm(ExampleServerSettings settings) {
            InitializeComponent();
            this.server = new ExampleServer(this, settings);
            this.server.messageLog += addMessageLogText;
            this.propertyGrid1.SelectedObject = settings;

        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            connectButton.Enabled = false;
            server.openConnection();
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


    }
}