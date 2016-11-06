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
            private float _currentX, _inc;
            private int _YLast;

            public int YLast
            {
                get
                {
                    return _YLast;
                }
            }

            public float CurrentX
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

            public ActiveEdge(Line line)
            {
                _YLast = (int)line.End.Y;
                _currentX = line.Begin.X;
                _inc = -(line.Begin.X - line.End.X) / (line.Begin.Y - line.End.Y);
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
