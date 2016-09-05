using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common3d;
using Math3d;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RayTrace {
	public enum SectionOrientation {
		Horizontal, Vertical
	}

	public class SectionRayTracer : RayTracer {
		#region Properties
		public DoubleSize SectionSize { get; set; }
		public double3 EdgeColor { get; set; }
		public SectionOrientation Orientation { get; set; }
		public bool ShowRays { get; set; }
		public int? NumRaysToDraw { get; set; }
		public bool AlwaysTakeVerticalFov { get; set; }
		#endregion Properties

		#region Constructors
		public SectionRayTracer () {
			this.InitTraceData = new TraceData ( 10, 10 );
			this.ClearColor = new double3 ( 1, 1, 1 );
			this.EdgeColor = double3.Zero;
			this.Orientation = SectionOrientation.Horizontal;
			this.ShowRays = true;
			this.AlwaysTakeVerticalFov = true;
		}
		#endregion Constructors

		#region Methods
		public override void Render () {
			Stopwatch sw = Stopwatch.StartNew ();
			AllocateImage ();

			Scene.NullColor = ClearColor;
			double3 xAxis = Camera.View;
			double3 yAxis = Orientation == SectionOrientation.Horizontal ? Camera.Right : -Camera.Up;
			double3 sideHalf = yAxis * SectionSize.height / 2;
			double3 topLeft = Camera.Pos - sideHalf;
			double3 bottomLeft = Camera.Pos + sideHalf;

			RenderEdges ( xAxis, yAxis, topLeft, bottomLeft );

			if ( ShowRays )
				RenderRays ( xAxis, yAxis, topLeft, bottomLeft );

			sw.Stop ();
			LastRenderTime = sw.Elapsed;
		}

		public void RenderEdges ( double3 xAxis, double3 yAxis, double3 topLeft, double3 bottomLeft ) {
			double3 topRight = topLeft + xAxis * SectionSize.width;
			double3 stepX = ( topRight - topLeft ) * ( 1.0 / ViewportSize.width );
			double3 stepY = ( bottomLeft - topLeft ) * ( 1.0 / ViewportSize.height );
			double3 hStepX = stepX * 0.5;
			double3 hStepY = stepY * 0.5;
			double3 startX = topLeft + hStepX;
			double3 startY = topLeft + hStepY;

			Task xTask = Task.Factory.StartNew ( () => {
				for ( int x = 0 ; x < ViewportSize.width ; x++ ) {
					Ray ray = new Ray ( startX + stepX * x, yAxis );

					List <IntersectData> isecs = Scene.Intersect ( ray );

					if ( isecs.Count > 0 ) {
						foreach ( IntersectData isecData in isecs ) {
							double3 pV = isecData.P - topLeft;
							double2 p = new double2 ( pV & xAxis, pV & yAxis );

							if ( p.y >= SectionSize.height )
								continue;

							int viewportX = ( int ) ( ViewportSize.width * ( p.x / SectionSize.width ) );
							int viewportY = ( int ) ( ViewportSize.height * ( p.y / SectionSize.height ) );
							image.Values [viewportX, viewportY] = EdgeColor;
						}
					}
				}
			}, TaskCreationOptions.LongRunning );

			Task yTask = Task.Factory.StartNew ( () => {
				for ( int y = 0 ; y < ViewportSize.height ; y++ ) {
					Ray ray = new Ray ( startY + stepY * y, xAxis );

					List <IntersectData> isecs = Scene.Intersect ( ray );

					if ( isecs.Count > 0 ) {
						foreach ( IntersectData isecData in isecs ) {
							double3 pV = isecData.P - topLeft;
							double2 p = new double2 ( pV & xAxis, pV & yAxis );

							if ( p.x >= SectionSize.width )
								continue;

							int viewportX = ( int ) ( ViewportSize.width * ( p.x / SectionSize.width ) );
							int viewportY = ( int ) ( ViewportSize.height * ( p.y / SectionSize.height ) );
							image.Values [viewportX, viewportY] = EdgeColor;
						}
					}
				}
			}, TaskCreationOptions.LongRunning );

			Task.WaitAll ( xTask, yTask );
		}

		public void RenderRays ( double3 xAxis, double3 yAxis, double3 topLeft, double3 bottomLeft ) {
			int numRays = NumRaysToDraw.HasValue ? NumRaysToDraw.Value : ViewportSize.height;
			int maxRays = Math.Max ( numRays, ViewportSize.height );
			double3 startP, step;

			if ( Orientation == SectionOrientation.Horizontal ) {
				double hFovX = ( AlwaysTakeVerticalFov ? Camera.FovY : Camera.FovX ) / 2;
				double4x4 rotY = double4x4.RotY ( hFovX );
				double4x4 rotMinusY = double4x4.RotY ( -hFovX );
				double3 left = Camera.ViewInvMatrix.Transform ( rotMinusY.Transform ( double3.UnitZ ) );
				double3 right = Camera.ViewInvMatrix.Transform ( rotY.Transform ( double3.UnitZ ) );
				step = ( right - left ) * ( 1.0 / maxRays );
				startP = left + step * 0.5;
			} else {
				double hFovY = Camera.FovY / 2;
				double4x4 rotX = double4x4.RotX ( hFovY );
				double4x4 rotMinusX = double4x4.RotX ( -hFovY );
				double3 top = Camera.ViewInvMatrix.Transform ( rotMinusX.Transform ( double3.UnitZ ) );
				double3 bottom = Camera.ViewInvMatrix.Transform ( rotX.Transform ( double3.UnitZ ) );
				step = ( bottom - top ) * ( 1.0 / maxRays );
				startP = top + step * 0.5;
			}

			Scene.TraceCallback = tr => {
				double sx = ViewportSize.width  / SectionSize.width;
				double sy = ViewportSize.height / SectionSize.height;
				double3 pV0 = tr.Ray.p - topLeft;
				double3 pV1 = tr.NearestIntersect.P - topLeft;
				int x0 = ( int ) Math.Round ( sx * ( pV0 & xAxis ) );
				int y0 = ( int ) Math.Round ( sy * ( pV0 & yAxis ) );
				int x1 = ( int ) Math.Round ( sx * ( pV1 & xAxis ) );
				int y1 = ( int ) Math.Round ( sy * ( pV1 & yAxis ) );

				if ( Math3.ClampLine ( ref x0, ref y0, ref x1, ref y1, ViewportSize.width, ViewportSize.height ) ) {
					image.DrawLineBresenhamClamped ( x0, y0, x1, y1, tr.Color );
					//image.Values [x0, y0] = 1 - tr.Color;
					image.Values [x1, y1] = new double3 ( 1, 1, 0 );
				}
			};

			if ( numRays == 1 ) {
				double3 p = startP + step * maxRays * 0.5;
				Ray r = new Ray ( Camera.Pos, p.Normalized );
				Scene.Trace ( r, new TraceData ( InitTraceData ) );
			} else {
				ParallelOptions parallelOpts = new ParallelOptions ();
				parallelOpts.MaxDegreeOfParallelism = MaxDegreeOfParallelism.HasValue ? MaxDegreeOfParallelism.Value : Environment.ProcessorCount;
				double xInc = ( double ) maxRays / ( numRays - 1 );
				
				Parallel.For ( 0, numRays, parallelOpts, x => {
					double3 p = startP + step * x * xInc;
					Ray r = new Ray ( Camera.Pos, p.Normalized );
					Scene.Trace ( r, new TraceData ( InitTraceData ) );
				} );
			}
		}
		#endregion Methods
	}
}
