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
using PolygonPainter.Interfaces;

namespace PolygonPainter.Shapes.PolygonClasses
{
    public partial class Polygon : Shape
    {
        private abstract class AbstractPolygonHandler : IHandler
        {
            protected readonly Color _DEFAULT_MARKING_COLOR = Color.Green;

            protected Polygon _currentPolygon;
            protected List<Shape> _polygons;
            protected int _polygonIndex;
            protected Color _markingColor;

            public AbstractPolygonHandler(Polygon currentPolygon,
                                          List<Shape> polygons,
                                          int polygonIndex,
                                          Color? markingColor = null)
            {
                _currentPolygon = currentPolygon;
                _polygons = polygons;
                _polygonIndex = polygonIndex;
                _markingColor = markingColor ?? _DEFAULT_MARKING_COLOR;
            }
            

            protected void _SetColorOfSide(int sideIndex, Color newColor)
            {
                _currentPolygon._sideColors[sideIndex] = newColor;
            }
            
            protected void _DeleteVertex(int vertexIndex)
            {
                _currentPolygon._vertexManager.DeleteVertex(vertexIndex);
                _currentPolygon._sideColors.RemoveAt(vertexIndex);
            }

            protected void _RemoveSide(int sideIndex)
            {
                // tutaj powinnam jakos sprawdzac, aby sasiednie relacje zostawic takie jakie byc powinny
                // bo przeciez usuwam tylko jedna krawedz
            
                _currentPolygon._vertexManager.DeleteVertex(sideIndex);
                _currentPolygon._sideColors.RemoveAt(sideIndex);
            }

            protected void _SetColor(Color vertexColor, Color sideColor)
            {
                for (int i = 0; i < _currentPolygon.NumberOfVertices; ++i)
                {
                    _currentPolygon._vertexManager.SetColorOfVertex(i, vertexColor);
                    _SetColorOfSide(i, sideColor);
                }
            }

            protected void _DeletePolygonIfTooSmall()
            {
                if (_currentPolygon.NumberOfVertices < 3)
                    _polygons.RemoveAt(_polygonIndex);
            }
            
            public abstract void Mark();
            public abstract void Unmark();
            public abstract void Delete();
            public abstract bool Move(PointF currentPoint);

            public virtual void FixAutomaticRelations()
            {
            }
        }
    }
}
