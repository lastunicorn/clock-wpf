namespace DustInTheWind.ClockWpf.Shapes;

public class ShapeAttribute : Attribute
{
    public string Id { get; }

    public ShapeAttribute(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        Id = id;
    }
}