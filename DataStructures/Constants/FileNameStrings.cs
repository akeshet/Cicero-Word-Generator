using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class FileNameStrings
    {
        public class Extensions
        {
            public const string ClientStartupSettings = ".dat";
            public const string ClientSettingsData = ".set";
            public const string SequenceData = ".seq";
            public const string ServerSettingsData = ".set";
        }

        private class DefaultFileNames
        {
            public const string ClientStartupSettings = "StartupSettings";
            public const string ClientSettingsData = "SettingsData";
            public const string SequenceData = "SequenceData";
            public const string ServerSettingsData = "AtticusServerSettings";
        }

        public class FriendlyNames
        {
            public const string ClientStartupSettings = "Cicero Startup";
            public const string ClientSettingsData = "Cicero Settings";
            public const string SequenceData = "Cicero Sequence";
            public const string ServerSettingsData = "Atticus Settings";
        }

        // Default save locations and file names
        public const string DefaultClientStartupSettingsFile = DefaultFileNames.ClientStartupSettings +Extensions.ClientStartupSettings;
        public const string DefaultClientSettingsDataFile = DefaultFileNames.ClientSettingsData + Extensions.ClientSettingsData;
        public const string DefaultSequenceDataFile = DefaultFileNames.SequenceData +Extensions.SequenceData;
        public const string DefaultServerSettingsDataFile = DefaultFileNames.ServerSettingsData + Extensions.ServerSettingsData;

        public const string BackupFolder = "backup/";

        public static string fileDialogFilterString(string userfriendlyFiletypeName, string fileExtension)
        {
            return userfriendlyFiletypeName + " files (*" + fileExtension + ")|*" + fileExtension +
                 "|All files (*.*)|*.*";
        }
    }
}
