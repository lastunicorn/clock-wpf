using System.Globalization;
using System.Windows.Data;

namespace DustInTheWind.ClockWpf.Utils;

public class TimeSpanToMillisecondsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is TimeSpan timespan)
            return timespan.TotalMilliseconds;

        return 0.0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double milliseconds)
            return TimeSpan.FromMilliseconds(milliseconds);

        return TimeSpan.Zero;
    }
}
