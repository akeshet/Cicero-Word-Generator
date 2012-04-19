using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class DefaultNames
    {
        public class Extensions
        {
            public const string ClientStartupSettings = "dat";
            public const string SettingsData = "set";
            public const string SequenceData = "seq";
        }

        private class FileNames
        {
            public const string ClientStartupSettings = "StartupSettings";
            public const string SettingsData = "SettingsData";
            public const string SequenceData = "SequenceData";
        }

        // Default save locations and file names
        public const string ClientStartupSettingsFile = FileNames.ClientStartupSettings + "." +Extensions.ClientStartupSettings;
        public const string SettingsDataFile = FileNames.SettingsData +"." + Extensions.SettingsData;
        public const string SequenceDataFile = FileNames.SequenceData + "." +Extensions.SequenceData;

        public const string BackupFolder = "backup/";
    }
}
