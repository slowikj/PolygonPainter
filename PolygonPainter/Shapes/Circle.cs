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


namespace PolygonPainter.Shapes
{
    class Circle : Shape
    {
        protected PointF _middle;
        protected float _radius;
         
        public Circle(PointF middle, float radius)
        {
            _middle = middle;
            _radius = radius;
        }

        public PointF[] GetIntersectionPointsWithHorizontalLine(float y)
        {
            PointF[] res = new PointF[2];

            float d = _radius * _radius - (y - _middle.Y) * (y - _middle.Y);
            if (d < 0)
                return new PointF[0];
            
            float s = (float)Math.Sqrt(d);

            res[0] = new PointF(_middle.X + s, y);
            res[1] = new PointF(_middle.X - s, y);
            
            return res;
        }

        public PointF[] GetIntersectionPointsWithVerticalLine(float x)
        {
            PointF[] res = new PointF[2];
            float d = _radius * _radius - (x - _middle.X) * (x - _middle.X);
            if (d < 0)
                return new PointF[0];

            float s = (float)Math.Sqrt(d);
            
            res[0] = new PointF(x, _middle.Y + s);
            res[1] = new PointF(x, _middle.Y - s);
         
            return res;
        }

        public PointF[] GetIntersectionPointsWithCircle(Circle other)
        {
            FreeVector v = new FreeVector(_middle, other._middle);
            float d = v.Length;

            if (d < Math.Abs(_radius - other._radius)
             || d > _radius + other._radius)
                return new PointF[0];

            PointF[] res = new PointF[2];

            float a = (_radius * _radius - other._radius * other._radius + d * d) / (2 * d);

            FreeVector V0 = new FreeVector(_middle);
            FreeVector V1 = new FreeVector(other._middle);
            FreeVector V2 = V0 + (V1 - V0) * a / d;

            float h = (float)Math.Sqrt(_radius * _radius - a * a);

            res[0] = new PointF(V2.X - h * (V1.Y - V0.Y) / d,
                               V2.Y + h * (V1.X - V0.X) / d);

            res[1] = new PointF(V2.X + h * (V1.Y - V0.Y) / d,
                               V2.Y - h * (V1.X - V0.X) / d);

            return res;
        }

        public override void DrawContours(PaintTools g)
        {
            throw new NotImplementedException();
        }

        public override IHandler GetEntireShapeHandler(PointF clickedPoint, List<Shape> polygons, int polygonIndex)
        {
            throw new NotImplementedException();
        }

        public override IHandler GetPartOfShapeHandler(PointF clickedPoint, List<Shape> polygons, int polygonIndex, CheckBox checkbox = null)
        {
            throw new NotImplementedException();
        }

        public override bool IsClickedBy(PointF p)
        {
            throw new NotImplementedException();
        }

        public override void DrawFilling(PaintTools paintTools)
        {
            throw new NotImplementedException();
        }

        public override void SetFilling(FillingInfo fillingInfo)
        {
            throw new NotImplementedException();
        }

        public override void DeleteFilling()
        {
            throw new NotImplementedException();
        }
    }
}
