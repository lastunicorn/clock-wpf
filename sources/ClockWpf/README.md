# Clock WPF

A customizable WPF analog clock control built using composable shapes.

## Getting Started

### Basic Usage

```xaml
<Window xmlns:clock="clr-namespace:DustInTheWind.ClockWpf;assembly=ClockWpf">
    <clock:AnalogClock Width="200" Height="200" />
</Window>
```

```csharp
AnalogClock clock = new()
{
    TimeProvider = new LocalTimeProvider(),
    ClockTemplate = new DefaultTemplate()
};
clock.TimeProvider.Start();
```

### Key Properties

- **`TimeProvider`** (`ITimeProvider`) - Controls the time source (local, UTC, broken time, etc.)
- **`ClockTemplate`** (`ClockTemplate`) - Applies a predefined visual template
- **`Shapes`** (`ObservableCollection<Shape>`) - Collection of shapes to render (background, hands, ticks, etc.)

## Architecture

The control renders shapes in layers, stacked from first to last:

1. **Simple Shapes** - Static elements (backgrounds, pins, text) drawn relative to center (0,0)
2. **Rim Shapes** - Repeated items around the dial edge (ticks, hour numerals)
3. **Hand Shapes** - Auto-rotating time indicators (hour, minute, second hands)

## Shape Types

### 1. Simple Shapes

Inherit directly from `Shape`. Origin (0,0) is the clock center.

**Built-in types**: `FlatBackground`, `FancyBackground`, `Pin`

**Example**:

```csharp
public class Pin : Shape
{
    public double Diameter { get; set; }

    public override void DoRender(ClockDrawingContext context)
    {
        double radius = context.ClockRadius * (Diameter / 100.0) / 2;
        Point center = new(0, 0);
        context.DrawingContext.DrawEllipse(FillBrush, StrokePen, center, radius, radius);
    }
}
```

### 2. Rim Shapes

Inherit from `RimBase`. Items are auto-positioned around the dial edge.

**Built-in types**: `Ticks`, `Hours`

**Key properties**:

- `Angle` - Degrees between items (default: 30°)
- `OffsetAngle` - Starting rotation offset (default: 0°)
- `DistanceFromEdge` - Distance from dial edge as % of radius
- `Orientation` - `FaceCenter`, `FaceOut`, or `Normal`
- `SkipIndex` - Skip every nth item (useful for alternating patterns)

**Example**:

```csharp
public class Ticks : RimBase
{
    public double Length { get; set; }

    protected override void RenderItem(DrawingContext drawingContext, int index)
    {
        double actualLength = context.ClockRadius * Length / 100.0;
        Point startPoint = new(0, -actualLength / 2);
        Point endPoint = new(0, actualLength / 2);
        drawingContext.DrawLine(StrokePen, startPoint, endPoint);
    }
}
```

**Usage**:

```csharp
new Ticks
{
    Angle = 6,           // Every 6° (60 ticks total)
    SkipIndex = 5,       // Skip every 5th tick
    Length = 5,          // 5% of radius
    DistanceFromEdge = 6 // 6% from edge
};
```

### 3. Hand Shapes

Inherit from `HandBase`. Auto-rotate based on time.

**Built-in types**: `SimpleHand`, `CapsuleHand`, `NibHand`, `DiamondHand`, `DotHand`

**Key properties**:

- `ComponentToDisplay` - `Hour`, `Minute`, or `Second`
- `Length` - Hand length as % of radius
- `IntegralValue` - If `true`, hand jumps discretely; if `false`, moves smoothly

**Example**:

```csharp
public class SimpleHand : HandBase
{
    public double TailLength { get; set; }
    public double PinDiameter { get; set; }

    public override void DoRender(ClockDrawingContext context)
    {
        context.DrawingContext.CreateDrawingPlan()
            .WithTransform(() =>
            {
                double angleDegrees = CalculateHandAngle(context.Time);
                return new RotateTransform(angleDegrees, 0, 0);
            })
            .Draw(dc => DrawHand(dc, context.ClockRadius));
    }
}
```

