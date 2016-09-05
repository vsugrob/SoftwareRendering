using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RayTrace;
using Math3d;
using Common3d;

namespace RayTraceDebug {
	class Program {
		static void Main ( string [] args ) {
			Scene scene = new Scene ();
			Sphere sphere = new Sphere ( 2, new double3 ( 0, 0, 10 ) );
			scene.Objects.Add ( sphere );

			EyeBasedRayTracer tracer = new EyeBasedRayTracer ();
			tracer.Scene = scene;
			tracer.ViewportSize = new IntSize ( 100, 100 );
			tracer.Camera = new Camera ();

			tracer.Render ();
		}
	}
}
