using System;
using System.Collections.Generic;
using System.Text;
using DataStructures;
using System.Threading;
using NationalInstruments.VisaNS;

namespace AtticusServer
{
    /// <summary>
    /// This class mimicks some of the functionality of the NI "task" class, but is used to drive various RS232 devices.
    /// This is where all of the rs232-device-specific code is stored.
    /// This class also deals with software pseudo-realtiming of rs232 commands.
    /// 
    /// The general structure of this class was copied from GPIBTask.
    /// </summary>
    public class RS232Task : DataStructures.Timing.SoftwareClockSubscriber
    {

        public event NationalInstruments.DAQmx.TaskDoneEventHandler Done;
        

        private struct RS232Command
        {
            public string command;
            /// <summary>
            /// Time during the sequence at which the command is to be output.
            /// The unit is "ticks".
            /// 1 tick = 100 ns.
            /// </summary>
            public long commandTime;

            public RS232Command(string comm, long commTime)
            {
                this.command = comm;
                this.commandTime = commTime;
            }
        }

        private List<RS232Command> commandBuffer = new List<RS232Command>();

        private int logicalChannelID;

        public override string ToString()
        {
            return "RS232 Logical Channel " + logicalChannelID + " ";
        }

        public RS232Task(NationalInstruments.VisaNS.SerialSession device)
        {
            this.device = device;
        }

        NationalInstruments.VisaNS.SerialSession device;


        public bool generateBuffer(SequenceData sequence, DeviceSettings deviceSettings, HardwareChannel hc, int logicalChannelID)
        {
            lock (commandBuffer)
            {
                commandBuffer.Clear();
                this.logicalChannelID = logicalChannelID;


                if (deviceSettings.StartTriggerType != DeviceSettings.TriggerType.SoftwareTrigger)
                {
                    throw new Exception("RS232 devices must have a software start trigger.");
                }


                //     List<TimeStep> enabledSteps = sequence.enabledTimeSteps();

                int currentStepIndex = -1;

                //measured in ticks. 1 tick = 100 ns.
                long currentTime = 0;

                long postRetriggerTime = 100; // corresponds to 10us.
                // A workaround to issue when using software timed groups in 
                // fpga-retriggered words

                // the workaround: delay the software timed group by an immesurable amount
                // if it is started in a retriggered word



                // This functionality is sort of somewhat duplicated in sequencedata.generatebuffers. It would be good
                // to come up with a more coherent framework to do these sorts of operations.
                while (true)
                {
                    currentStepIndex++;

                    if (currentStepIndex >= sequence.TimeSteps.Count)
                        break;

                    TimeStep currentStep = sequence.TimeSteps[currentStepIndex];

                    if (!currentStep.StepEnabled)
                        continue;

                    long postTime = 0;
                    if (currentStep.RetriggerOptions.WaitForRetrigger)
                        postTime = postRetriggerTime;

                    if (currentStep.rs232Group == null || !currentStep.rs232Group.channelEnabled(logicalChannelID))
                    {
                        currentTime += seconds_to_ticks(currentStep.StepDuration.getBaseValue());
                        continue;
                    }

                    // determine the index of the next step in which this channel has an action
                    int nextEnabledStepIndex = sequence.findNextRS232ChannelEnabledTimestep(currentStepIndex, logicalChannelID);

                    long groupDuration = seconds_to_ticks(sequence.timeBetweenSteps(currentStepIndex, nextEnabledStepIndex));

                    // now take action:

                    RS232GroupChannelData channelData = currentStep.rs232Group.getChannelData(logicalChannelID);

                    if (channelData.DataType == RS232GroupChannelData.RS232DataType.Raw)
                    {
                        // Raw string commands just get added 
                        string stringWithCorrectNewlines = AddNewlineCharacters(channelData.RawString);

                        commandBuffer.Add(new RS232Command(stringWithCorrectNewlines, currentTime + postTime));
                    }
                    else if (channelData.DataType == RS232GroupChannelData.RS232DataType.Parameter)
                    {
                        if (channelData.StringParameterStrings != null)
                        {
                            foreach (StringParameterString srs in channelData.StringParameterStrings)
                            {
                                commandBuffer.Add(new RS232Command(
                                    AddNewlineCharacters(srs.ToString()), currentTime + postTime));
                            }
                        }
                    }



                    currentTime += seconds_to_ticks(currentStep.StepDuration.getBaseValue());
                }
            }

           return true;
        }

