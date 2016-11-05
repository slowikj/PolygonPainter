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

namespace PolygonPainter.Shapes.PolygonClasses.Relations
{
    class HorizontalRelation : Relation
    {
        public HorizontalRelation (bool isFixed = true)
            : base(isFixed)
        {
        }

        public override Relation Copy()
        {
            return new HorizontalRelation();
        }

        public override void Draw(Graphics g, Line side)
        {
            base.Draw(g, side);

            PointF location = _GetLocation(side);

            Line horizontalLine = new Line(new PointF(location.X - _distanceFromMiddle, location.Y),
                                           new PointF(location.X + _distanceFromMiddle, location.Y),
                                           _penColor);

            horizontalLine.Draw(g);
        }

        public override bool IsEmpty()
        {
            return false;
        }

        public override string ToString()
        {
            return (_isFixed ? "HorizontalRelation"
                             : "EmptyRelation");            
        }
    }
}
