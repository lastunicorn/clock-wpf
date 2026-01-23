using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

public class FlatBackground : Shape
{
    static FlatBackground()
    {
        FillBrushProperty.OverrideMetadata(typeof(FlatBackground), new FrameworkPropertyMetadata(Brushes.WhiteSmoke));
        StrokeThicknessProperty.OverrideMetadata(typeof(FlatBackground), new FrameworkPropertyMetadata(0.0));
    }

    protected override bool OnRendering(ClockDrawingContext context)
    {
        if (FillBrush == null && StrokePen == null)
            return false;

        return base.OnRendering(context);
    }

    public override void DoRender(ClockDrawingContext context)
    {
        Point center = new(0, 0);
        double backgroundRadius = (context.ClockDiameter - StrokeThickness) / 2;

        context.DrawingContext.DrawEllipse(FillBrush, StrokePen, center, backgroundRadius, backgroundRadius);
    }
}
