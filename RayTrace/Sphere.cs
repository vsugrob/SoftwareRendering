using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace RayTrace {
	public class Sphere : Traceable {
		#region Properties
		public double Radius;
		#endregion Properties

		#region Constructors
		public Sphere ( double radius, double3 center ) : base () {
			this.Radius = radius;
			this.ModelMatrix = double4x4.Trans ( center );
		}
		#endregion Constructors

		#region Methods
		public override bool MayIntersect ( Ray r ) {
			double3 v = r.p - ModelMatrix.Translation;
			double lDotV = r.l & v;
			double d = lDotV * lDotV + Radius * Radius - v.LengthSq;

			return	d >= 0;
		}

		public override List <IntersectData> Intersect ( Ray r ) {
			List <IntersectData> isecs = new List <IntersectData> ();
			double3 v = r.p - ModelMatrix.Translation;
			double lDotV = r.l & v;
			double d = lDotV * lDotV + Radius * Radius - v.LengthSq;

			if ( d >= 0 && d <= Math3.DIFF_THR )
				isecs.Add ( new IntersectData ( -lDotV * r.l + r.p, this ) );
			else if ( d < 0 )
				return	isecs;
			else {
				double dRoot = Math.Sqrt ( d );
				double s1 = -lDotV + dRoot;
				double s2 = -lDotV - dRoot;

				if ( s1 >= 0 )
					isecs.Add ( new IntersectData ( s1 * r.l + r.p, this ) );

				if ( s2 >= 0 )
					isecs.Add ( new IntersectData ( s2 * r.l + r.p, this ) );
			}

			return	isecs;
		}

		public override double3 GetNormal ( IntersectData data ) {
			return	( data.P - ModelMatrix.Translation ).Normalized;
		}

		public override double2 GetTexCoord ( IntersectData data ) {
			double3 n = GetNormal ( data );
			n = ModelMatrix.Transform ( n );
			double asin = Math.Asin ( n.y );
			double ty = ( asin + Math.PI / 2 ) / Math.PI;
			double atan = Math.Atan2 ( n.z, n.x );
			double tx = ( atan + Math.PI ) / ( 2 * Math.PI );
			double2 t = new double2 ( tx, ty );

			// Scaling
			//t.x *= 0.5;
			//t.y *= 1;

			return	t;
		}

		public override double3 GetTangent ( IntersectData data ) {
			throw new NotImplementedException ();
		}

		public override double3 GetBinormal ( IntersectData data ) {
			throw new NotImplementedException ();
		}
		#endregion Methods
	}
}
