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

        public static AtticusServerCommunicator server;



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            /* //
             * // demand high security permissions for Atticus. This may help improve performance?

            SecurityPermission perm = new SecurityPermission(SecurityPermissionFlag.AllFlags);
            perm.Assert();

             * */

            try
            {

                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                try
                {
                    NationalInstruments.NI4882.Address testObject = new NationalInstruments.NI4882.Address();
                    NationalInstruments.DAQmx.CouplingTypes testObject2 = new NationalInstruments.DAQmx.CouplingTypes();
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to load National Instruments DAQmx libraries. You may have an older version of these libraries installed. Please install the latest version of DAQmx. Press OK to see the detailed exception.", "Unable to load DAQmx libraries.");
                    throw;
                }



                // Maximum server uptime: 1000 days. Lets see if this solves some of those "disconnection" issues we had
                System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseTime = new TimeSpan(1000, 0, 0, 0, 0);


                ServerSettings serverSettings;
                String serverSettingsFilesName = FileNameStrings.DefaultServerSettingsDataFile;

                try
                {
                    serverSettings = loadServerSettings(serverSettingsFilesName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception when attempting to load server settings.");
                    Console.WriteLine("Proceeding with blank settings.");
                    serverSettings = new ServerSettings();
                }
                

                Console.WriteLine("Creating AtticusServerRuntime object...");

                server = new AtticusServerCommunicator(serverSettings);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                MainServerForm form = new MainServerForm();

                server.updateGUI += form.updateGUI;
                server.registerMessageEventHandler(form.addMessageLogText);





                Application.Run(form);

                

                saveServerSettings(AppDomain.CurrentDomain.BaseDirectory +  FileNameStrings.DefaultServerSettingsDataFile, serverSettings);
            }
            catch (Exception e)
            {
                display_unhandled_exception(e);
            }
        }

        /// <summary>
        /// Attempts to load server settings object. If one of the fixable System.ArgumentException
        /// arises, due to a naming incompatability between GPIB Address objects, attempts to fix that
        /// and continues loading. If any other exception occurs, the exception is rethrown.
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>loaded server settings object</returns>
        private static ServerSettings loadServerSettings(string filename)
        {
            ServerSettings serverSettings = Common.loadBinaryObjectFromFile(filename) as ServerSettings;
            return serverSettings;
        }

        public static void saveServerSettings(string fileName, ServerSettings serverSettings)
        {

            // Save server settings

            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fst = new FileStream(fileName, FileMode.Create))
            {
                bf.Serialize(fst, serverSettings);
            }
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