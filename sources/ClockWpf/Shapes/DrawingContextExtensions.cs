using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

internal static class DrawingContextExtensions
{
    public static DrawingPlan CreateDrawingPlan(this DrawingContext drawingContext)
    {
        ArgumentNullException.ThrowIfNull(drawingContext);

        return new DrawingPlan(drawingContext);
    }
}
