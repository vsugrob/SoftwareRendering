using System;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Math3d {
	[StructLayout ( LayoutKind.Explicit )]
	public partial struct double3 {
		#region Fields
		[FieldOffset ( 0 )]
		public double x;
		[FieldOffset ( 8 )]
		public double y;
		[FieldOffset ( 16 )]
		public double z;
		[EditorBrowsable ( EditorBrowsableState.Advanced )]
		[FieldOffset ( 0 )]
		public double r;
		[EditorBrowsable ( EditorBrowsableState.Advanced )]
		[FieldOffset ( 8 )]
		public double g;
		[EditorBrowsable ( EditorBrowsableState.Advanced )]
		[FieldOffset ( 16 )]
		public double b;
		[EditorBrowsable ( EditorBrowsableState.Advanced )]
		[FieldOffset ( 0 )]
		public double s;
		[EditorBrowsable ( EditorBrowsableState.Advanced )]
		[FieldOffset ( 8 )]
		public double t;
		[EditorBrowsable ( EditorBrowsableState.Advanced )]
		[FieldOffset ( 16 )]
		public double p;
		#endregion Fields

		#region Constants
		public static readonly double3 Zero = new double3 ( 0 );
		public static readonly double3 UnitX = new double3 ( 1, 0, 0 );
		public static readonly double3 UnitY = new double3 ( 0, 1, 0 );
		public static readonly double3 UnitZ = new double3 ( 0, 0, 1 );
		#endregion Constants

		#region Properties
		public double Length2 {
			get { return	Math.Sqrt ( x * x + y * y ); }
			set {
				double2 n = this.Normalized2;

				x = n.x * value;
				y = n.y * value;
			}
		}

		public double LengthSq2 {
			get { return	x * x + y * y; }
			set {
				double2 n = this.Normalized2;
				double l = Math.Sqrt ( value );
				x = n.x * l;
				y = n.y * l;
			}
		}

		public double2 Normalized2 {
			get {
				double s = 1 / this.Length;

				return	new double2 ( x * s, y * s );
			}
		}

		public double Length {
			get { return	Math.Sqrt ( x * x + y * y + z * z ); }
			set {
				double3 n = this.Normalized;

				x = n.x * value;
				y = n.y * value;
				z = n.z * value;
			}
		}

		public double LengthSq {
			get { return	x * x + y * y + z * z; }
			set {
				double3 n = this.Normalized;
				double l = Math.Sqrt ( value );
				x = n.x * l;
				y = n.y * l;
				z = n.z * l;
			}
		}

		public double3 Normalized {
			get {
				double s = 1 / this.Length;

				return	new double3 ( x * s, y * s, z * s );
			}
		}

		public unsafe double this [int c] {
			get {
				if ( c > 3 || c < 0 )
					throw new IndexOutOfRangeException ();

				fixed ( double * ptr = &this.x ) {
					return	*( ptr + c );
				}
			}
			set {
				if ( c > 3 || c < 0 )
					throw new IndexOutOfRangeException ();

				fixed ( double * ptr = &this.x ) {
					*( ptr + c ) = value;
				}
			}
		}
		#endregion Properties

		#region Constructors
		public double3 ( double x, double y, double z ) : this () {
			this.x = x;  this.y = y;  this.z = z; 
		}

		public double3 ( double a ) : this ( a, a, a ) {}

		public double3 ( double2 v, double z ) : this () {
			this.x = v.x;  this.y = v.y;  this.z = z;
		}

		public double3 ( double x, double2 v ) : this () {
			this.x = x;  this.y = v.x;  this.z = v.y;
		}

		public double3 ( double3 v ) : this () {
			this.x = v.x;  this.y = v.y;  this.z = v.z;
		}

		#endregion Constructors

		#region Methods
		public static double D ( double3 a, double3 b ) {
			return	( a - b ).Length;
		}

		public static double DSq ( double3 a, double3 b ) {
			return	( a - b ).LengthSq;
		}

		public double3 Reflect ( double3 n ) {
			return	( 2 * n * ( this & n ) - this ).Normalized;
		}

		public double3 Reflect ( double3 n, double nDotV ) {
			return	( 2 * n * nDotV - this ).Normalized;
		}

		public double3 ReflectI ( double3 n ) {
			return	( this - 2 * n * ( this & n ) ).Normalized;
		}

		public double3 ReflectI ( double3 n, double nDotV ) {
			return	( this - 2 * n * nDotV ).Normalized;
		}

		public double3 Refract ( double3 n, double nDotV, double k ) {
			double cosF = Math.Sqrt ( 1 - k * k * ( 1 - nDotV * nDotV ) );
					
			if ( nDotV >= 0 )
				return	n * ( k * nDotV - cosF ) - this * k;
			else
				return	n * ( k * nDotV + cosF ) - this * k;
		}

		public double3 Refract ( double3 n, double k ) {
			return	this.Refract ( n, n & this, k );
		}

		public double3 RefractI ( double3 n, double nDotV, double k ) {
			double cosF = Math.Sqrt ( 1 - k * k * ( 1 - nDotV * nDotV ) );
			nDotV = -nDotV;
			
			if ( nDotV >= 0 )
				return	n * ( k * nDotV - cosF ) + this * k;
			else
				return	n * ( k * nDotV + cosF ) + this * k;
		}

		public double3 RefractI ( double3 n, double k ) {
			return	this.RefractI ( n, n & this, k );
		}

		#endregion Methods

		#region Overrides
		public override bool Equals ( object obj ) {
			if ( obj == null || !( obj is double3 ) )
				return	false;

			double3 v = ( double3 ) obj;

			return	Math.Abs ( x - v.x ) <= Math3.DIFF_THR && Math.Abs ( y - v.y ) <= Math3.DIFF_THR && Math.Abs ( z - v.z ) <= Math3.DIFF_THR;
		}

		public override int GetHashCode () {
			int ix = ( int ) ( x * Math3.DOUBLE_PRECISION );
			int iy = ( int ) ( y * Math3.DOUBLE_PRECISION );
			int iz = ( int ) ( z * Math3.DOUBLE_PRECISION );
			
			return	ix ^ iy ^ iz;
		}

		public override string ToString () {
			return	string.Format ( "x: {1}, y: {2}, z: {3}, length: {0}", Length, x, y, z );
		}

		#endregion Overrides

		#region Operators
		public static double3 operator + ( double3 a, double3 b ) {
			return	new double3 ( a.x + b.x, a.y + b.y, a.z + b.z );
		}

		public static double3 operator - ( double3 a, double3 b ) {
			return	new double3 ( a.x - b.x, a.y - b.y, a.z - b.z );
		}

		public static double3 operator ^ ( double3 a, double3 b ) {
			return	new double3 ( a.x * b.x, a.y * b.y, a.z * b.z );
		}

		public static double operator & ( double3 a, double3 b ) {
			return	a.x * b.x + a.y * b.y + a.z * b.z;
		}

		public static double3 operator * ( double3 a, double3 b ) {
			return	new double3 ( a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x );
		}

		public static double3 operator * ( double3 v, double s ) {
			return	new double3 ( v.x * s, v.y * s, v.z * s );
		}

		public static double3 operator * ( double s, double3 v ) {
			return	new double3 ( v.x * s, v.y * s, v.z * s );
		}

		public static double3 operator / ( double3 v, double s ) {
			return	new double3 ( v.x / s, v.y / s, v.z / s );
		}

		public static double3 operator + ( double3 v ) {
			return	v;
		}

		public static double3 operator - ( double3 v ) {
			return	new double3 ( -v.x, -v.y, -v.z );
		}

		public static bool operator == ( double3 a, double3 b ) {
			return	Math.Abs ( a.x - b.x ) <= Math3.DIFF_THR && Math.Abs ( a.y - b.y ) <= Math3.DIFF_THR && Math.Abs ( a.z - b.z ) <= Math3.DIFF_THR;
		}

		public static bool operator != ( double3 a, double3 b ) {
			return	Math.Abs ( a.x - b.x ) > Math3.DIFF_THR || Math.Abs ( a.y - b.y ) > Math3.DIFF_THR || Math.Abs ( a.z - b.z ) > Math3.DIFF_THR;
		}

		public static implicit operator double3 ( double value ) {
			return	new double3 ( value );
		}

		public static implicit operator double2 ( double3 v ) {
			return	new double2 ( v.x, v.y );
		}
		#endregion Operators

	}
}
