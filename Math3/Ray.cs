using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math3d {
	public struct Ray {
		#region Fields
		public double4 p;
		public double3 l;
		#endregion Fields

		#region Constructors
		public Ray ( double3 p, double3 l ) {
		    this.p = new double4 ( p, 1 );
			this.l = l;
		}
		#endregion Constructors

		#region Operators
		public static Ray operator - ( Ray r ) {
			return	new Ray ( r.p, -r.l );
		}
		#endregion Operators
	}
}
