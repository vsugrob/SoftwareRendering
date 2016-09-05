using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace RayTrace {
	public enum CsgOp {
		Union, Subtract, Intersect
	}

	public class CsgIntersectData : IntersectData {
	    #region Properties
	    public Traceable RealObject;
		public IntersectData RealData;
	    public bool IsFrontSurface;
	    #endregion Properties

	    #region Constructors
		public CsgIntersectData ( double3 p, Traceable obj,
			Traceable realObject, bool isFrontSurface,
			IntersectData realData ) : base ( p, obj )
		{
			this.RealObject = realObject;
			this.IsFrontSurface = isFrontSurface;
			this.RealData = realData;
		}
	    #endregion Constructors

		#region Mandatory Overrides
		public override int GetHashCode () {
			return	P.GetHashCode () ^ Object.GetHashCode () ^ RealObject.GetHashCode () ^ IsFrontSurface.GetHashCode ();
		}

		public override bool Equals ( object obj ) {
			CsgIntersectData isecData;

			if ( object.ReferenceEquals ( null, isecData = obj as CsgIntersectData ) ||
				 obj.GetType () != typeof ( CsgIntersectData ) )
				return	false;

		    return	this.P == isecData.P && this.Object == isecData.Object &&
					this.RealObject == isecData.RealObject && this.IsFrontSurface == isecData.IsFrontSurface;
		}
		#endregion Mandatory Overrides

		#region Operators
		public static bool operator == ( CsgIntersectData isecData1, CsgIntersectData isecData2 ) {
			return	object.ReferenceEquals ( isecData1, isecData2 ) || ( !object.ReferenceEquals ( isecData1, null ) && isecData1.Equals ( isecData2 ) );
		}

		public static bool operator != ( CsgIntersectData isecData1, CsgIntersectData isecData2 ) {
			return	!object.ReferenceEquals ( isecData1, isecData2 ) && ( !object.ReferenceEquals ( isecData1, null ) && !isecData1.Equals ( isecData2 ) );
		}
		#endregion Operators
	}

	public class CsgObject : Traceable {
		#region Properties
		public List <Traceable> Operands = new List <Traceable> ();
		public CsgOp Operation = CsgOp.Union;
		#endregion Properties

		#region Constructors
		public CsgObject ( CsgOp operation, params Traceable [] operands ) {
			this.Operation = operation;
			this.Operands = new List <Traceable> ( operands );
		}
		#endregion Constructors

		#region Overrides
		public override bool MayIntersect ( Ray r ) {
			if ( Operation == CsgOp.Subtract || Operation == CsgOp.Intersect )
				return	Operands [0].MayIntersect ( r );
			else if ( Operation == CsgOp.Union )
				return	Operands.Any ( obj => obj.MayIntersect ( r ) );
			else
				return	true;
		}

		public override List <IntersectData> Intersect ( Ray r ) {
			List <IntersectData> frontIsecs = new List <IntersectData> ();

			foreach ( Traceable operand in Operands )
				frontIsecs.AddRange ( operand.Intersect ( r ) );

			// Note: the following calculations assume that
			// nDotRay == 0 ignored cos it produces both
			// enter and exit at the same time.
			// Also operands should be non-self-intersecting figures.

			if ( frontIsecs.Count > 0 ) {
				List <IntersectData> backIsecs = new List <IntersectData> ();
				Ray nR = -r;

				foreach ( Traceable operand in Operands )
					backIsecs.AddRange ( operand.Intersect ( nR ) );

				var sortedIsecs = backIsecs.OrderByDescending ( isecData => ( isecData.P - r.p ).LengthSq )
					.Concat ( frontIsecs.OrderBy ( isecData => ( isecData.P - r.p ).LengthSq ) )
					.Distinct ();

				List <IntersectData> resultIsecs = new List <IntersectData> ();

				if ( Operation == CsgOp.Subtract ) {
					int numSubtrahendEnters = 0;
					int numOpAEnters = 0;

					foreach ( IntersectData isecData in sortedIsecs ) {
						bool isFrontSurface = true;
						bool InsideAAndExitedSubtrahends = false;
						bool InsideAAndEnteredSubtrahend = false;
						bool ExitedAAndOutsideSubtrahends = false;
						bool EnteredAAndOutsideSubtrahends = false;
						double3 n = isecData.Object.GetNormal ( isecData );
						double nDotRay = n & r.l;

						if ( isecData.Object != Operands [0] ) {
							if ( nDotRay < 0 ) {
								numSubtrahendEnters++;

								if ( numSubtrahendEnters == 1 && numOpAEnters == 1 ) {
									InsideAAndEnteredSubtrahend = true;
									isFrontSurface = false;
								}
							} else if ( nDotRay > 0 ) {
								numSubtrahendEnters--;

								if ( numSubtrahendEnters == 0 && numOpAEnters == 1 ) {
									InsideAAndExitedSubtrahends = true;
									isFrontSurface = false;
								}
							}
						} else {
							if ( nDotRay < 0 ) {
								numOpAEnters++;

								if ( numSubtrahendEnters == 0 && numOpAEnters == 1 )
									EnteredAAndOutsideSubtrahends = true;
							} else if ( nDotRay > 0 ) {
								numOpAEnters--;

								if ( numSubtrahendEnters == 0 && numOpAEnters == 0 )
									ExitedAAndOutsideSubtrahends = true;
							}
						}

						if ( InsideAAndExitedSubtrahends || InsideAAndEnteredSubtrahend ||
							 ExitedAAndOutsideSubtrahends || EnteredAAndOutsideSubtrahends )
						{
							CsgIntersectData csgIsecData = new CsgIntersectData ( isecData.P, this, isecData.Object, isFrontSurface, isecData );
							resultIsecs.Add ( csgIsecData );
						}
					}
				} else if ( Operation == CsgOp.Intersect ) {
					List <Traceable> insideList = new List <Traceable> ();
					bool isInside = false;

					foreach ( IntersectData isecData in sortedIsecs ) {
						double3 n = isecData.Object.GetNormal ( isecData );
						double nDotRay = n & r.l;

						if ( nDotRay < 0 ) {
							insideList.Add ( isecData.Object );

							if ( insideList.Count == Operands.Count ) {
								CsgIntersectData csgIsecData = new CsgIntersectData ( isecData.P, this, isecData.Object, true, isecData );
								resultIsecs.Add ( csgIsecData );
								isInside = true;
							}
						} else if ( nDotRay > 0 ) {
							insideList.Remove ( isecData.Object );

							if ( insideList.Count == Operands.Count - 1 && isInside ) {
								CsgIntersectData csgIsecData = new CsgIntersectData ( isecData.P, this, isecData.Object, true, isecData );
								resultIsecs.Add ( csgIsecData );
								isInside = false;
							}
						}
					}
				} else if ( Operation == CsgOp.Union ) {
					List <Traceable> insideList = new List <Traceable> ();

					foreach ( IntersectData isecData in sortedIsecs ) {
						double3 n = isecData.Object.GetNormal ( isecData );
						double nDotRay = n & r.l;

						if ( nDotRay < 0 ) {
							insideList.Add ( isecData.Object );

							if ( insideList.Count == 1 ) {
								CsgIntersectData csgIsecData = new CsgIntersectData ( isecData.P, this, isecData.Object, true, isecData );
								resultIsecs.Add ( csgIsecData );
							}
						} else if ( nDotRay > 0 ) {
							insideList.Remove ( isecData.Object );

							if ( insideList.Count == 0 ) {
								CsgIntersectData csgIsecData = new CsgIntersectData ( isecData.P, this, isecData.Object, true, isecData );
								resultIsecs.Add ( csgIsecData );
							}
						}
					}
				}

				resultIsecs = resultIsecs.Where ( isecData => !backIsecs.Contains ( ( isecData as CsgIntersectData ).RealData ) ).ToList ();

				return	resultIsecs;
			} else
				return frontIsecs;
		}

		public override double3 GetNormal ( IntersectData data ) {
			CsgIntersectData csgIsecData = data as CsgIntersectData;
			double3 n = csgIsecData.RealObject.GetNormal ( csgIsecData.RealData );

			return	csgIsecData.IsFrontSurface ? n : -n;
		}

		public override double2 GetTexCoord ( IntersectData data ) {
			throw new NotImplementedException ();
		}

		public override double3 GetTangent ( IntersectData data ) {
			throw new NotImplementedException ();
		}

		public override double3 GetBinormal ( IntersectData data ) {
			throw new NotImplementedException ();
		}
		#endregion Overrides
	}
}
