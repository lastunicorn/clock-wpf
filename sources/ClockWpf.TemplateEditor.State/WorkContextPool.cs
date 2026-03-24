using System.Collections;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.TemplateEditor.State;

public class WorkContextPool : IEnumerable<WorkContext>
{
    private readonly IClockTemplateFactory clockTemplateFactory;
    private readonly List<WorkContext> contexts = [];

    #region CurrentWorkContext Property

    public WorkContext CurrentWorkContext
    {
        get => field;
        private set
        {
            if (field == value)
                return;

            WorkContext oldContext = field;
            field = value;
            WorkContext newContext = value;

            CurrentWorkContextChangedEventArgs args = new(oldContext, newContext);
            OnCurrentWorkContextChanged(args);
        }
    }

    public event EventHandler<CurrentWorkContextChangedEventArgs> CurrentWorkContextChanged;

    private void OnCurrentWorkContextChanged(CurrentWorkContextChangedEventArgs e)
    {
        CurrentWorkContextChanged?.Invoke(this, e);
    }

    #endregion

    public WorkContextPool(IClockTemplateFactory clockTemplateFactory)
    {
        this.clockTemplateFactory = clockTemplateFactory ?? throw new ArgumentNullException(nameof(clockTemplateFactory));
    }

    public void AddRange(IEnumerable<Type> templateTypes)
    {
        ArgumentNullException.ThrowIfNull(templateTypes);

        foreach (Type templateType in templateTypes)
        {
            if (typeof(ClockTemplate).IsAssignableFrom(templateType))
            {
                WorkContext context = new(templateType, clockTemplateFactory);
                contexts.Add(context);
            }
        }
    }

    public void OpenWorkContext(Type templateType)
    {
        ArgumentNullException.ThrowIfNull(templateType);

        bool isClockTemplate = typeof(ClockTemplate).IsAssignableFrom(templateType);
        if (!isClockTemplate)
            throw new ArgumentException($"The type {templateType.FullName} is not a clock template.");

        WorkContext context = contexts
            .FirstOrDefault(x => x.ClockTemplateType == templateType);

        if (context == null)
        {
            context = new WorkContext(templateType, clockTemplateFactory);
            contexts.Add(context);
        }

        if (context.State == WorkContextState.Closed)
            context.Open();

        CurrentWorkContext = context;
    }

    public void OpenWorkContext<T>()
        where T : ClockTemplate
    {
        Type type = typeof(T);

        WorkContext context = contexts
            .FirstOrDefault(x => x.ClockTemplateType == type);

        if (context == null)
        {
            context = new WorkContext(type, clockTemplateFactory);
            contexts.Add(context);
        }

        context.Open();
        CurrentWorkContext = context;
    }

    public IEnumerator<WorkContext> GetEnumerator()
    {
        return contexts.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}