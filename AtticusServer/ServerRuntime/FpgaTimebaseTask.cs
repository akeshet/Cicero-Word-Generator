using System;
using System.Collections.Generic;
using System.Text;
using DataStructures;
using com.opalkelly.frontpanel;

namespace AtticusServer
{
    class FpgaTimebaseTask
    {

        okCUsbFrontPanel opalKellyDevice;

        private struct ListItem
        {
            public int onCounts;
            public int offCounts;
            public int repeats;
        }

        public static byte[] createByteArray(TimestepTimebaseSegmentCollection segments,
                                                SequenceData sequence, out int nSegments)
        {
            List<ListItem> listItems = new List<ListItem>();

            for (int stepID = 0; stepID < sequence.TimeSteps.Count; stepID++)
            {
                if (sequence.TimeSteps[stepID].StepEnabled)
                {
                    List<SequenceData.VariableTimebaseSegment> stepSegments = segments[sequence.TimeSteps[stepID]];
                    for (int i = 0; i < stepSegments.Count; i++)
                    {
                        ListItem item = new ListItem();
                        SequenceData.VariableTimebaseSegment currentSeg = stepSegments[i];

                        item.repeats = currentSeg.NSegmentSamples;
                        item.offCounts = currentSeg.MasterSamplesPerSegmentSample / 2;
                        item.onCounts = currentSeg.MasterSamplesPerSegmentSample - item.offCounts;

                        listItems.Add(item);
                    }
                }
            }



            // Add one final "pulse" at the end to trigger the dwell values. I'm basing this off the
            // old variable timebase code that I found in the SequenceData program. Pretty sure it is right,
            // and is unlikely to hurt. However, I do wonder if such short on pulses might not always be
            // recognized by the NI cards.

            ListItem finishItem = new ListItem();
            finishItem.onCounts = 1;
            finishItem.offCounts = 1;
            finishItem.repeats = 1;
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

        public FpgaTimebaseTask(DeviceSettings deviceSettings, okCUsbFrontPanel opalKellyDevice, SequenceData sequence, double masterClockPeriod, out int nSegments)
        {
            com.opalkelly.frontpanel.okCUsbFrontPanel.ErrorCode errorCode;

            this.opalKellyDevice = opalKellyDevice;

            TimestepTimebaseSegmentCollection segments = sequence.generateVariableTimebaseSegments(SequenceData.VariableTimebaseTypes.AnalogGroupControlledVariableFrequencyClock,
                                                        masterClockPeriod);

            byte[] data = FpgaTimebaseTask.createByteArray(segments, sequence, out nSegments);

            // Send the device an abort trigger.
            errorCode = opalKellyDevice.ActivateTriggerIn(0x40, 1);
            if (errorCode != okCUsbFrontPanel.ErrorCode.NoError)
            {
                throw new Exception("Unable to set abort trigger to FPGA device. Error code " + errorCode.ToString());
            }

            // Tell the device whether to wait for a hardware trigger.
            if (deviceSettings.StartTriggerType == DeviceSettings.TriggerType.SoftwareTrigger)
            {

                errorCode = opalKellyDevice.SetWireInValue(0x00, 0x00);

            }
            else
            {
                errorCode = opalKellyDevice.SetWireInValue(0x00, 0xFF);   
            }

            if (errorCode != okCUsbFrontPanel.ErrorCode.NoError)
            {
                throw new Exception("Unable to send a wire in value to FPGA device. Error code " + errorCode.ToString());
            }

            // pipe the byte stream to the device
            int xfered = opalKellyDevice.WriteToPipeIn(0x80, data.Length, data);
            if (xfered != data.Length)
            {
                throw new Exception("Error when piping clock data to FPGA device. Sent " + xfered + " bytes instead of " + data.Length + "bytes.");
            }
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
