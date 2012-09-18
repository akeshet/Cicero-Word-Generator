using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataStructures;
namespace WordGenerator.Controls.Dialogs
{
    public partial class PulseManager : Form
    {
        private List<Pulse> availablePulses;
        private List<Pulse> usedPulses;


        public PulseManager()
        {
            InitializeComponent();
        }

        public PulseManager(List<Pulse> passAvailablePulses, List<Pulse> passUsedPulses)
        {
            InitializeComponent();

         

            //I think passAvailablePulses and passUsedPulses were passed by value, but fuck it let's just be safe.
            availablePulses = passAvailablePulses;
            usedPulses = passUsedPulses;

            foreach (Pulse pulse in usedPulses)
            {
                UsedPulseListBox.Items.Add(pulse);
                availablePulses.Remove(pulse);
            }

            foreach (Pulse pulse in availablePulses)
            {
                AvailablePulseListBox.Items.Add(pulse);
            }

           
            

        }


        private void addPulseToUsedList(object sender, EventArgs e)
        {
           
        }

        private void removePulseFromUsedList(object sender, EventArgs e)
        {
            
        }


        private void closeWindow(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
