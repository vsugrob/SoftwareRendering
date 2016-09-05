using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTrace {
	public struct RefractionIndex {
		#region Fields
		public double CoefficientIn, CoefficientOut,
					  CriticalOutAngleCos,
					  CriticalInAngleCos;
		#endregion Fields

		#region Constructors
		public RefractionIndex ( double mediumIndex ) {
			CoefficientOut = mediumIndex;
			CoefficientIn = 1 / mediumIndex;

			if ( CoefficientOut > 1 ) {
				CriticalOutAngleCos = Math.Sqrt ( 1 - CoefficientIn * CoefficientIn );
				CriticalInAngleCos = 0;
			} else {
				CriticalOutAngleCos = 0;
				CriticalInAngleCos = Math.Sqrt ( 1 - CoefficientOut * CoefficientOut );
			}
		}
		#endregion Constructors

		#region Factory Methods
		public static RefractionIndex Water ( double wavelength ) {
			wavelength = wavelength / 1000;	// nanometers to micrometers

			double n = Math.Sqrt ( 1 +
				5.684027565E-1 * Math.Pow ( wavelength, 2 ) / ( Math.Pow ( wavelength, 2 ) - 5.101829712E-3 ) +
				1.726177391E-1 * Math.Pow ( wavelength, 2 ) / ( Math.Pow ( wavelength, 2 ) - 1.821153936E-2 ) +
				2.086189578E-2 * Math.Pow ( wavelength, 2 ) / ( Math.Pow ( wavelength, 2 ) - 2.620722293E-2 ) +
				1.130748688E-1 * Math.Pow ( wavelength, 2 ) / ( Math.Pow ( wavelength, 2 ) - 1.069792721E1 ) );

			return	new RefractionIndex ( n );
		}

		public static RefractionIndex Diamond ( double wavelength ) {
			wavelength = wavelength / 1000;	// nanometers to micrometers

			double n = Math.Sqrt ( 1 +
				4.3356 * Math.Pow ( wavelength, 2 ) / ( Math.Pow ( wavelength, 2 ) -
				Math.Pow ( 0.1060, 2 ) ) +
				0.3306 * Math.Pow ( wavelength, 2 ) / ( Math.Pow ( wavelength, 2 ) -
				Math.Pow ( 0.1750, 2 ) ) );

			return	new RefractionIndex ( n );
		}

		public static RefractionIndex OpticalGlassBaf10 ( double wavelength ) {
			wavelength = wavelength / 1000;	// nanometers to micrometers

			double n = Math.Sqrt ( 1 +
				1.5851495 * Math.Pow ( wavelength, 2 ) / ( Math.Pow ( wavelength, 2 ) - 0.00926681282 ) +
				0.143559385 * Math.Pow ( wavelength, 2 ) / ( Math.Pow ( wavelength, 2 ) - 0.0424489805 ) +
				1.08521269 * Math.Pow ( wavelength, 2 ) / ( Math.Pow ( wavelength, 2 ) - 105.613573 ) );

			return	new RefractionIndex ( n );
		}
		#endregion Factory Methods
	}
}
