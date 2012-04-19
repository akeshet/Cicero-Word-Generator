using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WordGenerator
{
    public partial class PropertyGridForm : Form
    {
        public PropertyGridForm()
        {
            InitializeComponent();
        }

        public PropertyGridForm(object o) : this()
        {
            this.propertyGrid1.SelectedObject = o;
        }
    }
}