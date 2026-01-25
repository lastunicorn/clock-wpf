using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

/// <summary>
/// A simple line clock hand. It is ususally used for displaying seconds.
/// It has a customizable tail length and pin diameter.
/// </summary>
public class SimpleHand : HandBase
{
    #region TailLength DependencyProperty

    public static readonly DependencyProperty TailLengthProperty = DependencyProperty.Register(
        nameof(TailLength),
        typeof(double),
        typeof(SimpleHand),
        new FrameworkPropertyMetadata(0.0));

    [Category("Appearance")]
    [DefaultValue(0.0)]
    [Description("The hand's length of the tail as percentage from the clock's radius.")]
    public double TailLength
    {
        get => (double)GetValue(TailLengthProperty);
        set => SetValue(TailLengthProperty, value);
    }

    #endregion

    #region PinDiameter DependencyProperty

    public static readonly DependencyProperty PinDiameterProperty = DependencyProperty.Register(
        nameof(PinDiameter),
        typeof(double),
        typeof(SimpleHand),
        new FrameworkPropertyMetadata(4.0));

    [Category("Appearance")]
    [DefaultValue(4.0)]
    [Description("The diameter of the pin as percentage from the clock's radius.")]
    public double PinDiameter
    {
        get => (double)GetValue(PinDiameterProperty);
        set => SetValue(PinDiameterProperty, value);
    }

    #endregion

    protected override Pen CreateStrokePen()
    {
        Pen pen = base.CreateStrokePen();

        if (pen != null)
        {

            pen.StartLineCap = PenLineCap.Round;
            pen.EndLineCap = PenLineCap.Round;
        }

        return pen;
    }

    public override void DoRender(ClockDrawingContext context)
    {
        DrawingPlan.Create(context.DrawingContext)
            .WithTransform(() =>
            {
                double angleDegrees = CalculateHandAngle(context.Time);
                return new RotateTransform(angleDegrees, 0, 0);
            })
            .Draw(dc =>
            {
                double radius = context.ClockDiameter / 2;

                DrawHandLine(dc, radius);
                DrawPin(dc, radius);
            });
    }

    private void DrawHandLine(DrawingContext drawingContext, double radius)
    {
        if (StrokePen == null)
            return;

        if (Length <= 0 && TailLength <= 0)
            return;

        double handLength = radius * (Length / 100.0);
        double tailLength = radius * (TailLength / 100.0);

        Point startPoint = new(0, tailLength);
        Point endPoint = new(0, -handLength);

        drawingContext.DrawLine(StrokePen, startPoint, endPoint);
    }

    private void DrawPin(DrawingContext drawingContext, double radius)
    {
        if (StrokeBrush == null)
            return;

        if (PinDiameter <= 0)
            return;

        double pinRadius = radius * (PinDiameter / 100.0) / 2;

        Point center = new(0, 0);
        drawingContext.DrawEllipse(StrokeBrush, null, center, pinRadius, pinRadius);
    }
}
