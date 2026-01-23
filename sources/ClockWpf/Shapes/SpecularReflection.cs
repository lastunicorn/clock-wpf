using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

public class SpecularReflection : Shape
{
    static SpecularReflection()
    {
        FillBrushProperty.OverrideMetadata(typeof(SpecularReflection), new FrameworkPropertyMetadata(Brushes.WhiteSmoke));
        StrokeThicknessProperty.OverrideMetadata(typeof(SpecularReflection), new FrameworkPropertyMetadata(0.0));
    }

    public SpecularReflection()
    {
        FillBrush = new RadialGradientBrush(
        [
            new GradientStop(Color.FromArgb(0xa0, 0xff, 0xff, 0xff), 0),
            new GradientStop(Color.FromArgb(0xa0, 0xff, 0xff, 0xff), 0.9),
            new GradientStop(Color.FromArgb(0x10, 0xff, 0xff, 0xff), 1)
        ]);
    }

    protected override bool OnRendering(ClockDrawingContext context)
    {
        if (FillBrush == null && StrokePen == null)
            return false;

        return base.OnRendering(context);
    }

    public override void DoRender(ClockDrawingContext context)
    {
        double x = -context.ClockRadius + (context.ClockRadius * 50) / 100 - 10;
        double y = -context.ClockRadius + (context.ClockRadius * 50) / 100 + 20;

        Point center = new(x, y);
        double radiusX = (context.ClockRadius * 35) / 100;
        double radiusY = (context.ClockRadius * 15) / 100;

        context.DrawingContext.PushTransform(new RotateTransform(-65, center.X, center.Y));
        context.DrawingContext.DrawEllipse(FillBrush, StrokePen, center, radiusX, radiusY);
        context.DrawingContext.Pop();
    }
}