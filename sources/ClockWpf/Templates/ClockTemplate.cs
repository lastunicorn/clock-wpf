using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Templates;

public abstract class ClockTemplate : Collection<Shape>
{
    public ClockTemplate()
    {
        IEnumerable<Shape> shapes = CreateShapes();

        foreach (Shape shape in shapes)
            Items.Add(shape);
    }

    protected abstract IEnumerable<Shape> CreateShapes();
}