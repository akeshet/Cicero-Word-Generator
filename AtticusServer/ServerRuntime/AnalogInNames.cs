using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace AtticusServer
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class AnalogInNames
    {
        #region Single
        
        private string aS0;
        [Description("AS0.")]
        public string AS0
        {
            get { return aS0; }
            set { aS0 = value; }
        }
        
        private string aS1;
        [Description("AS1.")]
        public string AS1
        {
            get { return aS1; }
            set { aS1 = value; }
        }

        private string aS2;
        [Description("AS2.")]
        public string AS2
        {
            get { return aS2; }
            set { aS2 = value; }
        }

        private string aS3;
        [Description("AS3.")]
        public string AS3
        {
            get { return aS3; }
            set { aS3 = value; }
        }

        private string aS4;
        [Description("AS4.")]
        public string AS4
        {
            get { return aS4; }
            set { aS4 = value; }
        }

        private string aS5;
        [Description("AS5.")]
        public string AS5
        {
            get { return aS5; }
            set { aS5 = value; }
        }

        private string aS6;
        [Description("AS6.")]
        public string AS6
        {
            get { return aS6; }
            set { aS6 = value; }
        }

        private string aS7;
        [Description("AS7.")]
        public string AS7
        {
            get { return aS7; }
            set { aS7 = value; }
        }

        private string aS8;
        [Description("AS8.")]
        public string AS8
        {
            get { return aS8; }
            set { aS8 = value; }
        }

        private string aS9;
        [Description("AS9.")]
        public string AS9
        {
            get { return aS9; }
            set { aS9 = value; }
        }

        #endregion Single
       
        #region Differential
        
        private string aD0;
        [Description("AD0.")]
        public string AD00
        {
            get { return aD0; }
            set { aD0 = value; }
        }

        private string aD1;
        [Description("AD1.")]
        public string AD01
        {
            get { return aD1; }
            set { aD1 = value; }
        }

        private string aD2;
        [Description("AD2.")]
        public string AD02
        {
            get { return aD2; }
            set { aD2 = value; }
        }

        private string aD3;
        [Description("AD3.")]
        public string AD03
        {
            get { return aD3; }
            set { aD3 = value; }
        }

        private string aD4;
        [Description("AD4.")]
        public string AD04
        {
            get { return aD4; }
            set { aD4 = value; }
        }

        private string aD5;
        [Description("AD5.")]
        public string AD05
        {
            get { return aD5; }
            set { aD5 = value; }
        }

        private string aD6;
        [Description("AD6.")]
        public string AD06
        {
            get { return aD6; }
            set { aD6 = value; }
        }

        private string aD7;
        [Description("AD7.")]
        public string AD07
        {
            get { return aD7; }
            set { aD7 = value; }
        }

        private string aD8;
        [Description("AD8.")]
        public string AD08
        {
            get { return aD8; }
            set { aD8 = value; }
        }

        private string aD9;
        [Description("AD9.")]
        public string AD09
        {
            get { return aD9; }
            set { aD9 = value; }
        }

        private string aD10;
        [Description("AD10.")]
        public string AD10
        {
            get { return aD10; }
            set { aD10 = value; }
        }

        #endregion Differential

        public AnalogInNames()
        {
            #region Single
            aS0 = "";
            aS1 = "";
            aS2 = "";
            aS3 = "";
            aS4 = "";
            aS5 = "";
            aS6 = "";
            aS7 = "";
            aS8 = "";
            aS9 = "";
            #endregion Single

            #region Differential
            aD0 = "";
            aD1 = "";
            aD2 = "";
            aD3 = "";
            aD4 = "";
            aD5 = "";
            aD6 = "";
            aD7 = "";
            aD8 = "";
            aD9 = "";
            aD10 = "";
            #endregion Differential

        }
    }
}
