using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using EnumWrapperTypeConverter = DataStructures.EnumWrapperTypeConverter;
using Units = DataStructures.Units;
using InterpolationException = DataStructures.InterpolationException;

namespace Cicero.DataStructures2
{
    /// <summary>
    /// This class defines a waveform, as used in Analog Groups, some gpibGroups, etc.
    /// </summary>
    [Serializable

    ,TypeConverter(typeof(ExpandableObjectConverter))
    // *** NOTE THE ABOVE LINE WAS ADDED Feb 11 2010. I don't know why it was missing in earlier revs,
    // and I am thus slightly suspicious that I may have taken it out as a workaround to some bug in .NET remoting.
    // SO if some weird bug in .NET remoting re-appears, I may have to remove above line.

    ]
	public class Waveform : Cicero2DataObject
	{

		protected override IEnumerable<Cicero.DataStructures2.ResourceID> ReferencedResources_Internal ()
		{
			List<ResourceID> ans = new List<Cicero.DataStructures2.ResourceID>();
			ans.AddRange(this.ExtraParameters.DownCast());
			ans.AddRange(this.XValues.DownCast());
			ans.AddRange(this.YValues.DownCast());
			ans.Add(this.WaveformDuration);
			return ans;
		}

        /*public void copyWaveform(Waveform copyMe)
        {
            if (copyMe.combiners!=null)
                this.combiners = new List<InterpolationType.CombinationOperators>(copyMe.combiners);
            else
                this.combiners = null;

            this.dataFileName = copyMe.dataFileName;
            this.dataFromFile = copyMe.dataFromFile;
            this.equationString = copyMe.equationString;

            if (copyMe.extraParameters != null)
                this.extraParameters = new List<DimensionedParameter>(copyMe.extraParameters);
            else
                this.extraParameters = null;

            this.myInterpolationType = copyMe.myInterpolationType;

            if (copyMe.referencedWaveforms != null)
                this.referencedWaveforms = new List<Waveform>(copyMe.referencedWaveforms);
            else
                this.referencedWaveforms = null;

            this.waveformDuration = new DimensionedParameter(copyMe.waveformDuration);

            this.waveformName = copyMe.waveformName;

            if (copyMe.xValues != null)
                this.xValues = new List<DimensionedParameter>(copyMe.xValues);
            else
                this.xValues = null;

            this.YUnits = copyMe.YUnits;

            if (copyMe.yValues != null)
                this.yValues = new List<DimensionedParameter>(copyMe.yValues);
            else
                this.yValues = null;

        }*/

		#region InterpolationType

        /// <summary>
        /// Edit this class to create new interpolation types. Stores all the names of interpolation types,
        /// the names and dimensions of their parameters, and the code to generate interpolations.
        /// </summary>
        [Serializable, TypeConverter(typeof (InterpolationType.InterpolationTypeConvertor))]
		public struct InterpolationType
        {

            #region InterpolationTypeConvertor
            /// <summary>
            /// This type converter allows interpolation types to be selectable form a combobox in propertygrids.
            /// </summary>
            public class InterpolationTypeConvertor : EnumWrapperTypeConverter
            {

                public InterpolationTypeConvertor() : base(InterpolationType.allInterpolationTypes)
                {
                }
            }
            #endregion

           

            private enum InterpolationID { Linear, Step, Exponential, Sinusoidal, TwoPointCubicSpline, Combination, Equation };
			private InterpolationID myID;

			private static readonly string [] InterpolationTypeString = 
			{"Piecewise Linear", "Step", "Exponential", "Sinusoidal", "Two Point Cubic Spline", "Combine Waveforms", "Equation"};

			public override string ToString()
			{
				return InterpolationTypeString[(int)myID];
			}

			/// <summary>
			/// Specifies whether, the corresponding interpolation type uses "normal" X-Y point parameters.
			/// </summary>
			private static readonly bool[] XYParametersEnabled = { true, true, false, false, true, false, false };

			/// <summary>
			/// True if x-y parameters are used in this interpolation type.
			/// </summary>
			public bool xyParametersEnabled
			{
				get
				{
					return XYParametersEnabled[(int)myID];
				}
			}

            /// <summary>
            /// Specifies whether the data for the corresponding interpolation type can be read from a file.
            /// REO 10/2008
            /// </summary>
            private static readonly bool[] CanReadDataFromFile = { true, true, false, false, true, false, false };

            /// <summary>
            /// True if data from this interpolation type can be read from a file.
            /// REO 10/2008
            /// </summary> 
            public bool canReadDataFromFile
            {
                get
                {
                    return CanReadDataFromFile[(int)myID];
                }
            }

            /// <summary>
			/// Specifies whether, for the corresponding interpolataion type, it the 
			/// </summary>
			private static readonly bool[] NumberOfXYParametersAreFixed = { false, false, true, true, true, true, true };

			/// <summary>
			/// True if the number of x-y parameters is must be a given fixed value.
			/// </summary>
			public bool xyParametersFixed
			{
				get
				{
					return NumberOfXYParametersAreFixed[(int)myID];
				}
			}

			private static readonly int[] FixedNumberOfXYParameters = {-1, -1, 0, 0, 2, 0, 0};

