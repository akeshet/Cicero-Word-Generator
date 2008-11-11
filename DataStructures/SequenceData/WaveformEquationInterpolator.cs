using System;
using System.Collections.Generic;
using System.Text;
using dotMath;

namespace DataStructures
{
    public class WaveformEquationInterpolator
    {
        public static string getEquationStatusString(string equationString, List<Variable> existingVariables)
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
                foreach (Variable var in existingVariables)
                {
                    if (!existingVariableNames.Contains(var.VariableName))
                    {
                        existingVariableNames.Add(var.VariableName);
                    }
                }

                foreach (string foundVariableName in foundVariables)
                {
                    if (foundVariableName != "t")
                    {
                        if (!(existingVariableNames.Contains(foundVariableName)))
                        {
                            return "No variable found with name " + foundVariableName;
                        }
                    }
                }
            }

            return "Valid equation.";

        }

        public static void getEquationInterpolation(string equationString, double startTime, double endTime, int nSamples, int startIndex, double[]output, List<Variable> existingVariables) 
        {
            string status = getEquationStatusString(equationString, existingVariables);

            if (status == "Valid equation.")
            {
                // HERE WE GO!

                dotMath.EqCompiler eq = new EqCompiler(equationString, true);
                // This should not fail, because if it did we would have caught it with the status check
                eq.Compile();

                string [] foundvars = eq.GetVariableList();

                bool usingTimeVariable = false;

                if (foundvars != null)
                {
                    foreach (string varname in foundvars)
                    {
                        if (varname != "t")
                        {
                            foreach (Variable var in existingVariables)
                            {
                                if (var.VariableName == varname)
                                {
                                    eq.SetVariable(varname, var.VariableValue);
                                }
                            }
                        }
                        else
                        {
                            usingTimeVariable = true;
                        }
                    }
                }

                for (int i = 0; i < nSamples; i++)
                {
                    if (usingTimeVariable)
                    {
                        double time = i * (endTime - startTime) / (double)nSamples + startTime;
                        eq.SetVariable("t", time);
                    }
                    try
                    {
                        double val = eq.Calculate();
                        if ((!double.IsInfinity(val)) && (!double.IsNaN(val)))
                        {
                            output[i + startIndex] = eq.Calculate();
                        }
                    }
                    catch (Exception e)
                    {
                        return;
                    }
                }
            }
        }
    }
}
