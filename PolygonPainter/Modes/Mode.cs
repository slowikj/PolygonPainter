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
    public abstract class Mode
    {
        protected List<Shape> _shapes;
        protected PictureBox _canvas;

        public Mode (List<Shape> Shapes, PictureBox canvas)
        {
            _shapes = Shapes;
            _canvas = canvas;
        }

        public abstract void MouseClick(object obj, MouseEventArgs e);
        public abstract void MouseUp(object obj, MouseEventArgs e);
        public abstract void MouseMove(object obj, MouseEventArgs e);
        public abstract void MousefloatClick(object obj, MouseEventArgs e);
        public abstract bool IsModeChangeForbidden();
        
    }
}
