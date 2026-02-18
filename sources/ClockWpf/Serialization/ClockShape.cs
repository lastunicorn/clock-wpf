namespace DustInTheWind.ClockWpf.Serialization;

public class ClockShape
{
    public string Id { get; set; }

    public Dictionary<string, string> Properties { get; } = [];

    public ClockShape(string id)
    {
        Id = id;
    }
}