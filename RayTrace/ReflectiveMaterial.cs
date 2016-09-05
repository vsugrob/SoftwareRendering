using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace RayTrace {
	public class ReflectiveMaterial : Material {
		#region Constants
		public const double DefaultReflectionAttenuation = 0.9;
		#endregion Constants

		#region Properties
		public double ReflectionAttenuation;
		#endregion Properties

		#region Constructors
		public ReflectiveMaterial ( double reflectionAttenuation = DefaultReflectionAttenuation ) {
			this.ReflectionAttenuation = reflectionAttenuation;
		}
		#endregion Constructors

		#region Overrides
		public override double3 CalculateColor ( Scene scene, Traceable traceable,
			IntersectData data, Ray ray, TraceData traceData )
		{
			if ( traceData.Reflections > 0 ) {
				double3 n = traceable.GetNormal ( data );
				double3 r = ray.l.ReflectI ( n );
				double3 p = traceable.Advance ( data.P, r );

				return	ReflectionAttenuation * scene.Trace ( new Ray ( p, r ), traceData.GetReflected () );
			} else {
				TraceData.ReflectionLimitExceedCount++;

				return	TraceData.ReflectionLimitExceedColor;
			}
		}
		#endregion Overrides
	}
}
