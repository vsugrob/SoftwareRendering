using System;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Math3d {
	[StructLayout ( LayoutKind.Explicit )]
	public partial struct double4 {
		#region Fields
		[FieldOffset ( 0 )]
		public double x;
		[FieldOffset ( 8 )]
		public double y;
		[FieldOffset ( 16 )]
		public double z;
		[FieldOffset ( 24 )]
		public double w;
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
		[FieldOffset ( 24 )]
		public double a;
		[EditorBrowsable ( EditorBrowsableState.Advanced )]
		[FieldOffset ( 0 )]
		public double s;
		[EditorBrowsable ( EditorBrowsableState.Advanced )]
		[FieldOffset ( 8 )]
		public double t;
		[EditorBrowsable ( EditorBrowsableState.Advanced )]
		[FieldOffset ( 16 )]
		public double p;
		[EditorBrowsable ( EditorBrowsableState.Advanced )]
		[FieldOffset ( 24 )]
		public double q;
		#endregion Fields

		#region Constants
		public static readonly double4 Zero = new double4 ( 0 );
		public static readonly double4 UnitX = new double4 ( 1, 0, 0, 0 );
		public static readonly double4 UnitY = new double4 ( 0, 1, 0, 0 );
		public static readonly double4 UnitZ = new double4 ( 0, 0, 1, 0 );
		public static readonly double4 UnitW = new double4 ( 0, 0, 0, 1 );
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

		public double Length3 {
			get { return	Math.Sqrt ( x * x + y * y + z * z ); }
			set {
				double3 n = this.Normalized3;

				x = n.x * value;
				y = n.y * value;
				z = n.z * value;
			}
		}

		public double LengthSq3 {
			get { return	x * x + y * y + z * z; }
			set {
				double3 n = this.Normalized3;
				double l = Math.Sqrt ( value );
				x = n.x * l;
				y = n.y * l;
				z = n.z * l;
			}
		}

		public double3 Normalized3 {
			get {
				double s = 1 / this.Length;

				return	new double3 ( x * s, y * s, z * s );
			}
		}

		public double Length {
			get { return	Math.Sqrt ( x * x + y * y + z * z + w * w ); }
			set {
				double4 n = this.Normalized;

				x = n.x * value;
				y = n.y * value;
				z = n.z * value;
				w = n.w * value;
			}
		}

		public double LengthSq {
			get { return	x * x + y * y + z * z + w * w; }
			set {
				double4 n = this.Normalized;
				double l = Math.Sqrt ( value );
				x = n.x * l;
				y = n.y * l;
				z = n.z * l;
				w = n.w * l;
			}
		}

		public double4 Normalized {
			get {
				double s = 1 / this.Length;

				return	new double4 ( x * s, y * s, z * s, w * s );
			}
		}

		public unsafe double this [int c] {
			get {
				if ( c > 4 || c < 0 )
					throw new IndexOutOfRangeException ();

				fixed ( double * ptr = &this.x ) {
					return	*( ptr + c );
				}
			}
			set {
				if ( c > 4 || c < 0 )
					throw new IndexOutOfRangeException ();

				fixed ( double * ptr = &this.x ) {
					*( ptr + c ) = value;
				}
			}
		}
		#endregion Properties

		#region Constructors
		public double4 ( double x, double y, double z, double w ) : this () {
			this.x = x;  this.y = y;  this.z = z;  this.w = w; 
		}

		public double4 ( double a ) : this ( a, a, a, a ) {}

		public double4 ( double2 v1, double2 v2 ) : this () {
			this.x = v1.x;  this.y = v1.y;  this.z = v2.x;  this.w = v2.y; 
		}

		public double4 ( double2 v, double z, double w ) : this () {
			this.x = v.x;  this.y = v.y;  this.z = z;  this.w = w; 
		}

		public double4 ( double x, double2 v, double w ) : this () {
			this.x = x;  this.y = v.x;  this.z = v.y;  this.w = w; 
		}

		public double4 ( double x, double y, double2 v ) : this () {
			this.x = x;  this.y = y;  this.z = v.x;  this.w = v.y; 
		}

		public double4 ( double3 v, double w ) : this () {
			this.x = v.x;  this.y = v.y;  this.z = v.z;  this.w = w; 
		}

		public double4 ( double x, double3 v ) : this () {
			this.x = x;  this.y = v.x;  this.z = v.y;  this.w = v.z; 
		}

		public double4 ( double4 v ) : this () {
			this.x = v.x;  this.y = v.y;  this.z = v.z;  this.w = v.w; 
		}

		#endregion Constructors

		#region Methods
		public static double D ( double4 a, double4 b ) {
			return	( a - b ).Length;
		}

		public static double DSq ( double4 a, double4 b ) {
			return	( a - b ).LengthSq;
		}

		#endregion Methods

		#region Overrides
		public override bool Equals ( object obj ) {
			if ( obj == null || !( obj is double4 ) )
				return	false;

			double4 v = ( double4 ) obj;

			return	Math.Abs ( x - v.x ) <= Math3.DIFF_THR && Math.Abs ( y - v.y ) <= Math3.DIFF_THR && Math.Abs ( z - v.z ) <= Math3.DIFF_THR && Math.Abs ( w - v.w ) <= Math3.DIFF_THR;
		}

		public override int GetHashCode () {
			int ix = ( int ) ( x * Math3.DOUBLE_PRECISION );
			int iy = ( int ) ( y * Math3.DOUBLE_PRECISION );
			int iz = ( int ) ( z * Math3.DOUBLE_PRECISION );
			int iw = ( int ) ( w * Math3.DOUBLE_PRECISION );
			
			return	ix ^ iy ^ iz ^ iw;
		}

		public override string ToString () {
			return	string.Format ( "x: {1}, y: {2}, z: {3}, w: {4}, length: {0}", Length, x, y, z, w );
		}

		#endregion Overrides

		#region Operators
		public static double4 operator + ( double4 a, double4 b ) {
			return	new double4 ( a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w );
		}

		public static double4 operator - ( double4 a, double4 b ) {
			return	new double4 ( a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w );
		}

		public static double4 operator ^ ( double4 a, double4 b ) {
			return	new double4 ( a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w );
		}

		public static double operator & ( double4 a, double4 b ) {
			return	a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		public static double3 operator * ( double4 a, double4 b ) {
			return	new double3 ( a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x );
		}

		public static double4 operator * ( double4 v, double s ) {
			return	new double4 ( v.x * s, v.y * s, v.z * s, v.w * s );
		}

		public static double4 operator * ( double s, double4 v ) {
			return	new double4 ( v.x * s, v.y * s, v.z * s, v.w * s );
		}

		public static double4 operator / ( double4 v, double s ) {
			return	new double4 ( v.x / s, v.y / s, v.z / s, v.w / s );
		}

		public static double4 operator + ( double4 v ) {
			return	v;
		}

		public static double4 operator - ( double4 v ) {
			return	new double4 ( -v.x, -v.y, -v.z, -v.w );
		}

		public static bool operator == ( double4 a, double4 b ) {
			return	Math.Abs ( a.x - b.x ) <= Math3.DIFF_THR && Math.Abs ( a.y - b.y ) <= Math3.DIFF_THR && Math.Abs ( a.z - b.z ) <= Math3.DIFF_THR && Math.Abs ( a.w - b.w ) <= Math3.DIFF_THR;
		}

		public static bool operator != ( double4 a, double4 b ) {
			return	Math.Abs ( a.x - b.x ) > Math3.DIFF_THR || Math.Abs ( a.y - b.y ) > Math3.DIFF_THR || Math.Abs ( a.z - b.z ) > Math3.DIFF_THR || Math.Abs ( a.w - b.w ) > Math3.DIFF_THR;
		}

		public static implicit operator double4 ( double value ) {
			return	new double4 ( value );
		}

		public static implicit operator double3 ( double4 v ) {
			return	new double3 ( v.x, v.y, v.z );
		}

		public static implicit operator double2 ( double4 v ) {
			return	new double2 ( v.x, v.y );
		}
		#endregion Operators

	}
}
