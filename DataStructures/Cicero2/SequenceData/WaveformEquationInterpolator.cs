using System;
using System.Collections.Generic;
using System.Text;
using dotMath;
using InvalidDataException = DataStructures.InvalidDataException;

namespace Cicero.DataStructures2
{
    public class WaveformEquationInterpolator
    {
        private static int stackCount = 0;

        public static string getEquationStatusString(string equationString, List<Variable> existingVariables, List<Waveform> existingCommonWaveforms)
        {
            dotMath.EqCompiler eq = new EqCompiler(equationString, true);
            try
            {
                eq.Compile();
            }
            catch (Exception e)
            {
                return e.Message;
            }

            string[] foundvars = eq.GetVariableList();
            if (foundvars != null)
            {

                List<string> foundVariables = new List<string>(eq.GetVariableList());

                List<string> existingVariableNames = new List<string>();
                List<string> existingCommonWaveformNames = new List<string>();
                foreach (Variable var in existingVariables)
                {
                    if (!existingVariableNames.Contains(var.VariableName))
                    {
                        existingVariableNames.Add(var.VariableName);
                    }
                }

                if (existingCommonWaveforms != null)
                {
                    foreach (Waveform wf in existingCommonWaveforms)
                    {
                        if (!existingCommonWaveformNames.Contains(wf.WaveformName))
                        {
                            existingCommonWaveformNames.Add(wf.WaveformName);
                        }
                    }
                }

                foreach (string foundVariableName in foundVariables)
                {
                    if (foundVariableName != "t")
                    {
                        if (!(existingVariableNames.Contains(foundVariableName)))
                        {
                            if (!existingCommonWaveformNames.Contains(foundVariableName))
                            {
                                return "No variable or common waveform found with name " + foundVariableName;
                            }
                        }
                    }
                }
            }

            return "Valid equation.";

        }

        public static void getEquationInterpolation(string equationString, double startTime, double endTime, int nSamples, int startIndex, double[]output, List<Variable> existingVariables, List<Waveform> existingCommonWaveforms, double wfDuration, Cicero2ResourceDictionary resources) 
        {
            string status = getEquationStatusString(equationString, existingVariables, existingCommonWaveforms);

            if (status == "Valid equation.")
            {
                // HERE WE GO!

                dotMath.EqCompiler eq = new EqCompiler(equationString, true);
                // This should not fail, because if it did we would have caught it with the status check
                eq.Compile();

                string [] foundvars = eq.GetVariableList();
                Dictionary<string, Waveform> waveformDependentVariables = new Dictionary<string, Waveform>();

                bool usingTimeVariable = false;
                bool usingCommonWaveforms = false;

                if (foundvars != null)
                {
                    foreach (string varname in foundvars)
                    {
                        bool variableMapped = false;
                        if (varname != "t")
                        {
                            foreach (Variable var in existingVariables)
                            {
                                if (var.VariableName == varname)
                                {
                                    eq.SetVariable(varname, var.VariableValue);
                                    variableMapped = true;
                                }
                            }
                        }

                        if (varname == "t")
                        {
                            usingTimeVariable = true;
                            variableMapped = true;
                        }

             

                        // variable named varname has not yet been mapped. Thus it must be a common waveform
                        if (!variableMapped)
                        {
                            if (existingCommonWaveforms != null)
                            {
                                foreach (Waveform wf in existingCommonWaveforms)
                                {
                                    if (wf.WaveformName == varname)
                                    {
                                        if (!waveformDependentVariables.ContainsKey(varname))
                                        {
                                            waveformDependentVariables.Add(varname, wf);
                                            usingCommonWaveforms = true;
                                        }
                                    }
                                }
                            }
                        }
                        
                    }
                }

                Dictionary<Waveform, double[]> commonWaveformInterpolations = new Dictionary<Waveform, double[]>();
                if (usingCommonWaveforms)
                {
                    foreach (Waveform wf in waveformDependentVariables.Values) {
                        WaveformEquationInterpolator.stackCount++;
                        if (stackCount > 40)
                        {
                            stackCount = 0;
                            throw new InvalidDataException("Stack count has reached 40 when attempting to interpolation an equation waveform. You have probably created a recursive reference loop using waveform equations. Please remove the offending circular references. Aborting interpolation.");
                        }
                            double[] interpol = wf.getInterpolation(nSamples, startTime, endTime, existingVariables, existingCommonWaveforms, resources);
                            commonWaveformInterpolations.Add(wf, interpol);
                        stackCount--;
                    }
                }

                for (int i = 0; i < nSamples; i++)
                {
                    if (usingTimeVariable)
                    {
                        double time = i * (endTime - startTime) / (double)nSamples + startTime;

                        if (time > wfDuration)
                            time = wfDuration;

                        eq.SetVariable("t", time);
                    }

                    if (usingCommonWaveforms)
                    {
                        foreach (string vname in waveformDependentVariables.Keys)
                        {
                            eq.SetVariable(vname, commonWaveformInterpolations[waveformDependentVariables[vname]][i]);
                        }
                    }

                    try
                    {
                        double val = eq.Calculate();
                        if ((!double.IsInfinity(val)) && (!double.IsNaN(val)))
                        {
                            output[i + startIndex] = eq.Calculate();
                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
            }
        }
    }
}
