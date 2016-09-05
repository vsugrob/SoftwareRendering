using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;
using System.Windows.Media;

namespace TextureFilteringDev {
	public static class ExtensionMethods {
		public static double3 ToDouble3 ( this Color color ) {
			return	new double3 ( color.ScR, color.ScG, color.ScB );
		}
	}
}
