using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataStructures;
using DataStructures.Timing;
using System.Runtime.InteropServices;

namespace SnippetServer
{
    public partial class MainForm : Form, DataStructures.Timing.SoftwareClockSubscriber
    {
       // public static AtticusServerCommunicator server;
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();


        private static SnippetServerCommunicator server;


        //Clock Providers
        private DataStructures.Timing.ComputerClockSoftwareClockProvider softwareClockProvider=null;
        private DataStructures.Timing.NetworkClockProvider networkClockProvider=null;
        private uint clockID;


       


        //Constructors
        public MainForm()
        {
            InitializeComponent();
            AllocConsole();

            //Initialize server communicator
            server = new SnippetServerCommunicator();

            //Initialize message events

            //I don't know if I need this...
           // server.registerMessageEventHandler(addMessageLogText);
            DataStructures.Timing.NetworkClockProvider.registerStaticMessageLogHandler(addMessageLogText);

            //Initialize network clock
            bool listenerCreated =
               DataStructures.Timing.NetworkClockProvider.startListener(DataStructures.Timing.NetworkClockEndpointInfo.HostTypes.Snippet_Server);
            if (!listenerCreated)
            {
                MessageBox.Show("Unable to start network clock listener. Is it possible that a separate Cicero instance is running on this computer?", "Unable to create clock listener.");
            }

            initializeNetworkClock();
        }

        private bool initializeNetworkClock()
        {
            Console.WriteLine("Initializing network clock...");
            Random rnd = new Random();
            clockID = (uint)rnd.Next();

            if (softwareClockProvider != null || networkClockProvider != null)
            {
               Console.WriteLine("A software clock provider already exists, unexpectedly. Aborting.");
                return false;
            }

           
            networkClockProvider = new NetworkClockProvider(clockID);
            networkClockProvider.addSubscriber(this, 41, 1);
            networkClockProvider.ArmClockProvider();
            networkClockProvider.StartClockProvider();
            Console.WriteLine("Network clock start succeeded.");
            return true;

        }


        private void connectButton_Click(object sender, EventArgs e)
        {
            if (server.CommunicatorStatus == ServerStructures.ServerCommunicatorStatus.Connecting) return;
            if (server.CommunicatorStatus == ServerStructures.ServerCommunicatorStatus.Null) return;
            if (server.CommunicatorStatus == ServerStructures.ServerCommunicatorStatus.Connected)
                server.reachMarshalStatus(ServerStructures.ServerCommunicatorStatus.Disconnected);
            if (server.CommunicatorStatus == ServerStructures.ServerCommunicatorStatus.Disconnected)
                server.reachMarshalStatus(ServerStructures.ServerCommunicatorStatus.Connected);
            //Console.WriteLine("test");
        }






        ///////////////////////////
        //////////SOFTWARECLOCK SUBSCRIBER interface implementation
        /////////////
        public bool reachedTime(uint time_ms, int priority)
        {
            Console.WriteLine("The time is:" + time_ms);
            return true;
        }

        /// <summary>
        /// Handle an exception that occured on the clock thread. 
        /// Return true if the exception was handled, otherwise return false
        /// and the exception will be re-thrown.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool handleExceptionOnClockThread(Exception e)
        {
            return true;
        }

        /// <summary>
        /// Will be possibly be called after there are no further calls
        /// to reachedTime, if the provider has reached its maximum output time limit
        /// </summary>
        /// <returns></returns>
        public bool providerTimerFinished(int priority)
        {
            return true;
        }



        ////////////////
        ///////MAKES MESSAGES
        ///////////////////
        public void addMessageLogText(object sender, MessageEvent e)
        {
           
            /// This is a closure. Fancy functional makes the code cleaner.
            if (networkClockProvider == null)
            {
                int s = 5;
            }
            Console.WriteLine(e.MyTime.ToString() + " " + sender.ToString() + ": " + e.ToString() + "\r\n");
            

        }
    }
}
