using System;
using System.ComponentModel;

namespace Math3d {
	public partial struct double3 {
		#region Swizzle Properties
		// x, y, z permutations
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

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 xxx {
			get { return	new double3 ( x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 xxy {
			get { return	new double3 ( x, x, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 xxz {
			get { return	new double3 ( x, x, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 xyx {
			get { return	new double3 ( x, y, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 xyy {
			get { return	new double3 ( x, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 xyz {
			get { return	new double3 ( x, y, z ); }
			set {
				x = value.x;
				y = value.y;
				z = value.z;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 xzx {
			get { return	new double3 ( x, z, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 xzy {
			get { return	new double3 ( x, z, y ); }
			set {
				x = value.x;
				z = value.y;
				y = value.z;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 xzz {
			get { return	new double3 ( x, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 yxx {
			get { return	new double3 ( y, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 yxy {
			get { return	new double3 ( y, x, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 yxz {
			get { return	new double3 ( y, x, z ); }
			set {
				y = value.x;
				x = value.y;
				z = value.z;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 yyx {
			get { return	new double3 ( y, y, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 yyy {
			get { return	new double3 ( y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 yyz {
			get { return	new double3 ( y, y, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 yzx {
			get { return	new double3 ( y, z, x ); }
			set {
				y = value.x;
				z = value.y;
				x = value.z;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 yzy {
			get { return	new double3 ( y, z, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 yzz {
			get { return	new double3 ( y, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 zxx {
			get { return	new double3 ( z, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 zxy {
			get { return	new double3 ( z, x, y ); }
			set {
				z = value.x;
				x = value.y;
				y = value.z;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 zxz {
			get { return	new double3 ( z, x, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 zyx {
			get { return	new double3 ( z, y, x ); }
			set {
				z = value.x;
				y = value.y;
				x = value.z;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 zyy {
			get { return	new double3 ( z, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 zyz {
			get { return	new double3 ( z, y, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 zzx {
			get { return	new double3 ( z, z, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 zzy {
			get { return	new double3 ( z, z, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 zzz {
			get { return	new double3 ( z ); }
		}

		// r, g, b permutations
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

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 rrr {
			get { return	new double3 ( r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 rrg {
			get { return	new double3 ( r, r, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 rrb {
			get { return	new double3 ( r, r, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 rgr {
			get { return	new double3 ( r, g, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 rgg {
			get { return	new double3 ( r, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 rgb {
			get { return	new double3 ( r, g, b ); }
			set {
				r = value.r;
				g = value.g;
				b = value.b;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 rbr {
			get { return	new double3 ( r, b, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 rbg {
			get { return	new double3 ( r, b, g ); }
			set {
				r = value.r;
				b = value.g;
				g = value.b;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 rbb {
			get { return	new double3 ( r, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 grr {
			get { return	new double3 ( g, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 grg {
			get { return	new double3 ( g, r, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 grb {
			get { return	new double3 ( g, r, b ); }
			set {
				g = value.r;
				r = value.g;
				b = value.b;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 ggr {
			get { return	new double3 ( g, g, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 ggg {
			get { return	new double3 ( g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 ggb {
			get { return	new double3 ( g, g, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 gbr {
			get { return	new double3 ( g, b, r ); }
			set {
				g = value.r;
				b = value.g;
				r = value.b;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 gbg {
			get { return	new double3 ( g, b, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 gbb {
			get { return	new double3 ( g, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 brr {
			get { return	new double3 ( b, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 brg {
			get { return	new double3 ( b, r, g ); }
			set {
				b = value.r;
				r = value.g;
				g = value.b;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 brb {
			get { return	new double3 ( b, r, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 bgr {
			get { return	new double3 ( b, g, r ); }
			set {
				b = value.r;
				g = value.g;
				r = value.b;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 bgg {
			get { return	new double3 ( b, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 bgb {
			get { return	new double3 ( b, g, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 bbr {
			get { return	new double3 ( b, b, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 bbg {
			get { return	new double3 ( b, b, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 bbb {
			get { return	new double3 ( b ); }
		}

		// s, t, p permutations
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

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 sss {
			get { return	new double3 ( s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 sst {
			get { return	new double3 ( s, s, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 ssp {
			get { return	new double3 ( s, s, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 sts {
			get { return	new double3 ( s, t, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 stt {
			get { return	new double3 ( s, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 stp {
			get { return	new double3 ( s, t, p ); }
			set {
				s = value.s;
				t = value.t;
				p = value.p;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 sps {
			get { return	new double3 ( s, p, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 spt {
			get { return	new double3 ( s, p, t ); }
			set {
				s = value.s;
				p = value.t;
				t = value.p;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 spp {
			get { return	new double3 ( s, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 tss {
			get { return	new double3 ( t, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 tst {
			get { return	new double3 ( t, s, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 tsp {
			get { return	new double3 ( t, s, p ); }
			set {
				t = value.s;
				s = value.t;
				p = value.p;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 tts {
			get { return	new double3 ( t, t, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 ttt {
			get { return	new double3 ( t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 ttp {
			get { return	new double3 ( t, t, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 tps {
			get { return	new double3 ( t, p, s ); }
			set {
				t = value.s;
				p = value.t;
				s = value.p;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 tpt {
			get { return	new double3 ( t, p, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 tpp {
			get { return	new double3 ( t, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 pss {
			get { return	new double3 ( p, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 pst {
			get { return	new double3 ( p, s, t ); }
			set {
				p = value.s;
				s = value.t;
				t = value.p;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 psp {
			get { return	new double3 ( p, s, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 pts {
			get { return	new double3 ( p, t, s ); }
			set {
				p = value.s;
				t = value.t;
				s = value.p;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 ptt {
			get { return	new double3 ( p, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 ptp {
			get { return	new double3 ( p, t, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 pps {
			get { return	new double3 ( p, p, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 ppt {
			get { return	new double3 ( p, p, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double3 ppp {
			get { return	new double3 ( p ); }
		}

		#endregion Swizzle Properties

	}
}
