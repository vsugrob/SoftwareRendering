using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace RayTrace {
	public class IntersectData {
		#region Properties
		public double3 P;
		public Traceable Object;
		#endregion Properties

		#region Constructors
		public IntersectData ( double3 p, Traceable obj ) {
			this.P = p;
			this.Object = obj;
		}
		#endregion Constructors

		#region Overrides
		public override string ToString () {
			return	string.Format ( "{0} at Point {{{1}}}", Object, P );
		}
		#endregion Overrides

		#region Mandatory Overrides
		public override int GetHashCode () {
			return	P.GetHashCode () ^ Object.GetHashCode ();
		}

		public override bool Equals ( object obj ) {
			IntersectData isecData;

			if ( object.ReferenceEquals ( null, isecData = obj as IntersectData ) ||
				 obj.GetType () != typeof ( IntersectData ) )
				return	false;

		    return	this.P == isecData.P && this.Object == isecData.Object;
		}
		#endregion Mandatory Overrides

		#region Operators
		public static bool operator == ( IntersectData isecData1, IntersectData isecData2 ) {
			return	object.ReferenceEquals ( isecData1, isecData2 ) || ( !object.ReferenceEquals ( isecData1, null ) && isecData1.Equals ( isecData2 ) );
		}

		public static bool operator != ( IntersectData isecData1, IntersectData isecData2 ) {
			return	!object.ReferenceEquals ( isecData1, isecData2 ) && ( !object.ReferenceEquals ( isecData1, null ) && !isecData1.Equals ( isecData2 ) );
		}
		#endregion Operators
	}
}
