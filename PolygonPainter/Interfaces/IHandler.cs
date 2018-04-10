using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonPainter.Interfaces
{
    public interface IHandler
    {
        void Mark();
        void Unmark();
        void Delete();
        bool Move(PointD currentPoint);
        void FixAutomaticRelations();
    }
}
