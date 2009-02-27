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
        struct WatchdogTimerSegment
        {
            public string timeStepName;
            public int timeStepID;
            public int sequenceSampleNumber;
            public long masterSampleNumber;
        }

        private List<WatchdogTimerSegment> watchdogTimerSegments;

        public WatchdogTimerTask(SequenceData sequence, int masterFrequency)
        {
            Dictionary<TimeStep, List<SequenceData.VariableTimebaseSegment>> segments = sequence.generateVariableTimebaseSegments(SequenceData.VariableTimebaseTypes.AnalogGroupControlledVariableFrequencyClock,
                                         1.0 / ((double)masterFrequency));

            int currentSequenceSampleNumber = 0;
            long currentMasterSampleNumber=0;
            watchdogTimerSegments = new List<WatchdogTimerSegment>();

            int thresholdTime = masterFrequency / 5;

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

        long taskStartTime;
        
     /*   public void Start()
        {

            taskStartTime = DateTime.Now.Ticks;
            currentCommand = 0;

            runThread = new Thread(new ThreadStart(runTaskProc));
            runThread.Start();

        }

        private void runTaskProc() {

        }
       */     
    }
}
