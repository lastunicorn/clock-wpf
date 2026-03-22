namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class PinT : ShapeT
{
    public double Diameter { get; set; } = 4.0;

    public PinT()
    {
        Name = "Pin";
        StrokeThickness = 0.0;
    }
}
