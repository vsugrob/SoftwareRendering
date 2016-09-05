using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RayTrace;
using Math3d;
using Common3d;
using System.Windows.Threading;
using System.Diagnostics;
using IO = System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace RayTraceWpfUi {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow :Window {
		Scene scene;
		ReflectiveMaterial reflectiveMaterial = new ReflectiveMaterial ();
		RefractiveMaterial refractiveMaterial = new RefractiveMaterial ( 1, 1.342 );
		RefractiveDispersiveMaterial refractiveDispersiveMaterial = RefractiveDispersiveMaterial.Water ();
		Camera camera;
		double3 sphere1Center;
		Sphere sphere1, sphere2;
		PlanarSurface floor, leftWall, rightWall, frontWall, backWall, ceiling;
		SpotLight spotLight1, spotLight2;
		RayTracer tracer;
		EyeBasedRayTracer eyeBasedTracer;
		SectionRayTracer sectionTracer;
		DispatcherTimer timer = new DispatcherTimer ();
		WriteableBitmap bmp;
		bool continuousRendering = false;
		int numFramesRendered = 0;
		CancellationTokenSource cts = new CancellationTokenSource ();
		Task renderTask;
		TimeSpan lastPresentTime = TimeSpan.Zero;

		bool scaleColorRange = false;

		public MainWindow () {
			InitializeComponent ();
		}

		private void Window_Loaded ( object sender, RoutedEventArgs e ) {
			scene = new Scene ();
			scene.AmbientColor = double3.Zero;

			sphere1Center = new double3 ( -3, 0, 10 );
			sphere1 = new Sphere ( 2, sphere1Center );
			//sphere1.Material = refractiveDispersiveMaterial;
			//sphere1.Material = new DebugMaterial ();
			//sphere1.Material = new MappedMaterial ( Texture2d.Load ( @"C:\media\images\Wallpaper\Текстуры\Minerals\AZURITE2.JPG" ), FilteringType.Nearest );
			//sphere1.Material = new MappedMaterial ( Texture2d.Load ( @"C:\media\images\earth2.png" ), TextureWrap.Clamp );
			sphere1.Material = RefractiveDispersiveMaterial.Diamond ();
			//sphere1.Material = new RefractiveDispersiveMaterial ( RefractionIndex.Water ( Light.RedWavelength ),
			//    RefractionIndex.Water ( Light.GreenWavelength ), RefractionIndex.Water ( Light.BlueWavelength ), 0.9 );
			//sphere1.Material = new RefractiveDispersiveMaterial ( new RefractionIndex ( 1.1 ),
			//    new RefractionIndex ( 1.5 ), new RefractionIndex ( 2 ) );
			//sphere2 = new Sphere ( 2, new double3 ( 2, 2, 12 ) );
			//sphere2.Material = reflectiveMaterial;			
			scene.Objects.Add ( sphere1 );
			//scene.Objects.Add ( sphere2 );
			//scene.Objects.Add ( sphere3 );

			// Lens test
			Sphere backSphere = new Sphere ( 2, new double3 ( -3, 0, 15 ) );
			backSphere.Material = reflectiveMaterial;

			CsgObject convexConcaveLens = new CsgObject ( CsgOp.Subtract,
				new Sphere ( 2, new double3 ( 0, 0, 7 ) ),
				new Sphere ( 6.5, new double3 ( 0, 0, 12 ) )
			);
			convexConcaveLens.Material = new RefractiveMaterial ( 1, 1.7 );

			CsgObject concaveConvexLens = new CsgObject ( CsgOp.Subtract,
				new Sphere ( 2, new double3 ( 0, 0, 7 ) ),
				new Sphere ( 6.5, new double3 ( 0, 0, 2 ) )
			);
			concaveConvexLens.Material = new RefractiveMaterial ( 1, 1.7 );

			//scene.Objects.Add ( backSphere );
			//scene.Objects.Add ( convexConcaveLens );
			//scene.Objects.Add ( concaveConvexLens );

			// Csg Subtraction test
			//sphere1 = new Sphere ( 2, sphere1Center );
			//sphere2 = new Sphere ( 2, sphere1Center + double3.UnitX * 2 );
			//Sphere sphere3 = new Sphere ( 2, sphere1Center - double3.UnitZ * 3 );
			//CsgObject csgObj = new CsgObject ( CsgOp.Subtract, sphere1, sphere2, sphere3 );
			//csgObj.Material = new PhongEnvironmentMaterial ( Colors.Orange.ToDouble3 () );
			//scene.Objects.Add ( csgObj );

			// Csg Intersection test
			//sphere1 = new Sphere ( 2, sphere1Center );
			//sphere2 = new Sphere ( 2, sphere1Center + double3.UnitX * 1 );
			//Sphere sphere3 = new Sphere ( 2, sphere1Center + double3.UnitY * 2 );
			//CsgObject csgObj = new CsgObject ( CsgOp.Intersect, sphere1, sphere2, sphere3 );
			//csgObj.Material = new PhongEnvironmentMaterial ( Colors.Orange.ToDouble3 () );
			//scene.Objects.Add ( csgObj );

			// Csg Subtraction and Intersection test
			//sphere1 = new Sphere ( 1.5, sphere1Center - double3.UnitX * 0.75 );
			//sphere2 = new Sphere ( 1.5, sphere1Center + double3.UnitX * 0.75 );
			//CsgObject csgObjIsec = new CsgObject ( CsgOp.Intersect, sphere1, sphere2 );
			////csgObjIsec.Material = new PhongEnvironmentMaterial ( Colors.Orange.ToDouble3 () );
			////scene.Objects.Add ( csgObjIsec );
			//Sphere sphere3 = new Sphere ( 2, sphere1Center + double3.UnitZ );
			//CsgObject csgObjSub = new CsgObject ( CsgOp.Subtract, sphere3, csgObjIsec );
			//csgObjSub.Material = new PhongEnvironmentMaterial ( Colors.Orange.ToDouble3 () );
			//scene.Objects.Add ( csgObjSub );

			// Csg Union test
			//sphere1 = new Sphere ( 2, sphere1Center );
			//sphere2 = new Sphere ( 2, sphere1Center + double3.UnitX * 2 );
			//CsgObject csgObjUnion = new CsgObject ( CsgOp.Union, sphere1, sphere2 );
			//csgObjUnion.Material = new PhongEnvironmentMaterial ( Colors.Violet.ToDouble3 () );
			//scene.Objects.Add ( csgObjUnion );

			// Csg Subtract Unions test
			//sphere1 = new Sphere ( 2, sphere1Center );
			//sphere2 = new Sphere ( 2, sphere1Center + double3.UnitX * 2 );
			//CsgObject csgObjUnion1 = new CsgObject ( CsgOp.Union, sphere1, sphere2 );
			//Sphere sphere3 = new Sphere ( 2, sphere1Center + double3.UnitY * 1 - double3.UnitZ );
			//Sphere sphere4 = new Sphere ( 2, sphere1Center + double3.UnitX * 2 + double3.UnitY * 1 - double3.UnitZ );
			//CsgObject csgObjUnion2 = new CsgObject ( CsgOp.Union, sphere3, sphere4 );
			//CsgObject csgObjSub = new CsgObject ( CsgOp.Subtract, csgObjUnion1, csgObjUnion2 );
			//csgObjSub.Material = new PhongEnvironmentMaterial ( Colors.Violet.ToDouble3 () );
			//scene.Objects.Add ( csgObjSub );

			floor = new PlanarSurface ( new double3 ( 0, -2, 0 ), double3.UnitY );
			//floor = new PlanarSurface ( new double3 ( 0, -2, 0 ), new double3 ( 0.2, 1, -0.3 ).Normalized );
			floor.Material = new DebugMaterial ();
			//floor.Material = new MappedMaterial ( Texture2d.Load ( @"C:\media\images\Wallpaper\Текстуры\Architectual\DIRTCHEK.JPG" ), TextureWrap.Repeat );
			floor.Material = new PhongEnvironmentMaterial ();
			leftWall = new PlanarSurface ( new double3 ( -10, 0, 0 ), double3.UnitX );
			//leftWall.Material = new CheckerMaterial ();
			//leftWall.Material = new PhongEnvironmentMaterial ( new double3 ( 1, 0, 0 ) );
			leftWall.Material = new PhongMaterial ( new double3 ( 1, 0, 0 ) );
			rightWall = new PlanarSurface ( new double3 ( 10, 0, 0 ), -double3.UnitX );
			//rightWall.Material = new PhongEnvironmentMaterial ( new double3 ( 0, 0.27, 1 ) );
			rightWall.Material = new PhongMaterial ( new double3 ( 0, 0.27, 1 ) );
			frontWall = new PlanarSurface ( new double3 ( 0, 0, 30 ), -double3.UnitZ );
			//frontWall.Material = new PhongEnvironmentMaterial ( new double3 ( 0.6, 0.27, 1 ) );
			frontWall.Material = new PhongMaterial ( new double3 ( 0.6, 0.27, 1 ) );
			backWall = new PlanarSurface ( new double3 ( 0, 0, -60 ), double3.UnitZ );
			//backWall.Material = new PhongEnvironmentMaterial ( new double3 ( 0.6, 0.6, 0.27 ) );
			backWall.Material = new PhongMaterial ( new double3 ( 0.6, 0.6, 0.27 ) );
			ceiling = new PlanarSurface ( new double3 ( 0, 10, 0 ), -double3.UnitY );
			//ceiling = new PlanarSurface ( new double3 ( 0, 10, 0 ), new double3 ( 0, -1, 0.02 ).Normalized );
			//ceiling.Material = new CheckerMaterial ();
			//ceiling.Material = new PhongEnvironmentMaterial ( new double3 ( 0.27, 1, 0.27 ) );
			ceiling.Material = new PhongMaterial ( new double3 ( 0.27, 1, 0.27 ) );
			scene.Objects.Add ( floor );
			scene.Objects.Add ( leftWall );
			scene.Objects.Add ( rightWall );
			scene.Objects.Add ( frontWall );
			scene.Objects.Add ( backWall );
			scene.Objects.Add ( ceiling );

			spotLight1 = new SpotLight ( new double3 ( -2, 4, 0 ), new double3 ( 1, 0.7, 0.3 ), new double3 ( 1, 0.7, 0.3 ), 5 );
			spotLight2 = new SpotLight ( new double3 ( 5, 3, 8 ), new double3 ( 0.3, 0.7, 1 ), new double3 ( 0.3, 0.7, 1 ), 5 );
			scene.Lights.Add ( spotLight1 );
			scene.Lights.Add ( spotLight2 );

			camera = new Camera ( new double3 ( 0, 0, 3 ) );
			camera.Pos += double4.UnitZ * 9;
			//camera.Pos += double4.UnitZ * 5 - double4.UnitX * 8 - double4.UnitY * 1.5;
			//camera.View = new double3 ( -2, 0, 10 ) - camera.Pos;
			camera.View = sphere1Center - camera.Pos;
			//camera.FovY = 140;

			eyeBasedTracer = new EyeBasedRayTracer ();
			eyeBasedTracer.InitTraceData = new TraceData ( 10, 10 );
			//eyeBasedTracer.ClearColor = double3.UnitY;
			eyeBasedTracer.Scene = scene;
			eyeBasedTracer.ViewportSize = new IntSize ( 400, 300 );
			//eyeBasedTracer.ViewportSize = new IntSize ( 1024, 768 );
			//eyeBasedTracer.ViewportSize = new IntSize ( 1920, 1080 );
			eyeBasedTracer.Camera = camera;

			sectionTracer = new SectionRayTracer ();
			sectionTracer.Scene = scene;
			sectionTracer.ViewportSize = new IntSize ( 400, 300 );
			//sectionTracer.ViewportSize = new IntSize ( 1024, 768 );
			//sectionTracer.ViewportSize = new IntSize ( 1920, 1080 );
			double side = 10;
			sectionTracer.SectionSize = new DoubleSize ( side * sectionTracer.ViewportSize.width / sectionTracer.ViewportSize.height, side );
			sectionTracer.Camera = camera;
			//camera.FovY = 50;
			sectionTracer.NumRaysToDraw = 300;
			//sectionTracer.Orientation = SectionOrientation.Vertical;

			tracer = eyeBasedTracer;
			//tracer = sectionTracer;
			//tracer.MaxDegreeOfParallelism = 1;
			tracer.InitTraceData = new TraceData ( 30, 30 );

			Render ();

			timer.Interval = TimeSpan.FromSeconds ( 2.5 );
			timer.Tick += new EventHandler ( timer_Tick );
		}

		private void Render () {
			RenderTime.Text = "...";

			if ( bmp == null ||
				bmp.Width  != tracer.ViewportSize.width ||
				bmp.Height != tracer.ViewportSize.height )
			{
				bmp = new WriteableBitmap ( tracer.ViewportSize.width, tracer.ViewportSize.height,
					96, 96, PixelFormats.Bgr32, null );
				RenderedImage.Source = bmp;

				camera.FovX = camera.FovY * tracer.ViewportSize.width / tracer.ViewportSize.height;
				sectionTracer.SectionSize =
					new DoubleSize ( sectionTracer.SectionSize.height * sectionTracer.ViewportSize.width / sectionTracer.ViewportSize.height,
						sectionTracer.SectionSize.height );
			}

			Action renderAction = () => {
				TraceData.ReflectionLimitExceedCount = 0;
				TraceData.RefractionLimitExceedCount = 0;
				tracer.Render ();
				numFramesRendered++;

				RenderTime.Dispatcher.Invoke ( new Action ( () => {
					RenderTime.Text = tracer.LastRenderTime.ToString ();

					if ( TraceData.ReflectionLimitExceedCount > 0 )
						Console.WriteLine ( "Reflection limit exceed {0} times", TraceData.ReflectionLimitExceedCount );

					if ( TraceData.RefractionLimitExceedCount > 0 )
						Console.WriteLine ( "Refraction limit exceed {0} times", TraceData.RefractionLimitExceedCount );
				} ) );
			};

			Action <Task> presentAction = task => {
				Present ();
			};
			
			if ( continuousRendering ) {
				Task.Factory.StartNew ( () => {
					while ( !cts.IsCancellationRequested ) {
						renderAction ();
						presentAction ( null );
						//camera.Pos -= new double4 ( camera.View * 0.1, 1 );
						//camera.Pos += new double4 ( 0, 0, 0.25, 0 );
						//camera.Pos += new double4 ( camera.View, 0 );
						//floor.Plane.n = new double3 ( Math.Sin ( Math.PI * numFramesRendered * 5 / 180 ) / 4, 1, Math.Cos ( Math.PI * numFramesRendered * 5 / 180 ) / 4 ).Normalized;
					}
				}, cts.Token );
			} else {
				renderTask = Task.Factory.StartNew ( renderAction );
				renderTask.ContinueWith ( presentAction );
				timer.Start ();
			}
		}

		void timer_Tick ( object sender, EventArgs e ) {
			if ( renderTask.Status == TaskStatus.Running )
				Present ();
			else
				timer.Stop ();

			double newPresentInterval = ( timer.Interval.TotalSeconds + lastPresentTime.TotalSeconds * 10 ) / 2;
			Console.WriteLine ( "Present interval is now {0} sec", newPresentInterval );
			timer.Interval = TimeSpan.FromSeconds ( newPresentInterval );
		}

		void Present () {
			if ( tracer.LastRenderImage == null || tracer.LastRenderImage.Values == null )
				return;

			bmp.Dispatcher.Invoke ( new Action ( () => {
				Stopwatch sw = Stopwatch.StartNew ();
				bmp.Lock ();
				
				unsafe {
					if ( scaleColorRange ) {
						double maxLen = 0;

						for ( int y = 0 ; y < tracer.ViewportSize.height ; y++ ) {
							for ( int x = 0 ; x < tracer.ViewportSize.width ; x++ ) {
								double len = tracer.LastRenderImage.Values [x, y].Length;

								if ( len > maxLen )
									maxLen = len;
							}
						}

						double maxLenInv = 1 / maxLen;

						for ( int y = 0 ; y < tracer.ViewportSize.height ; y++ ) {
							for ( int x = 0 ; x < tracer.ViewportSize.width ; x++ ) {
								double3 color = tracer.LastRenderImage.Values [x, y] * maxLenInv * 255;
								int r = Math.Max ( 0, Math.Min ( 255, ( int ) color.r ) );
								int g = Math.Max ( 0, Math.Min ( 255, ( int ) color.g ) );
								int b = Math.Max ( 0, Math.Min ( 255, ( int ) color.b ) );
								int intColor = b | ( g << 8 ) | ( r << 16 );

								*( ( ( int* ) bmp.BackBuffer ) + y * tracer.ViewportSize.width + x ) = intColor;
							}
						}
					} else {
						for ( int y = 0 ; y < tracer.ViewportSize.height ; y++ ) {
							for ( int x = 0 ; x < tracer.ViewportSize.width ; x++ ) {
								double3 color = tracer.LastRenderImage.Values [x, y] * 255;
								int r = Math.Max ( 0, Math.Min ( 255, color.r.PreciseRound () ) );
								int g = Math.Max ( 0, Math.Min ( 255, color.g.PreciseRound () ) );
								int b = Math.Max ( 0, Math.Min ( 255, color.b.PreciseRound () ) );
								int intColor = b | ( g << 8 ) | ( r << 16 );

								*( ( ( int* ) bmp.BackBuffer ) + y * tracer.ViewportSize.width + x ) = intColor;
							}
						}
					}
				}

				bmp.AddDirtyRect ( new Int32Rect ( 0, 0, tracer.ViewportSize.width, tracer.ViewportSize.height ) );
				bmp.Unlock ();
				sw.Stop ();
				Console.WriteLine ( "Present time: {0}", sw.Elapsed );
				lastPresentTime = sw.Elapsed;

				if ( continuousRendering || renderTask.IsCompleted ) {
					if ( !IO.Directory.Exists ( "renders" ) )
						IO.Directory.CreateDirectory ( "renders" );

					using ( IO.FileStream fs = IO.File.OpenWrite ( string.Format ( @"renders\{0}.bmp", DateTime.Now.Ticks ) ) ) {
						BmpBitmapEncoder bmpEncoder = new BmpBitmapEncoder ();
						bmpEncoder.Frames.Add ( BitmapFrame.Create ( bmp ) );
						bmpEncoder.Save ( fs );
					};
				}
			} ) );
		}

		private void Render400x300_Click ( object sender, RoutedEventArgs e ) {
			tracer.ViewportSize = new IntSize ( 400, 300 );
			Render ();
		}

		private void Render1920x1080_Click ( object sender, RoutedEventArgs e ) {
			tracer.ViewportSize = new IntSize ( 1920, 1080 );
			Render ();
		}
	}
}
