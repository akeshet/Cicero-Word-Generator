using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace AtticusServer
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class AnalogInChannels
    {
        //<AD>
        #region Old Code
        //#region Single

        //private bool aS0;
        //[Description("AS0.")]
        //public bool AS0
        //{
        //    get { return aS0; }
        //    set { aS0 = value; }
        //}
        
        //private bool aS1;
        //[Description("AS1.")]
        //public bool AS1
        //{
        //    get { return aS1; }
        //    set { aS1 = value; }
        //}

        //private bool aS2;
        //[Description("AS2.")]
        //public bool AS2
        //{
        //    get { return aS2; }
        //    set { aS2 = value; }
        //}

        //private bool aS3;
        //[Description("AS3.")]
        //public bool AS3
        //{
        //    get { return aS3; }
        //    set { aS3 = value; }
        //}

        //private bool aS4;
        //[Description("AS4.")]
        //public bool AS4
        //{
        //    get { return aS4; }
        //    set { aS4 = value; }
        //}

        //private bool aS5;
        //[Description("AS5.")]
        //public bool AS5
        //{
        //    get { return aS5; }
        //    set { aS5 = value; }
        //}

        //private bool aS6;
        //[Description("AS6.")]
        //public bool AS6
        //{
        //    get { return aS6; }
        //    set { aS6 = value; }
        //}

        //private bool aS7;
        //[Description("AS7.")]
        //public bool AS7
        //{
        //    get { return aS7; }
        //    set { aS7 = value; }
        //}

        //private bool aS8;
        //[Description("AS8.")]
        //public bool AS8
        //{
        //    get { return aS8; }
        //    set { aS8 = value; }
        //}

        //private bool aS9;
        //[Description("AS9.")]
        //public bool AS9
        //{
        //    get { return aS9; }
        //    set { aS9 = value; }
        //}

        //#endregion Single
       
        //#region Differential
        
        //private bool aD0;
        //[Description("AD0.")]
        //public bool AD00
        //{
        //    get { return aD0; }
        //    set { aD0 = value; }
        //}

        //private bool aD1;
        //[Description("AD1.")]
        //public bool AD01
        //{
        //    get { return aD1; }
        //    set { aD1 = value; }
        //}

        //private bool aD2;
        //[Description("AD2.")]
        //public bool AD02
        //{
        //    get { return aD2; }
        //    set { aD2 = value; }
        //}

        //private bool aD3;
        //[Description("AD3.")]
        //public bool AD03
        //{
        //    get { return aD3; }
        //    set { aD3 = value; }
        //}

        //private bool aD4;
        //[Description("AD4.")]
        //public bool AD04
        //{
        //    get { return aD4; }
        //    set { aD4 = value; }
        //}

        //private bool aD5;
        //[Description("AD5.")]
        //public bool AD05
        //{
        //    get { return aD5; }
        //    set { aD5 = value; }
        //}

        //private bool aD6;
        //[Description("AD6.")]
        //public bool AD06
        //{
        //    get { return aD6; }
        //    set { aD6 = value; }
        //}

        //private bool aD7;
        //[Description("AD7.")]
        //public bool AD07
        //{
        //    get { return aD7; }
        //    set { aD7 = value; }
        //}

        //private bool aD8;
        //[Description("AD8.")]
        //public bool AD08
        //{
        //    get { return aD8; }
        //    set { aD8 = value; }
        //}

        //private bool aD9;
        //[Description("AD9.")]
        //public bool AD09
        //{
        //    get { return aD9; }
        //    set { aD9 = value; }
        //}

        //private bool aD10;
        //[Description("AD10.")]
        //public bool AD10
        //{
        //    get { return aD10; }
        //    set { aD10 = value; }
        //}

        //#endregion Differential

        #endregion


        private string deviceName = AtticusServer.server.serverSettings.AIDev;

        [Description(""),
        Category("Names")]
        public string DeviceName
        {
            get { return deviceName; }
            //set { serverName = value; }
        }

        private string channelName;

        [Description(""),
        Category("Names")]
        public string ChannelName
        {
            get { return channelName; }
            set { channelName = value; }
        }

        private string saveName;

        [Description(""),
        Category("Names")]
        public string SaveName
        {
            get { return saveName; }
            set { saveName = value; }
        }

        private string label;

        [Description(""),
        Category("Names")]
        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        public enum AnalogInChannelTypeList { SingleEnded, Differential };

        private AnalogInChannelTypeList analogInChannelType;

        [Description(""),
        Category("Settings")]
        public AnalogInChannelTypeList AnalogInChannelType
        {
            get { return analogInChannelType; }
            set { analogInChannelType = value; }
        }

        private bool useChannel = false;

        [Description(""),
        Category("Settings")]
        public bool UseChannel
        {
            get { return useChannel; }
            set { useChannel = value; }
        }

        public AnalogInChannels()
        {
            #region OldCode
            #region Single
            //aS0 = false;
            //aS1 = false;
            //aS2 = false;
            //aS3 = false;
            //aS4 = false;
            //aS5 = false;
            //aS6 = false;
            //aS7 = false;
            //aS8 = false;
            //aS9 = false;
            #endregion Single

            #region Differential
            //aD0 = true;
            //aD1 = false;
            //aD2 = false;
            //aD3 = false;
            //aD4 = false;
            //aD5 = false;
            //aD6 = false;
            //aD7 = false;
            //aD8 = false;
            //aD9 = false;
            //aD10 = false;
            #endregion Differential
            #endregion OldCode

            deviceName = AtticusServer.server.serverSettings.AIDev;
            channelName = "ai";
            saveName = "AI";

        }

        //</AD>
    }
}
