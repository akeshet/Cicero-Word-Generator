using System;
using System.Collections;


namespace dotMath
{
	/// <remarks>
	/// Copyright (c) 2001-2004, Stephen Hebert
	/// All rights reserved.
	/// 
	/// 
	/// Redistribution and use in source and binary forms, with or without modification, 
	/// are permitted provided that the following conditions are met:
	/// 
	/// Redistributions of source code must retain the above copyright notice, 
	/// this list of conditions and the following disclaimer. 
	/// 
	/// Redistributions in binary form must reproduce the above 
	/// copyright notice, this list of conditions and the following disclaimer 
	/// in the documentation and/or other materials provided with the distribution. 
	/// 
	/// Neither the name of the .Math, nor the names of its contributors 
	/// may be used to endorse or promote products derived from this software without 
	/// specific prior written permission. 
	/// 
	/// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
	/// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED 
	/// TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
	/// PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS 
	/// BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
	/// OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
	/// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; 
	/// OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
	/// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR 
	/// OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
	/// EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
	/// 
	/// </remarks>
	/// 
	/// <summary>
	/// CFunctionLibrary provides additional math functions to the 
	/// equation compiler.  All functions may be added to the file using
	/// a static member variable.
	/// </summary>

	public class CFunctionLibrary
	{
		/// <summary>
		/// AddFunctions(EqCompiler): registers all the functions in the library with
		/// the provided compiler instance.
		/// </summary>
		/// <param name="eq">
		/// An incoming instance of the equation compiler.
		/// </param>
		/// <example>
		/// <code>
		/// EqCompiler comp = new EqCompiler("sqrt(a+b)");
		/// CFunctionLibrary.AddFunctions( comp );
		/// comp.Compile();
		/// </code>
		/// </example>
		public static void AddFunctions( EqCompiler eq )
		{
			eq.AddFunction( new CAbs() );
			eq.AddFunction( new CAcos());
			eq.AddFunction( new CAsin());
			eq.AddFunction( new CAtan());
			eq.AddFunction( new CCeiling());
			eq.AddFunction( new CCos());
			eq.AddFunction( new CCosh());
			eq.AddFunction( new CExp());
			eq.AddFunction( new CFloor());
			eq.AddFunction( new CLog());
			eq.AddFunction( new CLog10());
			eq.AddFunction( new CMax() );
			eq.AddFunction( new CMin() );
			eq.AddFunction( new CRound());
			eq.AddFunction( new CSign());
			eq.AddFunction( new CSin());
			eq.AddFunction( new CSinh());
			eq.AddFunction( new CSqrt());
			eq.AddFunction( new CTan());
			eq.AddFunction( new CTanh());

			eq.AddFunction( new CIf() );


		}
	}

	/// <summary>
	///  CAbs class implements the absolute value (abs(x)) function.
	/// </summary>
	public class CAbs : EqCompiler.CFunction 
	{
		/// <summary>
		/// An array of values associated with the function.
		/// </summary>
		ArrayList m_alValues;


		public CAbs( )
		{
		}

		/// <summary>
		/// CAbs.CreateInstance returns an instance of the CAbs object
		/// with the passed CValue object(s).
		/// </summary>
		/// <param name="alValues">An arraylist of values passed by the compiler.</param>
		/// <returns></returns>
		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CAbs oAbs = new CAbs();
			oAbs.SetValue( alValues );

			return oAbs;
		}

		/// <summary>
		/// CAbs.SetValue() retains the values in the arraylist for the current instance
		/// </summary>
		/// <param name="alValues">Arraylist of CValue objects</param>
		public override void SetValue( ArrayList alValues )
		{
			CheckParms(alValues, 1);
			m_alValues = alValues;
		}

