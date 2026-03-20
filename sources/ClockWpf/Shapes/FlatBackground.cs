using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

/// <summary>
/// Represents a flat, circular background shape for use in <see cref="AnalogClock"/>.
/// </summary>
/// <remarks>
/// This class provides a simple, filled circular background with no visible stroke by default.
/// <remarks>
public class FlatBackground : Shape
{
    static FlatBackground()
    {
        FillBrushProperty.OverrideMetadata(typeof(FlatBackground), new FrameworkPropertyMetadata(Brushes.WhiteSmoke));
        StrokeThicknessProperty.OverrideMetadata(typeof(FlatBackground), new FrameworkPropertyMetadata(0.0));
    }

    private Pen strokePen;
    private Point center = new(0, 0);
    private double backgroundRadius;

    protected override void CalculateCache(ClockDrawingContext context)
    {
        base.CalculateCache(context);

        strokePen = CreateStrokePen(context);
        backgroundRadius = (context.ClockDiameter - StrokeThickness) / 2;
    }

    protected override void DoRender(ClockDrawingContext context)
    {
        context.DrawingContext.DrawEllipse(FillBrush, strokePen, center, backgroundRadius, backgroundRadius);
    }
}
