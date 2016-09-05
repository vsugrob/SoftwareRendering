using System;
using System.ComponentModel;

namespace Math3d {
	public partial struct double2 {
		#region Swizzle Properties
		// x, y permutations
		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double2 xx {
			get { return	new double2 ( x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double2 xy {
			get { return	new double2 ( x, y ); }
			set {
				x = value.x;
				y = value.y;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double2 yx {
			get { return	new double2 ( y, x ); }
			set {
				y = value.x;
				x = value.y;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double2 yy {
			get { return	new double2 ( y ); }
		}

		// r, g permutations
		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double2 rr {
			get { return	new double2 ( r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double2 rg {
			get { return	new double2 ( r, g ); }
			set {
				r = value.r;
				g = value.g;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double2 gr {
			get { return	new double2 ( g, r ); }
			set {
				g = value.r;
				r = value.g;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double2 gg {
			get { return	new double2 ( g ); }
		}

		// s, t permutations
		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double2 ss {
			get { return	new double2 ( s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double2 st {
			get { return	new double2 ( s, t ); }
			set {
				s = value.s;
				t = value.t;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double2 ts {
			get { return	new double2 ( t, s ); }
			set {
				t = value.s;
				s = value.t;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double2 tt {
			get { return	new double2 ( t ); }
		}

		#endregion Swizzle Properties

	}
}
