using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates2.Shapes;

namespace DustInTheWind.ClockWpf.Templates2;

public class BladeTemplate : ClockTemplate
{
    protected override IEnumerable<ShapeT> CreateShapes()
    {
        GradientStopCollection gradientStops = new()
        {
            new GradientStop(Colors.WhiteSmoke, 0),
            new GradientStop(Colors.WhiteSmoke, 0.5),
            new GradientStop(Colors.LightGray, 1)
        };

        LinearGradientBrush linearGradientBrush = new(gradientStops, new Point(0.25, 0), new Point(0.75, 1));

        yield return new FlatBackgroundT
        {
            Name = "Background",
            FillBrush = linearGradientBrush
        };

        yield return new HourNumeralsT
        {
            Name = "Hour Numerals",
            Texts = ["3", "6", "9", "12"],
            Angle = 90,
            OffsetAngle = 90,
            FillBrush = Brushes.Black,
            DistanceFromEdge = 18
        };

        yield return new TicksT
        {
            Name = "Ticks",
            Angle = 30,
            OffsetAngle = 30,
            FillBrush = Brushes.Black,
            DistanceFromEdge = 18,
            SkipIndex = 3
        };

        SolidColorBrush handBorderBrush = new(Color.FromRgb(0x90, 0x90, 0x90));
        if (handBorderBrush.CanFreeze)
            handBorderBrush.Freeze();

        yield return new BladeHandT
        {
            Name = "Hour Hand",
            TimeComponent = TimeComponent.Hour,
            FillBrush = Brushes.Black,
            Length = 60,
            HipDistance = 13,
            Width = 17,
            StrokeBrush = handBorderBrush,
            StrokeThickness = 0.1
        };

        yield return new BladeHandT
        {
            Name = "Minute Hand",
            TimeComponent = TimeComponent.Minute,
            FillBrush = Brushes.Black,
            Length = 80,
            HipDistance = 10,
            Width = 10,
            StrokeBrush = handBorderBrush,
            StrokeThickness = 0.1
        };

        yield return new LineHandT
        {
            Name = "Second Hand",
            TimeComponent = TimeComponent.Second,
            Length = 70,
            StrokeThickness = 1.5,
            FillBrush = Brushes.Black,
            IntegralValue = true
        };

        yield return new PinT
        {
            Name = "Pin",
            Diameter = 13,
            FillBrush = Brushes.Black
        };
    }
}
