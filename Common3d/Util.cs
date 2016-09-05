using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common3d {
	public static class Util {
		public static void Swap <T> ( ref T a, ref T b ) {
			T t = a;
			a = b;
			b = t;
		}
	}
}
