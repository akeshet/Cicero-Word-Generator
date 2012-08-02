using Cicero.DataStructures2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Units = DataStructures.Units;
using System.Linq;

namespace CiceroSuiteUnitTests
{
    
    
    /// <summary>
    ///This is a test class for Cicero2ResourceDictionaryTest and is intended
    ///to contain all Cicero2ResourceDictionaryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Cicero2ResourceDictionaryTest
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
        ///A test for AddNew
        ///</summary>
        public void AddNewTestHelper<ResourceType>()
            where ResourceType : Cicero2DataObject
        {
            Cicero2ResourceDictionary target = new Cicero2ResourceDictionary(); // TODO: Initialize to an appropriate value
            ResourceType newResource = default(ResourceType); // TODO: Initialize to an appropriate value
            ResourceID<ResourceType> expected = new ResourceID<ResourceType>(); // TODO: Initialize to an appropriate value
            ResourceID<ResourceType> actual;
            actual = target.AddNew<ResourceType>(newResource);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void AddNewTest()
        {
            Cicero2ResourceDictionary resDict = new Cicero2ResourceDictionary();
            ResourceID<DimensionedParameter> dp1 = resDict.AddNew(new DimensionedParameter(Units.s, 1));
            ResourceID<DimensionedParameter> dp2 = resDict.AddNew(new DimensionedParameter(Units.V, 4));
            ResourceID<Pulse> p1 =  resDict.AddNew(new Pulse(resDict));
            ResourceID<DimensionedParameter> dp3 = resDict.AddNew(new DimensionedParameter(Units.Dimension.Hz));
            ResourceID<Variable> v1 = resDict.AddNew(new Variable());
            resDict.Get(v1).VariableValue = 1.85;

            resDict.Get(dp3).parameter.variable = v1;

            Assert.AreEqual(1, dp1.getBaseValue(resDict));
            Assert.AreEqual(4, dp2.getBaseValue(resDict));
            Assert.AreEqual(1.85, dp3.getBaseValue(resDict));

            Assert.AreEqual(1, resDict.ResourcesOfType<Variable>().Count());
            Assert.AreEqual(1, resDict.ResourcesOfType<Pulse>().Count());

        }
    }
}
