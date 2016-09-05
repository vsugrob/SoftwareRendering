using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Math3.Analyze {
	public static class Utils {
		#region Enum SetFlag
		public static T SetFlag <T> ( this Enum e, T flag, bool set ) {
			if ( e.GetType () != flag.GetType () )
				throw new ArgumentException ( "Enum type does not match" );
			
			ulong eVal = e.ToUInt64 ();
			ulong fVal = ( flag as Enum ).ToUInt64 ();
			ulong res;
			
			if ( set )
			    res = eVal | fVal;
			else
				res = eVal & ( ~fVal );

			return	( T ) Enum.ToObject ( e.GetType (), res );
		}

		public static ulong ToUInt64 ( this Enum value ) {
			switch ( Convert.GetTypeCode ( value ) ) {
			case TypeCode.SByte:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
				return	( ulong )	Convert.ToInt64 ( value, CultureInfo.InvariantCulture );
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
				return	Convert.ToUInt64 ( value, CultureInfo.InvariantCulture );
			}

			throw new InvalidOperationException ( "Unknown enum type" );
		}
		#endregion Enum SetFlag
	}
}
