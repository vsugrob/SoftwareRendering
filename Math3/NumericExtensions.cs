using System;
using System.Diagnostics;

namespace Math3d {
	public static class NumericExtensions {
		public static bool IsNumeric ( this object obj ) {
			return	obj is Byte || obj is SByte ||
				obj is Int16 || obj is UInt16 ||
				obj is Int32 || obj is UInt32 ||
				obj is Int64 || obj is UInt64 ||
				obj is Single || obj is Double ||
				obj is Decimal;
		}

		#region Clamp
		public static T Clamp <T> ( this T val, T min, T max ) where T : IComparable <T> {
			return	val.CompareTo ( min ) == -1 ? min : ( val.CompareTo ( max ) == 1 ? max : val );
		}

		public static T ClampBounds <T> ( this T val, T a, T b ) where T : IComparable <T> {
			if ( b.CompareTo ( a ) == 1 )
				return	val.CompareTo ( a ) == -1 ? a : ( val.CompareTo ( b ) == 1 ? b : val );
			else
				return	val.CompareTo ( b ) == -1 ? b : ( val.CompareTo ( a ) == 1 ? a : val );
		}

		public static double2 Clamp ( this double2 v, double2 min, double2 max ) {
			return	new double2 (
				v.x < min.x ? min.x : ( v.x > max.x ? max.x : v.x ),
				v.y < min.y ? min.y : ( v.y > max.y ? max.y : v.y )
			);
		}

		public static double2 ClampBounds ( this double2 v, double2 a, double2 b ) {
			return	new double2 (
				v.x.ClampBounds ( a.x, b.x ),
				v.y.ClampBounds ( a.y, b.y )
			);
		}

		public static double3 Clamp ( this double3 v, double3 min, double3 max ) {
			return	new double3 (
				v.x < min.x ? min.x : ( v.x > max.x ? max.x : v.x ),
				v.y < min.y ? min.y : ( v.y > max.y ? max.y : v.y ),
				v.z < min.z ? min.z : ( v.z > max.z ? max.z : v.z )
			);
		}

		public static double3 ClampBounds ( this double3 v, double3 a, double3 b ) {
			return	new double3 (
				v.x.ClampBounds ( a.x, b.x ),
				v.y.ClampBounds ( a.y, b.y ),
				v.z.ClampBounds ( a.z, b.z )
			);
		}

		public static double4 Clamp ( this double4 v, double4 min, double4 max ) {
			return	new double4 (
				v.x < min.x ? min.x : ( v.x > max.x ? max.x : v.x ),
				v.y < min.y ? min.y : ( v.y > max.y ? max.y : v.y ),
				v.z < min.z ? min.z : ( v.z > max.z ? max.z : v.z ),
				v.w < min.w ? min.w : ( v.w > max.w ? max.w : v.w )
			);
		}

		public static double4 ClampBounds ( this double4 v, double4 a, double4 b ) {
			return	new double4 (
				v.x.ClampBounds ( a.x, b.x ),
				v.y.ClampBounds ( a.y, b.y ),
				v.z.ClampBounds ( a.z, b.z ),
				v.w.ClampBounds ( a.w, b.w )
			);
		}
		#endregion Clamp

		public static int PreciseRound ( this double a ) {
			return	( int ) ( a + Math.Sign ( a ) * ( 0.5 + Math3.DIFF_THR ) );
		}

		public static void IntsAlongDir ( this double a, int unitDir, out int prev, out int next ) {
		    Debug.Assert ( Math.Abs ( unitDir ) == 1, "Invalid argument", "Argument unitDir must have value -1 or 1!" );
		    a += unitDir * Math3.DIFF_THR;

		    if ( unitDir == Math.Sign ( a ) ) {
		        prev = ( int ) a;
		        next = ( int ) ( a + unitDir );
		    } else {
		        prev = ( int ) ( a - unitDir );
		        next = ( int ) a;
		    }
		}

		public static int PrevIntAlongDir ( this double a, int unitDir ) {
		    Debug.Assert ( Math.Abs ( unitDir ) == 1, "Invalid argument", "Argument unitDir must have value -1 or 1!" );
		    a += unitDir * Math3.DIFF_THR;

		    if ( unitDir == Math.Sign ( a ) )
		        return	( int ) a;
		    else
		        return	( int ) ( a - unitDir );
		}

