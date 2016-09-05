using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace RayTrace {
	public class Scene {
		#region Properties
		public List <Traceable> Objects = new List <Traceable> ();
		public List <SpotLight> Lights = new List <SpotLight> ();
		public double3 AmbientColor = new double3 ( 0.1, 0.1, 0.1 );
		public double3 NullColor = double3.Zero;
		public Action <TraceResult> TraceCallback = null;
		#endregion Properties

		#region Methods
		public List <IntersectData> Intersect ( Ray ray ) {
			List <IntersectData> isecs = new List <IntersectData> ();

			foreach ( Traceable obj in Objects ) {
				if ( obj.MayIntersect ( ray ) )
					isecs.AddRange ( obj.Intersect ( ray ) );
			}

			return	isecs;
		}

		public double3 Trace ( Ray ray, TraceData traceData ) {
			List <IntersectData> isecs = Intersect ( ray );

			if ( isecs.Count > 0 ) {
				IntersectData nearestIsec = isecs.OrderBy ( isecData => ( isecData.P - ray.p ).LengthSq ).First ();
				Material material = nearestIsec.Object.Material;
				double3 color = material.CalculateColor ( this, nearestIsec.Object, nearestIsec, ray, new TraceData ( traceData ) );

				if ( TraceCallback != null ) {
					TraceResult tr = new TraceResult ( ray, traceData, nearestIsec, color );
					TraceCallback ( tr );
				}

				return	color;
			} else
				return	NullColor;
		}
		#endregion Methods
	}

	public struct TraceResult {
		public Ray Ray;
		public TraceData TraceData;
		public IntersectData NearestIntersect;
		public double3 Color;

		public TraceResult ( Ray ray, TraceData traceData,
			IntersectData nearestIntersect, double3 color )
		{
			this.Ray = ray;
			this.TraceData = traceData;
			this.NearestIntersect = nearestIntersect;
			this.Color = color;
		}
	}
}
