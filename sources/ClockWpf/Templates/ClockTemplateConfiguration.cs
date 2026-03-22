using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates2.Shapes;

namespace DustInTheWind.ClockWpf.Templates2;

public class ClockTemplateConfiguration
{
    private readonly Dictionary<Type, Type> templateShapeMappings = [];

    protected void Setup<TTemplateShape, TShape>()
        where TTemplateShape : ShapeT
        where TShape : Shape
    {
        templateShapeMappings.Add(typeof(TTemplateShape), typeof(TShape));
    }

    public Type GetShapeType<TTemplateShape>()
        where TTemplateShape : ShapeT
    {
        if (templateShapeMappings.TryGetValue(typeof(TTemplateShape), out Type shapeType))
            return shapeType;
        else
            return null;
    }

    public Type GetShapeType(Type templateShapeType)
    {
        if (templateShapeType == null)
            throw new ArgumentNullException(nameof(templateShapeType));

        if (templateShapeType.IsAssignableFrom(typeof(ShapeT)))
            throw new ArgumentException($"Type {templateShapeType.FullName} is not a valid template shape type.", nameof(templateShapeType));

        if (templateShapeMappings.TryGetValue(templateShapeType, out Type shapeType))
            return shapeType;
        else
            return null;
    }
}