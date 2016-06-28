using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseHelper;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Int16[,] testIm = new Int16[1000,1000];
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();
        System.Diagnostics.Stopwatch st1 = new System.Diagnostics.Stopwatch();
      DatabaseHelper.DatabaseHelper dbHelper = new DatabaseHelper.DatabaseHelper("192.168.1.227","192.168.1.227","root","w0lfg4ng","BECVDatabase"); //make a DB connection
      
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
          //  imageStruct f = dbHelper.readImage();
          //  variableStruct df = dbHelper.readVariableValues();
          //  f = dbHelper.readImage();
           // df = dbHelper.readVariableValues();
            

        }

        private void button3_Click(object sender, EventArgs e)
        {


            dbHelper.readVariableValues();
           



           // ------ Code for exporting images for use in Julia -----
            /* 
            using (FileStream fs = new FileStream("atomData.dat", FileMode.CreateNew))
            {
                using (BinaryWriter w = new BinaryWriter(fs))
                {
                    for (int i = 0; i < bytesAtoms.Length; i++)
                    {
                        w.Write(bytesAtoms[i]);
                    }
                }
            }
            using (FileStream fs = new FileStream("noAtomData.dat", FileMode.CreateNew))
            {
                using (BinaryWriter w = new BinaryWriter(fs))
                {
                    for (int i = 0; i < bytesNoAtoms.Length; i++)
                    {
                        w.Write(bytesNoAtoms[i]);
                    }
                }
            }
            using (FileStream fs = new FileStream("darkData.dat", FileMode.CreateNew))
            {
                using (BinaryWriter w = new BinaryWriter(fs))
                {
                    for (int i = 0; i < bytesDark.Length; i++)
                    {
                        w.Write(bytesDark[i]);
                    }
                }
            }
             */
            //---------


            
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // dbHelper.readVariableValues(2);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           // dbHelper.readVariableValues(3);
        }
    }

}
