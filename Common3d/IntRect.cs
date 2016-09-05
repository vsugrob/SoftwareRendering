using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common3d {
	public struct IntRect {
		#region Fields
		public int x;
		public int y;
		public int width;
		public int height;
		#endregion Fields

		#region Constructors
		public IntRect ( int x, int y, int width, int height ) {
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		public IntRect ( int x, int y, IntSize size ) {
			this.x = x;
			this.y = y;
			this.width = size.width;
			this.height = size.height;
		}
		#endregion Constructors
	}
}
