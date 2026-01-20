namespace DustInTheWind.ClockWpf.Shapes.Serialization;

/// <summary>
/// Base class for value converters that handle a specific type.
/// </summary>
/// <typeparam name="T">The type this converter handles.</typeparam>
public abstract class ValueConverterBase<T> : IValueConverter
{
    /// <summary>
    /// Gets the type that this converter handles.
    /// </summary>
    public Type TargetType => typeof(T);

    /// <summary>
    /// Determines whether this converter can handle the specified type.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns><c>true</c> if this converter can handle the type; otherwise, <c>false</c>.</returns>
    public virtual bool CanConvert(Type type)
    {
        return type == typeof(T);
    }

    /// <summary>
    /// Converts an object value to its string representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The string representation of the value.</returns>
    public string ConvertToString(object value)
    {
        if (value == null)
            return null;

        return Serialize((T)value);
    }

    /// <summary>
    /// Converts a string representation back to the object value.
    /// </summary>
    /// <param name="serializedValue">The string representation.</param>
    /// <param name="targetType">The target type to convert to.</param>
    /// <returns>The deserialized object value.</returns>
    public object ConvertFromString(string serializedValue, Type targetType)
    {
        if (serializedValue == null)
            return default(T);

        return Deserialize(serializedValue);
    }

    /// <summary>
    /// Serializes the strongly-typed value to a string.
    /// </summary>
    /// <param name="value">The value to serialize.</param>
    /// <returns>The string representation.</returns>
    protected abstract string Serialize(T value);

    /// <summary>
    /// Deserializes a string to the strongly-typed value.
    /// </summary>
    /// <param name="serializedValue">The string to deserialize.</param>
    /// <returns>The deserialized value.</returns>
    protected abstract T Deserialize(string serializedValue);
}
