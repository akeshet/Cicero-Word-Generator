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
    public partial class WaveformGraph : UserControl
    {
        public Waveform waveform;
        private WaveformEditor waveformEditor;
        private string channelName;

        private static readonly int NumSamples = 1000;

        private bool editable;

        /// <summary>
        /// This event gets raised if the waveform graph gets clicked on. It is meant to be caught by the waveform collection.
        /// </summary>
        public event EventHandler gotClicked;

        public WaveformGraph()
        {
            InitializeComponent();
            Deactivate();
            updateGraph(this, null);
        }

        public WaveformGraph(Waveform waveform, WaveformEditor waveformEditor, bool waveformEditable) : this()
        {
            editable = waveformEditable;
            if (!editable)
            {
                this.BackColor = Color.Gray;
            }
            setWaveformEditor(waveformEditor);
            setWaveform(waveform);
        }

        public void Activate()
        {
            if (editable)
            {
                this.BackColor = Color.Olive;
            }
        }

        public void Deactivate()
        {
            if (editable)
            {
                this.BackColor = Color.Snow;
            }
        }


        public void setWaveform(Waveform waveform) {
            this.waveform = waveform;
            updateGraph(this, null);
        }

        public void setWaveformEditor(WaveformEditor waveformEditor)
        {
            this.waveformEditor = waveformEditor;
        }

        public void setChannelName(string channelName)
        {
            this.channelLabel.Text = channelName;
            this.channelName = channelName;
            this.Invalidate();
        }

        private void clearPlot()
        {
            waveformGraph1.PlotY(new double[] { 0 });
            waveformGraph1.PlotX(new double[] { 0 });
        }

        public void updateGraph(Object sender, EventArgs e)
        {
            if (waveform != null)
            {
                this.waveFormNameLabel.Text = waveform.WaveformName;
                this.channelLabel.Text = channelName;

                int nSamples = NumSamples;
                double[] yValues;
                try
                {
                    yValues = waveform.getInterpolation(nSamples, Storage.sequenceData.Variables);
                    double [] xValues = new double[nSamples];
                    double start = 0;
                    double stepSize = waveform.WaveformDuration.getBaseValue() / (double)nSamples;
                    for (int i = 0; i < nSamples; i++)
                    {
                        xValues[i] = start + stepSize * i;
                    }
                    double duration = waveform.WaveformDuration.getBaseValue();
                    double timePerPixel = duration / (double) nSamples;

                    waveformGraph1.PlotX(xValues, 0, timePerPixel);
                    waveformGraph1.PlotY(yValues, 0, timePerPixel);

                }
                catch (InterpolationException exception)
                {
                    // user changed something that caused an interpolation error. Let's try to undo it.
                    IParameterEditor ipe = sender as IParameterEditor;
                    if (ipe != null)
                    {
                        MessageBox.Show("Invalid data detected, attempting to undo.");
                        ipe.undoLastChange(this, null);
                        this.updateGraph(this, null);
                    }
                    else
                    {
                        MessageBox.Show("Invalid data, unable to undo. " + exception.Message);
                        clearPlot();
                        this.waveformGraph1.Enabled = false;
                    }
                }
            }
            else
            {
                waveformGraph1.Enabled = false;
                clearPlot();
                waveFormNameLabel.Text = "";
                channelLabel.Text = "";
            }
            this.Invalidate();
        }

        private void WaveformGraph_Click(object sender, EventArgs e)
        {
            if (editable)
            {
                if (gotClicked != null)
                    gotClicked(this, e);
            }
        }


    }
}
