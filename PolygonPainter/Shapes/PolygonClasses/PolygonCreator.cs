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

        public void AddVertexClickedBy(PointF p)
        {
            int clickedVertexIndex = _GetIndexOfVertexClickedBy(p);

            if (clickedVertexIndex == 0 && this.NumberOfVertices >= 3)
            {
                _isComplete = true;
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

        public override void Draw(Graphics g)
        {
            if (this.NumberOfVertices > 0)
            {
                if (_isComplete)
                    base.Draw(g);
                else
                    _DrawPolyline(g);
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
    }
}
