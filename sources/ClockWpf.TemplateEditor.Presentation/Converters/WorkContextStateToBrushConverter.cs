using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using DustInTheWind.ClockWpf.TemplateEditor.State;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.Converters;

public class WorkContextStateToBrushConverter : IValueConverter
{
    public Brush ClosedBrush { get; set; } = Brushes.Gray;

    public Brush NewBrush { get; set; } = Brushes.Green;

    public Brush UnmodifiedBrush { get; set; } = Brushes.Green;

    public Brush ModifiedBrush { get; set; } = Brushes.Orange;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is WorkContextState state)
        {
            return state switch
            {
                WorkContextState.Closed => ClosedBrush,
                WorkContextState.New => NewBrush,
                WorkContextState.Unmodified=> UnmodifiedBrush,
                WorkContextState.Modified => ModifiedBrush,
                _ => Brushes.Transparent
            };
        }

        return Brushes.Transparent;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
