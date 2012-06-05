/*
Cicero Word Generator. A software suite for intuitive computer control
 of atomic physics experiments.

Copyright (C) 2008  Aviv Keshet

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

For the full text of the GNU General Public License, 
see <http://www.gnu.org/licenses/>.
*/

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using DataStructures;
using System.Runtime.Serialization;

namespace DataStructures
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class SequenceData
    {
        #region Members and Properties


        private SequenceMode currentMode;

        public SequenceMode CurrentMode
        {
            get { return currentMode; }
            set { currentMode = value; }
        }

        private List<TimestepGroup> timestepGroups;

        public List<TimestepGroup> TimestepGroups
        {
            get
            {
                if (timestepGroups == null)
                {
                    timestepGroups = new List<TimestepGroup>();
                }
                return timestepGroups;
            }
            set { timestepGroups = value; }
        }

        private List<SequenceMode> sequenceModes;

        public List<SequenceMode> SequenceModes
        {
            get
            {
                if (sequenceModes == null)
                    sequenceModes = new List<SequenceMode>();
                return sequenceModes;
            }
            set { sequenceModes = value; }
        }

        /// <summary>
        /// UI should set this to true if step hiding is enabled. 
        /// </summary>
        public bool stepHidingEnabled;

        private bool calibrationShot;

        [Description("Describes whether or not this sequence was run as a Calibration Shot. Meaningful only in run log files. Do not change manually."),
        Category("Global")]
        public bool CalibrationShot
        {
            get { return calibrationShot; }
            set { calibrationShot = value; }
        }


        private string sequenceDescription;

        [Description("A description of the purpose of the sequence."),
        Category("Global")]
        public string SequenceDescription
        {
            get {
                if (sequenceDescription == null)
                    sequenceDescription = "";

                return sequenceDescription; 
            }
            set { sequenceDescription = value; }
        }

        [Serializable, TypeConverter(typeof(ExpandableStructConverter))]
        public struct CalibrationShots
        {
            private bool calibrationShotsEnabled;

            public bool CalibrationShotsEnabled
            {
                get { return calibrationShotsEnabled; }
                set { calibrationShotsEnabled = value; }
            }
            private bool runCalibrationShotFirst;

            public bool RunCalibrationShotFirst
            {
                get { return runCalibrationShotFirst; }
                set { runCalibrationShotFirst = value; }
            }
            private bool runCalibrationShotLast;

            public bool RunCalibrationShotLast
            {
                get { return runCalibrationShotLast; }
                set { runCalibrationShotLast = value; }
            }
            private bool runCalibrationShotEveryN;

            public bool RunCalibrationShotEveryN
            {
                get { return runCalibrationShotEveryN; }
                set { runCalibrationShotEveryN = value; }
            }
            private int runCalibrationShotN;

            public int RunCalibrationShotN
            {
                get { return runCalibrationShotN; }
                set { runCalibrationShotN = value; }
            }
            private SequenceData calibrationShotSequence;

            public SequenceData CalibrationShotSequence
            {
                get { return calibrationShotSequence; }
                set { calibrationShotSequence = value; }
            }

            public bool calibrationShotRequiredOnThisRun(int runNumber, int totalRunCount)
            {
                if (calibrationShotSequence == null)
                    return false;
                if (!calibrationShotsEnabled)
                    return false;
                if (runCalibrationShotFirst)
                {
                    if (runNumber == 0)
                        return true;
                }
                if (runCalibrationShotLast)
                {
                    if (runNumber == totalRunCount - 1)
                        return true;
                }
                if (runCalibrationShotEveryN)
                {
                    if ((runNumber+1) % RunCalibrationShotN == 0)
                        return true;
                }
                return false;
            }
        }

        public CalibrationShots calibrationShotsInfo;

        public CalibrationShots CalibrationShotsInfo
        {
            get { return calibrationShotsInfo; }
        }




        private string sequenceName;


        /// <summary>
        /// Returns the first enabled timestep, which is going to act as the dwell word for our purposes.
        /// </summary>
        /// <returns></returns>
        public TimeStep dwellWord()
        {
            if (TimeSteps != null)
            {
                for (int i = 0; i < TimeSteps.Count; i++)
                {
                    if (TimeSteps[i].StepEnabled)
                        return TimeSteps[i];
                }
            }
            return new TimeStep();
        }


        private int listIterationNumber=0;

        /// <summary>
        /// The iteration number of the run. This will be zero in general, or non zero if 
        /// the run was part of a "batch list run".
        /// </summary>
        /// 
        [Description("List iteration number of this sequence."),
        Category("Variables")]
        public int ListIterationNumber
        {
            get { return listIterationNumber; }
            set { 
                listIterationNumber = value;
                this.setIterationNumber(listIterationNumber);
            }
        }
 

        [Category("Global"), Description("The name of the sequence.")]
        public string SequenceName
        {
            get {
                if (sequenceName == null)
                    sequenceName = "";
                
                return sequenceName; }
            set { sequenceName = value; }
        }

        public override string ToString()
        {
            return sequenceName;
        }

        [Description("Duration of the sequence, in seconds."),
        Category("Global")]
        public double SequenceDuration
        {
            get
            {
                if (this.TimeSteps == null)
                    return 0;

                double ans = 0;

                foreach (TimeStep step in TimeSteps)
                {
                    if (step.StepEnabled)
                    {
                        ans += step.StepDuration.getBaseValue();
                    }
                }

                return ans;
            }
        }

        private bool aISaved;
        [Browsable(false)]
        public bool AISaved
        {
            get { return aISaved; }
            set { aISaved=value;}
        }

        private void setIterationNumber(int iterationNumber)
        {

            double[] listValues = lists.getListValues(iterationNumber);

            foreach (Variable var in variables)
            {
                if (var.ListDriven)
                    var.VariableValue = listValues[var.ListNumber - 1];

                if (var.IsSpecialVariable && var.MySpecialVariableType == Variable.SpecialVariableType.IterationNum)
                {
                    var.VariableValue = iterationNumber;
                }

                if (var.IsSpecialVariable && var.MySpecialVariableType == Variable.SpecialVariableType.IterationCount)
                {
                    if (lists.ListLocked)
                    {
                        var.VariableValue = lists.iterationsCount();
                    }
                    else
                    {
                        var.VariableValue = 0;
                    }
                }
            }
            foreach (Variable var in variables)
            {
                if (var.DerivedVariable)
                {
                    var.parseVariableFormula(variables);
                }
            }
            
        }


        private List<Variable> variables;

        [Description("A list of the variables used in the sequence."),
        Category("Variables")]
        public List<Variable> Variables
        {
            get {
                if (variables == null)
                {
                    variables = new List<Variable>();
                }

                // This is a hack that makes sure that the first two variables in the list
                // are "special variables" iterCount and iterNum. It is done in this somewhat
                // inelegant way to ensure backwards compatibility when opening sequences that were
                // created before the special variables existed.

                for (int i = 0; i < Enum.GetValues(typeof(Variable.SpecialVariableType)).Length; i++)
                {
                    if (variables.Count <= i)
                    {
                        Variable newVar = new Variable();
                        newVar.IsSpecialVariable = true;
                        newVar.MySpecialVariableType = (Variable.SpecialVariableType) i;
                        newVar.VariableName = newVar.MySpecialVariableType.ToString();
                        variables.Add(newVar);
                    }

                    if (!variables[i].IsSpecialVariable || (variables[i].MySpecialVariableType != (Variable.SpecialVariableType)i))
                    {
                        Variable newVar = new Variable();
                        newVar.IsSpecialVariable = true;
                        newVar.MySpecialVariableType = (Variable.SpecialVariableType)i;
                        newVar.VariableName = newVar.MySpecialVariableType.ToString();
                        variables.Insert(i, newVar);   
                    }
                }
                
                return variables; 
            
            }
            set { variables = value; }
        }

        /// <summary>
        /// When inserting a sequence, the inserted sequence had its own copies of the special variables. Here, we will clean up
        /// and duplicate special variables. This will require the use of the passThroughVariable field of Variable. This is the
        /// only place where this field should be set from.
        /// 
        /// It is possible to make a mess here...
        /// </summary>
        public void cleanupSpecialVariables()
        {
            List<Variable> varsToKill = new List<Variable>();
            for (int i = Enum.GetValues(typeof(Variable.SpecialVariableType)).Length; i < variables.Count; i++)
            {
                if (variables[i].IsSpecialVariable)
                {
                    varsToKill.Add(variables[i]);
                    foreach (Variable var in variables)
                    {
                        if (var.IsSpecialVariable && var.MySpecialVariableType == variables[i].MySpecialVariableType)
                        {
                            if (var == variables[i])
                                throw new Exception("Got confused while attempting to clean up special variables of merged sequence. Giving up. Recommend that the user manually removes the extra special variables, and re-binds any parameters that were using these variables.");
                            variables[i].passThroughVariable = var;
                            break;
                        }
                    }
                }
            }

            foreach (Variable var in varsToKill)
            {
                this.variables.Remove(var);
            }
        }




        /// <summary>
        /// Returns variable object with given name. Null if such a named variable not found.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Variable getVariable(string name)
        {
            foreach (Variable var in Variables)
            {
                if (var.VariableName == name)
                    return var;
            }
            return null;
        }

        /// <summary>
        /// Returns variable #id (1-indexed). Returns null if no such variable.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Variable getVariable(int id)
        {
            if (id > Variables.Count)
                return null;
            return Variables[id - 1];
        }


        private List<TimeStep> steps;

        private List<RS232Group> rs232Groups;

        [Description("A list of the RS232 groups used in the sequence."),
        Category("Groups")]
        public List<RS232Group> RS232Groups
        {
            get {
                if (rs232Groups == null)
                    rs232Groups = new List<RS232Group>();
                return rs232Groups; 
            }
            set { rs232Groups = value; }
        }

        private List<AnalogGroup> analogGroups;

        [Description("A list of the analog groups used in the sequence."),
        Category("Groups")]
        public List<AnalogGroup> AnalogGroups
        {
            get { return analogGroups; }
            set { analogGroups = value; }
        }

        private List<GPIBGroup> gpibGroups;

        [Description("A list of the GPIB groups used in the sequence."),
        Category("Groups")]
        public List<GPIBGroup> GpibGroups
        {
            get { return gpibGroups; }
            set { gpibGroups = value; }
        }

        private List<Pulse> digitalPulses;

        [Description("A list of the digital pulses used in the sequence. These are a slight generalization of groups, for digital channels."),
        Category("Groups")]
        public List<Pulse> DigitalPulses
        {
            get {
                if (digitalPulses == null)
                {
                    digitalPulses = new List<Pulse>();
                }
                return digitalPulses; 
            }
            set { digitalPulses = value; }
        }

        [Category("Sequence"), Description("A list of the timesteps used in the sequence.")]
        public List<TimeStep> TimeSteps
        {
            get
            {
                return steps;
            }
            set { steps = value; }
        }

        private ListData lists;

        [Description("The iteration lists of the sequence."), Category("Variables")]
        public ListData Lists
        {
            get { return lists; }
            set { lists = value; }
        }

        private List<Waveform> commonWaveforms;

        [Category("Sequence"), Description("A list of the common waveforms used in the sequence.")]
        public List<Waveform> CommonWaveforms
        {
            get { return commonWaveforms; }
            set { commonWaveforms = value; }
        }

        [Category("Sequence"), Description("A list of just the enabled timesteps used in the sequence.")]
        public List<TimeStep> enabledTimeSteps()
        {
            List<TimeStep> ans = new List<TimeStep>();
            foreach (TimeStep step in this.TimeSteps)
            {
                if (step != null)
                    if (step.StepEnabled)
                        ans.Add(step);
            }
            return ans;
        }

        [Category("Sequence"), Description("The number of timesteps (included disabled ones) in the sequence.")]
        public int NTimeSteps
        {
            get
            {
                return steps.Count;
            }
        }

        private bool waitForReady;
        [Category("Sequence"), Description("Whether or not the server will use its Ready Wait feature when running this sequence.")]
        public bool WaitForReady
        {
            get { return waitForReady; }
            set { waitForReady = value; }
        }


        #endregion


        #region Constructors

        public SequenceData()
        {
            steps = new List<TimeStep>();
            commonWaveforms = new List<Waveform>();
            analogGroups = new List<AnalogGroup>();
            analogGroups.Add(new AnalogGroup("Unnamed"));
            gpibGroups = new List<GPIBGroup>();
            gpibGroups.Add(new GPIBGroup("Unnamed"));
            variables = new List<Variable>();
            lists = new ListData();
            versionNumberAtFirstCreation = DataStructuresVersionNumber.CurrentVersion;
        }

        /// <summary>
        /// Makes a sequence out of a collection of timesteps, making sure to update the
        /// group lists, variable lists, and common waveform lists.
        /// </summary>
        /// <param name="timeSteps"></param>
        public SequenceData(List<TimeStep> timeSteps) : this()
        {
            steps = new List<TimeStep>();
            foreach (TimeStep step in timeSteps)
            {
                steps.Add(step);
            }

            analogGroups = new List<AnalogGroup>();
            rs232Groups = new List<RS232Group>();
            gpibGroups = new List<GPIBGroup>();
            foreach (TimeStep step in steps)
            {
                if (step.AnalogGroup != null)
                    if (!analogGroups.Contains(step.AnalogGroup))
                        analogGroups.Add(step.AnalogGroup);
                if (step.rs232Group != null)
                    if (!rs232Groups.Contains(step.rs232Group))
                        rs232Groups.Add(step.rs232Group);
                if (step.GpibGroup != null)
                    if (!gpibGroups.Contains(step.GpibGroup))
                        gpibGroups.Add(step.GpibGroup);
            }

            commonWaveforms.AddRange( this.usedCommonWaveforms().Keys );
            variables.AddRange(this.usedVariables().Keys);
            DigitalPulses.AddRange(this.usedPulses());
            TimestepGroups.AddRange(this.usedTsGroups());
        }

        #endregion

        public string insertSequence(SequenceData insertMe, TimeStep insertAfter)
        {
            int index = -1;
            for (int i=0; i<TimeSteps.Count; i++) { 
                if (insertAfter == TimeSteps[i]) {
                    index = i;
                    break;
                }
            }

            return insertSequence(insertMe, index + 1);
        }

        public string insertSequence(SequenceData insertMe, int insertIndex)
        {
            bool specialUsed = false;
            Dictionary<Variable, string> temp = insertMe.usedVariables();
            foreach (Variable var in temp.Keys)
            {
                if (var.IsSpecialVariable)
                    specialUsed = true;
            }

            commonWaveforms.AddRange(insertMe.commonWaveforms);
            analogGroups.AddRange(insertMe.analogGroups);
            rs232Groups.AddRange(insertMe.rs232Groups);
            gpibGroups.AddRange(insertMe.gpibGroups);
            variables.AddRange(insertMe.variables);
            DigitalPulses.AddRange(insertMe.DigitalPulses);
            TimestepGroups.AddRange(insertMe.TimestepGroups);

            this.TimeSteps.InsertRange(insertIndex, insertMe.TimeSteps);


            this.cleanupSpecialVariables();

            if (specialUsed)
            {
                return "Warning: the inserted sequence made use of a \"Special Variable\" (iterCount, iterNum, etc.). The special variables used in the inserted sequence are now likely to be bound incorrectly. Please check the variables used in the inserted part of the sequence.";
            }
            else
            {
                return null;
            }
        }

        public TimeStep getTimeStepAtTime(double time)
        {
            if (time < 0) return null;

            foreach (TimeStep step in TimeSteps)
            {
                if (step != null)
                {
                    if (step.StepEnabled)
                    {
                        time -= step.StepDuration.getBaseValue();
                    }
                }
                if (time <= 0)
                    return step;
            }
            return null;
        }

        public double getTimeAtTimestep(TimeStep step)
        {
            if (!TimeSteps.Contains(step))
                return 0;
            double ans = 0;
            foreach (TimeStep st in TimeSteps)
            {
                if (st == step)
                    return ans;
                if (st.StepEnabled)
                {
                    ans += st.StepDuration.getBaseValue();
                }
            }
            return ans;
        }

        /// <summary>
        /// Gets the Nth timestep which is not hidden
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        public TimeStep getNthDisplayedTimeStep(int N)
        {
            if (N >= getNDisplayedTimeSteps())
                return null;
            if (N < 0)
                return null;

            return TimeSteps[getNthDisplayedTimeStepID(N)];
        }

        /// <summary>
        /// Gets the id of the Nth timestep which is not hidden
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        public int getNthDisplayedTimeStepID(int N)
        {
            if (stepHidingEnabled)
            {
                int j = 0;
                for (int i = 0; i < TimeSteps.Count; i++)
                {
                    // Count only non hidden timesteps. Once you've counted off enough, return the current timestep;
                    if (!TimeSteps[i].StepHidden)
                    {
                        if (j == N)
                            return i;
                        j++;
                    }
                }
            }
            else if (TimeSteps.Count > N)
                return N;

            throw new Exception("Cannot get the Nth displayed timestep ID, there are less than N timesteps.");
        }


        /// <summary>
        /// gets the number of displayed timesteps
        /// </summary>
        /// <returns></returns>
        public int getNDisplayedTimeSteps()
        {
            int ans = 0;
            foreach (TimeStep st in TimeSteps)
            {
                if ((!stepHidingEnabled) || !st.StepHidden)
                {
                    ans++;
                }
            }
            return ans;
        }

        public void getTimestepInterpolation(int timeStep, int analogID, double[] buf, int nPoints, int offset)
        {
            if (TimeSteps.Count <= timeStep)
                return;

            double lagTime = 0;
            int groupTimeStep = timeStep;
            while (groupTimeStep >= 0)
            {
                if (TimeSteps[groupTimeStep].StepEnabled)
                {
                    if (TimeSteps[groupTimeStep].AnalogGroup != null)
                    {
                        if (TimeSteps[groupTimeStep].AnalogGroup.channelEnabled(analogID))
                            break;
                    }
                }


                groupTimeStep--;

                if (groupTimeStep == -1)
                    return;
            }

            // calculate lag time
            for (int i = groupTimeStep; i < timeStep; i++)
            {
                if (TimeSteps[i].StepEnabled)
                    lagTime += TimeSteps[i].StepDuration.getBaseValue();
            }

            TimeSteps[groupTimeStep].AnalogGroup.ChannelDatas[analogID].waveform.getInterpolation(nPoints, lagTime, lagTime + TimeSteps[timeStep].StepDuration.getBaseValue(), buf, offset, Variables, CommonWaveforms);
        }

        public double getAnalogValueAtEndOfTimestep(int timeStep, int analogID, List<Variable> existingVariables)
        {
            double lagTime = 0;
            Waveform wf = getChannelWaveformAtTimestep(analogID, timeStep, ref lagTime);
            if (wf == null)
                return 0;
            return wf.getValueAtTime(lagTime + TimeSteps[timeStep].StepDuration.getBaseValue(), existingVariables, CommonWaveforms);
        }

        /// <summary>
        /// Returns the waveform for analog channel given by analogID and timestep given by timestep. Also, puts the amount
        /// of elapsed time from the beginning of the waveform to the beginning of timestep into lagtime.
        /// </summary>
        /// <param name="analogID"></param>
        /// <param name="timeStep"></param>
        /// <param name="lagTime"></param>
        /// <returns></returns>
        public Waveform getChannelWaveformAtTimestep(int analogID, int timeStep, ref double lagTime) {
            
            while (timeStep >= 0)
            {
                if (TimeSteps[timeStep].StepEnabled)
                {
                    if (TimeSteps[timeStep].AnalogGroup != null)
                    {
                        if (TimeSteps[timeStep].AnalogGroup.channelEnabled(analogID))
                        {
                            return TimeSteps[timeStep].AnalogGroup.ChannelDatas[analogID].waveform;
                        }
                    }
                }

                timeStep--;
                if (timeStep >= 0)
                {
                    if (TimeSteps[timeStep].StepEnabled)
                        lagTime += TimeSteps[timeStep].StepDuration.getBaseValue();
                }
            }
            return null;
        }

        public SingleOutputFrame getSingleOutputFrameAtEndOfTimestep(int timeStep, SettingsData settings, bool outputAnalogDwellValues)
        {
            if (TimeSteps == null)
                return null;
            if (timeStep>=TimeSteps.Count)
                return null;

            TimeStep step = TimeSteps[timeStep];
            int analogStep;
            if (outputAnalogDwellValues)
            {
                analogStep = TimeSteps.IndexOf(dwellWord());
            }
            else
            {
                analogStep = timeStep;
            }

            SingleOutputFrame ans = new SingleOutputFrame();

            foreach (int analogID in settings.logicalChannelManager.Analogs.Keys)
            {
                if (settings.logicalChannelManager.Analogs[analogID].TogglingChannel)
                {
                    ans.analogValues.Add(analogID, 0);
                }
                else if (settings.logicalChannelManager.Analogs[analogID].overridden)
                {
                    ans.analogValues.Add(analogID, settings.logicalChannelManager.Analogs[analogID].analogOverrideValue);
                }
                else
                {
                    if (settings.logicalChannelManager.Analogs[analogID].AnalogChannelOutputNowUsesDwellWord)
                    {
                        ans.analogValues.Add(analogID, getAnalogValueAtEndOfTimestep(TimeSteps.IndexOf(dwellWord()), analogID, Variables));
                    }
                    else
                    {
                        ans.analogValues.Add(analogID, getAnalogValueAtEndOfTimestep(analogStep, analogID, Variables));
                    }
                }
            }

            foreach (int digitalID in settings.logicalChannelManager.Digitals.Keys)
            {
                bool val = false;
                if (settings.logicalChannelManager.Digitals[digitalID].TogglingChannel) {
                    val = false;
                }
                else if (settings.logicalChannelManager.Digitals[digitalID].overridden)
                {
                    val = settings.logicalChannelManager.Digitals[digitalID].digitalOverrideValue;
                }
                else
                {
                    if (TimeSteps[timeStep].DigitalData.ContainsKey(digitalID))
                    {
                        DigitalDataPoint dp = TimeSteps[timeStep].DigitalData[digitalID];
                        if (!dp.DigitalContinue)
                        {
                            val = TimeSteps[timeStep].DigitalData[digitalID].getValue();
                        }
                        else  // this digital value is a "continue" value, so, we have to go backwards in time until we find 
                        // what value to continue from
                        {
                            int checkStep = timeStep - 1;
                            val = false; // if we hunt all the way to the first timestep with no answer, the default answer is false
                            while (checkStep >= 0)
                            {
                                if (TimeSteps[checkStep].StepEnabled)
                                {
                                    if (TimeSteps[checkStep].DigitalData.ContainsKey(digitalID))
                                    {
                                        if (TimeSteps[checkStep].DigitalData[digitalID].DigitalContinue)
                                        {
                                            checkStep--; // timestep had another continue keep hunting backwards...
                                        }
                                        else
                                        {
                                            val = TimeSteps[checkStep].DigitalData[digitalID].getValue();
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    checkStep--; // timestep was disabled, keep looking
                                }
                            }
                        }

                    }
                }
                ans.digitalValues.Add(digitalID, val);
            }

            return ans;

        }


        public void populateWithChannels(SettingsData settings)
        {
            foreach (TimeStep step in TimeSteps)
            {
                // Add digital datapoints to each timestep.
                foreach (int digitalID in settings.logicalChannelManager.ChannelCollections[HardwareChannel.HardwareConstants.ChannelTypes.digital].getSortedChannelIDList())
                {
                    if (!step.DigitalData.ContainsKey(digitalID))
                    {
                        step.DigitalData.Add(digitalID, new DigitalDataPoint());
                    }
                }
            }

            foreach (AnalogGroup ag in AnalogGroups)
            {
                foreach (int analogID in settings.logicalChannelManager.ChannelCollections[HardwareChannel.HardwareConstants.ChannelTypes.analog].getSortedChannelIDList())
                {
                    if (!ag.containsChannelID(analogID))
                    {
                        ag.addChannel(analogID);
                    }
                }
            }
        }


        #region Calculations

        public int nSamples(double timeStepSize)
        {
            double remainderTime=0;
            return 1 + this.nSamplesBetweenTimeSteps(0, TimeSteps.Count, ref remainderTime, timeStepSize);
        }

        public double[] computeAnalogBuffer(int analogChannelID, double timeStepSize)
        {
            int nSamples = this.nSamples(timeStepSize);

            double[] ans = new double[nSamples];

            this.computeAnalogBuffer(analogChannelID, timeStepSize, ans);
            return ans;
        }

       

        /// <summary>
        /// This function should hopefully handle timing intelligently now, keeping track of "remainder time" when there are timesteps with 
        /// durations that are not an integer multiple of the timestepsize.
        /// </summary>
        /// <param name="analogChannelID"></param>
        /// <param name="timeStepSize"></param>
        /// <returns></returns>
        public void computeAnalogBuffer(int analogChannelID, double timeStepSize, double [] ans)
        {
            int nSamples = this.nSamples(timeStepSize);

        
            ans[0] = dwellWord().getEndAnalogValue(analogChannelID, Variables, CommonWaveforms);


            // forward fast to first timestep which actually uses this channel
            int currentStep = 0;
            int currentSample = 0;

            double remainderTime = 0;


            currentStep = nextTimestepWithAnalogChannelEnabled(currentStep, analogChannelID);

            int nDwellSamples = nSamplesBetweenTimeSteps(0, currentStep, ref remainderTime, timeStepSize);

            // fill in the dwell time
            fillArrayRange(ans[0], ans, currentSample, nDwellSamples);

            currentSample += nDwellSamples;


            // now the timestep of the currentStep should be one that references analogChannelID


            while (true)
            {
                if (currentStep == TimeSteps.Count)
                    break;

                int endStep = nextTimestepWithAnalogChannelEnabled(currentStep+1, analogChannelID);

                double initialRemainderTime = remainderTime;
                int nStepSamples = nSamplesBetweenTimeSteps(currentStep, endStep, ref remainderTime, timeStepSize);

                /*Commented out Nov 28 2007. May be responsible for loss of sync between analog and digital data.
                 * 
                 * double interpolationStartTime = initialRemainderTime;
                double interpolationEndTime = nStepSamples * timeStepSize + remainderTime;
                */

                double interpolationStartTime = 0;
                double interpolationEndTime = nStepSamples * timeStepSize;


                Waveform theWaveform = TimeSteps[currentStep].getChannelWaveform(analogChannelID);

                theWaveform.getInterpolation(nStepSamples,
                    interpolationStartTime, interpolationEndTime, ans, currentSample, Variables, CommonWaveforms);
                
                currentStep = endStep;
                currentSample += nStepSamples;
            }

            if (currentSample != nSamples - 2)
                ans[nSamples - 2] = ans[nSamples - 3];

            ans[nSamples - 1] = ans[0];
            return;
        }

        /// <summary>
        /// Returns the step ID of the next timestep (after or including currentStep) 
        /// that has analog channel analogID enabled. Returns TimeSteps.Count if there is no 
        /// subsequent timestep with the channel enabled.
        /// </summary>
        /// <param name="currentStep"></param>
        /// <param name="analogID"></param>
        /// <returns></returns>
        public int nextTimestepWithAnalogChannelEnabled(int currentStep, int analogID)
        {
            while (true)
            {
                if (currentStep == TimeSteps.Count)
                    return TimeSteps.Count;

                if (TimeSteps[currentStep].StepEnabled)
                {
                    if (TimeSteps[currentStep].AnalogGroup != null)
                    {
                        if (TimeSteps[currentStep].AnalogGroup.channelEnabled(analogID))
                        {
                            return currentStep;
                        }
                    }
                }

                currentStep++;

            }
        }

        /// <summary>
        /// fills the specified double array with nEntries copies of value, starting at startIndex.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="array"></param>
        /// <param name="startIndex"></param>
        /// <param name="nEntries"></param>
        public static void fillArrayRange(double value, double[] array, int startIndex, int nEntries)
        {
            for (int i = 0; i < nEntries; i++)
            {
                array[i + startIndex] = value;
            }
        }

        /// <summary>
        /// Returns the start time of the specified timestep.
        /// </summary>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public double timeAtTimestep(int stepID)
        {
            double ans = 0;
            for (int i = 0; i < stepID; i++)
            {
                if (TimeSteps.Count <= i)
                    return ans;

                if (TimeSteps[i].StepEnabled)
                {
                    ans += TimeSteps[i].StepDuration.getBaseValue();
                }
            }
            return ans;
        }

        /// <summary>
        /// Returns the number of samples between startID timestep and endID timestep. This is done in a 
        /// maximally accurate counting method, timestep-by-timestep, and taking into account and "remainder time"
        /// (ie samples that straddle the border between timesteps).
        /// </summary>
        /// <param name="startID">ID of the start step</param>
        /// <param name="endID">ID of the end step</param>
        /// <param name="remainderTime">present remainder time. 0 if unspecified. currentSampleTime + remainderTime = currentActualTime</param>
        /// <param name="timeStepSize"></param>
        /// <returns></returns>
        public int nSamplesBetweenTimeSteps(int startID, int endID, ref double remainderTime, double timeStepSize)
        {
            int nSamples = 0;
            for (int i = startID; i < endID; i++)
            {
                if (TimeSteps[i].StepEnabled)
                {
                    int temp=0;
                    computeNSamplesAndRemainderTime(ref temp, ref remainderTime, TimeSteps[i].StepDuration.getBaseValue(), timeStepSize);
                    nSamples += temp;
                }
            }
            return nSamples;
        }

        /// <summary>
        /// Computes the number of samples and the remainder time corresponding to the given duration and timestep size. 
        /// </summary>
        /// <param name="nSteps"></param>
        /// <param name="remainderTime"></param>
        /// <param name="duration"></param>
        /// <param name="timeStepSize"></param>
        public static void computeNSamplesAndRemainderTime(ref int nSteps, ref double remainderTime, double duration, double timeStepSize)
        {
            nSteps = (int) (duration / timeStepSize);
            remainderTime += duration - nSteps * timeStepSize;
            // currentActualTime = currentSampleTime + remainderTime.
            // remainderTime should vary between -timeStepSize/2 and timeStepSize/2
            if (remainderTime >= (timeStepSize/2))
            {
                nSteps++;
                remainderTime -= timeStepSize;
            }
        }

        public bool[] computeDigitalBuffer(int digitalID, double timestepSize)
        {
            bool [] ans = new bool[nSamples(timestepSize)];
            computeDigitalBuffer(digitalID, timestepSize, ans);
            return ans;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="digitalID"></param>
        /// <param name="timestepSize"></param>
        /// <returns></returns>
        public void computeDigitalBuffer(int digitalID, double timestepSize, bool [] ans)
        {
            int nSamples = this.nSamples(timestepSize);


            ans[0] = dwellWord().getDigitalValue(digitalID, TimeSteps, 0);

            int currentSample = 0;
            int currentStep = findNextEnabledTimestep(-1);
            double remainderTime = 0;

            while (true)
            {
                if (currentStep == TimeSteps.Count)
                    break;

                int nStepSamples = 0;
                computeNSamplesAndRemainderTime(ref nStepSamples, ref remainderTime, TimeSteps[currentStep].StepDuration.getBaseValue(), timestepSize);
                bool value = TimeSteps[currentStep].getDigitalValue(digitalID, TimeSteps, currentStep);
                fillBoolArray(ans, value, currentSample, nStepSamples);
                currentSample += nStepSamples;
                currentStep = findNextEnabledTimestep(currentStep);
            }

            if (currentSample != nSamples - 2)
                ans[nSamples - 2] = ans[nSamples - 3];

            ans[nSamples - 1] = ans[0];

            if (this.digitalChannelUsesPulses(digitalID))
            {
                // Now fill in any digital pulses which may be on this channel...
                currentSample = 0;
                remainderTime = 0;
                foreach (TimeStep step in TimeSteps)
                {
                    if (step.StepEnabled)
                    {
                        if (step.DigitalData[digitalID] != null)
                        {
                            if (step.DigitalData[digitalID].usesPulse())
                            {
                                Pulse pulse = step.DigitalData[digitalID].DigitalPulse;

                                Pulse.PulseSampleTimes sampleTimes = pulse.getPulseSampleTimes(remainderTime, timestepSize, step.StepDuration.getBaseValue());

                                int start = currentSample + sampleTimes.startSample;
                                int end = currentSample + sampleTimes.endSample;

                                start = Math.Max(0, start);
                                end = Math.Min(end, ans.Length);
                                for (int i = start; i < end; i++)
                                {
                                    ans[i] = pulse.PulseValue;
                                }
                            }
                        }
                        int nStepSamples = 0;
                        computeNSamplesAndRemainderTime(ref nStepSamples, ref remainderTime, step.StepDuration.getBaseValue(), timestepSize);
                        currentSample += nStepSamples;
                    }

                }
            }
        }

        /// <summary>
        /// Returns true if there is an enabled timestep in which the given channel uses a pulse.
        /// </summary>
        /// <param name="digitalID"></param>
        /// <returns></returns>
        public bool digitalChannelUsesPulses(int digitalID)
        {
            foreach (TimeStep step in TimeSteps)
            {
                if (step.DigitalData.ContainsKey(digitalID))
                {
                    if (step.DigitalData[digitalID].usesPulse())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Computes 
        /// </summary>
        /// <param name="digitalID"></param>
        /// <param name="masterTimebaseSampleDuration"></param>
        /// <param name="ans"></param>
        /// <param name="timebaseSegments"></param>
        public void computeDigitalBuffer(int digitalID, double masterTimebaseSampleDuration, bool[] ans, TimestepTimebaseSegmentCollection timebaseSegments)
        {

            int currentSample = 0;

            for (int stepID = 0; stepID < TimeSteps.Count; stepID++)
            {
                if (TimeSteps[stepID].StepEnabled)
                {
                    TimeStep currentStep = TimeSteps[stepID];
                    bool channelValue = TimeSteps[stepID].getDigitalValue(digitalID, TimeSteps, stepID);
                    int nSegmentSamples = 0;
                    if (!timebaseSegments.ContainsKey(currentStep))
                        throw new Exception("No timebase segment for timestep " + currentStep.ToString());

                    nSegmentSamples = timebaseSegments.nSegmentSamples(currentStep);


                    for (int j = 0; j < nSegmentSamples; j++)
                    {
                        ans[j + currentSample] = channelValue;
                    }

                    currentSample += nSegmentSamples;
                }
            }

            ans[currentSample] = dwellWord().getDigitalValue(digitalID, TimeSteps, 0);

            if (this.digitalChannelUsesPulses(digitalID))
            {

                int currentMasterSample = 0;
                // now fill in any pulses which act on this channel...
                foreach (TimeStep step in TimeSteps)
                {
                    if (step.StepEnabled)
                    {

                        int nMasterSamplesInTimestep = timebaseSegments.nMasterSamples(step);

                        if (step.DigitalData.ContainsKey(digitalID))
                        {
                            if (step.DigitalData[digitalID].usesPulse())
                            {
                                Pulse pulse = step.DigitalData[digitalID].DigitalPulse;
                                Pulse.PulseSampleTimes sampleTimes = pulse.getPulseSampleTimes(nMasterSamplesInTimestep, masterTimebaseSampleDuration);

                                int start = currentMasterSample + sampleTimes.startSample;
                                int end = currentMasterSample + sampleTimes.endSample;

                                // ok. Paint the pulse...
                                // to do this, we need to find which derived sample (ie sample in this buffer) corresponds to 
                                // which master timestep.

                                int derivedStart = getDerivedSampleFromMasterSample(start, timebaseSegments);
                                int derivedEnd = getDerivedSampleFromMasterSample(end, timebaseSegments);

                                derivedStart = Math.Max(0, derivedStart);
                                derivedEnd = Math.Min(derivedEnd, ans.Length);

                                for (int i = derivedStart; i < derivedEnd; i++)
                                {
                                    ans[i] = pulse.PulseValue;
                                }
                            }
                        }
                        currentMasterSample += nMasterSamplesInTimestep;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the sample id of the first derived sample which is on or after the given master sample, given a set of variable timebase segments.
        /// </summary>
        /// <param name="masterSample"></param>
        /// <param name="timebaseSegments"></param>
        /// <returns></returns>
        public int getDerivedSampleFromMasterSample(int masterSample, TimestepTimebaseSegmentCollection timebaseSegments)
        {
            int currentDerivedSample = 0;
            int currentMasterSample=0;
            if (masterSample==0)
                return 0;

            foreach (TimeStep step in TimeSteps)
            {
                if (step.StepEnabled)
                {
                    foreach (VariableTimebaseSegment segment in timebaseSegments[step])
                    {
                        for (int i = 0; i < segment.NSegmentSamples; i++)
                        {
                            currentMasterSample += segment.MasterSamplesPerSegmentSample;
                            currentDerivedSample++;
                            if (currentMasterSample >= masterSample)
                                return currentDerivedSample;
                        }
                    }
                }
            }
            return currentDerivedSample;
        }


        /// <summary>
        /// Gets the master timebase sample from a given derived timebase sample.
        /// </summary>
        /// <param name="derivedSample"></param>
        /// <param name="timebaseSegments"></param>
        /// <returns></returns>
        public int getMasterSampleFromDerivedSample(int derivedSample, TimestepTimebaseSegmentCollection timebaseSegments)
        {
            int currentDerivedSample = 0;
            int currentMasterSample = 0;
            if (derivedSample == 0)
                return 0;
            foreach (TimeStep step in TimeSteps)
            {
                if (step.StepEnabled)
                {
                    foreach (VariableTimebaseSegment segment in timebaseSegments[step])
                    {
                        for (int i = 0; i < segment.NSegmentSamples; i++)
                        {
                            currentMasterSample += segment.MasterSamplesPerSegmentSample;
                            currentDerivedSample++;
                            if (currentDerivedSample >= derivedSample)
                                return currentMasterSample;
                        }
                    }
                }
            }
            return currentMasterSample;
        }

        public void computeAnalogBuffer(int analogChannelID, double masterTimebaseSampleDuration, double[] ans, TimestepTimebaseSegmentCollection timebaseSegments)
        {
            int currentSample = 0;
            double dwellingValue = dwellWord().getEndAnalogValue(analogChannelID, Variables, CommonWaveforms);
            AnalogGroup currentlyRunningGroup = null;
            double groupRunningTime = 0;


            for (int stepID = 0; stepID < TimeSteps.Count; stepID++)
            {
                TimeStep currentStep = TimeSteps[stepID];
                if (currentStep.StepEnabled)
                {
                    if (currentStep.AnalogGroup != null)
                    {
                        if (currentStep.AnalogGroup.channelEnabled(analogChannelID))
                        {
                            currentlyRunningGroup = currentStep.AnalogGroup;
                            groupRunningTime = 0;
                        }
                    }


                    if (currentlyRunningGroup == null)
                    {
                        int nSegmentSamples = timebaseSegments.nSegmentSamples(currentStep);



                        for (int j = 0; j < nSegmentSamples; j++)
                        {
                            ans[j + currentSample] = dwellingValue;
                        }
                        currentSample += nSegmentSamples;
                    }
                    else
                    {
                        Waveform runningWaveform = currentlyRunningGroup.ChannelDatas[analogChannelID].waveform;
                        double waveformRunningTime = groupRunningTime;
                        foreach (VariableTimebaseSegment segment in timebaseSegments[currentStep])
                        {
                            
                            runningWaveform.getInterpolation(segment.NSegmentSamples, waveformRunningTime,
                                waveformRunningTime + segment.NSegmentSamples * segment.MasterSamplesPerSegmentSample * masterTimebaseSampleDuration,
                                ans,
                                currentSample, Variables, CommonWaveforms);
                            currentSample += segment.NSegmentSamples;
                            waveformRunningTime += segment.NSegmentSamples * segment.MasterSamplesPerSegmentSample * masterTimebaseSampleDuration;
                        }

                        groupRunningTime += currentStep.StepDuration.getBaseValue();
                        if (runningWaveform.getEffectiveWaveformDuration() <= groupRunningTime)
                        {
                            currentlyRunningGroup = null;
                        }

                        dwellingValue = runningWaveform.getValueAtTime(runningWaveform.WaveformDuration.getBaseValue(),
                            Variables, CommonWaveforms);
                    }
                }
            }

            ans[currentSample] = dwellWord().getEndAnalogValue(analogChannelID, Variables, CommonWaveforms);
        }

        public static void fillBoolArray(bool[] array, bool value, int startIndex, int nEntries)
        {
            for (int i = 0; i < nEntries; i++)
            {
                array[i + startIndex] = value;
            }
        }

        /// <summary>
        /// returns the index of the next timestep (after the one indicated by startIndex) which
        /// is both enabled and which is not "continue" with respect to analog channel id specified.
        /// Returns step count if there is no next enabled step, or if the input data is somehow invalid.
        /// </summary>
        /// <param name="timeSteps"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int findNextAnalogChannelEnabledTimestep(List<TimeStep> timeSteps, int startIndex, int analogID)
        {
            for (int i = startIndex+1; i < timeSteps.Count; i++)
            {
                if (timeSteps[i] != null)
                {
                    if (timeSteps[i].StepEnabled)
                    {
                        if (timeSteps[i].AnalogGroup != null)
                        {
                            if (timeSteps[i].AnalogGroup.channelEnabled(analogID))
                                return i;
                        }
                    }
                }
            }
            return timeSteps.Count;
        }

        public int findNextAnalogChannelEnabledTimestep(int startIndex, int analogID)
        {
            return SequenceData.findNextAnalogChannelEnabledTimestep(this.TimeSteps, startIndex, analogID);
        }

        /// <summary>
        /// returns the index of the next timestep (after the one indicated by startIndex) which
        /// is both enabled and which is not "continue" with respect to gpib channel id specified.
        /// Returns step count if there is no next enabled step, or if the input data is somehow invalid.
        /// </summary>
        /// <param name="timeSteps"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int findNextGpibChannelEnabledTimestep(List<TimeStep> timeSteps, int startIndex, int gpibID)
        {
            for (int i = startIndex + 1; i < timeSteps.Count; i++)
            {
                if (timeSteps[i]!=null) {
                    if (timeSteps[i].StepEnabled) {
                        if (timeSteps[i].GpibGroup!=null) {
                            if (timeSteps[i].GpibGroup.channelEnabled(gpibID))
                            {
                                return i;
                            }
                        }
                    }
                }
            }
            return timeSteps.Count;
        }

        /// <summary>
        /// Returns the ID of the next enabled timestep after startIndex (not including startIndex).
        /// Returns TimeSteps.Count if there are no further enabled timesteps.
        /// </summary>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public int findNextEnabledTimestep(int startIndex)
        {
            for (int i = startIndex+1; i < TimeSteps.Count; i++)
            {
                if (TimeSteps[i].StepEnabled)
                    return i;
            }
            return TimeSteps.Count;
        }

        public int findNextGpibChannelEnabledTimestep(int startIndex, int gpibID)
        {
            return SequenceData.findNextGpibChannelEnabledTimestep(this.TimeSteps, startIndex, gpibID);
        }

        public int findNextRS232ChannelEnabledTimestep(int startIndex, int rs232ID)
        {
            for (int i = startIndex + 1; i < TimeSteps.Count; i++)
            {
                if (TimeSteps[i] != null)
                {
                    if (TimeSteps[i].StepEnabled)
                    {
                        if (TimeSteps[i].rs232Group!=null)
                        {
                            if (TimeSteps[i].rs232Group.channelEnabled(rs232ID))
                            {
                                return i;
                            }
                        }
                    }
                }
            }
            return TimeSteps.Count;
        }

        /// <summary>
        /// Returns the total duration of all the enabled timesteps between start and end. Including start, but not including end.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public double timeBetweenSteps(int start, int end)
        {
            double ans = 0;
            for (int i = start; i < end; i++)
            {
                if (i >= 0 && i < TimeSteps.Count)
                {
                    if (TimeSteps[i] != null)
                    {
                        if (TimeSteps[i].StepEnabled)
                            ans += TimeSteps[i].StepDuration.getBaseValue();
                    }
                }
            }
            return ans;
        }


        

    
        /// <summary>
        /// enumeration of the various variable timebase types supported. 
        /// </summary>
        public enum VariableTimebaseTypes
        {
            /// <summary>
            /// One digital output is to be used as a clock for all of the other analog and digital outputs. 
            /// The first sample of the clock buffer will be a false. Clock edges will be positive edges.
            /// There will be a clock edge at the beginning of each sequence timestep.
            /// In addition, if there are any running analog groups at the start of the timestep, then the following
            /// algorithm will be used to create additional clock edges during that timestep:
            ///  - The analog groups that are running during the beginning of that timestep are determined, as are
            /// their effective durations and the time elapsed since they started.
            ///  - Based on this, the timestep is further subdivided into VariableTimebaseSegments, until all of the
            /// analog groups running during that timestep are satisfied. Subdivision is only necessary if, for example,
            /// a high resolution runs for only half of the timestep, and then gives way to a lower resolution group.
            ///  - If there are no analog groups running during the beginning of that timestep, then still a single variabletimebase segment is created
            /// which just contains one clock edge.
            ///  - one additional clock pulse should be placed after the end of the last pulse, to trigger the return to the dwell word. This is not included
            /// in the list of variable timebase segments. 
            /// </summary>
            AnalogGroupControlledVariableFrequencyClock
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timebaseType"></param>
        /// <param name="masterTimebaseSampleDuration"></param>
        /// <returns></returns>
        public TimestepTimebaseSegmentCollection generateVariableTimebaseSegments(VariableTimebaseTypes timebaseType,  double masterTimebaseSampleDuration)
        {
            switch (timebaseType)
            {
                case VariableTimebaseTypes.AnalogGroupControlledVariableFrequencyClock:
                    {

                        TimestepTimebaseSegmentCollection ans = new TimestepTimebaseSegmentCollection();

                        Dictionary<TimeStep, List<DigitalImpingement>> digitalImpingements = getDigitalImpingements(masterTimebaseSampleDuration);

                        for (int stepID = 0; stepID < TimeSteps.Count; stepID++)
                        {
                            if (TimeSteps[stepID].StepEnabled)
                            {
                                TimeStep currentStep = TimeSteps[stepID];

                                VariableTimebaseSegmentCollection timestepSegments = new VariableTimebaseSegmentCollection();
                                Dictionary<AnalogGroup, double> runningGroups = getRunningGroupRemainingTime(stepID);

                                // first cull groups that have less remaining time than 2 master timbase cycles.
                                {
                                    List<AnalogGroup> groups = new List<AnalogGroup>(runningGroups.Keys);
                                    foreach (AnalogGroup ag in groups)
                                    {
                                        if (runningGroups[ag] < 2 * masterTimebaseSampleDuration) 
                                        {
                                            runningGroups.Remove(ag);
                                        }
                                    }
                                }

                                if (runningGroups.Count == 0)
                                {
                                    
                                    timestepSegments.Add(new VariableTimebaseSegment(1, (int)(currentStep.StepDuration.getBaseValue() / masterTimebaseSampleDuration)));
                                }
                                else
                                {
                                    double timeIntoCurrentStep = 0;

                                    while (runningGroups.Count != 0 && timeIntoCurrentStep < currentStep.StepDuration.getBaseValue())
                                    {
                                        // determine the most sensitive running group
                                        AnalogGroup smallestResolutionGroup = null;
                                        double smallestResolution = Double.MaxValue;
                                        double durationTiebreaker = Double.MinValue;
                                        foreach (AnalogGroup ag in runningGroups.Keys)
                                        {
                                            if (ag.TimeResolution.getBaseValue() < smallestResolution)
                                            {
                                                durationTiebreaker = runningGroups[ag];
                                                smallestResolution = ag.TimeResolution.getBaseValue();
                                                smallestResolutionGroup = ag;
                                            }
                                            else if (ag.TimeResolution.getBaseValue() == smallestResolution)
                                            {
                                                if (runningGroups[ag] >= durationTiebreaker)
                                                {
                                                    durationTiebreaker = runningGroups[ag];
                                                    smallestResolution = ag.TimeResolution.getBaseValue();
                                                    smallestResolutionGroup = ag;
                                                }
                                            }
                                        }

                                        double timeToRunGroup = Math.Min(currentStep.StepDuration.getBaseValue() - timeIntoCurrentStep, runningGroups[smallestResolutionGroup]);

                                        int masterTimebaseSamplesPerSegmentSample = 0;

                                        if (smallestResolutionGroup.TimeResolution.getBaseValue() != 0)
                                        {
                                            masterTimebaseSamplesPerSegmentSample = (int)(0.5 + smallestResolutionGroup.TimeResolution.getBaseValue() / masterTimebaseSampleDuration);
                                        }

                                        if (masterTimebaseSamplesPerSegmentSample < 2)
                                        {
                                            masterTimebaseSamplesPerSegmentSample = 2;
                                        }

                                        int nSegmentSamples = (int)(timeToRunGroup / ((double)masterTimebaseSamplesPerSegmentSample * masterTimebaseSampleDuration));

                               //         if (nSegmentSamples > 0)
                               //         {

                                            double actualGroupRunTime = nSegmentSamples * masterTimebaseSampleDuration * masterTimebaseSamplesPerSegmentSample;
                                            if (nSegmentSamples != 0)
                                            {
                                                timestepSegments.Add(new VariableTimebaseSegment(nSegmentSamples, masterTimebaseSamplesPerSegmentSample));
                                            }
                                            else
                                            {
                                            }
                                            runningGroups.Remove(smallestResolutionGroup);
                                            List<AnalogGroup> groups = new List<AnalogGroup>(runningGroups.Keys);
                                            foreach (AnalogGroup ag in groups)
                                            {
                                                runningGroups[ag] -= actualGroupRunTime;
                                                if (runningGroups[ag] <= 2 * masterTimebaseSampleDuration)
                                                    runningGroups.Remove(ag);

                                            }


                                            timeIntoCurrentStep += actualGroupRunTime;
                               //         }
                               //         else
                               //         {
                               //             timeIntoCurrentStep = currentStep.StepDuration.getBaseValue();
                               //         }
                                    }

                                    // if the present collection of segments does not get us to the end of the timestep, add
                                    // one filler segment.
                                    if (currentStep.StepDuration.getBaseValue() - timeIntoCurrentStep >= 2 * masterTimebaseSampleDuration)
                                    {
                                        timestepSegments.Add(new VariableTimebaseSegment(1, (int)(0.5 + (currentStep.StepDuration.getBaseValue() - timeIntoCurrentStep) / masterTimebaseSampleDuration)));
                                    }
                                }


                                // ok, now, for the love of god, we have to take into account digital pulse impingements, which may
                                // require the creation of yet more segments (by splitting those segments which already exist...)

                                #region Splitting segments due to digital pulse impingements. This is messy ugly business.
                                if (digitalImpingements.ContainsKey(currentStep))
                                {
                                    List<DigitalImpingement> impigs = digitalImpingements[currentStep];
                                    foreach (DigitalImpingement impig in impigs)
                                    {
                                        int samplesTillImpig = impig.nSamplesFromTimestepStart;
                                        VariableTimebaseSegment segmentToSplit = null;
                                        foreach (VariableTimebaseSegment seg in timestepSegments)
                                        {
                                            if (samplesTillImpig <= 0) break;
                                            if (samplesTillImpig < seg.NSegmentSamples * seg.MasterSamplesPerSegmentSample)
                                            {
                                                segmentToSplit = seg;
                                                break;
                                            }
                                            samplesTillImpig -= seg.NSegmentSamples * seg.MasterSamplesPerSegmentSample;
                                        }

                                        // if necessary, now we split the current segment (segmentToSplit)... argh.
                                        if (samplesTillImpig > 0)
                                        {
                                            // we now need to split the segment
                                            // this may take as many as 4 new segments to accomplish
                                            List<VariableTimebaseSegment> newSegments = new List<VariableTimebaseSegment>();
                                            int newSegmentSamples = 0;

                                            // Lead in segment. This runs at the original segment's usual clock rate, and runs until right before temp
                                            int nLeadSegmentSamples = samplesTillImpig / segmentToSplit.MasterSamplesPerSegmentSample;
                                            if (nLeadSegmentSamples != 0)
                                            {
                                                VariableTimebaseSegment newSeg = new VariableTimebaseSegment(nLeadSegmentSamples, segmentToSplit.MasterSamplesPerSegmentSample);
                                                newSegments.Add(newSeg);
                                                newSegmentSamples += newSeg.NSegmentSamples * newSeg.MasterSamplesPerSegmentSample;
                                                samplesTillImpig -= newSeg.MasterSamplesPerSegmentSample * newSeg.NSegmentSamples;
                                            }


                                            // Now, if necessary, add small jog in and jog out segment
                                            int jogIn = samplesTillImpig;
                                            int jogOut = segmentToSplit.MasterSamplesPerSegmentSample - jogIn;
                                            
                                            // have to be carefull. We can't jog in or out by less than 2.
                                            // If both are either greater than 1 or equal to zero, no problem.
                                            // If jogIn is 1, we will make it 2 if we can (ie if jogOut is not 0 or 2).
                                            // If jogIn is 1 and jogOut is 0, we make jogOut and jogIn 0.
                                            // If jogIn is 1 and jogOut is 2, we make jogin 3 and jogOut 0.
                                            // If jogOut is 1, then we will make it 0 if we can.

                                            if (jogIn == 1)
                                            {
                                                if (jogOut != 0 && jogOut != 2)
                                                {
                                                    jogIn++;
                                                    jogOut--;
                                                }
                                                else
                                                {
                                                    if (jogOut == 0)
                                                    {
                                                        jogIn = 0;
                                                    }
                                                    if (jogOut == 2)
                                                    {
                                                        jogOut = 0;
                                                        jogIn = 3;
                                                    }
                                                }
                                            }

                                            if (jogOut == 1)
                                            {
                                                if (jogIn > 2)
                                                {
                                                    jogOut--;
                                                    jogIn++;
                                                }
                                                else
                                                {
                                                    jogOut = 0;
                                                }
                                            }

                                            if (jogIn != 0)
                                            {
                                                VariableTimebaseSegment newSeg = new VariableTimebaseSegment(1, jogIn);
                                                newSegments.Add(newSeg);
                                                newSegmentSamples += jogIn;
                                            }
                                            if (jogOut != 0)
                                            {
                                                VariableTimebaseSegment newSeg = new VariableTimebaseSegment(1, jogOut);
                                                newSegments.Add(newSeg);
                                                newSegmentSamples += jogOut;
                                            }

                                            // now add lead out.
                                            int nSamplesToReplace = segmentToSplit.MasterSamplesPerSegmentSample * segmentToSplit.NSegmentSamples - newSegmentSamples;
                                            int nEndSegments = nSamplesToReplace / segmentToSplit.MasterSamplesPerSegmentSample;
                                            if (nEndSegments * segmentToSplit.MasterSamplesPerSegmentSample != nSamplesToReplace)
                                            {
                                                throw new InvalidDataException("Confusion during splitting of variable timebase segments due to digital pulses. This should not happen.");
                                            }
                                            if (nEndSegments != 0)
                                            {
                                                VariableTimebaseSegment newSeg = new VariableTimebaseSegment(nEndSegments, segmentToSplit.MasterSamplesPerSegmentSample);
                                                newSegments.Add(newSeg);
                                            }

                                            // ok. We have constructed the replacement segments. Now lets do the swap.
                                            int originalIndex = timestepSegments.IndexOf(segmentToSplit);
                                            timestepSegments.RemoveAt(originalIndex);
                                            timestepSegments.InsertRange(originalIndex, newSegments);

                                        }
                                    }
                                }

                                #endregion

                                // clean up -- if there any segments of length <=0, remove them
                                List<VariableTimebaseSegment> segmentsToRemove = new List<VariableTimebaseSegment>();
                                foreach (VariableTimebaseSegment seg in timestepSegments)
                                {
                                    if (seg.MasterSamplesPerSegmentSample <= 0 || seg.NSegmentSamples <= 0)
                                        segmentsToRemove.Add(seg);
                                }
                                foreach (VariableTimebaseSegment seg in segmentsToRemove)
                                {
                                    timestepSegments.Remove(seg);
                                }
                                

                                ans.Add(currentStep, timestepSegments);
                            }
                        }
                        return ans;
                    }
                default:
                    throw new Exception("Unsupported variable timebase type.");
            }
        }


        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class VariableTimebaseSegment
        {
            /// <summary>
            /// Number of samples is the given segment of the variable timebase.
            /// </summary>
            private int nSegmentSamples;

            public int NSegmentSamples
            {
                get { return nSegmentSamples; }
                set { nSegmentSamples = value; }
            }
            /// <summary>
            /// Number of master timebase samples corresponding to each segment sample. Minimum value is 2.
            /// </summary>
            private int masterSamplesPerSegmentSample;

            public int MasterSamplesPerSegmentSample
            {
                get { return masterSamplesPerSegmentSample; }
                set { masterSamplesPerSegmentSample = value; }
            }

            public VariableTimebaseSegment(int nSegmentSamples, int masterSamplesPerSegmentSample)  {
                this.nSegmentSamples = nSegmentSamples;
                this.masterSamplesPerSegmentSample = masterSamplesPerSegmentSample;
            }

            public VariableTimebaseSegment(int nSegmentSamples)
                : this(nSegmentSamples, 2)
            {
            }

            public override string ToString()
            {
                return NSegmentSamples.ToString() + " X "  + masterSamplesPerSegmentSample.ToString() + " (nseg * mast)";
                
            }
        }

        /// <summary>
        /// Generates the digital buffer for a digital output that will be clocked with the same clock
        /// as the one driving the variable timebase clock. This is for use on digital outputs that
        /// share a clock with the variable timebase clock output, but that we want to use
        /// for sequence data rather than as clocks.
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool [] getDigitalBufferClockSharedWithVariableTimebaseClock(TimestepTimebaseSegmentCollection timebaseSegments, int digitalID, double masterTimestepSize) {
            int nSamples = timebaseSegments.nMasterSamples();

            nSamples += 1 + 2; // 1 extra sample at the beginning for the clock's leading LOW, and 2 at the end for the extgra dwell pulse. (this matches
             // the number of samples in the variable timebase clock, see the following function

            int currentSample = 0;

            if (nSamples % 4 != 0)
                nSamples += (4 - nSamples % 4);

            bool [] ans = new bool[nSamples];

            ans[0] = dwellWord().getDigitalValue(digitalID, TimeSteps, 0);

            currentSample++;
            int stepID = 0;
            foreach (TimeStep step in TimeSteps)
            {
                if (step.StepEnabled)
                {
                    int nStepSamples = timebaseSegments.nMasterSamples(step);

                    // if the digital is true, fill this part of the buffer with trues. If not, 
                    // no need to do anything as the inital value of the array is false.
                    if (step.getDigitalValue(digitalID, TimeSteps, stepID))
                    {
                        for (int j = 0; j < nStepSamples; j++)
                        {
                            ans[j + currentSample] = true;
                        }
                    }
                    currentSample += nStepSamples;
                }
                stepID++;
            }

            // fill the rest of the buffer with dwell values
            if (dwellWord().getDigitalValue(digitalID, TimeSteps, 0))
            {
                for (int j = currentSample; j < nSamples; j++)
                {
                    ans[j] = true;
                }
            }

            // now, search for digital pulses, and if there are any on this channel, paint them on
            if (digitalChannelUsesPulses(digitalID))
            {
                currentSample = 1;
                foreach (TimeStep step in TimeSteps)
                {
                    if (step.StepEnabled)
                    {
                        int nStepSamples = timebaseSegments.nMasterSamples(step);

                        if (step.DigitalData[digitalID].usesPulse())
                        {
                            Pulse pulse = step.DigitalData[digitalID].DigitalPulse;

                            Pulse.PulseSampleTimes sampleTimes = pulse.getPulseSampleTimes(nStepSamples, masterTimestepSize);

                            int start = currentSample + sampleTimes.startSample;
                            int end = currentSample + sampleTimes.endSample;

                            start = Math.Max(0, start);
                            end = Math.Min(end, ans.Length);

                            for (int i = start; i < end; i++)
                            {
                                ans[i] = pulse.PulseValue;
                            }
                        }
                        currentSample += nStepSamples;
                    }
                }
            }

            return ans;
        }

        public bool[] getVariableTimebaseClock(TimestepTimebaseSegmentCollection timebaseSegments)
        {
            int nSamples = timebaseSegments.nMasterSamples();

            nSamples += 1 + 2; // add 1 false sample at the beginning so that we start with a false, and 2 at the end so we can 
             // trigger the dwell values;

            if (nSamples % 4 != 0)
                nSamples += (4 - nSamples % 4); // for daqMx drivers, the number of samples in a digital stream has to be a multiple of 4

            bool[] ans = new bool[nSamples];
            int currentSample = 1;  // start at sample 1, thus leaving sample 0 as false.

            for (int stepID = 0; stepID < TimeSteps.Count; stepID++)
            {
                TimeStep currentStep = TimeSteps[stepID];
                if (currentStep.StepEnabled)
                {
                    List<VariableTimebaseSegment> segments = timebaseSegments[currentStep];
                    foreach (VariableTimebaseSegment seg in segments)
                    {
                        for (int i = 0; i < seg.NSegmentSamples; i++)
                        {
                            ans[currentSample] = true;

                            // make the clock pulse a little bit longer if the clocks are sufficiently far apart
                            // for this segment. This is mainly an aesthetic detail, it makes it easier to 
                            // inspect the variable timebase on a scope, and should 
                            // make no functional different
                            if (seg.MasterSamplesPerSegmentSample > 2)
                            {
                                int repeats = (seg.MasterSamplesPerSegmentSample / 2);
                                for (int j = 0; j < repeats; j++)
                                {
                                    ans[currentSample + j] = true;
                                }
                            }

                            currentSample += seg.MasterSamplesPerSegmentSample;
                        }
                    }
                }
            }

            // Add one last pulse at the end to push into dwell values? I don't remember, this comment
            // it being written long after the code was.
            ans[currentSample] = true;
            return ans;

        }

        /// <summary>
        /// Returns a dictionary giving the running analog groups, as well as the remaining time until their effective duration
        /// is over, as determined at the beginning of the timestep given by timestepID.
        /// </summary>
        /// <param name="timeStepID"></param>
        /// <returns></returns>
        public Dictionary<AnalogGroup, double> getRunningGroupRemainingTime(int timeStepID)
        {
            Dictionary<AnalogGroup, double> ans = getRunningAnalogGroups(timeStepID);
            List<AnalogGroup> groups = new List<AnalogGroup>(ans.Keys);
            foreach (AnalogGroup ag in groups)
            {
                ans[ag] = ag.getEffectiveDuration() - ans[ag];
            }
            return ans;
        }

        /// <summary>
        /// Returns a dictionary containing all of the analog groups that are active at a given timestep, as well as a double
        /// representing how long they have been running.
        /// 
        /// An analog group is considered to be "active" at a certain timestep if:
        /// 1) Its effective duration has not elapsed. AND
        /// 2) It has at least one channel enabled which has not been subsequently interrupted.
        /// </summary>
        /// <param name="timeStepID"></param>
        /// <returns></returns>
        public Dictionary<AnalogGroup, double> getRunningAnalogGroups(int timeStepID)
        {
            Dictionary<AnalogGroup, double> ans = new Dictionary<AnalogGroup, double>();

            Dictionary<int, bool> channelActive = new Dictionary<int, bool>();

            // This function works by checking each of the previous sequence timesteps to see if it started an analog group,
            // and if so whether that group is still running.

            for (int startingID = 0; startingID <= timeStepID; startingID++)
            {
                if (TimeSteps[startingID].StepEnabled)
                {
                    if (TimeSteps[startingID].AnalogGroup != null)
                    {
                        AnalogGroup ag = TimeSteps[startingID].AnalogGroup;

                        bool groupStillRunning = true;

                        double elapsedTime = 0;
                        double effectiveDuration = ag.getEffectiveDuration();
                        channelActive.Clear();
                        foreach (int channelID in ag.ChannelDatas.Keys)
                        {
                            if (ag.channelEnabled(channelID))
                                channelActive.Add(channelID, true);
                        }

                        double previousTimestepDuration = TimeSteps[startingID].StepDuration.getBaseValue();

                        for (int interimStep = startingID + 1; interimStep <= timeStepID; interimStep++)
                        {
                            if (TimeSteps[interimStep].StepEnabled)
                            {
                                elapsedTime += previousTimestepDuration;
                                previousTimestepDuration = TimeSteps[interimStep].StepDuration.getBaseValue();
                                if (elapsedTime > effectiveDuration)
                                {
                                    groupStillRunning = false;
                                    break;
                                }

                                if (TimeSteps[interimStep].AnalogGroup != null)
                                {
                                    AnalogGroup interruptingGroup = TimeSteps[interimStep].AnalogGroup;
                                    foreach (int channelID in interruptingGroup.ChannelDatas.Keys)
                                    {
                                        if (interruptingGroup.channelEnabled(channelID))
                                        {
                                            if (channelActive.ContainsKey(channelID))
                                            {
                                                channelActive[channelID] = false;
                                            }
                                        }
                                    }
                                }

                                if (!channelActive.ContainsValue(true))
                                {
                                    groupStillRunning = false;
                                    break;
                                }

                            }
                        }

                        if (groupStillRunning)
                        {
                            ans.Add(ag, elapsedTime);
                        }
                    }
                }
            }

            return ans;
        }

        #region Code for dealing with digital pulses.

 
        /// <summary>
        /// Used in calculating a variable timebase, in the presenece of digital pulses. An impingement 
        /// is a point in time where a digital value is changing due to a pulse, but that is not either the start
        /// or the end of a timestep. These are points in time which may require the splitting of variable timebase segments
        /// to accomodate the change in output at that time.
        /// </summary>
        private class DigitalImpingement
        {
            public int nSamplesFromTimestepStart;
        }

        private Dictionary<TimeStep, List<DigitalImpingement>> getDigitalImpingements(double timeStepSize)
        {
            int nSamplesSoFar = 0;
            double remainderTime=0;

            Dictionary<TimeStep, List<DigitalImpingement>> ans = new Dictionary<TimeStep,List<DigitalImpingement>>();

            foreach (TimeStep step in TimeSteps)
            {
                if (step.StepEnabled)
                {
                    foreach (int digID in step.DigitalData.Keys)
                    {
                        if (step.DigitalData[digID].usesPulse())
                        {
                            Pulse pulse = step.DigitalData[digID].DigitalPulse;

                            Pulse.PulseSampleTimes sampleTimes = pulse.getPulseSampleTimes(remainderTime, timeStepSize, step.StepDuration.getBaseValue());

                            if (sampleTimes.startRequiresImpingement)
                            {
                                addImpingement(ans, nSamplesSoFar + sampleTimes.startSample, timeStepSize);
                            }

                            if (sampleTimes.endRequiresImpingement)
                            {
                                addImpingement(ans, nSamplesSoFar + sampleTimes.endSample, timeStepSize);
                            }


                            
                        }
                    }
                    int nStepSamples = 0;
                    computeNSamplesAndRemainderTime(ref nStepSamples, ref remainderTime, step.StepDuration.getBaseValue(), timeStepSize);
                    nSamplesSoFar += nStepSamples;
                }
            }

            return ans;
        }

        private void addImpingement(Dictionary<TimeStep, List<DigitalImpingement>> dict, int nSamplesFromStart, double timeStepSize) 
        {
            TimeStep impigStep = getTimestepAtSample(nSamplesFromStart, timeStepSize);
            int impigStepSample = getSamplesAtTimestep(impigStep, timeStepSize);
            DigitalImpingement impig = new DigitalImpingement();
            impig.nSamplesFromTimestepStart = nSamplesFromStart - impigStepSample;

            if (!dict.ContainsKey(impigStep))
                dict.Add(impigStep, new List<DigitalImpingement>());
            dict[impigStep].Add(impig);
        }

        public TimeStep getTimestepAtSample(int nSamples, double timeStepSize) 
        {
            int count = 0;
            double remainderTime = 0;
            foreach (TimeStep step in TimeSteps) {
                if (step.StepEnabled)
                {
                    int temp = 0;
                    computeNSamplesAndRemainderTime(ref temp, ref remainderTime, step.StepDuration.getBaseValue(), timeStepSize);
                    count += temp;
                    if (count > nSamples)
                        return step;
                }
            }
            return null;
        }

        public int getSamplesAtTimestep(TimeStep step, double timeStepSize)
        {
            int count = 0;
            double remainderTime = 0;
            foreach (TimeStep ts in TimeSteps)
            {
                if (ts == step)
                    return count;
                if (ts.StepEnabled)
                {
                    int temp = 0;
                    computeNSamplesAndRemainderTime(ref temp, ref remainderTime, ts.StepDuration.getBaseValue(), timeStepSize);
                    count += temp;
                }
            }
            return -1;
        }


#endregion



        /// <summary>
        /// Gets the number of timesteps that have enabled set to true.
        /// </summary>
        /// <returns></returns>
        public int getNEnabledTimesteps()
        {
            int ans = 0;
            foreach (TimeStep st in TimeSteps)
                if (st.StepEnabled)
                    ans++;
            return ans;
        }

        public int getNthEnabledTimestepID(int N)
        {
            for (int i = 0; i < TimeSteps.Count; i++)
            {
                if (TimeSteps[i].StepEnabled)
                {
                    N--;
                }
                if (N == 0)
                    return i;
            }
            return -1;
        }


        #endregion


        /// <summary>
        /// Similar tp usedVariables(), but for waveforms
        /// </summary>
        /// <returns></returns>
        public Dictionary<Waveform, string> usedWaveforms()
        {
            Dictionary<Waveform, string> ans = usedCommonWaveforms();

            foreach (AnalogGroup ag in AnalogGroups)
            {
                Dictionary<Waveform, string> temp = ag.usedWaveforms();

                foreach (Waveform wf in temp.Keys)
                {
                    if (!ans.ContainsKey(wf))
                    {
                        ans.Add(wf, "Analog Group " + ag.ToString() + " " + temp[wf]);
                    }
                    else
                    {
                        ans[wf] += " Analog Group " + ag.ToString() + " " + temp[wf];
                    }
                }
            }

            foreach (GPIBGroup gg in GpibGroups)
            {
                Dictionary<Waveform, string> temp = gg.usedWaveforms();

                foreach (Waveform wf in temp.Keys)
                {
                    if (!ans.ContainsKey(wf))
                    {
                        ans.Add(wf, "GPIB Group " + gg.ToString() + " " + temp[wf]);
                    }
                    else
                    {
                        ans[wf] += " GPIB Group " + gg.ToString() + " " + temp[wf];
                    }
                }
            }

            foreach (RS232Group rg in RS232Groups)
            {
                Dictionary<Waveform, string> temp = rg.usedWaveforms();

                foreach (Waveform wf in temp.Keys)
                {
                    if (!ans.ContainsKey(wf))
                    {
                        ans.Add(wf, "RS232 Group " + rg.ToString() + " " + temp[wf]);
                    }
                    else
                    {
                        ans[wf] += " RS232 Group " + rg.ToString() + " " + temp[wf];
                    }
                }
            }

            return ans;


        }

        public Dictionary<Waveform, string> usedCommonWaveforms()
        {
            Dictionary<Waveform, string> ans = new Dictionary<Waveform, string>();
            foreach (TimeStep st in TimeSteps)
            {
                Dictionary<Waveform, string> temp = st.usedCommonWaveforms();
                foreach (Waveform wf in temp.Keys)
                {
                    if (!ans.ContainsKey(wf))
                    {
                        ans.Add(wf, "Timestep " + st.ToString() + " " + temp[wf]);
                    }
                    else
                    {
                        ans[wf] += " Timestep " + st.ToString() + " " + temp[wf];
                    }
                }
            }
            return ans;
        }

        public List<Pulse> usedPulses()
        {
            List<Pulse> ans = new List<Pulse>();
            foreach (TimeStep st in TimeSteps)
            {
                foreach (DigitalDataPoint dp in st.DigitalData.Values)
                {
                    if (dp.DigitalPulse != null)
                        if (!ans.Contains(dp.DigitalPulse))
                            ans.Add(dp.DigitalPulse);
                }
            }
            return ans;
        }

        public List<TimestepGroup> usedTsGroups()
        {
            List<TimestepGroup> ans= new List<TimestepGroup>();
            foreach (TimeStep st in TimeSteps)
            {
                if (st.MyTimestepGroup != null)
                    if (!ans.Contains(st.MyTimestepGroup))
                        ans.Add(st.MyTimestepGroup);
            }
            return ans;
        }


        /// <summary>
        /// Returns dictionary of all the used variables in the sequence, along with a string description of where they are used. Does not include
        /// variables which exist but are unused.
        /// </summary>
        /// <returns></returns>
        public Dictionary<Variable, string> usedVariables()
        {
            Dictionary<Variable, string> ans = new Dictionary<Variable, string>();
            // check the timesteps
            foreach (TimeStep st in TimeSteps) {
                Dictionary<Variable, string> temp = st.usedVariables();
                foreach (Variable var in temp.Keys)
                {
                    if (!ans.ContainsKey(var))
                    {
                        ans.Add(var, "Timestep " + st.ToString() + " " + temp[var]);
                    }
                    else
                    {
                        ans[var] += " Timestep " + st.ToString() + " " + temp[var];
                    }
                }
            }

            foreach (TimestepGroup tsg in TimestepGroups)
            {
                Dictionary<Variable, string> temp = tsg.usedVariables();
                foreach (Variable var in temp.Keys)
                {
                    if (!ans.ContainsKey(var))
                    {
                        ans.Add(var, "Timestep Group" + tsg.ToString() + " " + temp[var]);
                    }
                    else
                    {
                        ans[var] += " Timestep Group" + tsg.ToString() + " " + temp[var];
                    }
                }
            }

            foreach (AnalogGroup ag in AnalogGroups)
            {
                Dictionary<Variable, string> temp = ag.usedVariables();

                foreach (Variable var in temp.Keys)
                {
                    if (!ans.ContainsKey(var))
                    {
                        ans.Add(var, "Analog Group " + ag.ToString() + " " + temp[var]);
                    }
                    else
                    {
                        ans[var] += " Analog Group " + ag.ToString() + " " + temp[var];
                    }
                }
            }

            foreach (GPIBGroup gg in GpibGroups)
            {
                Dictionary<Variable, string> temp = gg.usedVariables();

                foreach (Variable var in temp.Keys)
                {
                    if (!ans.ContainsKey(var))
                    {
                        ans.Add(var, "GPIB Group " + gg.ToString() + " " + temp[var]);
                    }
                    else
                    {
                        ans[var] += " GPIB Group " + gg.ToString() + " " + temp[var];
                    }
                }
            }

            foreach (RS232Group rg in RS232Groups)
            {
                Dictionary<Variable, string> temp = rg.usedVariables();

                foreach (Variable var in temp.Keys)
                {
                    if (!ans.ContainsKey(var))
                    {
                        ans.Add(var, "RS232 Group " + rg.ToString() + " " + temp[var]);
                    }
                    else
                    {
                        ans[var] += " RS232 Group " + rg.ToString() + " " + temp[var];
                    }
                }
            }

            foreach (Pulse pulse in DigitalPulses)
            {
                Dictionary<Variable, string> temp = pulse.usedVariables();

                foreach (Variable var in temp.Keys)
                {
                    if (!ans.ContainsKey(var))
                    {
                        ans.Add(var, "Pulse " + pulse.ToString() + " " + temp[var]);
                    }
                    else
                    {
                        ans[var] += " Pulse " + pulse.ToString() + " " + temp[var];
                    }
                    
                }
            }

            /*foreach (Variable varb in Variables)
            {
                Dictionary<Variable, string> temp = varb.usedVariables();

                foreach (Variable var in temp.Keys)
                {
                    if (!ans.ContainsKey(var))
                    {
                        ans.Add(var, "Variable " + varb.ToString() + " " + temp[var]);
                    }
                    else
                    {
                        ans[var] += " Variable " + varb.ToString() + " " + temp[var];
                    }

                } 
            }*/

            return ans;
        }

        /// <summary>
        /// Returns true if the given rs232 channel is used anywhere in the sequence. False otherwise.
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public bool rs232ChannelUsed(int channelID)
        {
            foreach (TimeStep step in TimeSteps)
            {
                if (step.StepEnabled)
                {
                    if (step.rs232Group != null)
                    {
                        if (step.rs232Group.channelEnabled(channelID))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Replaces all occurences of pulse replaceMe with withMe, and then removes replaceMe
        /// from Pulses list.
        /// </summary>
        /// <param name="replaceMe"></param>
        /// <param name="withMe"></param>
        public void replacePulse(Pulse replaceMe, Pulse withMe)
        {
            foreach (TimeStep step in TimeSteps)
            {
                foreach (DigitalDataPoint dp in step.DigitalData.Values)
                {
                    if (dp.usesPulse())
                    {
                        if (dp.DigitalPulse == replaceMe)
                        {
                            dp.DigitalPulse = withMe;
                        }
                    }
                }
            }

            if (DigitalPulses.Contains(replaceMe))
            {
                DigitalPulses.Remove(replaceMe);
            }
        }

        /// <summary>
        /// Replaces all occurences in sequence of analog gropu ReplaceMe with group Withme, then
        /// removes ReplaceMe from list of groups.
        /// </summary>
        /// <param name="replaceMe"></param>
        /// <param name="withMe"></param>
        public void replaceAnalogGroup(AnalogGroup replaceMe, AnalogGroup withMe)
        {
            foreach (TimeStep step in TimeSteps)
            {
                if (step.AnalogGroup == replaceMe)
                {
                    step.AnalogGroup = withMe;
                }
            }

            if (AnalogGroups.Contains(replaceMe))
                AnalogGroups.Remove(replaceMe);
        }

        /// <summary>
        /// Replaces all occurences in sequence of GPIB gropu ReplaceMe with group Withme, then
        /// removes ReplaceMe from list of groups.
        /// </summary>
        /// <param name="replaceMe"></param>
        /// <param name="withMe"></param>
        public void replaceGPIBGroup(GPIBGroup replaceMe, GPIBGroup withMe)
        {
            foreach (TimeStep step in TimeSteps)
            {
                if (step.GpibGroup == replaceMe)
                {
                    step.GpibGroup = withMe;
                }
            }

            if (gpibGroups.Contains(replaceMe))
                gpibGroups.Remove(replaceMe);
        }


        /// <summary>
        /// Replaces all occurences in sequence of RS232 gropu ReplaceMe with group Withme, then
        /// removes ReplaceMe from list of groups.
        /// </summary>
        /// <param name="replaceMe"></param>
        /// <param name="withMe"></param>
        public void replaceRS232Group(RS232Group replaceMe, RS232Group withMe)
        {
            foreach (TimeStep step in TimeSteps)
            {
                if (step.rs232Group == replaceMe)
                {
                    step.rs232Group = withMe;
                }
            }

            if (rs232Groups.Contains(replaceMe))
                rs232Groups.Remove(replaceMe);
        }

        /// <summary>
        /// Returns true if all the following conditions are met:
        /// 1) Timestep group contains timesteps.
        /// 2) Timesteps contained within group are consecutive.
        /// </summary>
        /// <returns></returns>
        public bool TimestepGroupIsLoopable(TimestepGroup tsg)
        {
            if (tsg == null)
                return false;
            if (!TimestepGroups.Contains(tsg))
                return false;

            List<TimeStep> memberSteps = new List<TimeStep>();
            List<int> memberStepIds = new List<int>();
            foreach (TimeStep step in TimeSteps)
            {
                if (step.MyTimestepGroup == tsg)
                {
                    memberSteps.Add(step);
                    memberStepIds.Add(TimeSteps.IndexOf(step));
                }
            }

            if (memberStepIds.Count == 0)
                return false;

            int firstVal = memberStepIds[0];
            for (int i = 0; i < memberStepIds.Count; i++)
            {
                if (memberStepIds[i] != firstVal + i)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Should be run whenever timesteps are created / inserted / moved,
        /// to ensure that the looping timestep groups are still loopable.
        /// </summary>
        public void timestepsInsertedOrMoved()
        {
            checkAndRefreshTimestepGroupLoopability();
        }

        /// <summary>
        /// Removes and "loop copy" timesteps. This must be run
        /// at least once after each operation that creates the loop copies.
        /// 
        /// In addition, it can be run at any other time as a sanity check to make sure that there are no
        /// loop copies. If there are no loop copies, the sequence will be unaffected.
        /// </summary>
        public void cleanupLoopCopies()
        {
            // This loop runs through the timestep list backwards. The reason to run backwards
            // is that this allows use of the "RemoveAt" method of TimeSteps without affecting
            // the index of earler-in-the-list items
            for (int stepNum = TimeSteps.Count - 1; stepNum >= 0; stepNum--)
            {
                if (TimeSteps[stepNum].LoopCopy)
                    TimeSteps.RemoveAt(stepNum);
            }
        }


        public void createLoopCopies()
        {
            foreach (TimestepGroup tsg in this.TimestepGroups)
            {
                if (tsg.LoopTimestepGroup)
                {
                    if (TimestepGroupIsLoopable(tsg))
                    {
                        if (tsg.LoopCountInt > 1)
                        {
                            List<TimeStep> stepsToCopy = new List<TimeStep>();
                            int lastId = 0;
                            foreach (TimeStep st in TimeSteps)
                            {
                                if (st.MyTimestepGroup == tsg)
                                {
                                    stepsToCopy.Add(st);
                                    lastId = TimeSteps.IndexOf(st);
                                }
                            }


                            int currentId = lastId + 1;
                            for (int loop = 1; loop < tsg.LoopCountInt; loop++)
                            {
                                foreach (TimeStep st in stepsToCopy)
                                {
                                    TimeSteps.Insert(currentId, st.getLoopCopy(loop+1, tsg.LoopCountInt));
                                    currentId++;
                                }
                            }
                        }
                    }
                }
            }
        }


        public void checkAndRefreshTimestepGroupLoopability()
        {
            foreach (TimestepGroup tsg in this.TimestepGroups)
            {
                if (tsg.LoopTimestepGroup)
                {
                    if (!TimestepGroupIsLoopable(tsg))
                    {
                        tsg.LoopTimestepGroup = false;
                    }
                }
            }
        }

        #region Version Number Tracking

        private DataStructuresVersionNumber versionNumberAtFirstCreation;

        public DataStructuresVersionNumber VersionNumberAtFirstCreation
        {
            get { return versionNumberAtFirstCreation; }
        }


        private DataStructuresVersionNumber versionNumberAtLastSerialization;

        public DataStructuresVersionNumber VersionNumberAtLastSerialization
        {
            get { return versionNumberAtLastSerialization; }
        }

        [OnSerializing]
        private void setSerializationVersionNumber(StreamingContext sc)
        {
            this.versionNumberAtFirstCreation = DataStructuresVersionNumber.CurrentVersion;
        }

        #endregion
    }
}
