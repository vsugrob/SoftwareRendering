using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;
using System.Diagnostics;

namespace Math3.Analyze {
	public class Mat4 {
		#region Fields
		public E [,] M = new E [,] {
			{ E.One, E.Zero, E.Zero, E.Zero },
			{ E.Zero, E.One, E.Zero, E.Zero },
			{ E.Zero, E.Zero, E.One, E.Zero },
			{ E.Zero, E.Zero, E.Zero, E.One }
		};
		#endregion Fields

		#region Properties
		public static Mat4 Identity { get { return	new Mat4 (); } }

		#region Columns
		public Vec4 Column1 {
			get { return	new Vec4 ( M [0, 0], M [1, 0], M [2, 0], M [3, 0] ); }
			set {
				M [0, 0] = value.x;
				M [1, 0] = value.y;
				M [2, 0] = value.z;
				M [3, 0] = value.w;
			}
		}

		public Vec4 Column2 {
			get { return	new Vec4 ( M [0, 1], M [1, 1], M [2, 1], M [3, 1] ); }
			set {
				M [0, 1] = value.x;
				M [1, 1] = value.y;
				M [2, 1] = value.z;
				M [3, 1] = value.w;
			}
		}

		public Vec4 Column3 {
			get { return	new Vec4 ( M [0, 2], M [1, 2], M [2, 2], M [3, 2] ); }
			set {
				M [0, 2] = value.x;
				M [1, 2] = value.y;
				M [2, 2] = value.z;
				M [3, 2] = value.w;
			}
		}

		public Vec4 Column4 {
			get { return	new Vec4 ( M [0, 3], M [1, 3], M [2, 3], M [3, 3] ); }
			set {
				M [0, 3] = value.x;
				M [1, 3] = value.y;
				M [2, 3] = value.z;
				M [3, 3] = value.w;
			}
		}
		#endregion Columns

		#region Rows
		public Vec4 Row1 {
			get { return	new Vec4 ( M [0, 0], M [0, 1], M [0, 2], M [0, 3] ); }
			set {
				M [0, 0] = value.x;
				M [0, 1] = value.y;
				M [0, 2] = value.z;
				M [0, 3] = value.w;
			}
		}

		public Vec4 Row2 {
			get { return	new Vec4 ( M [1, 0], M [1, 1], M [1, 2], M [1, 3] ); }
			set {
				M [1, 0] = value.x;
				M [1, 1] = value.y;
				M [1, 2] = value.z;
				M [1, 3] = value.w;
			}
		}

		public Vec4 Row3 {
			get { return	new Vec4 ( M [2, 0], M [2, 1], M [2, 2], M [2, 3] ); }
			set {
				M [2, 0] = value.x;
				M [2, 1] = value.y;
				M [2, 2] = value.z;
				M [2, 3] = value.w;
			}
		}

		public Vec4 Row4 {
			get { return	new Vec4 ( M [3, 0], M [3, 1], M [3, 2], M [3, 3] ); }
			set {
				M [3, 0] = value.x;
				M [3, 1] = value.y;
				M [3, 2] = value.z;
				M [3, 3] = value.w;
			}
		}
		#endregion Rows

		public string AsHtml {
			get {
				string tableStyle = "border : 1px solid #bbb; border-collapse : collapse;";
				string tdStyle = "border : 1px dashed #bbb; text-align : center; padding : 4px;";
				string html = string.Format ( @"<table cellpadding=0 cellspacing=0 style=""{0}"">", tableStyle );

				for ( int m = 0 ; m < 4 ; m++ ) {
					html += "<tr>";

					for ( int n = 0 ; n < 4 ; n++ ) {
						html += string.Format ( @"<td style=""{0}"">{1}</td>",
							tdStyle, M [m, n].AsHtml
						);
					}

					html += "</tr>";
				}

				html += "</table>";

				return	html;
			}
		}

		public Mat4 Transposed {
			get {
				Mat4 mat = new Mat4 ();

				for ( int m = 0 ; m < 4 ; m++ )
					for ( int n = 0 ; n < 4 ; n++ )
						mat.M [n, m] = M [m, n];

				return	mat;
			}
		}
		#endregion Properties

		#region Constructors
		public Mat4 () {}

		public Mat4 ( E [,] exprs ) {
			for ( int m = 0 ; m < 4 ; m++ )
				for ( int n = 0 ; n < 4 ; n++ )
					M [m, n] = exprs [m, n];

			ThrowIfInvalid ();
		}

