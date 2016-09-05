using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math3d;

namespace Math3Test {
	[TestClass]
	public class CommonTests {
		[TestMethod]
		public void Equality () {
			double2 a = new double2 ( 1, 2 );
			double2 b = new double2 ( 1, 2 );

			Assert.IsTrue ( a.Equals ( b ), "Member function Equals () returned false on equal vectors" );
			Assert.IsTrue ( double2.Equals ( a, b ), "Static function double2.Equals () returned false on equal vectors" );

			double2 c = 14;
			double2 d = new double2 ( 14, 14 );
			Assert.IsTrue ( c == d );

			Assert.IsTrue ( 1 == a.xx );

			Assert.IsTrue ( new double2 ( 2, 1 ) == a.yx );

			double2 e = a + 5;
		}

		[TestMethod]
		public void OperatorsTest () {
			double3 a = new double3 ( 1, 2, 3 );
			double4 b = new double4 ( 9, 8, 7, 6 );
			double4 c = new double4 ( 5, 4, 3, 2 );
			var r = a * b;	// * is public static double3 operator * ( double3 a, double3 b )
							// and that is right.
			var s = b * c;
		}

		[TestMethod]
		public void ItemPropertyTest () {
			double3 a = new double3 ( 1, 2, 3 );
			double4 b = new double4 ( 9, 8, 7, 6 );
			Assert.IsTrue ( a [1] == 2 );
			Assert.IsTrue ( b [3] == 6 );

			a [2] = 14;
			Assert.IsTrue ( a [2] == 14 );
			
			try {
				a [55] = 55;
				Assert.Fail ();
			} catch {}

			try {
				double v = a [55];
				Assert.Fail ();
			} catch {}
		}

		[TestMethod]
		public void RowMajorMatricesTest () {
			Math3.MatrixLayout = MatrixLayout.RowMajor;
			Math3.DIFF_THR = 1e-6;
			Math3.DOUBLE_PRECISION = 6;

			double4x4 rotX60FromXna = new double4x4 ( new double [,] {
				{1, 0, 0, 0},
				{0, 0.5, 0.8660254, 0},
				{0, -0.8660254, 0.5, 0},
				{0, 0, 0, 1}
			} );

			double4x4 copy = rotX60FromXna;
			Assert.IsTrue ( rotX60FromXna == copy );
			Assert.IsFalse ( rotX60FromXna != copy );

			copy [1, 1] = 0.14;
			Assert.IsTrue ( rotX60FromXna != copy );

			double4x4 rotX60 = double4x4.RotX ( 60 );
			Assert.IsTrue ( rotX60FromXna == rotX60 );

			double4x4 rotYMinus520FromXna = new double4x4 ( new double [,] {
				{-0.9396927, 0, 0.3420201, 0},
				{0, 1, 0, 0},
				{-0.3420201, 0, -0.9396927, 0},
				{0, 0, 0, 1}
			} );

			double4x4 rotYMinus520 = double4x4.RotY ( -520 );
			Assert.IsTrue ( rotYMinus520 == rotYMinus520FromXna );

			double4x4 rotZ33FromXna = new double4x4 ( new double [,] {
				{0.8386706, 0.5446391, 0, 0},
				{-0.5446391, 0.8386706, 0, 0},
				{0, 0, 1, 0},
				{0, 0, 0, 1}
			} );

			double4x4 rotZ33 = double4x4.RotZ ( 33 );
			Assert.IsTrue ( rotZ33 == rotZ33FromXna );

			double4 v1 = new double4 ( 1, 2, 3, 1 );
			v1 = v1 * rotZ33;

			double4 vFromXna = new double4 ( -0.2506076, 2.22198, 3, 1 );
			Assert.IsTrue ( v1 == vFromXna );
		}

