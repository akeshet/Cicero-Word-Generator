using System;
using System.Collections.Generic;
using System.Text;
using DataStructures;
using System.Threading;

namespace AtticusServer
{

    /// <summary>
    /// This class is not yet completed.
    /// </summary>
    class WatchdogTimerTask
    {

        NationalInstruments.DAQmx.Task taskToWatch;

        struct WatchdogTimerSegment
        {
            public string timeStepName;
            public int timeStepID;
            public int sequenceSampleNumber;
            public long masterSampleNumber;

            public override string ToString()
            {
                return timeStepName + " " + sequenceSampleNumber + "/" + masterSampleNumber;
            }
        }

        private List<WatchdogTimerSegment> watchdogTimerSegments;

        public WatchdogTimerTask(SequenceData sequence, int masterFrequency, NationalInstruments.DAQmx.Task taskToWatch,  double watchDogThresholdTime)
        {
            this.taskToWatch = taskToWatch;
            TimestepTimebaseSegmentCollection segments = sequence.generateVariableTimebaseSegments(SequenceData.VariableTimebaseTypes.AnalogGroupControlledVariableFrequencyClock,
                                         1.0 / ((double)masterFrequency));

            int currentSequenceSampleNumber = 0;
            long currentMasterSampleNumber=0;
            watchdogTimerSegments = new List<WatchdogTimerSegment>();

            int thresholdTime = (int)(masterFrequency*watchDogThresholdTime);

            for (int i = 0; i < sequence.TimeSteps.Count; i++)
            {
                TimeStep step = sequence.TimeSteps[i];
                if (step.StepEnabled)
                {

                    bool stepUsed = false;
                    if (segments.ContainsKey(step))
                    {
                        List<SequenceData.VariableTimebaseSegment> segmentList = segments[step];
                        for (int j = 0; j < segmentList.Count; j++)
                        {
                            SequenceData.VariableTimebaseSegment currentSegment = segmentList[j];

                            if (currentSegment.MasterSamplesPerSegmentSample >= thresholdTime)
                            {
                                if (!stepUsed)
                                {
                                    WatchdogTimerSegment wseg = new WatchdogTimerSegment();
                                    wseg.timeStepName = step.StepName;
                                    wseg.timeStepID = i;
                                    wseg.sequenceSampleNumber = currentSequenceSampleNumber + 1;
                                    wseg.masterSampleNumber = currentMasterSampleNumber + thresholdTime / 2;
                                    stepUsed = true;

                                    watchdogTimerSegments.Add(wseg);
                                }
                            }

                            currentSequenceSampleNumber += currentSegment.NSegmentSamples;
                            currentMasterSampleNumber += currentSegment.NSegmentSamples * currentSegment.MasterSamplesPerSegmentSample;
                        }

                    }
                }
                
            }
        }


/*
        Thread runThread;

        public void Start()
        {

            taskStartTime = DateTime.Now.Ticks;

            if (runThread != null)
            {
                if (runThread.ThreadState == ThreadState.Running)
                {
                    throw new Exception("Unable to start watchdog task, as the watchdog task running thread is already running.");
                }
            }

            currentCommand = 0;

            runThread = new Thread(new ThreadStart(runTaskProc));
            runThread.Start();

        }


        /// <summary>
        /// This function is copied from RFSGTask, with modifications.
        /// </summary>
        public void runTaskProc()
        {
            try
            {
                while (true)
                {
                    long elaspedTime = DateTime.Now.Ticks - taskStartTime;
                    if (currentCommand >= commandBuffer.Count)
                    {
                        disposeDevice();
                        if (this.Done != null)
                        {
                            this.Done(this, new NationalInstruments.DAQmx.TaskDoneEventArgs(null));
                        }
                        return;
                    }
                    while (elaspedTime >= commandBuffer[currentCommand].commandTime)
                    {
                        outputRfsgCommand(commandBuffer[currentCommand]);
                        currentCommand++;
                        if (currentCommand >= commandBuffer.Count)
                            break;

                        if (currentCommand >= commandBuffer.Count) // we've run out of new commands, so disable the timer.
                        {
                            disposeDevice();
                            if (this.Done != null)
                            {
                                this.Done(this, new NationalInstruments.DAQmx.TaskDoneEventArgs(null));
                            }
                            return;
                        }
                    }
                    Thread.Sleep(1);
                }
            }
            catch (Exception e)
            {
                if (e is ThreadAbortException)
                {
                    disposeDevice();
                    if (this.Done != null)
                    {
                        this.Done(this, new NationalInstruments.DAQmx.TaskDoneEventArgs(null));
                    }
                }
                else
                {
                    AtticusServer.server.messageLog(this, new MessageEvent("Caught an exception while running RFSG task for GPIB channel " + channelID + ": " + e.Message + e.StackTrace));
                    AtticusServer.server.messageLog(this, new MessageEvent("Aborting RFSG task."));
                    disposeDevice();
                    if (this.Done != null)
                    {
                        this.Done(this, new NationalInstruments.DAQmx.TaskDoneEventArgs(e));
                    }
                }
            }
        }*/

    }
}
