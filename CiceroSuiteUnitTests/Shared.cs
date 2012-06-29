using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CiceroSuiteUnitTests
{
    public class SharedTestFunctions
    {
        public static object loadTestFile(string path, Type desiredType, 
            bool failOnException = true, SerializationBinder customBinder = null)
        {
            Assert.IsTrue(System.IO.File.Exists(path), "Test file " + path + " does not exist.");
            BinaryFormatter b = new BinaryFormatter();
            object loaded = null;
            FileStream fs = null;

            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                if (customBinder != null)
                    b.Binder = customBinder;
                loaded = b.Deserialize(fs);
                fs.Close();
            }
            catch (Exception e)
            {
                fs.Close();

                if (failOnException)
                    Assert.Fail("Caught exception when loading test file: " + e.Message);
                else
                    throw;
            }

            Assert.IsNotNull(loaded, "Test file " + path + " loaded a null object.");
            Assert.IsInstanceOfType(loaded, desiredType, "Test file " + path + " did not resolve to desired type.");
            return loaded;
        }
    }
}