		public Mat4 ( double4x4 mat ) {
			for ( int m = 0 ; m < 4 ; m++ )
				for ( int n = 0 ; n < 4 ; n++ )
					M [m, n] = E.NumConst ( mat [m, n] );
		}

		public Mat4 ( double [,] values ) {
			for ( int m = 0 ; m < 4 ; m++ )
				for ( int n = 0 ; n < 4 ; n++ )
					M [m, n] = E.NumConst ( values [m, n] );
		}
		#endregion Constructors

		#region Factory Methods
		public static Mat4 RotX ( double a, MatrixLayout layout = MatrixLayout.Default ) {
			return	RotX ( E.NumConst ( Math.PI * a / 180 ), layout );
		}

		public static Mat4 RotX ( E radA, MatrixLayout layout = MatrixLayout.Default ) {
			return	RotX ( E.Cos ( radA ), E.Sin ( radA ), layout );
		}

		public static Mat4 RotX ( E cosA, E sinA, MatrixLayout layout = MatrixLayout.Default ) {
		    layout = Math3d.Math3.GetMatrixLayout ( layout );

		    if ( layout == MatrixLayout.RowMajor ) {
		        return	new Mat4 ( new E [,] {
		            {  E.One,            E.Zero, E.Zero, E.Zero },
		            { E.Zero,              cosA,   sinA, E.Zero },
		            { E.Zero, E.Negate ( sinA ),   cosA, E.Zero },
		            { E.Zero,            E.Zero, E.Zero,  E.One }
		        } );
		    } else {
		        return	new Mat4 ( new E [,] {
		            {  E.One, E.Zero,            E.Zero, E.Zero },
		            { E.Zero,   cosA, E.Negate ( sinA ), E.Zero },
		            { E.Zero,   sinA,              cosA, E.Zero },
		            { E.Zero, E.Zero,            E.Zero,  E.One }
		        } );
		    }
		}

		public static Mat4 RotY ( double a, MatrixLayout layout = MatrixLayout.Default ) {
			return	RotY ( E.NumConst ( Math.PI * a / 180 ), layout );
		}

		public static Mat4 RotY ( E radA, MatrixLayout layout = MatrixLayout.Default ) {
			return	RotY ( E.Cos ( radA ), E.Sin ( radA ), layout );
		}

		public static Mat4 RotY ( E cosA, E sinA, MatrixLayout layout = MatrixLayout.Default ) {
		    layout = Math3d.Math3.GetMatrixLayout ( layout );

		    if ( layout == MatrixLayout.RowMajor ) {
		        return	new Mat4 ( new E [,] {
		            {   cosA, E.Zero, E.Negate ( sinA ), E.Zero },
					{ E.Zero,  E.One,            E.Zero, E.Zero },
					{   sinA, E.Zero,              cosA, E.Zero },
					{ E.Zero, E.Zero,            E.Zero,  E.One }
		        } );
		    } else {
		        return	new Mat4 ( new E [,] {
		            {              cosA, E.Zero,   sinA, E.Zero },
					{            E.Zero,  E.One, E.Zero, E.Zero },
					{ E.Negate ( sinA ), E.Zero,   cosA, E.Zero },
					{            E.Zero, E.Zero, E.Zero,  E.One }
		        } );
		    }
		}

		public static Mat4 RotZ ( double a, MatrixLayout layout = MatrixLayout.Default ) {
			return	RotZ ( E.NumConst ( Math.PI * a / 180 ), layout );
		}

		public static Mat4 RotZ ( E radA, MatrixLayout layout = MatrixLayout.Default ) {
			return	RotZ ( E.Cos ( radA ), E.Sin ( radA ), layout );
		}

		public static Mat4 RotZ ( E cosA, E sinA, MatrixLayout layout = MatrixLayout.Default ) {
		    layout = Math3d.Math3.GetMatrixLayout ( layout );

		    if ( layout == MatrixLayout.RowMajor ) {
		        return	new Mat4 ( new E [,] {
		            {              cosA,   sinA, E.Zero, E.Zero },
					{ E.Negate ( sinA ),   cosA, E.Zero, E.Zero },
					{            E.Zero, E.Zero,  E.One, E.Zero },
					{            E.Zero, E.Zero, E.Zero,  E.One }
		        } );
		    } else {
		        return	new Mat4 ( new E [,] {
		            {   cosA, E.Negate ( sinA ), E.Zero, E.Zero },
					{   sinA,              cosA, E.Zero, E.Zero },
					{ E.Zero,            E.Zero,  E.One, E.Zero },
					{ E.Zero,            E.Zero, E.Zero,  E.One }
		        } );
		    }
		}