			/// <summary>
			/// Fixed number of x-y parameters if this interpolation type has a fixed number. -1 otherwise.
			/// </summary>
			public int xyParametersCount
			{
				get
				{
					return FixedNumberOfXYParameters[(int)myID];
				}
			}

			private static readonly bool[] ExtraParametersEnabled = { false, false, true, true, false, false, false };

			/// <summary>
			/// True if this interpolation type requires extra specific parameters. Only applies to numerical
            /// extra parameters. Equation extra parameters handled separately.
			/// </summary>
			public bool extraParametersEnabled
			{
				get
				{
					return ExtraParametersEnabled[(int)myID];
				}
			}

            public static readonly bool[] EquationParameterEnabled = { false, false, false, false, false, false, true };

            public bool equationParameterEnabled
            {
                get
                {
                    return EquationParameterEnabled[(int)myID];
                }
            }

			/// <summary>
			/// Names of additional InterpolationType-specific parameters
			/// </summary>
            private static readonly string[][] ExtraParameterNames = {
				new string [] {},                                                 // Linear
				new string [] {},                                                 // Step
				new string [] {"StartValue", "EndValue", "Tau", "StartTime", "EndTime"},     // Exponential
				new string [] {"Offset", "Amplitude", "Frequency", "Phase"},      // Sinusoidal
				new string [] {},                                                    // Spline
                new string [] {},                                                  // Combine
                new string [] {}                                                    // Equation
			};

			/// <summary>
			/// Names of extra parameters.
			/// </summary>
			public string[] extraParametersNames
			{
				get {
					return ExtraParameterNames[(int)myID];
				}
			}
			/// <summary>
			/// Number of extra parameters.
			/// </summary>
			public int extraParametersCount
			{
				get
				{
					return ExtraParameterNames[(int)myID].GetLength(0);
				}
			}

            /// <summary>
            /// Specifies which interpolation types make reference to other waveforms. For now, this is only true
            /// of "Combination
            /// </summary>
            private static readonly bool[] ReferencesOtherWaveforms = {
                false, // Linear
                false, // Step
                false, // Exponential
                false, // Sinusoidal
                false, // Spline
                true,   // Combine
                false // Equation
            };

            /// <summary>
            /// Whether this interpolation type makes use of a list of other waveforms. This is currently only true
            /// of the Combination type.
            /// </summary>
            public bool referencesOtherWaveforms
            {
                get
                {
                    return ReferencesOtherWaveforms[(int)myID];
                }
            }

            public enum CombinationOperators {Plus, Minus, Times, Then};

            public static readonly CombinationOperators[] allCombinationOperators = {CombinationOperators.Plus,
                CombinationOperators.Minus, CombinationOperators.Times, CombinationOperators.Then
            };

			/// <summary>
			/// Dimension type of the extra parameters for corresponding interpolation types. Note, these are actually
            /// over-riden by the bool array below, which specifies which of the extra dimensions have their dimension derived
            /// from the y-axis dimension.
			/// </summary>
            private static readonly Units.Dimension[][] ExtraParameterDimensions = {
				new Units.Dimension[] {},
				new Units.Dimension[] {},
				new Units.Dimension[] {Units.Dimension.V, Units.Dimension.V, Units.Dimension.s, Units.Dimension.s, Units.Dimension.s},
				new Units.Dimension[] {Units.Dimension.V, Units.Dimension.V, Units.Dimension.Hz, Units.Dimension.Degrees},
				new Units.Dimension[] {},
                new Units.Dimension[] {}, // Combine
                new Units.Dimension[] {} // Equation
			};

            /// <summary>
            /// Specifies whether an extra parameters dimensions are, instead of specified in the code, are instead determined
            /// by the dimensions of the y-axis.
            /// </summary>
            private static readonly bool[][] ExtraParameterDimensionUseYAxis = {
                new bool [] {},
                new bool [] {},
                new bool [] {true, true, false, false, false},
                new bool [] {true, true, false, false},
                new bool [] {},
                new bool [] {}, // Combine
                new bool [] {} // Equation
            };

            

			/// <summary>
			/// Dimensions of the extra parameters for this interpolation type, where yAxisDimension is the dimension
            /// of the y axis of the waveform (usually Volts, but in some cases could be Hz. Or maybe even amps?)
			/// </summary>
			public Units.Dimension [] extraParameterDimensionsForNonstandardYAxis (Units.Dimension yAxisDimension)
			{
                if (this.extraParametersCount==0)
                    return null;
                Units.Dimension[] answer = new Units.Dimension[this.extraParametersCount];
                for (int i=0; i<this.extraParametersCount; i++) {
                   if (ExtraParameterDimensionUseYAxis[(int)myID][i])
                       answer[i] = yAxisDimension;
                   else
                       answer[i] = ExtraParameterDimensions[(int)myID][i];
                }
                return answer;
			}

            /// <summary>
            /// This returns the list of extra parameter dimensions, that the "y axis" dimension of the waveform is Volts.
            /// </summary>
            /// <returns></returns>
            public Units.Dimension[] extraParametersDimensions
            {
                get
                {
                    return ExtraParameterDimensions[(int)myID];
                }
            }

