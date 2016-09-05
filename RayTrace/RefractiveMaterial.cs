using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace RayTrace {
	public class RefractiveMaterial : Material {
		#region Constants
		public const double DefaultRefractionIndex = 1.34312;	// H2O Daimon et al. 2007 [20.0 °C; 0.18~1.13 µm]
		public const double DefaultRefractionAttenuation = 1;
		#endregion Constants

		#region Properties
		public double RefractionAttenuation;
		public RefractionIndex RefractionIndex;
		#endregion Properties

		#region Constructors
		public RefractiveMaterial ( double refractionAttenuation = DefaultRefractionAttenuation,
			double refractionIndex = DefaultRefractionIndex )
		{
			this.RefractionAttenuation = refractionAttenuation;
			this.RefractionIndex = new RefractionIndex ( refractionIndex );
		}
		#endregion Constructors

		#region Overrides
		public override double3 CalculateColor ( Scene scene, Traceable traceable,
			IntersectData data, Ray ray, TraceData traceData )
		{
			double3 n = traceable.GetNormal ( data );
			double nDotRay = n & ray.l;
			double criticalAngleCos = nDotRay <= 0 ? RefractionIndex.CriticalInAngleCos : RefractionIndex.CriticalOutAngleCos;

			if ( Math.Abs ( nDotRay ) >= criticalAngleCos ) {
				if ( traceData.Refractions > 0 ) {
					double k = nDotRay <= 0 ? RefractionIndex.CoefficientIn : RefractionIndex.CoefficientOut;
					double3 f = ray.l.RefractI ( n, nDotRay, k );
					double3 p = traceable.Advance ( data.P, f );
					Ray refractedRay = new Ray ( p, f );
					traceData = traceData.GetRefracted ();
					double3 color = scene.Trace ( refractedRay, traceData );

					return	RefractionAttenuation * color;
				} else {
					TraceData.RefractionLimitExceedCount++;

					return	TraceData.RefractionLimitExceedCount;
				}
			} else {
			    if ( traceData.Reflections > 0 ) {
			        double3 r = ray.l.ReflectI ( n, nDotRay );
			        double3 p = traceable.Advance ( data.P, r );
			        Ray reflectedRay = new Ray ( p, r );
			        traceData = traceData.GetReflected ();
			        double3 color = scene.Trace ( reflectedRay, traceData );

			        return	RefractionAttenuation * color;
			    } else {
			        TraceData.ReflectionLimitExceedCount++;

			        return	TraceData.ReflectionLimitExceedColor;
			    }
			}
		}
		#endregion Overrides
	}
}