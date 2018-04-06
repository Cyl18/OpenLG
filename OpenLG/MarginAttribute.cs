using System;

namespace OpenLG
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class MarginAttribute : Attribute
    {
        public MarginAttribute(int top, int bottom, int left, int right)
        {
            Margin = new Margin(top, bottom, left, right);
        }

        public MarginAttribute(int topbottom, int leftright)
        {
            Margin = new Margin(topbottom, leftright);
        }

        public Margin Margin { get; }
    }
}