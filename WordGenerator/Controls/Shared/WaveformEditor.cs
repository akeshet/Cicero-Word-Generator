using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DataStructures;

namespace WordGenerator.Controls
{
    public partial class WaveformEditor : UserControl
    {
        private Waveform currentWaveform;

        public Waveform CurrentWaveform
        {
            get { return currentWaveform; }
        }

        private Dictionary<ComboBox, bool> combinationBoxIsAWaveform;

        public event EventHandler copyDuration;



        public Waveform getSelectedWaveform()
        {
            return currentWaveform;
        }

        private List<HorizontalParameterEditor> specializedParameters;
        private List<Label> specializedParameterLabels;

        private List<ComboBox> combinationParameters;

        private List<HorizontalParameterEditor> xyParameters;
        private List<Label> xyParameterLabels;

        private List<Variable> variables;

        private static readonly int y_parameter_spacing = 25;


        //REO 10/2008
        //how much the Open File Container box expands when "load from file" is selected
        private static readonly Size fileControlBoxOpenDelta = new Size(0, 27);
        //this is how much the file container box is actually expanded
        private int fileControlOffsetY = 0;
        //here we keep track of the state of the file container box
        private bool fileControlBoxOpen = false;

        /// <summary>
        /// This event gets raised whenever the waveform wants its associated graph to be updated.
        /// </summary>
        public event EventHandler updateGraph;

        public void updateGUI(Object sender, EventArgs e)
        {
            if (updateGraph != null)
                updateGraph(sender, e);
            this.Invalidate();
        }

        public void clearUpdateGraphEventHandler()
        {
            updateGraph = null;
        }

        public WaveformEditor()
        {
            InitializeComponent();
            foreach (Waveform.InterpolationType type in Waveform.InterpolationType.allInterpolationTypes)
            {
                interpolationTypeComboBox.Items.Add(type);
            }
            setWaveform(null);

            durationParameterEditor.setMinimumAllowableManualValue(0);

            toolTip1.SetToolTip(scaleButton, "Re-scales all of the time values so that the largest corresponds to the end of the waveform (as specified by the waveform duration).");
        }


        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (currentWaveform != null)
                    currentWaveform.WaveformName = nameTextBox.Text;
                updateGUI(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Caught exception when trying to change waveform name: " + ex.Message);
            }
        }



        public void setWaveform(Waveform waveform)
        {
            this.currentWaveform = waveform;
            layoutNewWaveform();

        }

        private void layoutNewWaveform()
        {
            if (currentWaveform != null)
            {
                this.Enabled = true;


                interpolationTypeComboBox.SelectedItem = currentWaveform.interpolationType;


                nameTextBox.Text = currentWaveform.WaveformName;
                durationParameterEditor.setParameterData(currentWaveform.WaveformDuration);

            }
            else
            {
                nameTextBox.Text = "";
                this.Enabled = false;
                interpolationTypeComboBox.SelectedIndex = -1;
            }

            layOutParameters();
        }

