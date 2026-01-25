namespace DustInTheWind.ClockWpf.Shapes;

public interface IHand
{
    double Length { get; set; }

    TimeComponent TimeComponent { get; set; }
}
