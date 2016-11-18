using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonPainter
{
    public enum Direction { Clockwise, Anticlockwise };

    public enum IntersectionType { Disjoint, Intersects, Touches, Covers, Parallel };

    public enum PolygonPositionRelation { IsInside, Contains, Disjoint, Intersects };

    public enum NormalVectorsType { FromTexture, Pyramid, None };
}
