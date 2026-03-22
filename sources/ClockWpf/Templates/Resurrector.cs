using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates.Shapes;

namespace DustInTheWind.ClockWpf.Templates;

public class Resurrector
{
    private readonly ShapeFactory shapeFactory;
    private readonly ClockTemplateConfiguration clockTemplateConfiguration;

    public Resurrector(ShapeFactory shapeFactory)
    {
        this.shapeFactory = shapeFactory ?? throw new ArgumentNullException(nameof(shapeFactory));

        clockTemplateConfiguration = new DefaultClockTemplateConfiguration();
    }

    public Resurrection Resurrect(ClockTemplate clockTemplate)
    {
        List<Shape> shapes = [];

        foreach (ShapeT shapeT in clockTemplate)
        {
            Type shapeType = clockTemplateConfiguration.GetShapeType(shapeT.GetType());
            Shape shape = shapeFactory.Create(shapeType);
            shape.Import(shapeT);
            shapes.Add(shape);
        }

        return new Resurrection
        {
            Shapes = shapes
        };
    }
}
