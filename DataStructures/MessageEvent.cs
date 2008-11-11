using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class MessageEvent : EventArgs
    {
        public string myString;

        public DateTime myTime;

        public override string ToString()
        {
            return myString;
        }
        public MessageEvent(string aString)
        {
            myString = aString;
            myTime = DateTime.Now;
        }

        public MessageEvent(string aString, DateTime eventTime)
        {
            myString = aString;
            myTime = eventTime;
        }
    }
}
