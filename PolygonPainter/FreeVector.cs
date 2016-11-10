using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

using PolygonPainter.Shapes;

namespace PolygonPainter
{
    public struct FreeVector
    {
        private PointD _p;

        const double eps_deg = 5 * Math.PI / 180;

        public bool IsVertical
        {
            get
            {
                return _p.X == 0;
            }
        }

        public bool IsApproxVertical()
        {
            //double angle = Math.Atan(_p.Y / _p.X);

            //return Math.Abs(angle - (Math.PI / 2)) <= (eps_deg * 1.5);

            FreeVector v = this;
            v.Length = 1;

            return Math.Abs(_p.X) <= 5;
        }

        public bool IsHorizontal
        {
            get
            {
                return _p.Y == 0;
            }
        }

        public bool IsApproxHorizontal()
        {
            //double angle = Math.Atan(_p.Y / _p.X);

            //return Math.Abs(angle) <= eps_deg;

            FreeVector v = this;
            v.Length = 1;

            return Math.Abs(_p.Y) <= 5;
        }

        public double Length
        {
            get
            {
                return _GetLength();
            }
            set
            {
                double oldLength = _GetLength();
                double x = _p.X * value / oldLength;
                double y = _p.Y * value / oldLength;
                _p = new PointD(x, y);

            }
        }

        public double X
        {
            get
            {
                return _p.X;
            }
        }

        public double Y
        {
            get
            {
                return _p.Y;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return _p.X == 0 && _p.Y == 0;
            }
        }

        public FreeVector(PointD p)
        {
            _p = p;
        }

        public FreeVector(PointD begin, PointD end)
        {
            _p = new PointD(end.X - begin.X, end.Y - begin.Y);
        }

        public double CrossProduct(FreeVector other)
        {
            Vector v = new Vector(_p.X, _p.Y);
            Vector u = new Vector(other._p.X, other._p.Y);
            //return this.X * other.Y - this.Y * other.X;

            return Vector.CrossProduct(v, u);
        }

        public Direction GetDirection(FreeVector v)
        {
            return this.CrossProduct(v) >= 0 ? Direction.Anticlockwise
                                             : Direction.Clockwise;
        }

        private double _GetLength()
        {
            return (double)Math.Sqrt(_p.X * _p.X + _p.Y * _p.Y);
        }

        public static PointD operator +(PointD p, FreeVector v)
        {
            return new PointD(p.X + v._p.X, p.Y + v._p.Y);
        }

        public static FreeVector operator +(FreeVector a, FreeVector b)
        {
            return new FreeVector(new PointD(a._p.X + b._p.X, a._p.Y + b._p.Y));
        }

        public static FreeVector operator *(FreeVector v, double r)
        {
            return new FreeVector(new PointD(v._p.X * r, v._p.Y * r));
        }

        public static FreeVector operator /(FreeVector v, double r)
        {
            return new FreeVector(new PointD(v._p.X / r, v._p.Y / r));
        }

        public static FreeVector operator -(FreeVector v)
        {
            return new FreeVector(new PointD(-v._p.X, -v._p.Y));
        }

        public static FreeVector operator -(FreeVector a, FreeVector b)
        {
            return a + (-b);
        }
    }
}
