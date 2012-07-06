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

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))           
            {
                return b.Deserialize(fs); // filestream gets closed anyway since that is in the finally block.
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


        /// <summary>
        /// Creates a deep copy of specified object, by serializing and deserialziing it.
        /// </summary>
        /// <param name="objectToCopy"></param>
        /// <returns></returns>
        public static Object createDeepCopyBySerialization(Object objectToCopy)
        {
            // Create a deep copy of the current sequence file, by serializing and then deserializing. A clever trick.
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bf.Serialize(ms, objectToCopy);
                ms.Position = 0;
                return bf.Deserialize(ms);
            }
        }


        /// <summary>
        /// Returns a time stamp string in a suitable format for use in a file name,
        /// and which will sort correctly by name.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string getTimeStampString(DateTime time)
        {
            string temp = time.ToString("s");
            temp = temp.Replace(":", "-");
            return temp;
        }


        /// <summary>
        /// Converts frequency integer (in Hz) to clock period double precision float (in s)
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public static double getPeriodFromFrequency(int frequency)
        {
            return 1.0 / (double)frequency;
        }
    }
}
