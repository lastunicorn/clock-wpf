namespace DustInTheWind.ClockWpf.Shapes;

public interface IHand
{
    double Length { get; set; }

    TimeComponent ComponentToDisplay { get; set; }
}
