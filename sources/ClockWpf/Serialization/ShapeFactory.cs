using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Serialization;

internal class ShapeFactory
{
    private Dictionary<string, Type> shapeTypes = [];

    public Shape CreateFrom(ClockShape clockShape)
    {
        ArgumentNullException.ThrowIfNull(clockShape);

        bool success = shapeTypes.TryGetValue(clockShape.Id, out Type shapeType);

        if (!success)
            throw new Exception($"Unknown shape id: '{clockShape.Id}'");

        Shape shape = (Shape)Activator.CreateInstance(shapeType);
        shape.Import(clockShape);

        return shape;
    }
}
