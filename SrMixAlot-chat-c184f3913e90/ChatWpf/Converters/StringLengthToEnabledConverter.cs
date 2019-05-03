using System;
using System.Globalization;
using System.Windows.Data;

namespace ChatWpf.Converters
{
    /// <summary>
    ///     Takes a string's length and converts it to Visibility.
    /// </summary>
    public class StringLengthToEnabledConverter : IValueConverter
    {
        /// <summary>
        ///     Takes a string's length and converts it to bool.
        /// </summary>
        /// <param name="value"> Value to convert. </param>
        /// <param name="targetType"> Type to be converted. </param>
        /// <param name="parameter"> Not used. </param>
        /// <param name="culture"> Not used. </param>
        /// <returns> True or false if string value is null or empty. </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return !string.IsNullOrEmpty(value as string);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