        public void layOutParameters()
        {

            this.SuspendLayout();

            equationStatusLabel.Enabled = false;
            equationStatusLabel.Visible = false;

            equationTextBox.Enabled = false;
            equationTextBox.Visible = false;
            if (currentWaveform != null)
            {
                equationTextBox.Text = currentWaveform.EquationString;
            }

            equationHelpText.Visible = false;

            upButton.Enabled = false;
            upButton.Visible = false;

            downButton.Visible = false;
            downButton.Enabled = false;

            sortButton.Visible = false;
            scaleButton.Visible = false;


            // REO 10/2008
            // first, remove the old stuff
            disableLoadFileControls();
            hideXYLabels();

            if (specializedParameters != null)
            {
                foreach (HorizontalParameterEditor ed in specializedParameters)
                {
                    this.specialParametersBox.Controls.Remove(ed);
                    ed.Dispose();
                }
                foreach (Label lab in specializedParameterLabels)
                {
                    this.specialParametersBox.Controls.Remove(lab);
                    lab.Dispose();
                }
            }
            specializedParameters = null;
            specializedParameterLabels = null;


            if (xyParameters != null)
            {
                foreach (HorizontalParameterEditor ed in xyParameters)
                {
                    this.Controls.Remove(ed);
                    ed.Dispose();
                }
                foreach (Label lab in xyParameterLabels)
                {
                    this.Controls.Remove(lab);
                    lab.Dispose();
                }
            }
            xyParameterLabels = null;
            xyParameters = null;


            if (combinationParameters != null)
            {
                foreach (ComboBox box in combinationParameters)
                {
                    this.Controls.Remove(box);
                    box.Dispose();
                }
            }

            combinationParameters = null;

            if (currentWaveform != null)
            {

                if (currentWaveform.interpolationType.xyParametersEnabled)
                {
                    if (currentWaveform.interpolationType.xyParametersFixed)
                    {
                        sortButton.Visible = false;
                        scaleButton.Visible = false;
                    }
                    else
                    {
                        sortButton.Visible = true;
                        scaleButton.Visible = true;
                    }
                }

                // lay out specialized param editors

                if (currentWaveform.interpolationType.extraParametersEnabled)
                {
                    specializedParameters = new List<HorizontalParameterEditor>();
                    specializedParameterLabels = new List<Label>();

                    for (int i = 0; i < currentWaveform.interpolationType.extraParametersCount; i++)
                    {
                        HorizontalParameterEditor hpe = new HorizontalParameterEditor(
                            currentWaveform.ExtraParameters[i]);

                        hpe.updateGUI += this.updateGUI;

                        hpe.Location = new Point(specialParametersStartPoint.Location.X,
                            specialParametersStartPoint.Location.Y + i * y_parameter_spacing);
                        hpe.Visible = true;
                        hpe.Enabled = true;
                        hpe.Size = specialParametersStartPoint.Size;

                        specializedParameters.Add(hpe);

                        Label lab = new Label();
                        lab.Text = currentWaveform.interpolationType.extraParametersNames[i];
                        lab.Location = new Point(specializedLabelStart.Location.X,
                            specializedLabelStart.Location.Y + i * y_parameter_spacing);
                        lab.Visible = true;
                        lab.Enabled = true;

                        specializedParameterLabels.Add(lab);

                    }

                    specialParametersBox.Controls.AddRange(specializedParameters.ToArray());
                    specialParametersBox.Controls.AddRange(specializedParameterLabels.ToArray());
                }

                // REO 10/2008
                // Now lay out load file box
                if (currentWaveform.interpolationType.canReadDataFromFile)
                {
                    enableLoadFileControls();
                }

                // Now lay out XY parameters.


                if (currentWaveform.interpolationType.xyParametersEnabled)
                {
                    xyParameters = new List<HorizontalParameterEditor>();
                    xyParameterLabels = new List<Label>();


                    if (currentWaveform.interpolationType.xyParametersFixed)
                    {
                        for (int i = 0; i < currentWaveform.interpolationType.xyParametersCount; i++)
                        {
                            addXYParameterEditor(i);
                        }

                    }
                    else
                    {
                        for (int i = 0; i < currentWaveform.XValues.Count; i++)
                        {
                            addXYParameterEditor(i);
                        }

                        enableAndPositionUpDownButtons();
                    }


                    if (xyParameters.Count != 0)
                    {
                        showXYLabels();
                    }


                    this.Controls.AddRange(xyParameters.ToArray());
                    this.Controls.AddRange(xyParameterLabels.ToArray());
                    //    foreach (HorizontalParameterEditor hpe in xyParameters)
                    //       this.Controls.Add(hpe);
                    //   foreach (Label lab in xyParameterLabels)
                    //       this.Controls.Add(lab);    
                }


                // REO 10/2008: added +fileControlOffsetY to position around file load box
                // Now layout waveform combination parameters


                if (currentWaveform.interpolationType.referencesOtherWaveforms)
                {
                    combinationParameters = new List<ComboBox>();
                    combinationBoxIsAWaveform = new Dictionary<ComboBox, bool>();


                    for (int i = 0; i < currentWaveform.ReferencedWaveforms.Count; i++)
                    {
                        ComboBox box = new ComboBox();
                        combinationBoxIsAWaveform.Add(box, true);
                        box.Size = waveformCombosStart.Size;
                        box.Location = new Point(waveformCombosStart.Location.X,
                            waveformCombosStart.Location.Y
                            + 2 * i * y_parameter_spacing
                            + fileControlOffsetY);

                        foreach (Waveform wf in Storage.sequenceData.CommonWaveforms)
                        {
                            if (wf.ToString() == null)
                                wf.WaveformName = "Name Required";
                            box.Items.Add(wf);
                        }

                        box.DropDownStyle = ComboBoxStyle.DropDownList;
                        box.Name = i.ToString();
                        box.SelectedIndexChanged += combinerComboBoxValueChanged;

                        box.SelectedItem = currentWaveform.ReferencedWaveforms[i];

                        combinationParameters.Add(box);

                    }

                    for (int i = 0; i < currentWaveform.WaveformCombiners.Count; i++)
                    {
                        ComboBox box = new ComboBox();
                        combinationBoxIsAWaveform.Add(box, false);
                        box.Size = waveformCombosStart.Size;
                        box.Location = new Point(waveformCombosStart.Location.X,
                            waveformCombosStart.Location.Y
                            + (2 * i + 1) * y_parameter_spacing
                            + fileControlOffsetY);

                        box.BeginUpdate();
                        foreach (Waveform.InterpolationType.CombinationOperators oper in Waveform.InterpolationType.allCombinationOperators)
                        {
                            box.Items.Add(oper);
                        }
                        box.EndUpdate();


                        box.DropDownStyle = ComboBoxStyle.DropDownList;
                        box.Name = i.ToString();
                        box.SelectedIndexChanged += combinerComboBoxValueChanged;

                        box.SelectedItem = currentWaveform.WaveformCombiners[i];
                        combinationParameters.Add(box);

                    }

                    this.Controls.AddRange(combinationParameters.ToArray());

                    //  foreach (ComboBox box in combinationParameters)
                    //      this.Controls.Add(box);

                    enableAndPositionUpDownButtons();
                }
            }
            if (currentWaveform != null)
            {
                if (currentWaveform.interpolationType.equationParameterEnabled)
                {
                    equationTextBox.Visible = true;
                    equationTextBox.Enabled = true;
                    equationStatusLabel.Visible = true;
                    equationStatusLabel.Enabled = true;

                    equationHelpText.Visible = true;

                    updateEquationStatusLabel();
                }
            }


            this.ResumeLayout();
        }

