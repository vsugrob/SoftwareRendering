using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math3d {
	public enum MatrixLayout {
		Default, RowMajor, ColumnMajor
	}

	public enum ClassifyResult {
		InFront, OnSurface, Behind
	}

	public enum Classify3Result {
		Distinct, HasCoincidence, AllCoincident
	}

	public static class Math3 {
		#region Constants
		public const int DEFAULT_DOUBLE_PRECISION = 12;
		public const double DEFAULT_DIFF_THR = 1e-12;
		public const double DEFAULT_LOW_DIFF_THR = 1e-4;	//1e-6;
		#endregion Constants

		#region Runtime Settings
		public static int DOUBLE_PRECISION = DEFAULT_DOUBLE_PRECISION;
		public static double DIFF_THR = DEFAULT_DIFF_THR;
		public static double DIFF_THR_SQ = DIFF_THR * DIFF_THR;
		public static double LOW_DIFF_THR = DEFAULT_LOW_DIFF_THR;
		public static double LOW_DIFF_THR_SQ = LOW_DIFF_THR * LOW_DIFF_THR;

		static MatrixLayout matrixLayout = MatrixLayout.RowMajor;
		public static MatrixLayout MatrixLayout {
			get { return	matrixLayout; }
			set {
				matrixLayout = value != MatrixLayout.Default ?
					value : MatrixLayout.RowMajor;
			}
		}

		public static MatrixLayout GetMatrixLayout ( MatrixLayout localLayoutSetting ) {
			return	localLayoutSetting != MatrixLayout.Default ?
				localLayoutSetting : Math3.MatrixLayout;
		}
		#endregion Runtime Settings

		#region Methods
		public static bool ClampLine ( ref int x0, ref int y0, ref int x1, ref int y1, int w, int h ) {
			if ( x0 == x1 ) {
				int h1 = h - 1;

				if ( ( x0 < 0 || x0 > h1 ) ||
					 ( y0 < 0 && y1 < 0 ) ||
					 ( y0 > h1 && y1 > h1 ) )
					return	false;

				y0 = y0.Clamp ( 0, h1 );
				y1 = y1.Clamp ( 0, h1 );
			} else if ( y0 == y1 ) {
				int w1 = w - 1;

				if ( ( y0 < 0 || y0 > w1 ) ||
					 ( x0 < 0 && x1 < 0 ) ||
					 ( x0 > w1 && x1 > w1 ) )
					return	false;

				x0 = x0.Clamp ( 0, w1 );
				x1 = x1.Clamp ( 0, w1 );
			} else {
				double k = ( double ) ( y1 - y0 ) / ( x1 - x0 );

				ClampNarrowLineEnd ( k, ref x0, ref y0, w, h );
				ClampNarrowLineEnd ( k, ref x1, ref y1, w, h );

				if ( ( x0 < 0 || x0 > w - 1 ||
					   x1 < 0 || x1 > w - 1 ||
					   y0 < 0 || y0 > h - 1 ||
					   y1 < 0 || y1 > h - 1 ) )
					return	false;
			}

			return	true;
		}
		
		public static void ClampNarrowLineEnd ( double k, ref int x, ref int y, int w, int h ) {
			if ( x < 0 ) {
				y = ( int ) Math.Round ( ( 0 - x ) * k + y );
				x = 0;
			} else if ( x >= w ) {
				y = ( int ) Math.Round ( ( w - 1 - x ) * k + y );
				x = w - 1;
			}

			if ( y < 0 ) {
				x = ( int ) Math.Round ( ( 0 - y ) / k + x );
				y = 0;
			} else if ( y >= h ) {
				x = ( int ) Math.Round ( ( h - 1 - y ) / k + x );
				y = h - 1;
			}
		}

		public static Classify3Result Classify3 <T> ( T [] vals, out int highIdx, out int midIdx, out int lowIdx,
			out int separateIdx, out int coincidentIdx )
			where T : IComparable <T>
		{
			Classify3Result res = Classify3Result.Distinct;
			separateIdx = 0;
			coincidentIdx = 0;

			int cmp10 = vals [1].CompareTo ( vals [0] );
			int cmp02 = vals [0].CompareTo ( vals [2] );

		    if ( cmp10 == 1 ) {	// 1 > 0
		        if ( cmp02 == 1 ) {	// 1 > 0 > 2
		            highIdx = 1;
		            midIdx = 0;
		            lowIdx = 2;
		        } else if ( cmp02 == -1 ) {	// 1 > 0 < 2 => 1 >= 2 > 0 || 2 > 1 > 0
		            lowIdx = 0;
					int cmp12 = vals [1].CompareTo ( vals [2] );

		            if ( cmp12 == 0 ) {	// 1 == 2 => 1 == 2 > 0
		                res = Classify3Result.HasCoincidence;
		                midIdx = 1;
		                highIdx = 1;
						separateIdx = 0;
						coincidentIdx = 2;
		            } else if ( cmp12 == 1 ) {	// 1 > 2 > 0
		                midIdx = 2;
		                highIdx = 1;
		            } else /*if ( cmp12 == -1 )*/ {	// 2 > 1 > 0
		                midIdx = 1;
		                highIdx = 2;
		            }
		        } else /*if ( cmp02 == 0 )*/ {	// 0 == 2 => 1 > 0 == 2
		            res = Classify3Result.HasCoincidence;
		            highIdx = 1;
		            midIdx = 0;
		            lowIdx = 0;
					separateIdx = 1;
					coincidentIdx = 2;
		        }
		    } else if ( cmp10 == -1 ) {	// 1 < 0
		        if ( cmp02 == 1 ) {	// 0 > 2 => 1 <= 2 < 0 || 2 < 1 < 0
		            highIdx = 0;
					int cmp12 = vals [1].CompareTo ( vals [2] );

		            if ( cmp12 == 0 ) {	// 1 == 2 => 1 == 2 < 0
		                res = Classify3Result.HasCoincidence;
		                midIdx = 1;
		                lowIdx = 1;
						separateIdx = 0;
						coincidentIdx = 2;
		            } else if ( cmp12 == 1 ) {	// 1 > 2 => 2 < 1 < 0
		                midIdx = 1;
		                lowIdx = 2;
		            } else /*if ( cmp12 == -1 )*/ {	// 1 < 2 => 1 < 2 < 0
		                midIdx = 2;
		                lowIdx = 1;
		            }
		        } else if ( cmp02 == -1 ) {	// 0 < 2 => 1 < 0 < 2
		            highIdx = 2;
		            midIdx = 0;
		            lowIdx = 1;
		        } else /*if ( cmp02 == 0 )*/ {	// 0 == 2 => 1 < 0 == 2
		            res = Classify3Result.HasCoincidence;
		            highIdx = 0;
		            midIdx = 0;
		            lowIdx = 1;
					separateIdx = 1;
					coincidentIdx = 2;
		        }
		    } else /*if ( cmp10 == 0 )*/ {	// 1 == 0
		        res = Classify3Result.HasCoincidence;
		        midIdx = 0;
				separateIdx = 2;
				coincidentIdx = 1;
				int cmp21 = vals [2].CompareTo ( vals [1] );

		        if ( cmp21 == -1 ) {	// 2 < 1 => 2 < 1 == 0
		            highIdx = 0;
		            lowIdx = 2;
		        } else if ( cmp21 == 1 ) {	// 2 > 1 => 2 > 1 == 0
		            highIdx = 2;
		            lowIdx = 0;
		        } else /*if ( cmp21 == 0 )*/ {	// 2 == 0 => 2 == 1 == 0
		            res = Classify3Result.AllCoincident;
					highIdx = 0;
					lowIdx = 0;
		        }
		    }

		    return	res;
		}

		public static double SignedArea ( double2 p0, double2 p1, double2 p2 ) {
			return	( ( p1.x - p0.x ) * ( p2.y - p0.y ) - ( p2.x - p0.x ) * ( p1.y - p0.y ) ) * 0.5;
		}

		public static double SignedArea2 ( double2 p0, double2 p1, double2 p2 ) {
			return	( p1.x - p0.x ) * ( p2.y - p0.y ) - ( p2.x - p0.x ) * ( p1.y - p0.y );
		}
		#endregion Methods
	}
}
