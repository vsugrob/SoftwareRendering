using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace TextureFilteringDev {
	public class EnumMembersConverter : IValueConverter {
		public object Convert ( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture ) {
			Type t = value as Type;

			if ( t == null )
				return	null;

			return	Enum.GetValues ( t );
		}

		public object ConvertBack ( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture ) {
			throw new NotImplementedException ();
		}
	}
}