        private void hideXYLabels()
        {
            XLabel.Visible = false;
            YLabel.Visible = false;
        }

        // REO 10/2008: added +fileControlOffsetY to position around file load box
        private void showXYLabels()
        {
            XLabel.Location = new Point(XLabel1.Location.X,
                XLabel1.Location.Y + fileControlOffsetY);
            XLabel.Visible = true;
            YLabel.Location = new Point(YLabel1.Location.X,
                YLabel1.Location.Y + fileControlOffsetY);
            YLabel.Visible = true;
        }

        /// <summary>
        /// REO 10/2008
        /// </summary>
        private void enableLoadFileControls()
        {
            fileLoadCheckBox.Enabled = true;
            fileLoadCheckBox.Checked = currentWaveform.DataFromFile;

            if (currentWaveform.DataFromFile)
            {
                showLoadFileControls();
            }
        }

        //REO 10/2008
        private void showLoadFileControls()
        {
            if (!fileControlBoxOpen)
            {
                fileLoadGroupBox.Size += fileControlBoxOpenDelta;
                fileControlBoxOpen = true;
                fileControlOffsetY += fileControlBoxOpenDelta.Height;
            }
            fileBrowseButton.Visible = true;
            fileBrowseButton.Enabled = true;
            fileLoadButton.Visible = true;
            fileLoadButton.Enabled = true;
            filePathTextBox.Enabled = true;
            filePathTextBox.Visible = true;
            filePathTextBox.Text = currentWaveform.DataFileName;

        }

        //REO 10/2008
        private void disableLoadFileControls()
        {
            hideLoadFileControls();
            fileLoadCheckBox.Enabled = false;
            fileLoadCheckBox.Checked = false;
        }

