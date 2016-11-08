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

namespace PolygonPainter.Shapes.PolygonClasses
{
   public partial class Polygon : Shape
   {
        private class EntirePolygonHandler : AbstractPolygonHandler
        {
            private PointD _clickedPoint;

            public EntirePolygonHandler(Polygon currentPolygon,
                                        List<Shape> polygons,
                                        int polygonIndex, PointD clickedPoint, CheckBox checkBox = null, Color? markingColor = null)
                : base(currentPolygon, polygons, polygonIndex, markingColor)
            {
                _polygonIndex = polygonIndex;
                _clickedPoint = clickedPoint;

                _polygons = polygons;
            }

            public override void Delete()
            {
                _polygons.RemoveAt(_polygonIndex);
                _DeletePolygonIfTooSmall();
            }

            public override void Mark()
            {
                _SetColor(_markingColor, _markingColor);
            }

            public override void Unmark()
            {
                _SetColor(_currentPolygon._defaultVertexColor,
                          _currentPolygon._defaultSideColor);
            }

            public override bool Move(PointD currentPoint)
            {
                FreeVector vector = new FreeVector(_clickedPoint, currentPoint);

                _currentPolygon._vertexManager.MoveAll(vector);

                _clickedPoint = currentPoint;

                return true;
            }
        }

    }
}
