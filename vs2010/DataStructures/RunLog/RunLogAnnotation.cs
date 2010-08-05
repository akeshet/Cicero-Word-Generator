using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataStructures
{
    /// <summary>
    /// This object contains user annotations, which are made after the user sees the image and are then subsequently added to the image file.
    /// </summary>
    /// 
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class RunLogAnnotation
    {
        public enum ImageQuality { NA, None, Bad, Good };
        public enum Flag { None, Discard, Keep, ReTake, Suspicious, Confusing, Inconclusive, Conclusive };
        private ImageQuality imageQual;

        public ImageQuality ImageQual
        {
            get { return imageQual; }
            set { imageQual = value; }
        }
        private Flag runFlag;

        public Flag RunFlag
        {
            get { return runFlag; }
            set { runFlag = value; }
        }

        private string comments;

        public string Comments
        {
            get
            {
                if (comments == null)
                    comments = "";

                return comments;
            }
            set { comments = value; }
        }
    }
}
