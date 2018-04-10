using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolygonPainter.Shapes.PolygonClasses.Relations;

using PolygonPainter.Interfaces;

namespace PolygonPainter.Shapes.PolygonClasses.Relations
{
    public class EmptyRelationSetter : IRelationSetter
    {
        public bool SetRelation(Relation relation)
        {
            return false;
        }

        public bool UnsetRelation()
        {
            return false;
        }
    }
}
