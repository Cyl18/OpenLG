using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace OpenLG
{
    public static class ImageExtensions
    {
        public static Bitmap Resize(this Image image, double widthPercent, double heightPercent)
        {
            var width = (int)(image.Width * widthPercent);
            var height = (int)(image.Height * heightPercent);

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Bitmap FillBackground(this Image image, Brush brush)
        {
            var dest = BitmapFactory.Create(image.Size);

            using (var graphics = Graphics.FromImage(dest))
            {
                var point = new Point(0, 0);
                graphics.FillRegion(brush, new Region(new Rectangle(point, image.Size)));
                graphics.DrawImage(image, point);
            }

            return dest;
        }
    }
}