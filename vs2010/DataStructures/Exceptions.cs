using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    /// <summary>
    /// This is an exception to be thrown by data processing methods if they come across
    /// invalid data.
    /// </summary>
    public class InvalidDataException : Exception
    {
        public InvalidDataException (string message) : base(message) {}
    }

    /// <summary>
    /// This is an exception thrown by Waveform.InterpolationType if it is given data which it 
    /// is unable to interpret.
    /// </summary>
    public class InterpolationException : InvalidDataException
    {
        public InterpolationException(string message) : base(message) {}
    }
}
