using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace RayTrace {
	public class PhongEnvironmentMaterial : Material {
		#region Constants
		public const double DefaultReflectionAttenuation = 0.3;
		public const double DefaultSpecularPower = 25;
		#endregion Constants

		#region Properties
		public double3 BaseColor = new double3 ( 0.5, 0.5, 0.5 );
		public double ReflectionAttenuation;
		public double SpecularPower;
		#endregion Properties

		#region Constructors
		public PhongEnvironmentMaterial ( double reflectionAttenuation = DefaultReflectionAttenuation,
			double specularPower = DefaultSpecularPower )
		{
			this.ReflectionAttenuation = reflectionAttenuation;
			this.SpecularPower = specularPower;
		}

		public PhongEnvironmentMaterial ( double3 baseColor, double reflectionAttenuation = DefaultReflectionAttenuation,
			double specularPower = DefaultSpecularPower ):
			this ( reflectionAttenuation, specularPower )
		{
			this.BaseColor = baseColor;
		}
		#endregion Constructors

		public override double3 CalculateColor ( Scene scene, Traceable traceable,
			IntersectData data, Ray ray, TraceData traceData )
		{
			double3 resultColor = double3.Zero;
			double3 n = traceable.GetNormal ( data );

			foreach ( SpotLight light in scene.Lights ) {
			    double3 l = light.Pos - data.P;
			    double distance = l.Length;
			    l = l.Normalized;
				double nDotL = n & l;

				if ( nDotL <= 0 )
				    continue;

				double shadowEdgeAttenuation = 1;

				List <IntersectData> shadowIsecs = scene.Intersect ( new Ray ( traceable.Advance ( data.P, l ), l ) );

				if ( shadowIsecs.Count > 0 ) {
					IntersectData obstacleIsecData = shadowIsecs.OrderBy ( shadowIsecData => ( shadowIsecData.P - data.P ).Length ).First ();

					if ( ( obstacleIsecData.P - data.P ).Length < distance ) {
						double3 obstacleL = light.Pos - obstacleIsecData.P;
						obstacleL = obstacleL.Normalized;
						double3 obstacleN = obstacleIsecData.Object.GetNormal ( obstacleIsecData );
						double3 obstacleR = l.ReflectI ( obstacleN );

						shadowEdgeAttenuation = obstacleR & obstacleL;

						if ( shadowEdgeAttenuation <= 0 )
							continue;

						shadowEdgeAttenuation = Math.Pow ( shadowEdgeAttenuation, 3 );
					}
				}

			    double3 diffuse = BaseColor ^ light.DiffuseColor * nDotL;

			    double3 r = l.Reflect ( n, nDotL );
			    double rDotV = Math.Max ( 0, ( -ray.l ) & r );
			    double3 specular = Math.Pow ( rDotV, SpecularPower ) * light.SpecularColor;

			    double attenuation = Math.Log ( distance + light.AttenuationBase, light.AttenuationBase );

			    resultColor += ( diffuse + specular ) * shadowEdgeAttenuation / attenuation;
			}

			if ( traceData.Reflections > 0 ) {
				double3 r = ray.l.ReflectI ( n );
				double3 p = traceable.Advance ( data.P, r );
				double3 reflectionColor = scene.Trace ( new Ray ( p, r ), traceData.GetReflected () );

				resultColor += reflectionColor * ReflectionAttenuation;
			} else {
				TraceData.ReflectionLimitExceedCount++;

				return	TraceData.ReflectionLimitExceedColor;
			}

			return	resultColor + scene.AmbientColor;
		}
	}
}
