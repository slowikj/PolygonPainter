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
using PolygonPainter.Modes;
using PolygonPainter.Shapes.PolygonClasses;
using PolygonPainter.Interfaces;

namespace PolygonPainter.Modes
{
    class AddVertexToPolygonMode : Mode
    {
        public AddVertexToPolygonMode(List<Shape> shapes, PictureBox canvas)
            : base(shapes, canvas)
        {
        }

        public override void MouseClick(object obj, MouseEventArgs e)
        {
            for (int i = _shapes.Count - 1; i >= 0; --i)
            {
                if (_shapes[i] is Polygon)
                {
                    IVertexAdder polygonAdder = (_shapes[i] as Polygon).GetVertexAdder();
                    if (polygonAdder.AddVertexClickedBy(e.Location))
                    {
                        this.UpdateCanvas();
                        break;
                    }             
                }
            }
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

        public override bool IsModeChangeForbidden()
        {
            return false;
        }

        public override string ToString()
        {
            return "AddVertexToPolygonMode";
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
