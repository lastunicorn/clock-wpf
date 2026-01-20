using System.Globalization;

namespace DustInTheWind.ClockWpf.Shapes.Serialization.Converters;

/// <summary>
/// Converts <see cref="TimeSpan"/> values to and from their string representation.
/// </summary>
public class TimeSpanValueConverter : ValueConverterBase<TimeSpan>
{
    /// <summary>
    /// Serializes a <see cref="TimeSpan"/> to its string representation.
    /// </summary>
    /// <param name="value">The TimeSpan to serialize.</param>
    /// <returns>The string representation.</returns>
    protected override string Serialize(TimeSpan value)
    {
        return value.ToString("c", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Deserializes a string to a <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="serializedValue">The string to deserialize.</param>
    /// <returns>The deserialized TimeSpan.</returns>
    protected override TimeSpan Deserialize(string serializedValue)
    {
        return TimeSpan.Parse(serializedValue, CultureInfo.InvariantCulture);
    }
}
