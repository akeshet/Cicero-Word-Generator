using System;
using System.Collections.Generic;
using System.Text;
using NationalInstruments.DAQmx;
using DataStructures;

namespace AtticusServer
{
    public class DaqMxTaskGenerator
    {


        /// <summary>
        /// This method is a sort of combination of createDaqMxVariableTimebaseSource and createDaqMxTask. It is intended 
        /// for use to create a digital output task that has both a variable timebase source on it, without having to discard
        /// all of the other channels on that port (and possibly on its neighboring port).
        /// 
        /// NOTE: No longer true. This function can not create the variable timebase outputs at the same time as
        /// normal outputs. If you attempt to use digital outputs on the same half of the card as your variable timebase output,
        /// this method will complain.
        /// </summary>
        /// <param name="channelName">
        /// Name of the channel that will output the variable timebase clock.
        /// </param>
        /// <param name="portsToUse">
        /// A list of integers specifying the digital ports that this task will use. The task will automatically
        /// make use of the full port that the variable timebase clock belongs to. If portsToUse is null, then 
        /// this function will automatically use both this port and its neighboring port (0 with 1, 2 with 3, etc).
        /// The rationale for this is that on some NI devices, these pairs of ports will share a sample clock and 
        /// cannot truly be used independently.
        /// </param>
        /// <param name="masterFrequency">
        /// The frequency, in hertz, of the master clock which will drive the variable timebase clock and the rest of the
        /// channels in this task.
        /// </param>
        /// <param name="sequenceData"></param>
        /// <param name="timebaseType"></param>
        /// <param name="deviceName"></param>
        /// <param name="deviceSettings"></param>
        /// <param name="sequence"></param>
        /// <param name="settings"></param>
        /// <param name="usedDigitalChannels"></param>
        /// <param name="usedAnalogChannels"></param>
        /// <param name="serverSettings"></param>
        /// <returns></returns>
        public static Task createDaqMxDigitalOutputAndVariableTimebaseSource(string channelName, List<int> portsToUse, int masterFrequency,
            SequenceData sequenceData, SequenceData.VariableTimebaseTypes timebaseType,
            string deviceName, DeviceSettings deviceSettings, SettingsData settings, Dictionary<int, HardwareChannel> usedDigitalChannels, ServerSettings serverSettings)
        {

            // First generate the variable timebase buffer. We will need stuff like its length for configuring the task, which is why we do this first.

            TimestepTimebaseSegmentCollection timebaseSegments = sequenceData.generateVariableTimebaseSegments(timebaseType, Common.getPeriodFromFrequency(masterFrequency));
            bool[] variableTimebaseBuffer = sequenceData.getVariableTimebaseClock(timebaseSegments);


            if (deviceName.ToUpper() != HardwareChannel.parseDeviceNameStringFromPhysicalChannelString(channelName).ToUpper())
            {
                throw new Exception("The variable timebase device " + HardwareChannel.parseDeviceNameStringFromPhysicalChannelString(channelName) + " does not match device " + deviceName + ". These must match for their their task to be created together.");
            }

            int timebasePortNum;
            int timebaseLineNum;

            try
            {
                timebasePortNum = HardwareChannel.parsePortNumberFromChannelString(channelName);
                timebaseLineNum = HardwareChannel.parseLineNumberFromChannelString(channelName);
            }
            catch (Exception)
            {
                throw new Exception("Channel name " + channelName + " is not a valid digital channel name. Cannot create a variable timebase output on this channel.");
            }



            if (portsToUse == null)
            {
                portsToUse = new List<int>();
                portsToUse.Add(timebasePortNum);
                int spousePort; // this port is likely to have a shared sample clock with timebasePortNum,
                // at least in my experience so far. 
                if (timebasePortNum % 2 == 0)
                    spousePort = timebasePortNum + 1;
                else
                    spousePort = timebasePortNum - 1;

                portsToUse.Add(spousePort);
            }

            if (!portsToUse.Contains(timebasePortNum))
            {
                portsToUse.Add(timebasePortNum);
            }

            bool otherChannelsUsedOnUsedPort = false;
            foreach (HardwareChannel hc in usedDigitalChannels.Values)
            {
                if (hc.DeviceName.ToUpper() == deviceName.ToUpper())
                {
                    if (portsToUse.Contains(hc.daqMxDigitalPortNumber()))
                        otherChannelsUsedOnUsedPort = true;
                }
            }

            if (otherChannelsUsedOnUsedPort)
            {
                throw new Exception("Variable timebase channel is on a port that shares a sample clock with a used output channel (on most devices, port 0 and 1 have a shared clock, and port 2 and 3 have a shared clock). This usage is not recommended, and not currently supported. Aborting buffer generation.");

                #region Deprecated code

                /*
                Task task = new Task("Variable timebase output task");

                // Create channels in the task
                foreach (int portNum in portsToUse)
                {
                    task.DOChannels.CreateChannel(deviceName + '/' + HardwareChannel.digitalPhysicalChannelName(portNum), "", ChannelLineGrouping.OneChannelForAllLines);
                }

                // Configure the task...

                task.Timing.ConfigureSampleClock("", masterFrequency, SampleClockActiveEdge.Rising, SampleQuantityMode.FiniteSamples, variableTimebaseBuffer.Length);

                if (serverSettings.VariableTimebaseTriggerInput != "")
                {
                    task.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(serverSettings.VariableTimebaseTriggerInput, DigitalEdgeStartTriggerEdge.Rising);
                }


                // Figure out which ports we are going to use, and which digital ID each line on each of those ports 
                // maps to. 
                Dictionary<int, HardwareChannel> digitalChannelsToUse = getChannelsOnDevice(usedDigitalChannels, deviceName);
                List<int> temp = new List<int>(digitalChannelsToUse.Keys);
                foreach (int id in temp)
                {
                    HardwareChannel ch = digitalChannelsToUse[id];
                    if (!portsToUse.Contains(HardwareChannel.parsePortNumberFromChannelString(ch.ChannelName)))
                    {
                        digitalChannelsToUse.Remove(id);
                    }
                }

                // Remove all of the digital channels this buffer generation is consuming from the
                // usedDigitalChannels dictionary. Why? Because there may still be another task to 
                // generate on this device, and this is a way of keeping track of which channels 
                // have already had their buffers generated.
                // Since digitalChannelsToUse gets passed by reference from AtticusServerRuntime.generateBuffers(...),
                // these changes thus get communicated back to AtticusServerRuntime.
                foreach (HardwareChannel hc in digitalChannelsToUse.Values)
                {
                    foreach (int i in usedDigitalChannels.Keys)
                    {
                        if (usedDigitalChannels[i] == hc)
                        {
                            usedDigitalChannels.Remove(i);
                            break;
                        }
                    }
                }

                List<int> ids = new List<int>(digitalChannelsToUse.Keys);
                ids.Sort();
                List<HardwareChannel> hcs = new List<HardwareChannel>();
                foreach (int id in ids)
                {
                    hcs.Add(digitalChannelsToUse[id]);
                }

                Dictionary<int, int[]> port_digital_ids;
                List<int> usedPorts;

                groupDigitalChannels(ids, hcs, out port_digital_ids, out usedPorts);



                // now to generate the buffers.


                if (usedPorts.Count != 0)
                {
                    byte[,] digitalBuffer;


                    try
                    {
                        digitalBuffer = new byte[usedPorts.Count, variableTimebaseBuffer.Length];
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Unable to allocate digital buffer for device " + deviceName + ". Reason: " + e.Message + "\n" + e.StackTrace);
                    }

                    for (int i = 0; i < usedPorts.Count; i++)
                    {
                        int portNum = usedPorts[i];
                        byte digitalBitMask = 1;
                        for (int lineNum = 0; lineNum < 8; lineNum++)
                        {
                            bool[] singleChannelBuffer = null;

                            if (portNum == timebasePortNum && lineNum == timebaseLineNum)
                            { // this current line is the variable timebase...
                                singleChannelBuffer = variableTimebaseBuffer;
                            }
                            else
                            {
                                int digitalID = port_digital_ids[portNum][lineNum];
                                if (digitalID != -1)
                                {
                                    if (settings.logicalChannelManager.Digitals[digitalID].overridden)
                                    {
                                        singleChannelBuffer = new bool[variableTimebaseBuffer.Length];
                                        if (settings.logicalChannelManager.Digitals[digitalID].digitalOverrideValue)
                                        {
                                            for (int j = 0; j < singleChannelBuffer.Length; j++)
                                            {
                                                singleChannelBuffer[j] = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        singleChannelBuffer = sequenceData.getDigitalBufferClockSharedWithVariableTimebaseClock(timebaseSegments, digitalID, 1.0 / deviceSettings.SampleClockRate);
                                    }
                                }
                            }

                            if (singleChannelBuffer != null)
                            {
                                for (int j = 0; j < singleChannelBuffer.Length; j++)
                                {
                                    if (singleChannelBuffer[j])
                                    {
                                        digitalBuffer[i, j] |= digitalBitMask;
                                    }
                                }
                            }
                            digitalBitMask = (byte)(digitalBitMask << 1);

                        }
                    }
                    System.GC.Collect();
                    DigitalMultiChannelWriter writer = new DigitalMultiChannelWriter(task.Stream);
                    writer.WriteMultiSamplePort(false, digitalBuffer);

                   
                }


                return task; */

                #endregion
            }
            else
            {
                return createDaqMxVariableTimebaseSource(
                    channelName, masterFrequency, sequenceData, timebaseType, serverSettings, deviceSettings);
            }
        }

