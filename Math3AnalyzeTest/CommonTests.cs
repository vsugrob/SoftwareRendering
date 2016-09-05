using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math3.Analyze;
using Math3d;

namespace Math3AnalyzeTest {
	[TestClass]
	public class CommonTests {
		[TestMethod]
		public void Common () {
			E v1 = E.Mul ( E.Num ( "s" ), E.Vec ( "v" ), E.NumConst ( 14.0 ) );
			Assert.IsTrue ( v1.InferredType == ExpressionType.Vector );
		}

		[TestMethod]
		public void TypeInferenceTest () {
			E c;

			c = E.Num ( "a" );
			Assert.IsTrue ( c.InferredType == ExpressionType.Numeric );

			c = E.NumConst ( 567 );
			Assert.IsTrue ( c.InferredType == ExpressionType.Numeric );

			c = E.Vec ( "v" );
			Assert.IsTrue ( c.InferredType == ExpressionType.Vector );

			c = E.Sum ( E.NumConst ( 14 ), E.Num ( "a" ) );
			Assert.IsTrue ( c.InferredType == ExpressionType.Numeric );

			try {
				c = E.Sum ( c, E.Vec ( "v" ) );
				Assert.Fail ( "E.Sum of Numeric and Vector should raise exception" );
			} catch {}

			c = E.Sub ( E.Num ( "a" ), E.Num ( "b" ), E.NumConst ( 1992.0 ) );
			Assert.IsTrue ( c.InferredType == ExpressionType.Numeric );

			try {
				c = E.Mul ( E.Vec ( "v" ), E.Vec ( "v2" ) );
				Assert.Fail ( "E.Mul of Vector and Vector should raise exc" );
			} catch {}

			c = E.Mul ( E.NumConst ( 1 ), E.Vec ( "v" ), E.Num ( "a" ) );
			Assert.IsTrue ( c.InferredType == ExpressionType.Vector );

			c = E.Mul ( E.Num ( "a" ), E.NumConst ( 123 ) );
			Assert.IsTrue ( c.InferredType == ExpressionType.Numeric );

			c = E.Div ( E.NumConst ( 18122010 ), E.Num ( "a" ) );
			Assert.IsTrue ( c.InferredType == ExpressionType.Numeric );

			c = E.Div ( E.Vec ( "v" ), E.Num ( "pi" ) );
			Assert.IsTrue ( c.InferredType == ExpressionType.Vector );

			try {
				c = E.Div ( E.Num ( "a" ), E.Vec ( "v" ) );
				Assert.Fail ( "E.Div of Numeric and Vector should raise exc" );
			} catch {}

			try {
				c = E.Div ( E.Vec ( "v" ), E.Vec ( "v2" ) );
				Assert.Fail ( "E.Div of Vector and Vector should raise exc" );
			} catch {}

			c = E.Negate ( E.Num ( "a" ) );
			Assert.IsTrue ( c.InferredType == ExpressionType.Numeric );

			c = E.Negate ( E.Vec ( "v" ) );
			Assert.IsTrue ( c.InferredType == ExpressionType.Vector );

			c = E.Dot ( E.Vec ( "v" ), E.Vec ( "v2" ) );
			Assert.IsTrue ( c.InferredType == ExpressionType.Numeric );

			try {
				c = E.Dot ( E.Num ( "a" ), E.NumConst ( 24 ), E.Vec ( "v" ) );
				Assert.Fail ( "Exc expected" );
			} catch {}

			c = E.Cross ( E.Vec ( "v" ), E.Vec ( "v2" ), E.Vec ( "v3" ) );
			Assert.IsTrue ( c.InferredType == ExpressionType.Vector );
			
			c = E.Sin ( E.Num ( "a" ) );
			Assert.IsTrue ( c.InferredType == ExpressionType.Numeric );

			c = E.Cos ( E.NumConst ( 14.1988 ) );
			Assert.IsTrue ( c.InferredType == ExpressionType.Numeric );

			c = E.Abs ( E.Num ( "a" ) );
			Assert.IsTrue ( c.InferredType == ExpressionType.Numeric );

			try {
				c = E.Sin ( E.Vec ( "a" ) );
				Assert.Fail ( "E.Sin with vector argument should raise exc" );
			} catch {}

			c = E.Vec ( "v" ).Prop ( "x" );
			Assert.IsTrue ( c.InferredType == ExpressionType.Numeric );

			try {
				c = E.Vec ( "v" ).Prop ( "r" );
				Assert.Fail ( "E.Prop with argument 'r' should raise exc" );
			} catch {}
		}

