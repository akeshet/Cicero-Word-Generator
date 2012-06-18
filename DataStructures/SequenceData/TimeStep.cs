using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DataStructures;

namespace DataStructures
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class TimeStep
    {
        private string stepName;

        [Category("Global"), Description("Name of the timestep.")]
        public string StepName
        {
            get { return stepName; }
            set { stepName = value; }
        }

        private string description;

        [Category("Global"), Description("Description of the timestep.")]
        public string Description
        {
            get {
                if (description == null)
                    description = "";
                return description; 
            }
            set { description = value; }
        }

        public bool usesPulses()
        {
            foreach (DigitalDataPoint dp in DigitalData.Values)
            {
                if (dp.usesPulse()) return true;
            }
            return false;
        }

        private TimestepGroup myTimestepGroup;

        public TimestepGroup MyTimestepGroup
        {
            get { return myTimestepGroup; }
            set { myTimestepGroup = value; }
        }

        private bool usesTimestepGroup;

        public bool UsesTimestepGroup
        {
            get
            {
                if (MyTimestepGroup == null)
                    return false;
                return true;
            }
        }

        private bool loopCopy;

        /// <summary>
        /// True if this timestep is in fact a temporary
        /// "loop copy" timestep which has been inserted into a sequence in 
        /// order to implement a timestep group loop.
        /// 
        /// Loop copy timesteps should be removed immediately after use.
        /// </summary>
        public bool LoopCopy
        {
            get
            {
                return loopCopy;
            }
            set { loopCopy = value; }
        }


        // 0 for unassigned
        private char hotKeyCharacter;

        /// <summary>
        /// 0 indicates unassigned
        /// </summary>
        public char HotKeyCharacter
        {
            get { return hotKeyCharacter; }
            set { hotKeyCharacter = value; }
        }

        private bool stepEnabled;

        [Category("Global"), Description("Whether or not the timestep is enabled.")]
        public bool StepEnabled
        {
            get
            {
                if (UsesTimestepGroup)
                    return (stepEnabled && MyTimestepGroup.GroupEnabled);
                else
                    return stepEnabled;
            }
            set
            {
                // Ignore outside modifications to enabled/disabled state if overridden by timestep group.
                if (UsesTimestepGroup)
                    if (!MyTimestepGroup.GroupEnabled)
                        return;
                stepEnabled = value;
            }
        }

        private bool stepHidden;

        [Category("Global"), Description("Whether or the the timestep is hidden.")]
        public bool StepHidden
        {
            get
            {
                if (UsesTimestepGroup)
                    return (stepHidden || MyTimestepGroup.GroupHidden);
                else
                    return stepHidden;
            }
            set
            {
                // Ignore outside modifications to hidden/visible state if overridden by timestep group.
                if (UsesTimestepGroup)
                    if (!MyTimestepGroup.GroupEnabled)
                        return;
                stepHidden = value;
            }
        }

        private bool waitForRetrigger;

        public bool WaitForRetrigger
        {
            get { return waitForRetrigger; }
            set { waitForRetrigger = value; }
        }

        private DimensionedParameter retriggerTimeout;

        public DimensionedParameter RetriggerTimeout
        {
            get
            {
                if (retriggerTimeout == null)
                    retriggerTimeout = new DimensionedParameter(Units.s, 0);
                return retriggerTimeout;
            }
            set { retriggerTimeout = value; }
        }

        private AnalogGroup myAnalogGroup;

        [Category("Groups"), Description("The analog group started in this timestep, if any.")]
        public AnalogGroup AnalogGroup
        {
            get { return myAnalogGroup; }
            set { myAnalogGroup = value; }
        }

        private GPIBGroup gpibGroup;

        [Category("Groups"), Description("The gpib group started in this timestep, if any.")]
        public GPIBGroup GpibGroup
        {
            get { return gpibGroup; }
            set { gpibGroup = value; }
        }

        private RS232Group myRS232Group;

        [Category("Groups"), Description("The rs232 group started in this timestep, if any.")]
        public RS232Group rs232Group
        {
            get { return myRS232Group; }
            set { myRS232Group = value; }
        }


        private DimensionedParameter stepDuration;

        [Category("Global"), Description("The duration of this timestep.")]
        public DimensionedParameter StepDuration
        {
            get { return stepDuration; }
            set { stepDuration = value; }
        }

        /// <summary>
        /// returns null if there is no waveform associated with that analog channel ID. Otherwise returns the waveform.
        /// </summary>
        /// <param name="analogChannelID"></param>
        /// <returns></returns>
        public Waveform getChannelWaveform(int analogChannelID) 
        {
            if (AnalogGroup == null)
                return null;
            if (!AnalogGroup.containsChannelID(analogChannelID))
                return null;
            if (!AnalogGroup.ChannelDatas[analogChannelID].ChannelEnabled)
                return null;
            return AnalogGroup.ChannelDatas[analogChannelID].waveform;
        }

        /// <summary>
        /// Gets the value of the specified analog channel at the end of the timestep. Returns 0 if value is unspecified. Note
        /// that this is not necessarily the true sequence-specified value, as this method does not take into account
        /// analog groups which have been continued from previous timesteps.
        /// </summary>
        /// <param name="analogChannelID"></param>
        /// <returns></returns>
        public double getEndAnalogValue(int analogChannelID, List<Variable> existingVariables, List<Waveform> existingCommonWaveforms)
        {
            Waveform wf = getChannelWaveform(analogChannelID);
            if (wf == null)
                return 0;

            return wf.getValueAtTime(this.stepDuration.getBaseValue(), existingVariables, existingCommonWaveforms);
               
        }

        /// <summary>
        /// Gets the digital value of the specified digital channel for this timestep. Returns false if unspecified.
        /// </summary>
        /// <param name="digitalChannelID"></param>
        /// <returns></returns>
        public bool getDigitalValue(int digitalChannelID, List<TimeStep> allSteps, int myIndex)
        {
            if (DigitalData == null)
                return false;
            if (!DigitalData.ContainsKey(digitalChannelID))
                return false;
            if (DigitalData[digitalChannelID].DigitalContinue) // this will be slightly more complicated, we need to look back in time
            {
                return calculateDigitalContinueValue(digitalChannelID, allSteps, myIndex);
            }
            else
            {
                return DigitalData[digitalChannelID].getValue();
            }
        }

        private bool calculateDigitalContinueValue(int digitalChannelID, List<TimeStep> allSteps, int myIndex)
        {
            if (allSteps[myIndex] != this)
                throw new Exception("Error when attempting to calculate digital continue value.");
            int checkStep = myIndex - 1;
            bool val = false;
            while (checkStep >= 0)
            {
                TimeStep st = allSteps[checkStep];
                if (!st.StepEnabled)
                {
                    checkStep--;
                    continue;
                }

                if (!st.digitalData.ContainsKey(digitalChannelID))
                {
                    val = false;
                    break;
                }

                if (st.digitalData.ContainsKey(digitalChannelID))
                {
                    if (st.digitalData[digitalChannelID].DigitalContinue)
                    {
                        checkStep--;
                        continue;
                    }

                    val = st.digitalData[digitalChannelID].getValue();
                    break;

                }
            }
            return val;
        }

        private Dictionary<int, DigitalDataPoint> digitalData;

        [Category("Digital"), Description("A list, indexed by digital channel ID, of the digital data for this timestep.")]
        public Dictionary<int, DigitalDataPoint> DigitalData
        {
            get { return digitalData; }
            set { digitalData = value; }
        }

        public TimeStep()
        {
            digitalData = new Dictionary<int, DigitalDataPoint>();
            stepDuration = new DimensionedParameter(Units.Dimension.s);
        }

        public TimeStep(string timeStepName) : this()
        {
            this.stepName = timeStepName;
        }

        /// <summary>
        /// Duplicate creation constructor for Timesteps. Creates an independently editable copy of the timestep
        /// </summary>
        /// <param name="duplicateMe"></param>
        public TimeStep(TimeStep duplicateMe) : this()
        {
            this.AnalogGroup = duplicateMe.AnalogGroup;
            foreach (int id in duplicateMe.DigitalData.Keys)
            {
                this.DigitalData.Add(id, new DigitalDataPoint(duplicateMe.DigitalData[id]));
            }
            this.GpibGroup = duplicateMe.GpibGroup;
            this.rs232Group = duplicateMe.rs232Group;
            this.StepDuration = new DimensionedParameter(duplicateMe.stepDuration);
            this.StepEnabled = duplicateMe.StepEnabled;
            this.StepHidden = duplicateMe.StepHidden;
            this.StepName = "Copy of " + duplicateMe.StepName;
            this.MyTimestepGroup = duplicateMe.MyTimestepGroup;
            this.WaitForRetrigger = duplicateMe.WaitForRetrigger;
            this.Description = duplicateMe.Description;
            this.LoopCopy = duplicateMe.LoopCopy;
        }

        public TimeStep getLoopCopy(int loopNum, int totalLoops)
        {
            TimeStep ans = new TimeStep(this);
            ans.LoopCopy = true;
            ans.loopOriginalCopy = this;
            ans.StepName = this.StepName + " Loop #" + loopNum + "/" + totalLoops;
            return ans;
        }

        public TimeStep loopOriginalCopy;

        public override string ToString()
        {
            return this.stepName;
        }

        /// <summary>
        /// Returns a dictionary containing all the variables used in the timestep, along with a string description of where they are.
        /// Does not include variables used in any of the analog or gpib etc groups.
        /// </summary>
        /// <returns></returns>
        public Dictionary<Variable, string> usedVariables()
        {
            Dictionary<Variable, string> ans = new Dictionary<Variable, string>();

            if (this.StepDuration.parameter.variable != null)
                ans.Add(this.stepDuration.parameter.variable, "Duration.");

            foreach (int digID in digitalData.Keys)
            {
                DigitalDataPoint dp = digitalData[digID];
                if (dp != null)
                {
                    if (dp.variable != null)
                    {
                        if (!ans.ContainsKey(dp.variable))
                        {
                            ans.Add(dp.variable, "Digital data, ID #" + digID + ".");
                        }
                    }
                }
            }

            return ans;
        }

        /// <summary>
        /// Doesn't really do anything.
        /// </summary>
        /// <returns></returns>
        public Dictionary<Waveform, string> usedCommonWaveforms()
        {
            return new Dictionary<Waveform, string>();
        }
    }
}
