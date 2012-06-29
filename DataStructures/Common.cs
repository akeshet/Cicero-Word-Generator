using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataStructures
{
    public class Common
    {
        /// <summary>
        /// Attempts to load a binary serialized object from the given file. Uses appropriate
        /// SerializationBinder which fixes issue loading old files.
        /// 
        /// Does not catch any exceptions which may result from loading file or trying to deserialize it.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Object loadBinaryObjectFromFile(String path)
        {
            if (path == null) 
                return null;

            BinaryFormatter b = new BinaryFormatter();
            b.Binder = new GpibBinderFix();
            bool fileOpened = false;
            FileStream fs = null;

            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                fileOpened = true;
                return b.Deserialize(fs); // filestream gets closed anyway since that is in the finally block.
            }
            finally
            {
                if (fileOpened)
                    fs.Close();
            }

        }

        /// <summary>
        /// This custom serialization binder fixed otherwise incompatibility between old and new
        ///  serialized files. Without using this binder, attemptiong to load a file which has any
        ///  reference to a NationalInstruments.NI4882.Address throws an ArgumentException.
        ///  
        /// With this serialization binder in place, the exceptions no not occur and files can be deserilized.
        /// HOWEVER -- Gpib Address information will be lost (reset to 0, 0), so the user will need to fix this
        /// manually and re-save the file.
        /// </summary>
        public sealed class GpibBinderFix : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {

                if (typeName == "NationalInstruments.NI4882.Address")
                {
                    return typeof(DataStructures.Gpib.Address);
                }
                else
                    return Type.GetType(typeName);
            }

        }
    }
}
