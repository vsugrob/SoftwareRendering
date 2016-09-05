using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace RayTrace {
	public class Material {
		public static readonly Material Default = new Material ();

		static Material () {
			Default = new PhongMaterial ();
		}

		public virtual double3 CalculateColor ( Scene scene, Traceable traceable,
			IntersectData data, Ray ray, TraceData traceData )
		{
			return	traceable.GetNormal ( data );
			//return	new double3 ( traceable.GetTexCoord ( data ), 1 );

			//// <Checker>
			//double2 texCoord = traceable.GetTexCoord ( data );
			//texCoord *= 20;
			//double3 evenColor = new double3 ( 0.8, 0.8, 0.8 );
			//double3 oddColor = new double3 ( 0.2, 0.2, 0.2 );

			//int x = ( int ) texCoord.x;
			//int y = ( int ) texCoord.y;
			//bool xIsEven = x % 2 == 0;
			//bool yIsEven = y % 2 == 0;

			//if ( xIsEven == yIsEven )
			//    return	evenColor;
			//else
			//    return	oddColor;
			//// </Checker>
		}
	}
}
