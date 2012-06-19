using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{

    public class MessageEvent : EventArgs
    {
        private string myString;

        public string MyString
        {
            get { return myString; }
        }

        private DateTime myTime;

        public DateTime MyTime
        {
            get { return myTime; }
        }

        private int verbosity;

        public int Verbosity
        {
            get { return verbosity; }
        }

        public enum MessageTypes { Routine, Warning, Error, Log };
        private MessageTypes messageType;

        public MessageTypes MessageType
        {
            get { return messageType; }
        }

        public enum MessageCategories { Unspecified, GPIB, Serial, RFSG, SoftwareClock, Networking };
        private MessageCategories messageCategory;

        public MessageCategories MessageCategory
        {
            get { return messageCategory; }
        }

        public override string ToString()
        {
            return myString;
        }

        public MessageEvent(String messageString, DateTime messageTime, int messageVerbosity = 0,
            MessageTypes messageType = MessageTypes.Routine, MessageCategories messageCategory = MessageCategories.Unspecified)
        {
            myString = messageString;
            myTime = messageTime;
            verbosity = messageVerbosity;
            this.messageType = messageType;
            this.messageCategory = messageCategory;
        }

        public MessageEvent(String messageString, int messageVerbosity = 0,
            MessageTypes messageType = MessageTypes.Routine, MessageCategories messageCategory = MessageCategories.Unspecified) :
            this(messageString, DateTime.Now, messageVerbosity, messageType, messageCategory) { }

    }
}
