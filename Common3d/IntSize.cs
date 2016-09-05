using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common3d {
	public struct IntSize {
		#region Fields
		public int width;
		public int height;
		#endregion Fields

		#region Constructors
		public IntSize ( int width, int height ) {
			this.width = width;
			this.height = height;
		}
		#endregion Constructors

		#region Overrides
		public override bool Equals ( object obj ) {
			if ( obj == null || !( obj is IntSize ) )
				return	false;

			IntSize sz = ( IntSize ) obj;

			return	width == sz.width && height == sz.height;
		}

		public override int GetHashCode () {
			return	width ^ height;
		}

		public override string ToString () {
			return	string.Format ( "width: {0}, height: {1}", width, height );
		}
		#endregion Overrides

		#region Operators
		public static bool operator == ( IntSize a, IntSize b ) {
			return	a.width == b.width && a.height == b.height;
		}

		public static bool operator != ( IntSize a, IntSize b ) {
			return	a.width != b.width || a.height != b.height;
		}
		#endregion Operators
	}
}
