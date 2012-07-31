using Cicero.DataStructures2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CiceroSuiteUnitTests
{
    
    
    /// <summary>
    ///This is a test class for ResourceIDTest and is intended
    ///to contain all ResourceIDTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ResourceIDTest
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
        ///A test for newRandom
        ///</summary>
        [TestMethod()]
        public void newRandomTest()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            HashSet<ResourceID> ids = new HashSet<ResourceID>();
            int genNumber = 1000000;
            for (int i = 0; i < genNumber; i++)
            {
                Assert.IsTrue(ids.Add(ResourceID.newRandom(ids)), "A duplicate resourceID was created.");
            }
            stopwatch.Stop();
            Assert.IsFalse(stopwatch.ElapsedMilliseconds > 1000, "Took longer than 10s to generate " + genNumber + " unique random resource IDs ("+stopwatch.ElapsedMilliseconds + "ms).");


        }
    }
}
