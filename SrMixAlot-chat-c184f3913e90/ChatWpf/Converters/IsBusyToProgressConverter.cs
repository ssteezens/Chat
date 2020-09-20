using System;
using System.Globalization;
using System.Windows.Data;

namespace ChatWpf.Converters
{
    /// <summary>
    ///     Converts a bool "IsBusy" value to 0 or 100 progress value.
    /// </summary>
    public class IsBusyToProgressConverter : IValueConverter
    {
        /// <summary>
        ///     Converts an IsBusy bool value to a 0 or 100 progress value.
        /// </summary>
        /// <param name="value"> The value to convert. </param>
        /// <param name="targetType"> The type to convert to. </param>
        /// <param name="parameter"> Not used. </param>
        /// <param name="culture"> Not used. </param>
        /// <returns> 0 if the "IsBusy" bool value is true and 100 if the "IsBusy" bool value is false. </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return boolValue ? 0 : 100;
            else
                throw new ArgumentException("Value passed to converter must be a bool");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