		[TestMethod]
		public void ColumnMajorMatricesTest () {
			Math3.MatrixLayout = MatrixLayout.ColumnMajor;
			Math3.DIFF_THR = 1e-6;
			Math3.DOUBLE_PRECISION = 6;

			double4x4 rotX60FromOpenTK = new double4x4 ( new double [,] {
				{1, 0, 0, 0},
				{0, 0.5, -0.8660254, 0},
				{0, 0.8660254, 0.5, 0},
				{0, 0, 0, 1}
			} );

			double4x4 rotX60 = double4x4.RotX ( 60 );
			Assert.IsTrue ( rotX60FromOpenTK == rotX60 );

			double4x4 rotYMinus520FromOpenTK = new double4x4 ( new double [,] {
				{-0.9396927, 0, -0.3420201, 0},
				{0, 1, 0, 0},
				{0.3420201, 0, -0.9396927, 0},
				{0, 0, 0, 1}
			} );

			double4x4 rotYMinus520 = double4x4.RotY ( -520 );
			Assert.IsTrue ( rotYMinus520 == rotYMinus520FromOpenTK );

			double4x4 rotZ33FromOpenTK = new double4x4 ( new double [,] {
				{0.8386706, -0.5446391, 0, 0},
				{0.5446391, 0.8386706, 0, 0},
				{0, 0, 1, 0},
				{0, 0, 0, 1}
			} );

			double4x4 rotZ33 = double4x4.RotZ ( 33 );
			Assert.IsTrue ( rotZ33 == rotZ33FromOpenTK );

			double4 v1 = new double4 ( 1, 2, 3, 1 );
			v1 = rotZ33 * v1;

			double4 vFromOpenTK = new double4 ( -0.2506076, 2.22198, 3, 1 );
			Assert.IsTrue ( v1 == vFromOpenTK );
		}

		[TestMethod]
		public void MatrixDeterminantAndInverseTest () {
			Math3.MatrixLayout = MatrixLayout.RowMajor;
			Math3.DIFF_THR = 1e-6;
			Math3.DOUBLE_PRECISION = 6;

			double4x4 rotX60FromOpenTK = new double4x4 ( new double [,] {
				{1, 0, 0, 0},
				{0, 0.5, -0.8660254, 0},
				{0, 0.8660254, 0.5, 0},
				{0, 0, 0, 1}
			} );

			double det = rotX60FromOpenTK.Det3;
			Assert.IsTrue ( Math.Abs ( det - 1 ) <= Math3.DIFF_THR );

			rotX60FromOpenTK._22 = 3;
			det = rotX60FromOpenTK.Det3;
			Assert.IsTrue ( Math.Abs ( det - 2.25 ) <= Math3.DIFF_THR );

			double4x4 rotX20 = double4x4.RotX ( 20 );
			double4x4 rotXMinus20 = double4x4.RotX ( -20 );
			double4x4 rotProduct = rotX20 * rotXMinus20;
			Assert.IsTrue ( rotProduct == double4x4.Identity );
		}

		[TestMethod]
		public void MatrixMultiplicationTest () {
			Math3.MatrixLayout = MatrixLayout.RowMajor;
			Math3.DIFF_THR = 1e-6;
			Math3.DOUBLE_PRECISION = 6;

			double4x4 rotX60FromXna = new double4x4 ( new double [,] {
				{1, 0, 0, 0},
				{0, 0.5, 0.8660254, 0},
				{0, -0.8660254, 0.5, 0},
				{0, 0, 0, 1}
			} );

			double4x4 rotYMinus520FromXna = new double4x4 ( new double [,] {
				{-0.9396927, 0, 0.3420201, 0},
				{0, 1, 0, 0},
				{-0.3420201, 0, -0.9396927, 0},
				{0, 0, 0, 1}
			} );

			double4x4 productFromXna = new double4x4 ( new double [,] {
				{-0.9396927, 0, 0.3420201, 0},
				{-0.2961981, 0.5, -0.8137978, 0},
				{-0.17101, -0.8660254, -0.4698463, 0},
				{0, 0, 0, 1}
			} );

			double4x4 product = rotX60FromXna * rotYMinus520FromXna;
			Assert.IsTrue ( product == productFromXna );
		}

		[TestMethod]
		public void MatrixMultiplicationOrder () {
			double4x4 rotX30 = double4x4.RotX ( 30 );
			double4x4 transXY = double4x4.Trans ( new double3 ( 5, 10, 0 ) );

			double4x4 rotXTrans = rotX30 * transXY;
			double4x4 transXRot = transXY * rotX30;
			Assert.IsTrue ( rotXTrans != transXRot );
		}

