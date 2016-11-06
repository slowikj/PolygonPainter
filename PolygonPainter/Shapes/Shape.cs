using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PolygonPainter.Shapes.PolygonClasses;
using PolygonPainter.Shapes;
using PolygonPainter.Interfaces;

namespace PolygonPainter.Shapes
{
    public abstract class Shape
    {
        protected const float EPS = (float)0.5;

        public static float DistanceSquared (PointF a, PointF b)
        {
            Func<float, float> Sqr = (x => x * x);

            return Sqr(a.X - b.X) + Sqr(a.Y - b.Y);
        }

        public static bool EqualsEps (float a, float b)
        {
            return Math.Abs(a - b) <= EPS;
        }

        public abstract void DrawContours (PaintTools paintTools);
        public abstract void DrawFilling(PaintTools paintTools);
        public abstract bool IsClickedBy (PointF p);
        public abstract IHandler GetEntireShapeHandler(PointF clickedPoint, List<Shape> polygons, int polygonIndex);
        public abstract IHandler GetPartOfShapeHandler(PointF clickedPoint, List<Shape> polygons, int polygonIndex, CheckBox checkBox = null);
    }
}
