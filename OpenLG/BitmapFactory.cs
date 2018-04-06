using System.Drawing;

namespace OpenLG
{
    public static class BitmapFactory
    {
        public static Bitmap Create(Size size)
        {
            return new Bitmap(size.Width, size.Height);
        }
    }
}