        //REO 10/2008
        private void hideLoadFileControls()
        {
            if (this.fileControlBoxOpen)
            {
                fileLoadGroupBox.Size -= fileControlBoxOpenDelta;
                fileControlBoxOpen = false;
                fileControlOffsetY -= fileControlBoxOpenDelta.Height;
            }
            fileBrowseButton.Visible = false;
            fileBrowseButton.Enabled = false;
            fileLoadButton.Visible = false;
            fileLoadButton.Enabled = false;
            filePathTextBox.Enabled = false;
            filePathTextBox.Visible = false;
            filePathTextBox.Text = null;
        }

        void combinerComboBoxValueChanged(object sender, EventArgs e)
        {
            ComboBox box = sender as ComboBox;
            if (box == null) return;

            int boxNumber = Int32.Parse(box.Name);

            // figure out which box was changed based on the selected object's type, and the combo box's name.
            Object selectedItem = box.SelectedItem;

            Waveform wf = selectedItem as Waveform;


            // Determine if the changed box was a waveform box or an operator box.
            if (combinationBoxIsAWaveform[box])
            {
                // so it was a waveform selecting box.

                currentWaveform.ReferencedWaveforms[boxNumber] = wf;
                if (currentWaveform.isWaveformReferenceRecursive())
                {
                    MessageBox.Show("Unable to have this reference reference that one, as it would result in a recursive reference loop.");
                    currentWaveform.ReferencedWaveforms[boxNumber] = null;
                    box.SelectedItem = null;
                }
            }

            else
            {
                // so it was a combination operator selecting box
                Waveform.InterpolationType.CombinationOperators oper = (Waveform.InterpolationType.CombinationOperators)box.SelectedIndex;

                currentWaveform.WaveformCombiners[boxNumber] = oper;

            }

            updateGraph(sender, e);

        }


        // REO 10/2008: added +fileControlOffsetY to position around file load box
        private void enableAndPositionUpDownButtons()
        {
            if (!fileLoadCheckBox.Checked)
            {
                upButton.Enabled = true;
                downButton.Enabled = true;
            }
            
            upButton.Visible = true;
            downButton.Visible = true;

            sortButton.Visible = true;
            scaleButton.Visible = true;

            int buttonTop;

            if (currentWaveform.interpolationType.referencesOtherWaveforms)
            {
                buttonTop = combinationParameters[combinationParameters.Count - 1].Location.Y + 2 * y_parameter_spacing;
            }
            else
            {

                if (xyParameters.Count > 0)
                    buttonTop = xyParameters[xyParameters.Count - 1].Location.Y + y_parameter_spacing;
                else buttonTop = XLabel.Location.Y + fileControlOffsetY;
            }

            upButton.Location = new Point(upButton.Location.X, buttonTop);
            downButton.Location = new Point(downButton.Location.X, buttonTop);

            sortButton.Location = new Point(sortButton.Location.X, buttonTop);
            scaleButton.Location = new Point(scaleButton.Location.X, buttonTop);


        }

        public void setVariables(List<Variable> variables)
        {
            this.variables = variables;
        }

        // REO 10/2008: added +fileControlOffsetY to position around file load box
        private void addXYParameterEditor(int i)
        {
            HorizontalParameterEditor hpex = new HorizontalParameterEditor();
            hpex.setParameterData(currentWaveform.XValues[i]);
            HorizontalParameterEditor hpey = new HorizontalParameterEditor();
            hpey.setParameterData(currentWaveform.YValues[i]);

            hpex.Location = new Point(0, this.XYParametersStart1.Location.Y
                + i * y_parameter_spacing + fileControlOffsetY);
            hpey.Location = new Point(hpex.Width + 3, this.XYParametersStart1.Location.Y
                + i * y_parameter_spacing + fileControlOffsetY);
            hpex.Visible = true;
            hpex.Enabled = !currentWaveform.DataFromFile;

            hpey.Enabled = !currentWaveform.DataFromFile;

            this.xyParameters.Add(hpex);
            this.xyParameters.Add(hpey);

            hpex.updateGUI += this.updateGUI;
            hpey.updateGUI += this.updateGUI;

            /*hpex.updateGUI += this.updateGUI;
            hpey.updateGUI += this.updateGUI;

            hpex.Location = new Point(this.XYParametersStart1.Location.X,
                this.XYParametersStart1.Location.Y + 2 * i * y_parameter_spacing);
            hpex.Visible = true;
            hpex.Enabled = true;
            hpex.Size = this.XYParametersStart1.Size;

            hpey.Location = new Point(this.XYParametersStart2.Location.X,
                this.XYParametersStart2.Location.Y + 2 * i * y_parameter_spacing);
            hpey.Visible = true;
            hpey.Enabled = true;
            hpey.Size = this.XYParametersStart2.Size;

            this.xyParameters.Add(hpex);
            this.xyParameters.Add(hpey);

            Label labx = new Label();
            Label laby = new Label();

            labx.Text = "X" + (i + 1).ToString();
            laby.Text = "Y" + (i + 1).ToString();
            labx.Location = new Point(this.XYLabelStart1.Location.X,
                this.XYLabelStart1.Location.Y + 2 * i * y_parameter_spacing);
            laby.Location = new Point(this.XYLabelStart2.Location.X,
                this.XYLabelStart2.Location.Y + 2 * i * y_parameter_spacing);
            labx.Visible = true;
            labx.Enabled = true;
            laby.Visible = true;
            laby.Enabled = true;

            this.xyParameterLabels.Add(labx);
            this.xyParameterLabels.Add(laby);
             * */
        }

