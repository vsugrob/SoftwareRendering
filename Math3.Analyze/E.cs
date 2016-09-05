using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;
using System.Diagnostics;

namespace Math3.Analyze {
	public enum ExpressionType {
		Undefined, Numeric, Vector
	}

	public enum ExpressionCompareResult {
		NotEqual, Equal, DiffersBySign
	}

	public abstract class E {
		#region Properties
		public virtual ExpressionType InferredType { get { return	ExpressionType.Undefined; } }
		public virtual bool IsValueNode { get { return	false; } }
		public virtual bool IsNegative { get { return	false; } }
		public virtual E SignFree { get { return	this; } }

		public virtual string AsHtml {
			get {
				string html = this.ToString ();
				html = Greek.ReplaceGreekWithHtmlEntities ( html );

				return	html;
			}
		}

		public static readonly EvalSettings DefaultEvalSettings = new EvalSettings ( true );
		#endregion Properties

		#region Constants
		public static readonly E One  = E.NumConst ( 1 );
		public static readonly E Zero = E.NumConst ( 0 );
		#endregion Constants

		#region Methods
		public virtual E Simplify ( EvalSettings evalSettings = null ) { return	this; }
		public virtual E Evaluate ( EvalSettings evalSettings = null, bool isRootNode = true ) { return	this; }
		public virtual ExpressionCompareResult Compare ( E e, EvalSettings evalSettings = null ) {
			evalSettings = evalSettings ?? E.DefaultEvalSettings;

			if ( this.Equals ( e ) )
				return	ExpressionCompareResult.Equal;
			else if ( ( this.IsNegative && !e.IsNegative && this.SignFree.Equals ( e ) ) ||
					  ( !this.IsNegative && e.IsNegative && this.Equals ( e.SignFree ) ) )
				return	ExpressionCompareResult.DiffersBySign;
			else
				return	ExpressionCompareResult.NotEqual;
		}

		protected static E SimplifyIfRoot ( E expr, EvalSettings evalSettings, bool isRootNode ) {
			if ( isRootNode ) {
				if ( evalSettings.MaximumSimplicity ) {
					int i = 0;
					E ePrev;

					do {
						ePrev = expr;
						expr = expr.Simplify ( evalSettings );
						i++;
					} while ( ePrev != expr && i <= EvalSettings.INFINITE_LOOP_CYCLE_NUM );

					if ( i == EvalSettings.INFINITE_LOOP_CYCLE_NUM )
						Debug.Fail ( "Simplify Error", "It seems that simplification will never stop. Aborting." );

					return	expr;
				} else
					return	expr.Simplify ( evalSettings );
			} else
				return	expr;
		}

		public E EvaluateStepByStep ( out string htmlLog, params object [] varValues ) {
		    List <Tuple <string, string>> htmlSteps = new List <Tuple <string, string>> ();

		    EvalFlags [] stepFlags = new [] {
		        EvalFlags.ReduceZeroMultiplier | EvalFlags.ReduceUnitMultiplier | EvalFlags.ReduceUnitDivider,
		        EvalFlags.GroupByCommonFactor | EvalFlags.GroupMultipliersToFractions,
		        EvalFlags.SumFractions,
		        EvalFlags.SimplifyTrigonometricFunctions,
		        EvalFlags.EvalFuncs,
				EvalFlags.SubstituteVariables
		    };
			
		    E e = this;
			htmlSteps.Add ( Tuple.Create ( "Initial expression", e.AsHtml ) );

		    for ( int i = 0 ; i < stepFlags.Length ; i++ ) {
				E prevE = e;
				int c = 0;

		        do {
		            for ( int j = 0 ; j <= i ; j++ ) {
		                prevE = e;
						
		                EvalSettings evalSettings = new EvalSettings ( stepFlags [j], varValues );
						evalSettings.MaximumSimplicity = false;
						e = e.Evaluate ( evalSettings );

						if ( e != prevE )
							htmlSteps.Add ( Tuple.Create ( stepFlags [j].ToString (), e.AsHtml ) );
		            }

					c++;
		        } while ( e != prevE && c <= EvalSettings.INFINITE_LOOP_CYCLE_NUM );

				if ( c == EvalSettings.INFINITE_LOOP_CYCLE_NUM )
					Debug.Fail ( "Simplify Error", "It seems that simplification will never stop. Aborting." );
		    }

			htmlLog = string.Join ( "<br />", htmlSteps.Select ( step => string.Format ( "<h4>{0}:</h4>{1}", step.Item1, step.Item2 ) ) );

		    return	e;
		}
		#endregion Methods

