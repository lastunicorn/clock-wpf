using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates2.Shapes;

namespace DustInTheWind.ClockWpf.Templates2;

public class PandaTemplate2 : ClockTemplate2
{
    protected override IEnumerable<ShapeT> CreateShapes()
    {
        yield return new FlatBackgroundT
        {
            Name = "Background",
            FillBrush = Brushes.White,
            StrokeBrush = Brushes.Black,
            StrokeThickness = 2
        };

        yield return new DotHandT
        {
            Name = "Hour Hand",
            TimeComponent = TimeComponent.Hour,
            Length = 50,
            Radius = 25,
            FillBrush = Brushes.Black,
            StrokeBrush = new SolidColorBrush(Color.FromRgb(0x64, 0x64, 0x64)),
            StrokeThickness = 1
        };

        yield return new DotHandT
        {
            Name = "Minute Hand",
            TimeComponent = TimeComponent.Minute,
            Length = 50,
            Radius = 15,
            FillBrush = Brushes.Black,
            StrokeBrush = new SolidColorBrush(Color.FromRgb(0x64, 0x64, 0x64)),
            StrokeThickness = 1
        };
    }
}
