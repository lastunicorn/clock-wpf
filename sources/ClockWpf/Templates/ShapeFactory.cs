using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Templates2;

public class ShapeFactory
{
    public Shape Create(Type shapeType)
    {
        ArgumentNullException.ThrowIfNull(shapeType);

        return (Shape)Activator.CreateInstance(shapeType);
    }
}