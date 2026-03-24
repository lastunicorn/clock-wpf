using System.Reflection;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.TemplateEditor.State;

public class ClockTemplateMetadata
{
    private readonly TemplateAttribute templateAttribute;
    private readonly Type type;
    private string name;
    private string description;

    public string Name
    {
        get
        {
            if (name == null)
            {
                name = templateAttribute?.Name ?? type.Name
                    .Replace("ClockTemplate", "")
                    .Replace("Template", "");
            }

            return name;
        }
    }

    public string Description
    {
        get
        {
            if (description == null)
                description = templateAttribute?.Description;

            return description;
        }
    }

    public ClockTemplateMetadata(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        this.type = type;

        bool isClockTemplate = typeof(ClockTemplate).IsAssignableFrom(type);
        if (!isClockTemplate)
            throw new ArgumentException($"The type {type.FullName} is not a clock template.", nameof(type));

        templateAttribute = type.GetCustomAttribute<TemplateAttribute>();
    }
}