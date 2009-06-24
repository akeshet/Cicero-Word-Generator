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
        private Dictionary<string, DateTime> recentFilesLastUsageDates;

        public Dictionary<string, DateTime> RecentFilesLastUsageDates
        {
            get {
                if (recentFilesLastUsageDates == null)
                {
                    recentFilesLastUsageDates = new Dictionary<string, DateTime>();
                }
                return recentFilesLastUsageDates; 
            }
            set { recentFilesLastUsageDates = value; }
        }

        public string settingsDataFileName;
        public string sequenceDataFileName;

        const int nMaxRecentFiles = 40;

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

            if (RecentFilesLastUsageDates.ContainsKey(newFile))
            {
                RecentFilesLastUsageDates.Remove(newFile);
            }
            RecentFilesLastUsageDates.Add(newFile, DateTime.Now);

            if (recentFiles.Count > nMaxRecentFiles)
            {
                string removedFileName = recentFiles[recentFiles.Count - 1];
                recentFiles.RemoveAt(recentFiles.Count - 1);
                if (RecentFilesLastUsageDates.ContainsKey(removedFileName))
                {
                    RecentFilesLastUsageDates.Remove(removedFileName);
                }
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
