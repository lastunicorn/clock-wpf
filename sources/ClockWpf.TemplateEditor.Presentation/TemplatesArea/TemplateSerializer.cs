using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using DustInTheWind.ClockWpf.Serialization;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates2;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.TemplatesArea;

public class TemplateSerializer
{
    private readonly ShapeSerializer shapeSerializer;
    private readonly JsonSerializerOptions options;

    public TemplateSerializer()
    {
        shapeSerializer = ShapeSerializer.Default;

        options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

    public void SaveTemplate(ClockTemplate template, string filePath)
    {
        //if (template == null)
        //    throw new ArgumentNullException(nameof(template));

        //if (string.IsNullOrWhiteSpace(filePath))
        //    throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        //JTemplate jTemplate = new()
        //{
        //    TemplateType = template.GetType().AssemblyQualifiedName,
        //    Shapes = []
        //};

        //foreach (Shape shape in template)
        //{
        //    ClockShape clockShape = shape.Export();

        //    JShape shapeData = new()
        //    {
        //        ShapeType = clockShape.Id,
        //        Properties = clockShape.Properties
        //    };

        //    jTemplate.Shapes.Add(shapeData);
        //}

        //string json = JsonSerializer.Serialize(jTemplate, options);
        //File.WriteAllText(filePath, json);
    }

    public ClockTemplate LoadTemplate(string filePath)
    {
        throw new NotImplementedException();

        //if (string.IsNullOrWhiteSpace(filePath))
        //    throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        //if (!File.Exists(filePath))
        //    throw new FileNotFoundException("Template file not found.", filePath);

        //string json = File.ReadAllText(filePath);
        //JTemplate templateData = JsonSerializer.Deserialize<JTemplate>(json);

        //if (templateData == null)
        //    throw new InvalidOperationException("Failed to deserialize template data.");

        //Type templateType = Type.GetType(templateData.TemplateType);

        //if (templateType == null)
        //    throw new InvalidOperationException($"Template type not found: {templateData.TemplateType}");

        //ClockTemplate template = (ClockTemplate)Activator.CreateInstance(templateType);
        //template.Clear();

        //foreach (JShape shapeData in templateData.Shapes)
        //{
        //    Type shapeType = Type.GetType(shapeData.ShapeType);

        //    if (shapeType == null)
        //        continue;

        //    Shape shape = (Shape)Activator.CreateInstance(shapeType);
        //    shapeSerializer.DeserializeProperties(shape, shapeData.Properties);
        //    template.Add(shape);
        //}

        //return template;
    }
}
