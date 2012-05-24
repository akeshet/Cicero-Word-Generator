using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DataStructures.Gpib
{
    [Serializable, TypeConverter(typeof(ExpandableStructConverter))]
    public struct Address
    {

        public Address(byte primaryAddress)
        {
            this.primaryAddress = primaryAddress;
            this.secondaryAddress = 0;
        }

        public Address(byte primaryAddress, byte secondaryAddress)
        {
            this.primaryAddress = primaryAddress;
            this.secondaryAddress = secondaryAddress;
        }


        private byte primaryAddress;

        public byte PrimaryAddress
        {
            get { return primaryAddress; }
            set { primaryAddress = value; }
        }


        private byte secondaryAddress;

        public byte SecondaryAddress
        {
            get { return secondaryAddress; }
            set { secondaryAddress = value; }
        }

        public override string ToString()
        {
            return "Address: PrimaryAddress="+primaryAddress.ToString() + ", SecondaryAddress=" + secondaryAddress.ToString();
        }

    }
}
