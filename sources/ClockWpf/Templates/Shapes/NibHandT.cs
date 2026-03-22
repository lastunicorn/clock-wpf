namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class NibHandT : HandT
{
    public double Width { get; set; } = 5.0;

    public bool KeepProportions { get; set; } = true;

    public NibHandT()
    {
        Name = "Nib Hand";
    }
}
