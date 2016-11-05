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
    public class FillPolygonMode : Mode
    {
        public FillPolygonMode(List<Shape> shapes, PictureBox canvas)
            : base(shapes, canvas)
        {
        }

        public override bool IsModeChangeForbidden()
        {
            return false;
        }

        public override void MouseClick(object obj, MouseEventArgs e)
        {
            int polygonIndex = _GetClickedPolygonIndex(e.Location);

            if (polygonIndex != -1)
            {
                (_shapes[polygonIndex] as Polygon).Filler = new PolygonFiller(Color.Magenta);

                _canvas.Invalidate();
                _canvas.Update();
            }
        }

        private int _GetClickedPolygonIndex(Point p)
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