		public static int NextIntAlongDir ( this double a, int unitDir ) {
		    Debug.Assert ( Math.Abs ( unitDir ) == 1, "Invalid argument", "Argument unitDir must have value -1 or 1!" );
		    a += unitDir * Math3.DIFF_THR;

		    if ( unitDir == Math.Sign ( a ) )
		        return	( int ) ( a + unitDir );
		    else
		        return	( int ) a;
		}
		
		#region Linear Interpolation
		public static int Lerp ( this double value, int a, int b ) {
			return	a + ( int ) Math.Round ( ( b - a ) * value );
		}
		
		public static long Lerp ( this double value, long a, long b ) {
			return	a + ( long ) Math.Round ( ( b - a ) * value );
		}
		
		public static double Lerp ( this double value, double a, double b ) {
			return	a + ( b - a ) * value;
		}

		public static double2 Lerp ( this double value, double2 a, double2 b ) {
		    return	a + ( b - a ) * value;
		}

		public static double3 Lerp ( this double value, double3 a, double3 b ) {
			return	a + ( b - a ) * value;
		}

		public static double4 Lerp ( this double value, double4 a, double4 b ) {
		    return	a + ( b - a ) * value;
		}
		
		public static int Lerp ( this float value, int a, int b ) {
			return	a + ( int ) Math.Round ( ( b - a ) * value );
		}
		
		public static long Lerp ( this float value, long a, long b ) {
			return	a + ( long ) Math.Round ( ( b - a ) * value );
		}
		
		public static float Lerp ( this float value, float a, float b ) {
			return	a + ( b - a ) * value;
		}
		
		public static double Lerp ( this float value, double a, double b ) {
			return	a + ( b - a ) * value;
		}

		public static double2 Lerp ( this float value, double2 a, double2 b ) {
		    return	a + ( b - a ) * value;
		}

		public static double3 Lerp ( this float value, double3 a, double3 b ) {
		    return	a + ( b - a ) * value;
		}

		public static double4 Lerp ( this float value, double4 a, double4 b ) {
		    return	a + ( b - a ) * value;
		}

		public static LinearInterpolator LerpTo ( this double value, double to, double range ) {
			return	new LinearInterpolator ( value, to, range );
		}

		public static LinearInterpolator2 LerpTo ( this double2 value, double2 to, double range ) {
			return	new LinearInterpolator2 ( value, to, range );
		}

		public static LinearInterpolator3 LerpTo ( this double3 value, double3 to, double range ) {
			return	new LinearInterpolator3 ( value, to, range );
		}
		#endregion Linear Interpolation

		#region Perspective Correct Linear Interpolation
		public static double ZLerp ( this double value, double a, double b, double az, double bz ) {
		    double zInv = value.Lerp ( 1 / az, 1 / bz );

		    return	value.Lerp ( a / az, b / bz ) / zInv;
		}

		public static double ZLerp ( this double value, double a, double b, double az, double bz, double zInv ) {
		    return	value.Lerp ( a / az, b / bz ) / zInv;
		}

		public static double2 ZLerp ( this double value, double2 a, double2 b, double az, double bz ) {
		    return	new double2 ( value.ZLerp ( a.x, b.x, az, bz ), value.ZLerp ( a.y, b.y, az, bz ) );
		}

		public static double2 ZLerp ( this double value, double2 a, double2 b, double az, double bz, double zInv ) {
		    return	new double2 ( value.ZLerp ( a.x, b.x, az, bz, zInv ), value.ZLerp ( a.y, b.y, az, bz, zInv ) );
		}

		public static ZLinearInterpolator ZLerpTo ( this double value, double to, double range, double startZ, double endZ ) {
			return	new ZLinearInterpolator ( value, to, range, startZ, endZ );
		}

		public static ZLinearInterpolator2 ZLerpTo ( this double2 value, double2 to, double range, double startZ, double endZ ) {
			return	new ZLinearInterpolator2 ( value, to, range, startZ, endZ );
		}
		#endregion Perspective Correct Linear Interpolation
	}
}