using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace CodeGenerator {
	class Program {
		static void Main ( string [] args ) {
			Globals.Namespace = "Math3d";
			NumericExtensionsGenerator.Generate ();
			VectorCodeGenerator.Generate ();
		}
	}
}
