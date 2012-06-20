using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DataStructures;

namespace WordGenerator
{

    public static class GlobalInfo
    {
        /// <summary>
        /// Set to true if Windows 7 is being used.
        /// </summary>
        public static bool usingWindows7 = false;
    }

    [Serializable]
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {


            runCicero();
        }

        [STAThread]
        public static void runCicero()
        {
            runCicero(null);
        }

        private RunLog runLog;
        public Program(RunLog runLog)
        {
            this.runLog = runLog;
        }

        public void runProgram()
        {
            runCicero(runLog);
        }

        [STAThread]
        public static void runCicero(RunLog runLog)
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Application.EnableVisualStyles();

                // Detect if Windows 7 or Vista is being used.
                OperatingSystem osInfo = System.Environment.OSVersion;
                if (osInfo.Version.Major == 6)
                {
                    WordGenerator.GlobalInfo.usingWindows7 = true;
                }
                
               

                try
                {

                    Application.SetCompatibleTextRenderingDefault(false);
                   
                }
                catch (InvalidOperationException)
                {
                }


                MainClientForm mainForm;

                if (runLog == null)
                {
                    mainForm = new MainClientForm();
                }
                else
                {
                    mainForm =  new MainClientForm(runLog);
                }

                ClientRunner runner = new ClientRunner();
                runner.messageLog += new EventHandler<MessageEvent>(mainForm.addMessageLogText);

                DataStructures.Timing.NetworkClockProvider.registerStaticMessageLogHandler(mainForm.addMessageLogText);
                DataStructures.Timing.NetworkClockProvider.startListener();

                Application.Run(mainForm);

                DataStructures.Timing.NetworkClockProvider.shutDown();
            }
            catch (Exception e)
            {
                display_unhandled_exception(e);
            }
        }
        

        private static void display_unhandled_exception(Exception e)
        {
            if (e != null)
            {
                ExceptionViewerDialog dial = new ExceptionViewerDialog(e);
                dial.ShowDialog();
                System.Console.WriteLine("Caught an unhandled exception. " + e.Message + e.InnerException + e.Source + e.StackTrace);
            }
            else
            {
                MessageBox.Show("Caught an unhandled null exception.");
                System.Console.WriteLine("Caught an unhandled null exception.");
            }
            //throw e;

        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            display_unhandled_exception(e.ExceptionObject as Exception);
        }
    }
}