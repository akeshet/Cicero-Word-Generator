using dotMath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Globalization;

namespace CiceroSuiteUnitTests
{
    
    
    /// <summary>
    ///This is a test class for EqCompilerTest and is intended
    ///to contain all EqCompilerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EqCompilerTest
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
        ///Tests that the dotMath compile function works correctly even when installed
        ///under a French culture.
        ///
        /// (Previously, used to fail due to attempting to parse floating point numbers
        /// in the French format of 1,23 instead of 1.23
        ///</summary>
        [TestMethod()]
        public void CompileTest()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");

            string testEquation = "3.2+2";
            EqCompiler compiler = new EqCompiler(testEquation, true);
            compiler.Compile();
            Assert.AreEqual(5.2, compiler.Calculate());

        }
    }
}
