using DataStructures.Timing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CiceroSuiteUnitTests
{
    
    
    /// <summary>
    ///This is a test class for NetworkClockDatagramTest and is intended
    ///to contain all NetworkClockDatagramTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NetworkClockDatagramTest
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
        ///A test for toByteStream
        ///</summary>
        [TestMethod()]
        public void toByteStreamTest()
        {
            Random rnd = new Random();
            for (int i = 0; i < 30; i++)
            {
                NetworkClockDatagram testGram = new NetworkClockDatagram((uint)rnd.Next(), (uint)rnd.Next(), (uint)rnd.Next());
                byte []bytes = testGram.toByteStream();
                NetworkClockDatagram resultGram = new NetworkClockDatagram(bytes);
                Assert.AreEqual(testGram, resultGram);
                //Assert.AreEqual(testGram.ElaspedTime, resultGram.ElaspedTime);
                //Assert.AreEqual(testGram.ClockID, resultGram.ClockID);
            }
        }
    }
}
