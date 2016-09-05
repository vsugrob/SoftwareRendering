using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace Common3d {
	public struct DoubleSize {
		#region Fields
		public double width;
		public double height;
		#endregion Fields

		#region Constructors
		public DoubleSize ( double width, double height ) {
			this.width = width;
			this.height = height;
		}
		#endregion Constructors

		#region Overrides
		public override bool Equals ( object obj ) {
			if ( obj == null || !( obj is DoubleSize ) )
				return	false;

			DoubleSize sz = ( DoubleSize ) obj;

			return	width == sz.width && height == sz.height;
		}

		public override int GetHashCode () {
			int iWidth  = ( int ) ( width  * Math3.DOUBLE_PRECISION );
			int iHeight = ( int ) ( height * Math3.DOUBLE_PRECISION );
			
			return	iWidth ^ iHeight;
		}

		public override string ToString () {
			return	string.Format ( "width: {0}, height: {1}", width, height );
		}
		#endregion Overrides

		#region Operators
		public static bool operator == ( DoubleSize a, DoubleSize b ) {
			return	a.width == b.width && a.height == b.height;
		}

		public static bool operator != ( DoubleSize a, DoubleSize b ) {
			return	a.width != b.width || a.height != b.height;
		}
		#endregion Operators
	}
}
