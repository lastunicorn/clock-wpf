namespace DustInTheWind.ClockWpf.Shapes.Serialization.Converters;

/// <summary>
/// Converts string array values to and from their string representation.
/// </summary>
public class StringArrayValueConverter : ValueConverterBase<string[]>
{
    /// <summary>
    /// Serializes a string array to its string representation.
    /// Format: "value1;value2;..."
    /// </summary>
    /// <param name="value">The string array to serialize.</param>
    /// <returns>The string representation.</returns>
    protected override string Serialize(string[] value)
    {
        if (value == null || value.Length == 0)
            return string.Empty;

        return string.Join(";", value);
    }

    /// <summary>
    /// Deserializes a string to a string array.
    /// </summary>
    /// <param name="serializedValue">The string to deserialize.</param>
    /// <returns>The deserialized string array.</returns>
    protected override string[] Deserialize(string serializedValue)
    {
        if (string.IsNullOrEmpty(serializedValue))
            return new string[0];

        return serializedValue.Split(';');
    }
}
