using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeus
{
    public class HardwareChannelRule
    {
        private string _channelName;
        public string ChannelName
        {
            get { return _channelName; }
            set { _channelName = value; }
        }
        private int _inputPin;
        public int InputPin
        {
            get { return _inputPin; }
            set { _inputPin = value; }
        }

        private int _responsePin;
        public int ResponsePin
        {
            get { return _responsePin; }
            set { _responsePin = value; }
        }

        private bool _useResponse;
        public bool UseResponse
        {
            get { return _useResponse; }
            set { _useResponse = value; }
        }

        private bool _desiredState;
        public bool DesiredState
        {
            get { return _desiredState; }
            set { _desiredState = value; }
        }

        public HardwareChannelRule()
        {
            _channelName = "Interlock 1";
            _inputPin = 1;
            _useResponse = false;

        }
    }
}
