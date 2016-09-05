using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace Common3d {
	public class HdrBuffer {
		#region Fields
		public readonly double3 [,] Values;
		public readonly IntSize Size;
		#endregion Fields

		#region Constructors
		public HdrBuffer ( int width, int height ):
			this ( new IntSize ( width, height ) ) {}

		public HdrBuffer ( IntSize size ) {
			Values = new double3 [size.width, size.height];
			this.Size = size;
		}
		#endregion Constructors

		#region Methods
		public void SetValues ( double3 value ) {
			for ( int x = 0 ; x < Size.width ; x++ )
				for ( int y = 0 ; y < Size.height ; y++ )
					Values [x, y] = value;
		}

		public static HdrBuffer Load ( string uri ) {
			BitmapImage bi = new BitmapImage ( new Uri ( uri, UriKind.RelativeOrAbsolute ) );
			//PixelFormat fmt = bi.Format;	// FIXIT: handle multiple formats
			int [,] pixels = new int [bi.PixelHeight, bi.PixelWidth];
			
			unsafe {
				fixed ( int * p = pixels ) {
					IntPtr ip = new IntPtr ( p );
					bi.CopyPixels ( Int32Rect.Empty, ip, bi.PixelWidth * bi.PixelHeight * 4, bi.PixelWidth * 4 );
				}
			}

			HdrBuffer image = new HdrBuffer ( bi.PixelWidth, bi.PixelHeight );
			
			for ( int y = 0 ; y < bi.PixelHeight ; y++ ) {
				for ( int x = 0 ; x < bi.PixelWidth ; x++ ) {
					int intC = pixels [y, x];

					double3 c = new double3 (
						( double ) ( ( intC & 0x00ff0000 ) >> 16 ) / 255,
						( double ) ( ( intC & 0x0000ff00 ) >> 8 ) / 255,
						( double ) ( intC & 0x000000ff ) / 255
					);

					image.Values [x, y] = c;
				}
			}
			
			return	image;
		}

		public void Save ( string uri ) {
			int [,] pixels = new int [Size.height, Size.width];

			for ( int y = 0 ; y < Size.height ; y++ ) {
				for ( int x = 0 ; x < Size.width ; x++ ) {
					double3 c = Values [x, y].Clamp ( 0, 1 )  * 255;
					pixels [y, x] = c.b.PreciseRound () |
						( c.g.PreciseRound () << 8 ) |
						( c.r.PreciseRound () << 16 );
				}
			}

			using ( FileStream fs = File.OpenWrite ( uri ) ) {
				BmpBitmapEncoder bmpEncoder = new BmpBitmapEncoder ();

				unsafe {
					fixed ( int * p = pixels ) {
						IntPtr ip = new IntPtr ( p );

						BitmapSource bmpSource = BitmapFrame.Create ( Size.width, Size.height,
							92, 92, PixelFormats.Bgr32, null,
							ip, Size.width * Size.height * 4, Size.width * 4 );

						bmpEncoder.Frames.Add ( BitmapFrame.Create ( bmpSource ) );
						bmpEncoder.Save ( fs );
					}
				}
			};
		}

		public void DrawLineBresenham ( int x0, int y0, int x1, int y1, double3 color, Func <double3, double3, double3> blendFunc = null ) {
			if ( Math3.ClampLine ( ref x0, ref y0, ref x1, ref y1, Size.width, Size.height ) )
				DrawLineBresenhamClamped ( x0, y0, x1, y1, color, blendFunc );
		}

		public void DrawLineBresenhamClamped ( int x0, int y0, int x1, int y1, double3 color, Func <double3, double3, double3> blendFunc = null ) {
			if ( x0 == x1 && y0 == y1 )
				Values [x0, y0] = blendFunc == null ? color : blendFunc ( Values [x0, y0], color );
			else if ( x0 == x1 ) {
				if ( y0 > y1 )
					Util.Swap ( ref y0, ref y1 );

				for ( int y = y0 ; y <= y1 ; y++ )
					Values [x0, y] = blendFunc == null ? color : blendFunc ( Values [x0, y], color );
			} else if ( y0 == y1 ) {
				if ( x0 > x1 )
					Util.Swap ( ref x0, ref x1 );

				for ( int x = x0 ; x <= x1 ; x++ )
					Values [x, y0] = blendFunc == null ? color : blendFunc ( Values [x, y0], color );
			} else {
				bool steep = Math.Abs ( y1 - y0 ) > Math.Abs ( x1 - x0 );

				if ( steep ) {
					Util.Swap ( ref x0, ref y0 );
					Util.Swap ( ref x1, ref y1 );
				}

				if ( x0 > x1 ) {
					Util.Swap ( ref x0, ref x1 );
					Util.Swap ( ref y0, ref y1 );
				}

				int sy = y1 > y0 ? 1 : -1;
				int y = y0;
				float e = 0;
				float de = ( float ) Math.Abs ( y1 - y0 ) / ( x1 - x0 );

				for ( int x = x0 ; x <= x1 ; x++ ) {
					if ( steep )
						Values [y, x] = blendFunc == null ? color : blendFunc ( Values [y, x], color );
					else
						Values [x, y] = blendFunc == null ? color : blendFunc ( Values [x, y], color );

					e += de;

					if ( e >= 0.5 ) {
						y += sy;
						e -= 1;
					}
				}
			}
		}

		public void DrawTriangle ( double2 [] pts, double3 color, Func <double3, double3, double3> blendFunc = null ) {
			int bottomIdx, midIdx, topIdx, separateIdx, coincidentIdx;
			Classify3Result res = Math3.Classify3 ( new [] { pts [0].y, pts [1].y, pts [2].y },
				out topIdx, out midIdx, out bottomIdx, out separateIdx, out coincidentIdx );

			if ( res == Classify3Result.AllCoincident ) {
				int x = ( int ) Math.Round ( pts [0].x, MidpointRounding.AwayFromZero );
				int y = ( int ) Math.Round ( pts [0].y, MidpointRounding.AwayFromZero );
				Values [x, y] = blendFunc == null ? color : blendFunc ( Values [x, y], color );

				return;
			} else if ( res == Classify3Result.HasCoincidence ) {
				double2 separatePoint = pts [separateIdx];
				double2 midPoint = pts [midIdx];
				double2 coincidentPoint = pts [coincidentIdx];

				DrawXAlignedTriangle ( separatePoint, midPoint.x, coincidentPoint.x, midPoint.y, color, true, blendFunc );
			} else {
				double2 topPoint = pts [topIdx];
				double2 midPoint = pts [midIdx];
				double2 bottomPoint = pts [bottomIdx];
				double dx = topPoint.x - bottomPoint.x;
				double dy = topPoint.y - bottomPoint.y;
				double ik = dx / dy;
				double x2 = ik * ( midPoint.y - bottomPoint.y ) + bottomPoint.x;

				DrawXAlignedTriangle ( bottomPoint, midPoint.x, x2, midPoint.y, color, true, blendFunc );
				DrawXAlignedTriangle ( topPoint, midPoint.x, x2, midPoint.y, color, false, blendFunc );
			}
		}

		public void DrawXAlignedTriangle ( double2 p0, double x1, double x2, double sideY, double3 color,
			bool includeSide = true, Func <double3, double3, double3> blendFunc = null )
		{
			int h1 = Size.height - 1;
			int w1 = Size.width - 1;

			if ( ( p0.y < 0 && sideY < 0 ) ||
				 ( p0.y > h1 && sideY > h1 ) ||
				 ( p0.x < 0 && x1 < 0 && x2 < 0 ) ||
				 ( p0.x > w1 && x1 > w1 && x2 > w1 ) )
				return;

			double dy = ( sideY - p0.y );
			double dx1 = ( x1 - p0.x );
			double dx2 = ( x2 - p0.x );
			int stepY = Math.Sign ( dy );
			dy = Math.Abs ( dy );
			double ik1 = dx1 / dy;
			double ik2 = dx2 / dy;
			int startY = ( int ) Math.Round ( p0.y );
			int endY = ( int ) Math.Round ( sideY );
			double curX1 = p0.x, curX2 = p0.x;

			if ( startY < 0 ) {
				int displY = 0 - startY;
				curX1 += ik1 * displY;
				curX2 += ik2 * displY;
				startY = 0;
			} else if ( startY > h1 ) {
				int displY = h1 - startY;
				curX1 += ik1 * displY;
				curX2 += ik2 * displY;
				startY = h1;
			}

			if ( endY < 0 ) {
				endY = 0;
				includeSide = true;
			} else if ( endY > h1 ) {
				endY = h1;
				includeSide = true;
			}

			int numLines = Math.Abs ( endY - startY );
			bool reflectX = x1 > x2;

			if ( includeSide )
				numLines++;

			ik1 = dx1 / numLines;
			ik2 = dx2 / numLines;

			for ( int i = 0, curY = startY ; i < numLines ; i++, curY += stepY ) {
				int leftX, rightX;

				// FIXIT: fix rounding (clearly seen on sides like ( 10, 100, 10, 101 )
				if ( reflectX ) {
				    leftX = ( int ) Math.Round ( curX2, MidpointRounding.AwayFromZero );
				    rightX = ( int ) Math.Round ( curX1, MidpointRounding.AwayFromZero );
				} else {
				    leftX = ( int ) Math.Round ( curX1, MidpointRounding.AwayFromZero );
				    rightX = ( int ) Math.Round ( curX2, MidpointRounding.AwayFromZero );
				}

				if ( leftX < 0 )
					leftX = 0;
				
				if ( rightX > w1 )
					rightX = w1;

				for ( int x = leftX ; x <= rightX ; x++ )
					Values [x, curY] = blendFunc == null ? color : blendFunc ( Values [x, curY], color );

				curX1 += ik1;
				curX2 += ik2;
			}
		}
		#endregion Methods
	}
}
