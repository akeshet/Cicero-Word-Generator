using System;
using System.Collections.Generic;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataStructures
{
    /// <summary>
    /// The ClientStartupSettings class contains information that are relevant upon client start-up,
    /// such as:
    ///     1) Default settings file;
    ///     2) Default sequence file;
    ///     3) List of recently-opened sequence files
    /// </summary>
    
    [Serializable]
    public class ClientStartupSettings
    {
        public List<string> recentFiles;

        public string settingsDataFileName;
        public string sequenceDataFileName;

        const int nMaxRecentFiles = 20;

        public ClientStartupSettings()
        {
            settingsDataFileName = DefaultNames.SettingsDataFile;
            sequenceDataFileName = DefaultNames.SequenceDataFile;

            ClearRecentFiles();
        }

        /// <summary>
        ///  Add a new file to the list of recently opened files
        /// </summary>
        public void AddNewFile(string newFile)
        {
            if (recentFiles.Contains(newFile))
                recentFiles.Remove(newFile);

            recentFiles.Insert(0, newFile);

            if (recentFiles.Count > nMaxRecentFiles)
            {
                recentFiles.RemoveAt(recentFiles.Count - 1);
            }
        }

        /// <summary>
        /// Reinitialize the list of recent files
        /// </summary>
        public void ClearRecentFiles()
        {
            recentFiles = new List<string>();
        }
    }
}
