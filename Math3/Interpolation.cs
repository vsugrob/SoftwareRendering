using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math3d {
	public static class Interpolation {
		#region Methods
		public static double Bilinear ( double s, double t,
			double a, double b, double c, double d )
		{
			return	t.Lerp ( s.Lerp ( a, b ), s.Lerp ( c, d ) );
		}

		public static double Bilinear ( double2 st,
			double a, double b, double c, double d )
		{
			return	st.t.Lerp ( st.s.Lerp ( a, b ), st.s.Lerp ( c, d ) );
		}

		public static double3 Bilinear ( double s, double t,
			double3 a, double3 b, double3 c, double3 d )
		{
			return	t.Lerp ( s.Lerp ( a, b ), s.Lerp ( c, d ) );
		}

		public static double3 Bilinear ( double2 st,
			double3 a, double3 b, double3 c, double3 d )
		{
			return	st.t.Lerp ( st.s.Lerp ( a, b ), st.s.Lerp ( c, d ) );
		}

		public static void Advance ( this IEnumerable <IInterpolatorEnumerator> interpolators, double rangeDelta ) {
			foreach ( var interpolator in interpolators )
				interpolator.MoveNext ( rangeDelta );
		}
		#endregion Methods
	}

	public interface IInterpolatorEnumerator {
		bool MoveNext ( double rangeDelta );
	}
}
