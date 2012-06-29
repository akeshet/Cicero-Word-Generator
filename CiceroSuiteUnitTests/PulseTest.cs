using DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CiceroSuiteUnitTests
{
    
    
    /// <summary>
    ///This is a test class for PulseTest and is intended
    ///to contain all PulseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PulseTest
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
        /// Test that pulse parameters are deserialized properly
        ///</summary>
        [TestMethod()]
        public void PulseDeserializationTest()
        {
            SequenceData seq = (SequenceData) SharedTestFunctions.loadTestFile("pulseSerTest.seq", typeof(SequenceData));
            Assert.IsNotNull(seq.DigitalPulses[0]);
            Assert.AreEqual("testPulse", seq.DigitalPulses[0].PulseName);
            Assert.AreEqual(1.5, seq.DigitalPulses[0].startDelay.getBaseValue());
            Assert.AreEqual(2.5, seq.DigitalPulses[0].pulseDuration.getBaseValue());
        }
    }
}
