using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataStructures;

namespace WordGenerator
{
    public partial class ServerManagerForm : Form
    {

   //     delegate void MessageEventHandler(object sender, MessageEvent message);

        delegate bool boolMessageLogDelegate (EventHandler<MessageEvent> messageLog) ;
        
   //     delegate void MessageEventCallDelegate(object sender, EventArgs message);

        public ServerManagerForm()
        {
            InitializeComponent();
            this.propertyGrid1.SelectedObject = Storage.settingsData.serverManager;
        }

        private static boolMessageLogDelegate connectDelegate;
        private static IAsyncResult connectAsyncResult;


        private void button1_Click(object sender, EventArgs e)
        {
            if (Storage.settingsData.serverManager.clearConnections())
            {
                // The following call is done async to prevent blocking of the UI thread while networking stuff happens.
                connectDelegate = new boolMessageLogDelegate(Storage.settingsData.serverManager.connectAllEnabledServer);
                AsyncCallback connectDoneCallback = new AsyncCallback(callBack);
                connectAsyncResult = connectDelegate.BeginInvoke(addMessageLogText, connectDoneCallback, null);
            }
            else
            {
                addMessageLogText(this, new MessageEvent("Unable to access server manager, as a network connection is currently being attempted."));
            }
        }

        public void callBack(IAsyncResult result)
        {
            if (this.InvokeRequired)
            {
                AsyncCallback cb = new AsyncCallback(callBack);
                this.Invoke(cb, new object[] { result });
            }
            else
            {
                this.propertyGrid1.Refresh();
            }
        }


        public void addMessageLogText(object sender, MessageEvent e)
        {
            WordGenerator.mainClientForm.instance.handleMessageEvent(sender, e);

            if (this.InvokeRequired)
            {
                EventHandler<MessageEvent> ev = new EventHandler<MessageEvent>(addMessageLogText);
                this.BeginInvoke(ev, new object[] { sender, e });
            }
            else
            {
                MessageEvent message = e as MessageEvent;
                if (message != null)
                {
                    textBox1.AppendText(message.MyTime.ToString() + " " + sender.ToString() + ": " + message.ToString() + "\r\n");
                }
                else
                {
                    textBox1.AppendText(sender.ToString() + ": " + e.ToString() + "\r\n");
                }
            }
        }
    }
}