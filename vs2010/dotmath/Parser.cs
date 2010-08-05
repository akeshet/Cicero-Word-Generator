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

	/// <summary>
	/// Parser class: The Parser class is responsible for traversing a given
	///		function and creating an enumerable set of tokens.
	/// </summary>
	public class Parser
	{
		/// <summary>
		/// CharType enum:  This represents the various types of character types known
		///		by the parser.
		/// </summary>
		public enum CharType
		{
			CT_DELIM,
			CT_WHITESPACE,
			CT_NUMBER,
			CT_LETTER,
			CT_BRACKETS,
			CT_UNDEF
		}


		/// <summary>
		/// Token class:  The Token class represents a collection of 1 or more characters
		///		that form a given set of singly definable characters.
		/// </summary>
		public class Token
		{
            private string m_sToken;
			private CharType m_chType;

            public override int GetHashCode()
            {
                return 1;
            }

			/// <summary>
			/// Equals(Object): Tests for equality with another token.  Also
			///		handles the null case.
			/// </summary>
			/// <param name="sValue">string value representing the token being evaluated against.</param>
			/// <returns>bool where true means both objects are equal.</returns>
			public override bool Equals( Object sValue )
			{
				if (sValue == null )
					return m_sToken == null;
				 
				if( (string)sValue == m_sToken )
					return true;
				else
					return false;
			}

			/// <summary>
			/// operator == (Token, string): This overloaded operator checks for equality between a token and a string.
			/// </summary>
			/// <param name="sToken">Token - actual token object</param>
			/// <param name="sValue">string - token value to test against token object value</param>
			/// <returns></returns>
			public static bool operator == (Token sToken, string sValue)
			{
				// the following tests are order dependent.  If both are null, eq is true.  
				// if only one is null the eq is false.


				if( (object)sToken == null  &&  sValue == null )
						return true;

				if( (object)sToken == null || sValue == null )
					return false;

				if( sToken.m_sToken == sValue )
					return true;
				else
					return false;
			}

			/// <summary>
			/// operator != (Token, string): Overloaded operator that checks for inequality
			///		between the token value and the passed string.
			/// </summary>
			/// <param name="sToken">Token object being test</param>
			/// <param name="sValue">string that is being compared with the Token object's string value</param>
			/// <returns>bool indicating true for inequality</returns>
			public static bool operator != (Token sToken, string sValue)
			{
				if (sToken == null || sValue == null)
					return !(sToken == null && sValue == null);

				if( sToken.m_sToken != sValue )
					return true;
				else
					return false;
			}

			/// <summary>
			/// Token(string, CharType) constructor: A constructor that creates a new token object with the
			///		passed token string and character type information.
			/// </summary>
			/// <param name="sToken">string representing the token value</param>
			/// <param name="chType">Character enumeration describing the type of data in the token.</param>
			public Token(string sToken, CharType chType)
			{
				m_sToken = sToken;
				m_chType = chType;
			}

			/// <summary>
			/// Token() constructor:  creates an empty token
			/// </summary>
			public Token()
			{
				m_sToken = "";
				m_chType = CharType.CT_UNDEF;
			}

			/// <summary>
			/// Token(char, CharType) constructor: create a single character token with type info
			/// </summary>
			/// <param name="chToken">Single character token value</param>
			/// <param name="chType">Token Type information</param>
			public Token(char chToken, CharType chType)
			{
				m_sToken = "";

				m_sToken += chToken;
				m_chType = chType;
			}

			/// <summary>
			/// TokenType property: Returns the Character type associate with the token.
			/// </summary>
			public CharType TokenType
			{
				get
				{
					return m_chType;
				}
			}


			/// <summary>
			/// ToString() - returns the string name of the token.
			/// </summary>
			/// <returns></returns>
			public override string ToString()
			{
				return m_sToken;
			}
		}


		private string m_sFunction;
		private string m_sWhitespace;
		private string m_sDelimiters;
		private string m_sNumbers;
		private string m_sLetters;
		private string m_sBrackets;
		private ArrayList m_alTokens;

		/// <summary>
		/// Parser(string): This function takes an expression and launches the parsing process.
		/// </summary>
		/// <param name="sFunction">The expression string to be parsed.</param>
		public Parser(string sFunction)
		{
			m_sFunction = sFunction;

			m_sWhitespace = " \t";
			m_sDelimiters = "+-*/^()<>=&|!,";
			m_sNumbers = ".0123456789";
			m_sLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz%_";
			m_sBrackets = "[]";

			Parse();
		}

		/// <summary>
		/// GetTokenEnumerator(): Gets an enumerator associated with the token collection.
		/// </summary>
		/// <returns>IEnumerator object of the Token ArrayList</returns>
		public IEnumerator GetTokenEnumerator()
		{
			return m_alTokens.GetEnumerator();
		}

		/// <summary>
		/// CheckMultiCharOps(): This function checks for multi character operations that together take
		///		on a different meaning than simply by themselves.  This can reorganize the entire ArrayList by the
		///		time it is complete.
		/// </summary>
		private void CheckMultiCharOps()
		{
			ArrayList alTokens = new ArrayList();
			IEnumerator iEnum = GetTokenEnumerator();
			Token nextToken1 = null;
			Token nextToken2 = null;

			if( iEnum.MoveNext() )
				nextToken1 = (Token)iEnum.Current;

			if( iEnum.MoveNext() )
				nextToken2 = (Token)iEnum.Current;

			//bool bCombined;
			while( nextToken1 != null )
			{
				
				if( nextToken1.TokenType == CharType.CT_DELIM )
				{
					if( nextToken2 != null && nextToken2.TokenType ==CharType.CT_DELIM )
					{
						string s1 = nextToken1.ToString()+ nextToken2.ToString();

						if( s1 == "&&" || 
							s1 == "||" ||
							s1 == "<=" ||
							s1 == ">=" ||
							s1 == "!=" ||
							s1 == "<>" ||
							s1 == "==" )
						{
							

							nextToken1 = new Token(s1, CharType.CT_DELIM );
							//alTokens.Add(nextToken1 );

							if( iEnum.MoveNext() )
								nextToken2 = (Token)iEnum.Current;
							
						}
					}
				}

				alTokens.Add(nextToken1);

				nextToken1 = nextToken2;

				if( nextToken2 != null)
				{
					if( iEnum.MoveNext() )
						nextToken2 = (Token)iEnum.Current;
					else
						nextToken2 = null;
				}
				else
					nextToken1 = null;
			}

			m_alTokens = alTokens;
		}

		/// <summary>
		/// Parse():  This function kicks off the parsing process of the provided function.
		/// </summary>
		private void Parse()
		{
			m_alTokens = new ArrayList();
			CharType chType= CharType.CT_UNDEF;


			string sToken = "";

			CharEnumerator chEnum = m_sFunction.GetEnumerator();

			while( chEnum.MoveNext() )
			{
				if( m_sWhitespace.IndexOf(chEnum.Current) > -1 )
				{
					if( sToken.Length > 0 )
						m_alTokens.Add(new Token(sToken, chType));
					sToken = "";

					continue;
				}

				if( m_sDelimiters.IndexOf(chEnum.Current) > -1)
				{
					if( sToken.Length > 0 )
						m_alTokens.Add( new Token(sToken, chType ));

					m_alTokens.Add( new Token(chEnum.Current, CharType.CT_DELIM));

					sToken = "";
					chType = CharType.CT_UNDEF;
					continue;
				}

				if( m_sBrackets.IndexOf( chEnum.Current) > -1 )
				{
					if( sToken.Length > 0 )
						m_alTokens.Add( new Token( sToken, chType ));

					m_alTokens.Add( new Token( chEnum.Current, CharType.CT_BRACKETS ));

					sToken = "";
					chType = CharType.CT_UNDEF;
					continue;
				}

				if( m_sNumbers.IndexOf( chEnum.Current ) > -1 )
				{
					if( sToken.Length == 0 )
						chType = CharType.CT_NUMBER;

					sToken += chEnum.Current;
					continue;
				}

				if( m_sLetters.IndexOf( chEnum.Current ) > -1 )
				{
					if( sToken.Length == 0 )
						chType = CharType.CT_LETTER;
					else
					{
						if( chType != CharType.CT_LETTER )
							chType = CharType.CT_UNDEF;
					}
					sToken += chEnum.Current;
					continue;
				}

				if( sToken.Length > 0 )
					m_alTokens.Add( new Token(sToken, chType ));

				sToken = "";
				chType = CharType.CT_UNDEF;
				
			}

			if( sToken.Length > 0 )
				m_alTokens.Add( new Token( sToken, chType ));

		
			CheckMultiCharOps();
		}
	}
}
