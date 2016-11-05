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

namespace PolygonPainter
{
    public struct FreeVector
    {
        private PointF _p;

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
    
        public float Length
        {
            get
            {
                return _GetLength();
            }
            set
            {
                float oldLength = _GetLength();
                float x = _p.X * value / oldLength;
                float y = _p.Y * value / oldLength;
                _p = new PointF(x, y);
          
            }
        }

        public float X
        {
            get
            {
                return _p.X;
            }
        }

        public float Y
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

        private float _GetLength()
        {
            return (float)Math.Sqrt(_p.X * _p.X + _p.Y * _p.Y);
        }

        public FreeVector(PointF p)
        {
            _p = p;
        }

        public FreeVector(PointF begin, PointF end)
        {
            _p = new PointF(end.X - begin.X, end.Y - begin.Y);
        }

        public static PointF operator+ (PointF p, FreeVector v)
        {
            return new PointF(p.X + v._p.X, p.Y + v._p.Y);
        }

        public static FreeVector operator+ (FreeVector a, FreeVector b)
        {
            return new FreeVector(new PointF(a._p.X + b._p.X, a._p.Y + b._p.Y));
        }

        public static FreeVector operator* (FreeVector v, float r)
        {
            return new FreeVector(new PointF(v._p.X * r, v._p.Y * r));
        }

        public static FreeVector operator/ (FreeVector v, float r)
        {
            return new FreeVector(new PointF(v._p.X / r, v._p.Y / r));
        }

        public static FreeVector operator- (FreeVector v)
        {
            return new FreeVector(new PointF(-v._p.X, -v._p.Y));
        }

        public static FreeVector operator- (FreeVector a, FreeVector b)
        {
            return a + (-b);
        }
    }
}
