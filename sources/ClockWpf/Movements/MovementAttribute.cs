namespace DustInTheWind.ClockWpf.Movements;

/// <summary>
/// Specifies metadata for a movement type by providing a name and description.
/// This attribute is intended to be applied to classes that represent different clock movement types.
/// </summary>
/// <remarks>
/// The attribute is not inherited and cannot be applied multiple times to the same class.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class MovementAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the name of the clock movement type.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a short description for the clock movement type.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MovementAttribute"/> class.
    /// </summary>
    public MovementAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MovementAttribute"/> class with
    /// the name and description of the clock movement type.
    /// </summary>
    /// <param name="name">The name of the clock movement type.</param>
    /// <param name="description">The description of the clock movement type.</param>
    public MovementAttribute(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