			/// <summary>
			/// A Helper string to be displayed either under the interpolation type selector, or
			/// maybe if the user clicks a help button.
			/// </summary>
			private static readonly string[] UserHelpString = { 
				"Linear interpolation between specified time-value pairs.", // Linear
				"Step interpolation between specified time-value points. Value holds at last specified value until the next value.", // Step
				"Write me, I have no idea.", // Exponential
				"Write me.", // 
				"Write me.",
                "Combination waveforms, sequentially or though arithmetic operators.",
                "Specify waveform behavior with an equation."
			};



			public static readonly InterpolationType Linear = new InterpolationType(InterpolationID.Linear);
			public static readonly InterpolationType Step = new InterpolationType(InterpolationID.Step);
			public static readonly InterpolationType Exponential = new InterpolationType(InterpolationID.Exponential);
			public static readonly InterpolationType Sinusoidal = new InterpolationType(InterpolationID.Sinusoidal);
			public static readonly InterpolationType TwoPointCubicSpline = new InterpolationType(InterpolationID.TwoPointCubicSpline);
            public static readonly InterpolationType Combination = new InterpolationType(InterpolationID.Combination);
            public static readonly InterpolationType Equation = new InterpolationType(InterpolationID.Equation);

			public static readonly InterpolationType[] allInterpolationTypes = 
				{ Linear, Step, Exponential, Sinusoidal, TwoPointCubicSpline, Combination, Equation };

			#region Actual Interpolation Code. Here be dragons.

            /// <summary>
            /// Gets the waveforms value at a specified time.
            /// </summary>
            /// <param name="time"></param>
            /// <param name="xValues"></param>
            /// <param name="yValues"></param>
            /// <param name="extraValues"></param>
            /// <param name="referencedWaveforms"></param>
            /// <param name="combiners"></param>
            /// <returns></returns>
            public double getValueAtTime(double time, double wfDuration, double[] xValues, double[] yValues, double[] extraValues, List<Waveform> referencedWaveforms, List<CombinationOperators> combiners, string equationString, List<Variable> existingVariables, List<Waveform> existingCommonWaveforms,
			                             Cicero2ResourceDictionary resources)
            {

                double[] arr = new double[1];
                this.getInterpolation(1, time, time, wfDuration, xValues, yValues, extraValues, referencedWaveforms, combiners, arr, 0, equationString, existingVariables, existingCommonWaveforms, resources);
                return arr[0];

            }

