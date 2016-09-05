using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayTrace;
using Math3d;

namespace RayTraceTest {
	[TestClass]
	public class CommonTests {
		[TestMethod]
		public void SphereTest () {
			Sphere sph = new Sphere ( 2, 0 );
			Ray ray = new Ray ( new double3 ( 4, 0, 0 ), -double3.UnitX );
			Assert.IsTrue ( sph.MayIntersect ( ray ) );

			List <IntersectData> isecData = sph.Intersect ( ray );
			Assert.IsTrue ( isecData.Count == 2 );

			ray.p.y = 5;
			Assert.IsFalse ( sph.MayIntersect ( ray ) );

			sph.ModelMatrix = double4x4.Trans ( new double3 ( 0, 3, 0 ) );
			Assert.IsTrue ( sph.MayIntersect ( ray ) );

			isecData = sph.Intersect ( ray );
			Assert.IsTrue ( isecData.Count == 1 );
		}
	}
}
