using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class TicksT : RimBaseT
{
    public double Length { get; set; } = 5.0;

    public bool RoundEnds { get; set; }

    public TicksT()
    {
        Name = "Ticks";
        DistanceFromEdge = 6.0;
        Angle = 6.0;
        OffsetAngle = 6.0;
        Orientation = RimItemOrientation.FaceIn;
    }
}
