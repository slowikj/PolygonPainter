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
        private double[] _lightPoint;

        public FillMode(List<Shape> shapes, PictureBox canvas)
            : base(shapes, canvas)
        {
            _lightPoint = new double[3] { canvas.Width / 2, canvas.Height / 2, 50 };
        }

        public override void KeyEventHandler(Keys keyData)
        {
            const int pixelsChange = 10;

            switch(keyData)
            {
                case Keys.Up:
                    _lightPoint[1] = Math.Max(0, _lightPoint[1] - pixelsChange);
                    break;
                case Keys.Down:
                    _lightPoint[1] = Math.Min(_canvas.Height, _lightPoint[1] + pixelsChange);
                    break;
                case Keys.Left:
                    _lightPoint[0] = Math.Max(0, _lightPoint[0] - pixelsChange); 
                    break;
                case Keys.Right:
                    _lightPoint[0] = Math.Min(_canvas.Width, _lightPoint[0] + pixelsChange);
                    break;
                case Keys.W:
                    _lightPoint[2] = Math.Min(1000, _lightPoint[2] + pixelsChange);
                    break;
                case Keys.S:
                    _lightPoint[2] = Math.Max(1, _lightPoint[2] - pixelsChange);
                    break;
            }

            this.UpdateCanvas();
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
                _shapes[shapeIndex].SetFilling(filling, _lightPoint);

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
            for (int i = _shapes.Count - 1; i >= 0; --i)
            {
                if (_shapes[i].IsClickedBy(p))
                    return i;
            }

            return -1;
        }

        public override void MousedoubleClick(object obj, MouseEventArgs e)
        {
        }

        public override void MouseMove(object obj, MouseEventArgs e)
        {
        }

        public override void MouseUp(object obj, MouseEventArgs e)
        {
        }

        public override void Clear()
        {
        }
    }
}
