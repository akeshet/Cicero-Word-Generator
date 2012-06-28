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
    public partial class WaveformGraphCollection : UserControl
    {
        private List<WaveformGraph> waveformGraphs;

        private WaveformEditor waveformEditor;

        /// <summary>
        /// if this bool is set to true, then whenever an update_Graph event gets raised, it is passed to all of the graphs.
        /// This is intended to be used in the common waveform window, where graphs can affect eachother.
        /// </summary>
        public bool updateAllGraphsEachTime = false;

        public WaveformGraphCollection()
        {
            InitializeComponent();
            this.AutoScroll = true;
            this.VScroll = true;
            waveformGraphs = new List<WaveformGraph>();
            waveformEditor = new WaveformEditor();
        }

        public void redrawAllGraphs()
        {
            try
            {
                foreach (WaveformGraph gr in waveformGraphs)
                {
                    gr.updateGraph(this, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Caught exception when attempting to redraw waveform collection " + ex.Message);
            }
        }

        public void setWaveformEditor(WaveformEditor waveformEditor)
        {
            this.waveformEditor = waveformEditor;
            foreach (WaveformGraph wg in waveformGraphs)
            {
                wg.setWaveformEditor(waveformEditor);
            }
        }

        public void deactivateAllGraphs()
        {
            this.SuspendLayout();
            foreach (WaveformGraph wg in waveformGraphs)
            {
                wg.Deactivate();
            }
            this.ResumeLayout();
        }

        public void setChannelNames(List<string> channelNames)
        {
            for (int i = 0; i < waveformGraphs.Count; i++)
            {
                waveformGraphs[i].setChannelName(channelNames[i]);
            }
        }

        public void setWaveforms(List<Waveform> waveforms)
        {
            this.setWaveforms(waveforms, null);
        }

        public void setWaveforms(List<Waveform> waveforms, List<bool> waveformsEditable) 
        {
            bool allEditable = false;
            if (waveformsEditable == null)
            {
                allEditable = true;
            }

            this.SuspendLayout();
            if (WordGenerator.MainClientForm.instance!=null)
                WordGenerator.MainClientForm.instance.cursorWait();
            try
            {
                List<WaveformGraph> graphsToAdd = new List<WaveformGraph>();

                if (waveformGraphs != null)
                {
                    foreach (WaveformGraph wg in waveformGraphs)
                    {
                        this.flowLayoutPanel1.Controls.Remove(wg);
                        wg.Dispose();
                    }
                }

                waveformGraphs.Clear();
                if (waveforms == null || waveforms.Count == 0)
                {
                    if (WordGenerator.MainClientForm.instance != null)
                        WordGenerator.MainClientForm.instance.cursorWaitRelease();
                    return;
                }

                for (int i = 0; i < waveforms.Count; i++)
                {
                    bool editable;
                    if (allEditable)
                    {
                        editable = true;
                    }
                    else
                    {
                        editable = waveformsEditable[i];
                    }

                    waveformGraphs.Add(new WaveformGraph(waveforms[i], waveformEditor, editable));
                    waveformGraphs[i].Deactivate();
                    waveformGraphs[i].Visible = true;
                    waveformGraphs[i].gotClicked += new EventHandler(WaveformGraphCollection_gotClicked);
                }

                this.flowLayoutPanel1.Controls.AddRange(waveformGraphs.ToArray());

                this.setWaveformEditor(waveformEditor);

                this.ResumeLayout();



                this.Refresh();
            }
            finally
            {
                if (WordGenerator.MainClientForm.instance!=null)
                    WordGenerator.MainClientForm.instance.cursorWaitRelease();
            }

        }

        void WaveformGraphCollection_gotClicked(object sender, EventArgs e)
        {
            if (WordGenerator.MainClientForm.instance != null)
                WordGenerator.MainClientForm.instance.cursorWait();
            try
            {
                WaveformGraph wg = sender as WaveformGraph;
                if (wg == null)
                {
                    WordGenerator.MainClientForm.instance.cursorWaitRelease();
                    return;
                }

                if (!waveformGraphs.Contains(wg))
                {
                    WordGenerator.MainClientForm.instance.cursorWaitRelease();
                    return;
                }

                foreach (WaveformGraph graph in waveformGraphs)
                    graph.Deactivate();

                wg.Activate();
                waveformEditor.clearUpdateGraphEventHandler();
                if (!updateAllGraphsEachTime)
                    waveformEditor.updateGraph += wg.updateGraph;
                else
                    foreach (WaveformGraph graph in waveformGraphs)
                        waveformEditor.updateGraph += graph.updateGraph;

                waveformEditor.setWaveform(wg.getWaveform());
            }
            finally
            {
                if (WordGenerator.MainClientForm.instance != null)
                    WordGenerator.MainClientForm.instance.cursorWaitRelease();
            }
        }




        private Point computeWaveformGraphLocation(int i)
        {
            int x = (i % 3) * 250;
            int y = (int)(i / 3) * 250;
            return new Point(x, y);
        }

    }
}
