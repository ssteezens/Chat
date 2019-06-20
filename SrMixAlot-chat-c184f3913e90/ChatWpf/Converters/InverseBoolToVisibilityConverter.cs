using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ChatWpf.Converters
{
	/// <summary>
    ///		Inverse bool to visibility converter.
    /// </summary>
    public class InverseBoolToVisibilityConverter : IValueConverter
    {
		/// <summary>
        ///		Converts a bool a visibility.
        /// </summary>
        /// <param name="value"> Value to convert. </param>
        /// <param name="targetType"> Type to convert to. </param>
        /// <param name="parameter"> Not used. </param>
        /// <param name="culture"> Not used. </param>
        /// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
				throw new ArgumentException("Value passed to converter must be a bool.");

			return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }
        
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
