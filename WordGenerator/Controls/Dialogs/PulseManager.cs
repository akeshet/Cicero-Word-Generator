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
        private BindingList<Pulse> availablePulses;
        private BindingList<Pulse> usedPulses;

        private List<Pulse> outputPulseList;
        public List<Pulse> OutputPulseList
        {
            get { return outputPulseList; }

        }


        public event PropertyChangedEventHandler PropertyChanged;

        public PulseManager()
        {
            InitializeComponent();
        }

        public PulseManager(List<Pulse> passAvailablePulses, List<Pulse> passUsedPulses)
        {
            InitializeComponent();


         

            
            availablePulses = new BindingList<Pulse>();
            usedPulses = new BindingList<Pulse>();

            availablePulses.AllowNew = true;
            availablePulses.AllowRemove = true;

            usedPulses.AllowNew = true;
            usedPulses.AllowRemove = true;


            //we need to copy the lists element by element so we don't ever point to passAvailablePulses or passUsedPulses,
            //because we could then screw these things up in the rest of the program
            foreach (Pulse p in passAvailablePulses)
            {
                availablePulses.Add(p);
            }
            foreach (Pulse p in passUsedPulses)
            {
                usedPulses.Add(p);
                availablePulses.Remove(p);
            }

      

        

            
            //UsedPulseListBox.DisplayMember = "PulseName";


            
            UsedPulseListBox.DataSource = usedPulses;
            AvailablePulseListBox.DataSource = availablePulses;



            //These lines aren't necessary--I think I'd need them if I was making a new object when adding,
            //but I'm not. I'm just adding an object from one list to the other.
          //  availablePulses.AddingNew += new AddingNewEventHandler (availablePulses_AddingNew);
          //  usedPulses.AddingNew += new AddingNewEventHandler (usedPulses_AddingNew);


            /*foreach (Pulse pulse in availablePulses)
            {
                AvailablePulseListBox.Items.Add(pulse);
            }
            */
                  
            

        }

       /* private void availablePulses_AddingNew(object sender, AddingNewEventArgs e)
        {
            Pulse toMove = AvailablePulseListBox.SelectedItem as Pulse;
            if (toMove != null)//this check should already be okay
            {
                e.NewObject = toMove;
            }
        }

        private void usedPulses_AddingNew(object sender, AddingNewEventArgs e)
        {
            
        }*/

        private void addPulseToUsedList(object sender, EventArgs e)
        {
            Pulse toMove = AvailablePulseListBox.SelectedItem as Pulse;
            if (toMove != null)
            {
                usedPulses.Add(toMove);
               
                availablePulses.Remove(toMove);
            }
           
        }

        private void removePulseFromUsedList(object sender, EventArgs e)
        {
            Pulse toMove = UsedPulseListBox.SelectedItem as Pulse;
            if (toMove != null)
            {
                availablePulses.Add(toMove);

                usedPulses.Remove(toMove);
            }
           
        }

        private void clearUsedList(object sender, EventArgs e)
        {
            foreach (Pulse p in usedPulses)
            {
                availablePulses.Add(p);
            }
            usedPulses.Clear();
        }


        private void closeWindow(object sender, EventArgs e)
        {
            //Prepare to return the used list of pulses
            outputPulseList = new List<Pulse>(usedPulses);
            this.Close();
        }

        private void PulseManager_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Font f = new Font("Arial",12.0f);
            Brush b = new SolidBrush(Color.Blue);
            Pen p = new Pen(Color.Red, 5);
            g.DrawLine(p, 20, 20, 200, 210);
            g.DrawLine(p, 20, 200, 210, 20);

            g.DrawString("Pulse Visualizer goes here \n (..clearly still in development)", f, b, 160, 100);

        }


       
    }
}
