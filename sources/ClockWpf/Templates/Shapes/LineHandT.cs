namespace DustInTheWind.ClockWpf.Templates.Shapes;

public class LineHandT : HandT
{
    public bool RoundEnds { get; set; }

    public double TailLength { get; set; }

    public LineHandT()
    {
        Name = "Line Hand";
    }
}
