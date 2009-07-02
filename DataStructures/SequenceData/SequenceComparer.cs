using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class SequenceComparer
    {
        public class SequenceDifference
        {
            private string description;

            public string Description
            {
                get { return description; }
                set { description = value; }
            }

            public SequenceDifference(string desc)
            {
                this.description = desc;
            }

            public override string ToString()
            {
                return description;
            }
        }



        public static List<SequenceDifference> CompareSequences(SequenceData seq1, SequenceData seq2)
        {
            List<SequenceDifference> ans = new List<SequenceDifference>();

            if (seq1 == null || seq2 == null)
            {
                ans.Add(new SequenceDifference("One of the sequences is null. Aborting."));
                return ans;
            }


            if (seq1.TimeSteps.Count != seq2.TimeSteps.Count)
            {
                ans.Add(new SequenceDifference("Differing number of timesteps"));
            }

            bool diffs = false;
            // Compare sequences timestep-by-timestep

            diffs |= CompareLists<TimeStep>("Timesteps: ", seq1.TimeSteps, seq2.TimeSteps, ans, CompareTimesteps);

            diffs |= CompareLists<AnalogGroup>("Analog groups: ", seq1.AnalogGroups, seq2.AnalogGroups, ans, CompareAnalogGroups);
            diffs |= CompareLists<GPIBGroup>("Gpib groups: ", seq1.GpibGroups, seq2.GpibGroups, ans,CompareGpibGroups);
            diffs |= CompareLists<RS232Group>("RS232 groups: ", seq1.RS232Groups, seq2.RS232Groups, ans, CompareRs232Groups);

            diffs |= CompareLists<Variable>("Variables: ", seq1.Variables, seq2.Variables, ans, CompareVariables);
            diffs |= CompareLists<Pulse>("Pulses: ", seq1.DigitalPulses, seq2.DigitalPulses, ans, ComparePulses);

            diffs |= CompareLists<Waveform>("Common waveforms: ", seq1.CommonWaveforms, seq2.CommonWaveforms, ans, CompareWaveforms);

            diffs |= CompareLists<SequenceMode>("Sequence modes: ", seq1.SequenceModes, seq2.SequenceModes, ans, CompareSequenceModes);

            return ans;

        }

        private static bool CompareGroups<TChannelType>(string preString, Group<TChannelType> a, Group<TChannelType> b, List<SequenceDifference> ans,
            compareItems<TChannelType> compareChannelData) where TChannelType : new()
        {
            if (a.GroupName != b.GroupName)
            {
                ans.Add(new SequenceDifference(preString + "names differ " + a.GroupName + "vs. " + b.GroupName + " skipping details comparison."));
                return true;
            }

            return CompareDictionaries<int, TChannelType>(preString + a.GroupName + " channel data :",
                a.ChannelDatas, b.ChannelDatas, ans, compareChannelData);
        }

        private static bool CompareAnalogGroups(string preString, AnalogGroup a, AnalogGroup b, List<SequenceDifference> ans)
        {
            bool diffs = false;

            diffs |= CompareGroups<AnalogGroupChannelData>(preString, a, b, ans, CompareAnalogChannelData);
            diffs |= CompareParameters(preString + "time resolution ", a.TimeResolution, b.TimeResolution, ans);
            return diffs;
        }

        private static bool CompareGpibGroups(string preString, GPIBGroup a, GPIBGroup b, List<SequenceDifference> ans)
        {
            return CompareGroups<GPIBGroupChannelData>(preString, a, b, ans, CompareGpibChannelData);
        }

        private static bool CompareRs232Groups(string preString, RS232Group a, RS232Group b, List<SequenceDifference> ans)
        {
            return CompareGroups<RS232GroupChannelData>(preString, a, b, ans, CompareRs232ChannelData);
        }

        private static bool CompareAnalogChannelData(string preString, AnalogGroupChannelData a, AnalogGroupChannelData b, List<SequenceDifference> ans)
        {
            bool diffs = false;

            diffs |= CompareBools(preString + "enabled/disabled ", a.ChannelEnabled, b.ChannelEnabled, ans);
            diffs |= CompareBools(preString + "using common waveform ", a.ChannelWaveformIsCommon, b.ChannelWaveformIsCommon, ans);
            diffs |= CompareWaveforms(preString + "waveform ", a.waveform, b.waveform, ans);

            return diffs;
        }

        private static bool CompareGpibChannelData(string preString, GPIBGroupChannelData a, GPIBGroupChannelData b, List<SequenceDifference> ans)
        {
            bool diffs = false;
            diffs |= CompareGenericStructsAsStrings<GPIBGroupChannelData.GpibChannelDataType>(preString + "data type ", a.DataType, b.DataType, ans);
            diffs |= CompareBools(preString + "enabled/disabled ", a.Enabled, b.Enabled, ans);
            diffs |= CompareStrings(preString + "raw string ", a.RawString, b.RawString, ans);
            diffs |= CompareLists<StringParameterString>(preString + "string-parameter-string ", a.StringParameterStrings, b.StringParameterStrings, ans, CompareStringParameterStrings);
            diffs |= CompareWaveforms(preString + "freq waveform ", a.frequency, b.frequency, ans);
            diffs |= CompareWaveforms(preString + "volt waveform ", a.volts, b.volts, ans);
            return diffs;
        }

        private static bool CompareRs232ChannelData(string preString, RS232GroupChannelData a, RS232GroupChannelData b, List<SequenceDifference> ans)
        {
            bool diffs = false;
            diffs |= CompareGenericStructsAsStrings<RS232GroupChannelData.RS232DataType>(preString + "data type ", a.DataType, b.DataType, ans);
            diffs |= CompareBools(preString + "enabled/disabled ", a.Enabled, b.Enabled, ans);
            diffs |= CompareStrings(preString + "raw string ", a.RawString, b.RawString, ans);
            diffs |= CompareLists<StringParameterString>(preString + "string-parameter-strings ", a.StringParameterStrings, b.StringParameterStrings, ans, CompareStringParameterStrings);
            return diffs;
        }

        private static bool CompareStringParameterStrings(string preString, StringParameterString a, StringParameterString b, List<SequenceDifference> ans)
        {
            bool diffs = false;
            diffs |= CompareStrings(preString + "prefix ", a.Prefix, b.Prefix, ans);
            diffs |= CompareStrings(preString + "postfix ", a.Postfix, b.Postfix, ans);
            diffs |= CompareParameters(preString + "parameter ", a.Parameter, b.Parameter, ans);
            return diffs;
        }

        private static bool CompareWaveforms(string preString, Waveform a, Waveform b, List<SequenceDifference> ans)
        {
            if (a == null || b == null)
            {
                if (a == null && b == null)
                {
                    return false;
                }
                else
                {
                    addDifference(preString + " one waveform null, other not.", ans);
                    return true;
                }
            }

            bool diffs = false;

            diffs |= CompareParameters(preString + "duration ", a.WaveformDuration, b.WaveformDuration, ans);
            diffs |= CompareLists<DimensionedParameter>(preString + "xValues ", a.XValues, b.YValues, ans, CompareParameters);
            diffs |= CompareLists<DimensionedParameter>(preString + "yValues ", a.YValues, b.YValues, ans, CompareParameters);
            diffs |= CompareLists<DimensionedParameter>(preString + "special parameters ", a.ExtraParameters, b.ExtraParameters, ans, CompareParameters);
            diffs |= CompareStrings(preString + "data file names ", a.DataFileName, b.DataFileName, ans);
            diffs |= CompareBools(preString + "using data file ", a.DataFromFile, b.DataFromFile, ans);
            diffs |= CompareStrings(preString + "equation string ", a.EquationString, b.EquationString, ans);
            diffs |= CompareInts(preString + "interpolation type ", (int)a.interpolationType, (int)b.interpolationType, ans);
            if (!a.isWaveformReferenceRecursive() && !b.isWaveformReferenceRecursive())
            {
                diffs |= CompareLists<Waveform>(preString + "referenced waveforms ", a.ReferencedWaveforms, b.ReferencedWaveforms, ans, CompareWaveforms);
            }
            else
            {
                addDifference(preString + "unable to compare reference waveforms due to recursive loop.", ans);
                diffs = true;
            }
            diffs |= CompareLists<Waveform.InterpolationType.CombinationOperators>(preString + "combiner operators ", a.WaveformCombiners, b.WaveformCombiners, ans, 
                CompareGenericStructsAsStrings<Waveform.InterpolationType.CombinationOperators>);
            diffs |= CompareStrings(preString + "name ", a.WaveformName, b.WaveformName, ans);

            return diffs;
           

        }


        /// <summary>
        /// Use only for types that are int-castable!!! otherwise this will throw and exception and explode
        /// </summary>
        /// <param name="preString"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        private static bool CompareGenericStructsAsStrings<TType>(string preString, TType a, TType b, List<SequenceDifference> ans) where TType : struct
        {
            string aStr = a.ToString();
            string bStr = b.ToString();

            return CompareStrings(preString, aStr, bStr, ans);
        }

        private static bool CompareInts(string preString, int a, int b, List<SequenceDifference> ans)
        {
            if (a != b)
            {
                addDifference(preString + "differs.", ans);
                return true;
            }
            return false;
        }

        private static bool CompareStrings(string preString, string a, string b, List<SequenceDifference> ans)
        {
            if (a != b)
            {
                addDifference(preString + "differs.", ans);
                return true;
            }
            return false;
        }

        private static bool CompareTimesteps(string preString, TimeStep a, TimeStep b, List<SequenceDifference> ans) {
            
            
            if (a.StepName != b.StepName)
            {
                ans.Add(new SequenceDifference( preString+"Step names differ. " + a.StepName + " vs. " + b.StepName + " Skipping detailed comparison."));
                return true;
            }

            if (a.StepEnabled != b.StepEnabled)
            {
                ans.Add(new SequenceDifference(preString+": Step enabled/disabled differ. Skipping detailing comparison."));
            }

            bool diffs = false;

            diffs|=CompareParameters(preString + "Duration: " , a.StepDuration, b.StepDuration, ans);
            diffs|=CompareBools(preString + "Enabled/Disabled: ", a.StepEnabled, b.StepEnabled, ans);
            diffs|=CompareBools(preString + "Hidden/Shown: ", a.StepHidden, b.StepHidden, ans);

            diffs|=CompareDictionaries<int, DigitalDataPoint>(preString + "Digital Values: ", a.DigitalData, b.DigitalData, ans, CompareDigitalDataPoint );

            return diffs;
            
        }


        private static void addDifference(string difference, List<SequenceDifference> ans)
        {
            ans.Add(new SequenceDifference(difference));
        }
       

        delegate bool compareItems<TType>(string prestring, TType a, TType b, List<SequenceDifference> ans);

        private static bool CompareDigitalDataPoint(string prestring, DigitalDataPoint a, DigitalDataPoint b, List<SequenceDifference> ans)
        {
            bool diffs = false;

            if (a.DigitalPulse != null || b.DigitalPulse != null)
            {
                if (a.DigitalPulse != null && b.DigitalPulse != null)
                {
                    if (a.DigitalPulse.PulseName != b.DigitalPulse.PulseName)
                    {
                        diffs = true;
                        ans.Add(new SequenceDifference(prestring + "pulse names differ."));
                    }
                }
                else
                {
                    ans.Add(new SequenceDifference(prestring + "one uses pulse, other doesn't."));
                    diffs = true;
                }
            }

            diffs |= CompareBools(prestring + "manual value ", a.ManualValue, b.ManualValue, ans);
            return diffs;
        }

        private static bool CompareLists<TVal>(string prestring, List<TVal> a, List<TVal> b, List<SequenceDifference> ans,
            compareItems<TVal> compareValues)
        {


            if (a.Count != b.Count)
            {
                ans.Add(new SequenceDifference(prestring + "Lists different length. Skipping comparison."));
                return true;
            }

            bool diffs = false ;
            for (int i = 0; i < a.Count; i++)
            {
                TVal valA, valB;
                valA = a[i];
                valB = b[i];
                diffs |= compareValues(prestring + "Item #" + i + "["+valA.ToString()+"]: ", valA, valB, ans);
            }
            return diffs;
        }

        private static bool CompareDictionaries<Tkey, TVal>(string prestring, Dictionary<Tkey, TVal> d1, Dictionary<Tkey, TVal> d2, List<SequenceDifference> ans,
            compareItems<TVal> compareValues)
        {

            if (onewayKeyCompare<Tkey, TVal>(prestring, d1, d2, ans))
                return true;
            if (onewayKeyCompare<Tkey, TVal>(prestring, d2, d1, ans))
                return true;

            bool diff = false;

            foreach (Tkey key in d1.Keys) {
                TVal valA, valB;
                valA = d1[key];
                valB = d2[key];

                diff |= compareValues(prestring + " Item [" + key.ToString() + "]: " , valA, valB, ans);
                
            }

            return diff;

        }

        private static bool onewayKeyCompare<Tkey, TVal>(string prestring, Dictionary<Tkey, TVal> d1, Dictionary<Tkey, TVal> d2, List<SequenceDifference> ans)
        {
            foreach (Tkey key in d1.Keys)
            {
                if (!d2.ContainsKey(key))
                {
                    ans.Add(new SequenceDifference(prestring + "different keys."));
                    return true;
                }
            }
            return false;
        }

        private static bool CompareBools(string prestring, bool a, bool b, List<SequenceDifference> ans)
        {
            if (a != b)
            {
                ans.Add(new SequenceDifference(prestring + "differ."));
                return true;
            }
            return false;
        }

        private static bool CompareParameters(string prestring, DimensionedParameter a, DimensionedParameter b, List<SequenceDifference> ans)
        {
            if (a.myParameter.variable != null || b.myParameter.variable != null)
            {
                if (a.myParameter.variable != null && b.myParameter.variable != null)
                {
                    if (a.myParameter.variable.VariableName != b.myParameter.variable.VariableName)
                    {
                        ans.Add(new SequenceDifference(prestring + "bound to variables of different name."));
                        return true;
                    }
                }
                else
                {
                    ans.Add(new SequenceDifference(prestring + "one bound to variable, other manual."));
                    return true;
                }
            }
            else
            {
                if (a.getBaseValue() != b.getBaseValue())
                {
                    ans.Add(new SequenceDifference(prestring + "different manual values."));
                    return true;
                }
            }
            return false;
        }

        private static bool CompareVariables(string preString, Variable a, Variable b, List<SequenceDifference> ans)
        {
            bool diffs = false;
            diffs |= CompareBools(preString + "derived/nonderived ", a.DerivedVariable, b.DerivedVariable,ans);
            diffs |= CompareBools(preString + "special/nonspecial ", a.IsSpecialVariable, b.IsSpecialVariable, ans);
            diffs |= CompareBools(preString + "listdriven/nondriven ", a.ListDriven, b.ListDriven, ans);
            diffs |= CompareInts(preString + "list number ", a.ListNumber, b.ListNumber, ans);
            diffs |= CompareGenericStructsAsStrings<Variable.SpecialVariableType>(preString + "special variable type ", a.MySpecialVariableType, b.MySpecialVariableType, ans);
            diffs |= CompareDoubles(preString + "permanent Value ", a.PermanentValue, b.PermanentValue, ans);
            diffs |= CompareBools(preString + "permanent/nonpermanent ", a.PermanentVariable, b.PermanentVariable, ans);
            diffs |= CompareStrings(preString + "equation string ", a.VariableFormula, b.VariableFormula, ans);
            diffs |= CompareStrings(preString + "variable name ", a.VariableName, b.VariableName, ans);
            diffs |= CompareDoubles(preString + "variable value ", a.VariableValue, b.VariableValue, ans);
            return diffs;
        }

        private static bool ComparePulses(string preString, Pulse a, Pulse b, List<SequenceDifference> ans)
        {
            bool diffs = false;

            diffs |= CompareGenericStructsAsStrings<Pulse.PulseTimingCondition>(preString + "end condition ", a.endCondition, b.endCondition, ans);
            diffs |= CompareParameters(preString + "end delay ", a.endDelay, b.endDelay, ans);
            diffs |= CompareBools(preString + "end delayed/advanced ", a.endDelayed, b.endDelayed, ans);
            diffs |= CompareBools(preString + "end delay enabled/disabled ", a.endDelayEnabled, b.endDelayEnabled, ans);

            diffs |= CompareStrings(preString + "pulse name ", a.PulseName, b.PulseName, ans);
            diffs |= CompareBools(preString + "pulse value ", a.PulseValue, b.PulseValue, ans);
            
            diffs |= CompareGenericStructsAsStrings<Pulse.PulseTimingCondition>(preString + "start condition ", a.startCondition, b.startCondition, ans);
            diffs |= CompareParameters(preString + "start delay ", a.startDelay, b.startDelay, ans);
            diffs |= CompareBools(preString + "start delayed/advanced ", a.startDelayed, b.startDelayed, ans);
            diffs |= CompareBools(preString + "start delay enabled/disabled ", a.startDelayEnabled, b.startDelayEnabled, ans);

            diffs |= CompareBools(preString + "value from variable/manual ", a.ValueFromVariable, b.ValueFromVariable, ans);
            diffs |= CompareStrings(preString + "name of value variable ", a.ValueVariable.VariableName, b.ValueVariable.VariableName, ans);
            return diffs;

        }

        private static bool CompareDoubles(string preString, double a, double b, List<SequenceDifference> ans)
        {
            if (a != b)
            {
                addDifference("differs.", ans);
                return true;
            }
            return false;
        }

        private static bool CompareSequenceModes(string preString, SequenceMode a, SequenceMode b, List<SequenceDifference> ans)
        {
            bool diffs = false;
            diffs |= CompareStrings(preString + "mode name ", a.ModeName, b.ModeName, ans);
            diffs |= CompareDictionaries<TimeStep, SequenceMode.ModeEntry>(preString + "mode entries ", a.TimestepEntries, b.TimestepEntries, ans, CompareSequenceModeEntry);
            return diffs;
        }

        private static bool CompareSequenceModeEntry(string preString, SequenceMode.ModeEntry a, SequenceMode.ModeEntry b, List<SequenceDifference> ans)
        {
            bool diffs = false;
            diffs |= CompareBools(preString + "enabled/disabled ", a.StepEnabled, b.StepEnabled, ans);
            diffs |= CompareBools(preString + "hidden/shown ", a.StepHidden, b.StepHidden, ans);
            return diffs;
        }


    }
}
