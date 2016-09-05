using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace RayTrace {
	public class PlanarSurface : Traceable {
		#region Properties
		Plane origPlane;
		double3 origPlaneAxisX, origPlaneAxisY;
		Plane transPlane;
		double3 transPlaneAxisX, transPlaneAxisY;

		public double3 Normal {
		    get { return	transPlane.n; }
			set {
				if ( transPlane.n == value )
					return;

				origPlane.n = ModelMatrix.Transposed.Transform ( value );
				InitOriginalPlane ();
				InitTransformedPlane ();
			}
		}

		// FIXIT: implement P0 (need to implement double4x4.Inverted property)
		// also revise Normal (ModelMatrix.Transposed.Transform)

		public override double4x4 ModelMatrix {
			get { return	base.ModelMatrix; }
			set {
				if ( value == base.ModelMatrix )
					return;

				base.ModelMatrix = value;
				InitTransformedPlane ();
			}
		}
		#endregion Properties

		#region Constructors
		public PlanarSurface ( Plane plane ) {
			this.origPlane = plane;
			InitOriginalPlane ();
			InitTransformedPlane ();
		}

		public PlanarSurface ( double3 n, double d ) : this ( new Plane ( n, d ) ) {}
		public PlanarSurface ( double3 p, double3 n ) : this ( Plane.Create ( p, n ) ) {}
		public PlanarSurface ( double3 p1, double3 p2, double3 p3 ) : this ( Plane.Create ( p1, p2, p3 ) ) {}
		#endregion Constructors

		#region Methods
		void InitOriginalPlane () {
			double4x4 orient = double4x4.RotVV ( double3.UnitY, origPlane.n );
			this.origPlaneAxisX = double3.UnitX * orient;
			this.origPlaneAxisY = double3.UnitZ * orient;
		}

		void InitTransformedPlane () {
			this.transPlane = origPlane.Transform ( ModelMatrix );
			this.transPlaneAxisX = ModelMatrix.Transform ( origPlaneAxisX );
			this.transPlaneAxisY = ModelMatrix.Transform ( origPlaneAxisY );
		}

		public override bool MayIntersect ( Ray r ) {
			return	true;
		}

		public override List <IntersectData> Intersect ( Ray r ) {
			List <IntersectData> isecs = new List <IntersectData> ();
			double3 p;
			PlaneIRayResult result = transPlane.Intersect ( r, out p );

			if ( result == PlaneIRayResult.Intersects )
				isecs.Add ( new IntersectData ( p, this ) );

			return	isecs;
		}

		public override double3 GetNormal ( IntersectData data ) {
			return	transPlane.n;
		}

		public override double2 GetTexCoord ( IntersectData data ) {
			double3 p = data.P - transPlane.P0;
			double2 t = new double2 ( transPlaneAxisX & p, transPlaneAxisY & p );

			// Scaling
			t.x *= 0.3;
			t.y *= 0.3;

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
