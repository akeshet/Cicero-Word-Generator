using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataStructures
{
    public class SharedForms
    {
        /// <summary>
        /// Prompts the user with an OpenFileDialog with the appropriate message
        /// </summary>
        /// <param name="userfriendlyName">Description of file type requested, e.g. "ClientStartupSettings"</param>
        /// <param name="fileExtension">The appropriate extension for the requested file type</param>
        /// <returns>The path of the selected file, or null if user chooses 'Cancel'</returns>
        public static string PromptOpenFileDialog(string userfriendlyName, string fileExtension)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Open " + userfriendlyName;
            openFileDialog.Filter = FileNameStrings.fileDialogFilterString(userfriendlyName, fileExtension);

            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                return openFileDialog.FileName;
            else
                return null;
        }


        /// <summary>
        /// Prompt user to save file.
        /// </summary>
        /// <param name="fileKind">User-friendly name for file type.</param>
        /// <param name="fileExtension">File extension including period, for instance ".seq". This matches the form in DetaultNames.Extensions</param>
        /// <returns></returns>
        public static string PromptSaveFile(string fileKind, string fileExtension)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.DefaultExt = fileExtension;
            sf.Filter = FileNameStrings.fileDialogFilterString(fileKind, fileExtension);
            sf.FilterIndex = 1;
            sf.AddExtension = true;

            sf.Title = "Save " + fileKind;
            DialogResult dr = sf.ShowDialog();
            if (dr == DialogResult.OK)
                return sf.FileName;
            return null;
        }


    }
}
