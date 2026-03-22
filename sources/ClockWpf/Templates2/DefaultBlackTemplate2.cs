using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates2.Shapes;

namespace DustInTheWind.ClockWpf.Templates2;

public class DefaultBlackTemplate2 : ClockTemplate2
{
    protected override IEnumerable<ShapeT> CreateShapes()
    {
        FlatBackgroundT background = new()
        {
            Name = "Background",
            FillBrush = Brushes.Black
        };
        yield return background;

        SolidColorBrush minuteTicksBrush = new(Color.FromRgb(0x60, 0x60, 0x60));

        if (minuteTicksBrush.CanFreeze)
            minuteTicksBrush.Freeze();

        TicksT minuteTicks = new()
        {
            Name = "Minute Ticks",
            StrokeBrush = minuteTicksBrush
        };
        yield return minuteTicks;

        TicksT hourTicks = new()
        {
            Name = "Hour Ticks",
            Angle = 30,
            OffsetAngle = 30,
            StrokeThickness = 1.5,
            StrokeBrush = Brushes.WhiteSmoke
        };
        yield return hourTicks;

        HourNumeralsT hourNumerals = new()
        {
            Name = "Hour Numerals",
            FillBrush = Brushes.WhiteSmoke,
            DistanceFromEdge = 26
        };
        yield return hourNumerals;

        BarHandT hourHand = new()
        {
            Name = "Hour Hand",
            TimeComponent = TimeComponent.Hour,
            Length = 48,
            Width = 8,
            TailLength = 4,
            FillBrush = Brushes.WhiteSmoke,
            StrokeThickness = 0
        };
        yield return hourHand;

        BarHandT minuteHand = new()
        {
            Name = "Minute Hand",
            TimeComponent = TimeComponent.Minute,
            Length = 85,
            Width = 8,
            TailLength = 4,
            FillBrush = Brushes.WhiteSmoke,
            StrokeThickness = 0
        };
        yield return minuteHand;

        SimpleLineHandT secondHand = new()
        {
            Name = "Second Hand",
            TimeComponent = TimeComponent.Second,
            Length = 96.5,
            TailLength = 24,
            StrokeBrush = Brushes.OrangeRed,
            StrokeThickness = 1,
            IntegralValue = true
        };
        yield return secondHand;

        PinT pin = new()
        {
            Name = "Pin",
            Diameter = 8,
            FillBrush = Brushes.OrangeRed
        };
        yield return pin;
    }
}
