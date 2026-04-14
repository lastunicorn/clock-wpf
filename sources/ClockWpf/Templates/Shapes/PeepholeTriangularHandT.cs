namespace DustInTheWind.ClockWpf.Templates.Shapes;

public class PeepholeTriangularHandT : HandT
{
    public double SlotAngle { get; set; } = 10.0;

    public double Radius { get; set; } = 100.0;

    public double TailLength { get; set; }

    public double ShadowMargin { get; set; } = 2.0;

    public PeepholeTriangularHandT()
    {
        Name = "Peephole Triangular Hand";
    }
}
