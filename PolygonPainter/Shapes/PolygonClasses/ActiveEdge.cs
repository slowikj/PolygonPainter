using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PolygonPainter.Interfaces;

namespace PolygonPainter.Shapes.PolygonClasses
{
    public partial class PolygonFiller
    {
        class ActiveEdge
        {
            private double _currentX, _inc;
            private int _YLast;

            public int YLast
            {
                get
                {
                    return _YLast;
                }
            }

            public double CurrentX
            {
                get
                {
                    return _currentX;
                }

                set
                {
                    _currentX = value;
                }
            }

            public ActiveEdge(Segment segment)
            {
                _YLast = (int)segment.End.Y;
                _currentX = segment.Begin.X;
                _inc = -(segment.Begin.X - segment.End.X) / (segment.Begin.Y - segment.End.Y);
            }

            public void UpdateX ()
            {
                _currentX += _inc;
            }

            public override string ToString()
            {
                return "{" + "currentX: " + _currentX.ToString() + ", _YLast: " + _YLast.ToString() + "}";
            }
        }
    }
}
