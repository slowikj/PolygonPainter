using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PolygonPainter.Shapes;
using PolygonPainter.Shapes.PolygonClasses;


namespace PolygonPainter.Shapes.PolygonClasses
{
    public partial class Polygon : Shape
    {
        private class PolygonSideHandler : AbstractPolygonHandler
        {
            protected int _sideIndex;
            protected PointD _clickedPoint;

            public PolygonSideHandler(Polygon currentPolygon, List<Shape> polygons, int polygonIndex,
                                int sideIndex, PointD clickedPoint, CheckBox checkBox = null, Color? markingColor = null)
                : base(currentPolygon, polygons, polygonIndex, markingColor)
            {
                _sideIndex = sideIndex;
                _clickedPoint = clickedPoint;
            }

            public override void Delete()
            {
                _RemoveSide(_sideIndex);
                _DeletePolygonIfTooSmall();
            }

            public override void Mark()
            {
                _SetColorOfSide(_sideIndex, _markingColor);
            }

            public override bool Move(PointD currentPoint)
            {
                return false;
            }

            public override void Unmark()
            {
                _SetColorOfSide(_sideIndex, _currentPolygon._defaultSideColor);
            }
        }

    }
}
