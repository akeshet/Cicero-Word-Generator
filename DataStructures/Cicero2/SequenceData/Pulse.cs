using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Units = DataStructures.Units;
using InvalidDataException = DataStructures.InvalidDataException;


namespace Cicero.DataStructures2
{

    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class Pulse : Cicero2DataObject
    {

		protected override IEnumerable<Cicero.DataStructures2.ResourceID> ReferencedResources_Internal ()
		{
			return new ResourceID[] {
				this.endDelay,
				this.pulseDuration,
				this.startDelay,
				this.ValueVariable
			};
		}

        private bool autoName;

        private string savedUserDefName;
     

        public bool AutoName
        {
            get { return autoName; }
            set
            {
                autoName = value;
                if (value)
                {
                    savedUserDefName = PulseName;    
                    updateAutoName();
                }
                else
                {
                    PulseName = savedUserDefName;
                }
            }
        }

       

        /// <summary>
        /// This function cannot work in this fashion any more. It is here just to avoid compile time errors temporarily.
        /// </summary>
        public void updateAutoName()
        {
            throw new NotImplementedException();
        }

        public void updateAutoName(Cicero2ResourceDictionary resourceDictionary)
        {

            //if the pulse is invalid, autoname should't work. Maybe I call this method twice? Redudant? ASKAVIV
            if (!this.dataValid())
            {
                this.PulseName="Invalid Pulse";
                return;
            }

            if (AutoName)
            {
                string automaticName = "You should never see this";
                string first_half = "";
                string second_half = "";

                //true if simple pulse names will be used
                bool simpleCheck=true;
                //First, we determine if simple pulse names can be used
                if (this.startDelayEnabled | this.endDelayEnabled) 
                    simpleCheck=false;

                //if a simple name is possible, make a simple name
                if (simpleCheck)
                {
                    // set pulse value
                    if (pulseValue)
                        automaticName="H";
                    else
                        automaticName="L";

                    // if the pulse just lasts the duration of the word, then it's name is just "H" or "L", so we return
                    if (this.startCondition == PulseTimingCondition.TimestepStart && this.endCondition == PulseTimingCondition.TimestepEnd)
                    {
                        this.pulseName=automaticName;
                        return;
                    }                    
                    //otherwise, we must have a duration involved (note the case of reversing TimestepStart and TimestepEnd with 
                    //startCondition and endCondition is checked for by dataValid at the beginning of this method

                    //pulse duration
                    automaticName=resourceDictionary.Get(pulseDuration).ToShortString(resourceDictionary)+" "+automaticName;

                    if (this.startCondition == PulseTimingCondition.TimestepStart)
                        automaticName=automaticName+" OnStart";
                    else if (this.startCondition == PulseTimingCondition.TimestepEnd)
                        automaticName=automaticName+" Post";
                    else if (this.endCondition == PulseTimingCondition.TimestepStart)
                        automaticName = automaticName + " Pre";
                    else if (this.endCondition == PulseTimingCondition.TimestepEnd)
                        automaticName = automaticName + " ToEnd";

                    this.pulseName=automaticName;
                    return;//return so that we don't generate a complicated name below
                }
                

                //If we made it this far, then simpleCheck was false and thus we need to make a "complicated" name

                //Name the start condition
                if (this.startCondition == PulseTimingCondition.TimestepStart)
                    first_half = "S";
                else if (this.startCondition == PulseTimingCondition.TimestepEnd)
                    first_half = "E";
                else if (this.startCondition == PulseTimingCondition.Duration)
                    first_half = resourceDictionary.Get(pulseDuration).ToShortString(resourceDictionary) + "_D";

                if (this.startDelayEnabled && this.startCondition != PulseTimingCondition.Duration)
                {
                    if (this.startDelayed)
                        first_half = "d" + first_half;
                    else
                        first_half = "p" + first_half;

                    //could use the ToString function here, but it outputs a space and I don't want that
                    first_half = resourceDictionary.Get(startDelay).ToShortString(resourceDictionary) + "_" + first_half;
                }



                //Name the end condition
                if (this.endCondition == PulseTimingCondition.TimestepStart)
                    second_half = "S";
                else if (this.endCondition == PulseTimingCondition.TimestepEnd)
                    second_half = "E";
                else if (this.endCondition == PulseTimingCondition.Duration)
                    second_half = resourceDictionary.Get(pulseDuration).ToShortString(resourceDictionary) + "_D";

                if (this.endDelayEnabled && this.endCondition != PulseTimingCondition.Duration)
                {
                    if (this.endDelayed)
                        second_half = "d" + second_half;
                    else
                        second_half = "p" + second_half;

                    second_half = resourceDictionary.Get(endDelay).ToShortString(resourceDictionary) + "_" + second_half;
                }

                //add pos or neg pulse label

                automaticName = first_half + ":" + second_half;
                if (this.pulseValue)
                    automaticName= "high::"+automaticName;
                else
                    automaticName="low::"+automaticName;

                pulseName = automaticName;

            }
            // return automaticName;
        }


       /* public static bool Equivalent(Pulse a, Pulse b)
        {
            if (a.endCondition != b.endCondition)
                return false;
            if (!DimensionedParameter.Equivalent(a.endDelay, b.endDelay))
                return false;
            if (a.endDelayed != b.endDelayed)
                return false;
            if (a.endDelayEnabled != b.endDelayEnabled)
                return false;
            if (a.PulseDescription != b.PulseDescription)
                return false;
            if (!DimensionedParameter.Equivalent(a.pulseDuration, b.pulseDuration))
                return false;
            if (a.PulseName != b.PulseName)
                return false;
            if (a.PulseValue != b.PulseValue)
                return false;
            if (a.startCondition != b.startCondition)
                return false;
            if (!DimensionedParameter.Equivalent(a.startDelay ,b.startDelay))
                return false;
            if (a.startDelayed != b.startDelayed)
                return false;
            if (a.startDelayEnabled != b.startDelayEnabled)
                return false;
            if (a.ValueFromVariable != b.ValueFromVariable)
                return false;
            if (a.ValueVariable != b.ValueVariable)
                return false;

            return true;
        }*/

        private bool valueFromVariable;

        public bool ValueFromVariable
        {
            get { return valueFromVariable; }
            set { 
                valueFromVariable = value;
                if (valueFromVariable == false)
                {
                    ValueVariable = ResourceID.Null;
                }

                // TODO: TIMUR
                // Put these auto-name-update hooks into the 
                // settors for other properties in the pulse
                // that might affect the name
                // (pretty much all of them)
                if (AutoName)
                    updateAutoName();
            }
        }


        private ResourceID<Variable> valueVariable;

        public ResourceID<Variable> ValueVariable
        {
            get { return valueVariable; }
            set { valueVariable = value;
            
            if (AutoName)
               updateAutoName();
            }
        }

      /*  public Pulse(Pulse copyMe)
        {
            this.endCondition = copyMe.endCondition;
            this.endDelay = new DimensionedParameter(copyMe.endDelay);
            this.endDelayed = copyMe.endDelayed;
            this.endDelayEnabled = copyMe.endDelayEnabled;
            this.pulseDescription = copyMe.pulseDescription;
            this.pulseDuration = new DimensionedParameter(copyMe.pulseDuration);
            this.pulseName = "Copy of " + copyMe.pulseName;
            this.savedUserDefName = this.pulseName;
            this.pulseValue = copyMe.pulseValue;
            this.startCondition = copyMe.startCondition;
            this.startDelay = new DimensionedParameter(copyMe.startDelay);
            this.startDelayed = copyMe.startDelayed;
            this.startDelayEnabled = copyMe.startDelayEnabled;
            this.autoName = copyMe.autoName;
        }*/

        private string pulseName;

        public string PulseName
        {
            get {
                if (pulseName == null)
                {
                    pulseName = "";
                }
                return pulseName; }
            set { 
                if (!autoName)
                {
                    pulseName = value; 
                }
            
            }
        }
        private string pulseDescription;

        public string PulseDescription
        {
            get {
                if (pulseDescription == null)
                    pulseDescription = "";
                return pulseDescription; 
            }
            set { pulseDescription = value; }
        }

    /*    public Dictionary<Variable, string> usedVariables()
        {
            Dictionary<Variable, string> ans = new Dictionary<Variable, string>();

            if (startDelay.parameter.variable != null)
            {
                ans.Add(startDelay.parameter.variable, "start pretrig/delay.");
            }

            if (endDelay.parameter.variable != null)
            {
                if (!ans.ContainsKey(endDelay.parameter.variable))
                {
                    ans.Add(endDelay.parameter.variable, "end pretrig/delay.");
                }
            }

            if (pulseDuration.parameter.variable != null)
            {
                if (!ans.ContainsKey(pulseDuration.parameter.variable))
                {
                    ans.Add(pulseDuration.parameter.variable, "duration.");
                }
            }

            if (ValueFromVariable)
            {
                if (ValueVariable != null)
                {
                    if (!ans.ContainsKey(ValueVariable))
                    {
                        ans.Add(ValueVariable, "pulse value.");
                    }
                }
            }

            return ans;
        }*/


        public enum PulseTimingCondition { TimestepStart, TimestepEnd, Duration };

        public PulseTimingCondition startCondition;

        public PulseTimingCondition endCondition;

        /// <summary>
        /// startDelay TRUE: start delayed
        /// startDelay FALSE: start in advance
        /// </summary>
        public bool startDelayed;

        public bool startDelayEnabled;

        public ResourceID<DimensionedParameter> startDelay;

        /// <summary>
        /// endDelay TRUE:  end delayed
        /// endDelay FALSE: end in advance
        /// </summary>
        /// 
        public bool endDelayed;

        public ResourceID<DimensionedParameter> endDelay;

        public bool endDelayEnabled;

        public ResourceID<DimensionedParameter> pulseDuration;

        private bool pulseValue;

        public bool getPulseValue(Cicero2ResourceDictionary resources)
        {
                if (!ValueFromVariable)
                {
                    return pulseValue;
                }
                else {
                    if (ValueVariable==ResourceID.Null)
                        return false;
                    if (resources.Get(ValueVariable).VariableValue!=0)
                        return true;
                   return false;
			}
		}
	
	
        public void setPulseValue(bool value) 
		{ 
			pulseValue = value;
            if (AutoName)
                updateAutoName();
		}

        public Pulse(Cicero2ResourceDictionary resourceDictionary)
        {
            this.PulseName = "Unnamed";
            this.savedUserDefName = this.PulseName;
            this.endCondition = PulseTimingCondition.TimestepEnd;
            this.endDelay = resourceDictionary.AddNew(new DimensionedParameter(Units.s, 0));
            this.endDelayed = false;
            this.endDelayEnabled = false;

            this.pulseDuration = resourceDictionary.AddNew(new DimensionedParameter(Units.s, 0));

            this.pulseValue = true;

            this.startCondition = PulseTimingCondition.TimestepStart;
            this.startDelay = resourceDictionary.AddNew(new DimensionedParameter(Units.s, 0));
            this.startDelayed = false;
            this.startDelayEnabled = false;
            
        }


        public override string ToString()
        {
            return PulseName;
        }

        public string dataInvalidUICue()
        {
            if (startCondition == endCondition)
            {
                return "Cannot have same condition for both start and end.";
            }

            if (startCondition == PulseTimingCondition.Duration)
            {
                if (endCondition == PulseTimingCondition.Duration)
                {
                    return "Cannot have duration condition for both start and end.";
                }
            }

            if (startCondition == PulseTimingCondition.TimestepEnd)
            {
                if (endCondition == PulseTimingCondition.TimestepStart)
                {
                    return "Cannot have end before start.";
                }

                if (endCondition == PulseTimingCondition.TimestepEnd)
                {
                    return "Cannot have start and end at the same time.";
                }
            }

            if (startCondition == PulseTimingCondition.TimestepStart)
            {
                if (endCondition == PulseTimingCondition.TimestepStart)
                {
                    return "Cannot have start and end at the same time.";
                }
            }

            

            return null;
        }

        public bool dataValid()
        {
            if (dataInvalidUICue() != null)
                return false;

            return true;
        }

        /// <summary>
        /// Used in calculating buffers in the presence of digital pulses.
        /// </summary>
        public class PulseSampleTimes
        {
            /// <summary>
            /// Sample at which the pulse starts, relative to the beginning of the timestep its in.
            /// </summary>
            public int startSample;
            /// <summary>
            /// Sample at which the pulse ends, relative to the beginning of the timestep its in.
            /// </summary>
            public int endSample;

            /// <summary>
            /// True if startSample is neither 0 nor the number for sample in the sequence timestep.
            /// </summary>
            public bool startRequiresImpingement;

            /// <summary>
            /// True is endSample is neither 0 nor the number of samples in the sequence timestep.
            /// </summary>
            public bool endRequiresImpingement;
        }

   /*     public PulseSampleTimes getPulseSampleTimes(double remainderTime, double sampleDuration, double sequenceTimestepDuration, Cicero2ResourceDictionary resourceDictionary)
        {
            double remainderTimeAtEnd = remainderTime;
            int nSamplesInSequenceTimestep = 0;
            SequenceData.computeNSamplesAndRemainderTime(ref nSamplesInSequenceTimestep, ref remainderTimeAtEnd, sequenceTimestepDuration, sampleDuration);
            return getPulseSampleTimes(nSamplesInSequenceTimestep, sampleDuration);
        }
        */
 
        public PulseSampleTimes getPulseSampleTimes(int nSamplesInSequenceTimestep, double sampleDuration, Cicero2ResourceDictionary resourceDictionary)
        {
            if (!dataValid())
                throw new InvalidDataException("This pulse is invalid.");


            PulseSampleTimes ans = new PulseSampleTimes();

            if (startCondition == PulseTimingCondition.TimestepStart)
            {
                ans.startSample = 0;

                if (startDelayEnabled)
                {
                    int delaySamples = (int)(0.5 + startDelay.getBaseValue(resourceDictionary) / sampleDuration);
                    if (startDelayed) {
                        ans.startSample+=delaySamples;
                    }
                    else {
                        ans.startSample-=delaySamples;
                    }
                }
            }

            if (startCondition == PulseTimingCondition.TimestepEnd)
            {
                ans.startSample = nSamplesInSequenceTimestep;
                if (startDelayEnabled)
                {
                    int delaySamples = (int)(0.5 + resourceDictionary.Get(startDelay).getBaseValue(resourceDictionary) / sampleDuration);
                    if (startDelayed) {
                        ans.startSample+=delaySamples;
                    }
                    else {
                        ans.startSample-=delaySamples;
                    }
                }
            }

            if (endCondition == PulseTimingCondition.TimestepStart)
            {
                ans.endSample = 0;

                if (endDelayEnabled)
                {
                    int delaySamples = (int)(0.5 + resourceDictionary.Get(endDelay).getBaseValue(resourceDictionary) / sampleDuration);
                    if (endDelayed) {
                        ans.endSample+=delaySamples;
                    }
                    else {
                        ans.endSample-=delaySamples;
                    }
                }
            }

            if (endCondition == PulseTimingCondition.TimestepEnd)
            {
                ans.endSample = nSamplesInSequenceTimestep;

                if (endDelayEnabled)
                {
                    int delaySamples = (int)(0.5 + endDelay.getBaseValue(resourceDictionary) / sampleDuration);
                    if (endDelayed) {
                        ans.endSample+=delaySamples;
                    }
                    else {
                        ans.endSample-=delaySamples;
                    }
                }
            }

            if (endCondition == PulseTimingCondition.Duration)
            {
                ans.endSample = ans.startSample + (int)(0.5 + pulseDuration.getBaseValue(resourceDictionary) / sampleDuration);
            }

            if (startCondition == PulseTimingCondition.Duration)
            {
                ans.startSample = ans.endSample - (int)(0.5 + pulseDuration.getBaseValue(resourceDictionary) / sampleDuration);
            }

            if (ans.startSample!=0 && ans.startSample!=nSamplesInSequenceTimestep) {
                ans.startRequiresImpingement = true;
            }

            if (ans.endSample!=0 && ans.endSample!=nSamplesInSequenceTimestep) {
                ans.endRequiresImpingement = true;
            }

            return ans;
        }
    }
}
