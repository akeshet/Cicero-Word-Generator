using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Zeus
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseTime = new TimeSpan(1000, 0, 0, 0, 0);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainExampleServerlForm());
        }
    }
}