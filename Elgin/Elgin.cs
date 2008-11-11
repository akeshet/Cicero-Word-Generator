using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Elgin
{
    static class Elgin
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ElginMDIParent());
        }
    }
}