        /// <summary>
        /// Creates a task for a variable timebase output. Consumes the entire port (8 bits) that the timebase is on. (ie outputs the
        /// signal on all 8 bits
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="masterFrequency"></param>
        /// <param name="sequenceData"></param>
        /// <param name="timebaseType"></param>
        /// <returns></returns>
        public static Task createDaqMxVariableTimebaseSource(string channelName, int masterFrequency, SequenceData sequenceData,
            SequenceData.VariableTimebaseTypes timebaseType, ServerSettings serverSettings, DeviceSettings deviceSettings)
        {
            Task task = new Task("Variable timebase output task");

            TimestepTimebaseSegmentCollection timebaseSegments = sequenceData.generateVariableTimebaseSegments(timebaseType,
                Common.getPeriodFromFrequency(masterFrequency));

            bool[] buffer = sequenceData.getVariableTimebaseClock(timebaseSegments);

            string timebaseDeviceName = HardwareChannel.parseDeviceNameStringFromPhysicalChannelString(channelName);

            string timebasePort = HardwareChannel.parsePortStringFromChannelString(channelName);

            task.DOChannels.CreateChannel(timebasePort, "", ChannelLineGrouping.OneChannelForAllLines);

            task.Timing.ConfigureSampleClock("", (double)masterFrequency, deviceSettings.ClockEdge, SampleQuantityMode.FiniteSamples, buffer.Length);

            if (serverSettings.VariableTimebaseTriggerInput != "")
            {
                task.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(serverSettings.VariableTimebaseTriggerInput, DigitalEdgeStartTriggerEdge.Rising);
            }

            DigitalSingleChannelWriter writer = new DigitalSingleChannelWriter(task.Stream);

            byte[] byteBuffer = new byte[buffer.Length];
            for (int j = 0; j < buffer.Length; j++)
            {
                if (buffer[j])
                {
                    byteBuffer[j] = 255;
                }
            }

            writer.WriteMultiSamplePort(false, byteBuffer);


            return task;
        }





