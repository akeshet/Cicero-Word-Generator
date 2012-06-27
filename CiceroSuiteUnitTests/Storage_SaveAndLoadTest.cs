using WordGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataStructures;

namespace CiceroSuiteUnitTests
{
    
    
    /// <summary>
    ///This is a test class for Storage_SaveAndLoadTest and is intended
    ///to contain all Storage_SaveAndLoadTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Storage_SaveAndLoadTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Load
        ///</summary>
        [DeploymentItem("WordGenerator.exe"), TestMethod()]
        public void LoadTest()
        {
            /*
            string path = string.Empty; // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            actual = Storage_Accessor.SaveAndLoad.Load(path);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");*/


            testLoadFile("SettingsData-1.5.set", typeof(DataStructures.SettingsData));
            testLoadFile("Empty1.60Sequence.seq", typeof(DataStructures.SequenceData));
            testLoadFile("Empty1.61Sequence.seq", typeof(DataStructures.SequenceData));
            

        }

        /// <summary>
        ///A test for loading old gpib channels
        ///</summary>
        [DeploymentItem("WordGenerator.exe"), TestMethod()]
        public void LoadOldGpibTest()
        {


            SettingsData gpibTestSettings = (SettingsData)
                testLoadFile("OldGpibAddressSettings.set", typeof(SettingsData));

            Assert.AreEqual(5, gpibTestSettings.logicalChannelManager.GPIBs.Count);
            for (int i = 0; i < gpibTestSettings.logicalChannelManager.GPIBs.Count; i++)
            {
                LogicalChannel chan = gpibTestSettings.logicalChannelManager.GPIBs[i];
                Assert.AreEqual(HardwareChannel.HardwareConstants.ChannelTypes.gpib, chan.HardwareChannel.ChannelType);
                Assert.AreEqual("test description " + i, chan.HardwareChannel.ChannelDescription);
                Assert.AreEqual("test channel " + i, chan.HardwareChannel.ChannelName);
                Assert.AreEqual("test device " + i, chan.HardwareChannel.DeviceName);
                Assert.AreEqual(i, chan.HardwareChannel.GpibAddress.PrimaryAddress);
                Assert.AreEqual(i, chan.HardwareChannel.GpibAddress.SecondaryAddress);
            }


        }

        private object testLoadFile(string path, Type desiredType)
        {
            Assert.IsTrue(System.IO.File.Exists(path), "Test file " + path + " does not exist.");
            object loadedObject = Storage_Accessor.SaveAndLoad.Load(path);
            Assert.IsNotNull(loadedObject, "Test file " + path + " loaded a null object.");
            Assert.IsInstanceOfType(loadedObject, desiredType, "Test file " + path + " did not resolve to desired type.");
            return loadedObject;
        }
    }
}
