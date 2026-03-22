using System.ComponentModel;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Templates.Shapes;

public abstract class RimBaseT : ShapeT
{
    public double DistanceFromEdge { get; set; }

    public double Angle { get; set; } = 30.0;

    public double OffsetAngle { get; set; }

    public uint MaxCoverageCount { get; set; }

    public uint MaxCoverageAngle { get; set; } = 360;

    public RimItemOrientation Orientation { get; set; } = RimItemOrientation.FaceIn;

    public int SkipIndex { get; set; }
}
