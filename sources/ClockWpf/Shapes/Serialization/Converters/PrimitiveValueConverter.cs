using System.Globalization;

namespace DustInTheWind.ClockWpf.Shapes.Serialization.Converters;

/// <summary>
/// Converts primitive types and strings to and from their string representation.
/// </summary>
public class PrimitiveValueConverter : IValueConverter
{
    /// <summary>
    /// Gets the type that this converter handles. Returns null as this converter handles multiple types.
    /// </summary>
    public Type TargetType => null;

    /// <summary>
    /// Determines whether this converter can handle the specified type.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns><c>true</c> if this converter can handle the type; otherwise, <c>false</c>.</returns>
    public bool CanConvert(Type type)
    {
        return type.IsPrimitive || type == typeof(string) || type == typeof(decimal);
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

        switch (value)
        {
            case bool boolValue:
                return boolValue.ToString();

            case int intValue:
                return intValue.ToString(CultureInfo.InvariantCulture);

            case float floatValue:
                return floatValue.ToString(CultureInfo.InvariantCulture);

            case double doubleValue:
                return doubleValue.ToString(CultureInfo.InvariantCulture);

            case decimal decimalValue:
                return decimalValue.ToString(CultureInfo.InvariantCulture);

            case string stringValue:
                return stringValue;

            default:
                return Convert.ChangeType(value, typeof(string), CultureInfo.InvariantCulture) as string;
        }
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
            return null;

        if (targetType == typeof(bool))
            return bool.Parse(serializedValue);

        if (targetType == typeof(int))
            return int.Parse(serializedValue, CultureInfo.InvariantCulture);

        if (targetType == typeof(float))
            return float.Parse(serializedValue, CultureInfo.InvariantCulture);

        if (targetType == typeof(double))
            return double.Parse(serializedValue, CultureInfo.InvariantCulture);

        if (targetType == typeof(decimal))
            return decimal.Parse(serializedValue, CultureInfo.InvariantCulture);

        if (targetType == typeof(string))
            return serializedValue;

        return Convert.ChangeType(serializedValue, targetType, CultureInfo.InvariantCulture);
    }
}
