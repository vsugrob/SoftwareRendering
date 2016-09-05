using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common3d;
using Math3d;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RayTrace {
	public class EyeBasedRayTracer : RayTracer {
		#region Properties
		#endregion Properties

		#region Constructors
		public EyeBasedRayTracer () {
			this.InitTraceData = new TraceData ( 10, 10 );
		}
		#endregion Constructors

		#region Methods
		public override void Render () {
			Stopwatch sw = Stopwatch.StartNew ();
			AllocateImage ();

			double hFovX = Camera.FovX / 2;
			double hFovY = Camera.FovY / 2;
			double4x4 rotX = double4x4.RotX ( hFovY );
			double4x4 rotMinusX = double4x4.RotX ( -hFovY );
			double4x4 rotY = double4x4.RotY ( hFovX );
			double4x4 rotMinusY = double4x4.RotY ( -hFovX );
			double3 left = rotMinusY.Transform ( double3.UnitZ );
			double3 top = rotMinusX.Transform ( double3.UnitZ );
			double3 right = rotY.Transform ( double3.UnitZ );
			double3 bottom = rotX.Transform ( double3.UnitZ );
			double3 topLeft = Camera.ViewInvMatrix.Transform ( top + left );
			double3 topRight = Camera.ViewInvMatrix.Transform ( top + right );
			double3 bottomLeft = Camera.ViewInvMatrix.Transform ( bottom + left );
			double3 bottomRight = Camera.ViewInvMatrix.Transform ( bottom + right );
			double3 stepX = ( topRight - topLeft ) * ( 1.0 / ViewportSize.width );
			double3 stepY = ( bottomLeft - topLeft ) * ( 1.0 / ViewportSize.height );
			double3 hStepX = stepX * 0.5;
			double3 hStepY = stepY * 0.5;
			double3 startY = topLeft + hStepX + hStepY;
			Scene.NullColor = ClearColor;

			ParallelOptions parallelOpts = new ParallelOptions ();
			parallelOpts.MaxDegreeOfParallelism = MaxDegreeOfParallelism.HasValue ? MaxDegreeOfParallelism.Value : Environment.ProcessorCount;

			Parallel.For ( 0, ViewportSize.height, parallelOpts, y => {
				double3 pX = startY + stepY * y;

				for ( int x = 0 ; x < ViewportSize.width ; x++, pX += stepX ) {
					Ray r = new Ray ( Camera.Pos, pX.Normalized );
					image.Values [x, y] = Scene.Trace ( r, new TraceData ( InitTraceData ) );
				}
			} );

			sw.Stop ();
			LastRenderTime = sw.Elapsed;
		}
		#endregion Methods
	}
}
