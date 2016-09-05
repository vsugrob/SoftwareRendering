using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common3d;
using Math3d;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RayTrace {
	public abstract class RayTracer {
		#region Properties
		public Scene Scene { get; set; }
		public TraceData InitTraceData { get; set; }
		public IntSize ViewportSize { get; set; }
		public double3 ClearColor { get; set; }
		public Camera Camera { get; set; }
		public int? MaxDegreeOfParallelism { get; set; }

		protected HdrBuffer image;
		public HdrBuffer LastRenderImage { get { return	image; } }
		public TimeSpan LastRenderTime { get; protected set; }
		#endregion Properties

		#region Methods
		protected void AllocateImage () {
			if ( image == null || image.Size != ViewportSize ) {
				image = new HdrBuffer ( ViewportSize );

				if ( ClearColor != 0 )
					image.SetValues ( ClearColor );
			} else
				image.SetValues ( ClearColor );
		}

		public abstract void Render ();
		#endregion Methods
	}
}
