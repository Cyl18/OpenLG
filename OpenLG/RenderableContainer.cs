using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenLG
{
    public abstract class RenderableContainer<T> : IRenderable where T : RenderableContainer<T>
    {
        public Image Render()
        {
            //var instance = Activator.CreateInstance<T>();
            var instance = this;
            var children = GetRenderInfos((T)instance);

            var renderer = new Renderer(children);
            switch (instance)
            {
                case HorizontalLayout<T> _:
                    renderer.Option = RenderOption.Horizontal;
                    break;

                case VerticalLayout<T> _:
                    renderer.Option = RenderOption.Vertical;
                    break;

                default:
                    throw new NotSupportedException("LG 害人不浅");
            }

            return renderer.Render();
        }

        private IEnumerable<RenderInfo> GetRenderInfos(T instance)
        {
            foreach (var obj in typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<MarginAttribute>() != null)
                .Select(p => new { obj = p.GetValue(instance), attribute = p.GetCustomAttribute<MarginAttribute>() }))
            {
                if (obj.obj is ICollection<IRenderable> collection)
                {
                    foreach (var element in collection)
                    {
                        yield return new RenderInfo(element, obj.attribute);
                    }
                }
                else if (obj.obj is IRenderable)
                {
                    yield return new RenderInfo((IRenderable)obj.obj, obj.attribute);
                }
                else
                {
                    throw new NotSupportedException("LG 害人不浅");
                }
            }
        }
    }

    // xxx
    public abstract class HorizontalLayout<T> : RenderableContainer<T> where T : RenderableContainer<T>
    {
    }

    // x
    // x
    // x
    public abstract class VerticalLayout<T> : RenderableContainer<T> where T : RenderableContainer<T>
    {
    }

    public class Margin
    {
        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }

        public Margin(int top, int bottom, int left, int right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }

        public Margin(int topbottom, int leftright)
        {
            Top = topbottom;
            Bottom = topbottom;
            Left = leftright;
            Right = leftright;
        }
    }
}