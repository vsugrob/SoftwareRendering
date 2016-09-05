using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math3d {
	public class LinearInterpolator3 : IEnumerable <double3> {
		#region Properties
		public double3 Start;
		public double3 End;
		public double Range;
		#endregion Properties

		#region Constructors
		public LinearInterpolator3 ( double3 start, double3 end, double range ) {
			this.Start = start;
			this.End = end;
			this.Range = range;
		}
		#endregion Constructors

		#region Overrides
		public IEnumerator <double3> GetEnumerator () {
			return	new LinearInterpolator3Enumerator ( Start, End, Range );
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return	new LinearInterpolator3Enumerator ( Start, End, Range );
		}
		#endregion Overrides

		#region Methods
		public LinearInterpolator3Enumerator GetInterpolator () {
			return	new LinearInterpolator3Enumerator ( Start, End, Range );
		}
		#endregion Methods

		public class LinearInterpolator3Enumerator : IEnumerator <double3>, IInterpolatorEnumerator {
			#region Properties
			IEnumerator <double> xEnum;
			IEnumerator <double> yEnum;
			IEnumerator <double> zEnum;
			#endregion Properties

			#region Constructors
			public LinearInterpolator3Enumerator ( double3 start, double3 end, double range ) {
				xEnum = start.x.LerpTo ( end.x, range ).GetEnumerator ();
				yEnum = start.y.LerpTo ( end.y, range ).GetEnumerator ();
				zEnum = start.z.LerpTo ( end.z, range ).GetEnumerator ();
			}
			#endregion Constructors

			#region Overrides
			public double3 Current {
				get { return	new double3 ( xEnum.Current, yEnum.Current, zEnum.Current ); }
			}

			public void Dispose () {}

			object System.Collections.IEnumerator.Current {
				get { return	new double3 ( xEnum.Current, yEnum.Current, zEnum.Current ); }
			}

			public bool MoveNext () {
				return	MoveNext ( 1 );
			}

			public bool MoveNext ( double rangeDelta ) {
				return	( xEnum as IInterpolatorEnumerator ).MoveNext ( rangeDelta ) &&
						( yEnum as IInterpolatorEnumerator ).MoveNext ( rangeDelta ) &&
						( zEnum as IInterpolatorEnumerator ).MoveNext ( rangeDelta );
			}

			public void Reset () {
				xEnum.Reset ();
				yEnum.Reset ();
				zEnum.Reset ();
			}
			#endregion Overrides
		}
	}
}
