using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

internal class DrawingPlan
{
    private readonly DrawingContext drawingContext;
    private readonly List<Transform> transforms = [];

    private DrawingPlan(DrawingContext drawingContext)
    {
        this.drawingContext = drawingContext ?? throw new ArgumentNullException(nameof(drawingContext));
    }

    public static DrawingPlan Create(DrawingContext drawingContext)
    {
        return new DrawingPlan(drawingContext);
    }

    public DrawingPlan WithTransform<T>(Func<T> createTransform)
        where T : Transform
    {
        ArgumentNullException.ThrowIfNull(createTransform);

        T transform = createTransform.Invoke();

        if (transform != null)
            transforms.Add(transform);

        return this;
    }

    public void Draw(Action<DrawingContext> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        foreach (Transform transform in transforms)
            drawingContext.PushTransform(transform);

        action?.Invoke(drawingContext);

        for (int i = transforms.Count - 1; i >= 0; i--)
            drawingContext.Pop();
    }
}