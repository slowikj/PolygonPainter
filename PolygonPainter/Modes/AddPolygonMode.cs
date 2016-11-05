using System;
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

namespace PolygonPainter.Modes
{
    public class AddPolygonMode : Mode
    {
        private PolygonCreator _currentPolygon;
        private Color _vertexColor, _lineColor;
        
        public AddPolygonMode (List<Shape> Shapes, PictureBox canvas, Color vertexColor, Color lineColor)
            : base(Shapes, canvas)
        {
            _vertexColor = vertexColor;
            _lineColor = lineColor;
            
            _currentPolygon = new PolygonCreator(_vertexColor, _lineColor);
        }

        public override void MouseClick(object obj, MouseEventArgs e)
        {
            PointF clickedPointF = e.Location;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    _ProcessAddingPointF(e.Location);
                    _canvas.Invalidate();
                    _canvas.Update();
                    break;
                case MouseButtons.Right:
                    _UndoLastOperation();
                    _canvas.Invalidate();
                    _canvas.Update();
                    break;
            }            
        }

        public override string ToString()
        {
            return "AddPolygonMode";
        }

        private void _ProcessAddingPointF(PointF clickedPoint)
        {
            if (_currentPolygon.NumberOfVertices == 0)
                _StartCreatingNewPolygon(clickedPoint);
            else
                _AddNewPointToPolygon(clickedPoint);

            if (_currentPolygon.IsComplete())
                _currentPolygon = new PolygonCreator(_vertexColor, _lineColor);
        }

        private void _UndoLastOperation()
        {
            if (_currentPolygon.NumberOfVertices > 0)
            {
                _currentPolygon.UndoLastOperation();

                if (_currentPolygon.NumberOfVertices == 0)
                    _shapes.RemoveAt(_shapes.Count - 1);
            }
        }

        private void _StartCreatingNewPolygon(PointF point)
        {
            _shapes.Add(_currentPolygon);

            _AddNewPointToPolygon(point);
        }
        
        private void _AddNewPointToPolygon(PointF point)
        {
            _currentPolygon.AddVertexClickedBy(point);
        }

        public override void MouseMove(object obj, MouseEventArgs e)
        {
        }

        public override void MouseUp(object obj, MouseEventArgs e)
        {
        }
        
        public override void MousefloatClick(object obj, MouseEventArgs e)
        {
        }

        public override bool IsModeChangeForbidden()
        {
            return _currentPolygon.NumberOfVertices > 0 && !_currentPolygon.IsComplete();
        }
    }
}
