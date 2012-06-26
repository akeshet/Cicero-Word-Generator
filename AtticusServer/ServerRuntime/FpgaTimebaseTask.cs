using System;
using System.Collections.Generic;
using System.Text;
using DataStructures;
using com.opalkelly.frontpanel;
using System.Threading;

namespace AtticusServer
{
    /// <summary>
    /// Task that generated a variable timebase using an OpalKelly FPGA.
    /// </summary>
    class FpgaTimebaseTask : DataStructures.Timing.PollingThreadSoftwareClockProvider
    {
        private object lockObj = new object();

        private double masterClockPeriod;

        private okCFrontPanel opalKellyDevice;

        public class FpgaTimebaseGenerationException : Exception
        {

        }
        

        private struct ListItem
        {
            public uint onCounts;
            public uint offCounts;
            public uint repeats;

            public ListItem(uint onCounts, uint offCounts, uint repeats)
            {
                this.onCounts = onCounts;
                this.offCounts = offCounts;
                this.repeats = repeats;
            }

            public bool isAllZeros()
            {
                if (onCounts == 0 && offCounts == 0 && repeats == 0)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// Create byte array for use in programming FPGA
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="sequence"></param>
        /// <param name="nSegments"></param>
        /// <param name="masterClockPeriod"></param>
        /// <param name="assymetric"></param>
        /// <returns></returns>
        private static byte[] createByteArray(TimestepTimebaseSegmentCollection segments,
                                                SequenceData sequence, out int nSegments, double masterClockPeriod, bool assymetric)
        {
            List<ListItem> listItems = new List<ListItem>();

            for (int stepID = 0; stepID < sequence.TimeSteps.Count; stepID++)
            {
                if (sequence.TimeSteps[stepID].StepEnabled)
                {
                    if (sequence.TimeSteps[stepID].WaitForRetrigger)
                    {
                        uint waitTime = (uint) (sequence.TimeSteps[stepID].RetriggerTimeout.getBaseValue() / masterClockPeriod);
                        
                        uint retriggerFlags = 0;
                        if (sequence.TimeSteps[stepID].RetriggerOnEdge)
                            retriggerFlags += 1;
                        if (!sequence.TimeSteps[stepID].RetriggerOnNegativeValueOrEdge)
                            retriggerFlags += 2;
                        
                        listItems.Add(new ListItem(waitTime, retriggerFlags, 0));
                               // counts = 0 is a special signal for WAIT_FOR_RETRIGGER mode
                               // in this mode, FPGA waits a maximum of on_counts master samples
                               // before moving on anyway.
                               // (unless on_counts = 0, in which case it never artificially retriggers)
                                // retrigger flags set if the FPGA will trigger on edge or on value
                                // and whether to trigger on positive or negative (edge or value)
                    }

                    List<SequenceData.VariableTimebaseSegment> stepSegments = segments[sequence.TimeSteps[stepID]];
                    for (int i = 0; i < stepSegments.Count; i++)
                    {
                        ListItem item = new ListItem();
                        SequenceData.VariableTimebaseSegment currentSeg = stepSegments[i];

                        item.repeats = (uint)currentSeg.NSegmentSamples;
                        item.offCounts = (uint)(currentSeg.MasterSamplesPerSegmentSample / 2);
                        item.onCounts = (uint)(currentSeg.MasterSamplesPerSegmentSample - item.offCounts);

                        // in assymmetric mode (spelling?), the clock duty cycle is not held at 50%, but rather the pulses are made to be
                        // 5 master cycles long at most. This is a workaround for the weird behavior of one of our fiber links
                        // for sharing the variable timebase signal.
                        if (assymetric)
                        {
                            if (item.onCounts > 5)
                            {
                                uint difference = item.onCounts - 5;
                                item.onCounts = 5;
                                item.offCounts = item.offCounts + difference;
                            }
                        }

                        if (!item.isAllZeros())
                        { // filter out any erroneously produced all-zero codes, since these have
                            // special meaning to the FPGA (they are "wait for retrigger" codes
                            listItems.Add(item);
                        }
                    }
                }
            }



            // Add one final "pulse" at the end to trigger the dwell values. I'm basing this off the
            // old variable timebase code that I found in the SequenceData program. 

            // This final pulse is made to be 100 us long at least, just to be on the safe side. (unless assymetric mode is on)

            int minCounts = (int)(0.0001 / masterClockPeriod);
            if (minCounts <= 0)
                minCounts = 1;

            ListItem finishItem = new ListItem((uint)minCounts, (uint)minCounts, 1);

            if (assymetric)
            {
                finishItem.onCounts = 5;
            }

            listItems.Add(finishItem);


            nSegments = listItems.Count;

            byte[] byteArray = new byte[listItems.Count * 16];


            // This loop goes through the list items and creates
            // the data as it is to be sent to the FPGA
            // the data is a little shuffled because
            // of the details of the byte order in 
            // piping data to the fpga.
            
            // Each list item takes up 16 bytes in the output FIFO.
            for (int i = 0; i < listItems.Count; i++)
            {
                ListItem item = listItems[i];

                byte[] onb = splitIntToBytes(item.onCounts);
                byte[] offb = splitIntToBytes(item.offCounts);
                byte[] repb = splitIntToBytes(item.repeats);

                int offs = 16 * i;

                byteArray[offs + 2] = onb[1];
                byteArray[offs + 3] = onb[0];
                byteArray[offs + 4] = onb[3];
                byteArray[offs + 5] = onb[2];

                byteArray[offs + 8] = offb[1];
                byteArray[offs + 9] = offb[0];
                byteArray[offs + 10] = offb[3];
                byteArray[offs + 11] = offb[2];

                byteArray[offs + 12] = repb[1];
                byteArray[offs + 13] = repb[0];
                byteArray[offs + 14] = repb[3];
                byteArray[offs + 15] = repb[2];
            }

            return byteArray;

        }

        private static byte[] splitIntToBytes(uint splitMe)
        {
            byte[] ans = new byte[4];
            uint one = splitMe >> 24;
            uint two = (splitMe-(one << 24))>>16;
            uint three = (splitMe - (one<<24) - (two<<16))>>8;
            uint four = splitMe - (one << 24) - (two << 16) - (three << 8);
            ans[0] = (byte)one;
            ans[1] = (byte)two;
            ans[2] = (byte)three;
            ans[3] = (byte)four;
            return ans;
        }

        private UInt32 max_elapsedtime_ms;

        public FpgaTimebaseTask(DeviceSettings deviceSettings, okCFrontPanel opalKellyDevice, SequenceData sequence, double masterClockPeriod, out int nSegments, bool useRfModulation, bool assymetric)
            : base()
        {
            com.opalkelly.frontpanel.okCFrontPanel.ErrorCode errorCode;

            this.opalKellyDevice = opalKellyDevice;

            this.masterClockPeriod = masterClockPeriod;

            TimestepTimebaseSegmentCollection segments = sequence.generateVariableTimebaseSegments(SequenceData.VariableTimebaseTypes.AnalogGroupControlledVariableFrequencyClock,
                                                        masterClockPeriod);

            this.max_elapsedtime_ms = (UInt32)((sequence.SequenceDuration * 1000.0) + 100);

            byte[] data = FpgaTimebaseTask.createByteArray(segments, sequence, out nSegments, masterClockPeriod, assymetric );

            // Send the device an abort trigger.
            errorCode = opalKellyDevice.ActivateTriggerIn(0x40, 1);
            if (errorCode != okCFrontPanel.ErrorCode.NoError)
            {
                throw new Exception("Unable to set abort trigger to FPGA device. Error code " + errorCode.ToString());
            }

            UInt16 wireInValue = 0;
            if (deviceSettings.StartTriggerType != DeviceSettings.TriggerType.SoftwareTrigger)
            {
                wireInValue += 1;
            }

            if (useRfModulation)
            {
                wireInValue += 2;
            }

            errorCode = opalKellyDevice.SetWireInValue(0x00, wireInValue);

            if (errorCode != okCFrontPanel.ErrorCode.NoError)
            {
                throw new Exception("Unable to send a wire in value to FPGA device. Error code " + errorCode.ToString());
            }


            opalKellyDevice.UpdateWireIns();

            // pipe the byte stream to the device
            int xfered = opalKellyDevice.WriteToPipeIn(0x80, data.Length, data);
            if (xfered != data.Length)
            {
                throw new Exception("Error when piping clock data to FPGA device. Sent " + xfered + " bytes instead of " + data.Length + "bytes.");
            }



        }


        /// <summary>
        ///  Must call updateWireOuts before using.
        /// </summary>
        /// <returns></returns>
        private UInt32 getMasterSamplesGenerated()
        {
            return extractUInt32FromAddresses(0x22, 0x23);
        }

        private uint extractUInt32FromAddresses(Int32 lowWordAddr, Int32 highWordAddr)
        {
            UInt32 lowWord;
            UInt32 highWord;

            lock (lockObj)
            {
                lowWord = opalKellyDevice.GetWireOutValue(lowWordAddr);
                highWord = opalKellyDevice.GetWireOutValue(highWordAddr);
            }

            UInt32 ans = highWord;
            ans = ans << 16;
            ans = ans + lowWord;

            return ans;
        }

        /// <summary>
        ///  Must call updateWireOuts before using.
        /// </summary>
        /// <returns></returns>
        private UInt32 getSamplesWaitedForRetrigger()
        {
            return extractUInt32FromAddresses(0x26, 0x27);
        }

        /// <summary>
        ///  Must call updateWireOuts before using.
        /// </summary>
        /// <returns></returns>
        private UInt16 getRetriggerTimeoutCount()
        {
            return (UInt16) opalKellyDevice.GetWireOutValue(0x24);
        }

        private object startedLockObj = new object();
        private bool started = false;

        public void Start()
        {
            lock (lockObj)
            {
                // Send the device a start trigger.
                com.opalkelly.frontpanel.okCFrontPanel.ErrorCode errorCode = opalKellyDevice.ActivateTriggerIn(0x40, 0);
                if (errorCode != okCFrontPanel.ErrorCode.NoError)
                {
                    throw new Exception("Unable to send software start trigger to FPGA device. " + errorCode.ToString());
                }
                lock (startedLockObj)
                {
                    started = true;
                    Monitor.PulseAll(startedLockObj);
                }
            }
        }

        

        public uint getMistriggerStatus()
        {
            lock (lockObj)
                opalKellyDevice.UpdateWireOuts();

            return extractUInt32FromAddresses(0x20, 0x21);
        }

        private FpgaStatus getFpgaStatus()
        {
            uint statusRegister;
            lock (lockObj)
                statusRegister = opalKellyDevice.GetWireOutValue(0x25);

            FpgaStatus status;
            status.Finished = ((statusRegister & 1)!=0);
            status.Aborted = ((statusRegister & 2)!=0);
            return status;
        }

        private struct FpgaStatus
        {
            public bool Aborted;
            public bool Finished;
        }

        public void Stop()
        {
            lock (lockObj)
            {
                com.opalkelly.frontpanel.okCFrontPanel.ErrorCode errorCode;
                // Send the device an abort trigger.
                errorCode = opalKellyDevice.ActivateTriggerIn(0x40, 1);
                if (errorCode != okCFrontPanel.ErrorCode.NoError)
                {
                    throw new Exception("Unable to send software stop trigger to FPGA device. " + errorCode.ToString());
                }
            }
        }

        protected override void armTimerThread()
        {
            // nothing required
        }

        protected override void timerThreadProc()
        {
            uint lastmSamp=0;
            bool keepGoing = true;
            int rollovers = 0;
            uint rolloverTime = (uint)(UInt32.MaxValue * this.masterClockPeriod * 1000.0);

            // Wait for the started flag to get set (when Start() is called on Fpga task
            while (true)
            {
                lock (startedLockObj)
                {
                    if (started)
                        break;
                    Monitor.Wait(startedLockObj);
                }
            }


            while (keepGoing)
            {
                uint mSamp;
                FpgaStatus status;
                lock (lockObj)
                {
                    opalKellyDevice.UpdateWireOuts();
                    mSamp = getMasterSamplesGenerated();
                    status = getFpgaStatus();
                }

                if (status.Finished && lastmSamp >0)
                {
                    reachTime(max_elapsedtime_ms);
                    keepGoing = false;
                    continue;
                }


                if (mSamp < lastmSamp) // this may occur for very long sequences (about 429 seconds at least, if FPGA running at 10Mhz)
                {                       // or if the fpga finished a run (in which case master sample will return 0)
                    rollovers++;
                }

                if (mSamp == lastmSamp)
                    continue;
                lastmSamp = mSamp;

                uint nowTime = (uint)(mSamp * this.masterClockPeriod * 1000.0) + (uint)(rollovers * rolloverTime);

                keepGoing = reachTime(nowTime);
                Thread.Sleep(5);
            }
        }

        public FpgaRunReport getRunReport()
        {
            FpgaRunReport ans = new FpgaRunReport();
            opalKellyDevice.UpdateWireOuts();
            ans.retriggerWaitedSamples = this.getSamplesWaitedForRetrigger();
            ans.retriggerTimeoutCount = this.getRetriggerTimeoutCount();

            return ans;
        }

        
    }
}
