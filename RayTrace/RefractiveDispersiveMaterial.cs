using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace RayTrace {
	public enum RefractiveDispersiveMaterialProperties {
		Chroma
	}

	public class RefractiveDispersiveMaterial : Material {
		#region Constants
		public const double DefaultRefractionAttenuation = 1;
		#endregion Constants

		#region Properties
		public double RefractionAttenuation;
		public RefractionIndex RedRefractionIndex;
		public RefractionIndex GreenRefractionIndex;
		public RefractionIndex BlueRefractionIndex;
		#endregion Properties

		#region Constructors
		public RefractiveDispersiveMaterial ( RefractionIndex redRefractionIndex,
			RefractionIndex greenRefractionIndex, RefractionIndex blueRefractionIndex,
			double refractionAttenuation = DefaultRefractionAttenuation )
		{
			this.RedRefractionIndex = redRefractionIndex;
			this.GreenRefractionIndex = greenRefractionIndex;
			this.BlueRefractionIndex = blueRefractionIndex;
			this.RefractionAttenuation = refractionAttenuation;
		}
		#endregion Constructors

		#region Overrides
		public override double3 CalculateColor ( Scene scene, Traceable traceable,
			IntersectData data, Ray ray, TraceData traceData )
		{
			double3 n = traceable.GetNormal ( data );
			double nDotRay = n & ray.l;
			bool traceLimitExceed = false;
			double red = 0, green = 0, blue = 0;
			ColorComponent? chroma = null;
			
			if ( traceData.Properties.ContainsKey ( RefractiveDispersiveMaterialProperties.Chroma ) )
				chroma = ( ColorComponent ) traceData.Properties [RefractiveDispersiveMaterialProperties.Chroma];

			if ( !chroma.HasValue || chroma == ColorComponent.Red ) {
				red = RefractRay ( scene, traceable, data, ray, new TraceData ( traceData, RefractiveDispersiveMaterialProperties.Chroma, ColorComponent.Red ),
					n, nDotRay, RedRefractionIndex, out traceLimitExceed ).r;
			}

			if ( traceLimitExceed )
				red = 0;

			if ( !chroma.HasValue || chroma == ColorComponent.Green ) {
				green = RefractRay ( scene, traceable, data, ray, new TraceData ( traceData, RefractiveDispersiveMaterialProperties.Chroma, ColorComponent.Green ),
					n, nDotRay, GreenRefractionIndex, out traceLimitExceed ).g;
			}

			if ( traceLimitExceed )
				green = 0;

			if ( !chroma.HasValue || chroma == ColorComponent.Blue ) {
				blue = RefractRay ( scene, traceable, data, ray, new TraceData ( traceData, RefractiveDispersiveMaterialProperties.Chroma, ColorComponent.Blue ),
					n, nDotRay, BlueRefractionIndex, out traceLimitExceed ).b;
			}

			if ( traceLimitExceed )
				blue = 0;

			double3 color = new double3 ( red, green, blue );

			return	color;
		}
		#endregion Overrides

		#region Methods
		public double3 RefractRay ( Scene scene, Traceable traceable,
			IntersectData data, Ray ray, TraceData traceData,
			double3 n, double nDotRay,
			RefractionIndex refractionIndex, out bool traceDataExceed )
		{
			traceDataExceed = false;
			double criticalAngleCos = nDotRay <= 0 ? refractionIndex.CriticalInAngleCos : refractionIndex.CriticalOutAngleCos;

			if ( Math.Abs ( nDotRay ) >= criticalAngleCos ) {
				if ( traceData.Refractions > 0 ) {
					double k = nDotRay <= 0 ? refractionIndex.CoefficientIn : refractionIndex.CoefficientOut;
					double3 f = ray.l.RefractI ( n, nDotRay, k );
					double3 p = traceable.Advance ( data.P, f );
					Ray refractedRay = new Ray ( p, f );
					traceData = traceData.GetRefracted ();
					double3 color = scene.Trace ( refractedRay, traceData );

					return	RefractionAttenuation * color;
				} else {
					TraceData.RefractionLimitExceedCount++;
					traceDataExceed = true;

					return	TraceData.RefractionLimitExceedColor;
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
					traceDataExceed = true;

			        return	TraceData.ReflectionLimitExceedColor;
			    }
			}
		}
		#endregion Methods

		#region Factory Methods
		public static RefractiveDispersiveMaterial Water ( double refractionAttenuation = DefaultRefractionAttenuation ) {
			RefractionIndex red = RefractionIndex.Water ( Light.RedWavelength );
			RefractionIndex green = RefractionIndex.Water ( Light.GreenWavelength );
			RefractionIndex blue = RefractionIndex.Water ( Light.BlueWavelength );

			return	new RefractiveDispersiveMaterial ( red, green, blue, refractionAttenuation );
		}

		public static RefractiveDispersiveMaterial Diamond ( double refractionAttenuation = DefaultRefractionAttenuation ) {
			RefractionIndex red = RefractionIndex.Diamond ( Light.RedWavelength );
			RefractionIndex green = RefractionIndex.Diamond ( Light.GreenWavelength );
			RefractionIndex blue = RefractionIndex.Diamond ( Light.BlueWavelength );

			return	new RefractiveDispersiveMaterial ( red, green, blue, refractionAttenuation );
		}
		#endregion Factory Methods
	}
}