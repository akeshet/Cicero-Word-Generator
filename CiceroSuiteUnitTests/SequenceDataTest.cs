using DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CiceroSuiteUnitTests
{
    
    
    /// <summary>
    ///This is a test class for SequenceDataTest and is intended
    ///to contain all SequenceDataTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SequenceDataTest
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
        ///A test for SequenceData Constructor
        ///</summary>
        [TestMethod()]
        public void SequenceDataConstructorTest()
        {
            SequenceData target = new SequenceData();
            Assert.IsTrue(target.VersionNumberAtFirstCreation.isValid(), 
                "Invalid/unparseable version number in DataStructures. Please" +
                " ensure DataStructures version number string is of the form" +
                " X.YZ");
        }

        /// <summary>
        ///A test for usedVariables
        ///</summary>
        [TestMethod()]
        public void usedVariablesTest()
        {
            SequenceData target = new SequenceData(); // TODO: Initialize to an appropriate value
            Assert.AreEqual(0, target.usedVariables().Count);

            target.Variables.Add(new Variable());
            target.Variables[0].VariableName = "Variable Name";
            Assert.AreEqual(0, target.usedVariables().Count); // Unused variables in variable list should still not appear

            target.TimeSteps.Add(new TimeStep());
            target.TimeSteps[0].StepDuration.parameter.variable = target.Variables[0];
            Assert.AreEqual(1, target.usedVariables().Count); // Unused variables in variable list should still not appear

            target.TimeSteps.Add(new TimeStep());
            target.TimeSteps[1].StepDuration.parameter.variable = target.Variables[0];
            Assert.AreEqual(1, target.usedVariables().Count); // Variables should not appear duplicate times even if used in several places


        }

        /// <summary>
        ///A test for _createBufferSnapshot
        ///</summary>
        [TestMethod()]
        public void _createBufferSnapshotTest()
        {
            

        }

        private void testSnapshot(string path)
        {
            BufferTestSnapshot snapshot = (BufferTestSnapshot) Shared.loadTestFile(path, typeof(BufferTestSnapshot));

            BufferTestSnapshot newShapshot = snapshot.Sequence._createBufferSnapshot(snapshot.Settings, snapshot.MasterTimebaseSampleDuration);

            // compare digital buffers
            foreach (int digitalId in snapshot.Settings.logicalChannelManager.Digitals.Keys)
            {
                for (int i = 0; i < snapshot.DigitalFixed[digitalId].Length; i++)
                    Assert.AreEqual(snapshot.DigitalFixed[digitalId][i], newShapshot.DigitalFixed[digitalId][i],
                        "Snapshots differ at Digital Fixed, channel id " + digitalId + " sample " + i);

                for (int i = 0; i < snapshot.DigitalVar[digitalId].Length; i++)
                    Assert.AreEqual(snapshot.DigitalVar[digitalId][i], newShapshot.DigitalVar[digitalId][i],
                        "Snapshots differ at Digital Var, channel id " + digitalId + " sample " + i);
            }

            // compare analog buffers
            foreach (int analogId in snapshot.Settings.logicalChannelManager.Analogs.Keys)
            {
                for (int i = 0; i < snapshot.AnalogFixed[analogId].Length; i++)
                    Assert.AreEqual(snapshot.AnalogFixed[analogId][i], newShapshot.AnalogFixed[analogId][i],
                        "Snapshots differ at Analog Fixed, channel id " + analogId + " sample " + i);

                for (int i = 0; i < snapshot.AnalogVar[analogId].Length; i++)
                    Assert.AreEqual(snapshot.AnalogVar[analogId][i], newShapshot.AnalogVar[analogId][i],
                        "Snapshots differ at Analog Var, channel id " + analogId + " sample " + i);
            }
        }

    }
}
