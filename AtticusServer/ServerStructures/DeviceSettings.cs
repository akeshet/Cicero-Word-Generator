using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using NationalInstruments.DAQmx;

namespace AtticusServer
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class DeviceSettings
    {

        public override string ToString()
        {
            return deviceName;            
        }


        private bool deviceEnabled;

        [Description("Determines whether output on this device is enabled. Applies only to DAQmx devices."),
        Category("Global")]
        public bool DeviceEnabled
        {
            get { return deviceEnabled; }
            set { deviceEnabled = value; }
        }

        // This is a public field, but a read only property. Reason: We want the property,
        // which is what gets displayed to the user in the property grid, to be read only.
        // but we want the field to be modifyable by the refreshhardware method.
        public bool deviceConnected = true;
        [Description("Indicates whether this device is currently connected. This status is updated whenever the hardware channel list is refreshed."),
        Category("Global")]
        public bool DeviceConnected
        {
            get { return deviceConnected; }
        }

        private string deviceName;

        [Description("Name of the device."),
        Category("Global")]
        public string DeviceName
        {
            get { return deviceName; }
        }

        private bool analogInEnabled;

        [Description("Determines whether analog input on this device is enabled."),
        Category("Analog In")]
        public bool AnalogInEnabled
        {
            get { return analogInEnabled; }
            set { analogInEnabled = value; }
        }

        private bool useCustomAnalogTransferSettings;

        [Description("If true, then AnalogDataTransferMechanism and AnalogDataTransferCondition will apply. If false, they wont. Some devices do not support customization of these properties."),
        Category("Analog")]
        public bool UseCustomAnalogTransferSettings
        {
            get { return useCustomAnalogTransferSettings; }
            set { useCustomAnalogTransferSettings = value; }
        }

        private AODataTransferMechanism analogDataTransferMechanism;

        [Description("Sets the data transfer mechanism used to transfer analog data to the device. Normally use DMA, but if you are out of DMA channels try Interrupt."),
        Category("Analog")]
        public AODataTransferMechanism AnalogDataTransferMechanism
        {
            get { return analogDataTransferMechanism; }
            set { analogDataTransferMechanism = value; }
        }


      /*  private WriteWaitMode writeWaitType;

        [Description("Sets the wait for writes to the device output stream. Poll is most aggressive, at expense of CPU usage. Sleep is least aggressive.") 
        public WriteWaitMode WriteWaitType
        {
            get { return writeWaitType; }
            set { writeWaitType = value; }
        }*/



        private AODataTransferRequestCondition analogDataTransferCondition;


        [Description("Sets the condition under which device requests new analog data transfer."),
        Category("Analog")]
        public AODataTransferRequestCondition AnalogDataTransferCondition
        {
            get { return analogDataTransferCondition; }
            set { analogDataTransferCondition = value; }
        }


        private bool useCustomDigitalTransferSettings;

        [Description("If true, then DigitalDataTransferMechanism and DigitalDataTransferCondition will apply. If false, they wont. Some devices do not support customization of these properties."),
Category("Digital")]
        public bool UseCustomDigitalTransferSettings
        {
            get { return useCustomDigitalTransferSettings; }
            set { useCustomDigitalTransferSettings = value; }
        }

        private DODataTransferMechanism digitalDataTransferMechanism;

        [Description("Sets the data transfer mechanism used to tranfer digital data to the device. Normally use DMA, but if you are out of DMA channels try Interrupt."),
        Category("Digital")]
        public DODataTransferMechanism DigitalDataTransferMechanism
        {
            get { return digitalDataTransferMechanism; }
            set { digitalDataTransferMechanism = value; }
        }



        public bool use32BitDigitalPorts;
        [Description("For 6533 and 6534 cards, this is 16. For 6259 cards, this is 32. Other cards not yet supported."),
        Category("Digital")]
        public bool Use32BitDigitalPorts
        {
            get { return use32BitDigitalPorts; }
        }


        private DODataTransferRequestCondition digitalDataTransferCondition;

        [Description("Sets the condition under which device requests new digital data transfer."),
        Category("Digital")]
        public DODataTransferRequestCondition DigitalDataTransferCondition
        {
            get { return digitalDataTransferCondition; }
            set { digitalDataTransferCondition = value; }
        }

        public bool isFPGADevice;

        [Description("True if this device is an Opal Kelly FPGA device."),
        Category("Global")]
        public bool IsFPGADevice
        {
            get { return isFPGADevice; }
            
        }


        private string deviceDescription;

        [Description("Description of the device, as returned from its hardware driver."),
        Category("Global")]
        public string DeviceDescription
        {
            get { return deviceDescription; }
        }
        /// <summary>
        /// This is a list rather than a dictionary mapping port name to settings object. Reason: the CollectionEditor is unable
        /// to add entries to a dictionary, and I don't want to write a custom collection editor.
        /// </summary>
        private List<SerialPortSettings> serialSettings;

        [Description("This is no longer recommended. To set serial port settings, use MAX or the windows hardware manager to set the settings for the serial port of interest."),
        Category("Serial")]
        public List<SerialPortSettings> SerialSettings
        {
            get {
                if (serialSettings == null)
                    serialSettings = new List<SerialPortSettings>();
                return serialSettings; 
            }
            set { serialSettings = value; }
        }


        public enum TriggerType { SoftwareTrigger, TriggerIn };

        TriggerType startTriggerType;

        [Description("Indicates how this device is triggered. Applies only to Analog and Digital output cards. GPIB output card are software trigger only."),
        Category("Triggering")]
        public TriggerType StartTriggerType
        {
            get { return startTriggerType; }
            set { startTriggerType = value; }
        }

        private bool softTriggerLast;

        [Description("For a DAQmx device, if startTriggerType is SoftwareTrigger, then if this is set to true the device will soft trigger after all other devices have soft triggered."),
        Category("Triggering")]
        public bool SoftTriggerLast
        {
            get { return softTriggerLast; }
            set { softTriggerLast = value; }
        }

        private string masterTimebaseSource;

        [Description("Source port for the master timebase. For the built in timebase clock, use OnBoardClock"),
        Category("Timing"), DefaultValue("OnBoardClock")]
        public string MasterTimebaseSource
        {
            get {
                if (masterTimebaseSource == null)
                    masterTimebaseSource = "";
                return masterTimebaseSource; 
            }
            set { masterTimebaseSource = value; }
        }

        private NationalInstruments.DAQmx.SampleClockActiveEdge clockEdge;

        [Description("Clock edge for sample clock."),
        Category("Timing")]
        public NationalInstruments.DAQmx.SampleClockActiveEdge ClockEdge
        {
            get
            {
                if (clockEdge!= SampleClockActiveEdge.Rising && clockEdge!= SampleClockActiveEdge.Falling)
                    clockEdge = SampleClockActiveEdge.Rising;
                return clockEdge;
            }
            set { clockEdge = value; }
        }

        private string triggerInPort;

        [Description("Determines the port that will act as the trigger port for this device. Only is use if TriggerType is set to TriggerIn."),
        Category("Triggering")]
        public string TriggerInPort
        {
            get {
                if (triggerInPort == null)
                    triggerInPort = "";
                return triggerInPort; 
            }
            set { triggerInPort = value; }
        }

        private int sampleClockRate = 1000;

        /// <summary>
        /// in Hz
        /// </summary>
        /// 
        [Description("Sets rate of the sample clock, in Hz. If a variable timebase is being used, this must be set to the frequency of the variable timebase master clock."),
        Category("Timing")]
        public int SampleClockRate
        {
            get { 
                return sampleClockRate; }
            set { sampleClockRate = value; }
        }

        private bool usingVariableTimebase;

        [Description("Sets whether or not this card is to be triggered with a variable timebase."),
        Category("Timing")]
        public bool UsingVariableTimebase
        {
            get { return usingVariableTimebase; }
            set { usingVariableTimebase = value; }
        }

        private bool analogChannelsEnabled;

        [Description("For DAQmx devices, sets whether analog channels are enabled. Most devices support only analog OR digital devices simultaneously."),
        Category("Analog")]
        public bool AnalogChannelsEnabled
        {
            get { return analogChannelsEnabled; }
            set { analogChannelsEnabled = value; }
        }

        private bool digitalChannelsEnabled;

        public enum SampleClockSource { DerivedFromMaster, External };
        private SampleClockSource mySampleClockSource;

        [Description("For DAQmx devices, sets whether the sample clock is derived by dividing the master timebase, or weather it comes from an external input."),
        Category("Timing")]
        public SampleClockSource MySampleClockSource
        {
            get { return mySampleClockSource; }
            set { mySampleClockSource = value; }
        }


        private string sampleClockExternalSource;

        [Description("If MySampleClockSource is set to external, this string sets the external input which is used as the sample clock."),
        Category("Timing")]
        public string SampleClockExternalSource
        {
            get {
                if (sampleClockExternalSource == null)
                    sampleClockExternalSource = "";
                return sampleClockExternalSource; }
            set { sampleClockExternalSource = value; }
        }

        [Description("For DAQmx devices, sets whether digital channels are enabled. Most devices support only analog OR digital devices simultaneously."),
        Category("Digital")]
        public bool DigitalChannelsEnabled
        {
            get { return digitalChannelsEnabled; }
            set { digitalChannelsEnabled = value; }
        }

        private string refClockSource;


        [Description("Source for 10MHz reference clock. To use the front panel reference input, set to Ref_In. Otherwise leave blank."),
        Category("RFSG")]
        public string RefClockSource
        {
            get { return refClockSource; }
            set { refClockSource = value; }
        }

        private bool autoInitate;

        [Description("If true, rfsg device will automatically have an initiate command put at the beginning of its output buffer."),
Category("RFSG")]
        public bool AutoInitate
        {
            get { return autoInitate; }
            set { autoInitate = value; }
        }

        private bool autoEnable;

        [Description("If true, rfsg device will automatically have an enable command put at the beginning of its output buffer."),
        Category("RFSG")]
        public bool AutoEnable
        {
            get { return autoEnable; }
            set { autoEnable = value; }
        }

        public DeviceSettings()
        {
            this.deviceEnabled = false;
        }

        public DeviceSettings(string deviceName, string deviceDescription, int analogHardwareStructure, int [] digitalHardwareStructure) : this()
        {
            this.deviceName = deviceName;
            this.deviceDescription = deviceDescription;
            this.analogHardwareStructure = analogHardwareStructure;
            this.digitalHardwareStructure = digitalHardwareStructure;
        }

        public enum DeviceErrorCheckSamplesGeneratedSensitivity { Strict, Within4, Disabled };
        private DeviceErrorCheckSamplesGeneratedSensitivity samplesGeneratedCheckSensitivity;

        [Description("This field sets the strictness of how Atticus compares the number of samples generated to the number expected. Under certain circumstances, Atticus can be over sensitive to slight differences between generated and expected samples, and this field can be used to suppress false detections of mistriggers."),
Category("Error Checking")]
        public DeviceErrorCheckSamplesGeneratedSensitivity SamplesGeneratedCheckSensitivity
        {
            get { return samplesGeneratedCheckSensitivity; }
            set { samplesGeneratedCheckSensitivity = value; }
        }

        private UInt16 retriggerDebounceSamples;

        [Description("Applies only to FPGA Variable Timebase generation devices using their Retrigger input. Sets a debounce time for the retrigger input (measured in FPGA clock cycles). Increase this number to reduce spurious edges, while simultaneously slowing down retrigger response time."),
        Category("FPGA")]
        public UInt16 RetriggerDebounceSamples
        {
            get { return retriggerDebounceSamples; }
            set { retriggerDebounceSamples = value; }
        }

        private int analogHardwareStructure;

        [Description("Total number of enabled analog channels on device."),
        Category("Analog")]
        public int AnalogHardwareStructure
        {
            get { return analogHardwareStructure; }
           // set { analogHardwareStructure = value; }
        }


        private int[] digitalHardwareStructure;

        [Description("Array describing the digtal port/line configuration of the card. The index of the array corresponds to the port number, while the value in the array corresponds to the number of lines on that port. E.g. DigitalHardwareStructure[0]=8 means there are 8 lines on port 0. This value is null for non-digital output cards."),
        Category("Digital")]
        public int [] DigitalHardwareStructure
        {
            get { return digitalHardwareStructure; }
            //set { digitalHardwareStructure = value; }
        }

    }
}
