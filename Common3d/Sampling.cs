using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common3d {
	public enum FilteringType {
		Nearest, Bilinear, Trilinear, Anisotropic,
		TexCoord,
		Best
	}

	public enum TextureWrap {
		Clamp, ClampToEdge, ClampToBorder, Repeat, MirroredRepeat
	}
}
