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

using PolygonPainter.Shapes.PolygonClasses.Relations;
using PolygonPainter.Shapes;
using PolygonPainter.Shapes.PolygonClasses;
using PolygonPainter.Interfaces;

namespace PolygonPainter.Modes
{
    class SetRelationMode : Mode
    {
        public SetRelationMode(List<Shape> _shapes, PictureBox canvas)
            : base(_shapes, canvas)
        {
        }

        public override bool IsModeChangeForbidden()
        {
            return false;
        }

        public override void MouseClick(object obj, MouseEventArgs e)
        {
            IRelationSetter relationSetter = _GetRelationSetter(e.Location);
            
            switch (e.Button)
            {
                case MouseButtons.Left:
                    try
                    {
                        _SetRelation(relationSetter);
                    }
                    catch (OperationImpossibleException exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                    
                    break;
                case MouseButtons.Right:
                    _UnsetRelation(relationSetter);

                    break;
            }
        }

        private void _SetRelation(IRelationSetter relationSetter)
        {
            if (relationSetter is EmptyRelationSetter)
                return;

            Form2 dialogForm = new Form2();
            dialogForm.ShowDialog();
            // MessageBox.Show(string.Format("{0}", dialogForm.Relation.ToString()));
            
            dialogForm.Dispose();

            if (relationSetter.SetRelation(dialogForm.Relation))
            {
                _canvas.Invalidate();
                _canvas.Update();
            }
        }

        private void _UnsetRelation(IRelationSetter relationSetter)
        {
            if (relationSetter.UnsetRelation())
            {
                _canvas.Invalidate();
                _canvas.Update();
            }
        }

        private IRelationSetter _GetRelationSetter(PointF clickedPoint)
        {
            for (int i = 0; i < _shapes.Count; ++i)
            {
                if (_shapes[i] is Polygon)
                {
                    var res = (_shapes[i] as Polygon).GetRelationSetter(clickedPoint);
                    if (!(res is EmptyRelationSetter))
                        return res;
                }
            }

            return new EmptyRelationSetter();
        }

        public override void MousefloatClick(object obj, MouseEventArgs e)
        {
        }

        public override void MouseMove(object obj, MouseEventArgs e)
        {
        }

        public override void MouseUp(object obj, MouseEventArgs e)
        {
        }

        public override string ToString()
        {
            return "SetRelationMode";
        }
    }
}
