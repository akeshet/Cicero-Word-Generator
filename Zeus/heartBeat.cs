using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zeus
{
    public class heartBeat
    {
        private string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        private int _group;
        public int group
        {
            get { return _group; }
            set { _group = value; }
        }
        public heartBeat()
        {
            _name = "camera1";
            _group = 1;
        }
    }
}
