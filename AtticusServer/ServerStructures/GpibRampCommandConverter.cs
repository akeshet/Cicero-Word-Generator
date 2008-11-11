using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace AtticusServer
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class GpibRampCommandConverter
    {

        private string initializationCommand;

        [Description("Command to be output at the beginning of every sequence run, such as an output enable command."),
        Category("Other")]
        public string InitializationCommand
        {
            get { return initializationCommand; }
            set { initializationCommand = value; }
        }


        private string amplitudePrefix;

        [Description("Text that preceeds the amplitude number in an amplitude command."),
        Category("Amplitude")]
        public string AmplitudePrefix
        {
            get { return amplitudePrefix; }
            set { amplitudePrefix = value; }
        }
        private string amplitudePostfix;

        [Description("Text that follows the amplitude number in an amplitude command."),
Category("Amplitude")]
        public string AmplitudePostfix
        {
            get { return amplitudePostfix; }
            set { amplitudePostfix = value; }
        }
        private string frequencyPrefix;

        [Description("Text that preceeds the frequency number in an amplitude command."),
Category("Frequency")]
        public string FrequencyPrefix
        {
            get { return frequencyPrefix; }
            set { frequencyPrefix = value; }
        }
        private string frequencyPostfix;

        [Description("Text that follows the frequency number in an amplitude command."),
Category("Frequency")]
        public string FrequencyPostfix
        {
            get { return frequencyPostfix; }
            set { frequencyPostfix = value; }
        }
        private string deviceIdentifierString;

        [Description("String used to detect which devices use this ramp converter. This string should be a substring of the desired device's device identifier string."),
Category("Device detection")]
        public string DeviceIdentifierSubstring
        {
            get { return deviceIdentifierString; }
            set { deviceIdentifierString = value; }
        }

        public GpibRampCommandConverter(string a_pre, string a_post, string f_pre, string f_post, string deviceStr)
        {
            this.amplitudePrefix = a_pre;
            this.amplitudePostfix = a_post;
            this.frequencyPrefix = f_pre;
            this.frequencyPostfix = f_post;
            this.deviceIdentifierString = deviceStr;
        }

        public GpibRampCommandConverter() : this ("", "", "", "", "")
        {
        }

        public string frequencyCommand(double freq)
        {
            return (FrequencyPrefix + freq.ToString() + FrequencyPostfix).Replace("\\n", "\n").Replace("\\r", "\r");
        }

        public string amplitudeCommand(double amp)
        {
            return (AmplitudePrefix + amp.ToString() + AmplitudePostfix).Replace("\\n", "\n").Replace("\\r", "\r");
        }

        public string Command(double freq, double amp)
        {
            return frequencyCommand(freq) + amplitudeCommand(amp);
        }
    }
}