		#region Factory Methods
		public static NumericConstant NumConst ( double value ) {
			return	new NumericConstant ( value );
		}

		public static Variable Vec ( string name ) {
			return	new Variable ( name, VariableType.Vector );
		}

		public static Variable Vec ( string name, double4 value ) {
			return	new Variable ( name, VariableType.Vector, value );
		}

		public static Variable Num ( string name ) {
			return	new Variable ( name, VariableType.Numeric );
		}

		public static Variable Num ( string name, double value ) {
			return	new Variable ( name, VariableType.Numeric, value );
		}

		public static UnaryOp Negate ( E expr ) {
			return	new UnaryOp ( UnaryOpKind.Negate, expr );
		}

		public static MultipleOp MultipleOp ( MultipleOpKind opKind, params E [] operands ) {
			var expr = new MultipleOp ( opKind, operands );

			if ( expr.InferredType == ExpressionType.Undefined )
				throw new InvalidCastException ();

			return	expr;
		}

		public static MultipleOp Sum ( params E [] operands ) {
			return	MultipleOp ( MultipleOpKind.Sum, operands );
		}

		public static MultipleOp Sub ( params E [] operands ) {
			return	MultipleOp ( MultipleOpKind.Sum,
				new E [] { operands [0] }.Concat (
					operands.Skip ( 1 ).Select ( op => E.Negate ( op ) )
				).ToArray () );
		}

		public static MultipleOp Mul ( params E [] operands ) {
			return	MultipleOp ( MultipleOpKind.Mul, operands );
		}

		public static MultipleOp Div ( params E [] operands ) {
			if ( operands.Length != 2 )
				throw new ArgumentException ( "Division operation takes two arguments" );

			return	MultipleOp ( MultipleOpKind.Div, operands );
		}

		public static MultipleOp Mod ( params E [] operands ) {
			return	MultipleOp ( MultipleOpKind.Mod, operands );
		}

		public static MultipleOp Dot ( params E [] operands ) {
			if ( operands.Length != 2 )
				throw new ArgumentException ( "Dot operation takes 2 operands, not " + operands.Length );

			return	MultipleOp ( MultipleOpKind.Dot, operands );
		}

		public static MultipleOp Cross ( params E [] operands ) {
			return	MultipleOp ( MultipleOpKind.Cross, operands );
		}

		public static FuncCall Func ( FuncKind funcKind, params E [] args ) {
			FuncCall fc = new FuncCall ( funcKind, args );

			if ( fc.InferredType == ExpressionType.Undefined )
				throw new InvalidCastException ();

			return	fc;
		}

		public static FuncCall Sin ( E arg ) {
			return	Func ( FuncKind.Sin, arg );
		}

		public static FuncCall Cos ( E arg ) {
			return	Func ( FuncKind.Cos, arg );
		}

		public static FuncCall Abs ( E arg ) {
			return	Func ( FuncKind.Abs, arg );
		}

		public static FuncCall Sqrt ( E arg ) {
			return	Func ( FuncKind.Sqrt, arg );
		}

		public static FuncCall Pow ( E a, E b ) {
			return	Func ( FuncKind.Pow, a, b );
		}

		public PropertyAccess Prop ( string prop ) {
			PropertyAccess propAccess = new PropertyAccess ( this, prop );

			if ( propAccess.InferredType == ExpressionType.Undefined )
				throw new InvalidOperationException ();

			return	propAccess;
		}
		#endregion Factory Methods

		#region Mandatory Overrides
		public override int GetHashCode () {
			return	this.GetType () == typeof ( E ) ? 0 : this.GetHashCode ();
		}

		public override bool Equals ( object obj ) {
			return	this.GetType () == typeof ( E ) ? object.ReferenceEquals ( this, obj ) : this.Equals ( obj );
		}
		#endregion Mandatory Overrides

		#region Operators
		public static bool operator == ( E e1, E e2 ) {
			return	object.ReferenceEquals ( e1, e2 ) || ( !object.ReferenceEquals ( e1, null ) && e1.Equals ( e2 ) );
		}

		public static bool operator != ( E e1, E e2 ) {
			return	!object.ReferenceEquals ( e1, e2 ) && ( !object.ReferenceEquals ( e1, null ) && !e1.Equals ( e2 ) );
		}
		#endregion Operators
	}
}
