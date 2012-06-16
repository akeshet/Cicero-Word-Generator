using DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CiceroSuiteUnitTests
{
    
    
    /// <summary>
    ///This is a test class for VariableTest and is intended
    ///to contain all VariableTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VariableTest
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
        ///A test for parseVariableFormula
        ///</summary>
        [TestMethod()]
        public void parseVariableFormulaTest()
        {
            Variable var1 = new Variable();
            var1.VariableName = "var1";
            List<Variable> allVariables = new List<Variable>();
            allVariables.Add(var1);
            
            var1.DerivedVariable = true;
            var1.VariableFormula = "1+5";
            Assert.IsNull(var1.parseVariableFormula(allVariables));
            Assert.AreEqual((double) 6, var1.VariableValue);

            Variable var2 = new Variable();
            var2.VariableName = "var2";
            var2.VariableValue = 10;
            allVariables.Add(var2);
            var1.VariableFormula = "var2 * 6 + 10";
            Assert.IsNull(var1.parseVariableFormula(allVariables));
            Assert.AreEqual((double)70, var1.VariableValue);

            var2.DerivedVariable = true;
            var2.VariableFormula = "var1 + 1";
            Assert.AreEqual(Variable.EQUATION_ERROR_RECURSIVE,
                var1.parseVariableFormula(allVariables));

            var2.VariableFormula = "!#$^@#$%"; // gibberish
            Assert.IsNotNull(var1.parseVariableFormula(allVariables));         

        }
    }
}
