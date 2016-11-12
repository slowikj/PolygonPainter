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
        private Color _vertexColor, _sideColor;
        
        public AddPolygonMode (List<Shape> Shapes, PictureBox canvas, Color vertexColor, Color sideColor)
            : base(Shapes, canvas)
        {
            _vertexColor = vertexColor;
            _sideColor = sideColor;
            
            _currentPolygon = new PolygonCreator(_vertexColor, _sideColor);
        }

        public override void MouseClick(object obj, MouseEventArgs e)
        {
            PointD clickedPointD = e.Location;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    _ProcessAddingPointD(e.Location);
                    break;
                case MouseButtons.Right:
                    _UndoLastOperation();
                    break;
            }

            this.UpdateCanvas();     
        }

        public override string ToString()
        {
            return "AddPolygonMode";
        }

        private void _ProcessAddingPointD(PointD clickedPoint)
        {
            if (_currentPolygon.NumberOfVertices == 0)
                _StartCreatingNewPolygon(clickedPoint);
            else
                _AddNewPointToPolygon(clickedPoint);

            if (_currentPolygon.IsComplete())
                _currentPolygon = new PolygonCreator(_vertexColor, _sideColor);
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

        private void _StartCreatingNewPolygon(PointD point)
        {
            _shapes.Add(_currentPolygon);

            _AddNewPointToPolygon(point);
        }
        
        private void _AddNewPointToPolygon(PointD point)
        {
            _currentPolygon.AddVertexClickedBy(point);
        }

        public override void MouseMove(object obj, MouseEventArgs e)
        {
        }

        public override void MouseUp(object obj, MouseEventArgs e)
        {
        }
        
        public override void MousedoubleClick(object obj, MouseEventArgs e)
        {
        }

        public override bool IsModeChangeForbidden()
        {
            return _currentPolygon.NumberOfVertices > 0 && !_currentPolygon.IsComplete();
        }

        public override void Clear()
        {
        }

        public override void KeyEventHandler(Keys keyData)
        {
        }

        public override void TimerTickHandler()
        {
        }
    }
}
