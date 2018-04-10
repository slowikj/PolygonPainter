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
    public struct Segment
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
        }

        public PointD End
        {
            get
            {
                return _end;
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


        public Segment(PointD beg, PointD end, Color? color = null)
        {
            _beg = new Point();
            _end = new Point();
            _pen = new Pen(color ?? Color.Black);

            _AssignPoints(beg, end);
        }

        private void _AssignPoints(PointD a, PointD b)
        {
            _beg = _GetLowerPoint(a, b);
            _end = _GetUpperPoint(a, b);
        }

        private static PointD _GetLowerPoint(PointD a, PointD b)
        {
            if (a.Y != b.Y)
            {
                return a.Y > b.Y ? a
                                 : b;
            }
            else
            {
                return a.X > b.X ? b
                                 : a;
            }
        }

        private static PointD _GetUpperPoint(PointD a, PointD b)
        {
            if (a.Y != b.Y)
            {
                return a.Y > b.Y ? b
                                 : a;
            }
            else
            {
                return a.X > b.X ? a
                                 : b;
            }
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
            return this.HasPoint(p);
        }

        public bool ArePointsOnTheSameSide(PointD[] points)
        {
            FreeVector segmentVector = new FreeVector(_beg, _end);
            PointD beg = _beg;
            var r = points.Select(p => (segmentVector.GetDirection(new FreeVector(beg, p))));

            return r.All(x => x == Direction.Anticlockwise)
                || r.All(x => x == Direction.Clockwise);
        }



        public bool HasPoint(PointD p, double eps = Shape.DRAWING_EPS)
        {
            return Shape.EqualsEps(Math.Sqrt(Shape.DistanceSquared(_beg, p))
                                         + Math.Sqrt(Shape.DistanceSquared(p, _end)),
                                           Math.Sqrt(Shape.DistanceSquared(_beg, _end)),
                                   eps);
        }

        public SegmentIntersectionInfo GetIntersectionWith(Segment other)
        {
            double x1 = this.Begin.X, x2 = this.End.X;
            double y1 = this.Begin.Y, y2 = this.End.Y;

            double x3 = other.Begin.X, x4 = other.End.X;
            double y3 = other.Begin.Y, y4 = other.End.Y;
            
            // edges has one the same point
           
            if (_TouchesWithOneEnd(this.Begin, this.End, other.Begin, other.End))
            {
                return new SegmentIntersectionInfo(this.Begin, IntersectionType.Touches);
            }
            
            if (_TouchesWithOneEnd(this.Begin, this.End, other.End, other.Begin))
            {
                return new SegmentIntersectionInfo(this.Begin, IntersectionType.Touches);
            }
            
            if (_TouchesWithOneEnd(this.End, this.Begin, other.Begin, other.End))
            {
                return new SegmentIntersectionInfo(this.End, IntersectionType.Touches);
            }
            
            if (_TouchesWithOneEnd(this.End, this.Begin, other.End, other.Begin))
            {
                return new SegmentIntersectionInfo(this.End, IntersectionType.Touches);
            }

            double w = ((x1 - x2) * (y4 - y3)) - ((x4 - x3) * (y1 - y2));
            double wk = ((x4 - x2) * (y4 - y3)) - ((x4 - x3) * (y4 - y2));
            double wm = ((x1 - x2) * (y4 - y2)) - ((x4 - x2) * (y1 - y2));

            if (Shape.EqualsEps(w, 0, Shape.MATH_EPS)
                && Shape.EqualsEps(wm, (double)0.0, Shape.MATH_EPS) && Shape.EqualsEps(wk, 0, Shape.MATH_EPS))
            {
                // on the same line
                if (this.HasPoint(other.Begin, Shape.MATH_EPS) || this.HasPoint(other.End, Shape.MATH_EPS)
                    || other.HasPoint(this.Begin, Shape.MATH_EPS) || other.HasPoint(this.End, Shape.MATH_EPS))
                    return new SegmentIntersectionInfo(null, IntersectionType.Covers);
                else
                    return new SegmentIntersectionInfo(null, IntersectionType.Parallel);
            }
            else if (Shape.EqualsEps(w, 0, Shape.MATH_EPS)
                && (!Shape.EqualsEps(wk, (double)0.0, Shape.MATH_EPS) || !Shape.EqualsEps(wm, 0, Shape.MATH_EPS)))
                // parallel, but not on the same line
                return new SegmentIntersectionInfo(null, IntersectionType.Parallel);
            else
            {
                double k = (wk / w);
                double m = (wm / w);

                if (k >= 0.000000 && k <= 1.000000 && m >= 0.000000 && m <= 1.000000)
                {
                    double x = (double)((k * x1) + x2 - (k * x2));
                    double y = (double)((m * y3) + y4 - (m * y4));

                    if (Shape.EqualsEps(k, 0.0, Shape.MATH_EPS) || Shape.EqualsEps(k, 1.0, Shape.MATH_EPS)
                     || Shape.EqualsEps(m, 0.0, Shape.MATH_EPS) || Shape.EqualsEps(m, 1.0, Shape.MATH_EPS))
                        return new SegmentIntersectionInfo(new PointD(x, y), IntersectionType.Touches);

                    //,,normal'' intersection
                    return new SegmentIntersectionInfo(new PointD(x, y), IntersectionType.Intersects);
                }
                else if (k >= 0.000000 && k <= 1.000000 && !(m >= 0.000000 && m <= 1.000000))
                    //przedluzenie jednego odcinka przecina drugi odcinek
                    return new SegmentIntersectionInfo(null, IntersectionType.Disjoint);
                else if (!(k >= 0.000000 && k <= 1.000000) && (m >= 0.000000 && m <= 1.000000))
                    //to samo co wyzej
                    return new SegmentIntersectionInfo(null, IntersectionType.Disjoint);
                else if (!(k >= 0.000000 && k <= 1.000000) && !(m >= 0.000000 && m <= 1.000000))
                    //przedluzenia obu odcinkow sie przecinaja
                    return new SegmentIntersectionInfo(null, IntersectionType.Disjoint);
            }

            return new SegmentIntersectionInfo(null, IntersectionType.Disjoint);
        }

        private bool _TouchesWithOneEnd(PointD touchPointA, PointD beginA, PointD touchPointB, PointD endB)
        {
            if (Shape.EqualsEps(touchPointA.X, touchPointB.X, Shape.MATH_EPS)
                && Shape.EqualsEps(touchPointA.Y, touchPointB.Y, Shape.MATH_EPS))
            {
                double dist = (double)Math.Sqrt(Shape.DistanceSquared(beginA, endB));
                double sum = Math.Sqrt(Shape.DistanceSquared(beginA, touchPointA))
                            + Math.Sqrt(Shape.DistanceSquared(endB, touchPointB));

                if (Shape.EqualsEps(dist, sum, Shape.MATH_EPS))
                {
                    return true;
                }
            }

            return false;
        }
        
    }
}
