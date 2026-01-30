using System.ComponentModel;
using System.Windows;

namespace DustInTheWind.ClockWpf.Shapes;

public class Pin : Shape
{
    #region Diameter DependencyProperty

    public static readonly DependencyProperty DiameterProperty = DependencyProperty.Register(
        nameof(Diameter),
        typeof(double),
        typeof(Pin),
        new FrameworkPropertyMetadata(4.0, HandleDiameterChanged));

    private static void HandleDiameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Pin pin)
        {
            pin.InvalidateCache();
        }
    }

    [Category("Appearance")]
    [DefaultValue(4.0)]
    [Description("The diameter of the pin as percentage from the clock's radius.")]
    public double Diameter
    {
        get => (double)GetValue(DiameterProperty);
        set => SetValue(DiameterProperty, value);
    }

    #endregion

    protected override bool OnRendering(ClockDrawingContext context)
    {
        if (FillBrush == null && StrokePen == null)
            return false;

        if (Diameter <= 0)
            return false;

        return base.OnRendering(context);
    }

    Point pinCenter;
    double pinRadius;

    protected override void CalculateCache(ClockDrawingContext context)
    {
        base.CalculateCache(context);

        pinRadius = context.ClockRadius * (Diameter / 100.0) / 2;
        pinCenter = new Point(0, 0);
    }

    public override void DoRender(ClockDrawingContext context)
    {
        context.DrawingContext.DrawEllipse(FillBrush, StrokePen, pinCenter, pinRadius, pinRadius);
    }
}
