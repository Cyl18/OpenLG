# OpenLG
Just a C# graphics library using System.Drawing.Common.

## What is LG?
This guy [Lasm_Gratel](https://github.com/LasmGratel)

## Usage

### Definition..

```csharp
public class Test1 : HorizontalLayout<Test1>
{
    [Margin(20, 20)]
    public RenderableText Text1 { get; } = "Fork You LG1";

    [Margin(20, 20)]
    public RenderableText Text2 { get; } = "Fork You LG2";

    [Margin(20, 10)]
    public RenderableText Text3 { get; } = new RenderableText("fork you lg3", Color.Aqua);

    [Margin(3, 3)]
    public Vert Vert { get; } = new Vert();
}

public class Vert : VerticalLayout<Vert>
{
    [Margin(20, 20)]
    public RenderableText Text1 { get; } = "Fork You LGVert1";

    [Margin(20, 20)]
    public RenderableText Text2 { get; } = "Fork You LGVert2";

    [Margin(20, 10)]
    public RenderableText Text3 { get; } = new RenderableText("fork you lgvert3", Color.Aqua);
}
```

### Then..
`new Test1().Render().Save("test.png");`

### Result..
![1](Images/result.png)