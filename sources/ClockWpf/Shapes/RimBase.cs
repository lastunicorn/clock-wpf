using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Utils;

namespace DustInTheWind.ClockWpf.Shapes;

/// <summary>
/// The base class for rim shape that display items around the clock face.
/// </summary>
/// <remarks>
/// When inherinting this class, overwrite the <see cref="RenderItem"/> method to draw an item.
/// This method is called for each item that must be drawn around the clock face.
/// The position and orientation of the item is already set when this method is called.
/// The item should be drawn centered at the point (0,0).
/// </remarks>
public abstract class RimBase : Shape
{
    #region DistanceFromEdge DependencyProperty

    public static readonly DependencyProperty DistanceFromEdgeProperty = DependencyProperty.Register(
        nameof(DistanceFromEdge),
        typeof(double),
        typeof(RimBase),
        new FrameworkPropertyMetadata(0.0, HandleDistanceFromEdgeChanged));

    private static void HandleDistanceFromEdgeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RimBase rimBase)
        {
            rimBase.InvalidateCache();
            rimBase.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Layout")]
    [DefaultValue(6.0)]
    [Description("The hand's length of the tail as percentage from the clock's radius.")]
    public double DistanceFromEdge
    {
        get => (double)GetValue(DistanceFromEdgeProperty);
        set => SetValue(DistanceFromEdgeProperty, value);
    }

    #endregion

    #region Angle DependencyProperty

    public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(
        nameof(Angle),
        typeof(double),
        typeof(RimBase),
        new FrameworkPropertyMetadata(30.0, HandleAngleChanged));

    private static void HandleAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RimBase rimBase)
        {
            rimBase.InvalidateCache();
            rimBase.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Layout")]
    [DefaultValue(30)]
    [Description("The angle, in degrees, between two consecutive instances of the shape.")]
    public double Angle
    {
        get => (double)GetValue(AngleProperty);
        set => SetValue(AngleProperty, value);
    }

    #endregion

    #region OffsetAngle DependencyProperty

    public static readonly DependencyProperty OffsetAngleProperty = DependencyProperty.Register(
        nameof(OffsetAngle),
        typeof(double),
        typeof(RimBase),
        new FrameworkPropertyMetadata(0.0, HandleOffsetAngleChanged));

    private static void HandleOffsetAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RimBase rimBase)
        {
            rimBase.InvalidateCache();
            rimBase.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Layout")]
    [DefaultValue(0.0)]
    [Description("The angle, in degrees, between north and the first item that is displayed.")]
    public double OffsetAngle
    {
        get => (double)GetValue(OffsetAngleProperty);
        set => SetValue(OffsetAngleProperty, value);
    }

    #endregion

    #region MaxCoverageCount DependencyProperty

    public static readonly DependencyProperty MaxCoverageCountProperty = DependencyProperty.Register(
        nameof(MaxCoverageCount),
        typeof(uint),
        typeof(RimBase),
        new FrameworkPropertyMetadata((uint)0, HandleMaxCoverageCountChanged));

    private static void HandleMaxCoverageCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RimBase rimBase)
        {
            rimBase.InvalidateCache();
            rimBase.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Layout")]
    [DefaultValue((uint)0)]
    [Description("The maximum number of items to be drawn around the dial.")]
    public uint MaxCoverageCount
    {
        get => (uint)GetValue(MaxCoverageCountProperty);
        set => SetValue(MaxCoverageCountProperty, value);
    }

    #endregion

    #region MaxCoverageAngle DependencyProperty

    public static readonly DependencyProperty MaxCoverageAngleProperty = DependencyProperty.Register(
        nameof(MaxCoverageAngle),
        typeof(uint),
        typeof(RimBase),
        new FrameworkPropertyMetadata((uint)360, HandleMaxCoverageAngleChanged));

    private static void HandleMaxCoverageAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RimBase rimBase)
        {
            rimBase.InvalidateCache();
            rimBase.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Layout")]
    [DefaultValue((uint)360)]
    [Description("The maximum angle, in degrees, that items should cover around the dial.")]
    public uint MaxCoverageAngle
    {
        get => (uint)GetValue(MaxCoverageAngleProperty);
        set => SetValue(MaxCoverageAngleProperty, value);
    }

    #endregion

    #region Orientation DependencyProperty

    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
        nameof(Orientation),
        typeof(RimItemOrientation),
        typeof(RimBase),
        new FrameworkPropertyMetadata(RimItemOrientation.FaceIn, HandleOrientationChanged));

    private static void HandleOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RimBase rimBase)
        {
            rimBase.InvalidateCache();
            rimBase.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Layout")]
    [DefaultValue(RimItemOrientation.FaceIn)]
    [Description("Specifies the orientation of an item.")]
    public RimItemOrientation Orientation
    {
        get => (RimItemOrientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    #endregion

    #region SkipIndex DependencyProperty

    public static readonly DependencyProperty SkipIndexProperty = DependencyProperty.Register(
        nameof(SkipIndex),
        typeof(int),
        typeof(RimBase),
        new FrameworkPropertyMetadata(0, HandleSkipIndexChanged));

    private static void HandleSkipIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RimBase rimBase)
        {
            rimBase.InvalidateCache();
            rimBase.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Behavior")]
    [DefaultValue(0)]
    [Description("The index of the item that should not be skiped. Also, the multiples of this index are skipped.")]
    public int SkipIndex
    {
        get => (int)GetValue(SkipIndexProperty);
        set => SetValue(SkipIndexProperty, value);
    }

    #endregion

    public override void DoRender(ClockDrawingContext context)
    {
        double radius = context.ClockRadius;
        double calculatedDistanceFromEdge = DistanceFromEdge.RelativeTo(radius);
        double rimRadius = radius - calculatedDistanceFromEdge;

        int index = 0;
        double currentAngle = OffsetAngle + Angle * index;

        while (currentAngle >= 0)
        {
            if (MaxCoverageCount > 0 && index >= MaxCoverageCount)
                break;

            if (MaxCoverageAngle > 0 && currentAngle - OffsetAngle >= MaxCoverageAngle)
                break;

            bool shouldSkip = SkipIndex > 0 && (index + 1) % SkipIndex == 0;

            if (!shouldSkip)
            {
                DrawingPlan.Create(context.DrawingContext)
                    .WithTransform(() => new RotateTransform(currentAngle, 0, 0))
                    .WithTransform(() => new TranslateTransform(0, -rimRadius))
                    .WithTransform(() => CreateItemOrientationTransform(index))
                    .Draw(cd => RenderItem(context, index));
            }

            index++;
            currentAngle = OffsetAngle + Angle * index;
        }
    }

    private RotateTransform CreateItemOrientationTransform(int index)
    {
        switch (Orientation)
        {
            case RimItemOrientation.Normal:
                {
                    double currentAngle = OffsetAngle + Angle * index;
                    return new RotateTransform(-currentAngle, 0, 0);
                }

            default:
            case RimItemOrientation.FaceIn:
                return null;

            case RimItemOrientation.FaceOut:
                return new RotateTransform(180, 0, 0);

            case RimItemOrientation.HalfInHalfOut:
                {
                    double currentAngle = OffsetAngle + Angle * index;
                    double normalizedAngle = currentAngle % 360;

                    return normalizedAngle > 90 && normalizedAngle < 270
                        ? new RotateTransform(180, 0, 0)
                        : null;
                }

            case RimItemOrientation.Custom:
                return OnItemOrientation(index);
        }
    }

    /// <summary>
    /// Provides a custom orientation transform for the item at the specified index.
    /// </summary>
    /// <remarks>Override this method to supply a specific orientation for individual items. The default
    /// implementation returns <c>null</c>, indicating no rotation is applied.</remarks>
    /// <param name="index">The zero-based index of the item for which to retrieve the orientation transform.</param>
    /// <returns>A <see cref="RotateTransform"/> representing the orientation of the item at the specified index, or <c>null</c>
    /// if no orientation is applied.</returns>
    protected virtual RotateTransform OnItemOrientation(int index)
    {
        return null;
    }

    /// <summary>
    /// Draws the item at the specified index using the provided drawing context.
    /// </summary>
    /// <remarks>
    /// This method is called once for each item that must be drawn around the clock face.
    /// The position and orientation of the item is already set when this method is called.
    /// The item should be drawn centered at the point (0,0).
    /// </remarks>
    /// <param name="context">The drawing context to use for rendering the item.</param>
    /// <param name="index">The zero-based index of the item to render.</param>
    protected abstract void RenderItem(ClockDrawingContext context, int index);
}