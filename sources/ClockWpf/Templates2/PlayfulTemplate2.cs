using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates2.Shapes;

namespace DustInTheWind.ClockWpf.Templates2;

public class PlayfulTemplate2 : ClockTemplate2
{
    protected override IEnumerable<ShapeT> CreateShapes()
    {
        yield return new FlatBackgroundT
        {
            Name = "Background"
        };

        yield return new TicksT
        {
            Name = "Minute Ticks",
            Length = 4,
            DistanceFromEdge = 6,
            RoundEnds = true,
            StrokeBrush = new SolidColorBrush(Color.FromRgb(0x60, 0x60, 0x60))
        };

        yield return new TicksT
        {
            Name = "Hour Ticks",
            Angle = 30,
            OffsetAngle = 30,
            StrokeThickness = 2,
            Length = 8,
            DistanceFromEdge = 7.5,
            RoundEnds = true,
            StrokeBrush = new SolidColorBrush(Color.FromRgb(0x40, 0x40, 0x40))
        };

        yield return new HourNumeralsT
        {
            Name = "Hour Numerals",
            FontFamily = new FontFamily("Arial Rounded MT"),
            DistanceFromEdge = 30
        };

        yield return new BarHandT
        {
            Name = "Hour Hand",
            TimeComponent = TimeComponent.Hour,
            Length = 55,
            Width = 10,
            TailLength = -18,
            StrokeThickness = 0,
            FillBrush = Brushes.RoyalBlue
        };

        yield return new BarHandT
        {
            Name = "Minute Hand",
            TimeComponent = TimeComponent.Minute,
            Length = 84,
            Width = 8,
            TailLength = -18,
            StrokeThickness = 0,
            FillBrush = Brushes.LimeGreen
        };

        yield return new SimpleLineHandT
        {
            Name = "Second Hand",
            TimeComponent = TimeComponent.Second,
            Length = 84,
            TailLength = -18,
            StrokeBrush = Brushes.Red,
            StrokeThickness = 1,
            RoundEnds = true
        };

        yield return new PinT()
        {
            Name = "Pin",
            Diameter = 24,
            FillBrush = Brushes.Red,
            StrokeThickness = 0
        };
    }
}
