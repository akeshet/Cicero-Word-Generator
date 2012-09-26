using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WordGenerator.Controls.Dialogs
{
    public partial class PulseVisualizer : UserControl
    {
        private Graphics g;

        private int g_height;
        private int g_width;



        public PulseVisualizer()
        {
            InitializeComponent();
            g = this.CreateGraphics();
        
            g_height = this.Height;
            g_width = this.Width;


        }

    
       private void PaintMyself(object sender, PaintEventArgs e)
       {
           //Not sure if creating then disposing graphics everytime is a good idea, but it does fix the resize problem
           //namely, that resizing the PulseVisualizer form doesn't resize the graphics object.

           //Yes, now that I create a new graphics everytime, it doesn't really make sense for g to be a property of the class
           //I'll leave it as is for now, and make the code better later.
           g = this.CreateGraphics();
          
           DebugDraw(g);
           PulseGridDraw(g);
          
           g.Dispose();
           
       }


       private Rectangle CenterRect(int w, int h)
       {
           return new Rectangle((this.Width-w)/2,(this.Height-h)/2,w,h);
       }


       private void PulseGridDraw(Graphics gr)
       {
           Pen p = new Pen(Color.Black, 2);
           Pen dotted = new Pen(Color.Black, 2);
           dotted.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
           
           int box_width = 100;
           int box_height = 24;

           gr.DrawRectangle(p,new Rectangle((this.Width-box_width)/2,(this.Height-box_height)/2,box_width,box_height));
           gr.DrawRectangle(dotted, new Rectangle((this.Width-box_width)/2-box_width,(this.Height-box_height)/2,box_width,box_height));
           gr.DrawRectangle(dotted, new Rectangle((this.Width - box_width) / 2 + box_width, (this.Height - box_height) / 2, box_width, box_height));


       }


       private void DebugDraw(Graphics gr)
       {
           int circ_diam = 10;


           Font f = new Font("Courier", 10.0f);
           Brush b = new SolidBrush(Color.Maroon);
           Pen p = new Pen(Color.Maroon, 2);
           gr.DrawRectangle(p, 0, 0, this.Width, this.Height);

           Font fd = new Font("Arial", 7.0f);
           gr.DrawString("Box Width:" + this.Width + " Height:" + this.Height, fd, b, 10, 30);
           gr.DrawString("Prev Width:" + g_width + " Height:" + g_height, fd, b, 10, 50);


           gr.DrawString("(0,0)", f, b, 0, 0);
           gr.DrawEllipse(p, -circ_diam/2, -circ_diam/2, circ_diam, circ_diam);

           gr.DrawEllipse(p, (this.Width- circ_diam)/2, (this.Height - circ_diam)/2, circ_diam, circ_diam);
           gr.DrawString("(" + this.Width / 2 + "," + this.Height / 2 + ")", f, b, this.Width / 2, this.Height / 2);
           

       }

    }
}
