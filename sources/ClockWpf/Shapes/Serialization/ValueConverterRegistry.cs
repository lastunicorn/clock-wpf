using DustInTheWind.ClockWpf.Shapes.Serialization.Converters;

namespace DustInTheWind.ClockWpf.Shapes.Serialization;

/// <summary>
/// Manages a collection of value converters and provides methods to convert values to and from strings.
/// </summary>
public class ValueConverterRegistry
{
    private readonly List<IValueConverter> converters = new List<IValueConverter>();

    /// <summary>
    /// Gets the default instance of the registry with all standard converters registered.
    /// </summary>
    public static ValueConverterRegistry Default { get; } = CreateDefault();

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueConverterRegistry"/> class.
    /// </summary>
    public ValueConverterRegistry()
    {
    }

    /// <summary>
    /// Creates the default registry with all standard converters.
    /// </summary>
    /// <returns>A new registry with standard converters registered.</returns>
    private static ValueConverterRegistry CreateDefault()
    {
        ValueConverterRegistry registry = new ValueConverterRegistry();

        registry.Register(new PrimitiveValueConverter());
        registry.Register(new EnumValueConverter());
        registry.Register(new TimeSpanValueConverter());
        registry.Register(new StringArrayValueConverter());

        return registry;
    }

    /// <summary>
    /// Registers a converter with the registry.
    /// </summary>
    /// <param name="converter">The converter to register.</param>
    public void Register(IValueConverter converter)
    {
        if (converter == null)
            throw new ArgumentNullException(nameof(converter));

        converters.Add(converter);
    }

    /// <summary>
    /// Gets a converter that can handle the specified type.
    /// </summary>
    /// <param name="type">The type to find a converter for.</param>
    /// <returns>A converter that can handle the type, or <c>null</c> if none is found.</returns>
    public IValueConverter GetConverter(Type type)
    {
        foreach (IValueConverter converter in converters)
        {
            if (converter.CanConvert(type))
                return converter;
        }

        return null;
    }

    /// <summary>
    /// Converts a value to its string representation using the appropriate converter.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The string representation, or <c>null</c> if no converter is found.</returns>
    public string ConvertToString(object value)
    {
        if (value == null)
            return null;

        Type valueType = value.GetType();
        IValueConverter converter = GetConverter(valueType);

        if (converter != null)
            return converter.ConvertToString(value);

        return value.ToString();
    }

    /// <summary>
    /// Converts a string representation to a value of the specified type.
    /// </summary>
    /// <param name="serializedValue">The string representation.</param>
    /// <param name="targetType">The target type to convert to.</param>
    /// <returns>The deserialized value, or the original string if no converter is found.</returns>
    public object ConvertFromString(string serializedValue, Type targetType)
    {
        if (serializedValue == null)
            return null;

        IValueConverter converter = GetConverter(targetType);

        if (converter != null)
            return converter.ConvertFromString(serializedValue, targetType);

        return serializedValue;
    }
}