        /*
        /// <summary>
        /// This class contains both the analog and the digital tasks returned from a specific device. 
        /// The reason for its existence is that if a device is to have both its analog and its digital outputs used,
        /// then they must exist in separate tasks.
        /// </summary>
        /// 
        public class TaskCollection
        {
            public Task analogTask;
            public Task digitalTask;
        }
        */

        /*
        public static Task createVariableTimebaseTask(string digitalTimebaseOutLine, string analogTimebaseOutLine)
        {

        }*/

        ///
        /// This method creates a daqMX task for an "output now" command, and starts the output.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="settings"></param>
        /// <param name="output"></param>
        /// <returns></returns>


        public static Task createDaqMxTaskAndOutputNow(AtticusServerCommunicator sender, string deviceName, DeviceSettings deviceSettings, SingleOutputFrame output,
            SettingsData settings, Dictionary<int, HardwareChannel> usedDigitalChannels, Dictionary<int, HardwareChannel> usedAnalogChannels)
        {

            Task task = new Task(deviceName + " output task");

            List<int> analogIDs;
            List<HardwareChannel> analogs;
            Dictionary<int, int[]> port_digital_IDs;
            List<int> usedPortNumbers;


            // Parse and create channels.
            parseAndCreateChannels(sender, deviceName, deviceSettings, usedDigitalChannels, usedAnalogChannels, task, out analogIDs, out analogs, out port_digital_IDs, out usedPortNumbers);


            // now create buffer.

            task.Timing.SampleTimingType = SampleTimingType.OnDemand;

            // analog output
            if (analogIDs.Count != 0)
            {




                // extract a list of analog values corresponding to the list analodIDs. This is 
                // sorted in the same way as the channels were created in parseAndCreateChannels
                List<double> outputValues = new List<double>();
                foreach (int analogID in analogIDs)
                {
                    double val;
                    if (output.analogValues.ContainsKey(analogID))
                        val = output.analogValues[analogID];
                    else
                        val = 0;
                    outputValues.Add(val);
                }

                AnalogMultiChannelWriter writer = new AnalogMultiChannelWriter(task.Stream);
                writer.WriteSingleSample(true, outputValues.ToArray());
            }

            // digital output

            if (usedPortNumbers.Count != 0)
            {
                if (deviceSettings.DigitalHardwareStructure[0] == 8)
                {
                    List<byte> outputValues = new List<byte>();

                    foreach (int portNumber in usedPortNumbers)
                    {
                        byte digitalMask = 1;
                        byte value = 0;
                        for (int lineNum = 0; lineNum < deviceSettings.DigitalHardwareStructure[portNumber]; lineNum++)
                        {
                            int digitalID = port_digital_IDs[portNumber][lineNum];
                            if (digitalID != -1)
                            {
                                bool val = false;
                                if (output.digitalValues.ContainsKey(digitalID))
                                    val = output.digitalValues[digitalID];

                                if (val)
                                    value |= digitalMask;
                            }
                            digitalMask = (byte)(digitalMask << 1);
                        }

                        outputValues.Add(value);
                    }

                    DigitalMultiChannelWriter writer = new DigitalMultiChannelWriter(task.Stream);


                    writer.WriteSingleSamplePort(true, outputValues.ToArray());
                    // writer.WriteSingleSamplePort(
                }
                else if (deviceSettings.DigitalHardwareStructure[0] == 32)
                {

                    List<UInt32> outputValues = new List<UInt32>();

                    foreach (int portNumber in usedPortNumbers)
                    {
                        UInt32 digitalMask = 1;
                        UInt32 value = 0;
                        for (int lineNum = 0; lineNum < deviceSettings.DigitalHardwareStructure[portNumber]; lineNum++)
                        {
                            int digitalID = port_digital_IDs[portNumber][lineNum];
                            if (digitalID != -1)
                            {
                                bool val = false;
                                if (output.digitalValues.ContainsKey(digitalID))
                                    val = output.digitalValues[digitalID];

                                if (val)
                                    value |= digitalMask;
                            }
                            digitalMask = (UInt32)(digitalMask << 1);
                        }

                        outputValues.Add(value);
                    }

                    DigitalMultiChannelWriter writer = new DigitalMultiChannelWriter(task.Stream);
                    writer.WriteSingleSamplePort(true, outputValues.ToArray());
                }
                else
                {
                    throw new Exception("The number of ports per line on " + deviceSettings.DeviceName + " is not 32 or 8. This could be because you have bound digital outputs to software times channels.");
                }

            }



            return task;


        }


