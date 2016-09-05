using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTrace {
	public enum ColorComponent {
		Red, Green, Blue
	}

	public static class Light {
		#region Constants
		public const double RedWavelength = 685;	// roughly 630–740 nm
		public const double GreenWavelength = 545;	// roughly 520–570 nm
		public const double BlueWavelength = 465;	// roughly 440–490 nm
		#endregion Constants
	}
}
