using System.Reflection;

namespace DustInTheWind.ClockWpf.Shapes.Serialization;

/// <summary>
/// Provides serialization and deserialization capabilities for shape objects.
/// </summary>
public class ShapeSerializer
{
    private readonly ValueConverterRegistry converterRegistry;
    private readonly PropertyFilter propertyFilter;

    /// <summary>
    /// Gets the default instance using the default converter registry and property filter.
    /// </summary>
    public static ShapeSerializer Default { get; } = new ShapeSerializer(ValueConverterRegistry.Default, PropertyFilter.Default);

    /// <summary>
    /// Initializes a new instance of the <see cref="ShapeSerializer"/> class.
    /// </summary>
    /// <param name="converterRegistry">The registry of value converters to use.</param>
    /// <param name="propertyFilter">The property filter to determine which properties to serialize.</param>
    public ShapeSerializer(ValueConverterRegistry converterRegistry, PropertyFilter propertyFilter)
    {
        this.converterRegistry = converterRegistry ?? throw new ArgumentNullException(nameof(converterRegistry));
        this.propertyFilter = propertyFilter ?? throw new ArgumentNullException(nameof(propertyFilter));
    }

    /// <summary>
    /// Serializes the properties of a shape into a dictionary.
    /// </summary>
    /// <param name="shape">The shape to serialize.</param>
    /// <returns>A dictionary containing the serialized property names and values.</returns>
    public Dictionary<string, string> SerializeProperties(object shape)
    {
        if (shape == null)
            throw new ArgumentNullException(nameof(shape));

        Dictionary<string, string> properties = new Dictionary<string, string>();
        Type shapeType = shape.GetType();
        PropertyInfo[] propertyInfos = shapeType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo propertyInfo in propertyInfos)
        {
            if (!propertyFilter.ShouldSerialize(propertyInfo))
                continue;

            object value = propertyInfo.GetValue(shape);

            if (value != null)
            {
                string serializedValue = converterRegistry.ConvertToString(value);

                if (serializedValue != null)
                    properties[propertyInfo.Name] = serializedValue;
            }
        }

        return properties;
    }

    /// <summary>
    /// Deserializes properties from a dictionary and applies them to a shape.
    /// </summary>
    /// <param name="shape">The shape to apply the properties to.</param>
    /// <param name="properties">The dictionary of property names and serialized values.</param>
    public void DeserializeProperties(object shape, Dictionary<string, string> properties)
    {
        if (shape == null)
            throw new ArgumentNullException(nameof(shape));

        if (properties == null)
            return;

        Type shapeType = shape.GetType();

        foreach (KeyValuePair<string, string> kvp in properties)
        {
            string propertyName = kvp.Key;
            string serializedValue = kvp.Value;

            PropertyInfo propertyInfo = shapeType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null || !propertyInfo.CanWrite)
                continue;

            try
            {
                object value = converterRegistry.ConvertFromString(serializedValue, propertyInfo.PropertyType);
                propertyInfo.SetValue(shape, value);
            }
            catch
            {
                // Skip properties that fail to deserialize
            }
        }
    }
}
