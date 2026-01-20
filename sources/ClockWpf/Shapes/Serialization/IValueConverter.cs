namespace DustInTheWind.ClockWpf.Shapes.Serialization;

/// <summary>
/// Defines methods for converting values to and from their serialized string representation.
/// </summary>
public interface IValueConverter
{
    /// <summary>
    /// Gets the type that this converter handles.
    /// </summary>
    Type TargetType { get; }

    /// <summary>
    /// Determines whether this converter can handle the specified type.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns><c>true</c> if this converter can handle the type; otherwise, <c>false</c>.</returns>
    bool CanConvert(Type type);

    /// <summary>
    /// Converts an object value to its string representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The string representation of the value.</returns>
    string ConvertToString(object value);

    /// <summary>
    /// Converts a string representation back to the object value.
    /// </summary>
    /// <param name="serializedValue">The string representation.</param>
    /// <param name="targetType">The target type to convert to.</param>
    /// <returns>The deserialized object value.</returns>
    object ConvertFromString(string serializedValue, Type targetType);
}
