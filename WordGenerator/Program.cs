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

                // Test if certain important libraries are installed correctly.

                try
                {
                    NationalInstruments.NI4882.Address testObject = new NationalInstruments.NI4882.Address();
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to load National Instruments DAQmx libraries. You may have an older version of these libraries installed. Please install the latest version of DAQmx. Press OK to see the detailed exception.", "Unable to load DAQmx libraries.");
                    throw;
                }

                try
                {
                    NationalInstruments.UI.WaveformPlot testObject = new NationalInstruments.UI.WaveformPlot();
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to load National Instruments User Interface libraries. You may have an older version of these libraries installed, or your copy of Cicero may have been compiled on a Visual Studio installation without a working Measurement Studio license. Press OK to see the detailed exception.", "Unable to load National Instruments UI libraries.");
                    throw;
                }



                if (runLog == null)
                {
                    Application.Run(new mainClientForm());
                }
                else
                {
                    Application.Run(new mainClientForm(runLog));
                }
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