using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ChatWpf.Converters
{
    /// <summary>
    ///     Converts a null value to a visibility. 
    /// </summary>
    public class NullToVisibilityConverter : IValueConverter
    {
		/// <summary> Converts a null value to Collapsed and a non null value to Visible. </summary>
		/// <param name="value"> Value to convert. </param>
		/// <param name="targetType"> Type to convert to. </param>
		/// <param name="parameter"> Not used. </param>
		/// <param name="culture"> Not used. </param>
		/// <returns> Visible if not null, otherwise Collapsed. </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value is null ? Visibility.Collapsed : Visibility.Visible;
		}
        
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
