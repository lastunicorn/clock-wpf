using System.Globalization;
using System.Windows.Data;

namespace DustInTheWind.ClockWpf.Converters;

public class NegateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double doubleValue)
            return -doubleValue;

        if (value is int intValue)
            return -intValue;

        if (value is float floatValue)
            return -floatValue;

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Convert(value, targetType, parameter, culture);
    }
}
