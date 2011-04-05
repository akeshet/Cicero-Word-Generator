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
    class FpgaTimebaseTask
    {

        okCUsbFrontPanel opalKellyDevice;

        private struct ListItem
        {
            public int onCounts;
            public int offCounts;
            public int repeats;

            public ListItem(int onCounts, int offCounts, int repeats)
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

        public static byte[] createByteArray(TimestepTimebaseSegmentCollection segments,
                                                SequenceData sequence, out int nSegments, double masterClockPeriod, bool assymetric)
        {
            List<ListItem> listItems = new List<ListItem>();

            for (int stepID = 0; stepID < sequence.TimeSteps.Count; stepID++)
            {
                if (sequence.TimeSteps[stepID].StepEnabled)
                {
                    if (sequence.TimeSteps[stepID].WaitForRetrigger)
                    {
                        listItems.Add(new ListItem(0, 0, 0)); // 0,0,0 list item is code for "wait for retrigger
                                                              // FPGA knows how to handle this
                    }

                    List<SequenceData.VariableTimebaseSegment> stepSegments = segments[sequence.TimeSteps[stepID]];
                    for (int i = 0; i < stepSegments.Count; i++)
                    {
                        ListItem item = new ListItem();
                        SequenceData.VariableTimebaseSegment currentSeg = stepSegments[i];

                        item.repeats = currentSeg.NSegmentSamples;
                        item.offCounts = currentSeg.MasterSamplesPerSegmentSample / 2;
                        item.onCounts = currentSeg.MasterSamplesPerSegmentSample - item.offCounts;

                        // in assymmetric mode (spelling?), the clock duty cycle is not held at 50%, but rather the pulses are made to be
                        // 5 master cycles long at most. This is a workaround for the weird behavior of one of our fiber links
                        // for sharing the variable timebase signal.
                        if (assymetric)
                        {
                            if (item.onCounts > 5)
                            {
                                int difference = item.onCounts - 5;
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

            ListItem finishItem = new ListItem(minCounts, minCounts, 1);

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

        private static byte[] splitIntToBytes(int splitMe)
        {
            byte[] ans = new byte[4];
            int one = splitMe >> 24;
            int two = (splitMe-(one << 24))>>16;
            int three = (splitMe - (one<<24) - (two<<16))>>8;
            int four = splitMe - (one << 24) - (two << 16) - (three << 8);
            ans[0] = (byte)one;
            ans[1] = (byte)two;
            ans[2] = (byte)three;
            ans[3] = (byte)four;
            return ans;
        }

        public FpgaTimebaseTask(DeviceSettings deviceSettings, okCUsbFrontPanel opalKellyDevice, SequenceData sequence, SettingsData settingsData, double masterClockPeriod, out int nSegments, bool useRfModulation, bool assymetric)
        {
            com.opalkelly.frontpanel.okCUsbFrontPanel.ErrorCode errorCode;

            this.opalKellyDevice = opalKellyDevice;

            List<int> ignoreDigitals = settingsData.getIgnoredDigitalsForVariableTimebaseGeneration(deviceSettings.VariableTimebaseIDToGenerate);
            List<int> ignoreAnalogs = settingsData.getIgnoredAnalogsForVariableTimebaseGeneration(deviceSettings.VariableTimebaseIDToGenerate);

            TimestepTimebaseSegmentCollection segments = sequence.generateVariableTimebaseSegments(SequenceData.VariableTimebaseTypes.AnalogGroupControlledVariableFrequencyClock,
                                                        masterClockPeriod, ignoreAnalogs, ignoreDigitals);

            byte[] data = FpgaTimebaseTask.createByteArray(segments, sequence, out nSegments, masterClockPeriod, assymetric );

            // Send the device an abort trigger.
            errorCode = opalKellyDevice.ActivateTriggerIn(0x40, 1);
            if (errorCode != okCUsbFrontPanel.ErrorCode.NoError)
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

            if (errorCode != okCUsbFrontPanel.ErrorCode.NoError)
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

        public void triggerMonitoringProc()
        {

        }

        public void Start()
        {
            // Send the device a start trigger.
            com.opalkelly.frontpanel.okCUsbFrontPanel.ErrorCode errorCode = opalKellyDevice.ActivateTriggerIn(0x40, 0);
            if (errorCode != okCUsbFrontPanel.ErrorCode.NoError)
            {
                throw new Exception("Unable to send software start trigger to FPGA device. " + errorCode.ToString());
            }
        }

        public int getMistriggerStatus()
        {
            opalKellyDevice.UpdateWireOuts();
            int highWord;
            int lowWord;

            lowWord = opalKellyDevice.GetWireOutValue(0x20);
            highWord = opalKellyDevice.GetWireOutValue(0x21);

            int ans = highWord;
            ans = ans << 16;
            ans = ans + lowWord;
           
            return ans;
        }

        public void Stop()
        {
            com.opalkelly.frontpanel.okCUsbFrontPanel.ErrorCode errorCode;
            // Send the device an abort trigger.
            errorCode = opalKellyDevice.ActivateTriggerIn(0x40, 1);
            if (errorCode != okCUsbFrontPanel.ErrorCode.NoError)
            {
                throw new Exception("Unable to send software stop trigger to FPGA device. " + errorCode.ToString());
            }
        }
    }
}
