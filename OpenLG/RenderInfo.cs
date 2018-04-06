using System;
using System.Drawing;

namespace OpenLG
{
    public class RenderInfo
    {
        public IRenderable Renderable { get; }
        public MarginAttribute Info { get; }

        public Lazy<Image> Image { get; }

        public Size Size => Renderer.CalculateSize(Image.Value, Info.Margin);
        public Margin Margin => Info.Margin;

        public RenderInfo(IRenderable renderable, MarginAttribute info)
        {
            Renderable = renderable;
            Info = info;
            Image = new Lazy<Image>(() => Renderable.Render().AddToDisposeList());
        }
    }
}