using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataStructures
{
    /// <summary>
    /// A sequence genre (now known as a sequence mode) is a saved collection of the enabled/disabled and 
    /// visible/invisible states of all of the timesteps. 
    /// A given sequence can have many such modes, allowing it to quickly transform between different closely related sequence.
    /// </summary>
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class SequenceMode
    {
        private string modeName;

        public string ModeName
        {
            get {
                if (modeName == null)
                    modeName = "";
                return modeName; 
            }
            set { modeName = value; }
        }

        public override string ToString()
        {
            return ModeName;
        }

        [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
        public class ModeEntry {
            private bool stepEnabled;

            public bool StepEnabled
            {
                get { return stepEnabled; }
                set { stepEnabled = value; }
            }
            private bool stepHidden;

            public bool StepHidden
            {
                get { return stepHidden; }
                set { stepHidden = value; }
            }

            public ModeEntry(bool stepEnabled, bool stepHidden)
            {
                this.StepEnabled = stepEnabled;
                this.StepHidden = stepHidden;
            }
        }

        private Dictionary<TimeStep, ModeEntry> timestepEntries;

        public Dictionary<TimeStep, ModeEntry> TimestepEntries
        {
            get { return timestepEntries; }
            set { timestepEntries = value; }
        }

        public SequenceMode()
        {
            this.TimestepEntries = new Dictionary<TimeStep, ModeEntry>();
        }


        public static SequenceMode createSequenceMode(SequenceData sequence)
        {
            SequenceMode ans = new SequenceMode();
            foreach (TimeStep step in sequence.TimeSteps)
            {
                ans.TimestepEntries.Add(step, new ModeEntry(step.StepEnabled, step.StepHidden));
            }
            return ans;
        }

        public static string applySequenceMode(SequenceData sequence, SequenceMode genre)
        {
            string ans = "";
            foreach (TimeStep step in sequence.TimeSteps)
            {
                if (!genre.TimestepEntries.ContainsKey(step))
                {
                    ans += "No mode entry for [" + step.StepName + "]. ";
                }
                else
                {
                    step.StepHidden = genre.TimestepEntries[step].StepHidden;
                    step.StepEnabled = genre.TimestepEntries[step].StepEnabled;
                }
            }
            if (ans == "")
                return null;
            else
                return ans;
        }

        public void cleanupUnusedEntries(SequenceData sequence)
        {
            List<TimeStep> stepsToRemove = new List<TimeStep>();
            foreach (TimeStep step in TimestepEntries.Keys)
            {
                if (!sequence.TimeSteps.Contains(step))
                    stepsToRemove.Add(step);
            }

            foreach (TimeStep step in stepsToRemove)
            {
                TimestepEntries.Remove(step);
            }
        }

    }
}
