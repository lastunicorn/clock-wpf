namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.ShapesArea;

public class ShapeDescriptor
{
    public string Name { get; set; }

    public Type Type { get; set; }

    public ShapeDescriptor(Type type)
    {
        Name = type.Name.Replace("Shape", "");
        Type = type;
    }
}