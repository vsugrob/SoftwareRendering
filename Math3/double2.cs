using System;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Math3d {
	[StructLayout ( LayoutKind.Explicit )]
	public partial struct double2 {
		#region Fields
		[FieldOffset ( 0 )]
		public double x;
		[FieldOffset ( 8 )]
		public double y;
		[EditorBrowsable ( EditorBrowsableState.Advanced )]
		[FieldOffset ( 0 )]
		public double r;
		[EditorBrowsable ( EditorBrowsableState.Advanced )]
		[FieldOffset ( 8 )]
		public double g;
		[EditorBrowsable ( EditorBrowsableState.Advanced )]
		[FieldOffset ( 0 )]
		public double s;
		[EditorBrowsable ( EditorBrowsableState.Advanced )]
		[FieldOffset ( 8 )]
		public double t;
		#endregion Fields

		#region Constants
		public static readonly double2 Zero = new double2 ( 0 );
		public static readonly double2 UnitX = new double2 ( 1, 0 );
		public static readonly double2 UnitY = new double2 ( 0, 1 );
		#endregion Constants

		#region Properties
		public double Length {
			get { return	Math.Sqrt ( x * x + y * y ); }
			set {
				double2 n = this.Normalized;

				x = n.x * value;
				y = n.y * value;
			}
		}

		public double LengthSq {
			get { return	x * x + y * y; }
			set {
				double2 n = this.Normalized;
				double l = Math.Sqrt ( value );
				x = n.x * l;
				y = n.y * l;
			}
		}

		public double2 Normalized {
			get {
				double s = 1 / this.Length;

				return	new double2 ( x * s, y * s );
			}
		}

		public unsafe double this [int c] {
			get {
				if ( c > 2 || c < 0 )
					throw new IndexOutOfRangeException ();

				fixed ( double * ptr = &this.x ) {
					return	*( ptr + c );
				}
			}
			set {
				if ( c > 2 || c < 0 )
					throw new IndexOutOfRangeException ();

				fixed ( double * ptr = &this.x ) {
					*( ptr + c ) = value;
				}
			}
		}
		#endregion Properties

		#region Constructors
		public double2 ( double x, double y ) : this () {
			this.x = x;  this.y = y; 
		}

		public double2 ( double a ) : this ( a, a ) {}

		public double2 ( double2 v ) : this () {
			this.x = v.x;  this.y = v.y;
		}

		#endregion Constructors

		#region Methods
		public static double D ( double2 a, double2 b ) {
			return	( a - b ).Length;
		}

		public static double DSq ( double2 a, double2 b ) {
			return	( a - b ).LengthSq;
		}

		public double2 Reflect ( double2 n ) {
			return	( 2 * n * ( this & n ) - this ).Normalized;
		}

		public double2 Reflect ( double2 n, double nDotV ) {
			return	( 2 * n * nDotV - this ).Normalized;
		}

		public double2 ReflectI ( double2 n ) {
			return	( this - 2 * n * ( this & n ) ).Normalized;
		}

		public double2 ReflectI ( double2 n, double nDotV ) {
			return	( this - 2 * n * nDotV ).Normalized;
		}

		public double2 Refract ( double2 n, double nDotV, double k ) {
			double cosF = Math.Sqrt ( 1 - k * k * ( 1 - nDotV * nDotV ) );
					
			if ( nDotV >= 0 )
				return	n * ( k * nDotV - cosF ) - this * k;
			else
				return	n * ( k * nDotV + cosF ) - this * k;
		}

		public double2 Refract ( double2 n, double k ) {
			return	this.Refract ( n, n & this, k );
		}

		public double2 RefractI ( double2 n, double nDotV, double k ) {
			double cosF = Math.Sqrt ( 1 - k * k * ( 1 - nDotV * nDotV ) );
			nDotV = -nDotV;
			
			if ( nDotV >= 0 )
				return	n * ( k * nDotV - cosF ) + this * k;
			else
				return	n * ( k * nDotV + cosF ) + this * k;
		}

		public double2 RefractI ( double2 n, double k ) {
			return	this.RefractI ( n, n & this, k );
		}

		#endregion Methods

		#region Overrides
		public override bool Equals ( object obj ) {
			if ( obj == null || !( obj is double2 ) )
				return	false;

			double2 v = ( double2 ) obj;

			return	Math.Abs ( x - v.x ) <= Math3.DIFF_THR && Math.Abs ( y - v.y ) <= Math3.DIFF_THR;
		}

		public override int GetHashCode () {
			int ix = ( int ) ( x * Math3.DOUBLE_PRECISION );
			int iy = ( int ) ( y * Math3.DOUBLE_PRECISION );
			
			return	ix ^ iy;
		}

		public override string ToString () {
			return	string.Format ( "x: {1}, y: {2}, length: {0}", Length, x, y );
		}

		#endregion Overrides

		#region Operators
		public static double2 operator + ( double2 a, double2 b ) {
			return	new double2 ( a.x + b.x, a.y + b.y );
		}

		public static double2 operator - ( double2 a, double2 b ) {
			return	new double2 ( a.x - b.x, a.y - b.y );
		}

		public static double2 operator ^ ( double2 a, double2 b ) {
			return	new double2 ( a.x * b.x, a.y * b.y );
		}

		public static double operator & ( double2 a, double2 b ) {
			return	a.x * b.x + a.y * b.y;
		}

		public static double2 operator * ( double2 v, double s ) {
			return	new double2 ( v.x * s, v.y * s );
		}

		public static double2 operator * ( double s, double2 v ) {
			return	new double2 ( v.x * s, v.y * s );
		}

		public static double2 operator / ( double2 v, double s ) {
			return	new double2 ( v.x / s, v.y / s );
		}

		public static double2 operator + ( double2 v ) {
			return	v;
		}

		public static double2 operator - ( double2 v ) {
			return	new double2 ( -v.x, -v.y );
		}

		public static bool operator == ( double2 a, double2 b ) {
			return	Math.Abs ( a.x - b.x ) <= Math3.DIFF_THR && Math.Abs ( a.y - b.y ) <= Math3.DIFF_THR;
		}

		public static bool operator != ( double2 a, double2 b ) {
			return	Math.Abs ( a.x - b.x ) > Math3.DIFF_THR || Math.Abs ( a.y - b.y ) > Math3.DIFF_THR;
		}

		public static implicit operator double2 ( double value ) {
			return	new double2 ( value );
		}
		#endregion Operators

	}
}
