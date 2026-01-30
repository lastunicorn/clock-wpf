# Clock WPF

A customizable WPF analog clock control built using composable shapes.

## Getting Started

### Basic Usage

```xaml
<Window xmlns:clock="clr-namespace:DustInTheWind.ClockWpf;assembly=DustInTheWind.ClockWpf">
    <clock:AnalogClock
        ClockTemplate="{Binding ClockTemplate}"
        TimeProvider="{Binding TimeProvider}" />
</Window>
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