		/// <summary>
		///  GetValue() is called by the compiler when the user requests the
		///  function to be evaluated.
		/// </summary>
		/// <returns>
		/// a double value with absolute value applied to the
		/// child parameter
		/// </returns>
		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			return Math.Abs(oValue.GetValue());
		}

		/// <summary>
		/// GetFunction() returns the function name as it appears syntactically
		/// to the compiler.
		/// </summary>
		/// <returns></returns>
		public override string GetFunction()
		{
			return "abs";
		}
	}

	/// <summary>
	/// CAcos implements calculating the angle whose cosine is the specified number.
	/// </summary>
	public class CAcos : EqCompiler.CFunction 
	{
		
		ArrayList m_alValues;


		/// <summary>
		/// Standard Constructor - used for instantiating object for creation
		/// of other CAcos objects.
		/// </summary>
		public CAcos( )
		{
		}

		/// <summary>
		/// Creates an instance of CAcos used in the expression compilation. 
		/// </summary>
		/// <param name="alValues">array list of CValue objects representing
		/// the indicated child-values</param>
		/// <returns>CFunction-derived object - CAcos</returns>
		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CAcos oAcos = new CAcos();
			oAcos.SetValue( alValues );

			return oAcos;
		}

		/// <summary>
		/// Retains/validates child values used in the calculation.
		/// </summary>
		/// <param name="alValues">Array list of CValue objects representing
		/// indicated child-values.</param>
		public override void SetValue( ArrayList alValues )
		{
			CheckParms( alValues, 1);
			m_alValues = alValues;
		}

		/// <summary>
		/// GetValue returns a double value as a result of running the
		/// function against child-CValue objects.
		/// </summary>
		/// <returns></returns>
		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			return Math.Acos(oValue.GetValue());
		}

        /// <summary>
        /// GetFunction() returns the function name according to the syntax
        /// of the function within the compiler.
        /// </summary>
        /// <returns>string containing the function name</returns>
		public override string GetFunction()
		{
			return "acos";
		}
	}


	/// <summary>
	/// CAsin implements calculating the angle whose sine is the specified number.
	/// </summary>
	public class CAsin : EqCompiler.CFunction 
	{
		
		ArrayList m_alValues;

		/// <summary>
		/// Simple constructor.  Used when registering with the equation compiler.
		/// </summary>
		public CAsin( )
		{
		}

		/// <summary>
		/// CreateInstance() is responsible for createing a new instance
		/// of this object that can be used in calculations.
		/// </summary>
		/// <param name="alValues">Arraylist of CValue objects</param>
		/// <returns>EqCompiler.CFunction</returns>
		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CAsin oAsin = new CAsin();
			oAsin.SetValue( alValues );

			return oAsin;
		}

		/// <summary>
		/// SetValue allows for checking of parameters and storage during compilation.
		/// </summary>
		/// <param name="alValues">Set of CValue objects used in calculation.</param>
		public override void SetValue( ArrayList alValues )
		{
			CheckParms(alValues, 1);
			m_alValues = alValues;
		}

		/// <summary>
		/// GetValue() returns the value of the object and it's child members.
		/// </summary>
		/// <returns>double representing the value in the object.</returns>
		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			return Math.Asin(oValue.GetValue());
		}

		/// <summary>
		/// GetFunction() : returns the name of the function.
		/// </summary>
		/// <returns></returns>
		public override string GetFunction()
		{
			return "asin";
		}
	}


	/// <summary>
	/// CAtan implements calculating the angle whose tangent is the specified number.
	/// </summary>
	public class CAtan : EqCompiler.CFunction 
	{
		
		ArrayList m_alValues;


		public CAtan( )
		{
		}


		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CAtan oAtan = new CAtan();
			oAtan.SetValue( alValues );

			return oAtan;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms(alValues,1);
			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			return Math.Atan(oValue.GetValue());
		}

		public override string GetFunction()
		{
			return "atan";
		}
	}

	/// <summary>
	/// CFloor class: Returns the largest whole number less than or equal to the specified number.
	/// </summary>
	public class CFloor : EqCompiler.CFunction 
	{
		
		ArrayList m_alValues;


		public CFloor( )
		{
		}


		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CFloor oFloor = new CFloor();
			oFloor.SetValue( alValues );

			return oFloor;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms(alValues,1);

			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			return Math.Floor(oValue.GetValue());
		}

		public override string GetFunction()
		{
			return "floor";
		}
	}



	/// <summary>
	/// CCeiling class: Returns the largest whole number greater than or equal to the specified number.
	/// </summary>
	public class CCeiling : EqCompiler.CFunction 
	{
		
		ArrayList m_alValues;


		public CCeiling( )
		{
		}


		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CCeiling oCeiling = new CCeiling();
			oCeiling.SetValue( alValues );

			return oCeiling;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms(alValues,1);

			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			return Math.Ceiling(oValue.GetValue());
		}

		public override string GetFunction()
		{
			return "ceiling";
		}
	}

	/// <summary>
	/// CExp class: Returns e raised to the specified power.
	/// </summary>
	public class CExp : EqCompiler.CFunction 
	{
		
		ArrayList m_alValues;


		public CExp( )
		{
		}


		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CExp oExp = new CExp();
			oExp.SetValue( alValues );

			return oExp;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms(alValues,1);
			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			return Math.Exp(oValue.GetValue());
		}

		public override string GetFunction()
		{
			return "exp";
		}
	}



	/// <summary>
	/// CSin class: returns the sine of the specified angle.
	/// </summary>
	public class CSin : EqCompiler.CFunction 
	{
		EqCompiler.CValue m_oValue;
		
		
		public CSin()
		{
		}

		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CSin oSin = new CSin();
			oSin.SetValue(alValues);

			return oSin;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms(alValues,1);

			m_oValue = (EqCompiler.CValue) alValues[0];
		}

		public override double GetValue()
		{
			return Math.Sin(m_oValue.GetValue());
		}

		public override string GetFunction()
		{
			return "sin";
		}
	}


	/// <summary>
	/// CSinh class: Returns the hyperbolic sine of the specified angle.
	/// </summary>
	public class CSinh : EqCompiler.CFunction 
	{
		ArrayList m_alValues;
		
		public CSinh()
		{
		}

		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CSinh oSinh = new CSinh();
			oSinh.SetValue(alValues);

			return oSinh;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms(alValues, 1);

			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oValue1 = (EqCompiler.CValue)m_alValues[0];
			return Math.Sinh(oValue1.GetValue());
		}

		public override string GetFunction()
		{
			return "sinh";
		}
	}

	/// <summary>
	/// CSqrt class: implements the square root function.
	/// </summary>
	public class CSqrt : EqCompiler.CFunction 
	{
		ArrayList m_alValues;
		
		public CSqrt()
		{
		}

		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CSqrt oSqrt = new CSqrt();
			oSqrt.SetValue(alValues);

			return oSqrt;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms( alValues , 1);

			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oValue1 = (EqCompiler.CValue)m_alValues[0];
			return Math.Sqrt(oValue1.GetValue());
		}

		public override string GetFunction()
		{
			return "sqrt";
		}
	}

	/// <summary>
	/// CCos class: returns the cosine of the specified angle
	/// </summary>
	public class CCos : EqCompiler.CFunction 
	{
		
		ArrayList m_alValues;

		public CCos( )
		{
		}


		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CCos oCos = new CCos();
			oCos.SetValue( alValues );

			return oCos;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms(alValues,1);
			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			return Math.Cos(oValue.GetValue());
		}

		public override string GetFunction()
		{
			return "cos";
		}
	}



	/// <summary>
	/// CCosh class: returns the hyperbolic cosine of the specified angle
	/// </summary>
	public class CCosh : EqCompiler.CFunction 
	{
		
		ArrayList m_alValues;


		public CCosh( )
		{
		}


		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CCosh oCos = new CCosh();
			oCos.SetValue( alValues );

			return oCos;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms(alValues,1);

			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			return Math.Cosh(oValue.GetValue());
		}

		public override string GetFunction()
		{
			return "cosh";
		}
	}


	/// <summary>
	/// CIf class: implements if(condition, then,else) functionality.
	/// </summary>
	public class CIf : EqCompiler.CFunction
	{
		ArrayList m_alValues;

		public CIf()
		{
		}

		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CIf oIf = new CIf();
			oIf.SetValue( alValues );

			return oIf;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms( alValues,3);
            										
			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oTest = (EqCompiler.CValue) m_alValues[0];
			EqCompiler.CValue oThen = (EqCompiler.CValue) m_alValues[1];
			EqCompiler.CValue oElse = (EqCompiler.CValue) m_alValues[2];

			if( oTest.GetValue() != 0.0 )
				return oThen.GetValue();
			else
				return oElse.GetValue();

		}

		public override string GetFunction()
		{
			return "if";
		}
	}


	/// <summary>
	/// CMax class: returns the maximum value among provided parameters
	/// </summary>
	public class CMax : EqCompiler.CFunction
	{
		ArrayList m_alValues;

		public CMax()
		{
		}

		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CMax oMax = new CMax();
			oMax.SetValue( alValues );

			return oMax;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms( alValues, 2, -1 );  // check for a minimum of two values (no maximum limit)

			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			double dValue = oValue.GetValue();

			for( int i = 1; i < m_alValues.Count; i++ )
			{
				oValue = (EqCompiler.CValue) m_alValues[i];

				dValue = Math.Max(dValue, oValue.GetValue() );
			}

			return dValue;
			


		}

		public override string GetFunction()
		{
			return "max";
		}
	}


	/// <summary>
	/// CRound class:  implements a rouding of a number to the nearest whole number.
	/// </summary>
	public class CRound : EqCompiler.CFunction 
	{
		
		ArrayList m_alValues;


		public CRound( )
		{
		}


		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CRound oRound = new CRound();
			oRound.SetValue( alValues );

			return oRound;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms( alValues,1);
			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			return Math.Round(oValue.GetValue());
		}

		public override string GetFunction()
		{
			return "round";
		}
	}



	/// <summary>
	/// CSign class: returns a value that indicates the sign of the provide value.
	///	Returns the following possibilties:
	///	value < 0 = -1
	///	value = 0 = 0
	///	value > 0 = 1
	/// </summary>
	public class CSign : EqCompiler.CFunction
	{
		ArrayList m_alValues;

		public CSign()
		{
		}

		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CSign oSign = new CSign();
			oSign.SetValue( alValues );

			return oSign;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms(alValues, 1);
			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			return Math.Sign(oValue.GetValue());
		}

		public override string GetFunction()
		{
			return "sign";
		}
	}

	/// <summary>
	/// CMin class:  returns the minimum value among supplied values.
	/// </summary>
	public class CMin : EqCompiler.CFunction
	{
		ArrayList m_alValues;

		public CMin()
		{
		}

		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CMin oMin = new CMin();
			oMin.SetValue( alValues );

			return oMin;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms( alValues, 2, -1 );

			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			double dValue = oValue.GetValue();

			for( int i = 1; i < m_alValues.Count; i++ )
			{
				oValue = (EqCompiler.CValue) m_alValues[i];

				dValue = Math.Min(dValue, oValue.GetValue() );
			}

			return dValue;
		}

		public override string GetFunction()
		{
			return "min";
		}
	}



	/// <summary>
	/// CTan class: return the tagent of the supplied angle
	/// </summary>
	public class CTan : EqCompiler.CFunction 
	{
		ArrayList m_alValues;

		public CTan( )
		{
		}


		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CTan oTan = new CTan();
			oTan.SetValue( alValues );

			return oTan;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms(alValues,1);

			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			return Math.Tan(oValue.GetValue());
		}

		public override string GetFunction()
		{
			return "tan";
		}
	}

	/// <summary>
	/// CLog class: returns the logarithm of a specified number.
	/// </summary>
	public class CLog : EqCompiler.CFunction 
	{
		ArrayList m_alValues;

		public CLog( )
		{
		}


		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CLog oLog = new CLog();
			oLog.SetValue( alValues );

			return oLog;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms(alValues,1);

			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			return Math.Log(oValue.GetValue());
		}

		public override string GetFunction()
		{
			return "log";
		}
	}


	/// <summary>
	/// CLog10 class:  Returns the base 10 logarithm of a specified number.
	/// </summary>
	public class CLog10 : EqCompiler.CFunction 
	{
		ArrayList m_alValues;

		public CLog10( )
		{
		}


		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CLog10 oLog10 = new CLog10();
			oLog10.SetValue( alValues );

			return oLog10;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms( alValues, 1);

			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			return Math.Log10(oValue.GetValue());
		}

		public override string GetFunction()
		{
			return "log10";
		}
	}


	/// <summary>
	/// CTanh class: Returns the hyperbolic tangent of the supplied angle.
	/// </summary>
	public class CTanh : EqCompiler.CFunction 
	{
		ArrayList m_alValues;

		public CTanh( )
		{
		}


		public override EqCompiler.CFunction CreateInstance( ArrayList alValues )
		{
			CTanh oTanh = new CTanh();
			oTanh.SetValue( alValues );

			return oTanh;
		}

		public override void SetValue( ArrayList alValues )
		{
			CheckParms(alValues,1);

			m_alValues = alValues;
		}

		public override double GetValue()
		{
			EqCompiler.CValue oValue = (EqCompiler.CValue) m_alValues[0];
			return Math.Tanh(oValue.GetValue());
		}

		public override string GetFunction()
		{
			return "tanh";
		}
	}

}