        int currentCommand;

        private bool isDone = false;

        /// <summary>
        /// Implementation of SoftwareClockSubscriber
        /// </summary>
        /// <param name="elaspedTime_ms"></param>
        public bool reachedTime(uint elaspedTime_ms, int priority)
        {
            return runTick(elaspedTime_ms);
        }

        public bool handleExceptionOnClockThread(Exception e)
        {
            return false;
        }

        /// <summary>
        /// This method outputs new commands when necessary.
        /// </summary>
        /// <param name="junk"></param>
        private bool runTick(uint elaspedTime_ms)
        {
            if (isDone)
                return false;

            lock (commandBuffer)
            {
                long elaspedTime = DataStructures.Timing.Shared.MillisecondsToTicks(elaspedTime_ms);
                if (currentCommand >= commandBuffer.Count)
                {
                    if (this.Done != null)
                    {
                        this.Done(this, new NationalInstruments.DAQmx.TaskDoneEventArgs(null));
                    }
                    isDone = true;

                    return false;
                }

                while (elaspedTime >= commandBuffer[currentCommand].commandTime)
                {

                    device.Write(commandBuffer[currentCommand].command);
                    device.Flush(BufferTypes.OutBuffer, false);

                    AtticusServer.server.messageLog(this, new MessageEvent("Output rs232 command: " + commandBuffer[currentCommand].command, 1, MessageEvent.MessageTypes.Log, MessageEvent.MessageCategories.Serial));

                    currentCommand++;
                    if (currentCommand >= commandBuffer.Count)
                        break;
                }

            }

            //            }
            //           catch (Exception e)
            //           {
            //               AtticusServer.server.messageLog(this, new MessageEvent("Caught exception in RS232 task: " + e.Message + e.StackTrace));
            //              AtticusServer.server.messageLog(this, new MessageEvent("Stopping rs232 task."));
            //              if (this.Done != null)
            //             {
            //                 this.Done(this, new NationalInstruments.DAQmx.TaskDoneEventArgs(e));
            //                isDone = true;
            //           }
            //     }
            return true;
        }

        private struct DoubleIntPair
        {
            public double myDouble;
            public int myInt;
            public DoubleIntPair(double myDouble, int myInt)
            {
                this.myDouble = myDouble;
                this.myInt = myInt;
            }
        }

        /// <summary>
        /// This method converts an array of doubles into a list of index-value pairs. 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private List<DoubleIntPair> ConvertArrayToIndexValuePairs(double[] array)
        {
            if (array == null)
                return null;
            List<DoubleIntPair> ans = new List<DoubleIntPair>();

            if (array.Length == 0)
                return ans;

            ans.Add(new DoubleIntPair(array[0], 0));
            double lastValue = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] != lastValue)
                {
                    ans.Add(new DoubleIntPair(array[i], i));
                    lastValue = array[i];
                }
            }
            return ans;
        }

        public static string AddNewlineCharacters(string input)
        {
            if (input == null)
                return null;
            return input.Replace("\\n", "\n").Replace("\\r", "\r");
        }

        private static long seconds_to_ticks(double seconds)
        {
            return (long)(seconds * 10000000);
        }

        private static double ticks_to_seconds(long ticks)
        {
            return ((double)ticks) / 10000000.0;
        }


        public bool providerTimerFinished(int priority)
        {
            return true;
        }
        
    }
}
