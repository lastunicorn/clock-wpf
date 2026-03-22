namespace DustInTheWind.ClockWpf.Templates.Shapes;

public class Blade2HandT : HandT
{
    public double Width { get; set; } = 20.0;

    public double HipDistance { get; set; } = 45.0;

    public double TipLength { get; set; } = 15.0;

    public Blade2HandT()
    {
        Name = "Blade hand 2";
    }
}
