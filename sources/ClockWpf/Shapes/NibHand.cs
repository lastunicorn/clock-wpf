using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

/// <summary>
/// Draws a fancy clock hand that resembles a nib.
/// </summary>
public class NibHand : HandBase
{
    #region Width DependencyProperty

    public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
        nameof(Width),
        typeof(double),
        typeof(NibHand),
        new FrameworkPropertyMetadata(5.0));

    [Category("Appearance")]
    [DefaultValue(5.0)]
    [Description("The width of the hand.")]
    public double Width
    {
        get => (double)GetValue(WidthProperty);
        set => SetValue(WidthProperty, value);
    }

    #endregion

    #region KeepProportions DependencyProperty

    public static readonly DependencyProperty KeepProportionsProperty = DependencyProperty.Register(
        nameof(KeepProportions),
        typeof(bool),
        typeof(NibHand),
        new FrameworkPropertyMetadata(true));

    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("Specifies if the hand should keep its proportions when its length is changed.")]
    public bool KeepProportions
    {
        get => (bool)GetValue(KeepProportionsProperty);
        set => SetValue(KeepProportionsProperty, value);
    }

    #endregion

    public override void DoRender(ClockDrawingContext context)
    {
        context.DrawingContext.CreateDrawingPlan()
            .WithTransform(() =>
            {
                double angleDegrees = CalculateHandAngle(context.Time);
                return new RotateTransform(angleDegrees, 0, 0);
            })
            .Draw(dc =>
            {
                if (FillBrush == null && StrokePen == null)
                    return;

                double radius = context.ClockDiameter / 2;
                double scaleFactorY = Length > 0 ? Length / 280.0 : 1.0;
                double scaleFactorX = KeepProportions ? scaleFactorY : (Width > 0 ? Width / 30.0 : 1.0);

                dc.PushTransform(new ScaleTransform(scaleFactorX, scaleFactorY));

                PathGeometry nibGeometry = CreateNibGeometry();

                dc.DrawGeometry(FillBrush, StrokePen, nibGeometry);

                dc.Pop();
            });
    }

    private PathGeometry CreateNibGeometry()
    {
        PathFigure figure = new()
        {
            IsClosed = true
        };

        // First arc: RectangleF(-12f, 43f, 24f, 24f), -60f, 300f
        // This is an arc from angle -60° for 300° on a circle centered at (0, 55) with radius 12
        // Start point at angle -60°: (12*cos(-60°), 55 + 12*sin(-60°)) = (6, 55 - 10.39) ≈ (6, 44.6)
        // End point at angle 240°: (12*cos(240°), 55 + 12*sin(240°)) = (-6, 55 - 10.39) ≈ (-6, 44.6)
        double startAngle = -60;
        double centerY = 43 + 12; // 55
        double startX = 12 * Math.Cos(startAngle * Math.PI / 180);
        double startY = centerY + 12 * Math.Sin(startAngle * Math.PI / 180);
        
        figure.StartPoint = new Point(startX, startY);

        double endAngle = startAngle + 300; // 240°
        double endX = 12 * Math.Cos(endAngle * Math.PI / 180);
        double endY = centerY + 12 * Math.Sin(endAngle * Math.PI / 180);

        figure.Segments.Add(new ArcSegment(
            new Point(endX, endY),
            new Size(12, 12),
            0,
            true, // isLargeArc (300° is > 180°)
            SweepDirection.Clockwise,
            true
        ));

        // AddCurve with 2 points creates a curve from current point through these points
        figure.Segments.Add(new BezierSegment(
            new Point(-6, 37),
            new Point(-6, 36),
            new Point(-8, 35),
            true
        ));

        // AddCurve with 3 points
        figure.Segments.Add(new BezierSegment(
            new Point(-6, 32),
            new Point(-4, 20),
            new Point(-2, 11),
            true
        ));

        // AddArc: RectangleF(-12f, -13f, 24, 24), 90, 90
        // Center at (0, -1), radius 12, from 90° for 90° (ends at 180°)
        // Start at (0, 11), end at (-12, -1)
        figure.Segments.Add(new ArcSegment(
            new Point(-12, -1),
            new Size(12, 12),
            0,
            false,
            SweepDirection.Clockwise,
            true
        ));

        // AddCurve with 3 points
        figure.Segments.Add(new BezierSegment(
            new Point(-7, -40),
            new Point(-6, -90),
            new Point(-10, -119),
            true
        ));

        // AddCurve with 3 points
        figure.Segments.Add(new BezierSegment(
            new Point(-7.5, -121.5),
            new Point(-5, -125.5),
            new Point(-3, -129),
            true
        ));

        // AddArc: RectangleF(-15f, -159f, 30f, 30f), 90f, 90f
        // Center at (0, -144), radius 15, from 90° for 90° (ends at 180°)
        // Start at (0, -129), end at (-15, -144)
        figure.Segments.Add(new ArcSegment(
            new Point(-15, -144),
            new Size(15, 15),
            0,
            false,
            SweepDirection.Clockwise,
            true
        ));

        // AddCurve with 3 points
        figure.Segments.Add(new BezierSegment(
            new Point(-8.5, -175),
            new Point(-2, -215),
            new Point(-1, -249),
            true
        ));

        figure.Segments.Add(new LineSegment(new Point(-1, -280), true));

        // Center line
        figure.Segments.Add(new LineSegment(new Point(1, -280), true));

        figure.Segments.Add(new LineSegment(new Point(1, -249), true));

        // AddCurve with 3 points
        figure.Segments.Add(new BezierSegment(
            new Point(2, -215),
            new Point(8.5, -175),
            new Point(15, -144),
            true
        ));

        // AddArc: RectangleF(-15f, -159f, 30f, 30f), 0f, 90f
        // Center at (0, -144), radius 15, from 0° for 90° (ends at 90°)
        // Start at (15, -144), end at (0, -129)
        figure.Segments.Add(new ArcSegment(
            new Point(0, -129),
            new Size(15, 15),
            0,
            false,
            SweepDirection.Clockwise,
            true
        ));

        // AddCurve with 3 points
        figure.Segments.Add(new BezierSegment(
            new Point(5, -125.5),
            new Point(7.5, -121.5),
            new Point(10, -119),
            true
        ));

        // AddCurve with 3 points
        figure.Segments.Add(new BezierSegment(
            new Point(6, -90),
            new Point(7, -40),
            new Point(12, -1),
            true
        ));

        // AddArc: RectangleF(-12f, -13f, 24f, 24), 0f, 90f
        // Center at (0, -1), radius 12, from 0° for 90° (ends at 90°)
        // Start at (12, -1), end at (0, 11)
        figure.Segments.Add(new ArcSegment(
            new Point(0, 11),
            new Size(12, 12),
            0,
            false,
            SweepDirection.Clockwise,
            true
        ));

        // AddCurve with 3 points
        figure.Segments.Add(new BezierSegment(
            new Point(4, 20),
            new Point(6, 32),
            new Point(8, 35),
            true
        ));

        // AddCurve with 3 points
        double finalX = 10 * Math.Cos(Math.PI / 3.0);
        double finalY = 41 * Math.Sin(Math.PI / 3.0);
        figure.Segments.Add(new BezierSegment(
            new Point(6, 36),
            new Point(6, 37),
            new Point(finalX, finalY),
            true
        ));

        PathGeometry geometry = new();
        geometry.Figures.Add(figure);
        return geometry;
    }
}
