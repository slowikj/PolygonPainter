using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using PolygonPainter.Shapes;
using PolygonPainter.Modes;

namespace PolygonPainter
{
    public struct PointD
    {
        private double _x;
        private double _y;

        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public PointD(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public static implicit operator PointD(PointF p)
        {
            return new PointD(p.X, p.Y);
        }

        public static implicit operator PointD(Point p)
        {
            return new PointD(p.X, p.Y);
        }

        public static explicit operator Point(PointD p)
        {
            return new Point((int)p.X, (int)p.Y);
        }

        public static explicit operator PointF(PointD p)
        {
            return new PointF((float)p.X, (float)p.Y);
        }

        public static bool operator== (PointD a, PointD b)
        {
            return Shape.EqualsEps(a.X, b.X, Shape.MATH_EPS)
                && Shape.EqualsEps(a.Y, b.Y, Shape.MATH_EPS);
        }

        public static bool operator!= (PointD a, PointD b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return String.Format("({0}, {1})", _x, _y);
        }

        public override bool Equals(object obj)
        {
            if(!(obj is PointD))
            {
                return false;
            }

            PointD other = (PointD)obj;

            return this == other;
        }

        public override int GetHashCode()
        {
            return ((PointF)this).GetHashCode();
        }
    }
}
