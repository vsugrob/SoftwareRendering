using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;
using System.Windows.Media.Imaging;
using System.Windows;
using System.IO;
using System.Windows.Media;

namespace Common3d {
	public class Texture2d : Map2d {
		#region Properties
		bool mipsLoaded = false;
		HdrBuffer [] images;

		public override int Width { get { return	images [0].Size.width; } }
		public override int Height { get { return	images [0].Size.height; } }
		public override int TexelResolution { get { return	Math.Min ( images [0].Size.width, images [0].Size.height ); } }
		public override bool MipsLoaded { get { return	mipsLoaded; } }
		public override int NumLods { get { return	images.Length; } }
		public override int LodWidth ( int lod ) { return	images [lod.Clamp ( 0, NumLods - 1 )].Size.width; }
		public override int LodHeight ( int lod ) { return	images [lod.Clamp ( 0, NumLods - 1 )].Size.height; }
		public string Name { get; private set; }
		#endregion Properties

		#region Methods
		public override double3 Map ( int x, int y, int lod = 0 ) {
			return	images [lod.Clamp ( 0, NumLods - 1 )].Values [x, y];
		}

		public override void LoadMipmaps () {
			if ( mipsLoaded )
				return;

			int numLods = ( int ) Math.Log ( Math.Min ( Width, Height ), 2 ) + 1;
			HdrBuffer baseImage = images [0];
			images = new HdrBuffer [numLods];
			images [0] = baseImage;

			string cacheDir = @"..\..\Cache";

			if ( !Directory.Exists ( cacheDir ) )
				cacheDir = @"Cache";

			string mipDir = string.Format ( @"{0}\Mipmaps\{1}", cacheDir, Name );

			if ( Directory.Exists ( mipDir ) ) {
				for ( int lod = 1 ; lod < NumLods ; lod++ ) {
					string mipFileName = string.Format ( @"{0}\{1}.bmp", mipDir, lod );
					bool loaded = false;

					if ( File.Exists ( mipFileName ) ) {
						loaded = true;

						try {
							Load ( mipFileName, lod );
						} catch ( Exception ex ) {
							Console.WriteLine ( "Error while loading texture \"{0}\" miplevel {1}: {2}",
								Name, lod, ex.Message );

							loaded = false;
						}
					}

					if ( !loaded ) {
						BuildMipmap ( lod );
						Save ( mipFileName, lod );
					}
				}
			} else {
				Directory.CreateDirectory ( mipDir );

				for ( int lod = 1 ; lod < NumLods ; lod++ ) {
					string mipFileName = string.Format ( @"{0}\{1}.bmp", mipDir, lod );

					BuildMipmap ( lod );
					Save ( mipFileName, lod );
				}
			}

			mipsLoaded = true;
		}

		private void BuildMipmap ( int lod ) {
			int hiLod = lod - 1;
			int w = images [hiLod].Size.width,
				h = images [hiLod].Size.height;

			double dx = 0, dy = 0;

			if ( w % 2 != 0 ) {
				w /= 2;
				dx = 1.0 / w;
			} else
				w /= 2;

			if ( h % 2 != 0 ) {
				h /= 2;
				dy = 1.0 / h;
			} else
				h /= 2;

			double wQuotInv = 1 / ( 2 + dx ),
					hQuotInv = 1 / ( 2 + dy );

			HdrBuffer hiMip = images [hiLod];
			HdrBuffer lowMip = new HdrBuffer ( w, h );
				
			double ky0 = 1, ky2 = dy;

			for ( int ly = 0, hy = 0 ;
					ly < h ;
					ly++, hy += 2, ky0 -= dy, ky2 += dy )
			{
				double kx0 = 1, kx2 = dx;

				for ( int lx = 0, hx = 0 ;
						lx < w ;
						lx++, hx += 2, kx0 -= dx, kx2 += dx )
				{
					int hx1 = hx + 1, hx2 = hx + 2,
						hy1 = hy + 1, hy2 = hy + 2;
					double3 c00 = hiMip.Values [hx, hy] * kx0;
					double3 c10 = hiMip.Values [hx1, hy];
					double3 c20 = kx2 != 0 ? hiMip.Values [hx2, hy] * kx2 : double3.Zero;

					double3 c01 = hiMip.Values [hx, hy1] * kx0;
					double3 c11 = hiMip.Values [hx1, hy1];
					double3 c21 = kx2 != 0 ? hiMip.Values [hx2, hy1] * kx2 : double3.Zero;

					double3 c02 = ky2 != 0 ? hiMip.Values [hx, hy2] * kx0 : double3.Zero;
					double3 c12 = ky2 != 0 ? hiMip.Values [hx1, hy2] : double3.Zero;
					double3 c22 = ky2 != 0 && kx2 != 0 ? hiMip.Values [hx2, hy2] * kx2 : double3.Zero;

					lowMip.Values [lx, ly] = (
						( c00 + c10 + c20 ) * ky0 +
						( c01 + c11 + c21 ) +
						( c02 + c12 + c22 ) * ky2
					) * wQuotInv * hQuotInv;
				}
			}

			//for ( int ly = 0, hy = 0 ; ly < h ; ly++, hy += 2 ) {
			//    for ( int lx = 0, hx = 0 ; lx < w ; lx++, hx += 2 ) {
			//        int hx1 = hx + 1;
			//        int hy1 = hy + 1;

			//        lowMip.Values [lx, ly] = (
			//            hiMip.Values [hx, hy ] + hiMip.Values [hx1, hy ] +
			//            hiMip.Values [hx, hy1] + hiMip.Values [hx1, hy1] 
			//            ) * 0.25;
			//    }
			//}

			images [lod] = lowMip;
		}

		public void Load ( string uri, int lod ) {
			images [lod] = HdrBuffer.Load ( uri );
		}

		public void Save ( string uri, int lod ) {
			lod = lod.Clamp ( 0, NumLods - 1 );
			HdrBuffer image = images [lod];
			image.Save ( uri );
		}

		public override string ToString () {
			return	string.Format ( "Name: {0}, Width: {1}, Height: {2}, {3}",
				Name, images [0].Size.width, images [0].Size.height,
				MipsLoaded ? string.Format ( "Mips loaded ({0} levels)", NumLods ) : "Mips not loaded" );
		}
		#endregion Methods

		#region Factory Methods
		public static Texture2d Load ( string uri, bool loadMipmaps = true ) {
			Texture2d tex = new Texture2d ();
			tex.images = new HdrBuffer [1];
			tex.Load ( uri, 0 );
			tex.Name = Path.GetFileName ( uri );

			if ( loadMipmaps )
				tex.LoadMipmaps ();

			return	tex;
		}
		#endregion Factory Methods
	}
}