            /// <summary>
            /// Returns a double array with an interpolation.
            /// </summary>
            /// <param name="nSamples">Size of the returned array.</param>
            /// <param name="startTime">Time value corresponding to the start of the array.</param>
            /// <param name="endTime">Time value corresponding to the end of the array.</param>
            /// <param name="xValues">X data points.</param>
            /// <param name="yValues">Y data points.</param>
            /// <param name="extraValues">Extra parameters.</param>
            /// <param name="referencedWaveforms">List of other referenced waveforms, used only in those interpolation types that support it.</param>
            /// <param name="combiners">List of waveform combiners, used only in comining interpolation types.</param>
            /// <param name="usingManualValues">Bool specifying whether using "manual" or "variable" values. True for manual.</param>
            /// <param name="variableValues">Double array of variable values. Used only if usingManualValues is false, and if this is a combination type interpolation.</param>
            /// <returns></returns>
			public void getInterpolation(int nSamples, 
			                             double startTime, 
			                             double endTime, 
			                             double wfDuration , 
			                             double [] xValues, 
			                             double [] yValues, 
			                             double [] extraValues, 
			                             List<Waveform> referencedWaveforms, 
			                             List<CombinationOperators> combiners, 
			                             double [] output, 
			                             int startIndex, 
			                             string equationString, 
			                             List<Variable> existingVariables, 
			                             List<Waveform> existingCommonWaveforms,
			                             Cicero2ResourceDictionary resources) {
                if (startTime > endTime)
                    startTime = endTime;
 
                int numExtras;
                if (extraValues == null)
                    numExtras = 0;
                else numExtras = extraValues.Length;

                if (numExtras < extraParametersCount) 
                    throw new InterpolationException("Interpolation Exception: insufficient number of parameters provided for interpolation type " + this.ToString());


             /*   double [] ans;
				try {
					ans = new double[nSamples];
				}
				catch (Exception e) {
					throw new InterpolationException("Interpolation Exception: unable to allocate " +nSamples.ToString() + " length array. Reason: " + e.Message);
				}
                */

				switch (myID) {
						// Linear Interpolation
					case InterpolationID.Linear:
						{

                            if (xValues.Length == 0)
                                return;

							int nextInterpolationPoint = 0;
							double x;
							double stepSize = (endTime - startTime) / ((double)nSamples);

							int i;
							for (i = 0; (i < nSamples); i++)
							{
                                x = startTime + stepSize * i;
                                if (x > wfDuration)
                                    x = wfDuration;

								// increment nextInterpolationPoint if necessary, until it is after time x
								while ((nextInterpolationPoint < xValues.Length) && (xValues[nextInterpolationPoint] <= x))
									nextInterpolationPoint++;

								// nextInterpolationPoint is after the last data point, so hold interpolation flat
								if (nextInterpolationPoint == xValues.Length)
								{
									output[i+startIndex] = yValues[xValues.Length - 1];
									continue;
								}

								// linear interpolation between nextInterpolationPoint and previous point
								if (nextInterpolationPoint > 0)
								{
									double prevX = xValues[nextInterpolationPoint - 1];
									double prevY = yValues[nextInterpolationPoint - 1];
									double nextX = xValues[nextInterpolationPoint];
									double nextY = yValues[nextInterpolationPoint];
									output[i+startIndex] = prevY + (nextY - prevY) / (nextX - prevX) * (x - prevX);
								}
							}
						}
						break;

						//Step Interpolation
					case InterpolationID.Step:
						{

                            if (xValues.Length == 0)
                                return;

							int nextInterpolationPoint = 0;
							double x;
							double stepSize = (endTime - startTime) / ((double)nSamples);

							int i;
							for (i = 0; (i < nSamples); i++)
							{
                                x = startTime + stepSize * i;
                                if (x > wfDuration)
                                    x = wfDuration;

								// increment nextInterpolationPoint if necessary, until it is after time x
								while ((nextInterpolationPoint < xValues.Length) && (xValues[nextInterpolationPoint] <= x))
									nextInterpolationPoint++;

								// nextInterpolationPoint is after the last data point, so hold interpolation flat
								if (nextInterpolationPoint == xValues.Length)
								{
									output[i+startIndex] = yValues[xValues.Length - 1];
									continue;
								}

								// step interpolation between nextInterpolationPoint and previous point
								if (nextInterpolationPoint > 0)
								{

									double prevY = yValues[nextInterpolationPoint - 1];
									output[i+startIndex] = prevY;
								}
							}
						}
						break;

						// Exponential Interpolation
					case InterpolationID.Exponential:
                        {
                            double low = extraValues[0];
                            double high = extraValues[1];
                            double tau = extraValues[2];
                            double rampStart = extraValues[3];
                            double rampEnd = extraValues[4];

                            double rampDuration = rampEnd - rampStart;
                            double stepSize = (endTime - startTime) / ((double) nSamples);

                            // the following voodoo is from widagdo's code. I assume it works.

                            double ert;
                            double A, B, C, x;

                            if (tau > 0)
                            {
                                if (tau < 1e-6) tau = 1e-6;
                                ert = Math.Exp(rampDuration / tau);
                                A = (high - low) / (ert - 1);
                                B = 1 / tau;
                                C = (high / ert - low) / (1 / ert - 1);
                            }
                            else
                            {
                                if (tau > -1e-6) tau = -1e-6;
                                ert = Math.Exp(rampDuration / tau);
                                A = (high - low) / (ert - 1);
                                B = 1 / tau;
                                C = (high - ert * low) / (1 - ert);
                            }

                            for (int i = 0; i < nSamples; i++)
                            {
                                x = startTime + stepSize * i;
                                if (x > wfDuration)
                                    x = wfDuration;

                                if (x < rampStart)
                                    output[i+startIndex] = low;
                                else if (x > rampEnd)
                                    output[i+startIndex] = high;
                                else
                                    output[i+startIndex] = A * Math.Exp(B * (x - rampStart)) + C;
                            }
						}
						break;

						// Sinusoidal Interpolation
                    case InterpolationID.Sinusoidal:
                        {
                            double offset = extraValues[0];
                            double amplitude = extraValues[1];
                            double frequency = extraValues[2];
                            double phase = extraValues[3];

                            double x;
                            double stepSize = (endTime - startTime) / ((double)nSamples);
                            for (int i = 0; i < nSamples; i++)
                            {
                                x = startTime + stepSize * i;
                                if (x >= wfDuration)
                                    x = wfDuration;

                                output[i + startIndex] = offset + amplitude * Math.Sin(x * 2.0 * Math.PI * frequency + Math.PI * phase / 180.0);

                            }
                        }
                        break;

						// Spline Interpolation
                    case InterpolationID.TwoPointCubicSpline:
                        {
                            // I have no idea how a two point cubic spline works and I don't care to find out
                            // but this is a port of widagdo's code

                            if (xValues.Length<2||yValues.Length<2)
                                throw new InterpolationException("Interpolation Exception: 2 x-y points needed for a two point cubic spline.");


                            double low = yValues[0];
                            double high = yValues[1];
                            double splineStart = xValues[0];
                            double splineEnd = xValues[1];
                            double splineDuration = splineEnd - splineStart;
                            double x = startTime;
                            double stepSize = (endTime - startTime) / ((double)nSamples);

                            for (int i = 0; i < nSamples; i++)
                            {
                                x = startTime + stepSize * i;
                                if (x > wfDuration)
                                    x = wfDuration;

                                if (x < splineStart)
                                    output[i + startIndex] = low;
                                else if (x > splineEnd)
                                    output[i + startIndex] = high;
                                else
                                {
                                    double t = (x - splineStart) / splineDuration;
                                    double omt = 1 - t;
                                    double val = omt * omt * omt * low + 3 * t * omt * omt * low + 3 * t * t * omt * high + t * t * t * high;
                                    output[i+startIndex] = val;
                                }
                            }

                        }
                        break;

                        // Comination of waveforms
                    case InterpolationID.Combination: 
                        {
							throw new InterpolationException("Combination interpolation is deprechated.");
                        }
                        break;

                    case InterpolationID.Equation:
                        {
                            WaveformEquationInterpolator.getEquationInterpolation(equationString, startTime, endTime, nSamples, startIndex, output, existingVariables, existingCommonWaveforms, wfDuration, resources);
                        }
                        break;
				}

				return;
            }
            #region Combination Interpolater
            private void combinationInterpolation(int nSamples, double startTime, double endTime, List<Waveform> referencedWaveforms, List<CombinationOperators> combiners, double [] output, int startIndex, List<Variable> existingVariables, List<Waveform> existingCommonWaveforms,
			                                      Cicero2ResourceDictionary resources)
            {
				throw new NotImplementedException("Combination interpolation is being deprecated. Use equation interpolation instead.");
            }

