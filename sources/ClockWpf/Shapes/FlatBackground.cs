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

    private Pen strokePen;

    protected override void CalculateCache(ClockDrawingContext context)
    {
        base.CalculateCache(context);

        strokePen = CreateStrokePen(true);
    }

    public override void DoRender(ClockDrawingContext context)
    {
        Point center = new(0, 0);
        double backgroundRadius = (context.ClockDiameter - StrokeThickness) / 2;

        context.DrawingContext.DrawEllipse(FillBrush, strokePen, center, backgroundRadius, backgroundRadius);
    }
}
