using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math3d {
	public class LinearInterpolator : IEnumerable <double> {
		#region Properties
		public double Start;
		public double End;
		public double Range;
		#endregion Properties

		#region Constructors
		public LinearInterpolator ( double start, double end, double range ) {
			this.Start = start;
			this.End = end;
			this.Range = range;
		}
		#endregion Constructors

		#region Overrides
		public IEnumerator <double> GetEnumerator () {
			return	new LinearInterpolatorEnumerator ( Start, End, Range );
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return	new LinearInterpolatorEnumerator ( Start, End, Range );
		}
		#endregion Overrides

		#region Methods
		public LinearInterpolatorEnumerator GetInterpolator () {
			return	new LinearInterpolatorEnumerator ( Start, End, Range );
		}
		#endregion Methods

		public class LinearInterpolatorEnumerator : IEnumerator <double>, IInterpolatorEnumerator {
			#region Properties
			double range, rangeProgress;
			double start, end, val, valDelta;
			bool beforeFirst;
			#endregion Properties

			#region Constructors
			public LinearInterpolatorEnumerator ( double start, double end, double range ) {
				if ( range <= 0 )
					throw new ArgumentOutOfRangeException ( "range", range, "Argument range must be positive." );

				this.start = start;
				this.end = end;
				this.range = range;
				
				Reset ();
			}
			#endregion Constructors

			#region Overrides
			public double Current {
				get { return	val; }
			}

			public void Dispose () {}

			object System.Collections.IEnumerator.Current {
				get { return	val; }
			}

			public bool MoveNext () {
				return	MoveNext ( 1 );
			}

			public bool MoveNext ( double rangeDelta ) {
				if ( beforeFirst ) {
					beforeFirst = false;

					return	true;
				}

				if ( rangeProgress >= range )
					return	false;

				if ( rangeProgress + rangeDelta > range ) {
					val = end;
					rangeProgress = range;
				} else {
					val += valDelta * rangeDelta;
					val = val.ClampBounds ( start, end );
					rangeProgress += rangeDelta;
				}
				
				return	rangeProgress <= range;
			}

			public void Reset () {
				rangeProgress = 0;
				valDelta = ( end - start ) / range;
				val = start;
				beforeFirst = true;
			}
			#endregion Overrides
		}
	}
}