		[TestMethod]
		public void ExpressionToStringTest () {
			// r = a + b * ( c - d )
			E r = E.Sum ( E.Num ( "a" ), E.Mul ( E.Num ( "b" ), E.Sub ( E.Num ( "c" ), E.Num ( "d" ) ) ) );

			// r = y * cos ( a ) - z * sin ( a )
			E a = E.Num ( "a" );
			r = E.Sub ( E.Mul ( E.Num ( "y" ), E.Cos ( a ) ), E.Mul ( E.Num ( "z" ), E.Sin ( a ) ) );

			Vec4 v1 = new Vec4 ( E.Num ( "x" ), E.Num ( "y" ), E.Num ( "z" ), E.Num ( "w" ) );
			Vec4 v2 = new Vec4 ( 1, 2, 3, 4 );
			E v1DotV2 = v1 & v2;

			Mat4 rotX30 = Mat4.RotX ( 30 );
			Mat4 rotXAlpha30 = Mat4.RotX ( E.Num ( "alpha", 30 ) );
			Mat4 trans234 = Mat4.Trans ( new double3 ( 2, 3, 4 ) );
			Mat4 multiplied = rotXAlpha30 * trans234;
		}

		[TestMethod]
		public void EvaluateTest () {
			E numExpr = E.Num ( "a", 14.0 );
			E vecExpr = E.Vec ( "v", new double4 ( 2, 3, 4, 5 ) );
			EvalSettings evalSettings = new EvalSettings ( true );
			E r = numExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is NumericConstant );

			// -a
			E negatedExpr = E.Negate ( numExpr );
			r = negatedExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is NumericConstant );
			Assert.IsTrue ( ( r as NumericConstant ).Value == -14 );