        /// <summary>
        /// This method creates analog and digital output buffers for daqMx cards. Note that the daqmx library seems to only support
        /// either analog OR digital on a given card at one time. Despite the fact that this method will create both types of buffers,
        /// it will probably throw some daqMX level exceptions if asked to create both analog and digital buffers for the same device.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="deviceSettings"></param>
        /// <param name="sequence"></param>
        /// <param name="settings"></param>
        /// <param name="usedDigitalChannels">digital channels which reside on this server.</param>
        /// <param name="usedAnalogChannels">analog channels which reside on this server</param>
        /// <returns></returns>
        public static Task createDaqMxTask(AtticusServerCommunicator sender, string deviceName, DeviceSettings deviceSettings, SequenceData sequence,
            SettingsData settings, Dictionary<int, HardwareChannel> usedDigitalChannels, Dictionary<int, HardwareChannel> usedAnalogChannels,
            ServerSettings serverSettings, out long expectedSamplesGenerated)
        {
            expectedSamplesGenerated = 0;

            Task task = new Task(deviceName + " output task");

            Task DOtask = new Task(deviceName + " output DO task");

            List<int> analogIDs;
            List<HardwareChannel> analogs;
            Dictionary<int, int[]> port_digital_IDs;
            List<int> usedPortNumbers;

            //boolean to check for 6363 device, which requires 32lines per port

            // Parse and create channels.
            parseAndCreateChannels(sender, deviceName, deviceSettings, usedDigitalChannels, usedAnalogChannels, task, out analogIDs, out analogs, out port_digital_IDs, out usedPortNumbers);



            if (analogIDs.Count != 0)
            {
                if (deviceSettings.UseCustomAnalogTransferSettings)
                {
                    task.AOChannels.All.DataTransferMechanism = deviceSettings.AnalogDataTransferMechanism;
                    task.AOChannels.All.DataTransferRequestCondition = deviceSettings.AnalogDataTransferCondition;
                }
            }
            if (usedPortNumbers.Count != 0)
            {
                if (deviceSettings.UseCustomDigitalTransferSettings)
                {
                    task.DOChannels.All.DataTransferMechanism = deviceSettings.DigitalDataTransferMechanism;
                    task.DOChannels.All.DataTransferRequestCondition = deviceSettings.DigitalDataTransferCondition;
                }
            }

            // ok! now create the buffers

            #region NON variable timebase buffer
            if (deviceSettings.UsingVariableTimebase == false)
            {
                // non "variable timebase" buffer creation


                double timeStepSize = Common.getPeriodFromFrequency(deviceSettings.SampleClockRate);
                int nBaseSamples = sequence.nSamples(timeStepSize);

                // for reasons that are utterly stupid and frustrating, the DAQmx libraries seem to prefer sample
                // buffers with lengths that are a multiple of 4. (otherwise they, on occasion, depending on the parity of the 
                // number of channels, throw exceptions complaining.
                // thus we add a few filler samples at the end of the sequence which parrot back the last sample.

                int nFillerSamples = 4 - nBaseSamples % 4;
                if (nFillerSamples == 4)
                    nFillerSamples = 0;

                int nSamples = nBaseSamples + nFillerSamples;

                if (deviceSettings.MySampleClockSource == DeviceSettings.SampleClockSource.DerivedFromMaster)
                {
                    task.Timing.ConfigureSampleClock("", deviceSettings.SampleClockRate, deviceSettings.ClockEdge, SampleQuantityMode.FiniteSamples, nSamples);
                    //"" empty string defaults to analog input sample clock.
                }
                else
                {
                    task.Timing.ConfigureSampleClock(deviceSettings.SampleClockExternalSource, deviceSettings.SampleClockRate, deviceSettings.ClockEdge, SampleQuantityMode.FiniteSamples, nSamples);
                }
                if (deviceSettings.MasterTimebaseSource != "" && deviceSettings.MasterTimebaseSource != null)
                {
                    task.Timing.MasterTimebaseSource = deviceSettings.MasterTimebaseSource.ToString();
                }



                // Analog first...

                if (analogIDs.Count != 0)
                {
                    double[,] analogBuffer;
                    double[] singleChannelBuffer;
                    try
                    {
                        analogBuffer = new double[analogs.Count, nSamples];
                        singleChannelBuffer = new double[nSamples];
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Unable to allocate analog buffer for device " + deviceName + ". Reason: " + e.Message + "\n" + e.StackTrace);
                    }

                    for (int i = 0; i < analogIDs.Count; i++)
                    {
                        int analogID = analogIDs[i];

                        if (settings.logicalChannelManager.Analogs[analogID].TogglingChannel)
                        {
                            DaqMxTaskGenerator.getAnalogTogglingBuffer(singleChannelBuffer);
                        }
                        else if (settings.logicalChannelManager.Analogs[analogID].overridden)
                        {
                            for (int j = 0; j < singleChannelBuffer.Length; j++)
                            {
                                singleChannelBuffer[j] = settings.logicalChannelManager.Analogs[analogID].analogOverrideValue;
                            }
                        }
                        else
                        {
                            sequence.computeAnalogBuffer(analogIDs[i], timeStepSize, singleChannelBuffer);
                        }

                        for (int j = 0; j < nBaseSamples; j++)
                        {
                            analogBuffer[i, j] = singleChannelBuffer[j];
                        }
                        for (int j = nBaseSamples; j < nSamples; j++)
                        {
                            analogBuffer[i, j] = analogBuffer[i, j - 1];
                        }
                    }

                    singleChannelBuffer = null;
                    System.GC.Collect();


                    AnalogMultiChannelWriter writer = new AnalogMultiChannelWriter(task.Stream);

                    writer.WriteMultiSample(false, analogBuffer);
                    // analog cards report the exact number of generated samples. for non-variable timebase this is nSamples
                    expectedSamplesGenerated = nSamples;

                }

                //Now on to the digital
                //SOMEBULLSHIT
                if (usedPortNumbers.Count != 0)
                {
                    if (deviceSettings.DigitalHardwareStructure[0] == 8)
                    {
                        byte[,] digitalBuffer;
                        bool[] singleChannelBuffer;

                        try
                        {
                            digitalBuffer = new byte[usedPortNumbers.Count, nSamples];
                            singleChannelBuffer = new bool[nSamples];
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Unable to allocate digital buffer for device " + deviceName + ". Reason: " + e.Message + "\n" + e.StackTrace);
                        }

                        for (int i = 0; i < usedPortNumbers.Count; i++)
                        {

                            int portNum = usedPortNumbers[i];
                            byte digitalBitMask = 1;
                            for (int lineNum = 0; lineNum < deviceSettings.DigitalHardwareStructure[portNum]; lineNum++)
                            {
                                int digitalID = port_digital_IDs[portNum][lineNum];
                                if (digitalID != -1)
                                {
                                    if (settings.logicalChannelManager.Digitals[digitalID].TogglingChannel)
                                    {
                                        getDigitalTogglingBuffer(singleChannelBuffer);
                                    }
                                    else if (settings.logicalChannelManager.Digitals[digitalID].overridden)
                                    {
                                        for (int j = 0; j < singleChannelBuffer.Length; j++)
                                        {
                                            singleChannelBuffer[j] = settings.logicalChannelManager.Digitals[digitalID].digitalOverrideValue;
                                        }
                                    }
                                    else
                                    {

                                        sequence.computeDigitalBuffer(digitalID, timeStepSize, singleChannelBuffer);
                                    }
                                    // byte digitalBitMask = (byte)(((byte) 2)^ ((byte)lineNum));
                                    for (int j = 0; j < nBaseSamples; j++)
                                    {
                                        // copy the bit value into the digital buffer byte.
                                        if (singleChannelBuffer[j])
                                            digitalBuffer[i, j] |= digitalBitMask;
                                    }

                                }
                                digitalBitMask = (byte)(digitalBitMask << 1);
                            }
                            for (int j = nBaseSamples; j < nSamples; j++)
                            {
                                digitalBuffer[i, j] = digitalBuffer[i, j - 1];
                            }
                        }
                        singleChannelBuffer = null;
                        System.GC.Collect();
                        DigitalMultiChannelWriter writer = new DigitalMultiChannelWriter(task.Stream);
                        writer.WriteMultiSamplePort(false, digitalBuffer);

                        /// Digital cards report the number of generated samples as a multiple of 4
                        expectedSamplesGenerated = nSamples;
                    }
                    else if (deviceSettings.DigitalHardwareStructure[0] == 32)
                    {
                        UInt32[,] digitalBuffer;
                        bool[] singleChannelBuffer;

                        try
                        {
                            digitalBuffer = new UInt32[usedPortNumbers.Count, nSamples];
                            singleChannelBuffer = new bool[nSamples];
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Unable to allocate digital buffer for device " + deviceName + ". Reason: " + e.Message + "\n" + e.StackTrace);
                        }

                        for (int i = 0; i < usedPortNumbers.Count; i++)
                        {

                            int portNum = usedPortNumbers[i];
                            UInt32 digitalBitMask = 1;
                            for (int lineNum = 0; lineNum < deviceSettings.DigitalHardwareStructure[portNum]; lineNum++)
                            {
                                int digitalID = port_digital_IDs[portNum][lineNum];
                                if (digitalID != -1)
                                {
                                    if (settings.logicalChannelManager.Digitals[digitalID].TogglingChannel)
                                    {
                                        getDigitalTogglingBuffer(singleChannelBuffer);
                                    }
                                    else if (settings.logicalChannelManager.Digitals[digitalID].overridden)
                                    {
                                        for (int j = 0; j < singleChannelBuffer.Length; j++)
                                        {
                                            singleChannelBuffer[j] = settings.logicalChannelManager.Digitals[digitalID].digitalOverrideValue;
                                        }
                                    }
                                    else
                                    {

                                        sequence.computeDigitalBuffer(digitalID, timeStepSize, singleChannelBuffer);
                                    }
                                    // byte digitalBitMask = (byte)(((byte) 2)^ ((byte)lineNum));
                                    for (int j = 0; j < nBaseSamples; j++)
                                    {
                                        // copy the bit value into the digital buffer byte.
                                        if (singleChannelBuffer[j])
                                            digitalBuffer[i, j] |= digitalBitMask;
                                    }

                                }
                                digitalBitMask = (UInt32)(digitalBitMask << 1);
                            }
                            for (int j = nBaseSamples; j < nSamples; j++)
                            {
                                digitalBuffer[i, j] = digitalBuffer[i, j - 1];
                            }
                        }
                        singleChannelBuffer = null;
                        System.GC.Collect();
                        DigitalMultiChannelWriter writer = new DigitalMultiChannelWriter(task.Stream);
                        writer.WriteMultiSamplePort(false, digitalBuffer);
                        /// Digital cards report the number of generated samples as a multiple of 4
                        expectedSamplesGenerated = nSamples;
                    }
                    else
                    {
                        throw new Exception("The number of ports per line on " + deviceSettings.DeviceName + " is not 32 or 8. This could be because you have bound digital outputs to software times channels.");
                    }
                }


            }
            #endregion
            #region Variable timebase buffer creation
            else // variable timebase buffer creation...
            {



                double timeStepSize = Common.getPeriodFromFrequency(deviceSettings.SampleClockRate);

                TimestepTimebaseSegmentCollection timebaseSegments =
                    sequence.generateVariableTimebaseSegments(serverSettings.VariableTimebaseType,
                                            timeStepSize);

                int nBaseSamples = timebaseSegments.nSegmentSamples();

                nBaseSamples++; // add one sample for the dwell sample at the end of the buffer

                // for reasons that are utterly stupid and frustrating, the DAQmx libraries seem to prefer sample
                // buffers with lengths that are a multiple of 4. (otherwise they, on occasion, depending on the parity of the 
                // number of channels, throw exceptions complaining.
                // thus we add a few filler samples at the end of the sequence which parrot back the last sample.

                int nFillerSamples = 4 - nBaseSamples % 4;
                if (nFillerSamples == 4)
                    nFillerSamples = 0;

                int nSamples = nBaseSamples + nFillerSamples;

                if (deviceSettings.MySampleClockSource == DeviceSettings.SampleClockSource.DerivedFromMaster)
                {
                    throw new Exception("Attempt to use a uniform sample clock with a variable timebase enabled device. This will not work. To use a variable timebase for this device, you must specify an external sample clock source.");
                }
                else
                {
                    task.Timing.ConfigureSampleClock(deviceSettings.SampleClockExternalSource, deviceSettings.SampleClockRate, deviceSettings.ClockEdge, SampleQuantityMode.FiniteSamples, nSamples);
                }


                // Analog first...

                if (analogIDs.Count != 0)
                {
                    double[,] analogBuffer;
                    double[] singleChannelBuffer;
                    try
                    {
                        analogBuffer = new double[analogs.Count, nSamples];
                        singleChannelBuffer = new double[nSamples];
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Unable to allocate analog buffer for device " + deviceName + ". Reason: " + e.Message + "\n" + e.StackTrace);
                    }

                    for (int i = 0; i < analogIDs.Count; i++)
                    {
                        int analogID = analogIDs[i];

                        if (settings.logicalChannelManager.Analogs[analogID].TogglingChannel)
                        {
                            getAnalogTogglingBuffer(singleChannelBuffer);
                        }
                        else if (settings.logicalChannelManager.Analogs[analogID].overridden)
                        {
                            for (int j = 0; j < singleChannelBuffer.Length; j++)
                            {
                                singleChannelBuffer[j] = settings.logicalChannelManager.Analogs[analogID].analogOverrideValue;
                            }
                        }
                        else
                        {
                            sequence.computeAnalogBuffer(analogIDs[i], timeStepSize, singleChannelBuffer, timebaseSegments);
                        }
                        for (int j = 0; j < nBaseSamples; j++)
                        {
                            analogBuffer[i, j] = singleChannelBuffer[j];
                        }
                        for (int j = nBaseSamples; j < nSamples; j++)
                        {
                            analogBuffer[i, j] = analogBuffer[i, j - 1];
                        }
                    }

                    singleChannelBuffer = null;
                    System.GC.Collect();


                    AnalogMultiChannelWriter writer = new AnalogMultiChannelWriter(task.Stream);

                    writer.WriteMultiSample(false, analogBuffer);
                    // Analog cards report the exact number of samples generated. for variable timebase this is nBaseSamples
                    expectedSamplesGenerated = nBaseSamples;

                }


                if (usedPortNumbers.Count != 0)
                {
                    if (deviceSettings.DigitalHardwareStructure[0] == 8)
                    {
                        byte[,] digitalBuffer;
                        bool[] singleChannelBuffer;

                        try
                        {
                            digitalBuffer = new byte[usedPortNumbers.Count, nSamples];
                            singleChannelBuffer = new bool[nSamples];
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Unable to allocate digital buffer for device " + deviceName + ". Reason: " + e.Message + "\n" + e.StackTrace);
                        }

                        for (int i = 0; i < usedPortNumbers.Count; i++)
                        {
                            int portNum = usedPortNumbers[i];
                            byte digitalBitMask = 1;
                            for (int lineNum = 0; lineNum < 8; lineNum++)
                            {
                                int digitalID = port_digital_IDs[portNum][lineNum];
                                if (digitalID != -1)
                                {
                                    if (settings.logicalChannelManager.Digitals[digitalID].TogglingChannel)
                                    {
                                        getDigitalTogglingBuffer(singleChannelBuffer);
                                    }
                                    else if (settings.logicalChannelManager.Digitals[digitalID].overridden)
                                    {
                                        for (int j = 0; j < singleChannelBuffer.Length; j++)
                                        {
                                            singleChannelBuffer[j] = settings.logicalChannelManager.Digitals[digitalID].digitalOverrideValue;
                                        }
                                    }
                                    else
                                    {

                                        sequence.computeDigitalBuffer(digitalID, timeStepSize, singleChannelBuffer, timebaseSegments);
                                    }
                                    // byte digitalBitMask = (byte)(((byte) 2)^ ((byte)lineNum));
                                    for (int j = 0; j < nBaseSamples; j++)
                                    {
                                        // copy the bit value into the digital buffer byte.
                                        if (singleChannelBuffer[j])
                                            digitalBuffer[i, j] |= digitalBitMask;
                                    }

                                }
                                digitalBitMask = (byte)(digitalBitMask << 1);
                            }
                            for (int j = nBaseSamples; j < nSamples; j++)
                            {
                                digitalBuffer[i, j] = digitalBuffer[i, j - 1];
                            }
                        }
                        singleChannelBuffer = null;
                        System.GC.Collect();
                        DigitalMultiChannelWriter writer = new DigitalMultiChannelWriter(task.Stream);
                        writer.WriteMultiSamplePort(false, digitalBuffer);
                        // digital cards report number of samples generated up to multiple of 4
                        expectedSamplesGenerated = nSamples;
                    }
                    else if (deviceSettings.DigitalHardwareStructure[0] == 32)
                    {
                        UInt32[,] digitalBuffer;
                        bool[] singleChannelBuffer;

                        try
                        {
                            digitalBuffer = new UInt32[usedPortNumbers.Count, nSamples];
                            singleChannelBuffer = new bool[nSamples];
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Unable to allocate digital buffer for device " + deviceName + ". Reason: " + e.Message + "\n" + e.StackTrace);
                        }

                        for (int i = 0; i < usedPortNumbers.Count; i++)
                        {
                            int portNum = usedPortNumbers[i];
                            UInt32 digitalBitMask = 1;
                            for (int lineNum = 0; lineNum < 8; lineNum++)
                            {
                                int digitalID = port_digital_IDs[portNum][lineNum];
                                if (digitalID != -1)
                                {
                                    if (settings.logicalChannelManager.Digitals[digitalID].TogglingChannel)
                                    {
                                        getDigitalTogglingBuffer(singleChannelBuffer);
                                    }
                                    else if (settings.logicalChannelManager.Digitals[digitalID].overridden)
                                    {
                                        for (int j = 0; j < singleChannelBuffer.Length; j++)
                                        {
                                            singleChannelBuffer[j] = settings.logicalChannelManager.Digitals[digitalID].digitalOverrideValue;
                                        }
                                    }
                                    else
                                    {

                                        sequence.computeDigitalBuffer(digitalID, timeStepSize, singleChannelBuffer, timebaseSegments);
                                    }
                                    // byte digitalBitMask = (byte)(((byte) 2)^ ((byte)lineNum));
                                    for (int j = 0; j < nBaseSamples; j++)
                                    {
                                        // copy the bit value into the digital buffer byte.
                                        if (singleChannelBuffer[j])
                                            digitalBuffer[i, j] |= digitalBitMask;
                                    }

                                }
                                digitalBitMask = (UInt32)(digitalBitMask << 1);
                            }
                            for (int j = nBaseSamples; j < nSamples; j++)
                            {
                                digitalBuffer[i, j] = digitalBuffer[i, j - 1];
                            }
                        }
                        singleChannelBuffer = null;
                        System.GC.Collect();
                        DigitalMultiChannelWriter writer = new DigitalMultiChannelWriter(task.Stream);
                        writer.WriteMultiSamplePort(false, digitalBuffer);
                        // digital cards report number of samples generated up to multiple of 4
                        expectedSamplesGenerated = nSamples;
                    }
                }
            }

            #endregion

            if (deviceSettings.StartTriggerType == DeviceSettings.TriggerType.TriggerIn)
            {

                task.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(
                    deviceSettings.TriggerInPort,
                    DigitalEdgeStartTriggerEdge.Rising);

            }

            task.Control(TaskAction.Verify);
            task.Control(TaskAction.Commit);
            task.Control(TaskAction.Reserve);


            return task;
        }

