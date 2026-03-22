using System.Reflection;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.TemplatesArea;

public class TemplateDescriptor
{
    public string Name { get; }

    public string Description { get; }

    public Type Type { get; }

    public TemplateDescriptor(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        Type = type;

        TemplateAttribute templateAttribute = type.GetCustomAttribute<TemplateAttribute>();

        Name = templateAttribute?.Name ?? type.Name
            .Replace("ClockTemplate", "")
            .Replace("Template", "");

        Description = templateAttribute?.Description;
    }
}