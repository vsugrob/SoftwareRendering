using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace RayTrace {
	public enum DebugMaterialMode {
		Checker, TexCoord, Tangent, Binormal, Normal
	}

	public class DebugMaterial : Material {
		#region Properties
		public DebugMaterialMode Mode;
		#endregion Properties

		#region Constructors
		public DebugMaterial ( DebugMaterialMode mode = DebugMaterialMode.Checker ) {
			this.Mode = mode;
		}
		#endregion Constructors

		public override double3 CalculateColor ( Scene scene, Traceable traceable,
			IntersectData data, Ray ray, TraceData traceData )
		{
			if ( Mode == DebugMaterialMode.Checker ) {
				int numSquares = 10;
				double3 c1 = 0.25;
				double3 c2 = 0.75;
				double2 t = traceable.GetTexCoord ( data );
				int nx = ( int ) Math.Round ( t.x * numSquares );
				int ny = ( int ) Math.Round ( t.y * numSquares );

				return	( nx % 2 == 0 ) == ( ny % 2 == 0 ) ? c1 : c2;
			} else {
				return	new double3 ( traceable.GetTexCoord ( data ), 0 );
			}
		}
	}
}
