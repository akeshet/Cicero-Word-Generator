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

        [TestMethod()]
        public void retriggerOptionsSerializationTest()
        {
            SequenceData target = (SequenceData)SharedTestFunctions.loadTestFile("retriggers.seq", typeof(SequenceData));
            Assert.AreEqual(false, target.TimeSteps[0].RetriggerOptions.WaitForRetrigger);
            Assert.AreEqual(true, target.TimeSteps[1].RetriggerOptions.WaitForRetrigger);
            Assert.AreEqual(true, target.TimeSteps[2].RetriggerOptions.WaitForRetrigger);

            Assert.AreEqual(true, target.TimeSteps[1].RetriggerOptions.RetriggerOnEdge);
            Assert.AreEqual(true, target.TimeSteps[1].RetriggerOptions.RetriggerOnNegativeValueOrEdge);

            Assert.AreEqual(false, target.TimeSteps[2].RetriggerOptions.RetriggerOnEdge);
            Assert.AreEqual(false, target.TimeSteps[2].RetriggerOptions.RetriggerOnNegativeValueOrEdge);

            Assert.AreEqual((double)17, target.TimeSteps[1].RetriggerOptions.RetriggerTimeout.getBaseValue());            
        }

        /// <summary>
        /// Tests some complicated sequences the ensure that they reproduce old buffer generation behavior.
        ///</summary>
        [TestMethod()]
        public void bufferSnapshotTest()
        {
            // A complicated test snapshot which exercises most cicero features
            // (pulses, permanent variables, equations, timestep loops, lists, iteration counts,
            // 
            testSnapshot("bufferGenTest1.buf");

            // A simple test snapshot which exercises pulses with start or end
            // pretriggers or delays
            testSnapshot("bufferGenTest2.buf");
        }

        private void testSnapshot(string path)
        {
            BufferTestSnapshot snapshot = (BufferTestSnapshot) SharedTestFunctions.loadTestFile(path, typeof(BufferTestSnapshot));

            BufferTestSnapshot newShapshot = snapshot.Sequence._createBufferSnapshot(snapshot.Settings, snapshot.MasterTimebaseSampleDuration);


            // compare digital buffers
            Assert.AreEqual(snapshot.DigitalFixed.Count, newShapshot.DigitalFixed.Count,
                "Number of digital channels differ.");
            foreach (int digitalId in snapshot.Settings.logicalChannelManager.Digitals.Keys)
            {
                Assert.AreEqual(snapshot.DigitalFixed[digitalId].Length, newShapshot.DigitalFixed[digitalId].Length,
                    "Digital fixed buffer lengths differ.");
                for (int i = 0; i < snapshot.DigitalFixed[digitalId].Length; i++)
                    Assert.AreEqual(snapshot.DigitalFixed[digitalId][i], newShapshot.DigitalFixed[digitalId][i],
                        "Snapshots differ at Digital Fixed, channel id " + digitalId + " sample " + i);

                Assert.AreEqual(snapshot.DigitalVar[digitalId].Length, newShapshot.DigitalVar[digitalId].Length,
                    "Digital var buffer lengths differ.");
                for (int i = 0; i < snapshot.DigitalVar[digitalId].Length; i++)
                    Assert.AreEqual(snapshot.DigitalVar[digitalId][i], newShapshot.DigitalVar[digitalId][i],
                        "Snapshots differ at Digital Var, channel id " + digitalId + " sample " + i);
            }

            // compare analog buffers
            Assert.AreEqual(snapshot.AnalogFixed.Count, newShapshot.AnalogFixed.Count,
                "Number of analog channels differ.");
            foreach (int analogId in snapshot.Settings.logicalChannelManager.Analogs.Keys)
            {
                Assert.AreEqual(snapshot.AnalogFixed[analogId].Length, newShapshot.AnalogFixed[analogId].Length,
                    "Analog fixed buffer lengths differ.");
                for (int i = 0; i < snapshot.AnalogFixed[analogId].Length; i++)
                    Assert.AreEqual(snapshot.AnalogFixed[analogId][i], newShapshot.AnalogFixed[analogId][i],
                        "Snapshots differ at Analog Fixed, channel id " + analogId + " sample " + i);

                Assert.AreEqual(snapshot.AnalogVar[analogId].Length, newShapshot.AnalogVar[analogId].Length,
                    "Analog var buffer lengths differ.");
                for (int i = 0; i < snapshot.AnalogVar[analogId].Length; i++)
                    Assert.AreEqual(snapshot.AnalogVar[analogId][i], newShapshot.AnalogVar[analogId][i],
                        "Snapshots differ at Analog Var, channel id " + analogId + " sample " + i);
            }
        }




        /// <summary>
        ///A test for getTimeStepAtTime
        ///</summary>
        [TestMethod()]
        public void getTimeStepAtTimeTest()
        {
            SequenceData target = (SequenceData)SharedTestFunctions.loadTestFile("gettimesteptestseq.seq", typeof(SequenceData), true, null);
            Assert.AreEqual("1", target.getTimeStepAtTime(.5).StepName);
            Assert.AreEqual("1", target.getTimeStepAtTime(.5).StepName);
            Assert.AreEqual("1", target.getTimeStepAtTime(1).StepName);
            Assert.AreEqual("2", target.getTimeStepAtTime(1.5).StepName);
            Assert.AreEqual("4", target.getTimeStepAtTime(4).StepName);
            Assert.IsNull(target.getTimeStepAtTime(-1));
            Assert.IsNull(target.getTimeStepAtTime(10));
        }
    }
}
