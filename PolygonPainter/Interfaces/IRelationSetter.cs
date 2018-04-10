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

namespace PolygonPainter.Interfaces
{
    public interface IRelationSetter
    {
        bool SetRelation(Relation relation);
        bool UnsetRelation();
    }
}
