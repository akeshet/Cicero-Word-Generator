using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructures;
using System.Windows.Forms;

namespace WordGenerator
{
    public class ClientRunner
    {
        public event EventHandler<MessageEvent> messageLog;
        public static ClientRunner instance;

        public ClientRunner()
        {
            if (instance != null)
            {
                throw new Exception("Clientrunner is already created!");
            }
            instance = this;
        }

        /// <summary>
        /// outputs the editor's timestep. set silent to true if no message logs should be generated.
        /// </summary>
        /// <param name="silent"></param>
        /// <returns></returns>
        public bool outputTimestepNow(TimeStep timeStep, bool silent, bool showErrorDialog)
        {
            List<string> unconnectedServers = Storage.settingsData.unconnectedRequiredServers();

            if (!Storage.sequenceData.Lists.ListLocked)
            {
                WordGenerator.MainClientForm.instance.variablesEditor.tryLockLists();
            }
            if (!Storage.sequenceData.Lists.ListLocked)
            {
                if (!silent)
                    messageLog(this, new MessageEvent("Unable to output timestep, lists not locked."));
                if (showErrorDialog)
                {
                    MessageBox.Show("Unable to output timestep, lists not locked.");
                }
                return false;
            }

            if (unconnectedServers.Count == 0)
            {

                WordGenerator.MainClientForm.instance.cursorWait();
                ServerManager.ServerActionStatus actionStatus;
                try
                {


                    actionStatus = Storage.settingsData.serverManager.outputSingleTimestepOnConnectedServers(
                        Storage.settingsData,
                        Storage.sequenceData.getSingleOutputFrameAtEndOfTimestep(timeStep, Storage.settingsData, Storage.settingsData.OutputAnalogDwellValuesOnOutputNow),
                        messageLog);
                }
                finally
                {
                    WordGenerator.MainClientForm.instance.cursorWaitRelease();
                }

                if (actionStatus == ServerManager.ServerActionStatus.Success)
                {
                    if (!silent)
                        messageLog(this, new MessageEvent("Successfully output timestep " + timeStep.ToString()));
                    WordGenerator.MainClientForm.instance.CurrentlyOutputtingTimestep = timeStep;
                    return true;
                }
                else
                {

                    if (!silent)
                        messageLog(this, new MessageEvent("Communication or server error attempting to output this timestep: " + actionStatus.ToString()));
                    if (showErrorDialog)
                    {
                        MessageBox.Show("Communication or server error attempting to output this timestep: " + actionStatus.ToString());
                    }
                }
            }
            else
            {
                string missingServerList = ServerManager.convertListOfServersToOneString(unconnectedServers);
                if (!silent)
                    messageLog(this, new MessageEvent("Unable to output this timestep. The following required servers are not connected: " + missingServerList));

                if (showErrorDialog)
                {
                    MessageBox.Show("Unable to output this timestep. The following required servers are not connected: " + missingServerList);
                }
            }
            return false;
        }
    }
}
