using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace OpenLG
{
    public class Renderer
    {
        private Point _point = Point.Empty;

        public Renderer(IEnumerable<RenderInfo> children)
        {
            Infos = children;
        }

        public IEnumerable<RenderInfo> Infos { get; }
        public RenderOption Option { get; set; }

        public Image Render()
        {
            var sizes = GetSizes().ToArray();
            var size = CalculateSize(sizes);
            var bitmap = BitmapFactory.Create(size).AddToDisposeList();
            using (var graphics = Graphics.FromImage(bitmap))
            {
                foreach (var info in Infos)
                {
                    BeforeRender(info.Margin);
                    graphics.DrawImage(info.Image.Value, _point);
                    AfterRender(info.Margin, info.Size);
                }
            }

            return bitmap;
        }

        private void BeforeRender(Margin margin)
        {
            _point.X += margin.Left;
            _point.Y += margin.Top;
        }

        private void AfterRender(Margin margin, Size size)
        {
            _point.X -= margin.Left;
            _point.Y -= margin.Top;
            switch (Option)
            {
                case RenderOption.Horizontal:
                    _point.X += size.Width;
                    break;

                case RenderOption.Vertical:
                    _point.Y += size.Height;
                    break;
            }
        }

        private Size CalculateSize(IEnumerable<Size> sizes)
        {
            int height, width;
            var enumerable = sizes as Size[] ?? sizes.ToArray();
            switch (Option)
            {
                case RenderOption.Horizontal:
                    // 横向渲染 高度为最大值 宽度为和
                    height = enumerable.Max(size => size.Height);
                    width = enumerable.Sum(size => size.Width);
                    break;

                case RenderOption.Vertical:
                    // 纵向渲染 宽度为最大值 高度为和
                    width = enumerable.Max(size => size.Width);
                    height = enumerable.Sum(size => size.Height);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new Size(width, height);
        }

        private IEnumerable<Size> GetSizes()
        {
            return Infos.Select(info => info.Size);
        }

        public static Size CalculateSize(Image image, Margin margin)
        {
            var width = margin.Left + image.Size.Width + margin.Right;
            var height = margin.Top + image.Size.Height + margin.Bottom;
            return new Size(width, height);
        }
    }

    public enum RenderOption
    {
        Horizontal, // xxx
        Vertical // x\nx
    }
}