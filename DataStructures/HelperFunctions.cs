using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public class HelperFunctions
    {
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
    }
}
