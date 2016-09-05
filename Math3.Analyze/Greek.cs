using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Math3.Analyze {
	public static class Greek {
		public static readonly ReadOnlyCollection <string> Alphabet =
			new List <string> ( new [] {
				"Alpha",	// Αα
				"Beta",		// Ββ
				"Gamma",	// Γγ
				"Delta",	// Δδ
				"Epsilon",	// Εε
				"Zeta",		// Ζζ
				"Eta",		// Ηη
				"Theta",	// Θθ
				"Iota",		// Ιι
				"Kappa",	// Κκ
				"Lambda",	// Λλ
				"Mu",		// Μμ
				"Nu",		// Νν
				"Xi",		// Ξξ
				"Omicron",	// Οο
				"Pi",		// Ππ
				"Rho",		// Ρρ
				"Sigma",	// Σσς
				"Tau",		// Ττ
				"Upsilon",	// Υυ
				"Phi",		// Φφ
				"Chi",		// Χχ
				"Psi",		// Ψψ
				"Omega",	// Ωω
			}
		).AsReadOnly ();

		public static readonly Regex GreekLetterRegex =
			new Regex ( string.Join ( "|", Alphabet ), RegexOptions.IgnoreCase );

		public static string ReplaceGreekWithHtmlEntities ( string str ) {
			return	GreekLetterRegex.Replace ( str, "&$0;" );
		}
	}
}
