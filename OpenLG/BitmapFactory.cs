using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenLG
{
    public class BitmapFactory
    {
        public static Bitmap Create(Size size)
        {
            return new Bitmap(size.Width, size.Height);
        }
    }
}