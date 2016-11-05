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
    public struct Line
    {
        private PointF _beg, _end;
        private Pen _pen;

        public Color Color
        {
            get
            {
                return _pen.Color;
            }
            set
            {
                _pen.Color = value;
            }
        }

        public PointF Begin
        {
            get
            {
                return _beg;
            }
            set
            {
                _beg = value;
            }
        }

        public PointF End
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
            }
        }

        public PointF Middle
        {
            get
            {
                return new PointF((_beg.X + _end.X) / 2,
                                 (_beg.Y + _end.Y) / 2);
            }
        }

        public int Length
        {
            get
            {
                return (int)Math.Sqrt((_beg.X - End.X) * (_beg.X - End.X)
                                      + (_beg.Y - End.Y) * (_beg.Y - End.Y));
            }
        }

        public bool IsVertical
        {
            get
            {
                return _beg.X == _end.X;
            }
        }

        public bool IsHorizontal
        {
            get
            {
                return _beg.Y == _end.Y;
            }
        }


        public Line(PointF beg, PointF end, Color? color = null)
        {
            _beg = _GetLowerPoint(beg, end);
            _end = _GetUpperPoint(beg, end);
            _pen = new Pen(color ?? Color.Black);
        }

        private static PointF _GetLowerPoint(PointF a, PointF b)
        {
            return a.Y > b.Y ? a
                             : b;
        }

        private static PointF _GetUpperPoint(PointF a, PointF b)
        {
            return a.Y > b.Y ? b
                             : a;
        }

        public void Draw(Graphics g)
        {
            g.DrawLine(_pen, _beg, _end);

            //int x1 = (int)_beg.X, y1 = (int)_beg.Y;
            //int x2 = (int)_end.X, y2 = (int)_end.Y;

            //int d, dx, dy, ai, bi, xi, yi;
            //int x = x1, y = y1;

            //// determining the direction for drawing
            //if (x1 < x2)
            //{
            //    xi = 1;
            //    dx = x2 - x1;
            //}
            //else
            //{
            //    xi = -1;
            //    dx = x1 - x2;
            //}
            //if (y1 < y2)
            //{
            //    yi = 1;
            //    dy = y2 - y1;
            //}
            //else
            //{
            //    yi = -1;
            //    dy = y1 - y2;
            //}

            //// put the first pixel
            //g.DrawRectangle(_pen, x, y, (float)0.1, (float)0.1);
            //// OX
            //if (dx > dy)
            //{
            //    ai = (dy - dx) * 2;
            //    bi = dy * 2;
            //    d = bi - dx;
            //    // a loop for x
            //    while (x != x2)
            //    {
            //        // coefficient test
            //        if (d >= 0)
            //        {
            //            x += xi;
            //            y += yi;
            //            d += ai;
            //        }
            //        else
            //        {
            //            d += bi;
            //            x += xi;
            //        }
            //        g.DrawRectangle(_pen, x, y, (float)0.1, (float)0.1);
            //    }
            //}
            //// OY
            //else
            //{
            //    ai = (dx - dy) * 2;
            //    bi = dx * 2;
            //    d = bi - dy;
            //    // a loop for y
            //    while (y != y2)
            //    {
            //        // coefficient test
            //        if (d >= 0)
            //        {
            //            x += xi;
            //            y += yi;
            //            d += ai;
            //        }
            //        else
            //        {
            //            d += bi;
            //            y += yi;
            //        }
            //        g.DrawRectangle(_pen, x, y, (float)0.1, (float)0.1);
            //    }
            //}
        }

        public bool IsClickedBy(PointF p)
        {
            return Shape.EqualsEps((float)Math.Sqrt(Shape.DistanceSquared(_beg, p)) 
                                        + (float)Math.Sqrt(Shape.DistanceSquared(p, _end)),
                                   (float)Math.Sqrt(Shape.DistanceSquared(_beg, _end)));
                
        }
    }
}
