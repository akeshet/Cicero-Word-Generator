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

        public static byte[] createByteArray(Dictionary<TimeStep, List<SequenceData.VariableTimebaseSegment>> segments,
                                                SequenceData sequence)
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

        public FpgaTimebaseTask(DeviceSettings deviceSettings, okCUsbFrontPanel opalKellyDevice, SequenceData sequence, double masterClockPeriod)
        {
            this.opalKellyDevice = opalKellyDevice;

            Dictionary<TimeStep, List<SequenceData.VariableTimebaseSegment>> segments = sequence.generateVariableTimebaseSegments(SequenceData.VariableTimebaseTypes.AnalogGroupControlledVariableFrequencyClock,
                                                        masterClockPeriod);

            byte[] data = FpgaTimebaseTask.createByteArray(segments, sequence);

            // Send the device an abort trigger.
            opalKellyDevice.ActivateTriggerIn(0x40, 1);

            // Tell the device whether to wait for a hardware trigger.
            if (deviceSettings.StartTriggerType == DeviceSettings.TriggerType.SoftwareTrigger)
            {

                opalKellyDevice.SetWireInValue(0x00, 0x00);
            }
            else
            {
                opalKellyDevice.SetWireInValue(0x00, 0xFF);   
            }

            // pipe the byte stream to the device
            opalKellyDevice.WriteToPipeIn(0x80, data.Length, data);
        }

        public void Start()
        {
            // Send the device a start trigger.
            opalKellyDevice.ActivateTriggerIn(0x40, 0);
        }

        public void Stop()
        {
            // Send the device an abort trigger.
            opalKellyDevice.ActivateTriggerIn(0x40, 1);
        }
    }
}
