using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DataStructures;

namespace DataStructures
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class Variable
    {
        public static readonly string EQUATION_ERROR_RECURSIVE = "Recursive variable reference detected.";

        /// <summary>
        /// This field is to be used only in very limited circumstances. When it is set to null, it makes no difference
        /// to the usual behavior of the variable. When it is non-null, it will cause this variable to get its value from the 
        /// variable specified in passthroughvariable.
        /// 
        /// At present, this is only used in one circumstance -- when inserting a sequence into another sequence, some parameters
        /// in the original inserted sequence may have referenced special variables. Instead of searching out those parameters,
        /// we instead take the special variables from the inserted sequence and make them get their values from the new special variables.
        /// 
        /// This should only ever be non-null for special variables.
        /// </summary>
        public Variable passThroughVariable;

        private string variableName;

        /// <summary>
        /// Number of lists to display in UI.
        /// </summary>
        public static readonly int NLists = 10;

        private bool relevant;

        /// <summary>
        /// A user-settable flag indicating whether the varaible is "relevant" to
        /// whatever operation they are performing. This will get logged, and could
        /// be useful for culling the information you're looking for from large archives
        /// of run logs.
        /// </summary>
        public bool Relevant
        {
            get { return relevant; }
            set { relevant = value; }
        }


        private string description;

        public string Description
        {
            get
            {
                if (description == null)
                    description = "";
                return description;
            }
            set { description = value; }
        }

        private bool permanentVariable;

        public bool PermanentVariable
        {
            get { return permanentVariable; }
            set { permanentVariable = value; }
        }

        private double permanentValue;

        public double PermanentValue
        {
            get { return permanentValue; }
            set { permanentValue = value; }
        }

        private string variableFormula;

        public string VariableFormula
        {
            get {

                if (variableFormula == null)
                    variableFormula = "";
                return variableFormula; 
            }
            set { variableFormula = value; }
        }

        /// <summary>
        /// If variable's DerivedVariable flag is set to true, evaluates the
        /// equation in VariableFormula and stores the result in VariableValue. 
        /// 
        /// In case of an error (such as recursive variable references, or
        /// references to variables that don't exist, or parse errors) returns a user-comprehensible
        /// string description of the problem.
        /// 
        /// Returns null if successfull with no errors.
        /// </summary>
        /// <param name="allVariables">List of all other variable objects, in order to track down 
        /// values of other referenced variables.</param>
        /// <returns></returns>
        public string parseVariableFormula(List<Variable> allVariables) 
        {
            return parseVariableFormula(allVariables, new List<Variable>());
        }

        private string parseVariableFormula(List<Variable> allVariables, List<Variable> treeVariables)
        {

            if (treeVariables.Contains(this))
                return EQUATION_ERROR_RECURSIVE;

            treeVariables.Add(this);

            Dictionary<string, Variable> variableNames = new Dictionary<string, Variable>();
            foreach (Variable var in allVariables)
            {
                if (variableNames.ContainsKey(var.VariableName))
                    variableNames[var.VariableName] = null;
                else
                    variableNames.Add(var.VariableName, var);
            }

            dotMath.EqCompiler eq = new dotMath.EqCompiler(variableFormula, true);

           // eq.AddFunction(new CPow());


            try
            {
                eq.Compile();
            }
            catch (Exception e)
            {
                return e.Message;
            }

            string[] usedVariables = eq.GetVariableList();

            if (usedVariables != null)
            {


                for (int i = 0; i < usedVariables.Length; i++)
                {

                    if (!variableNames.ContainsKey(usedVariables[i]))
                    {
                        VariableValue = 0;
                        return "Unable to parse formula. There is no variable named " + usedVariables[i] + ".";
                    }

                    if (variableNames[usedVariables[i]] == null)
                    {
                        VariableValue = 0;
                        return "Unable to parse formula. There are multiple variables named " + usedVariables[i] + ".";
                    }

                    Variable currentVar = variableNames[usedVariables[i]];

                    if (currentVar.DerivedVariable)
                    {
                        string err = currentVar.parseVariableFormula(allVariables, treeVariables);
                        if (err != null)
                        {
                            VariableValue = 0;
                            return err;
                        }
                    }

                    eq.SetVariable(usedVariables[i], currentVar.VariableValue);
                }

            }

            try
            {
                this.VariableValue = eq.Calculate();
            }
            catch (Exception e)
            {
                VariableValue = 0;
                return e.Message;
            }

            treeVariables.Remove(this);

            return null;

        }

        private double variableValue;

        private bool listDriven;

        [Description("Indicates whether or not this variable is being driven by a list.")]
        public bool ListDriven
        {
            get { return listDriven; }
            set { listDriven = value; }
        }

        //Database driven:
        private bool dbDriven;

        public bool DBDriven
        {
            get { return dbDriven; }
            set { dbDriven = value; }

        }

        //Database field number if applicable:
        private int dbFieldNumber;

        public int DBFieldNumber
        {
            get { return dbFieldNumber; }
            set { dbFieldNumber = value; }

        }
        //LUT driven status:
        private bool lutDriven;

        public bool LUTDriven
        {
            get { return lutDriven; }
            set { lutDriven = value; }

        }

        //LUT number if applicable:
        private int LUTdbFieldNumber;

        public int LUTNumber
        {
            get { return LUTdbFieldNumber; }
            set { LUTdbFieldNumber = value; }

        }

        //LUT input variable if applicable
        private Variable lutInput;

        public Variable LUTInput
        {
            get { return lutInput; }
            set { lutInput = value; }
        }

        private int listNumber;

        [Description("If ListDriven is true, indicates the list number which is driving this variable.")]
        public int ListNumber
        {
            get { return listNumber; }
            set { listNumber = value; }
        }

        [Description("The value of this variable.")]
        public double VariableValue
        {
            get {
          /*      if (this.DerivedVariable)
                {
                    if (this.derivedVariableSanityCheck()==null)
                    {
                        return derivedValue();
                    }
                    else
                    {
                        return 0;
                    }
                }*/
                if (passThroughVariable != null)
                    return passThroughVariable.VariableValue;

                if (this.PermanentVariable)
                    return this.PermanentValue;

                return variableValue; 
            }
            set {
             /*   if (!this.DerivedVariable)
                {*/
                    variableValue = value;
               /* }*/
            }
        }

        [Description("The name of this variable.")]
        public string VariableName
        {
            get
            {
                if (variableName == null)
                    variableName = "";
                return variableName;
            }
            set { variableName = value; }
        }

        public override string ToString()
        {
            return variableName;
        }




#region Derived variables. IE variables that are arithmetically driven by other variables.

    /*    public  const  string plus = "+";
        public  const  string minus = "-";
        public  const  string times = "*";
        public  const  string over = "/";
        public static readonly string[] combinerOperators = new string[] { plus, minus, times, over };

        private List<string> combiners;

        public List<string> Combiners
        {
            get {
                if (combiners == null)
                {
                    combiners = new List<string>();
                }
                return combiners; }
            set { combiners = value; }
        }
        private List<DimensionedParameter> combinedValues;

        public List<DimensionedParameter> CombinedValues
        {
            get {
                if (combinedValues == null)
                {
                    combinedValues = new List<DimensionedParameter>();
                }
                return combinedValues; }
            set { combinedValues = value; }
        }*/

        private bool derivedVariable;

        public bool DerivedVariable
        {
            get { return derivedVariable; }
            set
            {
                derivedVariable = value;
                if (derivedVariable)
                {
                    this.ListDriven = false;
                }
            }
        }
        



   /*     public double derivedValue()
        {
            double accumulator = 0;
            double register = 0;
            register += CombinedValues[0].getBaseValue();
            for (int i = 1; i < combinedValues.Count; i++)
            {
                switch (this.Combiners[i - 1])
                {
                    case plus:
                        accumulator += register;
                        register = CombinedValues[i].getBaseValue();
                        break;
                    case minus:
                        accumulator += register;
                        register = -CombinedValues[i].getBaseValue();
                        break;
                    case times:
                        register *= CombinedValues[i].getBaseValue();
                        break;
                    case over:
                        if (CombinedValues[i].getBaseValue() == 0)
                        {
                            throw new InvalidDataException("Division by zero in calculating value of derived variable " + this.VariableName);
                        }
                        register /= CombinedValues[i].getBaseValue();
                        break;
                }
            }
            accumulator += register;
            return accumulator;
        }*/

        /*

        public Dictionary<Variable, string> usedVariables()
        {
            Dictionary<Variable, string> ans = new Dictionary<Variable, string>();
            if (DerivedVariable)
            {
                foreach (DimensionedParameter dp in this.CombinedValues)
                {
                    if (dp.parameter.variable != null)
                    {
                        if (!ans.ContainsKey(dp.parameter.variable))
                        {
                            ans.Add(dp.parameter.variable, "");
                        }
                    }
                }
            }
            return ans;
        }
        */


#endregion

        #region Special Variables, like Iteration #, Iteration Count.

        private bool isSpecialVariable;

        /// <summary>
        /// Special variables are NOT user-editable, and should not be
        /// displayed in the variable editor list. They are computed internally
        /// by Cicero.
        /// </summary>
        public bool IsSpecialVariable
        {
            get { return isSpecialVariable; }
            set { isSpecialVariable = value; }
        }
        public enum SpecialVariableType { IterationNum, IterationCount, RunningCounter };
        private SpecialVariableType mySpecialVariableType;

        public SpecialVariableType MySpecialVariableType
        {
            get { return mySpecialVariableType; }
            set { mySpecialVariableType = value; }
        }


        #endregion




    #region Extensions to dotMath

     /*   /// <summary>
        /// Power function. pow(a, b) will return a^b
        /// </summary>
        public class CPow : dotMath.EqCompiler.CFunction
        {

            private System.Collections.ArrayList values;

            public override string GetFunction()
            {
                return "pow";
            }

            public override dotMath.EqCompiler.CFunction CreateInstance(System.Collections.ArrayList alValues)
            {

                CPow ans = new CPow();
                ans.SetValue(alValues);
                return ans;
            }

            public override void SetValue(System.Collections.ArrayList alValues)
            {
                CheckParms(alValues, 2);
                this.values = alValues;
            }

            public override double GetValue()
            {

                dotMath.EqCompiler.CValue oValue1 = (dotMath.EqCompiler.CValue) values[0];
                dotMath.EqCompiler.CValue oValue2 = (dotMath.EqCompiler.CValue) values[1];

                if (oValue1==null || oValue2 == null) {
                    throw new Exception("Not enough arguments for pow. 2 argument required.");
                }

                return Math.Pow(oValue1.GetValue(), oValue2.GetValue());
            }
        }*/

    #endregion


    }

    



}