        /// <summary>
        /// This function creates channels in the given task, for the named device.
        /// </summary>
        /// <param name="deviceName">
        /// The name of the device which the channels will will be created for. Eg Dev1
        /// </param>
        /// <param name="usedDigitalChannels"></param>
        /// A dictionary, indexed by logical ID#, of all of the digital hardware channels parsed from parsed from settings data which
        /// reside on this server.
        /// <param name="usedAnalogChannels">
        /// A dictionary, indexed by logical ID#, of all of the analog hardware channels parsed from the settings data, which reside on this server.
        /// </param>
        /// <param name="task">The task for which channels will be created.</param>
        /// <param name="analogIDs">
        /// An out parameter, which will store a list of the analog IDs that were created, in the order created.
        /// </param>
        /// <param name="analogs">
        /// An out parameter, which will store a list of the analog hardware channels corresponding to the channels created, in the order created.
        /// </param>
        /// <param name="port_digital_IDs"></param>
        /// An out parameter, which will store a dictionary mapping a digital port number to an 8-length array of integers specifying the logical ID# of each bit of the 
        /// given port #. Entries of -1 in this array indicate bits that have no logical ID# assigned. See **** in this method's source code for further information.
        /// <param name="usedPortNumbers">
        /// An out parameter, which will store a list of integers corresponding to the digital port numbers that were used on this device, in the order created.
        /// </param>
        private static void parseAndCreateChannels(AtticusServerCommunicator sender, string deviceName, DeviceSettings deviceSettings, Dictionary<int, HardwareChannel> usedDigitalChannels, Dictionary<int, HardwareChannel> usedAnalogChannels, Task task, out List<int> analogIDs, out List<HardwareChannel> analogs, out Dictionary<int, int[]> port_digital_IDs, out List<int> usedPortNumbers)
        {
            // figure out which of the analog and digital channels belong on this device. Add them here and index by 
            // logical ID#
            Dictionary<int, HardwareChannel> analogsUnsorted = getChannelsOnDevice(usedAnalogChannels, deviceName);
            Dictionary<int, HardwareChannel> digitalsUnsorted = getChannelsOnDevice(usedDigitalChannels, deviceName);


            // sort the lists by ID
            analogIDs = new List<int>();
            analogs = new List<HardwareChannel>();
            sortDicionaryByID(analogIDs, analogs, analogsUnsorted);

            // list of the digital IDs of channels on this device
            List<int> digitalIDs = new List<int>();
            // list of the corresponding hardware channels
            List<HardwareChannel> digitals = new List<HardwareChannel>();
            sortDicionaryByID(digitalIDs, digitals, digitalsUnsorted);


            //**deviceSettings IS VERY IMPORTANT HERE


            // ****
            // description of port_digital_IDs:
            // mapping from port number to an array of integers. The nth element of the array gives the digital ID
            // of the channel which is connected to the nth line of that port. If there is no channel on that port,
            // the ID is -1.

            if (deviceSettings.DeviceDescription.Contains("6533") | deviceSettings.DeviceDescription.Contains("6534"))
            {
                groupDigitalChannels(digitalIDs, digitals, out port_digital_IDs, out usedPortNumbers, deviceSettings.DigitalHardwareStructure, true);

            }
            else
            {
                groupDigitalChannels(digitalIDs, digitals, out port_digital_IDs, out usedPortNumbers, deviceSettings.DigitalHardwareStructure, false);

            }
            //ok! create the channels.

            // analog first
            for (int i = 0; i < analogs.Count; i++)
            {
                task.AOChannels.CreateVoltageChannel(analogs[i].physicalChannelName(), "", -10, 10, AOVoltageUnits.Volts);
            }

            // now digital
            for (int i = 0; i < usedPortNumbers.Count; i++)
            {
                int portNum = usedPortNumbers[i];
                try
                {
                    task.DOChannels.CreateChannel(deviceName + '/' + HardwareChannel.digitalPhysicalChannelName(portNum), "", ChannelLineGrouping.OneChannelForAllLines);
                }
                catch (Exception e)
                {
                    sender.messageLog(sender, new MessageEvent("Couldn't create channel:" + deviceName + '/' + HardwareChannel.digitalPhysicalChannelName(portNum)));
                }
            }
        }


