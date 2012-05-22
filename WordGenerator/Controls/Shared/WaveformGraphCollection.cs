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
        private List<WaveformGraph2> waveformGraphs;

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
            waveformGraphs = new List<WaveformGraph2>();
            waveformEditor = new WaveformEditor();
        }

        public void redrawAllGraphs()
        {
            try
            {
                foreach (WaveformGraph2 gr in waveformGraphs)
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
            foreach (WaveformGraph2 wg in waveformGraphs)
            {
                wg.setWaveformEditor(waveformEditor);
            }
        }

        public void deactivateAllGraphs()
        {
            this.SuspendLayout();
            foreach (WaveformGraph2 wg in waveformGraphs)
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
            if (WordGenerator.mainClientForm.instance!=null)
                WordGenerator.mainClientForm.instance.cursorWait();

            List<WaveformGraph2> graphsToAdd = new List<WaveformGraph2>();

            if (waveformGraphs != null)
            {
                foreach (WaveformGraph2 wg in waveformGraphs)
                {
                    this.flowLayoutPanel1.Controls.Remove(wg);
                    wg.Dispose();
                }
            }

            waveformGraphs.Clear();
            if (waveforms == null || waveforms.Count == 0)
            {
                if (WordGenerator.mainClientForm.instance!=null)
                    WordGenerator.mainClientForm.instance.cursorWaitRelease();
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

                waveformGraphs.Add(new WaveformGraph2(waveforms[i], waveformEditor, editable));            
                waveformGraphs[i].Deactivate();
                waveformGraphs[i].Visible = true;
                waveformGraphs[i].gotClicked += new EventHandler(WaveformGraphCollection_gotClicked);
            }

            this.flowLayoutPanel1.Controls.AddRange(waveformGraphs.ToArray());

            this.setWaveformEditor(waveformEditor);

           this.ResumeLayout();

     
            
            this.Refresh();

            WordGenerator.mainClientForm.instance.cursorWaitRelease();

        }

        void WaveformGraphCollection_gotClicked(object sender, EventArgs e)
        {
            if (WordGenerator.mainClientForm.instance != null)
                WordGenerator.mainClientForm.instance.cursorWait();

            WaveformGraph2 wg = sender as WaveformGraph2;
            if (wg == null)
            {
                WordGenerator.mainClientForm.instance.cursorWaitRelease();
                return;
            }

            if (!waveformGraphs.Contains(wg))
            {
                WordGenerator.mainClientForm.instance.cursorWaitRelease();
                return;
            }

            foreach (WaveformGraph2 graph in waveformGraphs)
                graph.Deactivate();

            wg.Activate();
            waveformEditor.clearUpdateGraphEventHandler();
            if (!updateAllGraphsEachTime)
                waveformEditor.updateGraph += wg.updateGraph;
            else
                foreach (WaveformGraph2 graph in waveformGraphs)
                    waveformEditor.updateGraph += graph.updateGraph;

            waveformEditor.setWaveform(wg.getWaveform());

            if (WordGenerator.mainClientForm.instance != null)
                WordGenerator.mainClientForm.instance.cursorWaitRelease();


        }




        private Point computeWaveformGraphLocation(int i)
        {
            int x = (i % 3) * 250;
            int y = (int)(i / 3) * 250;
            return new Point(x, y);
        }

    }
}
