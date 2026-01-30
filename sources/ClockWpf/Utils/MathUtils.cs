using System.Windows;

namespace DustInTheWind.ClockWpf.Utils;

internal static class MathUtils
{
    public static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }

    public static double CalculateRadius(Rect ellipse, double angleDegrees)
    {
        double angleRadians = ToRadians(angleDegrees);

        // Calculate the radius at this angle for an ellipse using the formula:
        // r = (a * b) / sqrt(a² * sin(θ)² + b² * cos(θ)²)

        // Becasue the angleDegrees is measured from the vertical axis, we swap a and b in the formula:
        // a - The horizontal radius of ellipse
        // b - The vertical radius of ellipse
        // θ - The angle in radians from the horizontal axis mewasured counterclockwise

        double a = ellipse.Width / 2;
        double b = ellipse.Height / 2;

        double cosAngle = Math.Cos(angleRadians);
        double sinAngle = Math.Sin(angleRadians);

        return (a * b) / Math.Sqrt(a * a * sinAngle * sinAngle + b * b * cosAngle * cosAngle);
    }
}
