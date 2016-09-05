using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math3.Analyze {
	public class NumericConstant : Literal {
		public double Value;
		public override bool IsValueNode { get { return	true; } }

		public override string AsHtml {
			get {
				return	Value.ToString ();
			}
		}

		public NumericConstant ( double value ) {
			this.Value = value;
		}

		public override bool IsNegative { get { return	Value < 0; } }
		public override E SignFree { get { return	Value < 0 ? E.NumConst ( -Value ) : this; } }

		public override ExpressionType InferredType {
			get { return	ExpressionType.Numeric; }
		}

		public override string ToString () {
			return	Value.ToString ();
		}

		#region Mandatory Overrides
		public override int GetHashCode () {
			return	Value.GetHashCode ();
		}

		public override bool Equals ( object obj ) {
			return	obj is NumericConstant && ( Math.Abs ( ( obj as NumericConstant ).Value - this.Value ) < Math3d.Math3.DIFF_THR );
		}
		#endregion Mandatory Overrides

		#region Operators
		public static bool operator == ( NumericConstant e1, NumericConstant e2 ) {
			return	object.ReferenceEquals ( e1, e2 ) || ( !object.ReferenceEquals ( e1, null ) && e1.Equals ( e2 ) );
		}

		public static bool operator != ( NumericConstant e1, NumericConstant e2 ) {
			return	!object.ReferenceEquals ( e1, e2 ) && ( !object.ReferenceEquals ( e1, null ) && !e1.Equals ( e2 ) );
		}
		#endregion Operators
	}
}
