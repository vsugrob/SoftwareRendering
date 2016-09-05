using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common3d;
using System.Windows.Media.Imaging;
using IO = System.IO;

namespace TextureFilteringDev {
	public class TextureEntry {
		public Map2d Map;
		public string Name { get; set; }
		public string Path { get; set; }

		protected TextureEntry ( Map2d map, string fileName ) {
			this.Map = map;
			this.Name = IO.Path.GetFileName ( fileName );
			this.Path = IO.Path.GetFullPath ( fileName );
		}

		public static TextureEntry Load ( string fileName ) {
			Texture2d tex;

			try {
				tex = Texture2d.Load ( fileName );
			} catch ( Exception ex ) {
				Console.WriteLine ( "Error while creating Texture2d: {0}", ex.Message );

				return	null;
			}

			return	new TextureEntry ( tex, fileName );
		}
	}
}
