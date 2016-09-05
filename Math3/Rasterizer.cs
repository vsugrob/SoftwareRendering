using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;

namespace Math3d {
	public static class Rasterizer {
		public static Rasterizer <TData> Create <TData> ( int lowerBound, int upperBound, double from, double to,
			TData data, params IInterpolatorEnumerator [] enumerators )
		{
			return	new Rasterizer <TData> ( lowerBound, upperBound, from, to, data, enumerators );
		}
	}

	public class Rasterizer <TData> : IEnumerable <Tuple <double, TData>> {
		#region Properties
		public int LowerBound;
		public int UpperBound;
		public double From;
		public double To;
		public TData Data;
		public IInterpolatorEnumerator [] Enumerators;
		#endregion Properties

		#region Constructors
		public Rasterizer ( int lowerBound, int upperBound, double from, double to, TData data, params IInterpolatorEnumerator [] enumerators ) {
			this.LowerBound = lowerBound;
			this.UpperBound = upperBound;
			this.From = from;
			this.To = to;
			this.Data = data;
			this.Enumerators = enumerators;
		}
		#endregion Constructors

		#region Overrides
		public IEnumerator <Tuple <double, TData>> GetEnumerator () {
			Debug.Assert ( LowerBound <= UpperBound );

			var interpEnumerators = Enumerators.Cast <IInterpolatorEnumerator> ();
			int iFrom, iTo, iCur, numInts;
			bool plotLast = false;
			double dy = To - From;
			int step = Math.Sign ( dy );
			interpEnumerators.Advance ( 50513810 );

			if ( From < LowerBound ) {
				interpEnumerators.Advance ( LowerBound - From );
				iFrom = LowerBound;
			} else if ( From > UpperBound ) {
				interpEnumerators.Advance ( From - UpperBound );
				iFrom = UpperBound;
			} else {
				int nextIntFrom = ( int ) From;

				if ( nextIntFrom != From ) {
					nextIntFrom = From.NextIntAlongDir ( step );
					int curIntFrom = From.PreciseRound ();

					if ( curIntFrom != nextIntFrom )
						yield return	Tuple.Create ( From, Data );

					interpEnumerators.Advance ( Math.Abs ( nextIntFrom - From ) );
				}

				iFrom = nextIntFrom;
			}

			if ( To > UpperBound )
				iTo = UpperBound;
			else if ( To < LowerBound )
				iTo = LowerBound;
			else {
				int prevIntOfTo = ( int ) To;

				if ( prevIntOfTo != To ) {
					prevIntOfTo = To.PrevIntAlongDir ( step );
					int curIntOfTo = To.PreciseRound ();

					if ( curIntOfTo != prevIntOfTo )
						plotLast = true;
				}

				iTo = prevIntOfTo;
			}

			int idy = iTo - iFrom;

			if ( Math.Sign ( dy ) == Math.Sign ( idy ) ) {
				numInts = Math.Abs ( idy ) + 1;
				iCur = iFrom;

				for ( int i = 0 ; i < numInts ; i++ ) {
					yield return	Tuple.Create ( ( double ) iCur, Data );

					interpEnumerators.Advance ( 1 );
					iCur += step;
				}

				if ( plotLast ) {
					interpEnumerators.Advance ( Math.Abs ( To - iTo ) );

					yield return	Tuple.Create ( To, Data );
				}
			}

			foreach ( var enumerator in interpEnumerators )
				( enumerator as IEnumerator ).Reset ();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return	this.GetEnumerator ();
		}
		#endregion Overrides

		public class RasterizerEnumerator : IEnumerator <Tuple <double, TData>> {
			#region Properties
			int lowerBound, upperBound;
			double from, to, cur;
			int iFrom, iTo, iCur, step, numInts, curIntIdx;
			TData data;
			IInterpolatorEnumerator [] enumerators;
			bool plotFirst = false, plotLast = false;
			#endregion Properties

			#region Constructors
			public RasterizerEnumerator ( int lowerBound, int upperBound, double from, double to,
				TData data, IInterpolatorEnumerator [] enumerators )
			{
				Debug.Assert ( lowerBound <= upperBound );

				this.lowerBound = lowerBound;
				this.upperBound = upperBound;
				this.from = from;
				this.to = to;
				this.data = data;
				this.enumerators = enumerators;

				Reset ();
			}
			#endregion Constructors

			#region Overrides
			public Tuple <double, TData> Current {
				get { return	Tuple.Create ( cur, data ); }
			}

			public void Dispose () {}

			object System.Collections.IEnumerator.Current {
				get { return	Tuple.Create ( cur, data ); }
			}

			public bool MoveNext () {
				if ( plotFirst ) {
					cur = from;
					plotFirst = false;
				} else {
					iCur += step;
					curIntIdx++;

					if ( curIntIdx < numInts ) {
						cur = iCur;
						enumerators.Advance ( 1 );
					} else if ( plotLast ) {
						cur = to;
						enumerators.Advance ( Math.Abs ( to - iTo ) );
						plotLast = false;
					} else {
						Reset ();
						ResetChildren ();

						return	false;
					}
				}

				return	true;
			}

			public void Reset () {
				step = Math.Sign ( to - from );

				if ( from < lowerBound ) {
					enumerators.Advance ( lowerBound - from );
					iFrom = lowerBound;
				} else if ( from > upperBound ) {
					enumerators.Advance ( from - upperBound );
					iFrom = upperBound;
				} else {
					int nextIntFrom = ( int ) from;

					if ( nextIntFrom != from ) {
						nextIntFrom = from.NextIntAlongDir ( step );
						int curIntFrom = from.PreciseRound ();

						if ( curIntFrom != nextIntFrom )
						    plotFirst = true;

						enumerators.Advance ( Math.Abs ( nextIntFrom - from ) );
					}

					iFrom = nextIntFrom;
				}

				if ( to > upperBound )
					iTo = upperBound;
				else if ( to < lowerBound )
					iTo = lowerBound;
				else {
					int prevIntOfTo = ( int ) to;

					if ( prevIntOfTo != to ) {
						prevIntOfTo = to.PrevIntAlongDir ( step );
						int curIntOfTo = to.PreciseRound ();

						if ( curIntOfTo != prevIntOfTo )
							plotLast = true;
					}

					iTo = prevIntOfTo;
				}

				numInts = Math.Abs ( iTo - iFrom ) + 1;
				iCur = iFrom - step;
				curIntIdx = -1;
			}
			#endregion Overrides

			#region Methods
			void ResetChildren () {
				foreach ( var enumerator in enumerators )
					( enumerator as IEnumerator <double> ).Reset ();
			}
			#endregion Methods
		}
	}
}