		public static Mat4 RotToXY ( double3 p, MatrixLayout layout = MatrixLayout.Default ) {
		    return	RotToXY ( new Vec4 ( E.NumConst ( p.x ), E.NumConst ( p.y ), E.NumConst ( p.z ), E.One ), layout );
		}

		public static Mat4 RotToXY ( Vec4 p, MatrixLayout layout = MatrixLayout.Default ) {
		    E _2 = E.NumConst ( 2 );
		    E projPxzLen = E.Sqrt ( E.Sum ( E.Pow ( p.x, _2 ),
		                                    E.Pow ( p.z, _2 ) ) );
		    E cosA = E.Div ( p.x, projPxzLen );
		    E sinA = E.Div ( p.z, projPxzLen );

		    return	RotY ( cosA, sinA, layout );
		}

		public static Mat4 RotToXZ ( double3 p, MatrixLayout layout = MatrixLayout.Default ) {
		    return	RotToXZ ( new Vec4 ( E.NumConst ( p.x ), E.NumConst ( p.y ), E.NumConst ( p.z ), E.One ), layout );
		}

		public static Mat4 RotToXZ ( Vec4 p, MatrixLayout layout = MatrixLayout.Default ) {
		    E _2 = E.NumConst ( 2 );
			E projPyzLen = E.Sqrt ( E.Sum ( E.Pow ( p.y, _2 ),
		                                    E.Pow ( p.z, _2 ) ) );
		    E cosA = E.Div ( p.z, projPyzLen );
		    E sinA = E.Div ( p.y, projPyzLen );

		    return	RotX ( cosA, sinA, layout );
		}

		public static Mat4 RotToYZ ( double3 p, MatrixLayout layout = MatrixLayout.Default ) {
		    return	RotToYZ ( new Vec4 ( E.NumConst ( p.x ), E.NumConst ( p.y ), E.NumConst ( p.z ), E.One ), layout );
		}

		public static Mat4 RotToYZ ( Vec4 p, MatrixLayout layout = MatrixLayout.Default ) {
		    E _2 = E.NumConst ( 2 );
		    E projPxyLen = E.Sqrt ( E.Sum ( E.Pow ( p.x, _2 ),
		                                    E.Pow ( p.y, _2 ) ) );
		    E cosA = E.Div ( p.x, projPxyLen );
		    E sinA = E.Div ( p.y, projPxyLen );

		    return	RotZ ( cosA, sinA, layout );
		}

		public static Mat4 RotToX ( double3 p, MatrixLayout layout = MatrixLayout.Default ) {
		    return	RotToX ( new Vec4 ( E.NumConst ( p.x ), E.NumConst ( p.y ), E.NumConst ( p.z ), E.One ), layout );
		}

		public static Mat4 RotToX ( Vec4 p, MatrixLayout layout = MatrixLayout.Default ) {
			Mat4 rotToXZ = RotToXZ ( p, layout );
		    E _2 = E.NumConst ( 2 );
		    E vLen = E.Sqrt ( E.Sum ( E.Pow ( p.x, _2 ),
									  E.Pow ( p.y, _2 ),
									  E.Pow ( p.z, _2 ) ) );
			E projPyzLen = E.Sqrt ( E.Sum ( E.Pow ( p.y, _2 ),
		                                    E.Pow ( p.z, _2 ) ) );
		    E cosA = E.Div ( p.x, vLen );
		    E sinA = E.Div ( projPyzLen, vLen );

		    return	( rotToXZ * RotY ( cosA, sinA, layout ) ).Evaluate ();
		}

		public static Mat4 RotToY ( double3 p, MatrixLayout layout = MatrixLayout.Default ) {
		    return	RotToY ( new Vec4 ( E.NumConst ( p.x ), E.NumConst ( p.y ), E.NumConst ( p.z ), E.One ), layout );
		}

		public static Mat4 RotToY ( Vec4 p, MatrixLayout layout = MatrixLayout.Default ) {
			Mat4 rotToXY = RotToXY ( p, layout );
		    E _2 = E.NumConst ( 2 );
		    E vLen = E.Sqrt ( E.Sum ( E.Pow ( p.x, _2 ),
									  E.Pow ( p.y, _2 ),
									  E.Pow ( p.z, _2 ) ) );
			E projPxzLen = E.Sqrt ( E.Sum ( E.Pow ( p.x, _2 ),
		                                    E.Pow ( p.z, _2 ) ) );
		    E cosA = E.Div ( p.y, vLen );
		    E sinA = E.Div ( projPxzLen, vLen );

		    return	( rotToXY * RotZ ( cosA, sinA, layout ) ).Evaluate ();
		}