        /// <summary>
        /// given a list of digitalIDs and hardware channels, constructs a list of all the used port numbers,
        /// as well as a mapping from each port number to an array of integers giving the used digital IDs for
        /// each line of that port.
        /// </summary>
        /// <param name="digitalIDs"></param>
        /// <param name="digitals"></param>
        /// <param name="port_digital_IDs"></param>
        /// <param name="usedPortNumbers"></param>
        private static void groupDigitalChannels(List<int> digitalIDs, List<HardwareChannel> digitals, out Dictionary<int, int[]> port_digital_IDs, out List<int> usedPortNumbers, int[] digitalHardwareStructure, bool requiresLegacyPortPairs)
        {
            List<int> allPorts = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                allPorts.Add(i);
            }
            groupDigitalChannels(digitalIDs, digitals, out port_digital_IDs, out usedPortNumbers, digitalHardwareStructure, requiresLegacyPortPairs, allPorts);
        }

        /// <summary>
        /// Same as other version of groupDigitalChannels, but only allows for port numbers in the list allowedPortsToUse
        /// </summary>
        /// <param name="digitalIDs"></param>
        /// <param name="digitals"></param>
        /// <param name="port_digital_IDs"></param>
        /// <param name="usedPortNumbers"></param>
        /// <param name="allowedPortsToUse"></param>
        private static void groupDigitalChannels(List<int> digitalIDs, List<HardwareChannel> digitals, out Dictionary<int, int[]> port_digital_IDs, out List<int> usedPortNumbers, int[] digitalHardwareStructure, bool requiresLegacyPortPairs, List<int> allowedPortsToUse)
        {
            // Irritating but true fact of life: To make the DAQmx drivers happy 
            // we have to output the digital outputs in 8 bit groups corresponding to 
            // the "port" of the device. This means we first have to amalgamate the digital channels that belong
            // to the same port, and fill in any "holes" (ie missing channels) with empty data
            {
                usedPortNumbers = new List<int>();


                // mapping from port number to an array of integers. The nth element of the array gives the digital ID
                // of the channel which is connected to the nth line of that port. If there is no channel on that port,
                // the ID is -1.
                port_digital_IDs = new Dictionary<int, int[]>();

                for (int i = 0; i < digitalIDs.Count; i++)
                {
                    int digitalID = digitalIDs[i];
                    HardwareChannel hc = digitals[i];

                    int portNum = hc.daqMxDigitalPortNumber();
                    if (!usedPortNumbers.Contains(portNum))
                    {
                        if (allowedPortsToUse.Contains(portNum))
                        {
                            if (requiresLegacyPortPairs)
                            {
                                // Ports have to be used by pairs for the Daqmx driver to work. If port 0 is used, port 1 has to be used too, and the opposite too, etc...
                                //..so we just add all of the ports available on these cards. This is necessary only for 6533 and 6534 cards, which are legacy cards now
                                usedPortNumbers.Add(0);
                                usedPortNumbers.Add(1);
                                usedPortNumbers.Add(2);
                                usedPortNumbers.Add(3);
                                port_digital_IDs.Add(0, new int[] { -1, -1, -1, -1, -1, -1, -1, -1 });
                                port_digital_IDs.Add(1, new int[] { -1, -1, -1, -1, -1, -1, -1, -1 });
                                port_digital_IDs.Add(2, new int[] { -1, -1, -1, -1, -1, -1, -1, -1 });
                                port_digital_IDs.Add(3, new int[] { -1, -1, -1, -1, -1, -1, -1, -1 });
                                //SOMEBULLSHIT


                            }
                            else
                            {
                                //the PXIe6363 card has port0/(line0-31), port1/(line0-7), port2/(line0-7). Only port0 is hardware timed, hence we don't even try to buffer port1 and port 2. 



                                usedPortNumbers.Add(portNum);
                                //digitalHardwareStructure contains all of the information about the number of lines per port on a given card. 
                                int[] add_port = new int[digitalHardwareStructure[portNum]];
                                for (int line_count = 0; line_count < add_port.Length; line_count++)
                                {
                                    add_port[line_count] = -1;
                                }
                                //the above loop usually ends up adding either {-1,-1,-1,-1,-1,-1,-1,-1} or 
                                //{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1}, depending on whether
                                //the channel has 8 or 32 lines per port. (The code is flexible to be able to add any number of lines per port, but 8 and 32 is typical in NI systems)


                                //pass by reference
                                port_digital_IDs.Add(portNum, add_port);
                                //Possible issue: I do not explicity disallow the use of port1 and port2 for a 6363 card here. I assume that they've somehow been disabled in Cicero, so we're only working
                                //with port0

                            }

                        }
                    }

                    if (usedPortNumbers.Contains(portNum))
                    {
                        port_digital_IDs[portNum][hc.daqMxDigitalLineNumber()] = digitalID;
                    }
                }
            }
        }

