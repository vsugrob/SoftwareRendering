using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math3.Analyze {
	public enum FuncKind {
		Abs, /*Sign*/
		Sqrt, Pow, /*Exp, Log, Ln*/
		Sin, Cos, /*Tan, Ctg */
	}

	public class FuncCall : E {
		#region Properties
		public FuncKind FuncKind;
		public List <E> Args;
		static Dictionary <FuncKind, string> FuncKindString = new Dictionary <FuncKind, string> ();

		public override string AsHtml {
			get {
				if ( FuncKind == FuncKind.Pow && Args.Count == 2 ) {
					return	string.Format ( @"<table cellpadding=0 cellspacing=0 style=""display : inline;""><tr>
	<td style=""height : 100%;"">
		{0}
	</td>
	<td style=""height : 100%; vertical-align : top;"">
		<sup>{1}</sup>
	</td>
</tr></table>",
						Args [0] is Literal ? Args [0].AsHtml : string.Format ( "</td><td>(</td><td>{0}</td><td>)</td>", Args [0].AsHtml ),
						Args [1] is Literal ? Args [1].AsHtml : string.Format ( "( {0} )", Args [1].AsHtml ) );
				} else if ( FuncKind == FuncKind.Sqrt && Args.Count == 1 ) {
					return	string.Format (
@"<table cellpadding=0 cellspacing=0 style=""display : inline;""><tr>
	<td style=""height : 100%;"">
		<table cellpadding=0 cellspacing=0 style=""height : 100%;"">
		<tr><td style=""border-right : 1px solid black;"">
			<div style=""width 1px; height : 1px; overflow : hidden;"">&nbsp;</div>
		</td></tr>
		<tr><td style=""height : 16px;"">
			<img src=""http://t1web.ru/share/radix.png""/>
		</td></tr>
		</table>
	</td>
	<td style=""border-top : 1px solid black; padding-right : 0.25em; padding-left : 0.25em;"">
		{0}
	</td>
</tr></table>", Args [0].AsHtml );
				} else if ( FuncKind == FuncKind.Abs && Args.Count == 1 ) {
					return	string.Format (
@"<table cellpadding=0 cellspacing=0 style=""display : inline;""><tr>
	<td style=""border-left : 1px solid black; border-right : 1px solid black; padding-right : 0.25em; padding-left : 0.25em;"">
		{0}
	</td>
</tr></table>", Args [0].AsHtml );
				} else {
					if ( Args.Count == 0 )
						return	string.Format ( "{0} ()", FuncKindString [FuncKind] );
					else {
						return	string.Format ( "<table cellpadding=0 cellspacing=0 style=\"display : inline;\"><tr><td>{0}</td><td>(</td>{1}<td>)</td></tr></table>",
							FuncKindString [FuncKind],
							string.Join ( "<td>, </td>", Args.Select ( expr => string.Format ( "<td>{0}</td>", expr.AsHtml ) ) ) );
					}
				}
			}
		}
		#endregion Properties

		static FuncCall () {
			FuncKindString.Add ( FuncKind.Sin , "sin"  );
			FuncKindString.Add ( FuncKind.Cos , "cos"  );
			FuncKindString.Add ( FuncKind.Abs , "abs"  );
			FuncKindString.Add ( FuncKind.Sqrt, "sqrt" );
			FuncKindString.Add ( FuncKind.Pow , "pow"  );
		}

		public FuncCall ( FuncKind funcKind ) {
			this.FuncKind = funcKind;
			this.Args = new List <E> ();
		}

		public FuncCall ( FuncKind funcKind, params E [] args ) {
			this.FuncKind = funcKind;
			this.Args = new List <E> ( args );
		}

		public override ExpressionType InferredType {
			get {
				if ( FuncKind == FuncKind.Sin ||
					 FuncKind == FuncKind.Cos ||
					 FuncKind == FuncKind.Abs ||
					 FuncKind == FuncKind.Sqrt )
				{
					if ( Args.Count == 1 && Args [0].InferredType == ExpressionType.Numeric )
						return	ExpressionType.Numeric;
				} else if ( FuncKind == FuncKind.Pow ) {
					if ( Args.Count == 2 &&
						 Args [0].InferredType == ExpressionType.Numeric &&
						 Args [1].InferredType == ExpressionType.Numeric )
						return	ExpressionType.Numeric;
				}
				
				return	ExpressionType.Undefined;
			}
		}

		public override E Simplify ( EvalSettings evalSettings = null ) {
			evalSettings = evalSettings ?? E.DefaultEvalSettings;

			if ( FuncKind == Analyze.FuncKind.Pow ||
				 FuncKind == Analyze.FuncKind.Sqrt ) {
				return	global::Math3.Analyze.MultipleOp.SimplifyNestedPowers ( this );
			}

			return	base.Simplify ( evalSettings );
		}

		public override E Evaluate ( EvalSettings evalSettings = null, bool isRootNode = true ) {
			evalSettings = evalSettings ?? E.DefaultEvalSettings;
			List <E> evaluatedArgs = new List <E> ( Args.Count );

			foreach ( E arg in Args )
				evaluatedArgs.Add ( arg.Evaluate ( evalSettings, false ) );

			if ( evalSettings.EvalFuncs ) {
				if ( FuncKind == FuncKind.Sin ||
					 FuncKind == FuncKind.Cos ||
					 FuncKind == FuncKind.Abs ||
					 FuncKind == FuncKind.Sqrt )
				{
					E firstArg = evaluatedArgs [0];

					if ( firstArg is NumericConstant ) {
						double inputValue = ( firstArg as NumericConstant ).Value;
						double returnValue = 0;

						if ( FuncKind == Analyze.FuncKind.Sin )
							returnValue = Math.Sin ( inputValue );
						else if ( FuncKind == Analyze.FuncKind.Cos )
							returnValue = Math.Cos ( inputValue );
						else if ( FuncKind == Analyze.FuncKind.Abs )
							returnValue = Math.Abs ( inputValue );
						else if ( FuncKind == Analyze.FuncKind.Sqrt )
							returnValue = Math.Sqrt ( inputValue );

						return	SimplifyIfRoot ( E.NumConst ( returnValue ), evalSettings, isRootNode );
					} else {
						if ( FuncKind == Analyze.FuncKind.Sin && firstArg.IsNegative )
							return	SimplifyIfRoot ( E.Negate ( E.Sin ( firstArg.SignFree ) ), evalSettings, isRootNode );
						else if ( FuncKind == Analyze.FuncKind.Cos && firstArg.IsNegative )
							return	SimplifyIfRoot ( E.Cos ( firstArg.SignFree ), evalSettings, isRootNode );
						else if ( FuncKind == Analyze.FuncKind.Abs && firstArg.IsNegative )
							return	SimplifyIfRoot ( E.Abs ( firstArg.SignFree ), evalSettings, isRootNode );
					}
				} else if ( FuncKind == FuncKind.Pow ) {
					E firstArg = evaluatedArgs [0];
					E secondArg = evaluatedArgs [1];

					if ( firstArg is NumericConstant &&
						 secondArg is NumericConstant ) {
						double a = ( firstArg as NumericConstant ).Value;
						double b = ( secondArg as NumericConstant ).Value;

						return	SimplifyIfRoot ( E.NumConst ( Math.Pow ( a, b ) ), evalSettings, isRootNode );
					} else if ( secondArg is NumericConstant ) {
						double b = ( secondArg as NumericConstant ).Value;

						if ( firstArg.IsNegative ) {
							if ( b % 2 == 0 )
								return	SimplifyIfRoot ( E.Pow ( firstArg.SignFree, secondArg ), evalSettings, isRootNode );
							else
								return	SimplifyIfRoot ( E.Negate ( E.Pow ( firstArg.SignFree, secondArg ) ), evalSettings, isRootNode );
						}
					}
				}
			}

			return	SimplifyIfRoot ( E.Func ( FuncKind, evaluatedArgs.ToArray () ), evalSettings, isRootNode );
		}

		public override string ToString () {
			if ( Args.Count == 0 )
				return	string.Format ( "{0} ()", FuncKindString [FuncKind] );
			else {
				return	string.Format ( "{0} ( {1} )", FuncKindString [FuncKind],
					string.Join ( ", ", Args.Select ( expr => expr.ToString () ) ) );
			}
		}

		#region Mandatory Overrides
		public override int GetHashCode () {
			return	FuncKind.GetHashCode () ^ Args.Aggregate ( 0, ( hash, arg ) => hash ^ arg.GetHashCode () );
		}

		public override bool Equals ( object obj ) {
			FuncCall funcCallObj;

			if ( object.ReferenceEquals ( null, funcCallObj = obj as FuncCall ) )
				return	false;

		    return	this.FuncKind == funcCallObj.FuncKind && this.Args.Count == funcCallObj.Args.Count &&
					Enumerable.Range ( 0, this.Args.Count ).All ( i => this.Args [i].Equals ( funcCallObj.Args [i] ) );
		}
		#endregion Mandatory Overrides

		#region Operators
		public static bool operator == ( FuncCall e1, FuncCall e2 ) {
			return	object.ReferenceEquals ( e1, e2 ) || ( !object.ReferenceEquals ( e1, null ) && e1.Equals ( e2 ) );
		}

		public static bool operator != ( FuncCall e1, FuncCall e2 ) {
			return	!object.ReferenceEquals ( e1, e2 ) && ( !object.ReferenceEquals ( e1, null ) && !e1.Equals ( e2 ) );
		}
		#endregion Operators
	}
}
