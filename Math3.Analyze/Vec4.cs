using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace Math3.Analyze {
	public class Vec4 {
		#region Fields
		public E x, y, z, w;
		#endregion Fields

		#region Constructors
		public Vec4 ( E x, E y, E z, E w ) {
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;

			ThrowIfInvalid ();
		}

		public Vec4 ( double x, double y, double z, double w ) {
			this.x = E.NumConst ( x );
			this.y = E.NumConst ( y );
			this.z = E.NumConst ( z );
			this.w = E.NumConst ( w );
		}

		public Vec4 ( double4 v ) {
			this.x = E.NumConst ( v.x );
			this.y = E.NumConst ( v.y );
			this.z = E.NumConst ( v.z );
			this.w = E.NumConst ( v.w );
		}
		#endregion Constructors

		#region Operators
		public static E operator & ( Vec4 a, Vec4 b ) {
			return	E.Sum ( E.Mul ( a.x, b.x ), E.Mul ( a.y, b.y ), E.Mul ( a.z, b.z ), E.Mul ( a.w, b.w ) );
		}
		#endregion Operators

		#region Overrides
		public override string ToString () {
			return	string.Format ( "{0}   {1}   {2}   {3}", x, y, z, w );
		}
		#endregion Overrides

		#region Methods
		public bool IsValid () {
			return	x.InferredType == ExpressionType.Numeric &&
					y.InferredType == ExpressionType.Numeric &&
					z.InferredType == ExpressionType.Numeric &&
					w.InferredType == ExpressionType.Numeric;
		}

		void ThrowIfInvalid () {
			if ( !IsValid () )
				throw new InvalidCastException ();
		}

		public Vec4 Evaluate ( EvalSettings evalSettings = null ) {
			evalSettings = evalSettings ?? E.DefaultEvalSettings;
			Vec4 v = new Vec4 ( x.Evaluate ( evalSettings ), y.Evaluate ( evalSettings ),
				z.Evaluate ( evalSettings ), w.Evaluate ( evalSettings ) );

			return	v;
		}
		#endregion Methods
	}
}
