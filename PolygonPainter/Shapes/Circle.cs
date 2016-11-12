using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PolygonPainter.Interfaces;
using PolygonPainter.Modes.LightManagers;

namespace PolygonPainter.Shapes
{
    class Circle : Shape
    {
        protected PointD _middle;
        protected double _radius;
         
        public Circle(PointD middle, double radius)
        {
            _middle = middle;
            _radius = radius;
        }

        public PointD[] GetIntersectionPointsWithHorizontalLine(double y)
        {
            PointD[] res = new PointD[2];

            double d = _radius * _radius - (y - _middle.Y) * (y - _middle.Y);
            if (d < 0)
                return new PointD[0];
            
            double s = (double)Math.Sqrt(d);

            res[0] = new PointD(_middle.X + s, y);
            res[1] = new PointD(_middle.X - s, y);
            
            return res;
        }

        public PointD[] GetIntersectionPointsWithVerticalLine(double x)
        {
            PointD[] res = new PointD[2];
            double d = _radius * _radius - (x - _middle.X) * (x - _middle.X);
            if (d < 0)
                return new PointD[0];

            double s = (double)Math.Sqrt(d);
            
            res[0] = new PointD(x, _middle.Y + s);
            res[1] = new PointD(x, _middle.Y - s);
         
            return res;
        }

        public PointD[] GetIntersectionPointsWithCircle(Circle other)
        {
            FreeVector v = new FreeVector(_middle, other._middle);
            double d = v.Length;

            if (d < Math.Abs(_radius - other._radius)
             || d > _radius + other._radius)
                return new PointD[0];

            PointD[] res = new PointD[2];

            double a = (_radius * _radius - other._radius * other._radius + d * d) / (2 * d);

            FreeVector V0 = new FreeVector(_middle);
            FreeVector V1 = new FreeVector(other._middle);
            FreeVector V2 = V0 + (V1 - V0) * a / d;

            double h = (double)Math.Sqrt(_radius * _radius - a * a);

            res[0] = new PointD(V2.X - h * (V1.Y - V0.Y) / d,
                               V2.Y + h * (V1.X - V0.X) / d);

            res[1] = new PointD(V2.X + h * (V1.Y - V0.Y) / d,
                               V2.Y - h * (V1.X - V0.X) / d);

            return res;
        }

        public override void DrawContours(PaintTools g)
        {
            throw new NotImplementedException();
        }

        public override IHandler GetEntireShapeHandler(PointD clickedPoint, List<Shape> polygons, int polygonIndex,
                                                        Color? markingColor = null)
        {
            throw new NotImplementedException();
        }

        public override IHandler GetPartOfShapeHandler(PointD clickedPoint, List<Shape> polygons, int polygonIndex,
                                                        CheckBox checkBox = null, Color? markingColor = null)
        {
            throw new NotImplementedException();
        }

        public override bool IsClickedBy(PointD p)
        {
            throw new NotImplementedException();
        }

        public override void DrawFilling(PaintTools paintTools)
        {
            throw new NotImplementedException();
        }

        public override void SetFilling(FillingInfo fillingInfo, LightManager lightManager)
        {
            throw new NotImplementedException();
        }

        public override void DeleteFilling()
        {
            throw new NotImplementedException();
        }

        public override double Area()
        {
            throw new NotImplementedException();
        }

        public override void ChangeLightManager(LightManager lightManager)
        {
            throw new NotImplementedException();
        }
    }
}
