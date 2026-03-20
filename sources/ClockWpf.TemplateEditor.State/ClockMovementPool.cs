using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.TemplateEditor.State;

public class ClockMovementPool
{
    private readonly IClockMovementFactory clockMovementFactory;
    private readonly List<MovementDescriptor> descriptors = [];

    #region CurrentMovement Property

    public MovementDescriptor CurrentMovement
    {
        get => field;
        private set
        {
            if (field == value)
                return;

            MovementDescriptor oldMovement = value;
            oldMovement?.Instance?.Stop();

            field = value;

            MovementDescriptor newMovement = value;
            newMovement?.Instance?.Start();

            CurrentMovementChangedEventArgs args = new(oldMovement?.Instance, newMovement?.Instance);
            OnCurrentMovementChanged(args);
        }
    }

    public event EventHandler<CurrentMovementChangedEventArgs> CurrentMovementChanged;

    private void OnCurrentMovementChanged(CurrentMovementChangedEventArgs e)
    {
        CurrentMovementChanged?.Invoke(this, e);
    }

    #endregion

    public ClockMovementPool(IClockMovementFactory clockMovementFactory)
    {
        this.clockMovementFactory = clockMovementFactory ?? throw new ArgumentNullException(nameof(clockMovementFactory));
    }

    public void AddRange(IEnumerable<Type> types)
    {
        ArgumentNullException.ThrowIfNull(types);

        foreach (Type type in types)
            AddDescriptorFor(type);
    }

    private void AddDescriptorFor(Type type)
    {
        MovementDescriptor movementDescriptor = new(type);
        descriptors.Add(movementDescriptor);
    }

    public void SetDefault(Type type)
    {
        CurrentMovement = type == null
            ? null
            : GetOrCreateDescriptorFor(type);
    }

    private MovementDescriptor GetOrCreateDescriptorFor(Type type)
    {
        bool isClockMovement = typeof(IMovement).IsAssignableFrom(type);
        if (!isClockMovement)
            throw new ArgumentException($"The type {type.FullName} is not a clock movement.");

        MovementDescriptor descriptor = descriptors
            .FirstOrDefault(x => x.Type == type);

        if (descriptor == null)
        {
            descriptor = new MovementDescriptor(type);
            descriptors.Add(descriptor);
        }

        if (descriptor.Instance == null)
            descriptor.CreateInstance(clockMovementFactory);

        return descriptor;
    }

    public void SetDefault<T>()
        where T : IMovement
    {
        MovementDescriptor descriptor = descriptors
            .Where(x => x.Type == typeof(T))
            .FirstOrDefault();

        if (descriptor == null)
        {
            descriptor = new MovementDescriptor(typeof(T));
            descriptors.Add(descriptor);
        }

        if (descriptor.Instance == null)
            descriptor.CreateInstance(clockMovementFactory);

        CurrentMovement = descriptor;
    }

    public void RecreateCurrentTemplate()
    {
        MovementDescriptor descriptor = CurrentMovement;

        if (descriptor == null)
            return;

        CurrentMovement = null;
        descriptor.CreateInstance(clockMovementFactory);
        CurrentMovement = descriptor;
    }

    public IEnumerable<MovementDescriptor> EnumerateKnownMovements()
    {
        return descriptors;
    }
}
