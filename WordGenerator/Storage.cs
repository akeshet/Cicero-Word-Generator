using System;
using DataStructures;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WordGenerator
{
    /// <summary>
    /// The Storage class is a static container for application-related data. Its main components (henceforth referred to as
    /// the "Storage subclasses") are:
    ///     1) clientStartupSettings;
    ///     2) settingsData;
    ///     3) sequenceData;
    /// For further details on each subclass, please see the associated source file.
    /// </summary>
    public static class Storage
    {
        private static string clientStartupSettingsFileName;

        public static ClientStartupSettings clientStartupSettings;
        public static SettingsData settingsData;

        public static SequenceData sequenceData;

        public static Waveform clipboardWaveform;


        /// <summary>
        /// The SaveAndLoad class is a collection of methods that implement (as the title suggests!) saving and loading of
        /// the Storage subclasses. The private Load(...), Save(...), PromptOpenFileDialog(...) are basic methods that implement the
        /// .NET mechanism for serialization. From the application, we indicate that we want to save or load by invoking the
        /// appropriate public intermediary method; for example, LoadSettingsData(string path) to load the SettingsData file.
        /// </summary>
        public static class SaveAndLoad
        {
            /// <summary>
            /// Loads an object from serialized format using .NET's BinaryFormatter. The method internally catches 
            /// (and quiets) FileNotFoundException, SerializationException and ArgumentNullException. In such cases, 
            /// a null object is returned as an error flag.
            /// </summary>
            /// <returns>Deserialized object or null</returns>
            private static object Load(string path)
            {
                BinaryFormatter b = new BinaryFormatter();
                object result = null;

                if (path == null)
                    return null;

                try
                {
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                    result = b.Deserialize(fs);
                    fs.Close();
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("SaveAndLoad.Load(), FileNotFoundException: " + e.Message);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("SaveAndLoad.Load(), SerializationException: " + e.Message);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("SaveAndLoad.Load(), ArgumentNullException: " + e.Message);
                }

                return result;
            }

            /// <summary>
            /// Saves an object to the specified path using the .NET BinaryFormatter. In contrast to the Load(string path)
            /// method, Save(...) does NOT internally catch any exceptions. Furthermore, If a file already exists at the target 
            /// path, the method will create a backup.
            /// </summary>
            private static void Save(string path, object obj)
            {
                Save(path, obj, true);
            }
            private static void Save(string path, object obj, bool saveOldFile)
            {
                BinaryFormatter b = new BinaryFormatter();
                
                

                if( saveOldFile )
                    #region Backup old file
                    if (File.Exists(path)) // Does our target already exist?
                    {
                        // Make sure that the backup folder exists
                        if (!Directory.Exists(DefaultNames.BackupFolder))
                            Directory.CreateDirectory(DefaultNames.BackupFolder);

                        // We'll use the current time to generate a unique file name. Unfortunately, the standard
                        // ToString methods associated with the DateTime class generate strings incompatible for filenames.
                        // So we have our own little scheme, below:
                        DateTime dt = DateTime.Now;
                        string timestamp = dt.Year.ToString()   + "-" +
                                           dt.Month.ToString()  + "-" +
                                           dt.Day.ToString()    + "-" +
                                           dt.Hour.ToString()   + "-" +
                                           dt.Minute.ToString() + "-" +
                                           dt.Second.ToString();

                        // It is possible that the given path is absolute, in which case our naming scheme below will not work. So, 
                        // with the following code, we fetch only the name of the file.
                        string fileNameOnly;
                        #region Get the file name from (possibly) the absolute path
                    char pathDelimiter = '\\';
                    string[] split = null;

                    split = path.Split(pathDelimiter);
                    fileNameOnly = split[split.Length - 1];
                    #endregion

                        string backupFileName = DefaultNames.BackupFolder + fileNameOnly + "." + timestamp + "." + "backup";

                        File.Copy(path, backupFileName, true); // We allow overwrite, in the off chance that the user requests
                                                               // two consecutive saves within the same second, and generates a 
                                                               // duplicate filename!
                    
                        // Acknowledge
                        Console.WriteLine("SaveAndLoad.Save(): Created backup file at " + backupFileName);
                    }
                    #endregion

                // Since we have created a backup, it ought to be okay to clobber the old!
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                b.Serialize(fs, obj);
                fs.Close();
            }

            /// <summary>
            /// Prompts the user with an OpenFileDialog with the appropriate message
            /// </summary>
            /// <param name="subclassType">Description of file type requested, e.g. "ClientStartupSettings"</param>
            /// <param name="subclassExtension">The appropriate extension for the requested file type</param>
            /// <returns>The path of the selected file, or null if user chooses 'Cancel'</returns>
            public static string PromptOpenFileDialog(string subclassType, string subclassExtension)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Title = "Open " + subclassType;
                openFileDialog.Filter =
                 subclassType + " files (*" + subclassExtension + ")|*" + subclassExtension +
                 "|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    return openFileDialog.FileName;
                else
                    return null;
            }
            

            /// <summary>
            /// The LoadAllSubclasses(string clientStartupSettingsFileNameParam) method will set ALL subclasses of 
            /// the Storage container based on the information given in the clientStartupSettings file.
            /// </summary>
            /// <param name="clientStartupSettingsFileNameParam">Target ClientStartupSettings filename</param>
            public static void LoadAllSubclasses(string clientStartupSettingsFileNameParam)
            {
                clientStartupSettingsFileName = clientStartupSettingsFileNameParam;

                LoadClientStartupSettings(clientStartupSettingsFileName);
                LoadSettingsData(clientStartupSettings.settingsDataFileName);
                LoadSequenceDataToStorage(clientStartupSettings.sequenceDataFileName);

                if (Storage.settingsData == null)
                    Storage.settingsData = new SettingsData();

                if (Storage.sequenceData == null)
                    Storage.sequenceData = new SequenceData();
            }

            public static void LoadClientStartupSettings(string path)
            {
                //string targetFile;
                clientStartupSettings = Load(path) as ClientStartupSettings;
                
                if (clientStartupSettings == null) // Failed to load ClientStartupSettings in the first pass. Probably file doesn't exist.
                {
                   /* targetFile =
                        PromptOpenFileDialog("ClientStartupSettings", DefaultNames.Extensions.ClientStartupSettings);

                    //if (targetFile == null) // Null return indicates that user has opted for 'Cancel' i.e. default state
                    //{
                      MessageBox.Show("Proceeding with default ClientStartupSettings!"); */
                        clientStartupSettings = new ClientStartupSettings();

                        // Also, give it the default name
                        clientStartupSettingsFileName = DefaultNames.ClientStartupSettingsFile;
                    /*}
                    else
                    {
                        clientStartupSettings = Load(targetFile) as ClientStartupSettings;
                        clientStartupSettingsFileName = targetFile;
                    }*/
                }
            }



            private delegate bool boolVoidDelegate();

            public static bool LoadSettingsData(string path)
            {
                SettingsData loadMe = null;

                if (path != null)
                {
                    loadMe = Load(path) as SettingsData;                    
                }
                else
                {
                    path =
                        PromptOpenFileDialog("Settings", DefaultNames.Extensions.SettingsData);

                    loadMe = Load(path) as SettingsData;
                }

                if (loadMe != null)
                {
                    Storage.settingsData = loadMe;
                    WordGenerator.mainClientForm.instance.OpenSettingsFileName = path;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static SequenceData LoadSequenceWithFileDialog()
            {
                    string path =
                        PromptOpenFileDialog("Sequence", DefaultNames.Extensions.SequenceData);

                    return  Load(path) as SequenceData;
            }

            public static bool LoadSequenceDataToStorage(string path)
            {

                SequenceData loadMe;

                if (path != null)
                {
                    loadMe = Load(path) as SequenceData;
                }
                else
                {
                    path =
                        PromptOpenFileDialog("Sequence", DefaultNames.Extensions.SequenceData);

                    loadMe = Load(path) as SequenceData;
                }

                if (loadMe != null)
                {
                    Storage.sequenceData = loadMe;
                    clientStartupSettings.AddNewFile(path);
                    WordGenerator.mainClientForm.instance.OpenSequenceFileName = path;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
            public static void SaveAllSubclasses()
            {
                SaveClientStartupSettings(clientStartupSettingsFileName);
                SaveSettingsData(clientStartupSettings.settingsDataFileName);
                SaveSequenceData(clientStartupSettings.sequenceDataFileName);
            }

            public static void SaveClientStartupSettings()
            {
                Save(AppDomain.CurrentDomain.BaseDirectory + clientStartupSettingsFileName, clientStartupSettings);
            }
            public static void SaveClientStartupSettings(string path)
            {
                Save(path, clientStartupSettings);
            }


            public static void SaveSettingsData(string path)
            {

                if (path == null)
                {
                    path = PromptSaveFile("Settings", DefaultNames.Extensions.SettingsData);
                    if (path != null)
                    {
                        Save(path, settingsData);
                        WordGenerator.mainClientForm.instance.OpenSettingsFileName = path;
                    }
                }
                if (path != null)
                {
                    Save(path, settingsData);
                    WordGenerator.mainClientForm.instance.OpenSettingsFileName = path;
                }
            }

            public static string PromptSaveFile(string fileKind, string fileExtension)
            {
                SaveFileDialog sf = new SaveFileDialog();
                sf.DefaultExt = fileExtension;
                sf.Filter = fileKind + " files (*."+fileExtension+") |*." + fileExtension + "|All files (*.*)|*.*";
                sf.FilterIndex = 1;
                sf.AddExtension = true;
            
                sf.Title = "Save " + fileKind;
                DialogResult dr = sf.ShowDialog();
                if (dr == DialogResult.OK)
                    return sf.FileName;
                return null;
            }

            public static void SaveSequenceData(string path) {
                SaveSequenceData(path, Storage.sequenceData);
            }

            public static void SaveSequenceData(string path, SequenceData sequence)
            {
                if (path == null)
                {
                    path = PromptSaveFile("Sequence", "seq");
                }

                if (path != null)
                {
                    Save(path, sequence);
                    if (sequence == sequenceData)
                        WordGenerator.mainClientForm.instance.OpenSequenceFileName = path;
                    clientStartupSettings.AddNewFile(path);
                }
            }
        }
    }
}
