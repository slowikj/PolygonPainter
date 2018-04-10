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
    class EmptyRelation : Relation
    {
        public EmptyRelation()
            : base(false)
        {
        }

        public override Relation Copy()
        {
            return new EmptyRelation();
        }

        public override void Draw(PaintTools g, Segment side)
        {
        }

        public override bool IsEmpty()
        {
            return true;
        }

        public override string ToString()
        {
            return "EmptyRelation";
        }
    }
}
