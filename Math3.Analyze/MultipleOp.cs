using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace Math3.Analyze {
	public enum MultipleOpKind {
		Sum, Mul, Div, Mod,
		Dot, Cross
	}

	public enum Commutativity {
		Commutative, NonCommutative
	}

	public enum OperandQuantity {
		Zero, Unary, Binary, Multiple
	}

	public class MultipleOp : E {
		public MultipleOpKind OpKind;
		public List <E> Operands;
		static readonly Dictionary <MultipleOpKind, string> MultipleOpKindString = new Dictionary <MultipleOpKind, string> ();
		static readonly Dictionary <MultipleOpKind, string> MultipleOpKindHtml = new Dictionary <MultipleOpKind, string> ();
		static readonly Dictionary <MultipleOpKind, int> MultipleOpKindPrecedence = new Dictionary <MultipleOpKind, int> ();
		static readonly Dictionary <MultipleOpKind, Commutativity> MultipleOpKindCommutativity = new Dictionary <MultipleOpKind, Commutativity> ();
		static readonly Dictionary <MultipleOpKind, OperandQuantity> MultipleOpKindQuantity = new Dictionary <MultipleOpKind, OperandQuantity> ();

		public override string AsHtml {
			get {
				if ( OpKind == MultipleOpKind.Sum ) {
					return	string.Format ( "<table cellspacing=0 style='display : inline;'><tr>{0}</tr></table>",
						Operands.Skip ( 1 ).Aggregate ( "<td>" + Operands.First ().AsHtml + "</td>",
							( str, expr ) => {
								bool embrace = false;

								if ( expr is MultipleOp ) {
									MultipleOp exprMulOp = expr as MultipleOp;

									if ( MultipleOpKindPrecedence [exprMulOp.OpKind] > MultipleOpKindPrecedence [OpKind] )
										embrace = true;
								}

								string exprStr = expr.IsNegative ? expr.SignFree.AsHtml : expr.AsHtml;

								return	string.Format ( "{0}<td>{1}</td><td>{2}</td>", str,
									expr.IsNegative ? "-" : "+",
									embrace ? string.Format ( "(</td><td>{0}</td><td>)</td>", exprStr ) : exprStr );
							}
						)
					);
				} else if ( OpKind == MultipleOpKind.Div ) {
					return	string.Format (
@"<table cellspacing=0 style=""display : inline;"">
<tr><td style=""border-bottom : 1px solid black; text-align : center;"">{0}</td></tr>
<tr><td style=""text-align : center;"">{1}</td></tr>
</table>", Operands [0].AsHtml, Operands [1].AsHtml );
				} else {
					return	string.Format ( "<table cellspacing=0 style='display : inline;'><tr><td>{0}</td></tr></table>",
						string.Join ( string.Format ( "</td><td>{0}</td><td>", MultipleOpKindHtml [OpKind] ),
							Operands.Select ( expr => {
								if ( expr is MultipleOp ) {
									MultipleOp exprMulOp = expr as MultipleOp;

									if ( MultipleOpKindPrecedence [exprMulOp.OpKind] > MultipleOpKindPrecedence [OpKind] )
										return	string.Format ( "(</td><td>{0}</td><td>)</td>", exprMulOp.AsHtml );
								}
							
								return	expr.AsHtml;
							} )
						)
					);
				}
			}
		}

		static MultipleOp () {
			MultipleOpKindString.Add ( MultipleOpKind.Sum, "+" );
			MultipleOpKindString.Add ( MultipleOpKind.Mul, "*" );
			MultipleOpKindString.Add ( MultipleOpKind.Div, "/" );
			MultipleOpKindString.Add ( MultipleOpKind.Mod, "%" );
			MultipleOpKindString.Add ( MultipleOpKind.Dot, "&" );
			MultipleOpKindString.Add ( MultipleOpKind.Cross, "x" );

			MultipleOpKindHtml.Add ( MultipleOpKind.Sum, "+" );
			MultipleOpKindHtml.Add ( MultipleOpKind.Mul, "&sdot;" );
			MultipleOpKindHtml.Add ( MultipleOpKind.Div, "/" );
			MultipleOpKindHtml.Add ( MultipleOpKind.Mod, "%" );
			MultipleOpKindHtml.Add ( MultipleOpKind.Dot, "&bull;" );
			MultipleOpKindHtml.Add ( MultipleOpKind.Cross, "&times;" );

			MultipleOpKindPrecedence.Add ( MultipleOpKind.Sum, 5 );
			MultipleOpKindPrecedence.Add ( MultipleOpKind.Mul, 0 );
			MultipleOpKindPrecedence.Add ( MultipleOpKind.Div, 1 );
			MultipleOpKindPrecedence.Add ( MultipleOpKind.Mod, 4 );
			MultipleOpKindPrecedence.Add ( MultipleOpKind.Dot, 2 );
			MultipleOpKindPrecedence.Add ( MultipleOpKind.Cross, 3 );

			MultipleOpKindCommutativity.Add ( MultipleOpKind.Sum, Commutativity.Commutative );
			MultipleOpKindCommutativity.Add ( MultipleOpKind.Mul, Commutativity.Commutative );
			MultipleOpKindCommutativity.Add ( MultipleOpKind.Div, Commutativity.NonCommutative );
			MultipleOpKindCommutativity.Add ( MultipleOpKind.Mod, Commutativity.NonCommutative );
			MultipleOpKindCommutativity.Add ( MultipleOpKind.Dot, Commutativity.Commutative );
			MultipleOpKindCommutativity.Add ( MultipleOpKind.Cross, Commutativity.NonCommutative );

			MultipleOpKindQuantity.Add ( MultipleOpKind.Sum, OperandQuantity.Multiple );
			MultipleOpKindQuantity.Add ( MultipleOpKind.Mul, OperandQuantity.Multiple );
			MultipleOpKindQuantity.Add ( MultipleOpKind.Div, OperandQuantity.Binary );
			MultipleOpKindQuantity.Add ( MultipleOpKind.Mod, OperandQuantity.Binary );
			MultipleOpKindQuantity.Add ( MultipleOpKind.Dot, OperandQuantity.Binary );
			MultipleOpKindQuantity.Add ( MultipleOpKind.Cross, OperandQuantity.Multiple );
		}

		public MultipleOp ( MultipleOpKind opKind ) {
			this.OpKind = opKind;
			this.Operands = new List <E> ();
		}

		public MultipleOp ( MultipleOpKind opKind, params E [] operands ) {
			this.OpKind = opKind;
			this.Operands = new List <E> ( operands );
		}

		public override bool IsNegative {
			get {
				// Note: MultipleOpKind.Mod was not revised
				if ( OpKind != MultipleOpKind.Sum ) {
					return	Operands.Count ( operand => operand.IsNegative ) % 2 == 1;
				} else
					return	false;
			}
		}

		public override E SignFree {
			get {
				// Note: MultipleOpKind.Mod was not revised
				if ( OpKind != MultipleOpKind.Sum )
					return	E.MultipleOp ( OpKind, Operands.Select ( op => op.SignFree ).ToArray () );
				else
					return	this;
			}
		}

		public override E Simplify ( EvalSettings evalSettings = null ) {
			evalSettings = evalSettings ?? E.DefaultEvalSettings;
			MultipleOp simplifiedExpr = this;

			simplifiedExpr = FlattenMultipleOp ( simplifiedExpr );

			simplifiedExpr = E.MultipleOp ( OpKind, simplifiedExpr.Operands.Select ( op => op.Simplify ( evalSettings ) ).ToArray () );
			bool isNegative = false;

			// Note: MultipleOpKind.Mod was not revised
			if ( OpKind != MultipleOpKind.Sum ) {
				// Purpose: extract sign, free expression from sign
				int negationsCount = simplifiedExpr.Operands.Count ( operand => operand.IsNegative );

				if ( negationsCount > 0 ) {
					simplifiedExpr = simplifiedExpr.SignFree as MultipleOp;

					if ( negationsCount % 2 == 1 )
						isNegative = true;
				}
			}
			
			if ( evalSettings.GroupByCommonFactor ) {
				if ( OpKind == MultipleOpKind.Sum ) {
					// Purpose: extract coefficients, group similiar expressions with summarized coefficients
					List <E> examinedOperands = new List <E> ( simplifiedExpr.Operands );
					List <E> resultOperands = new List <E> ();

					while ( examinedOperands.Count > 0 ) {
						E opA = examinedOperands [0];
						double k = GetNumericCoefficient ( opA, out opA );
						examinedOperands.RemoveAt ( 0 );

						for ( int i = 0 ; i < examinedOperands.Count ; i++ ) {
							E opB = examinedOperands [i];
							double kDelta = GetNumericCoefficient ( opB, out opB );
							ExpressionCompareResult cmpRes = opA.Compare ( opB, evalSettings );

							if ( cmpRes == ExpressionCompareResult.NotEqual )
								kDelta = 0;
							else if ( cmpRes == ExpressionCompareResult.DiffersBySign )
								kDelta *= -1;

							if ( kDelta != 0 ) {
								k += kDelta;
								examinedOperands.RemoveAt ( i );
								i--;
							}
						}

						if ( k == 0 )
							continue;
						else if ( k > 1 || k < -1 )
							resultOperands.Add ( E.Mul ( E.NumConst ( k ), opA ) );
						else if ( k == -1 )
							resultOperands.Add ( E.Negate ( opA ) );
						else //if ( sum == 1 )
							resultOperands.Add ( opA );
					}

					if ( resultOperands.Count > 1 )
						simplifiedExpr = E.MultipleOp ( simplifiedExpr.OpKind, resultOperands.ToArray () );
					else if ( resultOperands.Count == 1 )
						return	resultOperands [0];
					else
						return	E.Zero;
				}
			}

			if ( evalSettings.SimplifyTrigonometricFunctions ) {
				if ( OpKind == MultipleOpKind.Sum ) {
					// Purpose: pow ( sin ( a ), 2 ) + pos ( cos ( a ), 2 ) => 1
					List <E> examinedOperands = new List <E> ( simplifiedExpr.Operands );
					List <E> resultOperands = new List <E> ();

					while ( examinedOperands.Count > 0 ) {
						E opA = examinedOperands [0];
						double k1 = GetNumericCoefficient ( opA, out opA );
						examinedOperands.RemoveAt ( 0 );

						bool opAIsNotTriFunc = true;
						bool found = false;
						FuncCall triCallA = null;

						if ( opA is FuncCall && ( opA as FuncCall ).FuncKind == FuncKind.Pow ) {
							FuncCall powCallA = opA as FuncCall;
							triCallA = powCallA.Args [0] as FuncCall;

							if ( triCallA != null &&
								 ( triCallA.FuncKind == FuncKind.Sin ||
								   triCallA.FuncKind == FuncKind.Cos ) ) {
								E powA = powCallA.Args [1];

								if ( powA is NumericConstant && ( powA as NumericConstant ).Value == 2 )
									opAIsNotTriFunc = false;
							}
						}
					
						if ( !opAIsNotTriFunc ) {
							for ( int i = 0 ; i < examinedOperands.Count ; i++ ) {
								E opB = examinedOperands [i];
								double k2 = GetNumericCoefficient ( opB, out opB );

								if ( opB is FuncCall && ( opB as FuncCall ).FuncKind == FuncKind.Pow &&
									 Math.Sign ( k1 ) == Math.Sign ( k2 ) && k1 == k2 ) {
									FuncCall powCallB = opB as FuncCall;
									FuncCall triCallB = powCallB.Args [0] as FuncCall;

									if ( triCallB != null &&
										 ( ( triCallA.FuncKind == FuncKind.Cos && triCallB.FuncKind == FuncKind.Sin ) ||
										   ( triCallA.FuncKind == FuncKind.Sin && triCallB.FuncKind == FuncKind.Cos ) ) ) {
										E powB = powCallB.Args [1];

										if ( powB is NumericConstant && ( powB as NumericConstant ).Value == 2 ) {
											examinedOperands.RemoveAt ( i );
											found = true;

											break;
										}
									}
								}
							}
						}

						if ( found )
							resultOperands.Add ( E.NumConst ( k1 ) );
						else if ( k1 == 0 )
							continue;
						else if ( k1 == 1 )
							resultOperands.Add ( opA );
						else if ( k1 == -1 )
							resultOperands.Add ( E.Negate ( opA ) );
						else //if ( k1 != 1 )
							resultOperands.Add ( E.Mul ( E.NumConst ( k1 ), opA ) );
					}

					if ( resultOperands.Count != simplifiedExpr.Operands.Count ) {
						if ( resultOperands.Count > 1 )
							simplifiedExpr = E.Sum ( resultOperands.ToArray () );
						else if ( resultOperands.Count == 1 )
							return	resultOperands [0];
						else
							return	E.Zero;
					}
				}
			}

			if ( evalSettings.GroupMultipliersToFractions ) {
				if ( OpKind == MultipleOpKind.Mul ||
					 OpKind == MultipleOpKind.Div ) {
					// Purpose: partition multipliers to numerator and denumerator
					List <E> numeratorOperands = new List <E> ();
					List <E> denomenatorOperands = new List <E> ();

					GetNumeratorAndDenomenatorOperands ( simplifiedExpr, out numeratorOperands, out denomenatorOperands );

					List <E> examinedNumeratorOperands = new List <E> ( numeratorOperands );
					List <E> examinedDenomenatorOperands = new List <E> ( denomenatorOperands );
					numeratorOperands.Clear ();
					denomenatorOperands.Clear ();

					while ( examinedNumeratorOperands.Count > 0 ) {
						E opA = examinedNumeratorOperands [0];
						bool isBoxedExprANegated;
						bool isBoxedExprANeedAbs;
						double pow = GetNumericPower ( opA, out opA, out isBoxedExprANegated, out isBoxedExprANeedAbs );

						if ( isBoxedExprANeedAbs )
							opA = E.Abs ( opA );

						examinedNumeratorOperands.RemoveAt ( 0 );

						for ( int i = 0 ; i < examinedNumeratorOperands.Count ; i++ ) {
							E opB = examinedNumeratorOperands [i];
							bool isBoxedExprBNegated;
							bool isBoxedExprBNeedAbs;
							double powB = GetNumericPower ( opB, out opB, out isBoxedExprBNegated, out isBoxedExprBNeedAbs );

							if ( isBoxedExprBNeedAbs )
								opB = E.Abs ( opB );

							ExpressionCompareResult cmpRes = opA.Compare ( opB, evalSettings );

							if ( cmpRes == ExpressionCompareResult.Equal ) {
								pow += powB;
								examinedNumeratorOperands.RemoveAt ( i );
								i--;
								isBoxedExprANegated = isBoxedExprANegated != isBoxedExprBNegated;
							}
						}

						for ( int i = 0 ; i < examinedDenomenatorOperands.Count ; i++ ) {
							E opB = examinedDenomenatorOperands [i];
							bool isBoxedExprBNegated;
							bool isBoxedExprBNeedAbs;
							double powB = GetNumericPower ( opB, out opB, out isBoxedExprBNegated, out isBoxedExprBNeedAbs );

							if ( isBoxedExprBNeedAbs )
								opB = E.Abs ( opB );

							ExpressionCompareResult cmpRes = opA.Compare ( opB, evalSettings );

							if ( cmpRes == ExpressionCompareResult.Equal ) {
								pow -= powB;
								examinedDenomenatorOperands.RemoveAt ( i );
								i--;
								isBoxedExprANegated = isBoxedExprANegated != isBoxedExprBNegated;
							}
						}

						if ( pow == 0 )
							continue;
						else if ( pow == 1 )
							numeratorOperands.Add ( opA );
						else if ( pow == -1 )
							denomenatorOperands.Add ( opA );
						else if ( pow == 0.5 )
							numeratorOperands.Add ( E.Sqrt ( opA ) );
						else if ( pow > 0 )
							numeratorOperands.Add ( E.Pow ( opA, E.NumConst ( pow ) ) );
						else if ( pow == -0.5 )
							denomenatorOperands.Add ( E.Sqrt ( opA ) );
						else if ( pow < 0 )
							denomenatorOperands.Add ( E.Pow ( opA, E.NumConst ( -pow ) ) );
					}

					while ( examinedDenomenatorOperands.Count > 0 ) {
						E opA = examinedDenomenatorOperands [0];
						bool isBoxedExprANegated;
						bool isBoxedExprANeedAbs;
						double pow = GetNumericPower ( opA, out opA, out isBoxedExprANegated, out isBoxedExprANeedAbs );

						if ( isBoxedExprANeedAbs )
							opA = E.Abs ( opA );

						examinedDenomenatorOperands.RemoveAt ( 0 );

						for ( int i = 0 ; i < examinedDenomenatorOperands.Count ; i++ ) {
							E opB = examinedDenomenatorOperands [i];
							bool isBoxedExprBNegated;
							bool isBoxedExprBNeedAbs;
							double powB = GetNumericPower ( opB, out opB, out isBoxedExprBNegated, out isBoxedExprBNeedAbs );

							if ( isBoxedExprBNeedAbs )
								opB = E.Abs ( opB );

							ExpressionCompareResult cmpRes = opA.Compare ( opB, evalSettings );

							if ( cmpRes == ExpressionCompareResult.Equal ) {
								pow += powB;
								examinedDenomenatorOperands.RemoveAt ( i );
								i--;
								isBoxedExprANegated = isBoxedExprANegated != isBoxedExprBNegated;
							}
						}

						if ( pow == 0 )
							continue;
						else if ( pow == 1 )
							denomenatorOperands.Add ( opA );
						else if ( pow == -1 )
							numeratorOperands.Add ( opA );
						else if ( pow == 0.5 )
							denomenatorOperands.Add ( E.Sqrt ( opA ) );
						else if ( pow > 0 )
							denomenatorOperands.Add ( E.Pow ( opA, E.NumConst ( pow ) ) );
						else if ( pow == -0.5 )
							numeratorOperands.Add ( E.Sqrt ( opA ) );
						else if ( pow < 0 )
							numeratorOperands.Add ( E.Pow ( opA, E.NumConst ( -pow ) ) );
					}

					if ( denomenatorOperands.Count > 0 ) {
						E numeratorExpr;
						E denomenatorExpr;

						if ( numeratorOperands.Count > 1 )
							numeratorExpr = E.Mul ( numeratorOperands.ToArray () );
						else
							numeratorExpr = numeratorOperands [0];

						if ( denomenatorOperands.Count > 1 )
							denomenatorExpr = E.Mul ( denomenatorOperands.ToArray () );
						else
							denomenatorExpr = denomenatorOperands [0];
					
						simplifiedExpr = E.Div ( numeratorExpr, denomenatorExpr );
					} else if ( numeratorOperands.Count > 1 )
						simplifiedExpr = E.Mul ( numeratorOperands.ToArray () );
					else if ( numeratorOperands.Count == 1 )
						return	isNegative ? E.Negate ( numeratorOperands [0] ) : numeratorOperands [0];
					else if ( numeratorOperands.Count == 0 )
						return	isNegative ? E.NumConst ( -1 ) : E.One;
				}
			}

			if ( evalSettings.SumFractions ) {
				if ( OpKind == MultipleOpKind.Sum ) {
					// Purpose: union fractions with the same denomenator
					List <E> examinedOperands = new List <E> ( simplifiedExpr.Operands );
					List <E> resultOperands = new List <E> ();

					while ( examinedOperands.Count > 0 ) {
						E opA = examinedOperands [0];
						examinedOperands.RemoveAt ( 0 );

						MultipleOp divOpA = opA as MultipleOp;
						E denomA = null;
						List <E> numeratorOps = new List <E> ();

						if ( divOpA != null && divOpA.OpKind == MultipleOpKind.Div ) {
							denomA = divOpA.Operands [1];
							bool numerAAdded = false;

							for ( int i = 0 ; i < examinedOperands.Count ; i++ ) {
								E opB = examinedOperands [i];
								MultipleOp divOpB = opB as MultipleOp;

								if ( divOpB != null && divOpB.OpKind == MultipleOpKind.Div ) {
									E denomB = divOpB.Operands [1];

									ExpressionCompareResult cmpRes = denomA.Compare ( denomB, evalSettings );

									if ( cmpRes == ExpressionCompareResult.Equal ) {
										if ( !numerAAdded ) {
											numeratorOps.Add ( divOpA.Operands [0] );
											numerAAdded = true;
										}

										numeratorOps.Add ( divOpB.Operands [0] );
										examinedOperands.RemoveAt ( i );
										i--;
									}
								}
							}
						}

						if ( numeratorOps.Count > 0 ) {
							MultipleOp numeratorSum = E.Sum ( numeratorOps.ToArray () );
							numeratorSum = FlattenMultipleOp ( numeratorSum );
							resultOperands.Add ( E.Div ( numeratorSum, denomA ) );
						} else
							resultOperands.Add ( opA );
					}

					if ( resultOperands.Count > 1 )
						simplifiedExpr = E.MultipleOp ( simplifiedExpr.OpKind, resultOperands.ToArray () );
					else if ( resultOperands.Count == 1 )
						return	resultOperands [0];
					else
						return	E.Zero;
				}
			}
			
			if ( isNegative )
				return	E.Negate ( simplifiedExpr );
			else
				return	simplifiedExpr;
		}

		public static MultipleOp FlattenMultipleOp ( MultipleOp expr ) {
			expr = E.MultipleOp ( expr.OpKind, expr.Operands.ToArray () );

			if ( MultipleOpKindQuantity [expr.OpKind] == OperandQuantity.Multiple ) {
				for ( int i = 0 ; i < expr.Operands.Count ; i++ ) {
					E op = expr.Operands [i];
					bool opIsNegated = false;

					if ( op.IsNegative ) {
						op = op.SignFree;
						opIsNegated = true;
					}

					MultipleOp mulOp = op as MultipleOp;

					if ( mulOp != null && expr.OpKind == mulOp.OpKind ) {
						expr.Operands.RemoveAt ( i );
						mulOp = FlattenMultipleOp ( mulOp );

						if ( opIsNegated )
							expr.Operands.InsertRange ( i, mulOp.Operands.Select ( subOp => E.Negate ( subOp ) ) );
						else
							expr.Operands.InsertRange ( i, mulOp.Operands );

						i = i + mulOp.Operands.Count - 1;
					}
				}
			}

			return	expr;
		}

		public static void GetNumeratorAndDenomenatorOperands ( MultipleOp mulOpExpr,
			out List <E> numeratorOperands, out List <E> denomenatorOperands )
		{
			numeratorOperands = new List <E> ();
			denomenatorOperands = new List <E> ();

			if ( mulOpExpr.OpKind == MultipleOpKind.Mul ) {
				foreach ( E operand in mulOpExpr.Operands ) {
					if ( operand is MultipleOp ) {
						MultipleOp mulOpOperand = operand as MultipleOp;

						if ( mulOpOperand.OpKind == MultipleOpKind.Div ) {
							numeratorOperands.Add ( mulOpOperand.Operands [0] );
							denomenatorOperands.AddRange ( mulOpOperand.Operands.GetRange ( 1, mulOpOperand.Operands.Count - 1 ) );
						} else
							numeratorOperands.Add ( mulOpOperand );
					} else
						numeratorOperands.Add ( operand );
				}
			} else if ( mulOpExpr.OpKind == MultipleOpKind.Div ) {
				MultipleOp divNumeratorMulOp = mulOpExpr.Operands [0] as MultipleOp;
				MultipleOp divDenomenatorMulOp = mulOpExpr.Operands [1] as MultipleOp;

				if ( divNumeratorMulOp != null &&
					 ( divNumeratorMulOp.OpKind == MultipleOpKind.Mul || divNumeratorMulOp.OpKind == MultipleOpKind.Div ) )
					GetNumeratorAndDenomenatorOperands ( divNumeratorMulOp, out numeratorOperands, out denomenatorOperands );
				else
					numeratorOperands.Add ( mulOpExpr.Operands [0] );

				if ( divDenomenatorMulOp != null &&
					 ( divDenomenatorMulOp.OpKind == MultipleOpKind.Mul || divDenomenatorMulOp.OpKind == MultipleOpKind.Div ) ) {
					List <E> subNumeratorOperands = new List <E> ();
					List <E> subDenomenatorOperands = new List <E> ();

					GetNumeratorAndDenomenatorOperands ( divDenomenatorMulOp, out subNumeratorOperands, out subDenomenatorOperands );

					numeratorOperands.AddRange ( subDenomenatorOperands );
					denomenatorOperands.AddRange ( subNumeratorOperands );
				} else
					denomenatorOperands.Add ( mulOpExpr.Operands [1] );
			} else
				numeratorOperands.Add ( mulOpExpr );
		}

		public static double GetNumericCoefficient ( E inExpr, out E outExpr ) {
			bool isNegated = false;
			double k = 1;

			if ( inExpr.IsNegative ) {
				isNegated = true;
				inExpr = inExpr.SignFree;
			}

			if ( inExpr is MultipleOp && ( inExpr as MultipleOp ).OpKind == MultipleOpKind.Mul ) {
				MultipleOp inMulOp = inExpr as MultipleOp;
				List <E> numOperands = new List <E> ();
				List <E> exprOperands = new List <E> ();

				foreach ( E operand in inMulOp.Operands ) {
					if ( operand is NumericConstant )
						numOperands.Add ( operand );
					else
						exprOperands.Add ( operand );
				}

				if ( exprOperands.Count == 0 ) {
					outExpr = inExpr;
					k = 1;
				} else {
					if ( exprOperands.Count > 1 )
						outExpr = E.Mul ( exprOperands.ToArray () );
					else
						outExpr = exprOperands [0];

					if ( numOperands.Count > 0 ) {
						MultipleOp numMul = E.Mul ( numOperands.ToArray () );
						E res = numMul.Evaluate ( new EvalSettings () );
						NumericConstant numConst = res as NumericConstant;
						k = numConst.Value;
					} else
						k = 1;
				}
			} else {
				outExpr = inExpr;
				k = 1;
			}

			return	isNegated ? -k : k;
		}

		public static E SimplifyNestedPowers ( E e ) {
			bool isNegated;
			bool needAbs;
			double pow = GetNumericPower ( e, out e, out isNegated, out needAbs );

			if ( needAbs )
				e = E.Abs ( e );

			if ( pow == 0 )
				e = E.One;
			else if ( pow == 0.5 )
				e = E.Sqrt ( e );
			else if ( pow != 1 )
				e = E.Pow ( e, E.NumConst ( pow ) );

			if ( isNegated )
				e = E.Negate ( e );

			return	e;
		}

		public static double GetNumericPower ( E inExpr, out E outExpr, out bool isNegated, out bool needAbs ) {
			outExpr = null;
			isNegated = false;
			needAbs = false;
			double pow = 1;

			if ( inExpr.IsNegative ) {
				isNegated = true;
				inExpr = inExpr.SignFree;
			}

			FuncCall inFuncCall = inExpr as FuncCall;
			
			if ( inFuncCall != null ) {
				bool powIsExtracted = false;

				if ( inFuncCall.FuncKind == FuncKind.Pow ) {
					E powExpr = inFuncCall.Args [1];
					NumericConstant powNum;
					bool isPowNumNegated = false;

					if ( powExpr.IsNegative ) {
						isPowNumNegated = true;
						powExpr = powExpr.SignFree;
					}

					if ( powExpr is NumericConstant ) {
						powNum = powExpr as NumericConstant;
						pow = powNum.Value;

						if ( isPowNumNegated )
							pow *= -1;

						outExpr = inFuncCall.Args [0];
						powIsExtracted = true;
					}
				} else if ( inFuncCall.FuncKind == FuncKind.Sqrt ) {
					pow = 0.5;
					powIsExtracted = true;
					outExpr = inFuncCall.Args [0];
				}

				if ( powIsExtracted ) {
					bool innerIsNegated;
					bool innerNeedAbs;
					double innerPow = GetNumericPower ( outExpr, out outExpr, out innerIsNegated, out innerNeedAbs );
					needAbs = innerNeedAbs;
					double newPow = pow * innerPow;
					bool innerIsEven = innerPow % 2 == 0 || ( ( int ) innerPow != innerPow );
					bool isEven = newPow % 2 == 0 || ( ( int ) newPow != newPow );

					if ( innerIsEven && isEven ) {
						// no need to abs or extract sign
					} else if ( innerIsEven && !isEven ) {
						needAbs = true;
					} else if ( !innerIsEven && !isEven ) {
						isNegated = isNegated != innerIsNegated;
					} else if ( !innerIsEven && isEven ) {
						needAbs = false;
					}

					return	pow * innerPow;
				}
			}

			if ( outExpr == null )
				outExpr = inExpr;

			return	1;
		}

		public override ExpressionType InferredType {
			get {
				var types = Operands.Select ( expr => expr.InferredType );
				ExpressionType firstType = types.First ();

				if ( OpKind == MultipleOpKind.Sum ) {
					return	types.All ( et => et == firstType ) ? firstType : ExpressionType.Undefined;
				} else if ( OpKind == MultipleOpKind.Mod ) {
					return	types.All ( et => et == ExpressionType.Numeric ) ? ExpressionType.Numeric : ExpressionType.Undefined;
				} else if ( OpKind == MultipleOpKind.Mul ) {
					if ( types.All ( et => et == ExpressionType.Numeric ) )
						return	ExpressionType.Numeric;
					else if ( types.All ( et => et == ExpressionType.Numeric || et == ExpressionType.Vector ) &&
						types.Count ( et => et == ExpressionType.Vector ) == 1 )
						return	ExpressionType.Vector;
					else
						return	ExpressionType.Undefined;
				} else if ( OpKind == MultipleOpKind.Div ) {
					var restTypes = types.Skip ( 1 );

					if ( firstType == ExpressionType.Numeric )
						return	restTypes.All ( et => et == ExpressionType.Numeric ) ? ExpressionType.Numeric : ExpressionType.Undefined;
					else if ( firstType == ExpressionType.Vector )
						return	restTypes.All ( et => et == ExpressionType.Numeric ) ? ExpressionType.Vector : ExpressionType.Undefined;
					else
						return	ExpressionType.Undefined;
				} else if ( OpKind == MultipleOpKind.Dot ) {
					return	( types.Count ( et => et == ExpressionType.Vector ) == 2 &&
						types.All ( et => et == ExpressionType.Vector ) ) ? ExpressionType.Numeric : ExpressionType.Undefined;
				} else if ( OpKind == MultipleOpKind.Cross ) {
					return	types.All ( et => et == ExpressionType.Vector ) ? ExpressionType.Vector : ExpressionType.Undefined;
				} else
					return	ExpressionType.Undefined;
			}
		}

		public override E Evaluate ( EvalSettings evalSettings = null, bool isRootNode = true ) {
			evalSettings = evalSettings ?? E.DefaultEvalSettings;
			List <E> evaluatedOperands = new List <E> ( Operands.Count );

			foreach ( E operand in Operands )
				evaluatedOperands.Add ( operand.Evaluate ( evalSettings, false ) );

			List <E> valueNodes = new List <E> ();
			List <E> exprNodes = new List <E> ();

			foreach ( E evaluatedOperand in evaluatedOperands ) {
				if ( evaluatedOperand.IsValueNode )
					valueNodes.Add ( evaluatedOperand );
				else
					exprNodes.Add ( evaluatedOperand );
			}

			if ( valueNodes.Count > 0 ) {
				E resultNode = null;

				if ( OpKind == MultipleOpKind.Sum ) {
					if ( evaluatedOperands [0].InferredType == ExpressionType.Numeric ) {
						double res = valueNodes.Aggregate ( 0.0, ( s, node ) => s + ( node as NumericConstant ).Value );

						if ( res == 0 && exprNodes.Count > 0 )
							resultNode = null;
						else
							resultNode = E.NumConst ( res );
					} else if ( evaluatedOperands [0].InferredType == ExpressionType.Vector ) {
						double4 res = valueNodes.Aggregate ( double4.Zero, ( s, node ) => s + ( double4 ) ( node as Variable ).Value );
						res.w = 1;

						resultNode = E.Vec ( "v", res );
					}
				} else if ( OpKind == MultipleOpKind.Mod ) {
					double res = valueNodes.Skip ( 1 ).Aggregate ( ( valueNodes.First () as NumericConstant ).Value,
						( s, node ) => s % ( node as NumericConstant ).Value );

					resultNode = E.NumConst ( res );
				} else if ( OpKind == MultipleOpKind.Mul ) {
					Variable v = null;
					List <E> numOperands = new List <E> ();

					foreach ( E expr in valueNodes ) {
						if ( expr.InferredType == ExpressionType.Vector )
							v = expr as Variable;
						else
							numOperands.Add ( expr );
					}

					if ( v != null ) {
						double4 res = numOperands.Aggregate ( ( double4 ) v.Value,
							( s, node ) => s * ( node as NumericConstant ).Value );
						res.w = 1;

						resultNode = E.Vec ( "v", res );
					} else {
						double res = valueNodes.Aggregate ( 1.0,
							( s, node ) => s * ( node as NumericConstant ).Value );

						if ( res == 0 && evalSettings.ReduceZeroMultiplier ) {
							resultNode = E.NumConst ( 0 );
							exprNodes.Clear ();
						} else if ( res == 1 && evalSettings.ReduceUnitMultiplier ) {
							if ( exprNodes.Count > 0 )
								resultNode = null;
							else
								resultNode = E.NumConst ( 1 );
						} else
							resultNode = E.NumConst ( res );
					}
				} else if ( OpKind == MultipleOpKind.Div ) {
					if ( exprNodes.Count == 0 ) {
						if ( valueNodes [0].InferredType == ExpressionType.Vector ) {
							double4 res = valueNodes.Skip ( 1 ).Aggregate ( ( double4 ) ( valueNodes.First () as Variable ).Value,
								( s, node ) => s / ( node as NumericConstant ).Value );
							res.w = 1;

							resultNode = E.Vec ( "v", res );
						} else if ( valueNodes [0].InferredType == ExpressionType.Numeric ) {
							double res = valueNodes.Skip ( 1 ).Aggregate ( ( valueNodes.First () as NumericConstant ).Value,
								( s, node ) => s / ( node as NumericConstant ).Value );

							resultNode = E.NumConst ( res );
						}
					} else if ( exprNodes.Count == 1 && evalSettings.ReduceUnitDivider &&
						evaluatedOperands [1].IsValueNode &&
						( evaluatedOperands [1] as NumericConstant ).Value == 1 ) {
							resultNode = exprNodes [0];
							exprNodes.Clear ();
					} else
						exprNodes = evaluatedOperands;
				} else if ( OpKind == MultipleOpKind.Dot ) {
					if ( valueNodes.Count == 2 &&
						 valueNodes [0].InferredType == ExpressionType.Vector &&
						 valueNodes [1].InferredType == ExpressionType.Vector )
					{
						Variable v1 = valueNodes [0] as Variable;
						Variable v2 = valueNodes [1] as Variable;

						double res = ( ( double4 ) v1.Value ).xyz & ( ( double4 ) v2.Value ).xyz;

						return	SimplifyIfRoot ( E.NumConst ( res ), evalSettings, isRootNode );
					}
				} else if ( OpKind == MultipleOpKind.Cross ) {
					double4 res = valueNodes.Skip ( 1 ).Aggregate ( ( double4 ) ( valueNodes.First () as Variable ).Value,
							( s, node ) => new double4 ( s * ( double4 ) ( node as Variable ).Value, 1 ) );

					resultNode = E.Vec ( "v", res );
				}

				if ( resultNode != null ) {
					if ( exprNodes.Count == 0 )
						return	SimplifyIfRoot ( resultNode, evalSettings, isRootNode );
					else {
						// FIXIT: remember about different op commutativity when mixin exprNodes with valueNodes!!!
						// The following code is a stub.
						exprNodes.Insert ( 0, resultNode );
					}
				}
			}

			if ( exprNodes.Count == 1 )
				return	SimplifyIfRoot ( exprNodes [0], evalSettings, isRootNode );
			else
				return	SimplifyIfRoot ( E.MultipleOp ( OpKind, exprNodes.ToArray () ), evalSettings, isRootNode );
		}
		
		public override ExpressionCompareResult Compare ( E e, EvalSettings evalSettings = null ) {
			evalSettings = evalSettings ?? E.DefaultEvalSettings;

			if ( this.Equals ( e ) )
				return	ExpressionCompareResult.Equal;
			else if ( OpKind != MultipleOpKind.Sum &&
					  ( ( this.IsNegative && !e.IsNegative && this.SignFree.Equals ( e ) ) ||
						( !this.IsNegative && e.IsNegative && this.Equals ( e.SignFree ) ) ) ) {
				return	ExpressionCompareResult.DiffersBySign;
			} else if ( OpKind == MultipleOpKind.Sum ) {
				MultipleOp eMulOp = e as MultipleOp;

				if ( eMulOp == null )
					return	ExpressionCompareResult.NotEqual;

				List <E> negatedOperands = new List <E> ( eMulOp.Operands );

				for ( int i = 0 ; i < negatedOperands.Count ; i++ )
					negatedOperands [i] = E.Negate ( negatedOperands [i] ).Simplify ( evalSettings );

				if ( this.Equals ( E.MultipleOp ( eMulOp.OpKind, negatedOperands.ToArray () ) ) )
					return	ExpressionCompareResult.DiffersBySign;
				else
					return	ExpressionCompareResult.NotEqual;
			} else
				return	ExpressionCompareResult.NotEqual;
		}

		public override string ToString () {
			if ( OpKind == MultipleOpKind.Sum ) {
				return	Operands.Skip ( 1 ).Aggregate ( Operands.First ().ToString (),
					( str, expr ) => {
						bool embrace = false;

						if ( expr is MultipleOp ) {
							MultipleOp exprMulOp = expr as MultipleOp;

							if ( MultipleOpKindPrecedence [exprMulOp.OpKind] > MultipleOpKindPrecedence [OpKind] )
								embrace = true;
						}

						string exprStr = expr.IsNegative ? expr.SignFree.ToString () : expr.ToString ();

						return	string.Format ( "{0} {1} {2}", str,
							expr.IsNegative ? "-" : "+",
							embrace ? string.Format ( "( {0} )", exprStr ) : exprStr );
					}
				);
			} else {
				return	string.Join ( string.Format ( " {0} ", MultipleOpKindString [OpKind] ),
					Operands.Select ( expr => {
						if ( expr is MultipleOp ) {
							MultipleOp exprMulOp = expr as MultipleOp;

							if ( MultipleOpKindPrecedence [exprMulOp.OpKind] > MultipleOpKindPrecedence [OpKind] )
								return	string.Format ( "( {0} )", exprMulOp );
						}
						
						return	expr.ToString ();
					} )
				);
			}
		}

		#region Mandatory Overrides
		public override int GetHashCode () {
			return	OpKind.GetHashCode () ^ Operands.Aggregate ( 0, ( hash, op ) => hash ^ op.GetHashCode () );
		}

		public override bool Equals ( object obj ) {
		    MultipleOp mulOpObj;
			
		    if ( object.ReferenceEquals ( null, mulOpObj = obj as MultipleOp ) )
		        return	false;

			if ( this.OpKind != mulOpObj.OpKind ||
				 this.Operands.Count != mulOpObj.Operands.Count )
				return	false;

			if ( MultipleOpKindCommutativity [OpKind] == Commutativity.Commutative ) {
				List <E> bOperands = new List <E> ( mulOpObj.Operands );

				foreach ( E operand in Operands ) {
					int idx = bOperands.IndexOf ( operand );

					if ( idx < 0 )
						return	false;

					bOperands.RemoveAt ( idx );
				}
			} else {
				for ( int i = 0 ; i < Operands.Count ; i++ )
					if ( !Operands [i].Equals ( mulOpObj.Operands [i] ) )
						return	false;
			}

			return	true;
		}
		#endregion Mandatory Overrides

		#region Operators
		public static bool operator == ( MultipleOp e1, MultipleOp e2 ) {
		    return	object.ReferenceEquals ( e1, e2 ) || ( !object.ReferenceEquals ( e1, null ) && e1.Equals ( e2 ) );
		}

		public static bool operator != ( MultipleOp e1, MultipleOp e2 ) {
		    return	!object.ReferenceEquals ( e1, e2 ) && ( !object.ReferenceEquals ( e1, null ) && !e1.Equals ( e2 ) );
		}
		#endregion Operators
	}
}
