using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures.Timing
{
    public class NetworkClockDatagram
    {
        private UInt32 elaspedTime;

        public UInt32 ElaspedTime
        {
            get { return elaspedTime; }
        }
        private UInt32 clockID;

        public UInt32 ClockID
        {
            get { return clockID; }
        }

        public NetworkClockDatagram(UInt32 elaspedTime, UInt32 clockID)
        {
            this.elaspedTime = elaspedTime;
            this.clockID = clockID;
        }

        public NetworkClockDatagram(byte[] byteStream, int offset)
        {
            if (byteStream.Length - offset < 8)
                throw new SoftwareClockProvider.SoftwareClockProviderException ("Attempted to construct a datagram from too short bytestream");

            elaspedTime = BitConverter.ToUInt32(byteStream, offset);
            clockID = BitConverter.ToUInt32(byteStream, offset + 4);
        }

        public NetworkClockDatagram(byte[] byteStream) : this(byteStream, 0) { }

        public byte[] toByteStream()
        {
            byte[] ans = new byte[8];
            byte[] etBytes = BitConverter.GetBytes(elaspedTime);
            byte[] clByes = BitConverter.GetBytes(clockID);

            for (int i = 0; i < 4; i++)
            {
                ans[i] = etBytes[i];
                ans[i + 4] = clByes[i];
            }

            return ans;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is NetworkClockDatagram))
                return false;
            NetworkClockDatagram other = (NetworkClockDatagram)obj;
            if (other.clockID != clockID)
                return false;
            if (other.elaspedTime != elaspedTime)
                return false;
            return true;
        }

    }
}
