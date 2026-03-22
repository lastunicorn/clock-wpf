using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Templates2.Shapes;
using DustInTheWind.ClockWpf.Utils;

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
            pin.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Appearance")]
    [DefaultValue(4.0)]
    [Description("The diameter of the pin, calculated as percentage from the clock's radius.")]
    public double Diameter
    {
        get => (double)GetValue(DiameterProperty);
        set => SetValue(DiameterProperty, value);
    }

    #endregion

    static Pin()
    {
        StrokeThicknessProperty.OverrideMetadata(typeof(Pin), new FrameworkPropertyMetadata(0.0));
    }

    private Point pinCenter;
    private double calculatedPinRadius;
    private Pen strokePen;

    protected override bool OnRendering(ClockDrawingContext context)
    {
        if (Diameter <= 0)
            return false;

        return base.OnRendering(context);
    }

    protected override void CalculateCache(ClockDrawingContext context)
    {
        base.CalculateCache(context);

        double calculatedPinDiameter = Diameter.RelativeTo(context.ClockRadius);
        calculatedPinRadius = calculatedPinDiameter / 2;
        pinCenter = new Point(0, 0);
        strokePen = CreateStrokePen(context);
    }

    protected override void DoRender(ClockDrawingContext context)
    {
        context.DrawingContext.DrawEllipse(FillBrush, strokePen, pinCenter, calculatedPinRadius, calculatedPinRadius);
    }

    public override void Import(ShapeT shapeT)
    {
        base.Import(shapeT);

        if (shapeT is not PinT pinT)
            return;

        Diameter = pinT.Diameter;
    }
}
