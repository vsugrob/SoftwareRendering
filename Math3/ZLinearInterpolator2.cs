using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math3d {
	public class ZLinearInterpolator2 : IEnumerable <double2> {
		#region Properties
		public double2 Start;
		public double2 End;
		public double Range;
		public double StartZ;
		public double EndZ;
		#endregion Properties

		#region Constructors
		public ZLinearInterpolator2 ( double2 start, double2 end, double range,
			double startZ, double endZ )
		{
			this.Start = start;
			this.End = end;
			this.Range = range;
			this.StartZ = startZ;
			this.EndZ = endZ;
		}
		#endregion Constructors

		#region Overrides
		public IEnumerator <double2> GetEnumerator () {
			return	new ZLinearInterpolator2Enumerator ( Start, End, Range, StartZ, EndZ );
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return	new ZLinearInterpolator2Enumerator ( Start, End, Range, StartZ, EndZ );
		}
		#endregion Overrides

		#region Methods
		public ZLinearInterpolator2Enumerator GetInterpolator () {
			return	new ZLinearInterpolator2Enumerator ( Start, End, Range, StartZ, EndZ );
		}
		#endregion Methods

		public class ZLinearInterpolator2Enumerator : IEnumerator <double2>, IInterpolatorEnumerator {
			#region Properties
			IEnumerator <double> xEnum;
			IEnumerator <double> yEnum;
			#endregion Properties

			#region Constructors
			public ZLinearInterpolator2Enumerator ( double2 start, double2 end, double range,
				double startZ, double endZ )
			{
				xEnum = start.x.ZLerpTo ( end.x, range, startZ, endZ ).GetEnumerator ();
				yEnum = start.y.ZLerpTo ( end.y, range, startZ, endZ ).GetEnumerator ();
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