            #endregion

            #endregion

            private InterpolationType(InterpolationID id)
			{
				this.myID = id;
			}

            /// <summary>
            /// This is an implicit typecast from InterpolationType to int.
            /// </summary>
            /// <param name="mul"></param>
            /// <returns></returns>
            static public implicit operator int(InterpolationType type)
            {
                return (int)type.myID;
            }

		}


		#endregion

		private InterpolationType myInterpolationType;

        [Description("The interpolation type of this waveform."), Category("Global")]
        public InterpolationType interpolationType {
            get {
                return myInterpolationType;
            }
            private set {
				myInterpolationType = value;
                //setInterpolationType(value);
            }
        }

        private string equationString;

        public string EquationString
        {
            get
            {
                if (equationString == null)
                    equationString = "";

                return equationString;
            }
            set { equationString = value; }
        }

        public void setToSpline(double duration, double startvalue, double endvalue, Cicero2ResourceDictionary resources)
        {
            this.setToLinearRamp(duration, startvalue, endvalue, resources);
            this.myInterpolationType = InterpolationType.TwoPointCubicSpline;
        }

        /// <summary>
        /// To be used only for programmatically creating waveforms. Not to be intermixed with UI-creation of waveforms.
        /// (not guaranteed to preserve references to DimensionedParameters).
        /// </summary>
        /// <param name="frequency"></param>
        /// <param name="amplitude"></param>
        /// <param name="offset"></param>
        /// <param name="phase"></param>
        public void setToSinusoidal(double frequency, double amplitude, double offset, double phase, double duration, Cicero2ResourceDictionary resources)
        {

            this.interpolationType = InterpolationType.Sinusoidal;

         
          // extra parameters for sinusoidal interpolation: {"Offset", "Amplitude", "Frequency", "Phase"},      // Sinusoidal

            extraParameters[0].forceToManualValue(offset, Units.V, resources);
            extraParameters[1].forceToManualValue(amplitude, Units.V, resources);
            extraParameters[2].forceToManualValue(frequency, Units.Hz, resources);
            extraParameters[3].forceToManualValue(phase, Units.deg, resources);

            waveformDuration.forceToManualValue(duration, Units.s, resources);
        }

        /// <summary>
        /// To be used only for programmatically creating waveforms. Not to be intermixed with UI-creation of waveforms.
        /// (not guaranteed to preserve references to DimensionedParameters).
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="startvalue"></param>
        /// <param name="endvalue"></param>
        public void setToLinearRamp(double duration, double startvalue, double endvalue, Cicero2ResourceDictionary resources)
        {
            this.interpolationType = InterpolationType.Linear;

            xValues.Clear();
            yValues.Clear();
            xValues.Add(resources.AddNew(new DimensionedParameter(Units.s, 0)));
            xValues.Add(resources.AddNew(new DimensionedParameter(Units.s, duration)));
            yValues.Add(resources.AddNew(new DimensionedParameter(Units.V, startvalue)));
            yValues.Add(resources.AddNew(new DimensionedParameter(Units.V, endvalue)));

            waveformDuration.forceToManualValue(duration, Units.s, resources);

        }

        private void setInterpolationType(InterpolationType value, Cicero2ResourceDictionary resources)
        {

            myInterpolationType = value;

            // ensure that my parameter lists are long enough if necessary

            if (myInterpolationType.extraParametersEnabled)
            {
                if (extraParameters == null)
                    extraParameters = new List<ResourceID<DimensionedParameter>>();
                extraParameters.Clear();

                // expand list of extra parameters as necessary
                for (int i = 0; i < myInterpolationType.extraParametersCount; i++)
                {
                    extraParameters.Add(
						resources.AddNew(new DimensionedParameter(myInterpolationType.extraParameterDimensionsForNonstandardYAxis(YUnits)[i])));
                }
            }

            if (myInterpolationType.xyParametersEnabled)
            {
                if (xValues == null)
                {
                    xValues = new List<ResourceID<DimensionedParameter>>();
                    yValues = new List<ResourceID<DimensionedParameter>>();
                }
                if (myInterpolationType.xyParametersFixed)
                {
                    for (int i = xValues.Count; i < myInterpolationType.xyParametersCount; i++)
                    {
                        xValues.Add(resources.AddNew(new DimensionedParameter(Units.Dimension.s)));
                        yValues.Add(resources.AddNew(new DimensionedParameter(YUnits)));
                        resources.Get(xValues[i]).parameter.ManualValue = i;
                    }
                }
            }

            if (myInterpolationType.referencesOtherWaveforms)
            {
                if (combiners == null)
                {
                    combiners = new List<InterpolationType.CombinationOperators>();
                }
                if (referencedWaveforms == null)
                {
                    referencedWaveforms = new List<Waveform>();
                    referencedWaveforms.Add(null);
                }
            }
        }


        private string waveformName;

