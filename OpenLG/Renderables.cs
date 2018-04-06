using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenLG
{
    public class Renderables
    {
        public static Font TextFont { get; set; } = new Font("Microsoft YaHei", 24);
        public static Color Color { get; set; } = Color.White;
    }

    public class RenderableImage : IRenderable
    {
        private readonly Image _image;

        public RenderableImage(Image image)
        {
            _image = image;
        }

        public Image Render()
        {
            return _image;
        }

        public static implicit operator RenderableImage(Image image)
        {
            return new RenderableImage(image);
        }
    }

    public class RenderableText : IRenderable
    {
        private static readonly Graphics EmptyGraphics = Graphics.FromImage(new Bitmap(1, 1));

        public Image Render()
        {
            var size = EmptyGraphics.MeasureString(Text, Font).ToSize();
            var image = BitmapFactory.Create(size);
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.DrawString(Text, Font, new SolidBrush(Color), new PointF(0, 0));
            }

            return image;
        }

        public RenderableText(string text, Color? color = null, Font font = null)
        {
            Font = font ?? Renderables.TextFont;
            Color = color ?? Renderables.Color;
            Text = text;
        }

        public Font Font { get; }
        public Color Color { get; }
        public string Text { get; }

        public static implicit operator RenderableText(string text)
        {
            return new RenderableText(text);
        }
    }

    public class SizeExtensions
    {
        public static Size ToSize(SizeF size)
        {
            return new Size((int)size.Width, (int)size.Height);
        }
    }
}