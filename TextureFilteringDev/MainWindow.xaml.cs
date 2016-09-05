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
using Math3d;
using Common3d;
using System.Diagnostics;
using System.Windows.Threading;
using IO = System.IO;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace TextureFilteringDev {
	public enum CycleDir { Cw, Ccw }
	public enum Culling { None, Cw, Ccw }

	public struct VertexPT {
		public double4 P;
		public double2 T;

		public VertexPT ( double4 p, double2 t ) {
			this.P = p;
			this.T = t;
		}
	}

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow :Window {
		VertexPT [] figureTriStrip;	// draw this using triangle strip

		double4 [] figureVertices;		// draw this using indices
		int [] figureIndices;
		double2 [] figureTexCoords;

		double3 cameraPos = -double3.UnitZ * 5;
		double cameraAngleY = 0, cameraAngleX = 0, cameraAngleZ = 0;
		double nearZ = 0.001;
		double4x4 viewMatrix = double4x4.Identity;
		double modelAngleY = -15, modelAngleX = -30, modelAngleZ = 0;
		double4x4 modelMatrix = double4x4.Identity;
		bool isRotating = false;
		double mouseXRatio, mouseYRatio;
		Culling culling = Culling.None;
		bool initialized = false;	// Output buffer is ready

		HdrBuffer image;
		WriteableBitmap bmp;
		IntSize size;
		double3 clearColor = new double3 ( 1, 1, 1 );
		Sampler2d sampler;

		DispatcherTimer resizeTimer = new DispatcherTimer ();

		Regex textureRegex = new Regex ( @"\.(?:jpg|jpeg|bmp|png|gif)$", RegexOptions.IgnoreCase );
		string texturesDir = "";
		IO.FileSystemWatcher fsWatcher;
		public ObservableCollection <TextureEntry> Textures { get; set; }

		static void GenCubeUsingIndices ( out double4 [] vertices, out int [] indices, out double2 [] texCoords ) {
			indices = new [] {
				0, 1, 2,  0, 2, 3,	// Front face
				7, 6, 5,  7, 5, 4,	// Back face
				3, 2, 6,  3, 6, 7,	// Right face
				4, 5, 1,  4, 1, 0,	// Left face
				1, 5, 6,  1, 6, 2,	// Top face
				4, 0, 3,  4, 3, 7	// Bottom face
			};

			vertices = new [] {
				// Front quad
				new double4 ( -5, -1, -1, 1 ),
				new double4 ( -5,  1, -1, 1 ),
				new double4 (  5,  1, -1, 1 ),
				new double4 (  5, -1, -1, 1 ),

				// Back quad
				new double4 ( -5, -1, 9, 1 ),
				new double4 ( -5,  1, 9, 1 ),
				new double4 (  5,  1, 9, 1 ),
				new double4 (  5, -1, 9, 1 ),
			};

			texCoords = new [] {
				// Front face
				new double2 ( 0, 0 ), new double2 ( 0, 2 ), new double2 ( 10, 2 ),
				new double2 ( 0, 0 ), new double2 ( 10, 2 ), new double2 ( 10, 0 ),
				// Back face
				new double2 ( 0, 0 ), new double2 ( 0, 1 ), new double2 ( 1, 1 ),
				new double2 ( 0, 0 ), new double2 ( 1, 1 ), new double2 ( 1, 0 ),
				// Right face
				new double2 ( 1, 0 ), new double2 ( 1, 1 ), new double2 ( 0, 1 ),
				new double2 ( 1, 0 ), new double2 ( 0, 1 ), new double2 ( 0, 0 ),
				// Left face
				new double2 ( 1, 0 ), new double2 ( 1, 1 ), new double2 ( 0, 1 ),
				new double2 ( 1, 0 ), new double2 ( 0, 1 ), new double2 ( 0, 0 ),
				// Top face
				new double2 ( 0, 0 ), new double2 ( 0, 10 ), new double2 ( 10, 10 ),
				new double2 ( 0, 0 ), new double2 ( 10, 10 ), new double2 ( 10, 0 ),
				// Bottom face
				new double2 ( 0, 0 ), new double2 ( 0, 1 ), new double2 ( 1, 1 ),
				new double2 ( 0, 0 ), new double2 ( 1, 1 ), new double2 ( 1, 0 ),
			};
		}

		static VertexPT [] GenQuadUsingTriangleStrip () {
			return	new VertexPT [] {
				new VertexPT ( new double4 ( -1, -1, 0, 1 ), new double2 ( 0, 0 ) ),
				new VertexPT ( new double4 ( -1,  1, 0, 1 ), new double2 ( 0, 1 ) ),
				new VertexPT ( new double4 (  5, -1, 0, 1 ), new double2 ( 1, 0 ) ),
				new VertexPT ( new double4 (  5,  1, 0, 1 ), new double2 ( 1, 1 ) ),

				//new VertexPT ( new double4 ( -1, -1, 0, 1 ), new double2 ( 0, 0 ) ),
				//new VertexPT ( new double4 ( -1,  1, 0, 1 ), new double2 ( 0, 2 ) ),
				//new VertexPT ( new double4 (  1, -1, 0, 1 ), new double2 ( 2, 0 ) ),
				//new VertexPT ( new double4 (  1,  1, 0, 1 ), new double2 ( 2, 2 ) ),

				//new VertexPT ( new double4 ( -1, -1, 0, 1 ), new double2 ( -1, -1 ) ),
				//new VertexPT ( new double4 ( -1,  1, 0, 1 ), new double2 ( -1,  1 ) ),
				//new VertexPT ( new double4 (  1, -1, 0, 1 ), new double2 (  1, -1 ) ),
				//new VertexPT ( new double4 (  1,  1, 0, 1 ), new double2 (  1,  1 ) ),

				//new VertexPT ( new double4 ( -1, -1, 0, 1 ), new double2 ( 0, 0 ) ),
				//new VertexPT ( new double4 ( -1,  1, 0, 1 ), new double2 ( 0, 0.5 ) ),
				//new VertexPT ( new double4 (  1, -1, 0, 1 ), new double2 ( 0.5, 0 ) ),
				//new VertexPT ( new double4 (  1,  1, 0, 1 ), new double2 ( 0.5, 0.5 ) ),
			};
		}

		public MainWindow () {
			string devTexturesDir = @"..\..\Textures";

			if ( IO.Directory.Exists ( "Textures" ) )
				texturesDir = "Textures";
			else if ( IO.Directory.Exists ( devTexturesDir ) )
				texturesDir = devTexturesDir;
			else {
				IO.Directory.CreateDirectory ( "Textures" );
				texturesDir = "Textures";
			}
			
			Textures = new ObservableCollection <TextureEntry> ();
			LoadTextures ();
			fsWatcher = new IO.FileSystemWatcher ( texturesDir );
			fsWatcher.Created += new IO.FileSystemEventHandler ( fsWatcher_Created );
			fsWatcher.EnableRaisingEvents = true;

			Map2d map = Texture2d.Load ( IO.Path.Combine ( texturesDir, "checker_small.bmp" ) );
			//Map2d map = Texture2d.Load ( IO.Path.Combine ( texturesDir, "checker_small_2x2.bmp" ) );
			//Map2d map = Texture2d.Load ( IO.Path.Combine ( texturesDir, "checker.jpg" );
			//Map2d map = Texture2d.Load ( @"D:\Programming\Projects\msvs\SoftwareRendering\TextureFilteringDev\lines.bmp" );
			//Map2d map = Texture2d.Load ( IO.Path.Combine ( texturesDir, "checker_colored.jpg" ) );
			//Map2d map = Texture2d.Load ( @"C:\media\images\Wallpaper\Текстуры\Architectual\DRTCHEK1.JPG" );
			//Map2d map = Texture2d.Load ( @"C:\media\images\keira knightley\marso-uwi_bI.jpg" );
			//Map2d map = Texture2d.Load ( @"C:\media\images\my renders\my first raytracer\refraction_fixed_water_1.342.bmp" );
			//Map2d map = Texture2d.Load ( IO.Path.Combine ( texturesDir, "alena_zayac.jpg" ) );
			sampler = new Sampler2d ( map, TextureWrap.Clamp, FilteringType.Nearest );

			InitializeComponent ();

			mouseXRatio = 360.0 / System.Windows.SystemParameters.PrimaryScreenWidth;
			mouseYRatio = 360.0 / System.Windows.SystemParameters.PrimaryScreenHeight;

			figureTriStrip = GenQuadUsingTriangleStrip ();
			GenCubeUsingIndices ( out figureVertices, out figureIndices, out figureTexCoords );

			resizeTimer.Interval = TimeSpan.FromSeconds ( 0.5 );
			resizeTimer.Tick += new EventHandler ( resizeTimer_Tick );
		}

		private void LoadTextures () {
			foreach ( string fileName in IO.Directory.EnumerateFiles ( texturesDir, "*", IO.SearchOption.AllDirectories ) ) {
				if ( !textureRegex.IsMatch ( fileName ) )
					continue;

				LoadTexture ( fileName );
			}
		}

		private void LoadTexture ( string fileName ) {
			TextureEntry tEntry = TextureEntry.Load ( fileName );

			if ( tEntry != null ) {
				this.Dispatcher.Invoke ( new Action ( () => {
					Textures.Add ( tEntry );
				} ) );
			}
		}

		void fsWatcher_Created ( object sender, IO.FileSystemEventArgs e ) {
			LoadTexture ( e.FullPath );
		}

		void resizeTimer_Tick ( object sender, EventArgs e ) {
			resizeTimer.Stop ();
			InitImage ();
		}

		private void Window_Loaded ( object sender, RoutedEventArgs e ) {
			FilteringTypeComboBox.SelectedIndex = 1;
			WrapModeComboBox.SelectedIndex = 0;
			CullingComboBox.SelectedIndex = 1;
			TexturesListBox.SelectedIndex = 0;
		}

		private void RenderedImageUnderlay_SizeChanged ( object sender, SizeChangedEventArgs e ) {
			if ( image == null )
				resizeTimer.Interval = TimeSpan.FromSeconds ( 0.1 );
			else
				resizeTimer.Interval = TimeSpan.FromSeconds ( 0.5 );
			
			if ( resizeTimer.IsEnabled )
				resizeTimer.Stop ();

			resizeTimer.Start ();
		}

		void InitImage () {
			int minSide = ( int ) Math.Min ( RenderedImageUnderlay.ActualWidth, RenderedImageUnderlay.ActualHeight );
			size = new IntSize ( minSide, minSide );
			image = new HdrBuffer ( size );
			bmp = new WriteableBitmap ( size.width, size.height,
					96, 96, PixelFormats.Bgr32, null );
			RenderedImage.Source = bmp;

			initialized = true;

			Render ();
		}

		void Render () {
			if ( !initialized )
				return;

			Clear ();
			//image.DrawLineBresenham ( -10, 10, 250, 100, double3.UnitX );
			//image.DrawLineBresenham ( -10, 11, 250, 101, double3.UnitY );
			//image.DrawXAlignedTriangle ( new double2 ( 100, 100 ), 50, 120, 200, double3.UnitX );

			//image.DrawTriangle ( new double2 [] { new double2 ( 100, 100 ), new double2 ( 50, 50 ), new double2 ( 170, 30 ) },
			//    double3.UnitX );

			//image.DrawTriangle ( new double2 [] { new double2 ( 100, -10 ), new double2 ( -30, 350 ), new double2 ( 470, 352 ) },
			//    double3.UnitX );

			//image.DrawLineBresenham ( 100, 50, 170, 51, double3.UnitY );
			
			viewMatrix = double4x4.Trans ( -cameraPos ) *
				double4x4.RotY ( -cameraAngleY ) *
				double4x4.RotX ( -cameraAngleX ) *
				double4x4.RotZ ( -cameraAngleZ );
			modelMatrix = double4x4.RotY ( modelAngleY ) *
				double4x4.RotX ( modelAngleX ) *
				double4x4.RotZ ( modelAngleZ );/* *
				double4x4.Trans ( double3.UnitZ * 2 );*/

			//DrawTriangleStrip ( figureTriStrip );
			DrawIndexedVertices ( figureVertices, figureIndices, figureTexCoords );

			double2 p0 = new double2 ( 117.225002862707, 318.097285672122 );
			double2 p1 = new double2 ( 89.8508890828692, 95.1732917403599 );

			//double2 p0 = new double2 ( 100, 103.5 );
			//double2 p1 = new double2 ( 103, 99.3 );

			//// Must plot same pixels but they dont <SOLVED>
			//image.DrawYSteppingLine ( p0, p1 );
			//image.DrawYSteppingLine ( p1, p0 );

			//// Third line plots extra points but it shouldnt <SOLVED>
			//double k = 0.5;
			//double2 mp = k.Lerp ( p0, p1 );
			//image.DrawYSteppingLine ( p0, p1 );
			//image.DrawYSteppingLine ( p0, mp );
			//image.DrawYSteppingLine ( mp, p1 );

			Present ();
		}

		VertexPT [] TransformVertices ( VertexPT [] vs, double4x4 m ) {
			VertexPT [] tVs = new VertexPT [vs.Length];

			for ( int i = 0 ; i < vs.Length ; i++ )
				tVs [i] = new VertexPT ( m.Transform ( vs [i].P ), vs [i].T );

			return	tVs;
		}

		double4 [] TransformVertices ( double4 [] vs, double4x4 m ) {
			double4 [] tVs = new double4 [vs.Length];

			for ( int i = 0 ; i < vs.Length ; i++ )
				tVs [i] = m.Transform ( vs [i] );

			return	tVs;
		}

		VertexPT [][] ClipTriangleByZ ( VertexPT [] vs, double z, int insideSign ) {
			// Warning: this algorithm generally does not preserve cw/ccw order
			Debug.Assert ( insideSign == 1 || insideSign == -1 );
			List <VertexPT> vsInside = new List <VertexPT> ( vs );
			List <VertexPT> vsOutside = new List <VertexPT> ();

			for ( int i = 0 ; i < vsInside.Count ; i++ ) {
				if ( vsInside [i].P.z.CompareTo ( z ) != insideSign ) {
					vsOutside.Add ( vsInside [i] );
					vsInside.RemoveAt ( i-- );
				}
			}

			if ( vsOutside.Count == 0 )
				return	new VertexPT [][] { ( VertexPT [] ) vs.Clone () };
			else if ( vsOutside.Count == 3 )
				return	new VertexPT [][] {};
			else if ( vsOutside.Count == 2 ) {
				VertexPT [] newVs = new VertexPT [3];
				newVs [0] = vsInside [0];

				double k1 = ( z - vsOutside [0].P.z ) / ( vsInside [0].P.z - vsOutside [0].P.z );
				double k2 = ( z - vsOutside [1].P.z ) / ( vsInside [0].P.z - vsOutside [1].P.z );

				newVs [1] = new VertexPT (
					k1.Lerp ( vsOutside [0].P, vsInside [0].P ),
					k1.Lerp ( vsOutside [0].T, vsInside [0].T )
				);

				newVs [2] = new VertexPT (
					k2.Lerp ( vsOutside [1].P, vsInside [0].P ),
					k2.Lerp ( vsOutside [1].T, vsInside [0].T )
				);

				return	new VertexPT [][] { newVs };
			} else if ( vsOutside.Count == 1 ) {
				double k1 = ( z - vsOutside [0].P.z ) / ( vsInside [0].P.z - vsOutside [0].P.z );
				double k2 = ( z - vsOutside [0].P.z ) / ( vsInside [1].P.z - vsOutside [0].P.z );

				VertexPT v1 = new VertexPT (
					k1.Lerp ( vsOutside [0].P, vsInside [0].P ),
					k1.Lerp ( vsOutside [0].T, vsInside [0].T )
				);

				VertexPT v2 = new VertexPT (
					k2.Lerp ( vsOutside [0].P, vsInside [1].P ),
					k2.Lerp ( vsOutside [0].T, vsInside [1].T )
				);

				return	new VertexPT [][] {
					new VertexPT [] { vsInside [0], v1, v2 },
					new VertexPT [] { vsInside [0], vsInside [1], v2 }
				};
			}

			throw new ArgumentException ( "Invalid vertex data." );
		}

		double3 GetPerspectiveTriangleNormal ( double3 [] pts, CycleDir cycleDir ) {
			for ( int i = 0 ; i < pts.Length ; i++ ) {
				double z = pts [i].z <= 0 ? 1e-5 : pts [i].z;
				pts [i].xy /= z;
			}

			if ( cycleDir == CycleDir.Cw )
				return	( ( pts [1] - pts [0] ) * ( pts [2] - pts [1] ) ).Normalized;
			else
				return	( ( pts [2] - pts [1] ) * ( pts [1] - pts [0] ) ).Normalized;
		}

		CycleDir GetPerspectiveTriangleOrder ( double3 [] pts ) {
			for ( int i = 0 ; i < pts.Length ; i++ ) {
				double z = pts [i].z <= 0 ? 1e-5 : pts [i].z;
				pts [i].xy /= z;
			}

			double signedArea = Math3.SignedArea2 ( pts [0].xy, pts [1].xy, pts [2].xy );

			return	signedArea >= 0 ? CycleDir.Ccw : CycleDir.Cw;
		}

		void DrawTriangleStrip ( VertexPT [] vs ) {
			int numTris = vs.Length - 2;
			double4x4 modelView = modelMatrix * viewMatrix;
			vs = TransformVertices ( vs, modelView );

			for ( int i = 0 ; i < numTris ; i++ ) {
				VertexPT [] triVs;

				if ( i % 2 == 0 )
					triVs = new [] { vs [i], vs [i + 1], vs [i + 2] };
				else
					triVs = new [] { vs [i], vs [i + 2], vs [i + 1] };

				DrawTriangle ( triVs );
			}
		}

		void DrawIndexedVertices ( double4 [] vs, int [] indices, double2 [] texCoords ) {
			Debug.Assert ( indices.Length % 3 == 0 && texCoords.Length == indices.Length );
			double4x4 modelView = modelMatrix * viewMatrix;
			vs = TransformVertices ( vs, modelView );

			for ( int i = 0 ; i < indices.Length ; i += 3 ) {
				VertexPT [] triVs = new [] {
					new VertexPT ( vs [indices [i]], texCoords [i] ),
					new VertexPT ( vs [indices [i + 1]], texCoords [i + 1] ),
					new VertexPT ( vs [indices [i + 2]], texCoords [i + 2] )
				};

				DrawTriangle ( triVs );
			}
		}

		private void DrawTriangle ( VertexPT [] triVs ) {
		    if ( culling != Culling.None ) {
				// Cull by normal
		        //double3 n = GetPerspectiveTriangleNormal ( new double3 [] { triVs [0].P, triVs [1].P, triVs [2].P },
		        //    culling == Culling.Ccw ? CycleDir.Ccw : CycleDir.Cw );

		        //if ( ( n & double3.UnitZ ) >= 0 )
		        //    return;

				// Cull by order
		        CycleDir triOrder = GetPerspectiveTriangleOrder ( new double3 [] { triVs [0].P, triVs [1].P, triVs [2].P } );

		        if ( ( triOrder == CycleDir.Ccw && culling == Culling.Ccw ) ||
		             ( triOrder == CycleDir.Cw  && culling == Culling.Cw ) )
		            return;
		    }

		    VertexPT [][] vss = ClipTriangleByZ (
		        triVs,
		        nearZ, 1 );

		    foreach ( VertexPT [] clippedVs in vss )
		        DrawClippedAndCulledTriangle ( clippedVs [0], clippedVs [1], clippedVs [2] );
		}

		//private void DrawTriangle ( VertexPT [] triVs ) {
		//    VertexPT [][] vss = ClipTriangleByZ (
		//        triVs,
		//        nearZ, 1 );

		//    foreach ( VertexPT [] clippedVs in vss ) {
		//        if ( culling != Culling.None ) {
		//            //double3 n = GetPerspectiveTriangleNormal ( new double3 [] { clippedVs [0].P, clippedVs [1].P, clippedVs [2].P },
		//            //    culling == Culling.Ccw ? CycleDir.Ccw : CycleDir.Cw );

		//            //if ( ( n & double3.UnitZ ) >= 0 )
		//            //    return;

		//            CycleDir triOrder = GetPerspectiveTriangleOrder ( new double3 [] { clippedVs [0].P, clippedVs [1].P, clippedVs [2].P } );

		//            if ( ( triOrder == CycleDir.Ccw && culling == Culling.Ccw ) ||
		//                 ( triOrder == CycleDir.Cw  && culling == Culling.Cw ) )
		//                return;
		//        }

		//        DrawClippedAndCulledTriangle ( clippedVs [0], clippedVs [1], clippedVs [2] );
		//    }
		//}

		void DrawClippedAndCulledTriangle ( VertexPT v0, VertexPT v1, VertexPT v2 ) {
			double tpp = GetTexelsPerPixel ( v0.P, v1.P, v2.P, v0.T, v1.T, v2.T,
				image.Size.width, image.Size.height,
				sampler.Map.Width, sampler.Map.Height );

			// Transform to screen space
			double2 halfWh = new double2 ( image.Size.width / 2, image.Size.height / 2 );
			v0.P.xy /= v0.P.z;
			v1.P.xy /= v1.P.z;
			v2.P.xy /= v2.P.z;

			double3 screenP0 = new double3 ( halfWh + ( v0.P.xy ^ halfWh ), v0.P.z );
			double3 screenP1 = new double3 ( halfWh + ( v1.P.xy ^ halfWh ), v1.P.z );
			double3 screenP2 = new double3 ( halfWh + ( v2.P.xy ^ halfWh ), v2.P.z );

			// Invert vertically
			screenP0.y = image.Size.height - screenP0.y;
			screenP1.y = image.Size.height - screenP1.y;
			screenP2.y = image.Size.height - screenP2.y;

			image.DrawTexturedTriangle ( new [] { screenP0, screenP1, screenP2 },
				new [] { v0.T, v1.T, v2.T }, sampler, tpp );
			//image.DrawTriangle ( new [] { screenP0, screenP1, screenP2 }, color );
		}

		public static double GetTexelsPerPixel ( double3 p0, double3 p1, double3 p2,
			double2 t0, double2 t1, double2 t2,
			int pixelsWidth, int pixelsHeight,
			int texelsWidth, int texelsHeight )
		{
			double tpp1 = GetTexelsPerPixel ( p0, p1, t0, t1,
				pixelsWidth, pixelsHeight,
				texelsWidth, texelsHeight );
			double tpp2 = GetTexelsPerPixel ( p0, p2, t0, t2,
				pixelsWidth, pixelsHeight,
				texelsWidth, texelsHeight );
			double tpp3 = GetTexelsPerPixel ( p1, p2, t1, t2,
				pixelsWidth, pixelsHeight,
				texelsWidth, texelsHeight );

			return	Math.Max ( tpp1, Math.Max ( tpp2, tpp3 ) );
		}

		public static double GetTexelsPerPixel ( double3 p0, double3 p1,
			double2 t0, double2 t1,
			int pixelsWidth, int pixelsHeight,
			int texelsWidth, int texelsHeight )
		{
			double2 tHypot = t1 - t0;
			tHypot.s = Math.Abs ( tHypot.s );
			tHypot.t = Math.Abs ( tHypot.t );
			double tHypotLen = tHypot.Length;
			double horzTexelRes = tHypot.s * texelsWidth;
			double vertTexelRes = tHypot.t * texelsHeight;
			double hypotLen = ( p1 - p0 ).Length;
			double horzPixelRes = hypotLen * ( tHypot.s / tHypotLen ) * pixelsWidth  / 2;	// / 2 is because screen unit resolution is 2
			double vertPixelRes = hypotLen * ( tHypot.t / tHypotLen ) * pixelsHeight / 2;
			double horzTpp = horzTexelRes / horzPixelRes;
			double vertTpp = vertTexelRes / vertPixelRes;

			if ( double.IsNaN ( horzTpp ) )
				return	vertTpp;
			else if ( double.IsNaN ( vertTpp ) )
				return	horzTpp;
			else
				return	Math.Max ( horzTpp, vertTpp );
		}

		private void Clear () {
			image.SetValues ( clearColor );
		}

		private void Present () {
			bmp.Lock ();

			unsafe {
				for ( int y = 0 ; y < bmp.PixelHeight ; y++ ) {
					for ( int x = 0 ; x < bmp.PixelWidth ; x++ ) {
						double3 color = image.Values [x, y] * 255;
						int r = Math.Max ( 0, Math.Min ( 255, ( int ) color.r ) );
						int g = Math.Max ( 0, Math.Min ( 255, ( int ) color.g ) );
						int b = Math.Max ( 0, Math.Min ( 255, ( int ) color.b ) );
						int intColor = b | ( g << 8 ) | ( r << 16 );

						*( ( ( int* ) bmp.BackBuffer ) + y * image.Size.width + x ) = intColor;
					}
				}
			}

			bmp.AddDirtyRect ( new Int32Rect ( 0, 0, bmp.PixelWidth, bmp.PixelHeight ) );
			bmp.Unlock ();
		}

		Point lastMousePos;

		private void Window_MouseLeftButtonDown ( object sender, MouseButtonEventArgs e ) {
			isRotating = true;
			lastMousePos = e.GetPosition ( this );
			this.CaptureMouse ();
		}

		private void Window_MouseMove ( object sender, MouseEventArgs e ) {
			if ( isRotating && initialized ) {
				Point pos = e.GetPosition ( this );
				modelAngleY -= ( pos.X - lastMousePos.X ) * mouseXRatio;
				modelAngleX -= ( pos.Y - lastMousePos.Y ) * mouseYRatio;
				lastMousePos = pos;

				Render ();
			}
		}

		private void Window_MouseLeftButtonUp ( object sender, MouseButtonEventArgs e ) {
			isRotating = false;
			this.ReleaseMouseCapture ();
		}

		private void Window_MouseWheel ( object sender, MouseWheelEventArgs e ) {
			cameraPos.z += e.Delta > 0 ? 0.125 : -0.125;

			if ( cameraPos.z > nearZ )
				cameraPos.z = nearZ;

			Render ();
		}

		private void Window_KeyDown ( object sender, KeyEventArgs e ) {
			if ( e.Key == Key.Enter )
				Render ();
		}

		private void FilteringTypeComboBox_SelectionChanged ( object sender, SelectionChangedEventArgs e ) {
			sampler.Filtering = ( FilteringType ) FilteringTypeComboBox.SelectedValue;
			Render ();
		}

		private void WrapModeComboBox_SelectionChanged ( object sender, SelectionChangedEventArgs e ) {
			sampler.WrapMode = ( TextureWrap ) WrapModeComboBox.SelectedValue;
			Render ();
		}

		private void CullingComboBox_SelectionChanged ( object sender, SelectionChangedEventArgs e ) {
			culling = ( Culling ) CullingComboBox.SelectedValue;
			Render ();
		}

		private void TexturesListBox_SelectionChanged ( object sender, SelectionChangedEventArgs e ) {
			TextureEntry tEntry = TexturesListBox.SelectedValue as TextureEntry;
			
			if ( tEntry != null ) {
				sampler.Map = tEntry.Map;
				Render ();
			}
		}

		private void ResetCameraButton_Click ( object sender, RoutedEventArgs e ) {
			cameraAngleX = cameraAngleY = cameraAngleZ = 0;
			modelAngleX = modelAngleY = modelAngleZ = 0;
			cameraPos = -double3.UnitZ * 2;
			Render ();
		}

		private void ClearColorPicker_ColorChanged ( object sender, ColorPicker.EventArgs<Color> e ) {
			clearColor = e.Value.ToDouble3 ();
			Render ();
		}

		private void BorderColorPicker_ColorChanged ( object sender, ColorPicker.EventArgs<Color> e ) {
			sampler.BorderColor = e.Value.ToDouble3 ();
			Render ();
		}

		private void ShowLodsCheckBox_Checked ( object sender, RoutedEventArgs e ) {
			Graphics.ShowLods = true;
			Render ();
		}

		private void ShowLodsCheckBox_Unchecked ( object sender, RoutedEventArgs e ) {
			Graphics.ShowLods = false;
			Render ();
		}

		private void UseMipMapsCheckBox_Checked ( object sender, RoutedEventArgs e ) {
			sampler.UseMipmaps = true;
			Render ();
		}

		private void UseMipMapsCheckBox_Unchecked ( object sender, RoutedEventArgs e ) {
			sampler.UseMipmaps = false;
			Render ();
		}

		private void ScreenshotButton_Click ( object sender, RoutedEventArgs e ) {
			string ssDir = @"..\..\Screenshots";

			if ( !IO.Directory.Exists ( ssDir ) )
				ssDir = @"Screenshots";

			IO.Directory.CreateDirectory ( ssDir );

			image.Save ( string.Format ( @"{0}\ss_{1}.bmp", ssDir, DateTime.Now.Ticks ) );
		}
	}
}