        [Description("The name of this waveform"), Category("Global")]
        public string WaveformName
        {
            get { return waveformName; }
            set { waveformName = value; }
        }

        private List<ResourceID<DimensionedParameter>> xValues;

        [Description("X value parameters."), Category("Parameters")]
        public List<ResourceID<DimensionedParameter>> XValues
        {
            get { return xValues; }
            set { xValues = value; }
        }

        private List<ResourceID<DimensionedParameter>> yValues;

        [Description("Y value parameters."), Category("Parameters")]
        public List<ResourceID<DimensionedParameter>> YValues
        {
            get { return yValues; }
            set { yValues = value; }
        }
        private List<ResourceID<DimensionedParameter>> extraParameters;

        [Description("Extra parameters."), Category("Parameters")]
        public List<ResourceID<DimensionedParameter>> ExtraParameters
        {
            get
            {
                if (extraParameters == null)
                    extraParameters = new List<ResourceID<DimensionedParameter>>();
                return extraParameters;
            }
            set { extraParameters = value; }
        }

        /// <summary>
        /// Here we keep track of whether the data for this waveform was loaded from a file.
        /// REO 10/2008
        /// </summary>
        private bool dataFromFile = false;

        [Description("Data from file."), Category("Parameters")]
        public bool DataFromFile
        {
            get { return dataFromFile; }
            set
            {
                dataFromFile = (myInterpolationType.canReadDataFromFile) ? value : false;
            }
        }

        /// <summary>
        /// The name of the file the data was loaded from.
        /// REO 10/2008
        /// </summary>
        private string dataFileName;

        [Description("Data file name."), Category("Parameters")]
        public string DataFileName
        {
            get { return dataFileName; }
            set { dataFileName = value; }
        }

        /// <summary>
        /// List of waveforms to be combined through combiners. Used only for interpolations types
        /// with the interpolationType.referencesOtherWaveforms bool set to true.
        /// 
        /// After adding waveforms to this list, make sure to call isWaveformRecursive() to make sure that 
        /// you haven't created a waveform loop. Otherwise, waveform will throw an exception when you try to 
        /// interpolate it.
        /// 
        /// It is safe to set any of these  list items to "null". Null waveforms will be treated as zero-valued waveforms with zero duration.
        /// </summary>
        private List<Waveform> referencedWaveforms;

        [Description("Other referenced waveforms, if any, used in a combination interpolation."), Category("Parameters")]
        public List<Waveform> ReferencedWaveforms
        {
            get
            {
                if (referencedWaveforms == null)
                    referencedWaveforms = new List<Waveform>();
                return referencedWaveforms;
            }
            set { referencedWaveforms = value; }
        }

        private List<InterpolationType.CombinationOperators> combiners;

        [Description("Combination operators used, if any, in a combination interpolation."), Category("Parameters")]
        public List<InterpolationType.CombinationOperators> WaveformCombiners
        {
            get
            {
                if (combiners == null)
                    combiners = new List<InterpolationType.CombinationOperators>();
                return combiners;
            }
            set { combiners = value; }
        }

		private ResourceID<DimensionedParameter> waveformDuration;

        [Description("The duration of this waveform."), Category("Parameters.")]
        public ResourceID<DimensionedParameter> WaveformDuration
        {
            get { return waveformDuration; }
            set { waveformDuration = value; }
        }


  /*      /// <summary>
        /// Returns a double array of length nSamples with an interpolation of the
        /// waveform, using the waveform's parameters manual values.
        /// </summary>
        /// <param name="nSamples"></param>
        /// <returns></returns>
        public double[] getManualInterpolation(int nSamples)
        {
            return this.getManualInterpolation(nSamples, 0, waveformDuration.getManualBaseValue());
        }

        /// <summary>
        /// Returns a double array of length nSamples with an interpolation of the
        /// waveform, using the waveform's parameters manual values.
        /// </summary>
        /// <param name="nSamples"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public double[] getManualInterpolation(int nSamples, double startTime, double endTime)
        {
            if (interpolationType.referencesOtherWaveforms && this.isWaveformReferenceRecursive()) throw new InterpolationException("Interpolation Exception: Attempted to interpolate a recursive wavefunction.");

            return this.interpolationType.getInterpolation(
                nSamples,
                startTime,
                endTime,
                DimensionedParameter.getManualBaseValues(xValues),
                DimensionedParameter.getManualBaseValues(yValues),
                DimensionedParameter.getManualBaseValues(extraParameters),
                referencedWaveforms,
                combiners);
        }

   * */

        /// <summary>
        /// Returns a double array of length nSamples with an interpolation of the
        /// waveform, using the waveform's parameters values.
        /// </summary>
        /// <param name="nSamples"></param>
        /// <returns></returns>
        public double[] getInterpolation(int nSamples, List<Variable> existingVariables, List<Waveform> existingCommonWaveforms, Cicero2ResourceDictionary resources)
        {
            return this.getInterpolation(nSamples, 0, waveformDuration.getBaseValue(resources), existingVariables, existingCommonWaveforms, resources);
        }

        public void getInterpolation(int nSamples, double[] output, int startIndex, List<Variable> existingVariables, List<Waveform> existingCommonWaveforms, Cicero2ResourceDictionary resources)
        {
            this.getInterpolation(nSamples, 0, waveformDuration.getBaseValue(resources), output, startIndex, existingVariables, existingCommonWaveforms, resources);
        }

