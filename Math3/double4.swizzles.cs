using System;
using System.ComponentModel;

namespace Math3d {
	public partial struct double4 {
		#region Swizzle Properties
		// x, y, z, w permutations
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

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xxxx {
			get { return	new double4 ( x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xxxy {
			get { return	new double4 ( x, x, x, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xxxz {
			get { return	new double4 ( x, x, x, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xxxw {
			get { return	new double4 ( x, x, x, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xxyx {
			get { return	new double4 ( x, x, y, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xxyy {
			get { return	new double4 ( x, x, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xxyz {
			get { return	new double4 ( x, x, y, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xxyw {
			get { return	new double4 ( x, x, y, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xxzx {
			get { return	new double4 ( x, x, z, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xxzy {
			get { return	new double4 ( x, x, z, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xxzz {
			get { return	new double4 ( x, x, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xxzw {
			get { return	new double4 ( x, x, z, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xxwx {
			get { return	new double4 ( x, x, w, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xxwy {
			get { return	new double4 ( x, x, w, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xxwz {
			get { return	new double4 ( x, x, w, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xxww {
			get { return	new double4 ( x, x, w, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xyxx {
			get { return	new double4 ( x, y, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xyxy {
			get { return	new double4 ( x, y, x, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xyxz {
			get { return	new double4 ( x, y, x, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xyxw {
			get { return	new double4 ( x, y, x, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xyyx {
			get { return	new double4 ( x, y, y, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xyyy {
			get { return	new double4 ( x, y, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xyyz {
			get { return	new double4 ( x, y, y, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xyyw {
			get { return	new double4 ( x, y, y, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xyzx {
			get { return	new double4 ( x, y, z, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xyzy {
			get { return	new double4 ( x, y, z, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xyzz {
			get { return	new double4 ( x, y, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xyzw {
			get { return	new double4 ( x, y, z, w ); }
			set {
				x = value.x;
				y = value.y;
				z = value.z;
				w = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xywx {
			get { return	new double4 ( x, y, w, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xywy {
			get { return	new double4 ( x, y, w, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xywz {
			get { return	new double4 ( x, y, w, z ); }
			set {
				x = value.x;
				y = value.y;
				w = value.z;
				z = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xyww {
			get { return	new double4 ( x, y, w, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xzxx {
			get { return	new double4 ( x, z, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xzxy {
			get { return	new double4 ( x, z, x, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xzxz {
			get { return	new double4 ( x, z, x, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xzxw {
			get { return	new double4 ( x, z, x, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xzyx {
			get { return	new double4 ( x, z, y, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xzyy {
			get { return	new double4 ( x, z, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xzyz {
			get { return	new double4 ( x, z, y, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xzyw {
			get { return	new double4 ( x, z, y, w ); }
			set {
				x = value.x;
				z = value.y;
				y = value.z;
				w = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xzzx {
			get { return	new double4 ( x, z, z, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xzzy {
			get { return	new double4 ( x, z, z, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xzzz {
			get { return	new double4 ( x, z, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xzzw {
			get { return	new double4 ( x, z, z, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xzwx {
			get { return	new double4 ( x, z, w, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xzwy {
			get { return	new double4 ( x, z, w, y ); }
			set {
				x = value.x;
				z = value.y;
				w = value.z;
				y = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xzwz {
			get { return	new double4 ( x, z, w, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xzww {
			get { return	new double4 ( x, z, w, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xwxx {
			get { return	new double4 ( x, w, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xwxy {
			get { return	new double4 ( x, w, x, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xwxz {
			get { return	new double4 ( x, w, x, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xwxw {
			get { return	new double4 ( x, w, x, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xwyx {
			get { return	new double4 ( x, w, y, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xwyy {
			get { return	new double4 ( x, w, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xwyz {
			get { return	new double4 ( x, w, y, z ); }
			set {
				x = value.x;
				w = value.y;
				y = value.z;
				z = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xwyw {
			get { return	new double4 ( x, w, y, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xwzx {
			get { return	new double4 ( x, w, z, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xwzy {
			get { return	new double4 ( x, w, z, y ); }
			set {
				x = value.x;
				w = value.y;
				z = value.z;
				y = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xwzz {
			get { return	new double4 ( x, w, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xwzw {
			get { return	new double4 ( x, w, z, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xwwx {
			get { return	new double4 ( x, w, w, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xwwy {
			get { return	new double4 ( x, w, w, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xwwz {
			get { return	new double4 ( x, w, w, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 xwww {
			get { return	new double4 ( x, w, w, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yxxx {
			get { return	new double4 ( y, x, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yxxy {
			get { return	new double4 ( y, x, x, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yxxz {
			get { return	new double4 ( y, x, x, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yxxw {
			get { return	new double4 ( y, x, x, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yxyx {
			get { return	new double4 ( y, x, y, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yxyy {
			get { return	new double4 ( y, x, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yxyz {
			get { return	new double4 ( y, x, y, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yxyw {
			get { return	new double4 ( y, x, y, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yxzx {
			get { return	new double4 ( y, x, z, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yxzy {
			get { return	new double4 ( y, x, z, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yxzz {
			get { return	new double4 ( y, x, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yxzw {
			get { return	new double4 ( y, x, z, w ); }
			set {
				y = value.x;
				x = value.y;
				z = value.z;
				w = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yxwx {
			get { return	new double4 ( y, x, w, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yxwy {
			get { return	new double4 ( y, x, w, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yxwz {
			get { return	new double4 ( y, x, w, z ); }
			set {
				y = value.x;
				x = value.y;
				w = value.z;
				z = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yxww {
			get { return	new double4 ( y, x, w, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yyxx {
			get { return	new double4 ( y, y, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yyxy {
			get { return	new double4 ( y, y, x, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yyxz {
			get { return	new double4 ( y, y, x, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yyxw {
			get { return	new double4 ( y, y, x, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yyyx {
			get { return	new double4 ( y, y, y, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yyyy {
			get { return	new double4 ( y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yyyz {
			get { return	new double4 ( y, y, y, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yyyw {
			get { return	new double4 ( y, y, y, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yyzx {
			get { return	new double4 ( y, y, z, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yyzy {
			get { return	new double4 ( y, y, z, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yyzz {
			get { return	new double4 ( y, y, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yyzw {
			get { return	new double4 ( y, y, z, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yywx {
			get { return	new double4 ( y, y, w, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yywy {
			get { return	new double4 ( y, y, w, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yywz {
			get { return	new double4 ( y, y, w, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yyww {
			get { return	new double4 ( y, y, w, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yzxx {
			get { return	new double4 ( y, z, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yzxy {
			get { return	new double4 ( y, z, x, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yzxz {
			get { return	new double4 ( y, z, x, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yzxw {
			get { return	new double4 ( y, z, x, w ); }
			set {
				y = value.x;
				z = value.y;
				x = value.z;
				w = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yzyx {
			get { return	new double4 ( y, z, y, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yzyy {
			get { return	new double4 ( y, z, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yzyz {
			get { return	new double4 ( y, z, y, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yzyw {
			get { return	new double4 ( y, z, y, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yzzx {
			get { return	new double4 ( y, z, z, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yzzy {
			get { return	new double4 ( y, z, z, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yzzz {
			get { return	new double4 ( y, z, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yzzw {
			get { return	new double4 ( y, z, z, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yzwx {
			get { return	new double4 ( y, z, w, x ); }
			set {
				y = value.x;
				z = value.y;
				w = value.z;
				x = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yzwy {
			get { return	new double4 ( y, z, w, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yzwz {
			get { return	new double4 ( y, z, w, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 yzww {
			get { return	new double4 ( y, z, w, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ywxx {
			get { return	new double4 ( y, w, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ywxy {
			get { return	new double4 ( y, w, x, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ywxz {
			get { return	new double4 ( y, w, x, z ); }
			set {
				y = value.x;
				w = value.y;
				x = value.z;
				z = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ywxw {
			get { return	new double4 ( y, w, x, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ywyx {
			get { return	new double4 ( y, w, y, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ywyy {
			get { return	new double4 ( y, w, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ywyz {
			get { return	new double4 ( y, w, y, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ywyw {
			get { return	new double4 ( y, w, y, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ywzx {
			get { return	new double4 ( y, w, z, x ); }
			set {
				y = value.x;
				w = value.y;
				z = value.z;
				x = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ywzy {
			get { return	new double4 ( y, w, z, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ywzz {
			get { return	new double4 ( y, w, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ywzw {
			get { return	new double4 ( y, w, z, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ywwx {
			get { return	new double4 ( y, w, w, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ywwy {
			get { return	new double4 ( y, w, w, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ywwz {
			get { return	new double4 ( y, w, w, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ywww {
			get { return	new double4 ( y, w, w, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zxxx {
			get { return	new double4 ( z, x, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zxxy {
			get { return	new double4 ( z, x, x, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zxxz {
			get { return	new double4 ( z, x, x, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zxxw {
			get { return	new double4 ( z, x, x, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zxyx {
			get { return	new double4 ( z, x, y, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zxyy {
			get { return	new double4 ( z, x, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zxyz {
			get { return	new double4 ( z, x, y, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zxyw {
			get { return	new double4 ( z, x, y, w ); }
			set {
				z = value.x;
				x = value.y;
				y = value.z;
				w = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zxzx {
			get { return	new double4 ( z, x, z, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zxzy {
			get { return	new double4 ( z, x, z, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zxzz {
			get { return	new double4 ( z, x, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zxzw {
			get { return	new double4 ( z, x, z, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zxwx {
			get { return	new double4 ( z, x, w, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zxwy {
			get { return	new double4 ( z, x, w, y ); }
			set {
				z = value.x;
				x = value.y;
				w = value.z;
				y = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zxwz {
			get { return	new double4 ( z, x, w, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zxww {
			get { return	new double4 ( z, x, w, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zyxx {
			get { return	new double4 ( z, y, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zyxy {
			get { return	new double4 ( z, y, x, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zyxz {
			get { return	new double4 ( z, y, x, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zyxw {
			get { return	new double4 ( z, y, x, w ); }
			set {
				z = value.x;
				y = value.y;
				x = value.z;
				w = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zyyx {
			get { return	new double4 ( z, y, y, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zyyy {
			get { return	new double4 ( z, y, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zyyz {
			get { return	new double4 ( z, y, y, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zyyw {
			get { return	new double4 ( z, y, y, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zyzx {
			get { return	new double4 ( z, y, z, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zyzy {
			get { return	new double4 ( z, y, z, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zyzz {
			get { return	new double4 ( z, y, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zyzw {
			get { return	new double4 ( z, y, z, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zywx {
			get { return	new double4 ( z, y, w, x ); }
			set {
				z = value.x;
				y = value.y;
				w = value.z;
				x = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zywy {
			get { return	new double4 ( z, y, w, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zywz {
			get { return	new double4 ( z, y, w, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zyww {
			get { return	new double4 ( z, y, w, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zzxx {
			get { return	new double4 ( z, z, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zzxy {
			get { return	new double4 ( z, z, x, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zzxz {
			get { return	new double4 ( z, z, x, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zzxw {
			get { return	new double4 ( z, z, x, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zzyx {
			get { return	new double4 ( z, z, y, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zzyy {
			get { return	new double4 ( z, z, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zzyz {
			get { return	new double4 ( z, z, y, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zzyw {
			get { return	new double4 ( z, z, y, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zzzx {
			get { return	new double4 ( z, z, z, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zzzy {
			get { return	new double4 ( z, z, z, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zzzz {
			get { return	new double4 ( z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zzzw {
			get { return	new double4 ( z, z, z, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zzwx {
			get { return	new double4 ( z, z, w, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zzwy {
			get { return	new double4 ( z, z, w, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zzwz {
			get { return	new double4 ( z, z, w, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zzww {
			get { return	new double4 ( z, z, w, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zwxx {
			get { return	new double4 ( z, w, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zwxy {
			get { return	new double4 ( z, w, x, y ); }
			set {
				z = value.x;
				w = value.y;
				x = value.z;
				y = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zwxz {
			get { return	new double4 ( z, w, x, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zwxw {
			get { return	new double4 ( z, w, x, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zwyx {
			get { return	new double4 ( z, w, y, x ); }
			set {
				z = value.x;
				w = value.y;
				y = value.z;
				x = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zwyy {
			get { return	new double4 ( z, w, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zwyz {
			get { return	new double4 ( z, w, y, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zwyw {
			get { return	new double4 ( z, w, y, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zwzx {
			get { return	new double4 ( z, w, z, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zwzy {
			get { return	new double4 ( z, w, z, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zwzz {
			get { return	new double4 ( z, w, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zwzw {
			get { return	new double4 ( z, w, z, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zwwx {
			get { return	new double4 ( z, w, w, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zwwy {
			get { return	new double4 ( z, w, w, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zwwz {
			get { return	new double4 ( z, w, w, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 zwww {
			get { return	new double4 ( z, w, w, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wxxx {
			get { return	new double4 ( w, x, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wxxy {
			get { return	new double4 ( w, x, x, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wxxz {
			get { return	new double4 ( w, x, x, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wxxw {
			get { return	new double4 ( w, x, x, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wxyx {
			get { return	new double4 ( w, x, y, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wxyy {
			get { return	new double4 ( w, x, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wxyz {
			get { return	new double4 ( w, x, y, z ); }
			set {
				w = value.x;
				x = value.y;
				y = value.z;
				z = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wxyw {
			get { return	new double4 ( w, x, y, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wxzx {
			get { return	new double4 ( w, x, z, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wxzy {
			get { return	new double4 ( w, x, z, y ); }
			set {
				w = value.x;
				x = value.y;
				z = value.z;
				y = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wxzz {
			get { return	new double4 ( w, x, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wxzw {
			get { return	new double4 ( w, x, z, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wxwx {
			get { return	new double4 ( w, x, w, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wxwy {
			get { return	new double4 ( w, x, w, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wxwz {
			get { return	new double4 ( w, x, w, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wxww {
			get { return	new double4 ( w, x, w, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wyxx {
			get { return	new double4 ( w, y, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wyxy {
			get { return	new double4 ( w, y, x, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wyxz {
			get { return	new double4 ( w, y, x, z ); }
			set {
				w = value.x;
				y = value.y;
				x = value.z;
				z = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wyxw {
			get { return	new double4 ( w, y, x, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wyyx {
			get { return	new double4 ( w, y, y, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wyyy {
			get { return	new double4 ( w, y, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wyyz {
			get { return	new double4 ( w, y, y, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wyyw {
			get { return	new double4 ( w, y, y, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wyzx {
			get { return	new double4 ( w, y, z, x ); }
			set {
				w = value.x;
				y = value.y;
				z = value.z;
				x = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wyzy {
			get { return	new double4 ( w, y, z, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wyzz {
			get { return	new double4 ( w, y, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wyzw {
			get { return	new double4 ( w, y, z, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wywx {
			get { return	new double4 ( w, y, w, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wywy {
			get { return	new double4 ( w, y, w, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wywz {
			get { return	new double4 ( w, y, w, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wyww {
			get { return	new double4 ( w, y, w, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wzxx {
			get { return	new double4 ( w, z, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wzxy {
			get { return	new double4 ( w, z, x, y ); }
			set {
				w = value.x;
				z = value.y;
				x = value.z;
				y = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wzxz {
			get { return	new double4 ( w, z, x, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wzxw {
			get { return	new double4 ( w, z, x, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wzyx {
			get { return	new double4 ( w, z, y, x ); }
			set {
				w = value.x;
				z = value.y;
				y = value.z;
				x = value.w;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wzyy {
			get { return	new double4 ( w, z, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wzyz {
			get { return	new double4 ( w, z, y, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wzyw {
			get { return	new double4 ( w, z, y, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wzzx {
			get { return	new double4 ( w, z, z, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wzzy {
			get { return	new double4 ( w, z, z, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wzzz {
			get { return	new double4 ( w, z, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wzzw {
			get { return	new double4 ( w, z, z, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wzwx {
			get { return	new double4 ( w, z, w, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wzwy {
			get { return	new double4 ( w, z, w, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wzwz {
			get { return	new double4 ( w, z, w, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wzww {
			get { return	new double4 ( w, z, w, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wwxx {
			get { return	new double4 ( w, w, x, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wwxy {
			get { return	new double4 ( w, w, x, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wwxz {
			get { return	new double4 ( w, w, x, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wwxw {
			get { return	new double4 ( w, w, x, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wwyx {
			get { return	new double4 ( w, w, y, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wwyy {
			get { return	new double4 ( w, w, y, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wwyz {
			get { return	new double4 ( w, w, y, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wwyw {
			get { return	new double4 ( w, w, y, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wwzx {
			get { return	new double4 ( w, w, z, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wwzy {
			get { return	new double4 ( w, w, z, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wwzz {
			get { return	new double4 ( w, w, z, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wwzw {
			get { return	new double4 ( w, w, z, w ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wwwx {
			get { return	new double4 ( w, w, w, x ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wwwy {
			get { return	new double4 ( w, w, w, y ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wwwz {
			get { return	new double4 ( w, w, w, z ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 wwww {
			get { return	new double4 ( w ); }
		}

		// r, g, b, a permutations
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

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rrrr {
			get { return	new double4 ( r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rrrg {
			get { return	new double4 ( r, r, r, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rrrb {
			get { return	new double4 ( r, r, r, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rrra {
			get { return	new double4 ( r, r, r, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rrgr {
			get { return	new double4 ( r, r, g, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rrgg {
			get { return	new double4 ( r, r, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rrgb {
			get { return	new double4 ( r, r, g, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rrga {
			get { return	new double4 ( r, r, g, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rrbr {
			get { return	new double4 ( r, r, b, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rrbg {
			get { return	new double4 ( r, r, b, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rrbb {
			get { return	new double4 ( r, r, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rrba {
			get { return	new double4 ( r, r, b, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rrar {
			get { return	new double4 ( r, r, a, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rrag {
			get { return	new double4 ( r, r, a, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rrab {
			get { return	new double4 ( r, r, a, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rraa {
			get { return	new double4 ( r, r, a, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rgrr {
			get { return	new double4 ( r, g, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rgrg {
			get { return	new double4 ( r, g, r, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rgrb {
			get { return	new double4 ( r, g, r, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rgra {
			get { return	new double4 ( r, g, r, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rggr {
			get { return	new double4 ( r, g, g, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rggg {
			get { return	new double4 ( r, g, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rggb {
			get { return	new double4 ( r, g, g, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rgga {
			get { return	new double4 ( r, g, g, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rgbr {
			get { return	new double4 ( r, g, b, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rgbg {
			get { return	new double4 ( r, g, b, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rgbb {
			get { return	new double4 ( r, g, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rgba {
			get { return	new double4 ( r, g, b, a ); }
			set {
				r = value.r;
				g = value.g;
				b = value.b;
				a = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rgar {
			get { return	new double4 ( r, g, a, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rgag {
			get { return	new double4 ( r, g, a, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rgab {
			get { return	new double4 ( r, g, a, b ); }
			set {
				r = value.r;
				g = value.g;
				a = value.b;
				b = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rgaa {
			get { return	new double4 ( r, g, a, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rbrr {
			get { return	new double4 ( r, b, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rbrg {
			get { return	new double4 ( r, b, r, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rbrb {
			get { return	new double4 ( r, b, r, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rbra {
			get { return	new double4 ( r, b, r, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rbgr {
			get { return	new double4 ( r, b, g, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rbgg {
			get { return	new double4 ( r, b, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rbgb {
			get { return	new double4 ( r, b, g, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rbga {
			get { return	new double4 ( r, b, g, a ); }
			set {
				r = value.r;
				b = value.g;
				g = value.b;
				a = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rbbr {
			get { return	new double4 ( r, b, b, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rbbg {
			get { return	new double4 ( r, b, b, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rbbb {
			get { return	new double4 ( r, b, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rbba {
			get { return	new double4 ( r, b, b, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rbar {
			get { return	new double4 ( r, b, a, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rbag {
			get { return	new double4 ( r, b, a, g ); }
			set {
				r = value.r;
				b = value.g;
				a = value.b;
				g = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rbab {
			get { return	new double4 ( r, b, a, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rbaa {
			get { return	new double4 ( r, b, a, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rarr {
			get { return	new double4 ( r, a, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rarg {
			get { return	new double4 ( r, a, r, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rarb {
			get { return	new double4 ( r, a, r, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rara {
			get { return	new double4 ( r, a, r, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ragr {
			get { return	new double4 ( r, a, g, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ragg {
			get { return	new double4 ( r, a, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ragb {
			get { return	new double4 ( r, a, g, b ); }
			set {
				r = value.r;
				a = value.g;
				g = value.b;
				b = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 raga {
			get { return	new double4 ( r, a, g, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rabr {
			get { return	new double4 ( r, a, b, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rabg {
			get { return	new double4 ( r, a, b, g ); }
			set {
				r = value.r;
				a = value.g;
				b = value.b;
				g = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 rabb {
			get { return	new double4 ( r, a, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 raba {
			get { return	new double4 ( r, a, b, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 raar {
			get { return	new double4 ( r, a, a, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 raag {
			get { return	new double4 ( r, a, a, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 raab {
			get { return	new double4 ( r, a, a, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 raaa {
			get { return	new double4 ( r, a, a, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 grrr {
			get { return	new double4 ( g, r, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 grrg {
			get { return	new double4 ( g, r, r, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 grrb {
			get { return	new double4 ( g, r, r, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 grra {
			get { return	new double4 ( g, r, r, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 grgr {
			get { return	new double4 ( g, r, g, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 grgg {
			get { return	new double4 ( g, r, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 grgb {
			get { return	new double4 ( g, r, g, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 grga {
			get { return	new double4 ( g, r, g, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 grbr {
			get { return	new double4 ( g, r, b, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 grbg {
			get { return	new double4 ( g, r, b, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 grbb {
			get { return	new double4 ( g, r, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 grba {
			get { return	new double4 ( g, r, b, a ); }
			set {
				g = value.r;
				r = value.g;
				b = value.b;
				a = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 grar {
			get { return	new double4 ( g, r, a, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 grag {
			get { return	new double4 ( g, r, a, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 grab {
			get { return	new double4 ( g, r, a, b ); }
			set {
				g = value.r;
				r = value.g;
				a = value.b;
				b = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 graa {
			get { return	new double4 ( g, r, a, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ggrr {
			get { return	new double4 ( g, g, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ggrg {
			get { return	new double4 ( g, g, r, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ggrb {
			get { return	new double4 ( g, g, r, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ggra {
			get { return	new double4 ( g, g, r, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gggr {
			get { return	new double4 ( g, g, g, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gggg {
			get { return	new double4 ( g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gggb {
			get { return	new double4 ( g, g, g, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ggga {
			get { return	new double4 ( g, g, g, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ggbr {
			get { return	new double4 ( g, g, b, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ggbg {
			get { return	new double4 ( g, g, b, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ggbb {
			get { return	new double4 ( g, g, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ggba {
			get { return	new double4 ( g, g, b, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ggar {
			get { return	new double4 ( g, g, a, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ggag {
			get { return	new double4 ( g, g, a, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ggab {
			get { return	new double4 ( g, g, a, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ggaa {
			get { return	new double4 ( g, g, a, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gbrr {
			get { return	new double4 ( g, b, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gbrg {
			get { return	new double4 ( g, b, r, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gbrb {
			get { return	new double4 ( g, b, r, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gbra {
			get { return	new double4 ( g, b, r, a ); }
			set {
				g = value.r;
				b = value.g;
				r = value.b;
				a = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gbgr {
			get { return	new double4 ( g, b, g, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gbgg {
			get { return	new double4 ( g, b, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gbgb {
			get { return	new double4 ( g, b, g, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gbga {
			get { return	new double4 ( g, b, g, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gbbr {
			get { return	new double4 ( g, b, b, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gbbg {
			get { return	new double4 ( g, b, b, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gbbb {
			get { return	new double4 ( g, b, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gbba {
			get { return	new double4 ( g, b, b, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gbar {
			get { return	new double4 ( g, b, a, r ); }
			set {
				g = value.r;
				b = value.g;
				a = value.b;
				r = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gbag {
			get { return	new double4 ( g, b, a, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gbab {
			get { return	new double4 ( g, b, a, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gbaa {
			get { return	new double4 ( g, b, a, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 garr {
			get { return	new double4 ( g, a, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 garg {
			get { return	new double4 ( g, a, r, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 garb {
			get { return	new double4 ( g, a, r, b ); }
			set {
				g = value.r;
				a = value.g;
				r = value.b;
				b = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gara {
			get { return	new double4 ( g, a, r, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gagr {
			get { return	new double4 ( g, a, g, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gagg {
			get { return	new double4 ( g, a, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gagb {
			get { return	new double4 ( g, a, g, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gaga {
			get { return	new double4 ( g, a, g, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gabr {
			get { return	new double4 ( g, a, b, r ); }
			set {
				g = value.r;
				a = value.g;
				b = value.b;
				r = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gabg {
			get { return	new double4 ( g, a, b, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gabb {
			get { return	new double4 ( g, a, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gaba {
			get { return	new double4 ( g, a, b, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gaar {
			get { return	new double4 ( g, a, a, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gaag {
			get { return	new double4 ( g, a, a, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gaab {
			get { return	new double4 ( g, a, a, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 gaaa {
			get { return	new double4 ( g, a, a, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 brrr {
			get { return	new double4 ( b, r, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 brrg {
			get { return	new double4 ( b, r, r, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 brrb {
			get { return	new double4 ( b, r, r, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 brra {
			get { return	new double4 ( b, r, r, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 brgr {
			get { return	new double4 ( b, r, g, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 brgg {
			get { return	new double4 ( b, r, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 brgb {
			get { return	new double4 ( b, r, g, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 brga {
			get { return	new double4 ( b, r, g, a ); }
			set {
				b = value.r;
				r = value.g;
				g = value.b;
				a = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 brbr {
			get { return	new double4 ( b, r, b, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 brbg {
			get { return	new double4 ( b, r, b, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 brbb {
			get { return	new double4 ( b, r, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 brba {
			get { return	new double4 ( b, r, b, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 brar {
			get { return	new double4 ( b, r, a, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 brag {
			get { return	new double4 ( b, r, a, g ); }
			set {
				b = value.r;
				r = value.g;
				a = value.b;
				g = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 brab {
			get { return	new double4 ( b, r, a, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 braa {
			get { return	new double4 ( b, r, a, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bgrr {
			get { return	new double4 ( b, g, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bgrg {
			get { return	new double4 ( b, g, r, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bgrb {
			get { return	new double4 ( b, g, r, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bgra {
			get { return	new double4 ( b, g, r, a ); }
			set {
				b = value.r;
				g = value.g;
				r = value.b;
				a = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bggr {
			get { return	new double4 ( b, g, g, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bggg {
			get { return	new double4 ( b, g, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bggb {
			get { return	new double4 ( b, g, g, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bgga {
			get { return	new double4 ( b, g, g, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bgbr {
			get { return	new double4 ( b, g, b, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bgbg {
			get { return	new double4 ( b, g, b, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bgbb {
			get { return	new double4 ( b, g, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bgba {
			get { return	new double4 ( b, g, b, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bgar {
			get { return	new double4 ( b, g, a, r ); }
			set {
				b = value.r;
				g = value.g;
				a = value.b;
				r = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bgag {
			get { return	new double4 ( b, g, a, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bgab {
			get { return	new double4 ( b, g, a, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bgaa {
			get { return	new double4 ( b, g, a, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bbrr {
			get { return	new double4 ( b, b, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bbrg {
			get { return	new double4 ( b, b, r, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bbrb {
			get { return	new double4 ( b, b, r, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bbra {
			get { return	new double4 ( b, b, r, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bbgr {
			get { return	new double4 ( b, b, g, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bbgg {
			get { return	new double4 ( b, b, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bbgb {
			get { return	new double4 ( b, b, g, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bbga {
			get { return	new double4 ( b, b, g, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bbbr {
			get { return	new double4 ( b, b, b, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bbbg {
			get { return	new double4 ( b, b, b, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bbbb {
			get { return	new double4 ( b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bbba {
			get { return	new double4 ( b, b, b, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bbar {
			get { return	new double4 ( b, b, a, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bbag {
			get { return	new double4 ( b, b, a, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bbab {
			get { return	new double4 ( b, b, a, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bbaa {
			get { return	new double4 ( b, b, a, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 barr {
			get { return	new double4 ( b, a, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 barg {
			get { return	new double4 ( b, a, r, g ); }
			set {
				b = value.r;
				a = value.g;
				r = value.b;
				g = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 barb {
			get { return	new double4 ( b, a, r, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bara {
			get { return	new double4 ( b, a, r, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bagr {
			get { return	new double4 ( b, a, g, r ); }
			set {
				b = value.r;
				a = value.g;
				g = value.b;
				r = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bagg {
			get { return	new double4 ( b, a, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 bagb {
			get { return	new double4 ( b, a, g, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 baga {
			get { return	new double4 ( b, a, g, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 babr {
			get { return	new double4 ( b, a, b, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 babg {
			get { return	new double4 ( b, a, b, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 babb {
			get { return	new double4 ( b, a, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 baba {
			get { return	new double4 ( b, a, b, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 baar {
			get { return	new double4 ( b, a, a, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 baag {
			get { return	new double4 ( b, a, a, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 baab {
			get { return	new double4 ( b, a, a, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 baaa {
			get { return	new double4 ( b, a, a, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 arrr {
			get { return	new double4 ( a, r, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 arrg {
			get { return	new double4 ( a, r, r, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 arrb {
			get { return	new double4 ( a, r, r, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 arra {
			get { return	new double4 ( a, r, r, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 argr {
			get { return	new double4 ( a, r, g, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 argg {
			get { return	new double4 ( a, r, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 argb {
			get { return	new double4 ( a, r, g, b ); }
			set {
				a = value.r;
				r = value.g;
				g = value.b;
				b = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 arga {
			get { return	new double4 ( a, r, g, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 arbr {
			get { return	new double4 ( a, r, b, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 arbg {
			get { return	new double4 ( a, r, b, g ); }
			set {
				a = value.r;
				r = value.g;
				b = value.b;
				g = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 arbb {
			get { return	new double4 ( a, r, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 arba {
			get { return	new double4 ( a, r, b, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 arar {
			get { return	new double4 ( a, r, a, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 arag {
			get { return	new double4 ( a, r, a, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 arab {
			get { return	new double4 ( a, r, a, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 araa {
			get { return	new double4 ( a, r, a, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 agrr {
			get { return	new double4 ( a, g, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 agrg {
			get { return	new double4 ( a, g, r, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 agrb {
			get { return	new double4 ( a, g, r, b ); }
			set {
				a = value.r;
				g = value.g;
				r = value.b;
				b = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 agra {
			get { return	new double4 ( a, g, r, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aggr {
			get { return	new double4 ( a, g, g, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aggg {
			get { return	new double4 ( a, g, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aggb {
			get { return	new double4 ( a, g, g, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 agga {
			get { return	new double4 ( a, g, g, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 agbr {
			get { return	new double4 ( a, g, b, r ); }
			set {
				a = value.r;
				g = value.g;
				b = value.b;
				r = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 agbg {
			get { return	new double4 ( a, g, b, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 agbb {
			get { return	new double4 ( a, g, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 agba {
			get { return	new double4 ( a, g, b, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 agar {
			get { return	new double4 ( a, g, a, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 agag {
			get { return	new double4 ( a, g, a, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 agab {
			get { return	new double4 ( a, g, a, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 agaa {
			get { return	new double4 ( a, g, a, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 abrr {
			get { return	new double4 ( a, b, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 abrg {
			get { return	new double4 ( a, b, r, g ); }
			set {
				a = value.r;
				b = value.g;
				r = value.b;
				g = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 abrb {
			get { return	new double4 ( a, b, r, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 abra {
			get { return	new double4 ( a, b, r, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 abgr {
			get { return	new double4 ( a, b, g, r ); }
			set {
				a = value.r;
				b = value.g;
				g = value.b;
				r = value.a;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 abgg {
			get { return	new double4 ( a, b, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 abgb {
			get { return	new double4 ( a, b, g, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 abga {
			get { return	new double4 ( a, b, g, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 abbr {
			get { return	new double4 ( a, b, b, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 abbg {
			get { return	new double4 ( a, b, b, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 abbb {
			get { return	new double4 ( a, b, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 abba {
			get { return	new double4 ( a, b, b, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 abar {
			get { return	new double4 ( a, b, a, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 abag {
			get { return	new double4 ( a, b, a, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 abab {
			get { return	new double4 ( a, b, a, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 abaa {
			get { return	new double4 ( a, b, a, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aarr {
			get { return	new double4 ( a, a, r, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aarg {
			get { return	new double4 ( a, a, r, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aarb {
			get { return	new double4 ( a, a, r, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aara {
			get { return	new double4 ( a, a, r, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aagr {
			get { return	new double4 ( a, a, g, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aagg {
			get { return	new double4 ( a, a, g, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aagb {
			get { return	new double4 ( a, a, g, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aaga {
			get { return	new double4 ( a, a, g, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aabr {
			get { return	new double4 ( a, a, b, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aabg {
			get { return	new double4 ( a, a, b, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aabb {
			get { return	new double4 ( a, a, b, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aaba {
			get { return	new double4 ( a, a, b, a ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aaar {
			get { return	new double4 ( a, a, a, r ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aaag {
			get { return	new double4 ( a, a, a, g ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aaab {
			get { return	new double4 ( a, a, a, b ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 aaaa {
			get { return	new double4 ( a ); }
		}

		// s, t, p, q permutations
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

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ssss {
			get { return	new double4 ( s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ssst {
			get { return	new double4 ( s, s, s, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sssp {
			get { return	new double4 ( s, s, s, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sssq {
			get { return	new double4 ( s, s, s, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ssts {
			get { return	new double4 ( s, s, t, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sstt {
			get { return	new double4 ( s, s, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sstp {
			get { return	new double4 ( s, s, t, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sstq {
			get { return	new double4 ( s, s, t, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ssps {
			get { return	new double4 ( s, s, p, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sspt {
			get { return	new double4 ( s, s, p, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sspp {
			get { return	new double4 ( s, s, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sspq {
			get { return	new double4 ( s, s, p, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ssqs {
			get { return	new double4 ( s, s, q, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ssqt {
			get { return	new double4 ( s, s, q, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ssqp {
			get { return	new double4 ( s, s, q, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ssqq {
			get { return	new double4 ( s, s, q, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 stss {
			get { return	new double4 ( s, t, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 stst {
			get { return	new double4 ( s, t, s, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 stsp {
			get { return	new double4 ( s, t, s, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 stsq {
			get { return	new double4 ( s, t, s, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 stts {
			get { return	new double4 ( s, t, t, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sttt {
			get { return	new double4 ( s, t, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sttp {
			get { return	new double4 ( s, t, t, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sttq {
			get { return	new double4 ( s, t, t, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 stps {
			get { return	new double4 ( s, t, p, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 stpt {
			get { return	new double4 ( s, t, p, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 stpp {
			get { return	new double4 ( s, t, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 stpq {
			get { return	new double4 ( s, t, p, q ); }
			set {
				s = value.s;
				t = value.t;
				p = value.p;
				q = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 stqs {
			get { return	new double4 ( s, t, q, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 stqt {
			get { return	new double4 ( s, t, q, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 stqp {
			get { return	new double4 ( s, t, q, p ); }
			set {
				s = value.s;
				t = value.t;
				q = value.p;
				p = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 stqq {
			get { return	new double4 ( s, t, q, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 spss {
			get { return	new double4 ( s, p, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 spst {
			get { return	new double4 ( s, p, s, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 spsp {
			get { return	new double4 ( s, p, s, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 spsq {
			get { return	new double4 ( s, p, s, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 spts {
			get { return	new double4 ( s, p, t, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sptt {
			get { return	new double4 ( s, p, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sptp {
			get { return	new double4 ( s, p, t, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sptq {
			get { return	new double4 ( s, p, t, q ); }
			set {
				s = value.s;
				p = value.t;
				t = value.p;
				q = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 spps {
			get { return	new double4 ( s, p, p, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sppt {
			get { return	new double4 ( s, p, p, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sppp {
			get { return	new double4 ( s, p, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sppq {
			get { return	new double4 ( s, p, p, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 spqs {
			get { return	new double4 ( s, p, q, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 spqt {
			get { return	new double4 ( s, p, q, t ); }
			set {
				s = value.s;
				p = value.t;
				q = value.p;
				t = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 spqp {
			get { return	new double4 ( s, p, q, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 spqq {
			get { return	new double4 ( s, p, q, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sqss {
			get { return	new double4 ( s, q, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sqst {
			get { return	new double4 ( s, q, s, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sqsp {
			get { return	new double4 ( s, q, s, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sqsq {
			get { return	new double4 ( s, q, s, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sqts {
			get { return	new double4 ( s, q, t, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sqtt {
			get { return	new double4 ( s, q, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sqtp {
			get { return	new double4 ( s, q, t, p ); }
			set {
				s = value.s;
				q = value.t;
				t = value.p;
				p = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sqtq {
			get { return	new double4 ( s, q, t, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sqps {
			get { return	new double4 ( s, q, p, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sqpt {
			get { return	new double4 ( s, q, p, t ); }
			set {
				s = value.s;
				q = value.t;
				p = value.p;
				t = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sqpp {
			get { return	new double4 ( s, q, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sqpq {
			get { return	new double4 ( s, q, p, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sqqs {
			get { return	new double4 ( s, q, q, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sqqt {
			get { return	new double4 ( s, q, q, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sqqp {
			get { return	new double4 ( s, q, q, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 sqqq {
			get { return	new double4 ( s, q, q, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tsss {
			get { return	new double4 ( t, s, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tsst {
			get { return	new double4 ( t, s, s, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tssp {
			get { return	new double4 ( t, s, s, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tssq {
			get { return	new double4 ( t, s, s, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tsts {
			get { return	new double4 ( t, s, t, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tstt {
			get { return	new double4 ( t, s, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tstp {
			get { return	new double4 ( t, s, t, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tstq {
			get { return	new double4 ( t, s, t, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tsps {
			get { return	new double4 ( t, s, p, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tspt {
			get { return	new double4 ( t, s, p, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tspp {
			get { return	new double4 ( t, s, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tspq {
			get { return	new double4 ( t, s, p, q ); }
			set {
				t = value.s;
				s = value.t;
				p = value.p;
				q = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tsqs {
			get { return	new double4 ( t, s, q, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tsqt {
			get { return	new double4 ( t, s, q, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tsqp {
			get { return	new double4 ( t, s, q, p ); }
			set {
				t = value.s;
				s = value.t;
				q = value.p;
				p = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tsqq {
			get { return	new double4 ( t, s, q, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ttss {
			get { return	new double4 ( t, t, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ttst {
			get { return	new double4 ( t, t, s, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ttsp {
			get { return	new double4 ( t, t, s, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ttsq {
			get { return	new double4 ( t, t, s, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ttts {
			get { return	new double4 ( t, t, t, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tttt {
			get { return	new double4 ( t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tttp {
			get { return	new double4 ( t, t, t, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tttq {
			get { return	new double4 ( t, t, t, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ttps {
			get { return	new double4 ( t, t, p, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ttpt {
			get { return	new double4 ( t, t, p, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ttpp {
			get { return	new double4 ( t, t, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ttpq {
			get { return	new double4 ( t, t, p, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ttqs {
			get { return	new double4 ( t, t, q, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ttqt {
			get { return	new double4 ( t, t, q, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ttqp {
			get { return	new double4 ( t, t, q, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ttqq {
			get { return	new double4 ( t, t, q, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tpss {
			get { return	new double4 ( t, p, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tpst {
			get { return	new double4 ( t, p, s, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tpsp {
			get { return	new double4 ( t, p, s, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tpsq {
			get { return	new double4 ( t, p, s, q ); }
			set {
				t = value.s;
				p = value.t;
				s = value.p;
				q = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tpts {
			get { return	new double4 ( t, p, t, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tptt {
			get { return	new double4 ( t, p, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tptp {
			get { return	new double4 ( t, p, t, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tptq {
			get { return	new double4 ( t, p, t, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tpps {
			get { return	new double4 ( t, p, p, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tppt {
			get { return	new double4 ( t, p, p, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tppp {
			get { return	new double4 ( t, p, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tppq {
			get { return	new double4 ( t, p, p, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tpqs {
			get { return	new double4 ( t, p, q, s ); }
			set {
				t = value.s;
				p = value.t;
				q = value.p;
				s = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tpqt {
			get { return	new double4 ( t, p, q, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tpqp {
			get { return	new double4 ( t, p, q, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tpqq {
			get { return	new double4 ( t, p, q, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tqss {
			get { return	new double4 ( t, q, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tqst {
			get { return	new double4 ( t, q, s, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tqsp {
			get { return	new double4 ( t, q, s, p ); }
			set {
				t = value.s;
				q = value.t;
				s = value.p;
				p = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tqsq {
			get { return	new double4 ( t, q, s, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tqts {
			get { return	new double4 ( t, q, t, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tqtt {
			get { return	new double4 ( t, q, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tqtp {
			get { return	new double4 ( t, q, t, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tqtq {
			get { return	new double4 ( t, q, t, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tqps {
			get { return	new double4 ( t, q, p, s ); }
			set {
				t = value.s;
				q = value.t;
				p = value.p;
				s = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tqpt {
			get { return	new double4 ( t, q, p, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tqpp {
			get { return	new double4 ( t, q, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tqpq {
			get { return	new double4 ( t, q, p, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tqqs {
			get { return	new double4 ( t, q, q, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tqqt {
			get { return	new double4 ( t, q, q, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tqqp {
			get { return	new double4 ( t, q, q, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 tqqq {
			get { return	new double4 ( t, q, q, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 psss {
			get { return	new double4 ( p, s, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 psst {
			get { return	new double4 ( p, s, s, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pssp {
			get { return	new double4 ( p, s, s, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pssq {
			get { return	new double4 ( p, s, s, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 psts {
			get { return	new double4 ( p, s, t, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pstt {
			get { return	new double4 ( p, s, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pstp {
			get { return	new double4 ( p, s, t, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pstq {
			get { return	new double4 ( p, s, t, q ); }
			set {
				p = value.s;
				s = value.t;
				t = value.p;
				q = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 psps {
			get { return	new double4 ( p, s, p, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pspt {
			get { return	new double4 ( p, s, p, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pspp {
			get { return	new double4 ( p, s, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pspq {
			get { return	new double4 ( p, s, p, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 psqs {
			get { return	new double4 ( p, s, q, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 psqt {
			get { return	new double4 ( p, s, q, t ); }
			set {
				p = value.s;
				s = value.t;
				q = value.p;
				t = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 psqp {
			get { return	new double4 ( p, s, q, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 psqq {
			get { return	new double4 ( p, s, q, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ptss {
			get { return	new double4 ( p, t, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ptst {
			get { return	new double4 ( p, t, s, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ptsp {
			get { return	new double4 ( p, t, s, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ptsq {
			get { return	new double4 ( p, t, s, q ); }
			set {
				p = value.s;
				t = value.t;
				s = value.p;
				q = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ptts {
			get { return	new double4 ( p, t, t, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pttt {
			get { return	new double4 ( p, t, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pttp {
			get { return	new double4 ( p, t, t, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pttq {
			get { return	new double4 ( p, t, t, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ptps {
			get { return	new double4 ( p, t, p, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ptpt {
			get { return	new double4 ( p, t, p, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ptpp {
			get { return	new double4 ( p, t, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ptpq {
			get { return	new double4 ( p, t, p, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ptqs {
			get { return	new double4 ( p, t, q, s ); }
			set {
				p = value.s;
				t = value.t;
				q = value.p;
				s = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ptqt {
			get { return	new double4 ( p, t, q, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ptqp {
			get { return	new double4 ( p, t, q, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ptqq {
			get { return	new double4 ( p, t, q, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ppss {
			get { return	new double4 ( p, p, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ppst {
			get { return	new double4 ( p, p, s, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ppsp {
			get { return	new double4 ( p, p, s, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ppsq {
			get { return	new double4 ( p, p, s, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ppts {
			get { return	new double4 ( p, p, t, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pptt {
			get { return	new double4 ( p, p, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pptp {
			get { return	new double4 ( p, p, t, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pptq {
			get { return	new double4 ( p, p, t, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ppps {
			get { return	new double4 ( p, p, p, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pppt {
			get { return	new double4 ( p, p, p, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pppp {
			get { return	new double4 ( p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pppq {
			get { return	new double4 ( p, p, p, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ppqs {
			get { return	new double4 ( p, p, q, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ppqt {
			get { return	new double4 ( p, p, q, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ppqp {
			get { return	new double4 ( p, p, q, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 ppqq {
			get { return	new double4 ( p, p, q, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pqss {
			get { return	new double4 ( p, q, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pqst {
			get { return	new double4 ( p, q, s, t ); }
			set {
				p = value.s;
				q = value.t;
				s = value.p;
				t = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pqsp {
			get { return	new double4 ( p, q, s, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pqsq {
			get { return	new double4 ( p, q, s, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pqts {
			get { return	new double4 ( p, q, t, s ); }
			set {
				p = value.s;
				q = value.t;
				t = value.p;
				s = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pqtt {
			get { return	new double4 ( p, q, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pqtp {
			get { return	new double4 ( p, q, t, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pqtq {
			get { return	new double4 ( p, q, t, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pqps {
			get { return	new double4 ( p, q, p, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pqpt {
			get { return	new double4 ( p, q, p, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pqpp {
			get { return	new double4 ( p, q, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pqpq {
			get { return	new double4 ( p, q, p, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pqqs {
			get { return	new double4 ( p, q, q, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pqqt {
			get { return	new double4 ( p, q, q, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pqqp {
			get { return	new double4 ( p, q, q, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 pqqq {
			get { return	new double4 ( p, q, q, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qsss {
			get { return	new double4 ( q, s, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qsst {
			get { return	new double4 ( q, s, s, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qssp {
			get { return	new double4 ( q, s, s, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qssq {
			get { return	new double4 ( q, s, s, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qsts {
			get { return	new double4 ( q, s, t, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qstt {
			get { return	new double4 ( q, s, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qstp {
			get { return	new double4 ( q, s, t, p ); }
			set {
				q = value.s;
				s = value.t;
				t = value.p;
				p = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qstq {
			get { return	new double4 ( q, s, t, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qsps {
			get { return	new double4 ( q, s, p, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qspt {
			get { return	new double4 ( q, s, p, t ); }
			set {
				q = value.s;
				s = value.t;
				p = value.p;
				t = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qspp {
			get { return	new double4 ( q, s, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qspq {
			get { return	new double4 ( q, s, p, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qsqs {
			get { return	new double4 ( q, s, q, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qsqt {
			get { return	new double4 ( q, s, q, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qsqp {
			get { return	new double4 ( q, s, q, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qsqq {
			get { return	new double4 ( q, s, q, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qtss {
			get { return	new double4 ( q, t, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qtst {
			get { return	new double4 ( q, t, s, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qtsp {
			get { return	new double4 ( q, t, s, p ); }
			set {
				q = value.s;
				t = value.t;
				s = value.p;
				p = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qtsq {
			get { return	new double4 ( q, t, s, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qtts {
			get { return	new double4 ( q, t, t, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qttt {
			get { return	new double4 ( q, t, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qttp {
			get { return	new double4 ( q, t, t, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qttq {
			get { return	new double4 ( q, t, t, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qtps {
			get { return	new double4 ( q, t, p, s ); }
			set {
				q = value.s;
				t = value.t;
				p = value.p;
				s = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qtpt {
			get { return	new double4 ( q, t, p, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qtpp {
			get { return	new double4 ( q, t, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qtpq {
			get { return	new double4 ( q, t, p, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qtqs {
			get { return	new double4 ( q, t, q, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qtqt {
			get { return	new double4 ( q, t, q, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qtqp {
			get { return	new double4 ( q, t, q, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qtqq {
			get { return	new double4 ( q, t, q, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qpss {
			get { return	new double4 ( q, p, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qpst {
			get { return	new double4 ( q, p, s, t ); }
			set {
				q = value.s;
				p = value.t;
				s = value.p;
				t = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qpsp {
			get { return	new double4 ( q, p, s, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qpsq {
			get { return	new double4 ( q, p, s, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qpts {
			get { return	new double4 ( q, p, t, s ); }
			set {
				q = value.s;
				p = value.t;
				t = value.p;
				s = value.q;
			}
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qptt {
			get { return	new double4 ( q, p, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qptp {
			get { return	new double4 ( q, p, t, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qptq {
			get { return	new double4 ( q, p, t, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qpps {
			get { return	new double4 ( q, p, p, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qppt {
			get { return	new double4 ( q, p, p, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qppp {
			get { return	new double4 ( q, p, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qppq {
			get { return	new double4 ( q, p, p, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qpqs {
			get { return	new double4 ( q, p, q, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qpqt {
			get { return	new double4 ( q, p, q, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qpqp {
			get { return	new double4 ( q, p, q, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qpqq {
			get { return	new double4 ( q, p, q, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qqss {
			get { return	new double4 ( q, q, s, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qqst {
			get { return	new double4 ( q, q, s, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qqsp {
			get { return	new double4 ( q, q, s, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qqsq {
			get { return	new double4 ( q, q, s, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qqts {
			get { return	new double4 ( q, q, t, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qqtt {
			get { return	new double4 ( q, q, t, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qqtp {
			get { return	new double4 ( q, q, t, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qqtq {
			get { return	new double4 ( q, q, t, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qqps {
			get { return	new double4 ( q, q, p, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qqpt {
			get { return	new double4 ( q, q, p, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qqpp {
			get { return	new double4 ( q, q, p, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qqpq {
			get { return	new double4 ( q, q, p, q ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qqqs {
			get { return	new double4 ( q, q, q, s ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qqqt {
			get { return	new double4 ( q, q, q, t ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qqqp {
			get { return	new double4 ( q, q, q, p ); }
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		public double4 qqqq {
			get { return	new double4 ( q ); }
		}

		#endregion Swizzle Properties

	}
}
