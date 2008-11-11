using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataStructures
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class LogicalChannel
    {
        private bool enabled;

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        //Hotkey for toggling the override value.
        public char hotkeyChar;

        //Hotkey for turning override on and off.
        public char overrideHotkeyChar;

        public bool overridden;

        public bool digitalOverrideValue;
        public double analogOverrideValue;

        public string name;
        public string description;

        public HardwareChannel hardwareChannel;

        public LogicalChannel()
        {
            name = "";
            description = "";
            hardwareChannel = HardwareChannel.Unassigned;
            enabled = true;
            overridden = false;
            digitalOverrideValue = false;
            analogOverrideValue = 0;
        }

        /// <summary>
        /// Meaningfull only for analog logical channels. If true, then when running "output now" the analog channel
        /// gets its value from the end of the dwell word. Otherwise, it gets its value from the end of the output word.
        /// </summary>
        public bool analogChannelOutputNowUsesDwellWord;

    }
}
