using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataStructures;

namespace WordGenerator
{
    public partial class RunLogManager : Form
    {
        SettingsData settingsData;
        private bool writeVariableOutputTextFile;
        private string variableOutputFilename;
        private string variableOutputPath;
        public RunLogManager(SettingsData settingsData)
        {
            InitializeComponent();
            this.settingsData=settingsData;
            outputFileTextBox.Text = this.settingsData.VariableOutputFilename;
            simpleVariableOutputCheckBox.Checked = this.settingsData.WriteVariableOutputTextFile;
            folderBrowserDialog.SelectedPath = this.settingsData.VariableOutputFilenameDirectory;
            folderBrowserDialog.ShowNewFolderButton = false;
            

            if (simpleVariableOutputCheckBox.Checked)
                outputFileTextBox.ReadOnly = false;
            else
                outputFileTextBox.ReadOnly = true;


        
        }

        private void simpleVariableOutputCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (simpleVariableOutputCheckBox.Checked)
                outputFileTextBox.ReadOnly = false;
            else
                outputFileTextBox.ReadOnly = true;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
           this.Close();
        }


        private void outputFileTextBox_LostFocus(object sender, EventArgs e)
        {
          
        }

        private void okayButton_Click(object sender, EventArgs e)
        {
            settingsData.VariableOutputFilename = outputFileTextBox.Text;
            settingsData.WriteVariableOutputTextFile = simpleVariableOutputCheckBox.Checked;
            settingsData.VariableOutputFilenameDirectory = variableOutputPath;
            this.Close();
        }

        private void setPathButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            variableOutputPath = folderBrowserDialog.SelectedPath;
        }
    }
}
