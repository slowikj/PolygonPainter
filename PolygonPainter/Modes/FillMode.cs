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
    public class FillMode : Mode
    {
        public FillMode(List<Shape> shapes, PictureBox canvas)
            : base(shapes, canvas)
        {
        }

        public override bool IsModeChangeForbidden()
        {
            return false;
        }

        public override void MouseClick(object obj, MouseEventArgs e)
        {
            int shapeIndex = _GetClickedShapeIndex(e.Location);

            if (shapeIndex != -1)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        _SetFilling(shapeIndex);
                        break;
                    case MouseButtons.Right:
                        _DeleteFilling(shapeIndex);
                        break;
                }
            }
        }

        private void _SetFilling(int shapeIndex)
        {
            FillingInfo filling = _GetFilling();

            if (filling != null)
            {
                _shapes[shapeIndex].SetFilling(filling);

                this.UpdateCanvas();
            }
        }

        private void _DeleteFilling(int shapeIndex)
        {
            _shapes[shapeIndex].DeleteFilling();

            this.UpdateCanvas();
        }

        private FillingInfo _GetFilling()
        {
            FillingDialog fillingDialog = new FillingDialog();
            DialogResult dialogResult = fillingDialog.ShowDialog();
            
            switch (dialogResult)
            {
                case DialogResult.OK:
                    return fillingDialog.FillingInfo;
                default:
                    return null;
            }
        }

        private int _GetClickedShapeIndex(Point p)
        {
            for (int i = 0; i < _shapes.Count; ++i)
            {
                if (_shapes[i].IsClickedBy(p))
                    return i;
            }

            return -1;
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
    }
}
