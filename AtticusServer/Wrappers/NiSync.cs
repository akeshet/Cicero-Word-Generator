using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AtticusServer
{
    /// <summary>
    /// This class wrapps up a bunch of functionality that was
    /// added to Atticus at some point by Emmanuel Mimoun. I have no idea
    /// what it does, and it seems to have all sorts of hard coded strings
    /// that are specific only to his experiment.
    /// 
    /// I'm not even sure if they are still in use. Therefore, I have preserved them 
    /// here while removing them from the active code base.
    /// 
    /// - Aviv Keshet, July 15 2012
    /// </summary>
    public class NiSync
    {
        [DllImport(@"C://VXIPNP//WinNT//Bin//niSync.dll")]
        private static extern bool niSync_init(string resourceName, bool idQuery, bool resetDevice, out System.IntPtr vi_session);

        [DllImport(@"C://VXIPNP//WinNT//Bin//niSync.dll")]
        private static extern bool niSync_close(System.IntPtr vi_session);

        [DllImport(@"C://VXIPNP//WinNT//Bin//niSync.dll")]
        private static extern bool niSync_ConnectClkTerminals(System.IntPtr vi_session, string sourceTerminal, string destinationTerminal);

        [DllImport(@"C://VXIPNP//WinNT//Bin//niSync.dll")]
        private static extern bool niSync_DisconnectClkTerminals(System.IntPtr vi_session, string sourceTerminal, string destinationTerminal);

        [DllImport(@"C://VXIPNP//WinNT//Bin//niSync.dll")]
        private static extern bool niSync_ConnectTrigTerminals(System.IntPtr vi_session,
            string srcTerminal,
            string destTerminal,
            string syncClock,
            int invert,
            int updateEdge);

        [DllImport(@"C://VXIPNP//WinNT//Bin//niSync.dll")]
        private static extern bool niSync_DisconnectTrigTerminals(System.IntPtr vi,
            string srcTerminal,
            string destTerminal);

        [DllImport(@"C://VXIPNP//WinNT//Bin//niSync.dll")]
        private static extern bool niSync_ConnectSWTrigToTerminal(System.IntPtr vi,
                              string srcTerminal,
                              string destTerminal,
                              string syncClock,
                              int invert,
                              int updateEdge,
                              double delay);

        [DllImport(@"C://VXIPNP//WinNT//Bin//niSync.dll")]
        private static extern bool niSync_DisconnectSWTrigFromTerminal(System.IntPtr vi,
            string srcTerminal,
            string destTerminal);

        [DllImport(@"C://VXIPNP//WinNT//Bin//niSync.dll")]
        private static extern bool niSync_SendSoftwareTrigger(System.IntPtr vi,
            string srcTerminal);

        [DllImport(@"C://VXIPNP//WinNT//Bin//niSync.dll")]
        private static extern bool niSync_SetAttributeViInt32(System.IntPtr session,
                              string terminalName,
                              int attribute,
                              System.Int32 values);



        [DllImport(@"C://VXIPNP//WinNT//Bin//niSync.dll")]
        private static extern bool niSync_SetAttributeViReal64(System.IntPtr session,
            string terminalName,
            int attribute,
            double values);

        [DllImport(@"C://VXIPNP//WinNT//Bin//niSync.dll")]
        private static extern bool niSync_SetAttributeViString(System.IntPtr session,
            string terminalName,
            int attribute,
            string values);

        static System.IntPtr session = IntPtr.Zero;
        static bool status;

        public static void onLoad()
        {
            try
            {
                status = niSync_init("PXI1Slot2", true, true, out session);
                status = niSync_SetAttributeViReal64(session, "", 1150400, 20000000);
                status = niSync_SetAttributeViString(session, "", 1150201, "DDS");
                status = niSync_ConnectClkTerminals(session, "Oscillator", "PXI_Clk10");
                status = niSync_ConnectTrigTerminals(session, "SyncClkFullSpeed", "PXI_Trig7", "SyncClkFullSpeed", 0, 0);
            }
            catch { }
        }

        public static void onClose()
        {
            try
            {
                status = niSync_DisconnectClkTerminals(session, "Oscillator", "PXI_Clk10");
                status = niSync_DisconnectTrigTerminals(session, "SyncClkFullSpeed", "PXI_Trig7");
                status = niSync_close(session);
            }
            catch { }
        }

    }
}
