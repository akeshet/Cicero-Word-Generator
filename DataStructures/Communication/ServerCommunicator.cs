using System;
using System.Collections.Generic;
using System.Text;
using DataStructures;

namespace DataStructures
{
    /// <summary>
    /// This class defines the interface used to communication between the Client and Server (i.e. sequence editor 
    /// and hardware backend, Cicero and Atticus).
    /// 
    /// Communication can take place between processes on different computers using the .NET remoting infrastructure.
    /// Remoting requires this object to extend MarshalByRefObject
    /// </summary>
    public abstract class ServerCommunicator : MarshalByRefObject
    {

        public enum BufferGenerationStatus { Success, Failed_Out_Of_Memory, Failed_Invalid_Data, Failed_Settings_Null, Failed_Sequence_Null, Failed_Buffer_Underrun };


        public abstract void nextRunTimeStamp(DateTime timeStamp);


        /// <summary>
        /// Outputs a single timestep.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public abstract bool outputSingleTimestep(SettingsData settings, SingleOutputFrame output);


        /// <summary>
        /// Gets the server settings object for this server.
        /// </summary>
        /// <returns></returns>
        public abstract ServerSettingsInterface getServerSettings();

        /// <summary>
        /// Outputs a single gpib group. (evaluated at the beginning of the group.)
        /// </summary>
        /// <param name="gpibGroup"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public abstract bool outputGPIBGroup(GPIBGroup gpibGroup, SettingsData settings);

        /// <summary>
        /// Outputs a single rs232 Group. (evaluated at the beginning of the group.)
        /// </summary>
        /// <param name="rs232Group"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public abstract bool outputRS232Group(RS232Group rs232Group, SettingsData settings);

        /// <summary>
        /// Gets a list of hardware channels attached to server.
        /// </summary>
        /// <returns></returns>
        public abstract List<HardwareChannel> getHardwareChannels();

        /// <summary>
        /// Sets the sequence which is to be run.
        /// </summary>
        /// <param name="sequence"></param>
        public abstract bool setSequence(SequenceData sequence);

        /// <summary>
        /// Sets the settings object.
        /// </summary>
        public abstract bool setSettings(SettingsData settings);

       

        /// <summary>
        /// Prepares the server for a run. Generates relevant buffers / "tasks". This function is blocking, it wont return until
        /// the tasks are generated. Thus, it should not be run in a UI thread.
        /// </summary>
        /// <param name="listIterationNumber"></param>
        public abstract BufferGenerationStatus generateBuffers(int listIterationNumber);

        /// <summary>
        /// Generates a triggers. This in effect starts the sequence output.
        /// </summary>
        public abstract bool generateTrigger();

        /// <summary>
        /// Starts any Hardware Triggered tasks, or any software triggered tasks which run off an external sample clock.
        /// 
        /// clockID is a randomly-generated-per-shot number that is used to uniquely identify a shared network clock
        /// </summary>
        /// <returns></returns>
        public abstract bool armTasks(UInt32 clockID);

        /// <summary>
        /// Returns true if the most recent run completed sucessfully, false otherwise.
        /// </summary>
        /// <returns></returns>
        public abstract bool runSuccess();


        public abstract string getServerName();

        public abstract bool ping();

        public abstract void stop();

    }
}
