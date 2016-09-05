using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace Math3.Analyze {
	public enum UnaryOpKind {
		Negate, UnaryPlus
	}

	public class UnaryOp : E {
		public UnaryOpKind OpKind;
		public E Expression;
		static Dictionary <UnaryOpKind, string> UnaryOpKindString = new Dictionary <UnaryOpKind, string> ();

		public override string AsHtml {
			get {
				string html = @"<table cellspacing=0 style=""display : inline;""><tr>";

				if ( ( ( Expression is MultipleOp ) && ( Expression as MultipleOp ).OpKind == MultipleOpKind.Sum ) ||
					 Expression.IsNegative
				) {
					html += string.Format ( "<td>{0}</td><td>(</td><td>{1}</td><td>)</td>", UnaryOpKindString [OpKind], Expression.AsHtml );
				} else
					html += string.Format ( "<td>{0}</td><td>{1}</td>", UnaryOpKindString [OpKind], Expression.AsHtml );

				html += "</tr></table>";

				return	html;
			}
		}

		static UnaryOp () {
			UnaryOpKindString.Add ( UnaryOpKind.Negate, "-" );
			UnaryOpKindString.Add ( UnaryOpKind.UnaryPlus, "+" );
		}

		public UnaryOp ( UnaryOpKind opKind, E expression ) {
			this.OpKind = opKind;
			this.Expression = expression;
		}

		public override ExpressionType InferredType {
			get { return	Expression.InferredType; }
		}

		public override bool IsNegative { get { return	OpKind == UnaryOpKind.Negate; } }
		public override E SignFree { get { return	Expression; } }

		public override E Simplify ( EvalSettings evalSettings = null ) {
			evalSettings = evalSettings ?? E.DefaultEvalSettings;
			E simplifiedExpression = Expression.Simplify ( evalSettings );

			if ( OpKind == UnaryOpKind.Negate && simplifiedExpression.IsNegative )
				return	simplifiedExpression.SignFree;
			else
				return	new UnaryOp ( OpKind, simplifiedExpression );
		}

		public override E Evaluate ( EvalSettings evalSettings = null, bool isRootNode = true ) {
			evalSettings = evalSettings ?? E.DefaultEvalSettings;
			E evaluatedExpression = Expression.Evaluate ( evalSettings, false );

			if ( evaluatedExpression is Literal && OpKind == UnaryOpKind.Negate ) {
				if ( evaluatedExpression is NumericConstant ) {
					NumericConstant num = evaluatedExpression as NumericConstant;

					return	SimplifyIfRoot ( E.NumConst ( -num.Value ), evalSettings, isRootNode );
				} else if ( evaluatedExpression.IsValueNode ) {
					Variable var = evaluatedExpression as Variable;
					double4 v = ( double4 ) var.Value;

					return	SimplifyIfRoot ( E.Vec ( var.Name, -v ), evalSettings, isRootNode );
				}
			}

			if ( OpKind == UnaryOpKind.Negate )
				return	SimplifyIfRoot ( E.Negate ( evaluatedExpression ), evalSettings, isRootNode );
			else
				return	SimplifyIfRoot ( evaluatedExpression, evalSettings, isRootNode );
		}

		public override string ToString () {
			if ( ( ( Expression is MultipleOp ) && ( Expression as MultipleOp ).OpKind == MultipleOpKind.Sum ) ||
				 Expression.IsNegative
			) {
				return	string.Format ( "{0}( {1} )", UnaryOpKindString [OpKind], Expression );
			} else
				return	string.Format ( "{0}{1}", UnaryOpKindString [OpKind], Expression );
		}

		#region Mandatory Overrides
		public override int GetHashCode () {
			return	OpKind.GetHashCode () ^ Expression.GetHashCode ();
		}

		public override bool Equals ( object obj ) {
			UnaryOp unaryOpObj;

			if ( object.ReferenceEquals ( null, unaryOpObj = obj as UnaryOp ) )
				return	false;

		    return	this.OpKind == unaryOpObj.OpKind && this.Expression == unaryOpObj.Expression;
		}
		#endregion Mandatory Overrides

		#region Operators
		public static bool operator == ( UnaryOp e1, UnaryOp e2 ) {
			return	object.ReferenceEquals ( e1, e2 ) || ( !object.ReferenceEquals ( e1, null ) && e1.Equals ( e2 ) );
		}

		public static bool operator != ( UnaryOp e1, UnaryOp e2 ) {
			return	!object.ReferenceEquals ( e1, e2 ) && ( !object.ReferenceEquals ( e1, null ) && !e1.Equals ( e2 ) );
		}
		#endregion Operators
	}
}
