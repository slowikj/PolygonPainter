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
    partial class Polygon : Shape
    {
        class PolygonVertexHandler : AbstractPolygonHandler
        {
            protected int _vertexIndex;
            protected CheckBox _relationCheckBox;

            public PolygonVertexHandler(Polygon currentPolygon, List<Shape> polygons, int polygonIndex,
                                   int vertexIndex, CheckBox relationCheckBox, Color? markingColor = null)
                : base(currentPolygon, polygons, polygonIndex, markingColor)
            {
                _vertexIndex = vertexIndex;
                _relationCheckBox = relationCheckBox;
            }

            public override void Mark()
            {
                _currentPolygon._vertexManager.SetColorOfVertex(_vertexIndex, _markingColor);
            }

            public override void Unmark()
            {
                _currentPolygon._vertexManager.SetColorOfVertex(_vertexIndex, _currentPolygon._defaultVertexColor);
            }

            public override void Delete()
            {
                _DeleteVertex(_vertexIndex);
                _DeletePolygonIfTooSmall();
            }

            public override bool Move(PointF currentPoint)
            {
                PointF oldPoint = _currentPolygon._vertexManager.GetVertex(_vertexIndex).Location;

                FreeVector vector = new FreeVector(oldPoint, currentPoint);

                bool res = _currentPolygon._vertexManager.TryToMoveVertex(_vertexIndex, vector);
                if (res && _relationCheckBox.Checked) // automatic relation
                {
                    _currentPolygon._vertexManager.AddAutomaticRelations();
                }

                return res;
            }

            public override void FixAutomaticRelations()
            {
                if (_relationCheckBox.Checked)
                    _currentPolygon._vertexManager.FixRelations();
            }
        }

    }
}
