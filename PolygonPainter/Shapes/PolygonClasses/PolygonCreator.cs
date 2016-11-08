using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PolygonPainter.Shapes.PolygonClasses
{
    class PolygonCreator : Polygon
    {
        protected bool _isComplete;

        public PolygonCreator(Color vertexColor, Color lineColor)
            : base(vertexColor, lineColor)
        {
            _isComplete = false;
        }

        public void AddVertexClickedBy(PointD p)
        {
            int clickedVertexIndex = _GetIndexOfVertexClickedBy(p);
            
            if (clickedVertexIndex == 0 && this.NumberOfVertices >= 3)
            {
                _isComplete = true;
                _SetAntiClockwiseOrderOfVertices();
            }
            else if (clickedVertexIndex == -1)
            {
                _vertexManager.AppendVertex(new Vertex(p, _defaultVertexColor));
                _sideColors.Add(_defaultSideColor);
            }
        }

        public bool IsComplete()
        {
            return _isComplete;
        }

        public override void DrawContours(PaintTools paintTools)
        {
            if (this.NumberOfVertices > 0)
            {
                if (_isComplete)
                    base.DrawContours(paintTools);
                else
                    _DrawPolyline(paintTools);
            }
        }
        
        public void UndoLastOperation()
        {
            if (!this.IsComplete() && this.NumberOfVertices > 0)
            {
                _vertexManager.DeleteVertex(this.NumberOfVertices - 1);
                _sideColors.RemoveAt(_sideColors.Count - 1);
            }
        }

        private void _SetAntiClockwiseOrderOfVertices()
        {
            if(!_IsAntiClockwiseVerticesOrder())
            {
                _vertexManager.Vertices.Reverse();
            }
        }

        private bool _IsAntiClockwiseVerticesOrder()
        {
            return this.Area() >= 0;
        } 
    }
}
