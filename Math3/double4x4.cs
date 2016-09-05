using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Math3d {
	[StructLayout ( LayoutKind.Sequential, Pack=1 )]
	public struct double4x4 {
		#region Fields
		public double _11;
		public double _12;
		public double _13;
		public double _14;
		public double _21;
		public double _22;
		public double _23;
		public double _24;
		public double _31;
		public double _32;
		public double _33;
		public double _34;
		public double _41;
		public double _42;
		public double _43;
		public double _44;
		#endregion Fields

		#region Constants
		static readonly double4x4 IdentityConst = Scale ( 1 );
		#endregion Constants

		#region Properties
		public unsafe double this [int m, int n] {
			get {
				if ( m > 4 || m < 0 ||
					 n > 4 || n < 0 )
					throw new IndexOutOfRangeException ();

				fixed ( double * ptr = &this._11 ) {
					return	*( ptr + m * 4 + n );
				}
			}
			set {
				if ( m > 4 || m < 0 ||
					 n > 4 || n < 0 )
					throw new IndexOutOfRangeException ();

				fixed ( double * ptr = &this._11 ) {
					*( ptr + m * 4 + n ) = value;
				}
			}
		}

		#region Columns
		public double4 Column1 {
			get { return	new double4 ( _11, _21, _31, _41 ); }
			set {
				_11 = value.x;
				_21 = value.y;
				_31 = value.z;
				_41 = value.w;
			}
		}

		public double4 Column2 {
			get { return	new double4 ( _12, _22, _32, _42 ); }
			set {
				_12 = value.x;
				_22 = value.y;
				_32 = value.z;
				_42 = value.w;
			}
		}

		public double4 Column3 {
			get { return	new double4 ( _13, _23, _33, _43 ); }
			set {
				_13 = value.x;
				_23 = value.y;
				_33 = value.z;
				_43 = value.w;
			}
		}

		public double4 Column4 {
			get { return	new double4 ( _14, _24, _34, _44 ); }
			set {
				_14 = value.x;
				_24 = value.y;
				_34 = value.z;
				_44 = value.w;
			}
		}

		#endregion Columns

		#region Rows
		public double4 Row1 {
			get { return	new double4 ( _11, _12, _13, _14 ); }
			set {
				_11 = value.x;
				_12 = value.y;
				_13 = value.z;
				_14 = value.w;
			}
		}

		public double4 Row2 {
			get { return	new double4 ( _21, _22, _23, _24 ); }
			set {
				_21 = value.x;
				_22 = value.y;
				_23 = value.z;
				_24 = value.w;
			}
		}

		public double4 Row3 {
			get { return	new double4 ( _31, _32, _33, _34 ); }
			set {
				_31 = value.x;
				_32 = value.y;
				_33 = value.z;
				_34 = value.w;
			}
		}

		public double4 Row4 {
			get { return	new double4 ( _41, _42, _43, _44 ); }
			set {
				_41 = value.x;
				_42 = value.y;
				_43 = value.z;
				_44 = value.w;
			}
		}
		#endregion Rows

		public double4x4 Transposed {
			get {
				double4x4 mt = new double4x4 ();

				for ( int m = 0 ; m < 4 ; m++ )
					for ( int n = 0 ; n < 4 ; n++ )
						mt [n, m] = this [m, n];

				return	mt;
			}
		}

		public double Det3 {
			get {
				return	_11 * ( _22 * _33 - _32 * _23 ) +
						_12 * ( _23 * _31 - _33 * _21 ) +
						_13 * ( _21 * _32 - _22 * _31 );
			}
		}

		public double4 Translation {
			get {
				if ( Math3.MatrixLayout == MatrixLayout.RowMajor )
					return	Row4;
				else
					return	Column4;
			}
			set {
				if ( Math3.MatrixLayout == MatrixLayout.RowMajor )
					Row4 = value;
				else
					Column4 = value;
			}
		}

#if DEBUG
		public string _0Debug {
			get { return	ToString ( 4, true ); }
		}
#endif
		#endregion Properties

		#region Constructors
		public double4x4 ( double [,] elements ) : this () {
			for ( int m = 0 ; m < 4 ; m++ )
				for ( int n = 0 ; n < 4 ; n++ )
					this [m, n] = elements [m, n];
		}
		#endregion Constructors

		#region Factory Members
		public static double4x4 Identity { get { return	IdentityConst; } }

		public static double4x4 RotX ( double a, MatrixLayout layout = MatrixLayout.Default ) {
			double radA = Math.PI * a / 180;
			double cosA = Math.Cos ( radA );
			double sinA = Math.Sin ( radA );

			return	RotX ( cosA, sinA );
		}

		public static double4x4 RotX ( double cosA, double sinA, MatrixLayout layout = MatrixLayout.Default ) {
		    layout = Math3.GetMatrixLayout ( layout );

		    if ( layout == MatrixLayout.RowMajor ) {
		        return	new double4x4 ( new double [,] {
		            { 1,     0,    0, 0 },
					{ 0,  cosA, sinA, 0 },
					{ 0, -sinA, cosA, 0 },
					{ 0,     0,    0, 1 }
		        } );
		    } else {
		        return	new double4x4 ( new double [,] {
		            { 1,    0,     0, 0 },
					{ 0, cosA, -sinA, 0 },
					{ 0, sinA,  cosA, 0 },
					{ 0,    0,     0, 1 }
		        } );
		    }
		}

		public static double4x4 RotY ( double a, MatrixLayout layout = MatrixLayout.Default ) {
			double radA = Math.PI * a / 180;
			double cosA = Math.Cos ( radA );
			double sinA = Math.Sin ( radA );

			return	RotY ( cosA, sinA );
		}

		public static double4x4 RotY ( double cosA, double sinA, MatrixLayout layout = MatrixLayout.Default ) {
		    layout = Math3.GetMatrixLayout ( layout );

		    if ( layout == MatrixLayout.RowMajor ) {
		        return	new double4x4 ( new double [,] {
		            { cosA, 0, -sinA, 0 },
					{    0, 1,     0, 0 },
					{ sinA, 0,  cosA, 0 },
					{    0, 0,     0, 1 }
		        } );
		    } else {
		        return	new double4x4 ( new double [,] {
		            {  cosA, 0, sinA, 0 },
					{     0, 1,    0, 0 },
					{ -sinA, 0, cosA, 0 },
					{     0, 0,    0, 1 }
		        } );
		    }
		}

		public static double4x4 RotZ ( double a, MatrixLayout layout = MatrixLayout.Default ) {
			double radA = Math.PI * a / 180;
			double cosA = Math.Cos ( radA );
			double sinA = Math.Sin ( radA );

			return	RotZ ( cosA, sinA );
		}

		public static double4x4 RotZ ( double cosA, double sinA, MatrixLayout layout = MatrixLayout.Default ) {
		    layout = Math3.GetMatrixLayout ( layout );

		    if ( layout == MatrixLayout.RowMajor ) {
		        return	new double4x4 ( new double [,] {
		            {  cosA, sinA, 0, 0 },
					{ -sinA, cosA, 0, 0 },
					{     0,    0, 1, 0 },
					{     0,    0, 0, 1 }
		        } );
		    } else {
		        return	new double4x4 ( new double [,] {
		            { cosA, -sinA, 0, 0 },
					{ sinA,  cosA, 0, 0 },
					{    0,     0, 1, 0 },
					{    0,     0, 0, 1 }
		        } );
		    }
		}

		public static double4x4 RotToXYNearest ( double3 v, MatrixLayout layout = MatrixLayout.Default ) {
			if ( v.z == 0 )
				return	double4x4.Identity;

		    layout = Math3.GetMatrixLayout ( layout );
		    double projLen = Math.Sqrt ( v.x * v.x + v.z * v.z );
		    double cosA = v.x / projLen;
		    double sinA = v.z / projLen;

			if ( cosA < 0 ) {
				cosA = -cosA;
				sinA = -sinA;
			}

			return	RotY ( cosA, sinA, layout );
		}

		public static double4x4 RotToXY ( double3 v, MatrixLayout layout = MatrixLayout.Default ) {
			if ( v.z == 0 )
				return	double4x4.Identity;

		    layout = Math3.GetMatrixLayout ( layout );
		    double projLen = Math.Sqrt ( v.x * v.x + v.z * v.z );
		    double cosA = v.x / projLen;
		    double sinA = v.z / projLen;

			return	RotY ( cosA, sinA, layout );
		}

		public static double4x4 RotToXZNearest ( double3 v, MatrixLayout layout = MatrixLayout.Default ) {
			if ( v.y == 0 )
				return	double4x4.Identity;

		    layout = Math3.GetMatrixLayout ( layout );
		    double projLen = Math.Sqrt ( v.y * v.y + v.z * v.z );
		    double cosA = v.z / projLen;
		    double sinA = v.y / projLen;

			if ( cosA < 0 ) {
				cosA = -cosA;
				sinA = -sinA;
			}

			return	RotX ( cosA, sinA, layout );
		}

		public static double4x4 RotToXZ ( double3 v, MatrixLayout layout = MatrixLayout.Default ) {
			if ( v.y == 0 )
				return	double4x4.Identity;

		    layout = Math3.GetMatrixLayout ( layout );
		    double projLen = Math.Sqrt ( v.y * v.y + v.z * v.z );
		    double cosA = v.z / projLen;
		    double sinA = v.y / projLen;

			return	RotX ( cosA, sinA, layout );
		}

		public static double4x4 RotToYZNearest ( double3 v, MatrixLayout layout = MatrixLayout.Default ) {
			if ( v.x == 0 )
				return	double4x4.Identity;

		    layout = Math3.GetMatrixLayout ( layout );
		    double projLen = Math.Sqrt ( v.x * v.x + v.y * v.y );
		    double cosA = v.x / projLen;
		    double sinA = v.y / projLen;

			if ( cosA < 0 ) {
				cosA = -cosA;
				sinA = -sinA;
			}

			return	RotZ ( cosA, sinA, layout );
		}

		public static double4x4 RotToYZ ( double3 v, MatrixLayout layout = MatrixLayout.Default ) {
			if ( v.x == 0 )
				return	double4x4.Identity;

		    layout = Math3.GetMatrixLayout ( layout );
		    double projLen = Math.Sqrt ( v.x * v.x + v.y * v.y );
		    double cosA = v.x / projLen;
		    double sinA = v.y / projLen;

			return	RotZ ( cosA, sinA, layout );
		}

		public static double4x4 RotToXNearest ( double3 v, MatrixLayout layout = MatrixLayout.Default ) {
			if ( v.y == 0 && v.z == 0 )
				return	double4x4.Identity;

		    layout = Math3.GetMatrixLayout ( layout );
		    double projLen = Math.Sqrt ( v.y * v.y + v.z * v.z );
			double len = v.Length;
		    double cosA = v.x / len;
		    double sinA = projLen / len;

			if ( cosA < 0 ) {
				cosA = -cosA;
				sinA = -sinA;
			}

			if ( v.z < 0 )
				sinA = -sinA;

			return	RotToXZ ( v, layout ) * RotY ( cosA, sinA, layout );
		}

		public static double4x4 RotToX ( double3 v, MatrixLayout layout = MatrixLayout.Default ) {
			if ( v.y == 0 && v.z == 0 )
				return	double4x4.Identity;

		    layout = Math3.GetMatrixLayout ( layout );
		    double projLen = Math.Sqrt ( v.y * v.y + v.z * v.z );
			double len = v.Length;
		    double cosA = v.x / len;
		    double sinA = projLen / len;

			return	RotToXZ ( v, layout ) * RotY ( cosA, sinA, layout );
		}

		public static double4x4 RotToYNearest ( double3 v, MatrixLayout layout = MatrixLayout.Default ) {
			if ( v.x == 0 && v.z == 0 )
				return	double4x4.Identity;

		    layout = Math3.GetMatrixLayout ( layout );
		    double projLen = Math.Sqrt ( v.x * v.x + v.z * v.z );
			double len = v.Length;
		    double cosA = v.y / len;
		    double sinA = projLen / len;

			if ( cosA < 0 ) {
				cosA = -cosA;
				sinA = -sinA;
			}

			if ( v.x < 0 )
				sinA = -sinA;

			return	RotToXY ( v, layout ) * RotZ ( cosA, sinA, layout );
		}

		public static double4x4 RotToY ( double3 v, MatrixLayout layout = MatrixLayout.Default ) {
			if ( v.x == 0 && v.z == 0 )
				return	double4x4.Identity;

		    layout = Math3.GetMatrixLayout ( layout );
		    double projLen = Math.Sqrt ( v.x * v.x + v.z * v.z );
			double len = v.Length;
		    double cosA = v.y / len;
		    double sinA = projLen / len;

			return	RotToXY ( v, layout ) * RotZ ( cosA, sinA, layout );
		}

		public static double4x4 RotToZNearest ( double3 v, MatrixLayout layout = MatrixLayout.Default ) {
			if ( v.x == 0 && v.y == 0 )
				return	double4x4.Identity;

		    layout = Math3.GetMatrixLayout ( layout );
		    double projLen = Math.Sqrt ( v.x * v.x + v.y * v.y );
			double len = v.Length;
		    double cosA = v.z / len;
		    double sinA = projLen / len;

			if ( cosA < 0 ) {
				cosA = -cosA;
				sinA = -sinA;
			}

			if ( v.z < 0 )
				sinA = -sinA;

			return	RotToYZ ( v, layout ) * RotX ( cosA, sinA, layout );
		}

		public static double4x4 RotToZ ( double3 v, MatrixLayout layout = MatrixLayout.Default ) {
			if ( v.x == 0 && v.y == 0 )
				return	double4x4.Identity;

		    layout = Math3.GetMatrixLayout ( layout );
		    double projLen = Math.Sqrt ( v.x * v.x + v.y * v.y );
			double len = v.Length;
		    double cosA = v.z / len;
		    double sinA = projLen / len;

			return	RotToYZ ( v, layout ) * RotX ( cosA, sinA, layout );
		}

		public static double4x4 RotV ( double3 v, double a, MatrixLayout layout = MatrixLayout.Default ) {
			double radA = Math.PI * a / 180;
			double cosA = Math.Cos ( radA );
			double sinA = Math.Sin ( radA );

			return	RotV ( v, cosA, sinA, layout );
		}

		public static double4x4 RotV ( double3 v, double cosA, double sinA, MatrixLayout layout = MatrixLayout.Default ) {
		    layout = Math3.GetMatrixLayout ( layout );
			double4x4 rotToY = double4x4.RotToY ( v, layout );

			return	rotToY * double4x4.RotY ( cosA, sinA, layout ) * rotToY.Transposed;
		}

		public static double4x4 RotVV ( double3 v1, double3 v2, MatrixLayout layout = MatrixLayout.Default ) {
		    layout = Math3.GetMatrixLayout ( layout );
			double v1Len = v1.Length;
			double v2Len = v2.Length;
			double lenProd = v1Len * v2Len;
			double proj = v1 & v2;

			if ( Math.Abs ( lenProd - proj ) <= Math3.DIFF_THR )		// Are (+) collinear
				return	double4x4.Identity;
			else if ( Math.Abs ( - lenProd - proj ) <= Math3.DIFF_THR )	// Are (-) collinear
				return	double4x4.ScaleV ( 1, -1, -1, v1 );

			double oppositeSide = Math.Sqrt ( v2Len * v2Len - proj * proj );
			double cosA = proj / v2Len;
			double sinA = oppositeSide / v2Len;
			double3 rotationAxis = ( v1 * v2 ).Normalized;

			return	double4x4.RotV ( rotationAxis, cosA, sinA, layout );
		}

		public static double4x4 Trans ( double3 v, MatrixLayout layout = MatrixLayout.Default ) {
		    layout = Math3.GetMatrixLayout ( layout );
			double4x4 m = Identity;

		    if ( layout == MatrixLayout.RowMajor ) {
		        m [3, 0] = v.x;
				m [3, 1] = v.y;
				m [3, 2] = v.z;
		    } else {
		        m [0, 3] = v.x;
				m [1, 3] = v.y;
				m [2, 3] = v.z;
		    }

			return	m;
		}

		public static double4x4 Scale ( double s ) {
			double4x4 m = new double4x4 ();

			for ( int i = 0 ; i < 3 ; i++ )
				m [i, i] = s;

		    m [3, 3] = 1;

			return	m;
		}

		public static double4x4 Scale ( double sx, double sy, double sz ) {
			double4x4 m = new double4x4 ();

			m [0, 0] = sx;
			m [1, 1] = sy;
			m [2, 2] = sz;
		    m [3, 3] = 1;

			return	m;
		}

		public static double4x4 Scale ( double3 v ) {
			double4x4 m = new double4x4 ();

			for ( int i = 0 ; i < 3 ; i++ )
				m [i, i] = v [i];

		    m [3, 3] = 1;

			return	m;
		}

		public static double4x4 ScaleV ( double sv, double3 v ) {
			double4x4 rotToY = double4x4.RotToY ( v );
			double4x4 m = rotToY * double4x4.Scale ( new double3 ( 1, sv, 1 ) ) * rotToY.Transposed;

			return	m;
		}

		public static double4x4 ScaleV ( double sx, double sy, double sz, double3 v ) {
			double4x4 rotToY = double4x4.RotToY ( v );
			double4x4 m = rotToY * double4x4.Scale ( new double3 ( sx, sy, sz ) ) * rotToY.Transposed;

			return	m;
		}

		public static double4x4 Frame ( double3 axisX, double3 axisY, double3 axisZ, double3 pos, MatrixLayout layout = MatrixLayout.Default ) {
		    layout = Math3.GetMatrixLayout ( layout );
			double4x4 orient = double4x4.Identity;
			double4x4 trans = double4x4.Trans ( pos, layout );

		    if ( layout == MatrixLayout.RowMajor ) {
		        orient.Column1 = new double4 ( axisX, 0 );
				orient.Column2 = new double4 ( axisY, 0 );
				orient.Column3 = new double4 ( axisZ, 0 );
		    } else {
		        orient.Row1 = new double4 ( axisX, 0 );
				orient.Row2 = new double4 ( axisY, 0 );
				orient.Row3 = new double4 ( axisZ, 0 );
		    }

			return	trans * orient;
		}

		public static double4x4 FrameInv ( double3 axisX, double3 axisY, double3 axisZ, double3 pos, MatrixLayout layout = MatrixLayout.Default ) {
		    layout = Math3.GetMatrixLayout ( layout );
			double4x4 orient = double4x4.Identity;
			double4x4 trans = double4x4.Trans ( pos, layout );

		    if ( layout == MatrixLayout.RowMajor ) {
		        orient.Column1 = new double4 ( axisX, 0 );
				orient.Column2 = new double4 ( axisY, 0 );
				orient.Column3 = new double4 ( axisZ, 0 );
		    } else {
		        orient.Row1 = new double4 ( axisX, 0 );
				orient.Row2 = new double4 ( axisY, 0 );
				orient.Row3 = new double4 ( axisZ, 0 );
		    }

			return	orient.Transposed * trans;
		}
		#endregion Factory Members

		#region Overrides
		public override bool Equals ( object obj ) {
			if ( obj == null || !( obj is double4x4 ) )
				return	false;

			double4x4 b = ( double4x4 ) obj;

			for ( int m = 0 ; m < 4 ; m++ ) {
				for ( int n = 0 ; n < 4 ; n++ ) {
					if ( Math.Abs ( this [m, n] - b [m, n] ) > Math3.DIFF_THR )
						return	false;
				}
			}

			return	true;
		}

		public override int GetHashCode () {
			int hash = 0;

			for ( int m = 0 ; m < 4 ; m++ ) {
				for ( int n = 0 ; n < 4 ; n++ ) {
					hash ^= ( int ) ( this [m, n] * Math3.DOUBLE_PRECISION );
				}
			}

		    return	hash;
		}

		public override string ToString () {
		    return	ToString ( 4, false, ", ", "; " );
		}

		public string ToString ( int fracDigits, bool allowPadding = false,
			string elementSeparator = ", ", string rowSeparator = ", \r\n" )
		{
			string [,] matStrings = new string [4, 4];

			for ( int m = 0 ; m < 4 ; m++ )
				for ( int n = 0 ; n < 4 ; n++ )
					matStrings [m, n] = Math.Round ( this [m, n], fracDigits ).ToString ();

			int [] maxLengths = new int [4];

			for ( int c = 0 ; c < 4 ; c++ )
				maxLengths [c] = Enumerable.Range ( 0, 4 ).Max ( r => matStrings [r, c].Length );

			if ( allowPadding ) {
				for ( int m = 0 ; m < 4 ; m++ )
					for ( int n = 0 ; n < 4 ; n++ )
						matStrings [m, n] = matStrings [m, n].PadLeft ( maxLengths [n], ' ' );
			}

			string [] rowStrings = new string [4];

			for ( int r = 0 ; r < 4 ; r++ )
				rowStrings [r] = string.Join ( elementSeparator, Enumerable.Range ( 0, 4 ).Select ( c => matStrings [r, c] ) );

			string matString = string.Join ( rowSeparator, rowStrings );

			return	matString;
		}
		#endregion Overrides

		#region Operators
		public static double4 operator * ( double4x4 m, double4 v ) {
			return	new double4 ( m.Row1 & v, m.Row2 & v, m.Row3 & v, m.Row4 & v );
		}

		public static double3 operator * ( double4x4 m, double3 v ) {
			return	new double3 ( m.Row1 & v, m.Row2 & v, m.Row3 & v );
		}

		public static double4 operator * ( double4 v, double4x4 m ) {
			return	new double4 ( m.Column1 & v, m.Column2 & v, m.Column3 & v, m.Column4 & v );
		}

		public static double3 operator * ( double3 v, double4x4 m ) {
			return	new double3 ( m.Column1 & v, m.Column2 & v, m.Column3 & v );
		}

		public static double4x4 operator * ( double4x4 a, double4x4 b ) {
			double4x4 c = new double4x4 ();
			c.Row1 = a.Row1 * b;
			c.Row2 = a.Row2 * b;
			c.Row3 = a.Row3 * b;
			c.Row4 = a.Row4 * b;

			return	c;
		}

		public static bool operator == ( double4x4 a, double4x4 b ) {
			for ( int m = 0 ; m < 4 ; m++ ) {
				for ( int n = 0 ; n < 4 ; n++ ) {
					if ( Math.Abs ( a [m, n] - b [m, n] ) > Math3.DIFF_THR )
						return	false;
				}
			}

			return	true;
		}

		public static bool operator != ( double4x4 a, double4x4 b ) {
			for ( int m = 0 ; m < 4 ; m++ ) {
				for ( int n = 0 ; n < 4 ; n++ ) {
					if ( Math.Abs ( a [m, n] - b [m, n] ) > Math3.DIFF_THR )
						return	true;
				}
			}

			return	false;
		}
		#endregion Operators

		#region Methods
		public double4 Transform ( double4 v, MatrixLayout layout = MatrixLayout.Default ) {
		    layout = Math3.GetMatrixLayout ( layout );

		    if ( layout == MatrixLayout.RowMajor )
		        return	v * this;
		    else
		        return	this * v;
		}

		public double3 Transform ( double3 v, MatrixLayout layout = MatrixLayout.Default ) {
		    layout = Math3.GetMatrixLayout ( layout );

		    if ( layout == MatrixLayout.RowMajor )
		        return	v * this;
		    else
		        return	this * v;
		}
		#endregion Methods
	}
}
