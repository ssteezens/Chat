using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ChatWpf.Converters
{
    /// <summary>
    ///     Converts a bool to a visibility.
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(!(value is bool))
                throw new ArgumentException("Value passed to converter must be a bool.");

            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