		[TestMethod]
		public void VectorMultiplicationTest () {
			double3 view = double3.UnitZ;
			double3 up = double3.UnitY;
			double3 right = up * view;
			Assert.IsTrue ( right == double3.UnitX );

			up = view * right;
			Assert.IsTrue ( up == double3.UnitY );
		}

		[TestMethod]
		public void VectorRefractTest () {
			double sin45 = Math.Sin ( 45 * Math.PI / 180 );
			double2 v = new double2 ( -sin45, sin45 );
			double2 n = new double2 ( 0, 1 );
			double2 f = v.Refract ( n, 0.9 );
			Assert.IsTrue ( f == ( -v ).RefractI ( n, 0.9 ) );
		}

		[TestMethod]
		public void IntsAlongDirTest () {
			int p, n;
			1.3.IntsAlongDir ( 1, out p, out n );
			Assert.IsTrue ( p == 1 && n == 2 );
			1.3.IntsAlongDir ( -1, out p, out n );
			Assert.IsTrue ( p == 2 && n == 1 );
			( -1.3 ).IntsAlongDir ( -1, out p, out n );
			Assert.IsTrue ( p == -1 && n == -2 );
			( -1.3 ).IntsAlongDir ( 1, out p, out n );
			Assert.IsTrue ( p == -2 && n == -1 );
			1.0.IntsAlongDir ( 1, out p, out n );
			Assert.IsTrue ( p == 1 && n == 2 );
			1.0.IntsAlongDir ( -1, out p, out n );
			Assert.IsTrue ( p == 1 && n == 0 );
			( -1.0 ).IntsAlongDir ( -1, out p, out n );
			Assert.IsTrue ( p == -1 && n == -2 );
			( -1.0 ).IntsAlongDir ( 1, out p, out n );
			Assert.IsTrue ( p == -1 && n == 0 );

			( 1 - 1e-15 ).IntsAlongDir ( 1, out p, out n );
			Assert.IsTrue ( p == 1 && n == 2 );
			( 1 - 1e-15 ).IntsAlongDir ( -1, out p, out n );
			Assert.IsTrue ( p == 1 && n == 0 );
			( -1 + 1e-15 ).IntsAlongDir ( -1, out p, out n );
			Assert.IsTrue ( p == -1 && n == -2 );
			( -1 + 1e-15 ).IntsAlongDir ( 1, out p, out n );
			Assert.IsTrue ( p == -1 && n == 0 );
		}

		[TestMethod]
		public void LerpTest () {
			//var lerp = 5.0.LerpTo ( 20, 10 );
			//var zLerp = 11.0.ZLerpTo ( 2, 10, 1, 7 );
			//var interps = new IEnumerable <double> [] { lerp, zLerp };

			//var enums = interps.Select ( i => i.GetEnumerator () as IInterpolatorEnumerator );


			//double y0 = 5.3, y1 = 10.7;
			//double dy = Math.Abs ( y1 - y0 );
			//var lerp = 7.0.LerpTo ( 15, dy ).GetEnumerator ();
			//var rasterizer1 = Rasterizer.Create ( 0, 20, y0, y1, new { XLerp = lerp }, lerp );
			//var rasterizer2 = Rasterizer.Create ( 0, 20, y1, y0, new { XLerp = lerp }, lerp );
			
			//foreach ( var sample in rasterizer1 ) {
			//    Console.WriteLine ( "[{0}]: {1}", sample.Item1, sample.Item2.XLerp.Current );
			//}

			//foreach ( var sample in rasterizer2 ) {
			//    Console.WriteLine ( "[{0}]: {1}", sample.Item1, sample.Item2.XLerp.Current );
			//}


			//var lerp = double2.UnitX.LerpTo ( double2.UnitY, 5 );

			//foreach ( double2 sample in lerp )
			//    Console.WriteLine ( sample );

			//var lerp = double3.UnitX.LerpTo ( double3.UnitY + double3.UnitZ, 5 );

			//foreach ( double3 sample in lerp )
			//    Console.WriteLine ( sample );

			//var lerp = double2.UnitX.ZLerpTo ( double2.UnitY, 5, 1, 3 );

			//foreach ( double2 sample in lerp )
			//    Console.WriteLine ( sample );
		}
	}
}
