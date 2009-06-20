using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Virgil
{
    static class Program
    {

        public static string serverSettingsFileName = "VirgilServerSettings.set";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            VirgilServerSettings serverSettings;

            BinaryFormatter b = new BinaryFormatter();

            try
            {
                FileStream fs = new FileStream(serverSettingsFileName, FileMode.Open, FileAccess.Read, FileShare.None);
                serverSettings = (VirgilServerSettings)b.Deserialize(fs);
                fs.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Exception when attempting to load server settings.");
                Console.WriteLine("Proceeding with blank settings.");
                serverSettings = new VirgilServerSettings();
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainVirgilForm(serverSettings));

            saveServerSettings(serverSettingsFileName, serverSettings);
        }

        public static void saveServerSettings(string fileName, VirgilServerSettings serverSettings)
        {

            // Save server settings

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fst = new FileStream(fileName, FileMode.Create);
            bf.Serialize(fst, serverSettings);
            fst.Close();
        }
    }
}