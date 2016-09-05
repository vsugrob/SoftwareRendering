using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace Common3d {
	public class Sampler2d {
		#region Properties
		public Map2d Map;
		public TextureWrap WrapMode;
		public double3 BorderColor = double3.UnitY;

		public FilteringType filtering;
		public FilteringType Filtering {
			get { return	filtering; }
			set {
				filtering = value == FilteringType.Best ? FilteringType.Trilinear : value;
			}
		}

		bool useMipmaps = false;
		public bool UseMipmaps {
			get { return	useMipmaps; }
			set {
				if ( value == useMipmaps )
					return;

				useMipmaps = value;

				if ( useMipmaps && !Map.MipsLoaded )
				    Map.LoadMipmaps ();
			}
		}
		#endregion Properties

		#region Constructors
		public Sampler2d ( Map2d map, TextureWrap wrapMode, FilteringType filtering = FilteringType.Best, bool useMipmaps = true ) {
			this.Map = map;
			this.Filtering = filtering;
			this.WrapMode = wrapMode;
			this.UseMipmaps = useMipmaps;
		}
		#endregion Constructors

		#region Methods
		public double3 GetColor ( double2 t, double lod = 0 ) {
			if ( !UseMipmaps )
				lod = 0;
			else if ( lod < 0 )
				lod = 0;

			//int iLod = lod.PreciseRound ();
			int iLod = ( int ) lod;

			if ( WrapMode <= TextureWrap.ClampToBorder ) {
				t.x = t.x.Clamp ( 0, 1 );
				t.y = t.y.Clamp ( 0, 1 );
			} else if ( WrapMode == TextureWrap.Repeat ) {
				if ( t.x != 0 ) {
					double tx = t.x % 1;

					if ( tx == 0 )
						t.x = t.x < 0 ? 0 : 1;	// assuming tx = 1
					else
						t.x = t.x < 0 ? 1 - Math.Abs ( tx ) : tx;
				}

				if ( t.y != 0 ) {
					double ty = t.y % 1;

					if ( ty == 0 )
						t.y = t.y < 0 ? 0 : 1;	// assuming ty = 1
					else
						t.y = t.y < 0 ? 1 - Math.Abs ( ty ) : ty;
				}
			} else if ( WrapMode == TextureWrap.MirroredRepeat ) {
				// Note that ( int ) ( t.x ) % 2 == 0
				// has inverted meaning when tx == 1

				if ( t.x != 0 ) {
					double tx = t.x % 1;

					if ( tx == 0 )
						t.x = ( ( int ) ( t.x ) % 2 == 0 ) ? 0 : 1;	// assuming tx = 1
					else {
						t.x = ( ( int ) ( t.x ) % 2 == 0 ) ?
							Math.Abs ( tx ) : 1 - Math.Abs ( tx );
					}
				}

				if ( t.y != 0 ) {
					double ty = t.y % 1;

					if ( ty == 0 )
						t.y = ( ( int ) ( t.y ) % 2 == 0 ) ? 0 : 1;	// assuming ty = 1
					else {
						t.y = ( ( int ) ( t.y ) % 2 == 0 ) ?
							Math.Abs ( ty ) : 1 - Math.Abs ( ty );
					}
				}
			}

			if ( Filtering == FilteringType.TexCoord )
				return	new double3 ( t, 0 );

			t.y = 1 - t.y;

			if ( Filtering == FilteringType.Nearest ) {
				int x = ( t.x * ( Map.LodWidth ( iLod )  - 1 ) ).PreciseRound ();
				int y = ( t.y * ( Map.LodHeight ( iLod ) - 1 ) ).PreciseRound ();

				return	Map.Map ( x, y, iLod );
			} else if ( Filtering == FilteringType.Bilinear )
				return	Bilinear ( t, iLod );
			else if ( Filtering == FilteringType.Trilinear ) {
				if ( lod == 0 )
					return	Bilinear ( t, iLod );
				else {
					double k = lod - iLod;
				
					return	k.Lerp ( Bilinear ( t, iLod ), Bilinear ( t, iLod + 1 ) );
				}
			} else
				return	new double3 ( t, 0 );
		}

		private double3 Bilinear ( double2 t, int iLod ) {
			double txW = t.x * Map.LodWidth ( iLod );
			double tyH = t.y * Map.LodHeight ( iLod );
			int x1 = txW.PreciseRound ();
			int y1 = tyH.PreciseRound ();
			int x0 = x1 - 1;
			int y0 = y1 - 1;
			double kx = txW - x0 - 0.5;
			double ky = tyH - y0 - 0.5;

			if ( WrapMode <= TextureWrap.ClampToEdge ||
				 WrapMode == TextureWrap.MirroredRepeat ) {
				if ( x0 == -1 )
					x0 = 0;
				else if ( x1 == Map.LodWidth ( iLod ) )
					x1 = Map.LodWidth ( iLod ) - 1;

				if ( y0 == -1 )
					y0 = 0;
				else if ( y1 == Map.LodHeight ( iLod ) )
					y1 = Map.LodHeight ( iLod ) - 1;

				// FIXIT: there is no need to lerp in edge-cases
			} else if ( WrapMode == TextureWrap.ClampToBorder ) {
				double3 c00, c10, c01, c11;

				if ( x0 == -1 ) {
					if ( y0 == -1 ) {	// Top left corner
						c00 = c01 = c10 = BorderColor;
						c11 = Map.Map ( x1, y1, iLod );
					} else if ( y1 == Map.LodHeight ( iLod ) ) {	// Bottom left corner
						c00 = c01 = c11 = BorderColor;
						c10 = Map.Map ( x1, y0, iLod );
					} else {	// Left side
						c00 = c01 = BorderColor;
						c10 = Map.Map ( x1, y0, iLod );
						c11 = Map.Map ( x1, y1, iLod );
					}
				} else if ( x1 == Map.LodWidth ( iLod ) ) {
					if ( y0 == -1 ) {	// Top right corner
						c00 = c10 = c11 = BorderColor;
						c01 = Map.Map ( x0, y1, iLod );
					} else if ( y1 == Map.LodHeight ( iLod ) ) {	// Bottom right corner
						c10 = c11 = c01 = BorderColor;
						c00 = Map.Map ( x0, y0, iLod );
					} else {	// Right side
						c10 = c11 = BorderColor;
						c00 = Map.Map ( x0, y0, iLod );
						c01 = Map.Map ( x0, y1, iLod );
					}
				} else {
					if ( y0 == -1 ) {	// Top side
						c00 = c10 = BorderColor;
						c01 = Map.Map ( x0, y1, iLod );
						c11 = Map.Map ( x1, y1, iLod );
					} else if ( y1 == Map.LodHeight ( iLod ) ) {	// Bottom side
						c01 = c11 = BorderColor;
						c00 = Map.Map ( x0, y0, iLod );
						c10 = Map.Map ( x1, y0, iLod );
					} else {
						c00 = Map.Map ( x0, y0, iLod );
						c10 = Map.Map ( x1, y0, iLod );
						c01 = Map.Map ( x0, y1, iLod );
						c11 = Map.Map ( x1, y1, iLod );
					}
				}

				return Interpolation.Bilinear ( kx, ky,
					c00, c10,
					c01, c11
				);
			} else if ( WrapMode == TextureWrap.Repeat ) {
				if ( x0 == -1 )
					x0 = Map.LodWidth ( iLod ) - 1;
				else if ( x1 == Map.LodWidth ( iLod ) )
					x1 = 0;

				if ( y0 == -1 )
					y0 = Map.LodHeight ( iLod ) - 1;
				else if ( y1 == Map.LodHeight ( iLod ) )
					y1 = 0;
			}

			double3 c = Interpolation.Bilinear ( kx, ky,
				Map.Map ( x0, y0, iLod ), Map.Map ( x1, y0, iLod ),
				Map.Map ( x0, y1, iLod ), Map.Map ( x1, y1, iLod )
			);

			return c;
		}
		#endregion Methods
	}
}
