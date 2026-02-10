using System.Windows;

namespace DustInTheWind.ClockWpf.Utils;

public static class DoubleExtensions
{
    public static double RelativeTo(this double value, double referenceValue)
    {
        return referenceValue * value / 100.0;
    }

    public static double RelativeTo(this int value, int referenceValue)
    {
        return referenceValue * value / 100.0;
    }

    public static double RelativeTo(this double value, int referenceValue)
    {
        return referenceValue * value / 100.0;
    }

    public static double RelativeTo(this int value, double referenceValue)
    {
        return referenceValue * value / 100.0;
    }

    public static Point RelativeTo(this Point value, double referenceValue)
    {
        double x = referenceValue * value.X / 100.0;
        double y = referenceValue * value.Y / 100.0;

        return new Point(x, y);
    }
}
