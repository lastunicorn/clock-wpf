using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Templates;

public class Blade2Template : ClockTemplate
{
    protected override IEnumerable<Shape> CreateShapes()
    {
        GradientStopCollection gradientStops =
        [
            new GradientStop(Colors.WhiteSmoke, 0),
            new GradientStop(Colors.WhiteSmoke, 0.5),
            new GradientStop(Colors.LightGray, 1)
        ];

        LinearGradientBrush linearGradientBrush = new(gradientStops, new Point(0.25, 0), new Point(0.75, 1));

        yield return new FlatBackground
        {
            Name = "Background",
            FillBrush = linearGradientBrush
        };

        yield return new HourNumerals
        {
            Name = "Hour Numerals",
            Texts = ["3", "6", "9", "12"],
            Angle = 90,
            OffsetAngle = 90,
            FillBrush = Brushes.Black,
            DistanceFromEdge = 18
        };

        yield return new Ticks
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

        yield return new Blade2Hand
        {
            Name = "Hour Hand",
            TimeComponent = TimeComponent.Hour,
            FillBrush = Brushes.Black,
            Length = 60,
            HipDistance = 29,
            Width = 9,
            StrokeBrush = handBorderBrush,
            StrokeThickness = 0.1
        };

        yield return new Blade2Hand
        {
            Name = "Minute Hand",
            TimeComponent = TimeComponent.Minute,
            FillBrush = Brushes.Black,
            Length = 80,
            HipDistance = 43,
            TipLength = 18,
            Width = 9,
            StrokeBrush = handBorderBrush,
            StrokeThickness = 0.1
        };

        yield return new SimpleLineHand
        {
            Name = "Second Hand",
            Length = 70,
            StrokeThickness = 1.5,
            FillBrush = Brushes.Black,
            IntegralValue = true
        };

        yield return new Pin
        {
            Name = "Pin",
            Diameter = 13,
            FillBrush = Brushes.Black
        };
    }
}
