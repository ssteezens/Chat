using System;
using Windows.UI.Xaml.Data;

namespace Chat.Converters
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
        /// <param name="language"> Not used. </param>
        /// <returns> True or false if string value is null or empty. </returns>
		public object Convert(object value, Type targetType, object parameter, string language)
		{
            if(!(value is string stringValue))
                throw new ArgumentException("Value passed to converter must be a string.");

			return !string.IsNullOrEmpty(stringValue);
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
