using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Utils;

namespace DustInTheWind.ClockWpf.Shapes;

public class SpecularReflection : Shape
{
    static SpecularReflection()
    {
        FillBrushProperty.OverrideMetadata(typeof(SpecularReflection), new FrameworkPropertyMetadata(Brushes.WhiteSmoke));
        StrokeThicknessProperty.OverrideMetadata(typeof(SpecularReflection), new FrameworkPropertyMetadata(0.0));
    }

    private Pen strokePen;

    public SpecularReflection()
    {
        FillBrush = new RadialGradientBrush(
        [
            new GradientStop(Color.FromArgb(0xa0, 0xff, 0xff, 0xff), 0),
            new GradientStop(Color.FromArgb(0xa0, 0xff, 0xff, 0xff), 0.9),
            new GradientStop(Color.FromArgb(0x10, 0xff, 0xff, 0xff), 1)
        ]);
    }

    protected override void CalculateCache(ClockDrawingContext context)
    {
        base.CalculateCache(context);

        strokePen = CreateStrokePen(true);
    }

    public override void DoRender(ClockDrawingContext context)
    {
        double x = -context.ClockRadius + 50.RelativeTo(context.ClockRadius) - 10;
        double y = -context.ClockRadius + 50.RelativeTo(context.ClockRadius) + 20;

        Point center = new(x, y);
        double radiusX = 35.RelativeTo(context.ClockRadius);
        double radiusY = 15.RelativeTo(context.ClockRadius);

        context.DrawingContext.PushTransform(new RotateTransform(-65, center.X, center.Y));
        context.DrawingContext.DrawEllipse(FillBrush, strokePen, center, radiusX, radiusY);
        context.DrawingContext.Pop();
    }
}