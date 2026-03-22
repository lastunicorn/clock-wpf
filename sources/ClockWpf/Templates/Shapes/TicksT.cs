using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Templates.Shapes;

/// <summary>
/// Represents a tick mark element for a rim, providing properties to configure its appearance
/// such as length and whether its ends are rounded.
/// </summary>
public class TicksT : RimBaseT
{
    /// <summary>
    /// Gets or sets the length of the tick line.
    /// </summary>
    public double Length { get; set; } = 5.0;

    /// <summary>
    /// Gets or sets a value indicating whether the ends of the tick line should be rounded.
    /// </summary>
    public bool RoundEnds { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TicksT"/> class with default property values.
    /// </summary>
    public TicksT()
    {
        Name = "Ticks";
        DistanceFromEdge = 6.0;
        Angle = 6.0;
        OffsetAngle = 6.0;
        Orientation = RimItemOrientation.FaceIn;
    }
}
