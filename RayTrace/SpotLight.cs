using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace RayTrace {
	public class SpotLight {
		#region Properties
		public double4 Pos;
		public double3 DiffuseColor;
		public double3 SpecularColor;
		public double AttenuationBase;
		#endregion Properties

		#region Constructors
		public SpotLight ( double3 pos, double3 diffuseColor, double3 specularColor,
			double attenuationBase = Math.E )
		{
			this.Pos = new double4 ( pos, 1 );
			this.DiffuseColor = diffuseColor;
			this.SpecularColor = specularColor;
			this.AttenuationBase = attenuationBase;
		}
		#endregion Constructors
	}
}
