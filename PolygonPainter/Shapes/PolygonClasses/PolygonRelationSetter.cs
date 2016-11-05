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

using PolygonPainter.Shapes.PolygonClasses.Relations;
using PolygonPainter.Interfaces;

namespace PolygonPainter.Shapes.PolygonClasses
{
    public partial class Polygon : Shape
    {
        public class PolygonRelationSetter : IRelationSetter
        {
            private Polygon _polygon;
            private int _sideIndex;

            public PolygonRelationSetter(Polygon polygon, int sideIndex)
            {
                _polygon = polygon;
                _sideIndex = sideIndex;
            }

            public bool SetRelation(Relation relation)
            {
                return _polygon._vertexManager.SetRelation(_sideIndex, relation);
            }
            
            public bool UnsetRelation()
            {
                return _polygon._vertexManager.UnsetRelation(_sideIndex);
            }
        }
    }
}