		public static Mat4 RotToZ ( double3 p, MatrixLayout layout = MatrixLayout.Default ) {
		    return	RotToZ ( new Vec4 ( E.NumConst ( p.x ), E.NumConst ( p.y ), E.NumConst ( p.z ), E.One ), layout );
		}

		public static Mat4 RotToZ ( Vec4 p, MatrixLayout layout = MatrixLayout.Default ) {
			Mat4 rotToYZ = RotToYZ ( p, layout );
		    E _2 = E.NumConst ( 2 );
		    E vLen = E.Sqrt ( E.Sum ( E.Pow ( p.x, _2 ),
									  E.Pow ( p.y, _2 ),
									  E.Pow ( p.z, _2 ) ) );
			E projPxyLen = E.Sqrt ( E.Sum ( E.Pow ( p.x, _2 ),
		                                    E.Pow ( p.y, _2 ) ) );
		    E cosA = E.Div ( p.z, vLen );
		    E sinA = E.Div ( projPxyLen, vLen );

		    return	( rotToYZ * RotX ( cosA, sinA, layout ) ).Evaluate ();
		}

		public static Mat4 Trans ( double3 v, MatrixLayout layout = MatrixLayout.Default ) {
		    layout = Math3d.Math3.GetMatrixLayout ( layout );
		    Mat4 m = new Mat4 ();
			
		    if ( layout == MatrixLayout.RowMajor ) {
		        m.M [3, 0] = E.NumConst ( v.x );
		        m.M [3, 1] = E.NumConst ( v.y );
		        m.M [3, 2] = E.NumConst ( v.z );
		    } else {
		        m.M [0, 3] = E.NumConst ( v.x );
		        m.M [1, 3] = E.NumConst ( v.y );
		        m.M [2, 3] = E.NumConst ( v.z );
		    }

		    return	m;
		}

		public static Mat4 Trans ( E v, MatrixLayout layout = MatrixLayout.Default ) {
		    layout = Math3d.Math3.GetMatrixLayout ( layout );
		    Mat4 m = new Mat4 ();
			
		    if ( layout == MatrixLayout.RowMajor ) {
		        m.M [3, 0] = v.Prop ( "x" );
		        m.M [3, 1] = v.Prop ( "y" );
		        m.M [3, 2] = v.Prop ( "z" );
		    } else {
		        m.M [0, 3] = v.Prop ( "x" );
		        m.M [1, 3] = v.Prop ( "y" );
		        m.M [2, 3] = v.Prop ( "z" );
		    }

		    return	m;
		}

		public static Mat4 Scale ( double s ) {
			return	Scale ( E.NumConst ( s ) );
		}

		public static Mat4 Scale ( E s ) {
			Mat4 m = new Mat4 ();

			for ( int i = 0 ; i < 3 ; i++ )
				m.M [i, i] = s;

		    m.M [3, 3] = E.One;

			return	m;
		}

		public static Mat4 ScaleV ( double3 v ) {
			Mat4 m = new Mat4 ();
			string [] componentNames = new [] { "x", "y", "z" };

			for ( int i = 0 ; i < 3 ; i++ )
				m.M [i, i] = E.NumConst ( v [i] );

		    m.M [3, 3] = E.One;

			return	m;
		}

		public static Mat4 ScaleV ( E v ) {
			Mat4 m = new Mat4 ();
			string [] componentNames = new [] { "x", "y", "z" };

			for ( int i = 0 ; i < 3 ; i++ )
				m.M [i, i] = v.Prop ( componentNames [i] );

		    m.M [3, 3] = E.One;

			return	m;
		}
		#endregion Factory Methods

		#region Mandatory Overrides
		public override int GetHashCode () {
			int hash = 0;

			for ( int m = 0 ; m < 4 ; m++ )
				for ( int n = 0 ; n < 4 ; n++ )
					hash ^= M [m, n].GetHashCode ();

			return	hash;
		}

		public override bool Equals ( object obj ) {
			Mat4 matObj;

			if ( object.ReferenceEquals ( null, matObj = obj as Mat4 ) )
				return	false;

		    for ( int m = 0 ; m < 4 ; m++ )
				for ( int n = 0 ; n < 4 ; n++ )
					if ( !M [m, n].Equals ( matObj.M [m, n] ) )
						return	false;

			return	true;
		}
		#endregion Mandatory Overrides

