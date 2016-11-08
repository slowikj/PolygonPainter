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
        private PointD _beg, _end;
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

        public PointD Begin
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

        public PointD End
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

        public PointD Middle
        {
            get
            {
                return new PointD((_beg.X + _end.X) / 2,
                                 (_beg.Y + _end.Y) / 2);
            }
        }

        public double Length
        {
            get
            {
                return Math.Sqrt((_beg.X - End.X) * (_beg.X - End.X)
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


        public Line(PointD beg, PointD end, Color? color = null)
        {
            _beg = _GetLowerPoint(beg, end);
            _end = _GetUpperPoint(beg, end);
            _pen = new Pen(color ?? Color.Black);
        }

        private static PointD _GetLowerPoint(PointD a, PointD b)
        {
            return a.Y > b.Y ? a
                             : b;
        }

        private static PointD _GetUpperPoint(PointD a, PointD b)
        {
            return a.Y > b.Y ? b
                             : a;
        }

        public void Draw(PaintTools paintTools)
        {
            paintTools.Graphics.DrawLine(_pen, (PointF)_beg, (PointF)_end);

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
            //g.DrawRectangle(_pen, x, y, (double)0.1, (double)0.1);
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
            //        g.DrawRectangle(_pen, x, y, (double)0.1, (double)0.1);
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
            //        g.DrawRectangle(_pen, x, y, (double)0.1, (double)0.1);
            //    }
            //}
        }

        public bool IsClickedBy(PointD p)
        {
            return Shape.EqualsEps(Math.Sqrt(Shape.DistanceSquared(_beg, p)) 
                                        + Math.Sqrt(Shape.DistanceSquared(p, _end)),
                                          Math.Sqrt(Shape.DistanceSquared(_beg, _end)));
                
        }

        public PointD? GetIntersectionWith(Line other)
        {
            double x1 = this.Begin.X, x2 = this.End.X;
            double y1 = this.Begin.Y, y2 = this.End.Y;

            double x3 = other.Begin.X, x4 = other.End.X;
            double y3 = other.Begin.Y, y4 = other.End.Y;

            // edges has one the same point
           
            if (_TouchesWithOnePoint(this.Begin, this.End, other.Begin, other.End))
            {
                return this.Begin;
            }
            
            if (_TouchesWithOnePoint(this.Begin, this.End, other.End, other.Begin))
            {
                return this.Begin;
            }
            
            if (_TouchesWithOnePoint(this.End, this.Begin, other.Begin, other.End))
            {
                return this.End;
            }
            
            if (_TouchesWithOnePoint(this.End, this.Begin, other.End, other.Begin))
            {
                return this.End;
            }

            double w = ((x1 - x2) * (y4 - y3)) - ((x4 - x3) * (y1 - y2));
            double wk = ((x4 - x2) * (y4 - y3)) - ((x4 - x3) * (y4 - y2));
            double wm = ((x1 - x2) * (y4 - y2)) - ((x4 - x2) * (y1 - y2));

            if (Shape.EqualsEps(w, 0, (double)0.0001) 
                && Shape.EqualsEps(wm, (double)0.0, (double)0.0001) && Shape.EqualsEps(wk, 0, (double)0.0001))
                return null;
            else if (Shape.EqualsEps(w, 0, (double)0.0001) 
                && (!Shape.EqualsEps(wk, (double)0.0, (double)0.0001) || !Shape.EqualsEps(wm, 0, (double)0.0001)))
                return null;
            else
            {
                double k = (wk / w);
                double m = (wm / w);
                
                if (k >= 0.000000 && k <= 1.000000 && m >= 0.000000 && m <= 1.000000)
                {
                    //przeciecie

                    double x = (double)((k * x1) + x2 - (k * x2));
                    double y = (double)((m * y3) + y4 - (m * y4));
                    return new PointD(x, y);
                }
                else if (k >= 0.000000 && k <= 1.000000 && !(m >= 0.000000 && m <= 1.000000))
                    //przedluzenie jednego odcinka przecina drugi odcinek
                    return null;
                else if (!(k >= 0.000000 && k <= 1.000000) && (m >= 0.000000 && m <= 1.000000))
                    //to samo co wyzej
                    return null;
                else if (!(k >= 0.000000 && k <= 1.000000) && !(m >= 0.000000 && m <= 1.000000))
                    //przedluzenia obu odcinkow sie przecinaja
                    return null;
            }

            return null;
        }

        private bool _TouchesWithOnePoint(PointD touchPointA, PointD beginA, PointD touchPointB, PointD endB)
        {
            if (Shape.EqualsEps(touchPointA.X, touchPointB.X, 0.0001)
                && Shape.EqualsEps(touchPointA.Y, touchPointB.Y, 0.0001))
            {
                double dist = (double)Math.Sqrt(Shape.DistanceSquared(beginA, endB));
                double sum = Math.Sqrt(Shape.DistanceSquared(beginA, touchPointA))
                            + Math.Sqrt(Shape.DistanceSquared(endB, touchPointB));

                if (Shape.EqualsEps(dist, sum, 0.00001))
                {
                    return true;
                }
            }

            return false;
        }
        
    }
}
