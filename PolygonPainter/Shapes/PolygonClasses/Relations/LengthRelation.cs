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
    class LengthRelation : Relation
    {
        private int _length;
        private readonly Font _font;

        public LengthRelation (bool isFixed = true)
            : base(isFixed)
        {
        }

        public int Length
        {
            get
            {
                return _length;
            }
        }

        public LengthRelation(int length)
        {
            _length = length;
            _font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold);
        }

        public override void Draw(PaintTools paintTools, Segment side)
        {
            base.Draw(paintTools, side);

            PointD location = _GetLocation(side);

            paintTools.Graphics.DrawString(_length.ToString(), _font,
                                    new SolidBrush(_penColor), (float)(location.X - 2*_distanceFromMiddle - 3),
                                                               (float)(location.Y - _distanceFromMiddle));
        }

        public override string ToString()
        {
            return (_isFixed ? "LengthRelation"
                             : "EmptyRelation");
        }

        public override bool IsEmpty()
        {
            return false;
        }

        public override Relation Copy()
        {
            return new LengthRelation(_length);
        }
    }
}