        public double getValueAtTime(double time, List<Variable> existingVariables, List<Waveform> existingCommonWaveforms, Cicero2ResourceDictionary resources)
        {
            return this.interpolationType.getValueAtTime(time,
                waveformDuration.getBaseValue(resources),
                DimensionedParameter.getBaseValues(xValues, resources),
                DimensionedParameter.getBaseValues(yValues, resources),
                DimensionedParameter.getBaseValues(extraParameters, resources),
                referencedWaveforms,
                combiners,
                EquationString,
                existingVariables,
                existingCommonWaveforms, resources);
        }

        public void getInterpolation(int nSamples, double startTime, double endTime, double [] output, int startIndex, List<Variable> existingVariables, List<Waveform> existingCommonWaveforms, Cicero2ResourceDictionary resources)
        {
            if (interpolationType.referencesOtherWaveforms && this.isWaveformReferenceRecursive()) throw new InterpolationException("Interpolation Exception: Attempted to interpolate a recursive wavefunction.");

            this.interpolationType.getInterpolation(
                nSamples,
                startTime,
                endTime,
                waveformDuration.getBaseValue(resources),
                DimensionedParameter.getBaseValues(xValues, resources),
                DimensionedParameter.getBaseValues(yValues, resources),
                DimensionedParameter.getBaseValues(extraParameters, resources),
                referencedWaveforms,
                combiners,
                output,
                startIndex,
                EquationString,
                existingVariables,
                existingCommonWaveforms,
				resources);
        }

