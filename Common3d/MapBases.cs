using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace Common3d {
	public abstract class Map1d {
		public abstract double3 Map ( int x, int lod = 0 );
		public abstract int Width { get; }
	}

	public abstract class Map2d {
		public abstract double3 Map ( int x, int y, int lod = 0 );
		public abstract int Width { get; }
		public abstract int Height { get; }
		public abstract int TexelResolution { get; }
		public abstract int NumLods { get; }
		public abstract int LodWidth ( int lod );
		public abstract int LodHeight ( int lod );
		public abstract bool MipsLoaded { get; }
		public abstract void LoadMipmaps ();
	}

	public abstract class Map3d {
		public abstract double3 Map ( int x, int y, int z, int lod = 0 );
		public abstract int Width { get; }
		public abstract int Height { get; }
		public abstract int Depth { get; }
	}
}