        /// <summary>
        /// Ids will receive the sorted keys of dict. hc will receive the corresponding hardwareChannels.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="?"></param>
        /// <param name="?"></param>
        private static void sortDicionaryByID(List<int> ids, List<HardwareChannel> hc, Dictionary<int, HardwareChannel> dict)
        {
            ids.Clear();
            hc.Clear();
            ids.AddRange(dict.Keys);
            ids.Sort();
            for (int i = 0; i < ids.Count; i++)
            {
                hc.Add(dict[ids[i]]);
            }
        }

        /// <summary>
        /// Returns an int to channel dictionary of all the channels which reside on the given device.
        /// </summary>
        /// <param name="channels"></param>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        private static Dictionary<int, HardwareChannel> getChannelsOnDevice(Dictionary<int, HardwareChannel> channels, string deviceName)
        {
            Dictionary<int, HardwareChannel> ans = new Dictionary<int, HardwareChannel>();
            foreach (int id in channels.Keys)
            {
                if (channels[id].DeviceName == deviceName)
                {
                    ans.Add(id, channels[id]);
                }
            }

            return ans;
        }

        public static void getDigitalTogglingBuffer(bool[] buffer)
        {
            for (int i = 0; i < buffer.Length; i += 2)
            {
                buffer[i] = true;
                buffer[i + 1] = false;
            }
        }

        public static void getAnalogTogglingBuffer(double[] buffer)
        {
            for (int i = 0; i < buffer.Length; i += 2)
            {
                buffer[i] = 5;
                buffer[i + 1] = 0;
            }
        }
    }
}
