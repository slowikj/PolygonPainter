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
    public class VerticalRelation : Relation
    {
        public VerticalRelation(bool isFixed = true)
            : base(isFixed)
        {
        }

        public override Relation Copy()
        {
            return new VerticalRelation();
        }

        public override void Draw(PaintTools paintTools, Line side)
        {
            base.Draw(paintTools, side);

            PointF location = _GetLocation(side);

            Line verticalLine = new Line(new PointF(location.X, location.Y - _distanceFromMiddle),
                                         new PointF(location.X, location.Y + _distanceFromMiddle),
                                         _penColor);

            verticalLine.Draw(paintTools);
        }

        public override bool IsEmpty()
        {
            return false;
        }

        public override string ToString()
        {
            return (_isFixed ? "VerticalRelation" : "EmptyRelation");
        }
    }
}
