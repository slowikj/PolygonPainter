using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonPainter.Shapes.PolygonClasses
{
    public partial class Polygon : Shape
    {
        public class PolygonInclusionChecker
        {
            private Polygon _polygon;

            public PolygonInclusionChecker(Polygon polygon)
            {
                _polygon = polygon;
            }

            public bool Contains(Polygon otherPolygon)
            {
                bool everyVertexInside = otherPolygon._vertexManager.Vertices
                                                     .Select(v => v.Location)
                                                     .Select(p => this.Contains(p))
                                                     .All(res => res == true);

                return everyVertexInside
                    && !this.IntersectsWith(otherPolygon);
            }

            public bool Contains(PointD p)
            {
                if(this.HasOnSide(p))
                {
                    return true;
                }

                double maxX = _polygon._vertexManager.Vertices.Max(v => v.Location.X);
                Segment horizontalLine = new Segment(p, new PointD(maxX + 5, p.Y));

                int cnt = 0, inc = 1;
                for (int i = 0; i < _polygon.NumberOfVertices; i += inc)
                {
                    SegmentIntersectionInfo intersectionInfo = _polygon._GetSide(i)
                                                                       .GetIntersectionWith(horizontalLine);
                    
                    switch (intersectionInfo.Type)
                    {
                        case IntersectionType.Touches:
                            int nextInd = (i + 1) % _polygon.NumberOfVertices;
                            int prevInd = (i - 1 + _polygon.NumberOfVertices) % _polygon.NumberOfVertices;

                            if (_polygon._GetSide(nextInd).GetIntersectionWith(horizontalLine).Type
                                == IntersectionType.Covers)
                            {
                                inc = 3;
                                cnt += _GetIncrementationForSideCoveringCase(i + 1, horizontalLine);
                            }
                            else if (_polygon._GetSide(prevInd).GetIntersectionWith(horizontalLine).Type
                                == IntersectionType.Covers)
                            {
                                inc = 1;
                                cnt += _GetIncrementationForSideCoveringCase(i - 1, horizontalLine);
                            }
                            else
                            {
                                PointD touchPoint = intersectionInfo.Point.Value;
                                inc = (touchPoint == _polygon._vertexManager.GetVertex(i).Location ? 1
                                                                                                   : 2);

                                cnt += _GetIncrementationForVertexTouchingCase(i, touchPoint, horizontalLine);
                            }

                            break;

                        case IntersectionType.Covers:
                            inc = 2;
                            cnt += _GetIncrementationForSideCoveringCase(i, horizontalLine);

                            break;

                        case IntersectionType.Intersects:
                            cnt += 1;
                            inc = 1;

                            break;

                        default: // no intersection
                            inc = 1;
                            break;
                    }
                }

                return cnt % 2 == 0 ? false
                                    : true;
            }

            public bool HasOnSide (PointD p)
            {
                return Enumerable.Range(0, _polygon.NumberOfVertices)
                                 .Select(i => _polygon._GetSide(i)
                                                      .IsClickedBy(p))
                                 .Any(x => x == true);
            }

            public bool IsDisjointWith(Polygon otherPolygon)
            {
                return !this.Contains(otherPolygon)
                    && !this.IntersectsWith(otherPolygon)
                    && !otherPolygon.Contains(this._polygon);
            }

            public bool IntersectsWith(Polygon otherPolygon)
            {
                for (int i = 0; i < _polygon.NumberOfVertices; ++i)
                {
                    for (int j = 0; j < otherPolygon.NumberOfVertices; ++j)
                    {
                        IntersectionType type = _polygon._GetSide(i)
                                                        .GetIntersectionWith(otherPolygon._GetSide(j))
                                                        .Type;

                        if (type == IntersectionType.Intersects
                         || type == IntersectionType.Touches)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            private int _GetIncrementationForSideCoveringCase(int sideIndex, Segment horizontalLine)
            {
                PointD a = _polygon._vertexManager
                                   .GetVertex((sideIndex + 2) % _polygon.NumberOfVertices)
                                   .Location;

                PointD b = _polygon._vertexManager
                                   .GetVertex((sideIndex - 1 + _polygon.NumberOfVertices) % _polygon.NumberOfVertices)
                                   .Location;

                return horizontalLine.ArePointsOnTheSameSide(new PointD[] { a, b }) ? 0
                                                                                   : 1;
            }

            private int _GetIncrementationForVertexTouchingCase(int sideIndex, PointD touchPoint, Segment horizontalLine)
            {
                PointD a, b;
                if (touchPoint == _polygon._vertexManager.GetVertex(sideIndex).Location)
                {
                    a = _polygon._vertexManager
                                .GetVertex((sideIndex + 1) % _polygon.NumberOfVertices)
                                .Location;

                    b = _polygon._vertexManager
                                .GetVertex((sideIndex - 1 + _polygon.NumberOfVertices) % _polygon.NumberOfVertices)
                                .Location;
                }
                else
                {
                    a = _polygon._vertexManager
                                .GetVertex(sideIndex)
                                .Location;

                    b = _polygon._vertexManager
                                .GetVertex((sideIndex + 2) % _polygon.NumberOfVertices)
                                .Location;
                }

                return horizontalLine.ArePointsOnTheSameSide(new PointD[] { a, b }) ? 0
                                                                                   : 1;
            }

            
        }
    }
}
