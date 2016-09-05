using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CodeGenerator {
	public static class VectorCodeGenerator {
		static string [][] componentSets = new [] {
			new [] { "x", "y", "z", "w" },
			new [] { "r", "g", "b", "a" },
			new [] { "s", "t", "p", "q" }
		};
		
		public static void Generate () {
			if ( !Directory.Exists ( "Vectors" ) )
				Directory.CreateDirectory ( "Vectors" );
			
			GenSwizzles ();
			GenVectors ();
		}

		#region Vectors
		static void GenVectors () {
			for ( int dimension = 2 ; dimension <= 4 ; dimension++ ) {
				string typeName = string.Format ( @"{0}{1}", Globals.ComponentType, dimension );
				string fields = GetFieldsCodeForDimension ( dimension );
				string consts = GetConstsCodeForDimension ( dimension );
				string props = GetPropertiesCodeForDimension ( dimension );
				string ctors = GetConstructorsCodeForDimension ( dimension );
				string methods = GetMethodsCodeForDimension ( dimension );
				string overrides = GetOverridesCodeForDimension ( dimension );
				string ops = GetOperatorsCodeForDimension ( dimension );

				StringBuilder code = new StringBuilder ();
				code.AppendLine ( "using System;" );
				code.AppendLine ( "using System.Runtime.InteropServices;" );
				code.AppendLine ( "using System.ComponentModel;" );
				code.AppendLine ();
				code.AppendLine ( string.Format ( "namespace {0} {{", Globals.Namespace ) );
				code.AppendLine ( "\t[StructLayout ( LayoutKind.Explicit )]" );
				code.AppendLine ( string.Format ( "\tpublic partial struct {0}{1} {{", Globals.ComponentType, dimension ) );
				code.AppendLine ( Utils.IndentRight ( fields, "\t\t" ) );
				code.AppendLine ( Utils.IndentRight ( consts, "\t\t" ) );
				code.AppendLine ( Utils.IndentRight ( props, "\t\t" ) );
				code.AppendLine ( Utils.IndentRight ( ctors, "\t\t" ) );
				code.AppendLine ( Utils.IndentRight ( methods, "\t\t" ) );
				code.AppendLine ( Utils.IndentRight ( overrides, "\t\t" ) );
				code.AppendLine ( Utils.IndentRight ( ops, "\t\t" ) );
				code.AppendLine ( "\t}" );
				code.AppendLine ( "}" );

				File.WriteAllText ( string.Format ( @"Vectors\{0}.cs", typeName ), code.ToString () );
			}
		}

		#region Fields
		static string GetFieldsCodeForDimension ( int dimension ) {
			StringBuilder code = new StringBuilder ( "#region Fields" );
			code.AppendLine ();

			for ( int i = 0 ; i < componentSets.Length ; i++ ) {
				string [] componentSet = componentSets [i];
				int maxComponents = Math.Min ( dimension, componentSet.Length );

				for ( int c = 0; c < maxComponents; c++ ) {
					if ( i != 0 )
						code.AppendLine ( "[EditorBrowsable ( EditorBrowsableState.Advanced )]" );
					
					code.AppendLine ( string.Format ( "[FieldOffset ( {0} )]", c * Globals.ComponentSize ) );
					code.AppendLine ( string.Format ( "public {0} {1};", Globals.ComponentType, componentSet [c] ) );
				}
			}

			code.AppendLine ( "#endregion Fields" );

			return	code.ToString ();
		}
		#endregion Fields

		#region Constants
		static string GetConstsCodeForDimension ( int dimension ) {
			StringBuilder code = new StringBuilder ( "#region Constants" );
			code.AppendLine ();

			string typeName = string.Format ( "{0}{1}", Globals.ComponentType, dimension );
			code.AppendLine ( string.Format ( "public static readonly {0} Zero = new {0} ( 0 );", typeName ) );

			string [] coordComponents = componentSets [0];
			int maxComponents = Math.Min ( dimension, coordComponents.Length );

			for ( int c = 0; c < maxComponents; c++ ) {
				code.AppendLine ( string.Format ( "public static readonly {0} Unit{1} = new {0} ( {2} );",
					typeName, coordComponents [c].ToUpper (),
					string.Join ( ", ", Enumerable.Range ( 0, maxComponents ).Select ( i => i != c ? "0" : "1" ) ) ) );
			}

			code.AppendLine ( "#endregion Constants" );

			return	code.ToString ();
		}
		#endregion Constants

		#region Properties
		static string GetPropertiesCodeForDimension ( int dimension ) {
			StringBuilder code = new StringBuilder ( "#region Properties" );
			code.AppendLine ();
			List <string> props = new List <string> ();

			for ( int d = 2 ; d <= dimension ; d++ ) {
				props.Add ( GetLengthPropertyCodeForDimension ( d, dimension ) );
				props.Add ( GetLengthSqPropertyCodeForDimension ( d, dimension ) );
				props.Add ( GetNormalizedPropertyCodeForDimension ( d, dimension ) );
			}

			props.Add ( GetItemPropertyCodeForDimension ( dimension ) );

			code.Append ( string.Join ( "\r\n", props ) );
			code.AppendLine ( "#endregion Properties" );

			return	code.ToString ();
		}

		static string GetLengthPropertyCodeForDimension ( int dimension, int thisDimension ) {
			StringBuilder code = new StringBuilder ( string.Format ( "public {0} Length{1} {{",
				Globals.ComponentType, thisDimension != dimension ? dimension.ToString () : "" ) );
			code.AppendLine ();

			string typeName = string.Format ( "{0}{1}", Globals.ComponentType, dimension );
			string [] coordComponents = componentSets [0];
			int maxComponents = Math.Min ( dimension, coordComponents.Length );

			string getterStr = string.Format ( "get {{ return	Math.Sqrt ( {0} ); }}\r\n",
				string.Join ( " + ", Enumerable.Range ( 0, maxComponents )
					.Select ( i => string.Format ( "{0} * {0}", coordComponents [i] ) ) ) );

			StringBuilder setter = new StringBuilder ( string.Format ( "set {{\r\n\t{0} n = this.Normalized{1};", typeName, thisDimension != dimension ? dimension.ToString () : "" ) );
			setter.AppendLine ();
			setter.AppendLine ();

			for ( int c = 0; c < maxComponents; c++ )
				setter.AppendLine ( string.Format ( "\t{0} = n.{0} * value;", coordComponents [c] ) );

			setter.AppendLine ( "}" );
			getterStr = Utils.IndentRight ( getterStr, "\t" );
			string setterStr = Utils.IndentRight ( setter.ToString (), "\t" );

			code.Append ( string.Format ( "{0}{1}", getterStr, setterStr ) );
			code.AppendLine ( "}" );

			return	code.ToString ();
		}

		static string GetLengthSqPropertyCodeForDimension ( int dimension, int thisDimension ) {
			StringBuilder code = new StringBuilder ( string.Format ( "public {0} LengthSq{1} {{",
				Globals.ComponentType, thisDimension != dimension ? dimension.ToString () : "" ) );
			code.AppendLine ();

			string typeName = string.Format ( "{0}{1}", Globals.ComponentType, dimension );
			string [] coordComponents = componentSets [0];
			int maxComponents = Math.Min ( dimension, coordComponents.Length );

			string getterStr = string.Format ( "get {{ return	{0}; }}\r\n",
				string.Join ( " + ", Enumerable.Range ( 0, maxComponents )
					.Select ( i => string.Format ( "{0} * {0}", coordComponents [i] ) ) ) );

			StringBuilder setter = new StringBuilder ( string.Format ( "set {{\r\n\t{0} n = this.Normalized{1};", typeName, thisDimension != dimension ? dimension.ToString () : "" ) );
			setter.AppendLine ();
			setter.AppendLine ( string.Format ( "\t{0} l = Math.Sqrt ( value );", Globals.ComponentType ) );

			for ( int c = 0; c < maxComponents; c++ )
				setter.AppendLine ( string.Format ( "\t{0} = n.{0} * l;", coordComponents [c] ) );

			setter.AppendLine ( "}" );
			getterStr = Utils.IndentRight ( getterStr, "\t" );
			string setterStr = Utils.IndentRight ( setter.ToString (), "\t" );

			code.Append ( string.Format ( "{0}{1}", getterStr, setterStr ) );
			code.AppendLine ( "}" );

			return	code.ToString ();
		}

		static string GetNormalizedPropertyCodeForDimension ( int dimension, int thisDimension ) {
			string typeName = string.Format ( "{0}{1}", Globals.ComponentType, thisDimension != dimension ? dimension : thisDimension );

			StringBuilder code = new StringBuilder ( string.Format ( "public {0} Normalized{1} {{",
				typeName, thisDimension != dimension ? dimension.ToString () : "" ) );
			code.AppendLine ();

			string [] coordComponents = componentSets [0];
			int maxComponents = Math.Min ( dimension, coordComponents.Length );

			string getterStr = string.Format ( "get {{\r\n\t{0} s = 1 / this.Length;\r\n\r\n\treturn	new {1} ( {2} );\r\n}}\r\n",
				Globals.ComponentType, typeName,
				string.Join ( ", ", Enumerable.Range ( 0, maxComponents )
					.Select ( i => string.Format ( "{0} * s", coordComponents [i] ) ) ) );

			getterStr = Utils.IndentRight ( getterStr, "\t" );

			code.Append ( getterStr );
			code.AppendLine ( "}" );

			return	code.ToString ();
		}

		static string GetItemPropertyCodeForDimension ( int dimension ) {
			StringBuilder code = new StringBuilder ( string.Format ( "public unsafe {0} this [int c] {{",
				Globals.ComponentType ) );
			code.AppendLine ();

			string getterStr = string.Format (
@"get {{
	if ( c > {0} || c < 0 )
		throw new IndexOutOfRangeException ();

	fixed ( {1} * ptr = &this.x ) {{
		return	*( ptr + c );
	}}
}}
",
			dimension, Globals.ComponentType );

			string setterStr = string.Format (
@"set {{
	if ( c > {0} || c < 0 )
		throw new IndexOutOfRangeException ();

	fixed ( {1} * ptr = &this.x ) {{
		*( ptr + c ) = value;
	}}
}}
",
			dimension, Globals.ComponentType );

			getterStr = Utils.IndentRight ( getterStr, "\t" );
			setterStr = Utils.IndentRight ( setterStr, "\t" );

			code.Append ( string.Format ( "{0}{1}", getterStr, setterStr ) );
			code.AppendLine ( "}" );

			return	code.ToString ();
		}
		#endregion Properties

		#region Constructors
		static string GetConstructorsCodeForDimension ( int dimension ) {
			StringBuilder code = new StringBuilder ( "#region Constructors" );
			code.AppendLine ();
			List <string> ctors = new List <string> ();

			ctors.Add ( GetComponentConstructorCodeForDimension ( dimension ) );
			ctors.Add ( GetSingleComponentConstructorCodeForDimension ( dimension ) );
			ctors.Add ( GetMixedComponentAndVectorConstructorCodeForDimension ( dimension ) );

			code.AppendLine ( string.Join ( "\r\n", ctors ) );
			code.AppendLine ( "#endregion Constructors" );

			return	code.ToString ();
		}

		static string GetComponentConstructorCodeForDimension ( int dimension ) {
			string typeName = string.Format ( "{0}{1}", Globals.ComponentType, dimension );
			string [] coordComponents = componentSets [0];

			StringBuilder code = new StringBuilder ( string.Format ( "public {0} ( {1} ) : this () {{",
				typeName,
				string.Join ( ", ", Enumerable.Range ( 0, dimension )
					.Select ( i => string.Format ( "{0} {1}", Globals.ComponentType, coordComponents [i] ) ) )
			) );

			code.AppendLine ();
			List <string> initLines = new List <string> ();
			
			int maxComponents = Math.Min ( dimension, coordComponents.Length );
			string initLine = "\t" + string.Join ( " ", Enumerable.Range ( 0, maxComponents )
				.Select ( c => string.Format ( "this.{0} = {1}; ", coordComponents [c], coordComponents [c] ) ) );
				
			initLines.Add ( initLine );

			code.AppendLine ( string.Join ( "\r\n", initLines ) );
			code.AppendLine ( "}" );

			return	code.ToString ();
		}

		static string GetSingleComponentConstructorCodeForDimension ( int dimension ) {
			string typeName = string.Format ( "{0}{1}", Globals.ComponentType, dimension );

			StringBuilder code = new StringBuilder ( string.Format ( "public {0} ( {1} a ) : this ( {2} ) {{}}",
				typeName, Globals.ComponentType,
				string.Join ( ", ", Enumerable.Range ( 0, dimension )
					.Select ( i => string.Format ( "a", Globals.ComponentType ) ) )
			) );

			return	code.ToString ();
		}

		static string GetMixedComponentAndVectorConstructorCodeForDimension ( int dimension ) {
			string code = string.Empty;
			
			if ( dimension == 4 ) {
				code = string.Format ( @"
public {0}4 ( {0}2 v1, {0}2 v2 ) : this () {{
	this.x = v1.x;  this.y = v1.y;  this.z = v2.x;  this.w = v2.y; 
}}

public {0}4 ( {0}2 v, {0} z, {0} w ) : this () {{
	this.x = v.x;  this.y = v.y;  this.z = z;  this.w = w; 
}}

public {0}4 ( {0} x, {0}2 v, {0} w ) : this () {{
	this.x = x;  this.y = v.x;  this.z = v.y;  this.w = w; 
}}

public {0}4 ( {0} x, {0} y, {0}2 v ) : this () {{
	this.x = x;  this.y = y;  this.z = v.x;  this.w = v.y; 
}}

public {0}4 ( {0}3 v, {0} w ) : this () {{
	this.x = v.x;  this.y = v.y;  this.z = v.z;  this.w = w; 
}}

public {0}4 ( {0} x, {0}3 v ) : this () {{
	this.x = x;  this.y = v.x;  this.z = v.y;  this.w = v.z; 
}}

public {0}4 ( {0}4 v ) : this () {{
	this.x = v.x;  this.y = v.y;  this.z = v.z;  this.w = v.w; 
}}
", Globals.ComponentType );
			} else if ( dimension == 3 ) {
				code = string.Format ( @"
public {0}3 ( {0}2 v, {0} z ) : this () {{
	this.x = v.x;  this.y = v.y;  this.z = z;
}}

public {0}3 ( {0} x, {0}2 v ) : this () {{
	this.x = x;  this.y = v.x;  this.z = v.y;
}}

public {0}3 ( {0}3 v ) : this () {{
	this.x = v.x;  this.y = v.y;  this.z = v.z;
}}
", Globals.ComponentType );
			} else if ( dimension == 2 ) {
				code = string.Format ( @"
public {0}2 ( {0}2 v ) : this () {{
	this.x = v.x;  this.y = v.y;
}}
", Globals.ComponentType );
			}

			return	code;
		}
		#endregion Constructors

		#region Methods
		static string GetMethodsCodeForDimension ( int dimension ) {
			StringBuilder code = new StringBuilder ( "#region Methods" );
			code.AppendLine ();
			List <string> methods = new List <string> ();

			methods.Add ( GetDistanceMethodCodeForDimension ( dimension ) );
			methods.Add ( GetDistanceSqMethodCodeForDimension ( dimension ) );

			if ( dimension < 4 ) {
				methods.Add ( GetReflectMethodsCodeForDimension ( dimension ) );
				methods.Add ( GetRefractMethodsCodeForDimension ( dimension ) );
			}

			code.AppendLine ( string.Join ( "\r\n", methods ) );
			code.AppendLine ( "#endregion Methods" );

			return	code.ToString ();
		}

		static string GetDistanceMethodCodeForDimension ( int dimension ) {
			string typeName = string.Format ( "{0}{1}", Globals.ComponentType, dimension );

			StringBuilder code = new StringBuilder ( string.Format ( "public static {0} D ( {1} a, {1} b ) {{\r\n\treturn	( a - b ).Length;\r\n}}",
				Globals.ComponentType, typeName
			) );

			code.AppendLine ();

			return	code.ToString ();
		}

		static string GetDistanceSqMethodCodeForDimension ( int dimension ) {
			string typeName = string.Format ( "{0}{1}", Globals.ComponentType, dimension );

			StringBuilder code = new StringBuilder ( string.Format ( "public static {0} DSq ( {1} a, {1} b ) {{\r\n\treturn	( a - b ).LengthSq;\r\n}}",
				Globals.ComponentType, typeName
			) );

			code.AppendLine ();

			return	code.ToString ();
		}

		static string GetReflectMethodsCodeForDimension ( int dimension ) {
			string typeName = string.Format ( "{0}{1}", Globals.ComponentType, dimension );

			return	string.Format (
@"public {0} Reflect ( {0} n ) {{
	return	( 2 * n * ( this & n ) - this ).Normalized;
}}

public {0} Reflect ( {0} n, double nDotV ) {{
	return	( 2 * n * nDotV - this ).Normalized;
}}

public {0} ReflectI ( {0} n ) {{
	return	( this - 2 * n * ( this & n ) ).Normalized;
}}

public {0} ReflectI ( {0} n, double nDotV ) {{
	return	( this - 2 * n * nDotV ).Normalized;
}}
", typeName );
		}

		static string GetRefractMethodsCodeForDimension ( int dimension ) {
			string typeName = string.Format ( "{0}{1}", Globals.ComponentType, dimension );

			return	string.Format (
@"public {0} Refract ( {0} n, double nDotV, double k ) {{
	double cosF = Math.Sqrt ( 1 - k * k * ( 1 - nDotV * nDotV ) );
			
	if ( nDotV >= 0 )
		return	n * ( k * nDotV - cosF ) - this * k;
	else
		return	n * ( k * nDotV + cosF ) - this * k;
}}

public {0} Refract ( {0} n, double k ) {{
	return	this.Refract ( n, n & this, k );
}}

public {0} RefractI ( {0} n, double nDotV, double k ) {{
	double cosF = Math.Sqrt ( 1 - k * k * ( 1 - nDotV * nDotV ) );
	nDotV = -nDotV;
	
	if ( nDotV >= 0 )
		return	n * ( k * nDotV - cosF ) + this * k;
	else
		return	n * ( k * nDotV + cosF ) + this * k;
}}

public {0} RefractI ( {0} n, double k ) {{
	return	this.RefractI ( n, n & this, k );
}}
", typeName );
		}
		#endregion Methods

		#region Overrides
		static string GetOverridesCodeForDimension ( int dimension ) {
			StringBuilder code = new StringBuilder ( "#region Overrides" );
			code.AppendLine ();
			List <string> methods = new List <string> ();

			methods.Add ( GetEqualsOverrideCodeForDimension ( dimension ) );
			methods.Add ( GetGetHashCodeOverrideCodeForDimension ( dimension ) );
			methods.Add ( GetToStringOverrideCodeForDimension ( dimension ) );
			
			code.AppendLine ( string.Join ( "\r\n", methods ) );
			code.AppendLine ( "#endregion Overrides" );

			return	code.ToString ();
		}

		static string GetEqualsOverrideCodeForDimension ( int dimension ) {
			string typeName = string.Format ( "{0}{1}", Globals.ComponentType, dimension );
			string [] coordComponents = componentSets [0];
			int maxComponents = Math.Min ( dimension, coordComponents.Length );

			StringBuilder code = new StringBuilder ( string.Format (
@"public override bool Equals ( object obj ) {{
	if ( obj == null || !( obj is {0} ) )
		return	false;

	{0} v = ( {0} ) obj;

	return	{1};
}}
",
				typeName,
				string.Join ( " && ", Enumerable.Range ( 0, maxComponents )
					.Select ( c => string.Format ( "Math.Abs ( {0} - v.{0} ) <= Math3.DIFF_THR", coordComponents [c] ) )
				)
			) );

			return	code.ToString ();
		}

		static string GetGetHashCodeOverrideCodeForDimension ( int dimension ) {
			string typeName = string.Format ( "{0}{1}", Globals.ComponentType, dimension );
			string [] coordComponents = componentSets [0];
			int maxComponents = Math.Min ( dimension, coordComponents.Length );

			StringBuilder code = new StringBuilder ( string.Format (
@"public override int GetHashCode () {{
{0}
	
	return	{1};
}}
",
				string.Join ( "\r\n", Enumerable.Range ( 0, maxComponents )
					.Select ( c => string.Format ( "\tint i{0} = ( int ) ( {0} * Math3.DOUBLE_PRECISION );", coordComponents [c] ) )
				),
				string.Join ( " ^ ", Enumerable.Range ( 0, maxComponents )
					.Select ( c => string.Format ( "i{0}", coordComponents [c] ) )
				)
			) );

			return	code.ToString ();
		}

		static string GetToStringOverrideCodeForDimension ( int dimension ) {
			string typeName = string.Format ( "{0}{1}", Globals.ComponentType, dimension );
			string [] coordComponents = componentSets [0];
			int maxComponents = Math.Min ( dimension, coordComponents.Length );

			StringBuilder code = new StringBuilder ( string.Format (
@"public override string ToString () {{
	return	string.Format ( ""{0}, length: {{0}}"", Length, {1} );
}}
",
				string.Join ( ", ", Enumerable.Range ( 0, maxComponents )
					.Select ( c => coordComponents [c] + ": {" + ( c + 1 ) + "}" )
				),
				string.Join ( ", ", Enumerable.Range ( 0, maxComponents )
					.Select ( c => coordComponents [c] )
				)
			) );

			return	code.ToString ();
		}
		#endregion Overrides

		#region Operators
		static string GetOperatorsCodeForDimension ( int dimension ) {
			StringBuilder code = new StringBuilder ( "#region Operators" );
			code.AppendLine ();

			string [] coordComponents = componentSets [0];
			int maxComponents = Math.Min ( dimension, coordComponents.Length );
			List <string> operators = new List <string> ();
			string typeName = Globals.ComponentType + dimension.ToString ();

			string sumOp = GetBinaryOpCodeForDimension ( dimension, typeName,
				"+", typeName, "a", typeName, "b",
				( dim, ret, arg1, arg2 ) =>
					string.Format ( "return	new {0} ( {1} );", ret,
						string.Join ( ", ", Enumerable.Range ( 0, maxComponents )
							.Select ( c => string.Format ( "{0}.{1} + {2}.{1}", arg1, coordComponents [c], arg2 ) )
						)
					)
			);

			string subOp = GetBinaryOpCodeForDimension ( dimension, typeName,
				"-", typeName, "a", typeName, "b",
				( dim, ret, arg1, arg2 ) =>
					string.Format ( "return	new {0} ( {1} );", ret,
						string.Join ( ", ", Enumerable.Range ( 0, maxComponents )
							.Select ( c => string.Format ( "{0}.{1} - {2}.{1}", arg1, coordComponents [c], arg2 ) )
						)
					)
			);

			string vecScaleOp = GetBinaryOpCodeForDimension ( dimension, typeName,
				"^", typeName, "a", typeName, "b",
				( dim, ret, arg1, arg2 ) =>
					string.Format ( "return	new {0} ( {1} );", ret,
						string.Join ( ", ", Enumerable.Range ( 0, maxComponents )
							.Select ( c => string.Format ( "{0}.{1} * {2}.{1}", arg1, coordComponents [c], arg2 ) )
						)
					)
			);

			string dotOp = GetBinaryOpCodeForDimension ( dimension, Globals.ComponentType,
				"&", typeName, "a", typeName, "b",
				( dim, ret, arg1, arg2 ) =>
					string.Format ( "return	{0};",
						string.Join ( " + ", Enumerable.Range ( 0, maxComponents )
							.Select ( c => string.Format ( "{0}.{1} * {2}.{1}", arg1, coordComponents [c], arg2 ) )
						)
					)
			);

			string crossOp = dimension >= 3 ? GetBinaryOpCodeForDimension ( dimension, Globals.ComponentType + "3",
				"*", typeName, "a", typeName, "b",
				( dim, ret, arg1, arg2 ) =>
					string.Format ( "return	new {0} ( {1} );", ret,
						string.Join ( ", ", Enumerable.Range ( 0, 3 )
							.Select ( c => string.Format ( "{0}.{1} * {2}.{3} - {0}.{3} * {2}.{1}",
								arg1, coordComponents [( c + 1 ) % 3],
								arg2, coordComponents [( c + 2 ) % 3] ) )
						)
					)
			) : null;

			string scaleROp = GetBinaryOpCodeForDimension ( dimension, typeName,
				"*", typeName, "v", Globals.ComponentType, "s",
				( dim, ret, arg1, arg2 ) =>
					string.Format ( "return	new {0} ( {1} );", ret,
						string.Join ( ", ", Enumerable.Range ( 0, maxComponents )
							.Select ( c => string.Format ( "{0}.{1} * {2}", arg1, coordComponents [c], arg2 ) )
						)
					)
			);

			string scaleLOp = GetBinaryOpCodeForDimension ( dimension, typeName,
				"*", Globals.ComponentType, "s", typeName, "v",
				( dim, ret, arg1, arg2 ) =>
					string.Format ( "return	new {0} ( {1} );", ret,
						string.Join ( ", ", Enumerable.Range ( 0, maxComponents )
							.Select ( c => string.Format ( "{2}.{1} * {0}", arg1, coordComponents [c], arg2 ) )
						)
					)
			);

			string divOp = GetBinaryOpCodeForDimension ( dimension, typeName,
				"/", typeName, "v", Globals.ComponentType, "s",
				( dim, ret, arg1, arg2 ) =>
					string.Format ( "return	new {0} ( {1} );", ret,
						string.Join ( ", ", Enumerable.Range ( 0, maxComponents )
							.Select ( c => string.Format ( "{0}.{1} / {2}", arg1, coordComponents [c], arg2 ) )
						)
					)
			);

			string plusOp = GetUnaryOpCodeForDimension ( dimension, typeName,
				"+", typeName, "v",
				( dim, ret, arg1 ) =>
					string.Format ( "return	{0};", arg1 )
			);

			string negateOp = GetUnaryOpCodeForDimension ( dimension, typeName,
				"-", typeName, "v",
				( dim, ret, arg1 ) =>
					string.Format ( "return	new {0} ( {1} );", ret,
						string.Join ( ", ", Enumerable.Range ( 0, maxComponents )
							.Select ( c => string.Format ( "-{0}.{1}", arg1, coordComponents [c] ) )
						)
					)
			);

			string eqOp = GetBinaryOpCodeForDimension ( dimension, "bool",
				"==", typeName, "a", typeName, "b",
				( dim, ret, arg1, arg2 ) =>
					string.Format ( "return	{0};",
						string.Join ( " && ", Enumerable.Range ( 0, maxComponents )
							.Select ( c => string.Format ( "Math.Abs ( {0}.{1} - {2}.{1} ) <= Math3.DIFF_THR", arg1, coordComponents [c], arg2 ) )
						)
					)
			);

			string ineqOp = GetBinaryOpCodeForDimension ( dimension, "bool",
				"!=", typeName, "a", typeName, "b",
				( dim, ret, arg1, arg2 ) =>
					string.Format ( "return	{0};",
						string.Join ( " || ", Enumerable.Range ( 0, maxComponents )
							.Select ( c => string.Format ( "Math.Abs ( {0}.{1} - {2}.{1} ) > Math3.DIFF_THR", arg1, coordComponents [c], arg2 ) )
						)
					)
			);

			string componentToVectorConv = GetConvCodeForDimension ( dimension, typeName,
				Globals.ComponentType, "value",
				( dim, ret, arg1 ) =>
					string.Format ( "return	new {0} ( {1} );", ret, arg1 )
			);

			List <string> downgradeConversions = new List <string> ();

			for ( int i = dimension ; i > 2 ; i-- ) {
				int downDims = i - 1;
				string downTypeName = Globals.ComponentType + downDims.ToString ();
				string downgradeConvOp = GetConvCodeForDimension ( dimension, downTypeName,
					typeName, "v",
					( dim, ret, arg1 ) =>
						string.Format ( "return	new {0} ( {1} );", ret,
							string.Join ( ", ", Enumerable.Range ( 0, downDims )
								.Select ( c => string.Format ( "{0}.{1}", arg1, coordComponents [c] ) )
							)
						)
				);

				downgradeConversions.Add ( downgradeConvOp );
			}

			operators.Add ( sumOp );
			operators.Add ( subOp );
			operators.Add ( vecScaleOp );
			operators.Add ( dotOp );

			if ( crossOp != null )
				operators.Add ( crossOp );

			operators.Add ( scaleROp );
			operators.Add ( scaleLOp );
			operators.Add ( divOp );
			operators.Add ( plusOp );
			operators.Add ( negateOp );
			operators.Add ( eqOp );
			operators.Add ( ineqOp );
			operators.Add ( componentToVectorConv );
			operators.AddRange ( downgradeConversions );

			code.Append ( string.Join ( "\r\n", operators ) );
			code.AppendLine ( "#endregion Operators" );

			return	code.ToString ();
		}

		static string GetBinaryOpCodeForDimension ( int dimension, string retType, string op,
			string arg1Type, string arg1Name, string arg2Type, string arg2Name, Func <int, string, string, string, string> bodyCodeFunc ) {
			string bodyCode = bodyCodeFunc ( dimension, retType, arg1Name, arg2Name );

			return	string.Format ( "public static {0} operator {1} ( {2} {3}, {4} {5} ) {{\r\n\t{6}\r\n}}\r\n",
				retType, op, arg1Type, arg1Name, arg2Type, arg2Name, bodyCode );
		}

		static string GetUnaryOpCodeForDimension ( int dimension, string retType, string op,
			string arg1Type, string arg1Name, Func <int, string, string, string> bodyCodeFunc ) {
			string bodyCode = bodyCodeFunc ( dimension, retType, arg1Name );

			return	string.Format ( "public static {0} operator {1} ( {2} {3} ) {{\r\n\t{4}\r\n}}\r\n",
				retType, op, arg1Type, arg1Name, bodyCode );
		}

		static string GetConvCodeForDimension ( int dimension, string retType,
			string arg1Type, string arg1Name, Func <int, string, string, string> bodyCodeFunc ) {
			string bodyCode = bodyCodeFunc ( dimension, retType, arg1Name );

			return	string.Format ( "public static implicit operator {0} ( {1} {2} ) {{\r\n\t{3}\r\n}}\r\n",
				retType, arg1Type, arg1Name, bodyCode );
		}
		#endregion Operators
		#endregion Vectors

		#region Swizzles
		static void GenSwizzles () {
			for ( int dimension = 2 ; dimension <= 4 ; dimension++ ) {
				string typeName = string.Format ( @"{0}{1}", Globals.ComponentType, dimension );
				string swizzles = GetSwizzlesCodeForDimension ( dimension );

				StringBuilder code = new StringBuilder ();
				code.AppendLine ( "using System;" );
				code.AppendLine ( "using System.ComponentModel;" );
				code.AppendLine ();
				code.AppendLine ( string.Format ( "namespace {0} {{", Globals.Namespace ) );
				code.AppendLine ( string.Format ( "\tpublic partial struct {0}{1} {{", Globals.ComponentType, dimension ) );
				code.AppendLine ( Utils.IndentRight ( swizzles, "\t\t" ) );
				code.AppendLine ( "\t}" );
				code.AppendLine ( "}" );

				File.WriteAllText ( string.Format ( @"Vectors\{0}.swizzles.cs", typeName ), code.ToString () );
			}
		}

		static string GetSwizzlesCodeForDimension ( int dimension ) {
			StringBuilder code = new StringBuilder ( "#region Swizzle Properties" );
			code.AppendLine ();

			foreach ( string [] componentSet in componentSets ) {
				int maxComponents = Math.Min ( dimension, componentSet.Length );
				code.AppendLine ( string.Format ( "// {0} permutations", string.Join ( ", ", componentSet, 0, maxComponents ) ) );

				for ( int componentsNum = 2;
					componentsNum <= maxComponents;
					componentsNum++ ) {
					string [] components = new string [componentsNum];
					Array.Copy ( componentSet, components, componentsNum );
					List <string []> permutations = Utils.GetPermutations ( components );

					foreach ( string [] permutation in permutations ) {
						int distinctCount = permutation.Distinct ().Count ();
						string propDef = string.Format ( "[EditorBrowsable ( EditorBrowsableState.Never )]\r\npublic {0}{1} {2} {{",
							Globals.ComponentType, componentsNum, string.Join ( "", permutation ) );
						string getter;

						if ( distinctCount == 1 )
							getter = string.Format ( "get {{ return	new {0}{1} ( {2} ); }}", Globals.ComponentType, componentsNum, permutation [0] );
						else
							getter = string.Format ( "get {{ return	new {0}{1} ( {2} ); }}", Globals.ComponentType, componentsNum, string.Join ( ", ", permutation ) );

						getter += "\r\n";

						StringBuilder setter = new StringBuilder ();

						if ( distinctCount == permutation.Length ) {
							setter.AppendLine ( "set {" );
								
							for ( int i = 0 ; i < distinctCount ; i++ )
								setter.AppendLine ( string.Format ( "\t{0} = value.{1};", permutation [i], components [i] ) );

							setter.AppendLine ( "}" );
						}

						getter = Utils.IndentRight ( getter, "\t" );
						string setterStr = Utils.IndentRight ( setter.ToString (), "\t" );

						code.Append ( string.Format ( "{0}\r\n{1}{2}}}\r\n\r\n", propDef, getter, setterStr ) );
					}
				}
			}

			code.AppendLine ( "#endregion Swizzle Properties" );

			return	code.ToString ();
		}
		#endregion Swizzles
	}
}