**Usage**:

```csharp
new SimpleHand
{
    ComponentToDisplay = TimeComponent.Second,
    Length = 96,
    TailLength = 14,
    IntegralValue = true, // Tick-tock movement
    StrokeThickness = 1,
    StrokeBrush = Brushes.Red
};
```

## Common Shape Properties

All shapes inherit these properties:

- `Name` - User-friendly identifier
- `IsVisible` - Show/hide the shape
- `FillBrush` - Fill color/brush
- `StrokeBrush` - Outline color/brush
- `StrokeThickness` - Outline width

## Creating Templates

Templates group shapes into reusable clock designs.

```csharp
public class MyClockTemplate : ClockTemplate
{
    protected override IEnumerable<Shape> CreateShapes()
    {
        yield return new FlatBackground
        {
            FillBrush = Brushes.White
        };

        yield return new Ticks
        {
            Angle = 6,
            SkipIndex = 5,
            StrokeThickness = 1
        };

        yield return new CapsuleHand
        {
            ComponentToDisplay = TimeComponent.Hour,
            Length = 50,
            Width = 8,
            FillBrush = Brushes.Black
        };

        yield return new CapsuleHand
        {
            ComponentToDisplay = TimeComponent.Minute,
            Length = 85,
            Width = 6,
            FillBrush = Brushes.DarkGray
        };

        yield return new SimpleHand
        {
            ComponentToDisplay = TimeComponent.Second,
            Length = 96,
            StrokeBrush = Brushes.Red,
            IntegralValue = true
        };
    }
}
```

**Apply template**:

```csharp
clock.ClockTemplate = new MyClockTemplate();
// or
clock.ApplyClockTemplate(new MyClockTemplate());
```

## Time Providers

Implement `ITimeProvider` to create custom time sources.

**Built-in providers**:

- `LocalTimeProvider` - System local time
- `UtcTimeProvider` - UTC time
- `BrokenTimeProvider` - Accelerated time (useful for testing)
- `RandomTimeProvider` - Random time values

**Custom provider**:

```csharp
public class MyTimeProvider : TimeProviderBase
{
    protected override TimeSpan GetTime()
    {
        return DateTime.Now.TimeOfDay;
    }
}
```

**Usage**:

```csharp
ITimeProvider timeProvider = new LocalTimeProvider
{
    Interval = 100 // Refresh every 100ms
};
timeProvider.Start();
clock.TimeProvider = timeProvider;
```

## Advanced Shape Development

### Using ClockDrawingContext

```csharp
public override void DoRender(ClockDrawingContext context)
{
    double radius = context.ClockRadius;        // Half of ClockDiameter
    double diameter = context.ClockDiameter;     // Full clock size
    TimeSpan time = context.Time;                // Current time to display
    DrawingContext dc = context.DrawingContext;  // WPF drawing context
}
```

### Layout Optimization

Override `CalculateLayout()` for expensive calculations that don't change on every render:

```csharp
private Point calculatedPosition;
private double calculatedSize;

protected override void CalculateLayout(ClockDrawingContext context)
{
    base.CalculateLayout(context);
    calculatedPosition = new Point(context.ClockRadius * 0.5, 0);
    calculatedSize = context.ClockRadius * 0.1;
}

public override void DoRender(ClockDrawingContext context)
{
    context.DrawingContext.DrawEllipse(
        FillBrush, null, calculatedPosition, calculatedSize, calculatedSize);
}
```

Invalidate layout when properties change:

```csharp
private static void HandlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
{
    if (d is MyShape shape)
        shape.InvalidateLayout();
}
```

### Transformations

Use `DrawingPlan` for complex transformations:

```csharp
context.DrawingContext.CreateDrawingPlan()
    .WithTransform(() => new RotateTransform(45, 0, 0))
    .WithTransform(() => new TranslateTransform(10, 20))
    .Draw(dc => dc.DrawEllipse(FillBrush, null, new Point(0, 0), 5, 5));