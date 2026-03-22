using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates2.Shapes;

namespace DustInTheWind.ClockWpf.Templates2;

public class DefaultBlackTemplate : ClockTemplate
{
    protected override IEnumerable<ShapeT> CreateShapes()
    {
        yield return new FlatBackgroundT()
        {
            Name = "Background",
            FillBrush = Brushes.Black
        };

        SolidColorBrush minuteTicksBrush = new(Color.FromRgb(0x60, 0x60, 0x60));

        if (minuteTicksBrush.CanFreeze)
            minuteTicksBrush.Freeze();

        yield return new TicksT()
        {
            Name = "Minute Ticks",
            StrokeBrush = minuteTicksBrush
        };

        yield return new TicksT()
        {
            Name = "Hour Ticks",
            Angle = 30,
            OffsetAngle = 30,
            StrokeThickness = 1.5,
            StrokeBrush = Brushes.WhiteSmoke
        };

        yield return new HourNumeralsT()
        {
            Name = "Hour Numerals",
            FillBrush = Brushes.WhiteSmoke,
            DistanceFromEdge = 26
        };

        yield return new BarHandT()
        {
            Name = "Hour Hand",
            TimeComponent = TimeComponent.Hour,
            Length = 48,
            Width = 8,
            TailLength = 4,
            FillBrush = Brushes.WhiteSmoke,
            StrokeThickness = 0
        };

        yield return new BarHandT()
        {
            Name = "Minute Hand",
            TimeComponent = TimeComponent.Minute,
            Length = 85,
            Width = 8,
            TailLength = 4,
            FillBrush = Brushes.WhiteSmoke,
            StrokeThickness = 0
        };

        yield return new LineHandT()
        {
            Name = "Second Hand",
            TimeComponent = TimeComponent.Second,
            Length = 96.5,
            TailLength = 24,
            StrokeBrush = Brushes.OrangeRed,
            StrokeThickness = 1,
            IntegralValue = true
        };

        yield return new PinT()
        {
            Name = "Pin",
            Diameter = 8,
            FillBrush = Brushes.OrangeRed
        };
    }
}
