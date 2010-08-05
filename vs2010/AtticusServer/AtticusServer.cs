using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Security.Permissions;
using DataStructures;

namespace AtticusServer
{
    static class AtticusServer
    {

        public static AtticusServerRuntime server;
        public static string serverSettingsFileName = "AtticusServerSettings.set";



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            /*
             * // demand high security permissions for Atticus. This may help improve performance?

            SecurityPermission perm = new SecurityPermission(SecurityPermissionFlag.AllFlags);
            perm.Assert();

             * */

            try
            {

                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


                // Maximum server uptime: 1000 days. Lets see if this solves some of those "disconnection" issues we had
                System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseTime = new TimeSpan(1000, 0, 0, 0, 0);

                BinaryFormatter b = new BinaryFormatter();
                ServerSettings serverSettings;


                try
                {
                    FileStream fs = new FileStream(serverSettingsFileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    serverSettings = (ServerSettings)b.Deserialize(fs);
                    fs.Close();
                }
                catch (Exception)
                {
                    Console.WriteLine("Exception when attempting to load server settings.");
                    Console.WriteLine("Proceeding with blank settings.");
                    serverSettings = new ServerSettings();
                }

                Console.WriteLine("Creating AtticusServerRuntime object...");

                server = new AtticusServerRuntime(serverSettings);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                MainServerForm form = new MainServerForm();

                server.updateGUI += form.updateGUI;
                server.messageLog += form.addMessageLogText;



                Application.Run(form);

                saveServerSettings(AppDomain.CurrentDomain.BaseDirectory +  serverSettingsFileName, serverSettings);
            }
            catch (Exception e)
            {
                display_unhandled_exception(e);
            }
        }

        public static void saveServerSettings(string fileName, ServerSettings serverSettings)
        {

            // Save server settings

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fst = new FileStream(fileName, FileMode.Create);
            bf.Serialize(fst, serverSettings);
            fst.Close();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            display_unhandled_exception(e.ExceptionObject as Exception);
        }

        private static void display_unhandled_exception(Exception e)
        {
            ExceptionViewerDialog dial = new ExceptionViewerDialog(e);
            dial.ShowDialog();
            System.Console.WriteLine("Caught an unhandled exception. " + e.Message + e.InnerException + e.Source + e.StackTrace);
          //  throw e;
        }
    }
}