namespace DustInTheWind.ClockWpf.Templates;

/// <summary>
/// Specifies metadata for a clock template, including its human readable name and an optional description.
/// </summary>
public class ClockTemplateAttribute : Attribute
{
    /// <summary>
    /// Gets the name of the clock template.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets a short description for the clock template.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClockTemplateAttribute"/> class with the specified name and an optional description.
    /// </summary>
    /// <param name="name">The unique name that identifies the template. Cannot be null.</param>
    /// <param name="description">An optional description that provides additional information about the template. May be null.</param>
    public ClockTemplateAttribute(string name, string description = null)
    {
        ArgumentNullException.ThrowIfNull(name);

        Name = name;
        Description = description;
    }
}
