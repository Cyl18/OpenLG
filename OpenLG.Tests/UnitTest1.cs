using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenLG.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            new Test1().Render().FillBackground(Brushes.Black).Save("test.png");
        }
    }

    public class Test1 : HorizontalLayout<Test1>
    {
        [Margin(20, 20)]
        public RenderableText Text1 { get; } = "Fork You LG1";

        [Margin(20, 20)]
        public ICollection<IRenderable> Text2 { get; } = new RenderableText[] { "1", "2", "3" };

        [Margin(20, 10)]
        public RenderableText Text3 { get; } = new RenderableText("fork you lg3", Color.Aqua);

        [Margin(3, 3)]
        public Vert Vert { get; } = new Vert();
    }

    public class Vert : VerticalLayout<Vert>
    {
        [Margin(20, 20)]
        public RenderableText Text1 { get; } = "Fork You LGVert1";

        [Margin(20, 10)]
        public RenderableText Text3 { get; } = new RenderableText("fork you lgvert3", Color.Aqua);
    }
}