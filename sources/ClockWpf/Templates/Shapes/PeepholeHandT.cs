namespace DustInTheWind.ClockWpf.Templates.Shapes;

public class PeepholeHandT : HandT
{
    public double Width { get; set; } = 10.0;

    public double Radius { get; set; } = 100.0;

    public double TailLength { get; set; }

    public PeepholeHandT()
    {
        Name = "Peephole Hand";
    }
}
