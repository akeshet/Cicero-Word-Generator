using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeus
{
    public class emailList
    {
        private string addressString;
        public string AddressString
        {
            get { return addressString; }
            set { addressString = value; }
        }

        public emailList()
        {
            addressString = "";


        }
    }
}
