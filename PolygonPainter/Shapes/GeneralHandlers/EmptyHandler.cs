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
using PolygonPainter.Interfaces;

namespace PolygonPainter.Shapes.GeneralHandlers
{
    class EmptyHandler : IHandler
    {
        public EmptyHandler()
        {
        }

        public void Delete()
        {
        }

        public void FixAutomaticRelations()
        {
        }

        public void Mark()
        {
        }

        public bool Move(PointD currentPoint)
        {
            return false;
        }

        public void Unmark()
        {
        }
    }
}
