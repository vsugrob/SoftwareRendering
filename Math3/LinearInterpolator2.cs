using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math3d {
	public class LinearInterpolator2 : IEnumerable <double2> {
		#region Properties
		public double2 Start;
		public double2 End;
		public double Range;
		#endregion Properties

		#region Constructors
		public LinearInterpolator2 ( double2 start, double2 end, double range ) {
			this.Start = start;
			this.End = end;
			this.Range = range;
		}
		#endregion Constructors

		#region Overrides
		public IEnumerator <double2> GetEnumerator () {
			return	new LinearInterpolator2Enumerator ( Start, End, Range );
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return	new LinearInterpolator2Enumerator ( Start, End, Range );
		}
		#endregion Overrides

		#region Methods
		public LinearInterpolator2Enumerator GetInterpolator () {
			return	new LinearInterpolator2Enumerator ( Start, End, Range );
		}
		#endregion Methods

		public class LinearInterpolator2Enumerator : IEnumerator <double2>, IInterpolatorEnumerator {
			#region Properties
			IEnumerator <double> xEnum;
			IEnumerator <double> yEnum;
			#endregion Properties

			#region Constructors
			public LinearInterpolator2Enumerator ( double2 start, double2 end, double range ) {
				xEnum = start.x.LerpTo ( end.x, range ).GetEnumerator ();
				yEnum = start.y.LerpTo ( end.y, range ).GetEnumerator ();
			}
			#endregion Constructors

			#region Overrides
			public double2 Current {
				get { return	new double2 ( xEnum.Current, yEnum.Current ); }
			}

			public void Dispose () {}

			object System.Collections.IEnumerator.Current {
				get { return	new double2 ( xEnum.Current, yEnum.Current ); }
			}

			public bool MoveNext () {
				return	MoveNext ( 1 );
			}

			public bool MoveNext ( double rangeDelta ) {
				return	( xEnum as IInterpolatorEnumerator ).MoveNext ( rangeDelta ) &&
						( yEnum as IInterpolatorEnumerator ).MoveNext ( rangeDelta );
			}

			public void Reset () {
				xEnum.Reset ();
				yEnum.Reset ();
			}
			#endregion Overrides
		}
	}
}