		#region Operators
		public static bool operator == ( Mat4 m1, Mat4 m2 ) {
			return	object.ReferenceEquals ( m1, m2 ) || ( !object.ReferenceEquals ( m1, null ) && m1.Equals ( m2 ) );
		}

		public static bool operator != ( Mat4 m1, Mat4 m2 ) {
			return	!object.ReferenceEquals ( m1, m2 ) && ( !object.ReferenceEquals ( m1, null ) && !m1.Equals ( m2 ) );
		}

		public static Vec4 operator * ( Mat4 m, Vec4 v ) {
		    return	new Vec4 ( m.Row1 & v, m.Row2 & v, m.Row3 & v, m.Row4 & v );
		}

		public static Vec4 operator * ( Vec4 v, Mat4 m ) {
		    return	new Vec4 ( m.Column1 & v, m.Column2 & v, m.Column3 & v, m.Column4 & v );
		}

		public static Mat4 operator * ( Mat4 a, Mat4 b ) {
		    Mat4 c = new Mat4 ();
		    c.Row1 = a.Row1 * b;
		    c.Row2 = a.Row2 * b;
		    c.Row3 = a.Row3 * b;
		    c.Row4 = a.Row4 * b;

		    return	c;
		}
		#endregion Operators

		#region Methods
		public bool IsValid () {
			for ( int m = 0 ; m < 4 ; m++ )
				for ( int n = 0 ; n < 4 ; n++ )
					if ( M [m, n].InferredType != ExpressionType.Numeric )
						return	false;

			return	true;
		}

		void ThrowIfInvalid () {
			if ( !IsValid () )
				throw new InvalidCastException ();
		}

		public Mat4 Evaluate ( EvalSettings evalSettings = null ) {
			evalSettings = evalSettings ?? E.DefaultEvalSettings;
			Mat4 mat = new Mat4 ();

			for ( int m = 0 ; m < 4 ; m++ )
				for ( int n = 0 ; n < 4 ; n++ )
					mat.M [m, n] = M [m, n].Evaluate ( evalSettings, true );

			return	mat;
		}

		public Mat4 Simplify ( EvalSettings evalSettings = null ) {
			evalSettings = evalSettings ?? E.DefaultEvalSettings;
			Mat4 mat = new Mat4 ();

			for ( int m = 0 ; m < 4 ; m++ )
				for ( int n = 0 ; n < 4 ; n++ )
					mat.M [m, n] = M [m, n].Simplify ( evalSettings );

			return	mat;
		}

		public Mat4 EvaluateStepByStep ( out string htmlLog, params object [] varValues ) {
		    List <Tuple <string, string>> htmlSteps = new List <Tuple <string, string>> ();

		    EvalFlags [] stepFlags = new [] {
		        EvalFlags.ReduceZeroMultiplier | EvalFlags.ReduceUnitMultiplier | EvalFlags.ReduceUnitDivider,
		        EvalFlags.GroupByCommonFactor | EvalFlags.GroupMultipliersToFractions,
		        EvalFlags.SumFractions,
		        EvalFlags.SimplifyTrigonometricFunctions,
		        EvalFlags.EvalFuncs,
				EvalFlags.SubstituteVariables
		    };
			
		    Mat4 m = this;
			htmlSteps.Add ( Tuple.Create ( "Initial expression", m.AsHtml ) );

		    for ( int i = 0 ; i < stepFlags.Length ; i++ ) {
				Mat4 prevM = m;
				int c = 0;

		        do {
		            for ( int j = 0 ; j <= i ; j++ ) {
		                prevM = m;
						
		                EvalSettings evalSettings = new EvalSettings ( stepFlags [j], varValues );
						evalSettings.MaximumSimplicity = false;
						m = m.Evaluate ( evalSettings );

						if ( m != prevM )
							htmlSteps.Add ( Tuple.Create ( stepFlags [j].ToString (), m.AsHtml ) );
		            }

					c++;
		        } while ( m != prevM && c <= EvalSettings.INFINITE_LOOP_CYCLE_NUM );

				if ( c == EvalSettings.INFINITE_LOOP_CYCLE_NUM )
					Debug.Fail ( "Simplify Error", "It seems that simplification will never stop. Aborting." );
		    }

			htmlLog = string.Join ( "<br />", htmlSteps.Select ( step => string.Format ( "<h4>{0}:</h4>{1}", step.Item1, step.Item2 ) ) );

		    return	m;
		}
		#endregion Methods
	}
}
