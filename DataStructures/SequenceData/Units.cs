using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataStructures
{
    /// <summary>
    /// This class represents the units applied to a number. It keeps track of both the dimension (ie unity, s, volts, Hz) and a
    /// multiplier (ie M, u, k, etc).
    /// </summary>
    [Serializable, TypeConverter(typeof(ExpandableStructConverter))]
	public struct Units
	{
        [Serializable, TypeConverter(typeof (Dimension.DimensionTypeConverter))]
		public struct Dimension {

            public class DimensionTypeConverter : EnumWrapperTypeConverter
            {
                public DimensionTypeConverter() : base(Dimension.allDimensions) 
                {
                }
            }

            private enum DimensionID { unity, s, V, Hz, A, Degrees };
			private DimensionID myDimensionID;

			private static readonly string[] DimensionString = {
				"", "s", "V", "Hz", "A", "deg"};

            private static readonly string[] DimensionFullName = {
                "dimensionless", "seconds", "volts", "hertz", "amps", "degrees"};

			public static readonly Dimension unity = new Dimension(DimensionID.unity);
			public static readonly Dimension s = new Dimension(DimensionID.s);
			public static readonly Dimension V = new Dimension(DimensionID.V);
			public static readonly Dimension Hz = new Dimension(DimensionID.Hz);
            public static readonly Dimension A = new Dimension(DimensionID.A);
            public static readonly Dimension Degrees = new Dimension(DimensionID.Degrees);

            /// <summary>
            /// This property gives a list of commonly used multipliers for this dimension type.
            /// This should be used in the UI to give the user a limited number of multipliers to select from
            /// in a combo box, rather than presenting the user with a long list of impractical multipliers for
            /// a particular dimension type (ie Gs, or nHz)
            /// </summary>
            /// 
            public Multiplier[] commonlyUsedMultipliers { 
                get 
                {
                    return commonlyUsedMultipliersArray[(int)myDimensionID];
                } 
            }


            /// <summary>
            /// This is a two dimensional array of the multipliers which are commonly or practically useful
            /// for given dimension types. The first array index is the dimension type. For instance, 
            /// commonlyUsedMultipliers[Dimension.v] is an array of the multipliers commonly applied to Volts.
            /// The array is used by the property above.
            /// </summary>
            private static readonly Multiplier[][] commonlyUsedMultipliersArray =
            {   Multiplier.allMultipliers, // unity
                new Multiplier[] {Multiplier.u, Multiplier.m, Multiplier.unity}, // s
                new Multiplier[] {Multiplier.u, Multiplier.m, Multiplier.unity, Multiplier.k}, // V
                new Multiplier[] {Multiplier.unity, Multiplier.k, Multiplier.M, Multiplier.G}, //Hz
                new Multiplier [] {Multiplier.u, Multiplier.m, Multiplier.unity, Multiplier.k}, // A
                new Multiplier [] {Multiplier.unity} // deg
            };

            public static readonly Dimension[] allDimensions = {unity, s, V, Hz, A, Degrees};

	        public override string  ToString()
            {
	            return DimensionString[(int)myDimensionID];
            }

            public string toLongString()
            {
                return DimensionFullName[(int)myDimensionID];
            }

			private Dimension(DimensionID dimID) 
			{
				myDimensionID = dimID;
			}
		}

        [Serializable, TypeConverter(typeof (Multiplier.MultiplierTypeConverter))]
        public struct Multiplier
        {

            public class MultiplierTypeConverter : EnumWrapperTypeConverter
            {
                public MultiplierTypeConverter()
                    : base(Multiplier.allMultipliers)
                {
                }
            }

            private enum MultiplierID { n, u, m, unity, k, M, G };
            private static readonly double[] MultiplierValue = { .000000001, .000001, .001, 1, 1000, 1000000, 1000000000 };
            private static readonly string[] MultiplierString = { "n", "u", "m", "", "k", "M", "G" };
            private static readonly string[] MultiplierFullName = {"nano", "micro", "milli", "", "kilo", "mega", "giga"};

            public static readonly Multiplier n = new Multiplier(MultiplierID.n);
            public static readonly Multiplier u = new Multiplier(MultiplierID.u);
            public static readonly Multiplier m = new Multiplier(MultiplierID.m);
            public static readonly Multiplier unity = new Multiplier(MultiplierID.unity);
            public static readonly Multiplier k = new Multiplier(MultiplierID.k);
            public static readonly Multiplier M = new Multiplier(MultiplierID.M);
            public static readonly Multiplier G = new Multiplier(MultiplierID.G);

            public static readonly Multiplier[] allMultipliers = {n, u, m, unity, k, M, G};

            private MultiplierID myMultiplierID;

            private Multiplier(MultiplierID multID) {
                this.myMultiplierID = multID;
            }


            public override string  ToString()
            {
 	            return MultiplierString[(int)myMultiplierID];
            }

            public string toLongString()
            {
                return MultiplierFullName[(int)myMultiplierID];
            }

            public double getMultiplierFactor() 
            {
                return MultiplierValue[(int)myMultiplierID];
            }

            /// <summary>
            /// This is an implicit typecast from Multiplier to double, allowing for expressions like
            /// double a = number * Multiplier.M
            /// </summary>
            /// <param name="mul"></param>
            /// <returns></returns>
            static public implicit operator double(Multiplier mul)
            {
                return mul.getMultiplierFactor();
            }

        }


        private Dimension myDimension;

        public Dimension dimension
        {
            get { return myDimension; }
            set { myDimension = value; }
        }
        private Multiplier myMultiplier;

        public Multiplier multiplier
        {
            get { return myMultiplier; }
            set { myMultiplier = value; }
        }

        public override string  ToString()
        {
 	        return myMultiplier.ToString() + myDimension.ToString();
        }

        public string toLongString()
        {
            return myMultiplier.toLongString() + myDimension.toLongString();
        }

        /// <summary>
        /// This constructor attempts to construct a Unit object with the same long or short name as the given string.
        /// (ie it will parse strings like "mHz", "microvolts", "gigaunity", "M", "ks"). The constructor is case sensitive.
        /// If no matching unit type is found, the constructor throws an exception.
        /// </summary>
        /// <param name="unitString"></param>
        public Units(string unitString)
        {
            Units testUnit = new Units();
            foreach (Dimension dim in Dimension.allDimensions)
                foreach (Multiplier mul in Multiplier.allMultipliers)
                {
                    testUnit.myDimension = dim;
                    testUnit.myMultiplier = mul;
                    if ((unitString == testUnit.ToString()) || (unitString == testUnit.toLongString()))
                    {
                        myDimension = dim;
                        myMultiplier = mul;
                        return;
                    }
                }

            throw new Exception("Unrecognized Unit type " + unitString + " passed to Units(string unitString) constructor.");
        }

        public Units(Dimension dimension, Multiplier multiplier) : this()
        {
            this.dimension = dimension;
            this.multiplier = multiplier;
        }

        public static readonly Units V = new Units(Dimension.V, Multiplier.unity);
        public static readonly Units Hz = new Units(Dimension.Hz, Multiplier.unity);
        public static readonly Units s = new Units(Dimension.s, Multiplier.unity);
        public static readonly Units A = new Units(Dimension.A, Multiplier.unity);
        public static readonly Units deg = new Units(Dimension.Degrees, Multiplier.unity);
	}
}