			// -v
			negatedExpr = E.Negate ( vecExpr );
			r = negatedExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is Variable );
			Assert.IsTrue ( ( r as Variable ).Value is double4 );
			Assert.IsTrue ( ( double4 ) ( r as Variable ).Value == new double4 ( -2, -3, -4, -5 ) );

			// sin ( a )
			E funcExpr = E.Sin ( numExpr );
			r = funcExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is NumericConstant );
			Assert.IsTrue ( ( r as NumericConstant ).Value == Math.Sin ( 14.0 ) );

			// cos ( b )
			E varExpr = E.Num ( "b" );
			funcExpr = E.Cos ( varExpr );
			r = funcExpr.Evaluate ( evalSettings );
			Assert.IsFalse ( r is NumericConstant );

			// v.y
			E propAccess = vecExpr.Prop ( "y" );
			r = propAccess.Evaluate ( evalSettings );
			Assert.IsTrue ( r is NumericConstant );
			Assert.IsTrue ( ( r as NumericConstant ).Value == 3 );

			// a = 1988, b = numeric unknown
			// 14 + a + b
			E sumExpr = E.Sum ( E.NumConst ( 14.0 ), E.Num ( "a", 1988 ), E.Num ( "b" ) );
			r = sumExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is MultipleOp );
			Assert.IsTrue ( ( r as MultipleOp ).OpKind == MultipleOpKind.Sum );
			Assert.IsTrue ( ( r as MultipleOp ).Operands [0] is NumericConstant );
			Assert.IsTrue ( ( ( r as MultipleOp ).Operands [0] as NumericConstant ).Value == 2002 );

			// v = { 1, 0, 0, 0 }
			// v2 = { 0, 1, 0, 0 }
			// n = vector unknown
			// v + v2 + n
			E vSumExpr = E.Sum ( E.Vec ( "v", new double4 ( 1, 0, 0, 0 ) ), E.Vec ( "v2", double4.UnitY ), E.Vec ( "n" ) );
			r = vSumExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is MultipleOp );
			Assert.IsTrue ( ( r as MultipleOp ).OpKind == MultipleOpKind.Sum );
			Assert.IsTrue ( ( r as MultipleOp ).Operands [0] is Variable );
			Assert.IsTrue ( ( double4 ) ( ( r as MultipleOp ).Operands [0] as Variable ).Value == new double4 ( 1, 1, 0, 1 ) );

			// a = 14, b = numeric unknown
			// 1988 - a - b
			E subExpr = E.Sub ( E.NumConst ( 1988 ), E.Num ( "a", 14.0 ), E.Num ( "b" ) );
			r = subExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is MultipleOp );
			Assert.IsTrue ( ( r as MultipleOp ).OpKind == MultipleOpKind.Sum );
			Assert.IsTrue ( ( r as MultipleOp ).Operands [0] is NumericConstant );
			Assert.IsTrue ( ( ( r as MultipleOp ).Operands [0] as NumericConstant ).Value == 1974 );

			// v = { 1, 0, 0, 0 }
			// v2 = { 0, 1, 0, 0 }
			// n = vector unknown
			// v - v2 - n
			E vSubExpr = E.Sub ( E.Vec ( "v", new double4 ( 1, 0, 0, 0 ) ), E.Vec ( "v2", double4.UnitY ), E.Vec ( "n" ) );
			r = vSubExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is MultipleOp );
			Assert.IsTrue ( ( r as MultipleOp ).OpKind == MultipleOpKind.Sum );
			Assert.IsTrue ( ( r as MultipleOp ).Operands [0] is Variable );
			Assert.IsTrue ( ( double4 ) ( ( r as MultipleOp ).Operands [0] as Variable ).Value == new double4 ( 1, -1, 0, 1 ) );

			// a = 14, b = numeric unknown
			// 1988 % a % b
			E modExpr = E.Mod ( E.NumConst ( 1988 ), E.Num ( "a", 14.0 ), E.Num ( "b" ) );
			r = modExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is MultipleOp );
			Assert.IsTrue ( ( r as MultipleOp ).OpKind == MultipleOpKind.Mod );
			Assert.IsTrue ( ( r as MultipleOp ).Operands [0] is NumericConstant );
			Assert.IsTrue ( ( ( r as MultipleOp ).Operands [0] as NumericConstant ).Value == 0 );

			// a = 14, b = numeric unknown
			// 1988 * a * b
			E mulExpr = E.Mul ( E.NumConst ( 1988 ), E.Num ( "a", 14.0 ), E.Num ( "b" ) );
			r = mulExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is MultipleOp );
			Assert.IsTrue ( ( r as MultipleOp ).OpKind == MultipleOpKind.Mul );
			Assert.IsTrue ( ( r as MultipleOp ).Operands [0] is NumericConstant );
			Assert.IsTrue ( ( ( r as MultipleOp ).Operands [0] as NumericConstant ).Value == 27832 );

			// v = { 1, 2, 3, 1 }, b = numeric unknown
			// v * 14 * b
			E vMulExpr = E.Mul ( E.Vec ( "v", new double4 ( 1, 2, 3, 1 ) ), E.NumConst ( 14 ), E.Num ( "b" ) );
			r = vMulExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is MultipleOp );
			Assert.IsTrue ( ( r as MultipleOp ).OpKind == MultipleOpKind.Mul );
			Assert.IsTrue ( ( r as MultipleOp ).Operands [0] is Variable );
			Assert.IsTrue ( ( double4 ) ( ( r as MultipleOp ).Operands [0] as Variable ).Value == new double4 ( 14, 28, 42, 1 ) );

			// a = 14
			// 1988 / a
			E divExpr = E.Div ( E.NumConst ( 1988 ), E.Num ( "a", 14.0 ) );
			r = divExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is NumericConstant );
			Assert.IsTrue ( ( r as NumericConstant ).Value == 142 );

			// a = 14, b = numeric unknown
			// a / b
			divExpr = E.Div ( E.Num ( "a", 14.0 ), E.Num ( "b" ) );
			r = divExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is MultipleOp );
			Assert.IsTrue ( ( r as MultipleOp ).OpKind == MultipleOpKind.Div );
			Assert.IsTrue ( ( r as MultipleOp ).Operands [0] is NumericConstant );
			Assert.IsTrue ( ( ( r as MultipleOp ).Operands [0] as NumericConstant ).Value == 14 );

			// v = { 1, 2, 3, 1 }
			// v / 14
			E vDivExpr = E.Div ( E.Vec ( "v", new double4 ( 1, 2, 3, 1 ) ), E.NumConst ( 14 ) );
			r = vDivExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is Variable );
			Assert.IsTrue ( ( double4 ) ( r as Variable ).Value == new double4 ( 1.0 / 14, 2.0 / 14, 3.0 / 14, 1 ) );

			// v = { 1, 2, 3, 1 }, b = numeric unknown
			// v / b
			vDivExpr = E.Div ( E.Vec ( "v", new double4 ( 1, 2, 3, 1 ) ), E.Num ( "b" ) );
			r = vDivExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is MultipleOp );
			Assert.IsTrue ( ( r as MultipleOp ).OpKind == MultipleOpKind.Div );
			Assert.IsTrue ( ( r as MultipleOp ).Operands [0] is Variable );
			Assert.IsTrue ( ( double4 ) ( ( r as MultipleOp ).Operands [0] as Variable ).Value == new double4 ( 1.0, 2.0, 3.0, 1 ) );

			// v1 = { 1, 2, 3, 1 }
			// v2 = { 3, 2, 1, 1 }
			// v1 & v2
			E vDotExpr = E.Dot ( E.Vec ( "v1", new double4 ( 1, 2, 3, 1 ) ), E.Vec ( "v2", new double4 ( 3, 2, 1, 1 ) ) );
			r = vDotExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is NumericConstant );
			Assert.IsTrue ( ( r as NumericConstant ).Value == 10 );

			// v1 = { 1, 2, 3, 1 }
			// v2 = { 3, 2, 1, 1 }
			// v1 x v2
			E vCrossExpr = E.Cross ( E.Vec ( "v1", new double4 ( 1, 2, 3, 1 ) ), E.Vec ( "v2", new double4 ( 3, 2, 1, 1 ) ) );
			r = vCrossExpr.Evaluate ( evalSettings );
			Assert.IsTrue ( ( r is Variable ) );
			Assert.IsTrue ( ( double4 ) ( r as Variable ).Value == new double4 ( -4, 8, -4, 1 ) );
		}

		[TestMethod]
		public void MatrixEvaluationTest () {
			EvalSettings evalSettings = new EvalSettings ();
			evalSettings.EvalFuncs = false;

			Mat4 rotX30 = Mat4.RotX ( 30 );
			Mat4 trans = Mat4.Trans ( new double3 ( 2, 3, 4 ) );
			Mat4 r = rotX30 * trans;
			r = r.Evaluate ( evalSettings );

			//evalSettings = new EvalSettings ( "phi", 30, "theta", 60, "psi", 90.0 );
			Mat4 rotX = Mat4.RotX ( E.Num ( "phi" ) );
			Mat4 rotY  = Mat4.RotY ( E.Num ( "theta" ) );
			Mat4 rotZ = Mat4.RotZ ( E.Num ( "psi" ) );
			r = rotX * rotY * rotZ;
			r = r.Evaluate ( evalSettings );
			r = r.Transposed;
		}

		[TestMethod]
		public void ReduceMultiplyingTest () {
			EvalSettings evalSettings = new EvalSettings ();
			evalSettings.EvalFuncs = false;

			// 14 + 0 * sin ( 0.3 + 0.5 )
			E expr = E.Sum ( E.NumConst ( 14 ), E.Mul ( E.NumConst ( 0 ), E.Sin ( E.Sum ( E.NumConst ( 0.3 ), E.NumConst ( 0.5 ) ) ) ) );
			E r = expr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is NumericConstant );
			Assert.IsTrue ( ( r as NumericConstant ).Value == 14 );

			// 1988 + 0.5 * cos ( a ) * 2
			expr = E.Sum ( E.NumConst ( 1988 ), E.Mul ( E.NumConst ( 0.5 ), E.Cos ( E.Num ( "a" ) ), E.NumConst ( 2 ) ) );
			r = expr.Evaluate ( evalSettings );
			Assert.IsTrue ( r is MultipleOp );
			Assert.IsTrue ( ( r as MultipleOp ).OpKind == MultipleOpKind.Sum );
			Assert.IsTrue ( ( r as MultipleOp ).Operands [1] is FuncCall );
		}

		[TestMethod]
		public void ReduceUnitDivisionTest () {
			E a = E.Num ( "a" );
			E e = E.Div ( a, E.NumConst ( 1 ) );
			E r = e.Evaluate ( new EvalSettings ( false ) );
			Assert.IsTrue ( r == a );
		}

		[TestMethod]
		public void NegateSumTest () {
			E sumExpr = E.Sum ( E.NumConst ( 14 ), E.Num ( "a" ) );
			E negateSumExpr = E.Negate ( sumExpr );
		}

		[TestMethod]
		public void SimplifyNestedNegationsTest () {
			E neg1 = E.Negate ( E.NumConst ( 1 ) );
			E neg2 = E.Negate ( neg1 );
			E neg2Simplified = neg2.Simplify ();	// should be 1
			E neg3 = E.Negate ( neg2 );
			E neg3Simplified = neg3.Simplify ();	// should be -1

			E negativeNum = E.NumConst ( -14 );
			E negatedNegativeNum = E.Negate ( negativeNum );
			E negatedNegativeNumSimplified = negatedNegativeNum.Simplify ();	// should be 14
		}

		[TestMethod]
		public void SimplifyMultipleOpTest () {
			// a + b * ( -14 ) * ( -c ) * d
			E e = E.Sum ( E.Num ( "a" ), E.Mul ( E.Num ( "b" ), E.NumConst ( -14 ), E.Negate ( E.Num ( "c" ) ), E.Num ( "d" ) ) );
			E se = e.Simplify ();

			// a + ( -b ) * ( -14 ) * ( -c ) * d
			e = E.Sum ( E.Num ( "a" ), E.Mul ( E.Negate ( E.Num ( "b" ) ), E.NumConst ( -14 ), E.Negate ( E.Num ( "c" ) ), E.Num ( "d" ) ) );
			se = e.Simplify ();
		}

		[TestMethod]
		public void AsHtmlTest () {
			E r = E.Sqrt ( E.Sum ( E.Cos ( E.Num ( "alpha" ) ), E.Sin ( E.Num ( "beta" ) ) ) );
			r = E.Pow ( r, E.Mul ( E.Num ( "gamma" ), E.NumConst ( 14 ) ) );
			r = E.Pow ( E.Num ( "delta" ), r );
		}

		[TestMethod]
		public void RotToPlaneTest () {
			E x = E.Num ( "x" );
			E y = E.Num ( "y" );
			E z = E.Num ( "z" );
			Vec4 v = new Vec4 ( x, y, z, E.Zero );
			Mat4 rotToXY = Mat4.RotToXY ( v );
			Mat4 r = rotToXY.Evaluate ( new EvalSettings ( false ) );
		}

		[TestMethod]
		public void RotToAxisTest () {
			E x = E.Num ( "x" );
			E y = E.Num ( "y" );
			E z = E.Num ( "z" );
			Vec4 v = new Vec4 ( x, y, z, E.Zero );
			Mat4 rotToX = Mat4.RotToX ( v );
			Mat4 rotXAlpha = Mat4.RotX ( E.Num ( "alpha" ) );
			Mat4 r = rotToX * rotXAlpha;
			r = r.Evaluate ( new EvalSettings ( false ) );
		}

		[TestMethod]
		public void SimplifyDivisionMultiplicationsTest () {
			E e1 = E.Div ( E.Num ( "a" ), E.NumConst ( 2 ) );
			E e2 = E.Sum ( E.Num ( "b" ), E.NumConst ( 14 ) );
			E e3 = E.Negate ( E.Div ( E.Sin ( E.Num ( "theta" ) ), E.Dot ( E.Vec ( "v1" ), E.Vec ( "v2" ) ) ) );
			E e = E.Mul ( e1, e2, e3 );
			e = E.Sum ( E.Num ( "Delta" ), e );
			E r = e.Simplify ();
		}

		[TestMethod]
		public void SimplifyManyDivisionsTest () {
			// a / ( b / c ) => ( a * c ) / d
			E e = E.Div ( E.Num ( "a" ), E.Div ( E.Num ( "b" ), E.Num ( "c" ) ) );
			E r = e.Simplify ();

			// a / ( b / ( c / d ) ) => ( a * c ) / ( b * d )
			e = E.Div ( E.Num ( "a" ), E.Div ( E.Num ( "b" ), E.Div ( E.Num ( "c" ), E.Num ( "d" ) ) ) );
			r = e.Simplify ();
		}

		[TestMethod]
		public void InverseRotationTest () {
			Mat4 rotX = Mat4.RotX ( E.Num ( "alpha" ) );
			Mat4 rotXNeg = Mat4.RotX ( E.Negate ( E.Num ( "alpha" ) ) );
			Mat4 r = rotX * rotXNeg;
			r = r.Evaluate ( new EvalSettings ( false ) );
			Assert.IsTrue ( r == Mat4.Identity );

			E x = E.Num ( "x" );
			E y = E.Num ( "y" );
			E z = E.Num ( "z" );
			Vec4 v = new Vec4 ( x, y, z, E.Zero );
			Mat4 rotToXY = Mat4.RotToXY ( v );
			Mat4 rotToXYInv = rotToXY.Transposed;
			r = rotToXY * rotToXYInv;
			r = r.Evaluate ();
			Assert.IsTrue ( r == Mat4.Identity );

			Mat4 rotToX = Mat4.RotToX ( v );
			Mat4 rotToXInv = rotToX.Transposed;
			r = rotToX * rotToXInv;
			r = r.Evaluate ();
			r = r.Evaluate ( new EvalSettings ( "x", 10, "y", 15, "z", 20 ) );
			Assert.IsTrue ( r == Mat4.Identity );
		}

		[TestMethod]
		public void EqualityAndCompareTest () {
			E e1 = E.Mul ( E.Sum ( E.Negate ( E.Num ( "a" ) ), E.NumConst ( 14 ) ), E.Cos ( E.Num ( "theta" ) ) );
			E e2 = E.Mul ( E.Cos ( E.Num ( "theta" ) ), E.Sum ( E.Negate ( E.Num ( "a" ) ), E.NumConst ( 14 ) ) );
			Assert.IsTrue ( e1 == e2 );

			e1 = E.Num ( "a" );
			e2 = E.Negate ( E.Num ( "a" ) );
			Assert.IsTrue ( e1.Compare ( e2 ) == ExpressionCompareResult.DiffersBySign );

			e1 = E.NumConst ( 14 );
			e2 = E.NumConst ( -14 );
			Assert.IsTrue ( e1.Compare ( e2 ) == ExpressionCompareResult.DiffersBySign );

			e2 = E.Negate ( E.NumConst ( 14 ) );
			Assert.IsTrue ( e1.Compare ( e2 ) == ExpressionCompareResult.DiffersBySign );

			e1 = E.Mul ( E.Num ( "a" ), E.Sin ( E.Num ( "theta" ) ) );
			e2 = E.Mul ( E.Sin ( E.Num ( "theta" ) ), E.Num ( "a" ) );
			Assert.IsTrue ( e1.Compare ( e2 ) == ExpressionCompareResult.Equal );

			e1 = E.Sum ( E.Num ( "m" ), E.Pow ( E.NumConst ( 3 ), E.Sin ( E.Num ( "alpha" ) ) ) );
			e2 = E.Sum ( E.Negate ( E.Num ( "m" ) ), E.Negate ( E.Pow ( E.NumConst ( 3 ), E.Sin ( E.Num ( "alpha" ) ) ) ) );
			Assert.IsTrue ( e1.Compare ( e2 ) == ExpressionCompareResult.DiffersBySign );

			e1 = E.Mul ( E.Sin ( E.Num ( "alpha" ) ), E.Negate ( E.Cos ( E.Num ( "alpha" ) ) ) );
			e2 = E.Mul ( E.Sin ( E.Negate ( E.Num ( "alpha" ) ) ), E.Cos ( E.Num ( "alpha" ) ) );
			e1 = e1.Evaluate ();
			e2 = e2.Evaluate ();
			Assert.IsTrue ( e1.Compare ( e2 ) == ExpressionCompareResult.Equal );
		}

		[TestMethod]
		public void SimplifyByExtractingLinearCoefficientTest () {
			E e = E.Sum ( E.Num ( "a" ), E.Num ( "b" ), E.Num ( "a" ), E.Num ( "c" ), E.Negate ( E.Num ( "b" ) ), E.Negate ( E.Num ( "a" ) ) );
			e = e.Simplify ();
			Assert.IsTrue ( e == ( E.Sum ( E.Num ( "a" ), E.Num ( "c" ) ) ) );

			e = E.Sum ( E.Negate ( E.Mul ( E.NumConst ( 2 ), E.Num ( "b" ) ) ) );
			e = e.Simplify ();

			e = E.Sum ( E.Num ( "a" ), E.Num ( "b" ), E.Mul ( E.NumConst ( 14 ), E.Num ( "a" ) ), E.Negate ( E.Mul ( E.NumConst ( 2 ), E.Num ( "b" ) ) ) );
			e = e.Simplify ();
		}

		[TestMethod]
		public void SimplifyByExtractingPowerTest () {
			E e = E.Mul ( E.Num ( "a" ), E.Num ( "b" ), E.Num ( "a" ) );
			e = e.Simplify ();
			
			e = E.Mul ( E.Num ( "a" ), E.Div ( E.Mul ( E.Num ( "a" ), E.Num ( "b" ), E.Num ( "a" ) ),
									   E.Mul ( E.Num ( "c" ), E.Num ( "a" ), E.Num ( "b" ) ) ) );
			e = e.Simplify ();
		}

		[TestMethod]
		public void SimplifySin2PlusCos2Test () {
			E e = E.Sum ( E.Mul ( E.NumConst ( 14 ), E.Pow ( E.Sin ( E.Num ( "alpha" ) ), E.NumConst ( 2 ) ) ), E.Mul ( E.NumConst ( 14 ), E.Pow ( E.Cos ( E.Num ( "alpha" ) ), E.NumConst ( 2 ) ) ) );
			e = e.Simplify ();
			Assert.IsTrue ( e is NumericConstant );
			Assert.IsTrue ( ( e as NumericConstant ).Value == 14 );
		}

		[TestMethod]
		public void GetNumericPowerTest () {
			// pow ( pow ( -a, 3 ), 2 ) => pow ( a, 6 )
			E e = E.Pow ( E.Pow ( E.Negate ( E.Num ( "a" ) ), E.NumConst ( 3 ) ), E.NumConst ( 2 ) );
			E e1;
			bool isNeg;
			bool needAbs;
			double pow = MultipleOp.GetNumericPower ( e, out e1, out isNeg, out needAbs );
			Assert.IsTrue ( pow == 6 );
			
			// pow ( sqrt ( a ), 3 ) => pow ( a, 3 / 2 )
			e = E.Pow ( E.Sqrt ( E.Num ( "a" ) ), E.NumConst ( 3 ) );
			pow = MultipleOp.GetNumericPower ( e, out e1, out isNeg, out needAbs );
			Assert.IsTrue ( pow == 1.5 );

			// pow ( pow ( a, 1/4 ), 2 ) => pow ( a, 1 / 2 ) => sqrt ( a )
			e = E.Pow ( E.Pow ( E.Num ( "a" ), E.NumConst ( 0.25 ) ), E.NumConst ( 2 ) );
			pow = MultipleOp.GetNumericPower ( e, out e1, out isNeg, out needAbs );
			Assert.IsTrue ( pow == 0.5 );

			// pow ( pow ( -a, 2 ), 1.5 ) => pow ( abs ( a ), 3 )
			e = E.Pow ( E.Pow ( E.Negate ( E.Num ( "a" ) ), E.NumConst ( 2 ) ), E.NumConst ( 1.5 ) );
			pow = MultipleOp.GetNumericPower ( e, out e1, out isNeg, out needAbs );
			Assert.IsTrue ( pow == 3 );
		}

		[TestMethod]
		public void SimplifyPowTest () {
			// pow ( pow ( -a, 2 ), 1.5 ) => pow ( abs ( a ), 3 )
			E e = E.Pow ( E.Pow ( E.Negate ( E.Num ( "a" ) ), E.NumConst ( 2 ) ), E.NumConst ( 1.5 ) );
			e = e.Simplify ();
			Assert.IsTrue ( e == E.Pow ( E.Abs ( E.Num ( "a" ) ), E.NumConst ( 3 ) ) );

			// pow ( pow ( a, 3 ), 2 ) => pow ( a, 6 )
			e = E.Pow ( E.Pow ( E.Negate ( E.Num ( "a" ) ), E.NumConst ( 3 ) ), E.NumConst ( 2 ) );
			e = e.Simplify ();
			Assert.IsTrue ( e == E.Pow ( E.Num ( "a" ), E.NumConst ( 6 ) ) );
			
			// pow ( sqrt ( a ), 3 ) => pow ( a, 3 / 2 )
			e = E.Pow ( E.Sqrt ( E.Num ( "a" ) ), E.NumConst ( 3 ) );
			e = e.Simplify ();
			Assert.IsTrue ( e == E.Pow ( E.Num ( "a" ), E.NumConst ( 1.5 ) ) );

			// pow ( pow ( a, 1/4 ), 2 ) => pow ( a, 1 / 2 ) => sqrt ( a )
			e = E.Pow ( E.Pow ( E.Num ( "a" ), E.NumConst ( 0.25 ) ), E.NumConst ( 2 ) );
			e = e.Simplify ();
			Assert.IsTrue ( e == E.Sqrt ( E.Num ( "a" ) ) );

			// ( ( -a ) ^ 3 ) ^ 2 * ( a ^ 0.25 ) ^ 2 => ( -( a ^ 3 ) ) ^ 2 * a ^ 0.5 => a ^ 6 * a * 0.5 => a * 6.5
			e = E.Mul ( E.Pow ( E.Pow ( E.Negate ( E.Num ( "a" ) ), E.NumConst ( 3 ) ), E.NumConst ( 2 ) ),
						E.Pow ( E.Pow ( E.Num ( "a" ), E.NumConst ( 0.25 ) ), E.NumConst ( 2 ) ) );
			e = e.Simplify ();
			Assert.IsTrue ( e == E.Pow ( E.Num ( "a" ), E.NumConst ( 6.5 ) ) );
		}

		[TestMethod]
		public void MoveNegativePowersToNumeratorOrDenomenatorTest () {
			// a * pow ( b, -2 ) => a / pow ( b, 2 )
			E e = E.Mul ( E.Pow ( E.Num ( "a" ), E.NumConst ( -2 ) ), E.Num ( "b" ) );
			e = e.Simplify ();
			Assert.IsTrue ( e == E.Div ( E.Num ( "b" ), E.Pow ( E.Num ( "a" ), E.NumConst ( 2 ) ) ) );

			// b / pow ( a, -2 ) => pow ( a, 2 ) * b
			e = E.Div ( E.Num ( "b" ), E.Pow ( E.Num ( "a" ), E.NumConst ( -2 ) ) );
			e = e.Simplify ();
			Assert.IsTrue ( e == E.Mul ( E.Num ( "b" ), E.Pow ( E.Num ( "a" ), E.NumConst ( 2 ) ) ) );
		}

		[TestMethod]
		public void FlattenMultipleOpTest () {
			// w * ( ( x * y ) * ( x * z ) ) * z => w * pow ( x, 2 ) * y * pow ( z, 2 )
			E e = E.Mul ( E.Num ( "w" ),
						  E.Mul ( E.Mul ( E.Num ( "x" ), E.Num ( "y" ) ),
								  E.Mul ( E.Num ( "x" ), E.Num ( "z" ) ) ),
						  E.Num ( "z" ) );
			e = e.Simplify ();
			Assert.IsTrue ( e == E.Mul ( E.Num ( "w" ), E.Pow ( E.Num ( "x" ), E.NumConst ( 2 ) ), E.Num ( "y" ), E.Pow ( E.Num ( "z" ), E.NumConst ( 2 ) ) ) );
		}

		[TestMethod]
		public void FractionSumTest () {
			// a + b / c + ( a + c ) / c => a + ( b + a + c ) / c
			E e = E.Sum ( E.Num ( "a" ), E.Div ( E.Num ( "b" ), E.Num ( "c" ) ), E.Div ( E.Sum ( E.Num ( "a" ), E.Num ( "c" ) ), E.Num ( "c" ) ) );
			e = e.Simplify ();
			Assert.IsTrue ( e == E.Sum ( E.Num ( "a" ), E.Div ( E.Sum ( E.Num ( "b" ), E.Num ( "a" ), E.Num ( "c" ) ), E.Num ( "c" ) ) ) );
		}
	}
}
