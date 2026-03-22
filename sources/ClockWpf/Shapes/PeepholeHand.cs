using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Templates.Shapes;
using DustInTheWind.ClockWpf.Utils;

namespace DustInTheWind.ClockWpf.Shapes;

/// <summary>
/// A clock's hand that is actually a big disk with a rectangle slot carved in it through
/// which the user can see whatever is under the disk. Usually, the hours would be visible
/// under the slot.
/// </summary>
public class PeepholeHand : HandBase
{
    /// <summary>
    /// The default name for the hand.
    /// </summary>
    public const string DefaultName = "Peephole Hand";

    #region Width Dependency Property

    public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
        nameof(Width),
        typeof(double),
        typeof(PeepholeHand),
        new FrameworkPropertyMetadata(10.0, HandleWidthChanged));

    private static void HandleWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PeepholeHand slotHand)
        {
            slotHand.InvalidateCache();
            slotHand.OnChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets or sets the width of the slot carved inside the disk.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(10.0)]
    [Description("The width of the slot carved inside the disk, calculated as percentage from the clock's radius.")]
    public double Width
    {
        get => (double)GetValue(WidthProperty);
        set => SetValue(WidthProperty, value);
    }

    #endregion

    #region Radius Dependency Property

    public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
        nameof(Radius),
        typeof(double),
        typeof(PeepholeHand),
        new FrameworkPropertyMetadata(100.0, HandleRadiusChanged));

    private static void HandleRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PeepholeHand slotHand)
        {
            slotHand.InvalidateCache();
            slotHand.OnChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets or sets the radius of the opaque disk.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(100.0)]
    [Description("The radius of the opaque disk, calculated as percentage from the clock's radius.")]
    public double Radius
    {
        get => (double)GetValue(RadiusProperty);
        set => SetValue(RadiusProperty, value);
    }

    #endregion

    #region TailLength Dependency Property

    public static readonly DependencyProperty TailLengthProperty = DependencyProperty.Register(
        nameof(TailLength),
        typeof(double),
        typeof(PeepholeHand),
        new FrameworkPropertyMetadata(0.0, HandleTailLengthChanged));

    private static void HandleTailLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PeepholeHand slotHand)
        {
            slotHand.InvalidateCache();
            slotHand.OnChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets or sets the length of the tail of the hand.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(12.0)]
    [Description("The length of the hand's tail, calculated as percentage from the clock's radius.")]
    public double TailLength
    {
        get => (double)GetValue(TailLengthProperty);
        set => SetValue(TailLengthProperty, value);
    }

    #endregion

    private StreamGeometry geometry;

    /// <summary>
    /// Initializes a new instance of the <see cref="PeepholeHand"/> class with
    /// default values.
    /// </summary>
    public PeepholeHand()
    {
        Name = DefaultName;
    }

    /// <summary>
    /// Performs all the necessary calculations based on the public parameters, before drawing the shape.
    /// </summary>
    protected override void CalculateCache(ClockDrawingContext context)
    {
        base.CalculateCache(context);

        double radius = context.ClockRadius;

        StreamGeometry geometry = new();
        using StreamGeometryContext geometryContext = geometry.Open();

        // Circular Disk

        double calculatedDiskRadius = Radius.RelativeTo(radius);

        geometryContext.BeginFigure(new Point(-calculatedDiskRadius, 0), true, true);

        geometryContext.ArcTo(
            point: new Point(calculatedDiskRadius, 0),
            size: new Size(calculatedDiskRadius, calculatedDiskRadius),
            rotationAngle: 0,
            isLargeArc: true,
            sweepDirection: SweepDirection.Clockwise,
            isStroked: true,
            isSmoothJoin: false);

        geometryContext.ArcTo(
            point: new Point(-calculatedDiskRadius, 0),
            size: new Size(calculatedDiskRadius, calculatedDiskRadius),
            rotationAngle: 0,
            isLargeArc: true,
            sweepDirection: SweepDirection.Clockwise,
            isStroked: true,
            isSmoothJoin: false);

        // Rectangle Slot

        double calculatedLength = Length.RelativeTo(radius);
        double calculatedTailLength = TailLength.RelativeTo(radius);
        double calculatedWidth = Width.RelativeTo(radius);
        double calculatedHalfWidth = calculatedWidth / 2;

        Point rectanglePoint1 = new(-calculatedHalfWidth, calculatedTailLength);
        Point rectanglePoint2 = new(-calculatedHalfWidth, -calculatedLength);
        Point rectanglePoint3 = new(calculatedHalfWidth, -calculatedLength);
        Point rectanglePoint4 = new(calculatedHalfWidth, calculatedTailLength);

        geometryContext.BeginFigure(rectanglePoint1, true, true);
        geometryContext.LineTo(rectanglePoint2, true, false);
        geometryContext.LineTo(rectanglePoint3, true, false);
        geometryContext.LineTo(rectanglePoint4, true, false);

        // Finish

        geometryContext.Close();

        if (geometry.CanFreeze)
            geometry.Freeze();

        this.geometry = geometry;
    }

    protected override void DoRenderHand(ClockDrawingContext context)
    {
        context.DrawingContext.DrawGeometry(FillBrush, null, geometry);
    }

    public override void Import(ShapeT shapeT)
    {
        base.Import(shapeT);

        if (shapeT is not PeepholeHandT peepholeHandT)
            return;

        Width = peepholeHandT.Width;
        Radius = peepholeHandT.Radius;
        TailLength = peepholeHandT.TailLength;
    }
}
