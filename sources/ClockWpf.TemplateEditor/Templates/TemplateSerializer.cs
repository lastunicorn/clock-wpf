using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Shapes.Serialization;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.TemplateEditor.Templates;

public class TemplateSerializer
{
    private readonly ShapeSerializer shapeSerializer;

    public TemplateSerializer()
    {
        shapeSerializer = ShapeSerializer.Default;
    }

    public void SaveTemplate(ClockTemplate template, string filePath)
    {
        if (template == null)
            throw new ArgumentNullException(nameof(template));

        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        TemplateData templateData = new()
        {
            TemplateType = template.GetType().AssemblyQualifiedName,
            Shapes = []
        };

        foreach (Shape shape in template)
        {
            ShapeData shapeData = new()
            {
                ShapeType = shape.GetType().AssemblyQualifiedName,
                Properties = shapeSerializer.SerializeProperties(shape)
            };

            templateData.Shapes.Add(shapeData);
        }

        JsonSerializerOptions options = new()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        string json = JsonSerializer.Serialize(templateData, options);
        File.WriteAllText(filePath, json);
    }

    public ClockTemplate LoadTemplate(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException("Template file not found.", filePath);

        string json = File.ReadAllText(filePath);
        TemplateData templateData = JsonSerializer.Deserialize<TemplateData>(json);

        if (templateData == null)
            throw new InvalidOperationException("Failed to deserialize template data.");

        Type templateType = Type.GetType(templateData.TemplateType);

        if (templateType == null)
            throw new InvalidOperationException($"Template type not found: {templateData.TemplateType}");

        ClockTemplate template = (ClockTemplate)Activator.CreateInstance(templateType);
        template.Clear();

        foreach (ShapeData shapeData in templateData.Shapes)
        {
            Type shapeType = Type.GetType(shapeData.ShapeType);

            if (shapeType == null)
                continue;

            Shape shape = (Shape)Activator.CreateInstance(shapeType);
            shapeSerializer.DeserializeProperties(shape, shapeData.Properties);
            template.Add(shape);
        }

        return template;
    }
}
