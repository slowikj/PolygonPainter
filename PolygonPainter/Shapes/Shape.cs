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
        protected const double EPS = (double)0.5;

        public static double DistanceSquared (PointD a, PointD b)
        {
            Func<double, double> Sqr = (x => x * x);

            return Sqr(a.X - b.X) + Sqr(a.Y - b.Y);
        }

        public static bool EqualsEps (double a, double b, double eps = EPS)
        {
            return Math.Abs(a - b) <= eps;
        }

        public abstract void DrawContours (PaintTools paintTools);
        public abstract void DrawFilling(PaintTools paintTools);
        public abstract void SetFilling(FillingInfo fillingInfo);
        public abstract void DeleteFilling();
        public abstract bool IsClickedBy (PointD p);
        public abstract IHandler GetEntireShapeHandler(PointD clickedPoint, List<Shape> polygons, int polygonIndex);
        public abstract IHandler GetPartOfShapeHandler(PointD clickedPoint, List<Shape> polygons, int polygonIndex, CheckBox checkBox = null);
        public abstract double Area();
    }
}
