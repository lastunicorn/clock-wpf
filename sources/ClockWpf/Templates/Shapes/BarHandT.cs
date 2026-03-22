using System.ComponentModel;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class BarHandT : HandT
{
    public double Width { get; set; } = 10.0;

    public double TailLength { get; set; } = 2.0;

    public bool RoundEnds { get; set; } = true;

    public BarHandT()
    {
        Name = "Bar Hand";
    }
}
