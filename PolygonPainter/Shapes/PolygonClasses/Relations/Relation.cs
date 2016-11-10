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
    public abstract class Relation
    {
        protected readonly int _radius;
        protected readonly Color _penColor;
        protected readonly Color _fillingColor;
        protected readonly int _distanceFromMiddle;

        protected bool _isFixed;

        public bool IsFixed
        {
            get
            {
                return _isFixed;
            }
            set
            {
                _isFixed = value;
            }
        }
        
        public Relation(bool isFixed = true)
        {
            _radius = 20;
            _penColor = Color.Black;
            _fillingColor = Color.LightPink;
            _distanceFromMiddle = 5;

            _isFixed = isFixed;
        }
        
        public virtual void Draw(PaintTools paintTools, Segment side)
        {
            Vertex circle = new Vertex(_GetLocation(side), _fillingColor, _radius);
            circle.Draw(paintTools);
        }

        public abstract Relation Copy();
        
        public override string ToString()
        {
            return "AbstractRelation";
        }

        protected PointD _GetLocation(Segment side)
        {
            return side.Middle;
        }

        public abstract bool IsEmpty();
    }
}

