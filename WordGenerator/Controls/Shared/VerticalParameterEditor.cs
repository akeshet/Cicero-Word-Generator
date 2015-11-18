using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataStructures;

namespace WordGenerator.Controls
{
    public partial class VerticalParameterEditor : WordGenerator.Controls.HorizontalParameterEditor
    {
        public VerticalParameterEditor()
        {
            InitializeComponent();
            this.convertToVertical();
        }

        public VerticalParameterEditor(DimensionedParameter parameter) : base(parameter)
        {
            this.convertToVertical();
        }

        private void variableComboBox1_Load(object sender, EventArgs e)
        {

        }

        private void variableComboBox1_Load_1(object sender, EventArgs e)
        {

        }

        private void variableComboBox1_Load_2(object sender, EventArgs e)
        {

        }
    }
}

