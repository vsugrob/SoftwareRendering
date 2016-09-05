using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;
using Common3d;

namespace RayTrace {
	public class MappedMaterial : Material {
		#region Properties
		public Map2d Map;
		public Sampler2d Sampler;
		#endregion Properties

		#region Constructors
		public MappedMaterial ( Map2d map, TextureWrap wrapMode, FilteringType filtering = FilteringType.Best ) {
			this.Map = map;
			this.Sampler = new Sampler2d ( map, wrapMode, filtering );
		}
		#endregion Constructors

		public override double3 CalculateColor ( Scene scene, Traceable traceable,
			IntersectData data, Ray ray, TraceData traceData )
		{
			double2 t = traceable.GetTexCoord ( data );

			return	Sampler.GetColor ( t );
		}
	}
}
