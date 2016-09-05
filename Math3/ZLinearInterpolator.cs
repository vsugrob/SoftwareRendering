using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math3d {
	public class ZLinearInterpolator : IEnumerable <double> {
		#region Properties
		public double Start;
		public double End;
		public double StartZ;
		public double EndZ;
		public double Range;
		#endregion Properties

		#region Constructors
		public ZLinearInterpolator ( double start, double end, double range,
			double startZ, double endZ )
		{
			this.Start = start;
			this.End = end;
			this.StartZ = startZ;
			this.EndZ = endZ;
			this.Range = range;
		}
		#endregion Constructors

		#region Overrides
		public IEnumerator <double> GetEnumerator () {
			return	new ZLinearInterpolatorEnumerator ( Start, End, Range, StartZ, EndZ );
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return	new ZLinearInterpolatorEnumerator ( Start, End, Range, StartZ, EndZ );
		}
		#endregion Overrides

		#region Methods
		public ZLinearInterpolatorEnumerator GetInterpolator () {
			return	new ZLinearInterpolatorEnumerator ( Start, End, Range, StartZ, EndZ );
		}
		#endregion Methods

		public class ZLinearInterpolatorEnumerator : IEnumerator <double>, IInterpolatorEnumerator {
			#region Properties
			LinearInterpolator.LinearInterpolatorEnumerator li, liZ;
			double start, end, cur;
			#endregion Properties

			#region Constructors
			public ZLinearInterpolatorEnumerator ( double start, double end, double range,
				double startZ, double endZ )
			{
				this.start = start;
				this.end = end;

				li = new LinearInterpolator ( start / startZ, end / endZ, range ).GetEnumerator ()
					as LinearInterpolator.LinearInterpolatorEnumerator;
				liZ = new LinearInterpolator ( 1 / startZ, 1 / endZ, range ).GetEnumerator ()
					as LinearInterpolator.LinearInterpolatorEnumerator;
				Reset ();
			}
			#endregion Constructors

			#region Overrides
			public double Current {
				get { return	cur; }
			}

			public void Dispose () {}

			object System.Collections.IEnumerator.Current {
				get { return	cur; }
			}

			public bool MoveNext () {
				return	MoveNext ( 1 );
			}

			public bool MoveNext ( double rangeDelta ) {
				bool r = li.MoveNext ( rangeDelta ) && liZ.MoveNext ( rangeDelta );
				cur = li.Current / liZ.Current;
				cur = cur.ClampBounds ( start, end );

				return	r;
			}

			public void Reset () {
				li.Reset ();
				liZ.Reset ();
			}
			#endregion Overrides
		}
	}
}
