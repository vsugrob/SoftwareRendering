using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeGenerator {
	public static class Utils {
		public static List <T []> GetPermutations <T> ( T [] components ) {
			int b = components.Length;
			int num = ( int ) Math.Pow ( b, b );
			List <T []> permutations = new List <T []> ( num );

			for ( int i = 0 ; i < num ; i++ ) {
				int [] digits = ArrayPad ( ToNotation ( i, b ), b );
				T [] permutation = new T [b];

				for ( int j = 0 ; j < b ; j++ )
					permutation [j] = components [digits [j]];

				permutations.Add ( permutation );
			}

			return	permutations;
		}

		public static int [] ToNotation ( int value, int b ) {
			List <int> digits = new List <int> ();

			int q = value, r;

			do {
				r = q % b;
				q = q / b;

				digits.Add ( r );
			} while ( q != 0 );

			digits.Reverse ();

			return	digits.ToArray ();
		}

		public static T [] ArrayPad <T> ( T [] arr, int toLen ) {
			T [] resArr = new T [toLen];
			Array.Copy ( arr, 0, resArr, toLen - arr.Length, arr.Length );

			return	resArr;
		}

		public static string IndentRight ( string str, string indentStr ) {
			return	Regex.Replace ( str, @"(.+)(\r\n)", indentStr + "$1$2" );
		}
	}
}
