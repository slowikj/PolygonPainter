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
        private PointF _middle;
        private SolidBrush _brush;
        private float _radius;
        private const float _DEFAULT_RADIUS = (float)5.0;

        public PointF Location
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
         
        public Vertex (PointF point, Color color, float radius = _DEFAULT_RADIUS)
        {
            _middle = point;
            _brush = new SolidBrush(color);
            _radius = radius;
        }

        public void Draw (Graphics g)
        {
            g.FillEllipse(_brush, _middle.X - _radius, _middle.Y - _radius,
                          _radius + _radius, _radius + _radius);
        }

        public bool IsClickedBy (PointF p)
        {
            return Shape.DistanceSquared(_middle, p) <= _radius * _radius;
        }

        public PointF GetAngleRotated(PointF p, float angle)
        {
            PointF res = new PointF();
            res.X = (int)((_middle.X - p.X) * Math.Cos(angle) - (_middle.Y - p.Y) * Math.Sin(angle) + p.X);
            res.Y = (int)((_middle.X - p.X) * Math.Sin(angle) - (_middle.Y - p.Y) * Math.Cos(angle) + p.Y);

            return res;
        }
    }
}
