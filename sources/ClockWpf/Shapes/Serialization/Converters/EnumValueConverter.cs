namespace DustInTheWind.ClockWpf.Shapes.Serialization.Converters;

/// <summary>
/// Converts enum types to and from their string representation.
/// </summary>
public class EnumValueConverter : IValueConverter
{
    /// <summary>
    /// Gets the type that this converter handles. Returns null as this converter handles multiple enum types.
    /// </summary>
    public Type TargetType => null;

    /// <summary>
    /// Determines whether this converter can handle the specified type.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns><c>true</c> if the type is an enum; otherwise, <c>false</c>.</returns>
    public bool CanConvert(Type type)
    {
        return type.IsEnum;
    }

    /// <summary>
    /// Converts an enum value to its string representation.
    /// </summary>
    /// <param name="value">The enum value to convert.</param>
    /// <returns>The string representation of the enum value.</returns>
    public string ConvertToString(object value)
    {
        return value?.ToString();
    }

    /// <summary>
    /// Converts a string representation back to the enum value.
    /// </summary>
    /// <param name="serializedValue">The string representation.</param>
    /// <param name="targetType">The enum type to convert to.</param>
    /// <returns>The deserialized enum value.</returns>
    public object ConvertFromString(string serializedValue, Type targetType)
    {
        if (serializedValue == null)
            return null;

        return Enum.Parse(targetType, serializedValue);
    }
}