        private void interpolationTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (interpolationTypeComboBox.SelectedIndex != -1)
            {
                if (currentWaveform != null)
                    currentWaveform.interpolationType = Waveform.InterpolationType.allInterpolationTypes[interpolationTypeComboBox.SelectedIndex];
                layOutParameters();
                updateGUI(sender, e);
            }

        }

        private void downButton_Click(object sender, EventArgs e)
        {
            if (currentWaveform.interpolationType.referencesOtherWaveforms)
            {
                // add a new referencable waveform
                currentWaveform.ReferencedWaveforms.Add(null);
                currentWaveform.WaveformCombiners.Add(Waveform.InterpolationType.CombinationOperators.Plus);
            }
            else
            {
                // add a new xy point
                DimensionedParameter x = new DimensionedParameter(Units.s, 0);
                DimensionedParameter y = new DimensionedParameter(new Units(currentWaveform.YUnits, Units.Multiplier.unity), 0);
                if (currentWaveform.XValues.Count > 0)
                {
                    x.parameter.ManualValue = currentWaveform.XValues[currentWaveform.XValues.Count - 1].parameter.ManualValue + 1;
                }
                currentWaveform.XValues.Add(x);
                currentWaveform.YValues.Add(y);
            }


            this.layOutParameters();
            if (updateGraph != null)
                updateGraph(sender, e);
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            if (currentWaveform.interpolationType.referencesOtherWaveforms)
            {
                // remove a combinalbe wf
                if (currentWaveform.ReferencedWaveforms.Count > 1)
                {
                    currentWaveform.ReferencedWaveforms.RemoveAt(currentWaveform.ReferencedWaveforms.Count - 1);
                    currentWaveform.WaveformCombiners.RemoveAt(currentWaveform.WaveformCombiners.Count - 1);
                }
            }
            else
            {
                // remove an xy point
                if (currentWaveform.XValues.Count > 0)
                {
                    currentWaveform.XValues.RemoveAt(currentWaveform.XValues.Count - 1);
                    currentWaveform.YValues.RemoveAt(currentWaveform.YValues.Count - 1);
                }

            }

            this.layOutParameters();
            if (updateGraph != null)
            {
                updateGraph(sender, e);
            }
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            currentWaveform.sortXValues();
            layOutParameters();
            updateGraph(this, null);
        }

        private void scaleButton_Click(object sender, EventArgs e)
        {
            if (currentWaveform.scaleXValues())
            {
                layOutParameters();
                updateGraph(this, null);
            }
            else
            {
                MessageBox.Show("Unable to rescale time values. Either one of the time values or the duration value is a variable, or the maximum time value is 0");
            }
        }

        private void copyToCommonWaveformsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Waveform copy = new Waveform(currentWaveform);
            Storage.sequenceData.CommonWaveforms.Add(copy);
            WordGenerator.MainClientForm.instance.commonWaveformEditor.setCommonWaveforms(Storage.sequenceData.CommonWaveforms);
        }

        //REO 10/2008
        private void fileBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select Waveform File";
            fdlg.Filter = "All files (*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                filePathTextBox.Text = fdlg.FileName;
            }
        }

        //REO 10/2008
        private void fileLoadCheckBox_Click(object sender, EventArgs e)
        {
            currentWaveform.DataFromFile = fileLoadCheckBox.Checked;

            layOutParameters();
        }

        //REO 10/2008
        private void fileLoadButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(filePathTextBox.Text))
                {
                    //to hold temporary X and Y values
                    Waveform tempwaveform = new Waveform();
                    //to parse each line in the file at either space or comma seperator
                    //trimming each line of white space at the beginning and end
                    DataStructures.UtilityClasses.WaveformParser p = new  DataStructures.UtilityClasses.WaveformParser(sr, @"[,\s]\s*", delegate(string s) { return s.Trim(); });
                 
                    double[] values;
                    while ((values = p.ReadFloats()) != null)
                    {
                        //there should be two values, otherwise the file is formated wrong
                        if (values.Length != 2)
                            throw new System.ApplicationException("File should have two comma or tab separated numbers per line");

                        tempwaveform.XValues.Add(new DimensionedParameter(Units.s, values[0]));
                        tempwaveform.YValues.Add(new DimensionedParameter(new Units(currentWaveform.YUnits, Units.Multiplier.unity), values[1]));
                    }

                    currentWaveform.XValues = tempwaveform.XValues;
                    currentWaveform.YValues = tempwaveform.YValues;
                    currentWaveform.DataFileName = filePathTextBox.Text;
                    currentWaveform.WaveformDuration = (currentWaveform.XValues.Count > 0) 
                        ? currentWaveform.XValues[currentWaveform.XValues.Count-1] : currentWaveform.WaveformDuration;
                    layoutNewWaveform();

                    updateGraph(this, null);
                }
            }
            catch (Exception ex)
            {
                // Let the user know what went wrong.
                MessageBox.Show("The file could not be read:\n" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (copyDuration != null)
            {
                copyDuration(sender, e);
            }
        }

        private void equationTextBox_TextChanged(object sender, EventArgs e)
        {
            if (currentWaveform != null)
            {
                try
                {
                    currentWaveform.EquationString = equationTextBox.Text;
                    if (updateGraph != null)
                    {
                        updateGraph(this, null);
                    }

                    updateEquationStatusLabel();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Caught exception when attempting waveform interpolation: " + ex.Message + ". Clearing equation.");
                    currentWaveform.EquationString = "";
                    equationTextBox.Text = "";
                }
            }
        }

        private void updateEquationStatusLabel()
        {
            if (currentWaveform != null)
            {
                equationStatusLabel.Text = WaveformEquationInterpolator.getEquationStatusString(currentWaveform.EquationString, Storage.sequenceData.Variables, Storage.sequenceData.CommonWaveforms);
            }
            else
            {
                equationTextBox.Text = "No waveform.";
            }

            if (equationStatusLabel.Text == "Valid equation.")
                equationStatusLabel.BackColor = Color.Green;
            else
                equationStatusLabel.BackColor = Color.Red;
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentWaveform != null)
            {
                if (currentWaveform.interpolationType == Waveform.InterpolationType.Combination)
                {
                    MessageBox.Show("Sorry, waveforms of the Combination interpolation type cannot be copied to clipboard. Instead, try copying to the common waveforms.");
                }
                Storage.clipboardWaveform = new Waveform(this.currentWaveform);
            }
        }

        private void pasteFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentWaveform != null)
            {
                if (Storage.clipboardWaveform != null)
                {

                    List<Variable> usedVars = new List<Variable>(Storage.clipboardWaveform.usedVariables().Keys);

                    foreach (Variable var in usedVars)
                    {
                        if (!(Storage.sequenceData.Variables.Contains(var))) {
                            MessageBox.Show("You have attempted to paste in a waveform from another sequence object that made use of variables. This is not permitted.");
                            return;
                        }
                    }

                    this.currentWaveform.copyWaveform(Storage.clipboardWaveform);

                    this.layoutNewWaveform();
                    
                }
            }
        }

        private void equationHelpText_Click(object sender, EventArgs e)
        {

        }

        
    }
}
