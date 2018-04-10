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

using PolygonPainter.Interfaces;

namespace PolygonPainter.Shapes.PolygonClasses
{
    public partial class Polygon : Shape
    {
        public class PolygonVertexAdder: IVertexAdder
        {
            private Polygon _polygon;

            public PolygonVertexAdder(Polygon polygon)
            {
                _polygon = polygon;
            }

            public bool AddVertexClickedBy(PointD clickedPoint)
            {
                int sideIndex = _polygon._GetIndexOfSideClickedBy(clickedPoint);

                if (sideIndex != -1)
                {
                    Segment side = _polygon._GetSide(sideIndex);
                    _AddVertex(sideIndex, new Vertex(side.Middle, _polygon._defaultVertexColor));
                    return true;
                }

                return false;
            }

            protected void _AddVertex(int sideIndex, Vertex newVertex)
            {
                _polygon._vertexManager.AddVertex(sideIndex, newVertex);
                _polygon._sideColors.Insert((sideIndex + 1) % _polygon.NumberOfVertices,
                                            _polygon._defaultSideColor);
            }
        }
    }
}
