using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonPainter.Shapes
{
    public class Vertex
    {
        private PointD _middle;
        private SolidBrush _brush;
        private double _radius;
        private const double _DEFAULT_RADIUS = (double)5.0;

        public PointD Location
        {
            get
            {
                return _middle;
            }
            set
            {
                _middle = value;
            }
        }

        public Color Color
        {
            get
            {
                return _brush.Color;
            }
        }
         
        public Vertex (PointD point, Color color, double radius = _DEFAULT_RADIUS)
        {
            _middle = point;
            _brush = new SolidBrush(color);
            _radius = radius;
        }

        public void Draw (PaintTools paintTools)
        {
            paintTools.Graphics.FillEllipse(_brush, (float)(_middle.X - _radius), (float)(_middle.Y - _radius),
                                     (float)(_radius + _radius), (float)(_radius + _radius));
        }

        public bool IsClickedBy (PointD p)
        {
            return Shape.DistanceSquared(_middle, p) <= _radius * _radius;
        }

        public PointD GetAngleRotated(PointD p, double angle)
        {
            PointD res = new PointD();
            res.X = (int)((_middle.X - p.X) * Math.Cos(angle) - (_middle.Y - p.Y) * Math.Sin(angle) + p.X);
            res.Y = (int)((_middle.X - p.X) * Math.Sin(angle) - (_middle.Y - p.Y) * Math.Cos(angle) + p.Y);

            return res;
        }
    }
}
