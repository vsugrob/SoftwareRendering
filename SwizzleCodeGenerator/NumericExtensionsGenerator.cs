using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CodeGenerator {
	public static class NumericExtensionsGenerator {
		static Type [] NumericTypes = new [] {
			typeof ( Byte ), typeof ( SByte ),
			typeof ( Int16 ), typeof ( UInt16 ),
			typeof ( Int32 ), typeof ( UInt32 ),
			typeof ( Int64 ), typeof ( UInt64 ),
			typeof ( Single ), typeof ( Double ),
			typeof ( Decimal )
		};

		public static void Generate () {
			if ( !Directory.Exists ( "Utils" ) )
				Directory.CreateDirectory ( "Utils" );

			//string clampsCode = GenerateClampMethods ();

			string code = string.Format (
@"using System;

namespace {0} {{
	public static class NumericExtensions {{
		public static bool IsNumeric ( this object obj ) {{
			return	obj is Byte || obj is SByte ||
				obj is Int16 || obj is UInt16 ||
				obj is Int32 || obj is UInt32 ||
				obj is Int64 || obj is UInt64 ||
				obj is Single || obj is Double ||
				obj is Decimal;
		}}
		
		public static T Clamp <T> ( this T val, T min, T max ) where T : IComparable <T> {{
			return	val.CompareTo ( min ) == -1 ? min : ( val.CompareTo ( max ) == 1 ? max : val );
		}}
		
		#region Lerp
		public static int Lerp ( this double value, int a, int b ) {{
			return	a + ( int ) Math.Round ( ( b - a ) * value );
		}}
		
		public static long Lerp ( this double value, long a, long b ) {{
			return	a + ( long ) Math.Round ( ( b - a ) * value );
		}}
		
		public static double Lerp ( this double value, double a, double b ) {{
			return	a + ( b - a ) * value;
		}}
		
		public static int Lerp ( this float value, int a, int b ) {{
			return	a + ( int ) Math.Round ( ( b - a ) * value );
		}}
		
		public static long Lerp ( this float value, long a, long b ) {{
			return	a + ( long ) Math.Round ( ( b - a ) * value );
		}}
		
		public static float Lerp ( this float value, float a, float b ) {{
			return	a + ( b - a ) * value;
		}}
		
		public static double Lerp ( this float value, double a, double b ) {{
			return	a + ( b - a ) * value;
		}}
		#endregion Lerp
	}}
}}", Globals.Namespace );

			File.WriteAllText ( @"Utils\NumericExtensions.cs", code );
		}

		public static string GenerateClampMethods () {
			var methodCodes = NumericTypes.Select ( t => string.Format (
@"		public static {0} Clamp ( this {0} value, {0} min, {0} max ) {{
			return	value < min ? min : ( value > max ? max : value );
		}}", t.Name ) );

			return	string.Join ( "\r\n\r\n", methodCodes );
		}
	}
}