        public double [] getInterpolation(int nSamples, double startTime, double endTime, List<Variable> existingVariables, List<Waveform> existingCommonWaveforms, Cicero2ResourceDictionary resources)
        {
            if (nSamples > 0)
            {
                double[] ans = new double[nSamples];
                this.getInterpolation(nSamples, startTime, endTime, ans, 0, existingVariables, existingCommonWaveforms, resources);
                return ans;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns true if the referenced waveforms somehow form a recursive loop, which is not allowed.
        /// This method traverses through all referenced waveforms and attempts to see if any referenced waveforms eventually
        /// point back to a previously referenced one.
        /// </summary>
        /// <returns></returns>
        public bool isWaveformReferenceRecursive()
        {
            if (referencedWaveforms==null) return false;
            if (referencedWaveforms.Count == 0) return false;
            if (myInterpolationType != InterpolationType.Combination) return false;

            List<Waveform> callingStackWaveforms = new List<Waveform>();
            callingStackWaveforms.Add(this);
            return this.waveformHunt(callingStackWaveforms);
        }

        public Waveform(string waveformName, Cicero2ResourceDictionary resources) : 
			this(resources)
        {
            this.waveformName = waveformName;
        }

        /*public Waveform(Waveform copyMe)
        {
            if (copyMe.combiners != null)
                this.combiners = new List<InterpolationType.CombinationOperators>(copyMe.combiners);
            if (copyMe.extraParameters != null)
                this.extraParameters = new List<DimensionedParameter>(copyMe.extraParameters);

            this.myInterpolationType = copyMe.myInterpolationType;

            if (copyMe.referencedWaveforms != null)
                this.referencedWaveforms = new List<Waveform>(copyMe.referencedWaveforms);
            if (copyMe.waveformDuration != null)
                this.waveformDuration = new DimensionedParameter(copyMe.waveformDuration);
            if (copyMe.waveformName != null)
                this.waveformName = copyMe.waveformName;
            if (copyMe.xValues != null)
                this.xValues = new List<DimensionedParameter>(copyMe.xValues);
            if (copyMe.yValues != null)
                this.yValues = new List<DimensionedParameter>(copyMe.yValues);

            this.YUnits = copyMe.YUnits;

            this.equationString = copyMe.equationString;
            this.dataFileName = copyMe.dataFileName;
            this.dataFromFile = copyMe.dataFromFile;

        }*/

        public Waveform(Cicero2ResourceDictionary resources)
        {
            setInterpolationType(InterpolationType.Linear, resources);
        }


        public Units.Dimension YUnits = Units.Dimension.V;

        public Waveform(Units.Dimension nonStandardYAxisUnits, Cicero2ResourceDictionary resources)
        {
            this.YUnits = nonStandardYAxisUnits;
            this.setInterpolationType(InterpolationType.Linear, resources);
        }

        /// <summary>
        /// Iterates through each waveform in referencedWaveforms and determines if that waveform references any
        /// of the waveforms in foundWaveforms. This is done recursively, and is intended as an algorithm
        /// to find waveform reference loops, which are illegal.
        /// 
        /// Returns true is a reference loop is found.
        /// </summary>
        /// <param name="foundWaveforms"></param>
        /// <returns></returns>
        private bool waveformHunt(List<Waveform> callingStackWaveforms)
        {

            if (referencedWaveforms == null) return false;
            if (referencedWaveforms.Count == 0) return false;
            if (myInterpolationType != InterpolationType.Combination) return false;

            foreach (Waveform wf in referencedWaveforms)
            {
                if (wf != null)
                {

                    if (callingStackWaveforms.Contains(wf)) // waveform wf was already references in a higher nesting level
                        return true;                 // so we have found a recursive reference

                    callingStackWaveforms.Add(wf);               // add waveform wf to the call stack of waveforms to be on the lookout for
                    if (wf.waveformHunt(callingStackWaveforms))  // ask waveform wf for search for reference loops
                        return true;                             // wf found a recursive reference, thus so did we.
                    callingStackWaveforms.Remove(wf);            // wf found no recursive reference, remove it from the "call stack"
                }
            }
            return false;
        }

        public override string ToString()
        {
            return waveformName;
        }

 /*       public Dictionary<Variable, string> usedVariables()
        {
            Dictionary<Variable, string> ans = new Dictionary<Variable, string>();

            List<DimensionedParameter> allParameters = new List<DimensionedParameter>();
            if (XValues != null)
                allParameters.AddRange(this.XValues);
            if (YValues != null)
                allParameters.AddRange(this.YValues);
            if (extraParameters != null)
                allParameters.AddRange(this.extraParameters);

            allParameters.Add(this.WaveformDuration);

            foreach (DimensionedParameter dp in allParameters)
            {
                if (dp != null)
                {
                    if (dp.parameter.variable != null)
                    {
                        if (!ans.ContainsKey(dp.parameter.variable))
                        {
                            ans.Add(dp.parameter.variable, ".");
                        }
                    }
                }
            }

            return ans;
        }*/

        /// <summary>
        /// Returns the effective duration of the waveform, which is the time until the waveform holds flat.
        /// </summary>
        /// <returns></returns>
        public double getEffectiveWaveformDuration(Cicero2ResourceDictionary resources)
        {
            if (myInterpolationType == InterpolationType.Linear || myInterpolationType == InterpolationType.Step)
            {
                if (xValues.Count > 0)
                {
                    // The following behavior, which was designed to wave waveform effective durations only as long as
                    // the time in which they are changing, resulted in some glitches
                    // for sharp events that happen precisely at the end of a waveform (for instance
                    // if there were multiple x-values with the same value.
                    // The behavior is being replaced by the more
                    // naive and transparent behavior of just returning the waveformDuration value.
                    /*
                    double maxX = double.MinValue;
                    foreach (DimensionedParameter xval in xValues)
                    {
                        if (xval.getBaseValue() > maxX)
                            maxX = xval.getBaseValue();
                    }
                    return Math.Min(maxX, waveformDuration.getBaseValue());
                     * */

                    return waveformDuration.getBaseValue(resources);
                }
                else
                {
                    return 0;
                }
            }
            else if (myInterpolationType == InterpolationType.Sinusoidal)
            {
                return (waveformDuration.getBaseValue(resources));
            }
            else if (myInterpolationType == InterpolationType.Exponential)
            {
                DimensionedParameter endTime = resources.Get(extraParameters[4]);
                return Math.Min(endTime.getBaseValue(resources), waveformDuration.getBaseValue(resources));
            }
            else if (myInterpolationType == InterpolationType.TwoPointCubicSpline)
            {
                if (xValues.Count > 1)
                {
                    DimensionedParameter endTime = resources.Get(xValues[1]);
                    return Math.Min(endTime.getBaseValue(resources), waveformDuration.getBaseValue(resources));
                }
                else
                {
                    return 0;
                }
            }
            else if (myInterpolationType == InterpolationType.Combination)
            {
                return waveformDuration.getBaseValue(resources);
            }
            else if (myInterpolationType == InterpolationType.Equation)
            {
                return waveformDuration.getBaseValue(resources);
            }
            else
            {
                throw new Exception("Unsupported interpolation type.");
            }
        }

        public void sortXValues(Cicero2ResourceDictionary resources)
        {
            List<ResourceID<DimensionedParameter>> sortedX = new List<ResourceID<DimensionedParameter>>();
            List<ResourceID<DimensionedParameter>> sortedY = new List<ResourceID<DimensionedParameter>>();

            for (int inIndex = 0; inIndex < XValues.Count; inIndex++)
            {
                int outIndex = 0;
                foreach (ResourceID<DimensionedParameter> outX in sortedX)
                {
                    if (XValues[inIndex].getBaseValue(resources) <= outX.getBaseValue(resources))
                        break;
                    outIndex++;
                }
                sortedX.Insert(outIndex, XValues[inIndex]);
                sortedY.Insert(outIndex, YValues[inIndex]);
            }

            XValues = sortedX;
            YValues = sortedY;
        }


        public bool scaleXValues(Cicero2ResourceDictionary resources)
        {
            if (resources.Get(waveformDuration).parameter.variable != ResourceID.Null)
                return false;

            foreach (ResourceID<DimensionedParameter> dp in XValues)
            {
                if (resources.Get (dp).parameter.variable != ResourceID.Null)
                {
                    return false;
                }
            }

			double maxX = 0;
            foreach (ResourceID<DimensionedParameter> dp in XValues)
            {
                if (dp.getBaseValue(resources) > maxX)
                {
                    maxX = dp.getBaseValue(resources);
                }
            }

            if (maxX == 0) 
            {
                return false;
            }

            foreach (ResourceID<DimensionedParameter> dp in XValues)
            {
                resources.Get(dp).setBaseValue(dp.getBaseValue(resources) * waveformDuration.getBaseValue(resources) / maxX); 
            }
            return true;
        }

    }
}
