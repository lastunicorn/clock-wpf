# Clock Templates

A clock template is composed of a list of shapes which are displayed one over the other.

The templates are inheriting `ClockTemplate` abstract base class.

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
```

