using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3.Analyze;
using System.IO;
using Math3d;

namespace Math3AnalyzeDebug {
	static class Program {
		static void Main ( string [] args ) {
			//TbTaEqualTab ();
			//RotateAroundVector ();

			double3 v = new double3 ( 1, 1, 1 );
			double4x4 rotToXZ = double4x4.RotToXZNearest ( v.Normalized );
			double3 r = v * rotToXZ;
			double4x4 rotToXY = double4x4.RotToXYNearest ( v.Normalized );
			r = v * rotToXY;
			double4x4 rotToYZ = double4x4.RotToYZNearest ( v.Normalized );
			r = v * rotToYZ;

			double4x4 rotToX = double4x4.RotToXNearest ( v.Normalized );
			r = v * rotToX;
			double4x4 rotToY = double4x4.RotToYNearest ( v.Normalized );
			r = v * rotToY;
			double4x4 rotToZ = double4x4.RotToZNearest ( v.Normalized );
			r = v * rotToZ;

			//double3 axisY = new double3 ( 1, 1, 1 );
			//double3 commonPerp = ( double3.UnitY * axisY ).Normalized;
			//double4x4 rotV = double4x4.RotV ( commonPerp, 54 );
			//r = double3.UnitX * rotV;

			//double4x4 rotVV = double4x4.RotVV ( double3.UnitY, new double3 ( 1, 1, 1 ) );
			//r = double3.UnitX * rotVV;

			rotToY = double4x4.RotToY ( v );
			double4x4 scaleAlongV = rotToY * double4x4.Scale ( new double3 ( 1, 3, 1 ) ) * rotToY.Transposed;

			//double3 axisY = new double3 ( -0.5, 1, 0.2 );
			//double3 axisX = new double3 ( axisY.y, -axisY.x, -axisY.z );
			//double3 axisZ = new double3 ( axisY.z

			//double3 axisY = new double3 ( 0, 1, 0 );
			//double3 axisX, axisZ;
			//TransformFrame ( axisY, new double3 ( -1, 1, 1 ), out axisX, out axisZ );
			//TestTransformFrame ( axisY, new double3 ( 1, 1, 1 ) );
			//TestTransformFrame ( axisY, new double3 ( -1, 1, -1 ) );
			//TestTransformFrame ( axisY, new double3 ( -1, 1, 1 ) );
			//TestTransformFrame ( axisY, new double3 ( -1, -1, 1 ) );
			//TestTransformFrame ( axisY, new double3 ( 1, -1, 1 ) );
			//TestTransformFrame ( axisY, new double3 ( 1, -1, -1 ) );

			rotToXY = double4x4.RotToXY ( v.Normalized );

			//Vec4 v = new Vec4 ( E.Num ( "x" ), E.Num ( "y" ), E.Num ( "z" ), E.Num ( "w" ) );
			//Mat4 rotToX = Mat4.RotToX ( v );
			//Vec4 rotatedV = v * rotToX;
			//rotatedV = rotatedV.Evaluate ();
		}

		public static void TestTransformFrame ( double3 axisY, double3 newAxisY ) {
			double3 axisX, axisZ;
			TransformFrame ( axisY, newAxisY, out axisX, out axisZ );
			double d1 = axisX & newAxisY;
			double d2 = axisZ & newAxisY;
			double d3 = axisX & axisZ;

			if ( Math.Abs ( d1 ) > 10e-12 )
				throw new Exception ( "test failed" );

			if ( Math.Abs ( d2 ) > 10e-12 )
				throw new Exception ( "test failed" );

			if ( Math.Abs ( d3 ) > 10e-12 )
				throw new Exception ( "test failed" );
		}

		public static void TransformFrame ( double3 axisY, double3 newAxisY, out double3 newAxisX, out double3 newAxisZ ) {
			double3 commonPerp = ( axisY * newAxisY ).Normalized;
			double3 newXYBisector = ( commonPerp * newAxisY ).Normalized;
			newAxisX = ( newXYBisector + commonPerp ).Normalized;
			newAxisZ = ( newXYBisector - commonPerp ).Normalized;
		}

		private static void RotateAroundVector () {
			// Rotate around vector
			Mat4 rotToX = Mat4.RotToX ( new Vec4 ( E.Num ( "x" ), E.Num ( "y" ), E.Num ( "z" ), E.Num ( "w" ) ) );
			Mat4 rotXAlpha = Mat4.RotX ( E.Num ( "alpha" ) );
			Mat4 rotV = rotToX * rotXAlpha * rotToX.Transposed;
			rotV = rotV.Evaluate ();
		}

		private static void TbTaEqualTab () {
			// Tb * Ta == T( a * b )
			Mat4 rotX = Mat4.RotX ( E.Num ( "alpha" ) );
			Mat4 rotY = Mat4.RotY ( E.Num ( "beta" ) );
			Mat4 rotXY = rotX * rotY;
			rotXY = rotXY.Evaluate ();
			Mat4 rotTYTX = rotY.Transposed * rotX.Transposed;
			rotTYTX = rotTYTX.Evaluate ();
			Mat4 rotTXY = ( rotX * rotY ).Transposed;
			rotTXY = rotTXY.Evaluate ();
		}

		private static void EvalStepByStep () {
			string htmlLog;

			E e = E.Sum ( E.Div ( E.NumConst ( 1 ),
								  E.Num ( "d" ) ),
						  E.Mul ( E.Num ( "a" ), E.Div ( E.Sum ( E.Num ( "b" ), E.NumConst ( 14 ), E.Num ( "c" ) ),
														 E.Num ( "d" ) ) ) );
			e = e.EvaluateStepByStep ( out htmlLog, "a", 2, "b", 33, "c", 24 );

			E x = E.Num ( "x" );
			E y = E.Num ( "y" );
			E z = E.Num ( "z" );
			Vec4 v = new Vec4 ( x, y, z, E.Zero );
			Mat4 rotToX = Mat4.RotToX ( v );
			Mat4 rotXAlpha = Mat4.RotX ( E.Num ( "alpha" ) );
			Mat4 r = rotToX * rotXAlpha;
			r.EvaluateStepByStep ( out htmlLog/*, "x", 1, "y", 1, "z", 1*/ );

			Mat4 rotX = Mat4.RotX ( E.Num ( "phi" ) );
			Mat4 rotY  = Mat4.RotY ( E.Num ( "theta" ) );
			Mat4 rotZ = Mat4.RotZ ( E.Num ( "psi" ) );
			r = rotX * rotY * rotZ;
			r.EvaluateStepByStep ( out htmlLog );

			Mat4 rotToZ = Mat4.RotToZ ( v );
			r = rotToZ.Evaluate ();
		}
	}
}
