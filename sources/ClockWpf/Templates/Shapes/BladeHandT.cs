namespace DustInTheWind.ClockWpf.Templates.Shapes;

public class BladeHandT : HandT
{
    public double Width { get; set; } = 20.0;

    public double HipDistance { get; set; } = 20.0;

    public double ShadowMargin { get; set; } = 2.0;

    public BladeHandT()
    {
        Name = "Blade hand";
    }
}
