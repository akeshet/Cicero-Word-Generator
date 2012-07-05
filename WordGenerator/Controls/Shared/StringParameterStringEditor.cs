using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DataStructures;

namespace WordGenerator.Controls
{
    public partial class StringParameterStringEditor : UserControl
    {
        private StringParameterString sps;


        public event Action<StringParameterString> insertBelow;
        public event Action<StringParameterString> insertAbove;
        public event Action<StringParameterString> delete;

        public StringParameterStringEditor()
        {
            InitializeComponent();

            insBef.Click += new EventHandler(insBef_Click);
            insAft.Click += new EventHandler(insAft_Click);
            del.Click += new EventHandler(del_Click);

            layout();
        }

        void del_Click(object sender, EventArgs e)
        {
            if (delete != null)
            {
                delete(sps);
            }
        }

        void insAft_Click(object sender, EventArgs e)
        {
            if (insertBelow != null)
            {
                insertBelow(sps);
            }
        }

        void insBef_Click(object sender, EventArgs e)
        {
            if (insertAbove != null)
            {
                insertAbove(sps);
            }
        }

        public StringParameterStringEditor(StringParameterString sps) : this()
        {
            this.sps = sps;
            this.prefixTextbox.Text = sps.Prefix;
            this.postfixTextbox.Text = sps.Postfix;
            this.horizontalParameterEditor1.setParameterData(this.sps.Parameter);
            layout();
        }

        public void layout()
        {
            if (sps == null)
            {
                this.prefixTextbox.Enabled = false;
                this.postfixTextbox.Enabled = false;
                this.horizontalParameterEditor1.Enabled = false;
            }
            else
            {
                this.prefixTextbox.Enabled = true;
                this.postfixTextbox.Enabled = true;
                this.horizontalParameterEditor1.Enabled = true;
            }
        }

        private void prefixTextbox_TextChanged(object sender, EventArgs e)
        {
            if (sps != null)
            {
                sps.Prefix = prefixTextbox.Text;
            }
        }

        private void postfixTextbox_TextChanged(object sender, EventArgs e)
        {
            if (sps != null)
            {
                sps.Postfix = postfixTextbox.Text;
            }
        }
    }
}
