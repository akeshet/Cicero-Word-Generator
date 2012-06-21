using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures.Timing
{
    public class NetworkClockDatagram
    {
        public static readonly int datagramByteLength = 12;

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

        private UInt32 datagramCount;

        public UInt32 DatagramCount
        {
            get { return datagramCount; }
            set { datagramCount = value; }
        }

        public NetworkClockDatagram(UInt32 elaspedTime, UInt32 clockID, UInt32 datagramCount)
        {
            this.elaspedTime = elaspedTime;
            this.clockID = clockID;
            this.datagramCount = datagramCount;
        }

        public NetworkClockDatagram(byte[] byteStream, int offset)
        {
            if (byteStream.Length - offset < 12)
                throw new SoftwareClockProvider.SoftwareClockProviderException ("Attempted to construct a datagram from too short bytestream");

            elaspedTime = BitConverter.ToUInt32(byteStream, offset);
            clockID = BitConverter.ToUInt32(byteStream, offset + 4);
            datagramCount = BitConverter.ToUInt32(byteStream, offset + 8);
        }

        public NetworkClockDatagram(byte[] byteStream) : this(byteStream, 0) { }

        public byte[] toByteStream()
        {
            byte[] ans = new byte[12];
            byte[] etBytes = BitConverter.GetBytes(elaspedTime);
            byte[] clBytes = BitConverter.GetBytes(clockID);
            byte[] dcBytes = BitConverter.GetBytes(datagramCount);

            for (int i = 0; i < 4; i++)
            {
                ans[i] = etBytes[i];
                ans[i + 4] = clBytes[i];
                ans[i + 8] = dcBytes[i];
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
            if (other.datagramCount != datagramCount)
                return false;
            return true;
        }

        public override string ToString()
        {
            return "clockID: " + String.Format("{0:X8}", clockID) + " time_ms: " + elaspedTime + " gram_num: " + this.datagramCount;
        }

    }
}
