using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math3d {
	public enum PlaneIRayResult {
		Intersects, Parallel, OnPlane, NotIntersects
	}

	public struct Plane {
		#region Fields
		public double3 n;
		public double d;
		#endregion Fields

		#region Properties
		public double3 P0 {
			get { return	n * ( -d ); }
		}
		#endregion Properties

		#region Constructors
		public Plane ( double3 n, double d ) {
			this.n = n;
			this.d = d;
		}
		#endregion Constructors

		#region Factory Methods
		public static Plane Create ( double3 p, double3 n ) {
			return	new Plane ( n, -( n & p ) );
		}

		public static Plane Create ( double3 p1, double3 p2, double3 p3 ) {
			double3 n = ( p2 - p1 ) & ( p3 - p1 );

			return	new Plane ( n, -( n & p1 ) );
		}
		#endregion Factory Methods

		#region Methods
		public bool Contains ( double3 p ) {
			return	Distance ( p ) <= Math3.DIFF_THR;
		}

		public ClassifyResult Classify ( double3 p ) {
			double r = SignedDistance ( p );

			if ( r > Math3.DIFF_THR )
				return	ClassifyResult.InFront;
			else if ( r < -Math3.DIFF_THR )
				return	ClassifyResult.Behind;
			else
				return	ClassifyResult.OnSurface;
		}

		public double Distance ( double3 p ) {
			return	Math.Abs ( n & p + d );
		}

		public double SignedDistance ( double3 p ) {
			return	n & p + d;
		}

		public double3 Project ( double3 p ) {
			return	p - n * SignedDistance ( p );
		}

		public PlaneIRayResult Intersect ( Ray r, out double3 p ) {
			double nDotL = n & r.l;
			double3 v = P0 - r.p;
			p = new double3 ();

			if ( Math.Abs ( nDotL ) <= Math3.DIFF_THR ) {
				if ( v.LengthSq <= Math3.DIFF_THR_SQ )
					return	PlaneIRayResult.OnPlane;
				else
					return	PlaneIRayResult.Parallel;
			} else {
				double k = ( n & v ) / nDotL;

				if ( k < 0 )
				    return	PlaneIRayResult.NotIntersects;
				else {
					p = r.p + r.l * k;

					return	PlaneIRayResult.Intersects;
				}
			}
		}
		#endregion Methods

		#region Transforms
		public Plane Transform ( double4x4 m ) {
			return	Plane.Create ( m.Transform ( P0 ), m.Transform ( n ) );
		}
		#endregion Transforms

		#region Operators
		public static Plane operator * ( Plane p, double4x4 m ) {
			return	Plane.Create ( p.P0 * m, p.n * m );
		}

		public static Plane operator * ( double4x4 m, Plane p ) {
			return	Plane.Create ( m * p.P0, m * p.n );
		}
		#endregion Operators
	}
}
