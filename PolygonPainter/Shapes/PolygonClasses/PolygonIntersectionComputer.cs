using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonPainter.Shapes.PolygonClasses
{
    public partial class Polygon : Shape
    {
        public class PolygonIntersectionComputer
        {
            private Polygon _clip;
            private Polygon _subject;
            private List<PolygonIntersectionPoint>[] _clipPoints;
            private List<PolygonIntersectionPoint>[] _subjectPoints;

            private Color _vertexColor, _sideColor;
            
            class PolygonIntersectionPoint
            {
                private PointD _location;
                private Point _otherSegmentPosition;
               
                public PointD Location
                {
                    get
                    {
                        return _location;
                    }
                    set
                    {
                        _location = value;
                    }
                }
                
                public Point OtherSegmentPosition
                {
                    get
                    {
                        return _otherSegmentPosition;
                    }

                    set
                    {
                        _otherSegmentPosition = value;
                    }
                }

                public PolygonIntersectionPoint(PointD location)
                {
                    _location = location;
                }
            }

            public PolygonIntersectionComputer(Polygon clip, Polygon subject)
            {
                _clip = clip;
                _subject = subject;

                _vertexColor = Color.Gold;
                _sideColor = Color.CornflowerBlue;
            }

            public Polygon[] GetIntersectionPolygons()
            {
                switch(_clip.GetRelationWith(_subject))
                {
                    case PolygonPositionRelation.Contains:
                        return new Polygon[] { new Polygon(_subject._vertexManager.VertexLocations, _vertexColor, _sideColor)};
                    case PolygonPositionRelation.IsInside:
                        return new Polygon[] { new Polygon(_clip._vertexManager.VertexLocations, _vertexColor, _sideColor) };
                    case PolygonPositionRelation.Disjoint:
                        return new Polygon[0];
                }

                _MoveClipPolygonIfPointTouchesSubject();

                _PreparePolygonIntersectionPointsLists();
                List<PolygonIntersectionPoint> entryPoints = _GetEntryPoints();
                
                HashSet<PointD> visited = new HashSet<PointD>();
                List<Polygon> res = new List<Polygon>();
                foreach(var entryPoint in entryPoints)
                {
                    if(!visited.Contains(entryPoint.Location))
                    {
                        res.Add(_GetIntersectionPolygon(entryPoint, visited));
                    }
                }

                return res.ToArray();
            }
            
            private void _PreparePolygonIntersectionPointsLists()
            {
                _clipPoints = _GetEmptyPolygonIntersectionPointsList(_clip.NumberOfVertices);
                _subjectPoints = _GetEmptyPolygonIntersectionPointsList(_subject.NumberOfVertices);

                _ComputeIntersections();
                _SortPoints(_clip, _clipPoints);
                _SortPoints(_subject, _subjectPoints);

                _SetOtherLocationPointers(_clipPoints, _subjectPoints);
                _SetOtherLocationPointers(_subjectPoints, _clipPoints);
            }

            private void _ComputeIntersections()
            {
                for (int i = 0; i < _clip.NumberOfVertices; ++i)
                {
                    for (int j = 0; j < _subject.NumberOfVertices; ++j)
                    {
                        SegmentIntersectionInfo SegmentIntersectionInfo = _clip._GetSide(i)
                                                                               .GetIntersectionWith(_subject._GetSide(j));

                        if (SegmentIntersectionInfo.Point.HasValue)
                        {
                            PointD p = SegmentIntersectionInfo.Point.Value;
                            _clipPoints[i].Add(new PolygonIntersectionPoint(p));
                            _subjectPoints[j].Add(new PolygonIntersectionPoint(p));
                        }
                    }
                }
            }

            private void _SortPoints(Polygon polygon, List<PolygonIntersectionPoint>[] points)
            {
                for(int i = 0; i < points.Length; ++i)
                {
                    PointD edgeBegin = polygon._vertexManager.GetVertex(i).Location;
                    points[i].Sort((a, b) => Shape.DistanceSquared(edgeBegin, a.Location)
                                                  .CompareTo(Shape.DistanceSquared(edgeBegin, b.Location)));                                                           
                }
            }

            private List<PolygonIntersectionPoint>[] _GetEmptyPolygonIntersectionPointsList(int size)
            {
                return (new List<PolygonIntersectionPoint>[size]).Select(x => new List<PolygonIntersectionPoint>())
                                                                 .ToArray();
            }

            private void _SetOtherLocationPointers(List<PolygonIntersectionPoint>[] points, 
                                                   List<PolygonIntersectionPoint>[] otherPoints)
            {
                Dictionary<PointD, Point> listPositions = new Dictionary<PointD, Point>();

                for(int i = 0; i < points.Length; ++i)
                {
                    for(int j = 0; j < points[i].Count; ++j)
                    {
                        listPositions[points[i][j].Location] = new Point(i, j);
                    }
                }

                for(int i = 0; i < otherPoints.Length; ++i)
                {
                    for(int j = 0; j < otherPoints[i].Count; ++j)
                    {
                        otherPoints[i][j].OtherSegmentPosition = listPositions[otherPoints[i][j].Location];
                    }
                }
            }

            private void _MoveClipPolygonIfPointTouchesSubject()
            {
                FreeVector moveVector = new FreeVector(new Point(6, 0));

                while(_clip.HasOnSideAnyPointFrom(_subject)
                  ||  _subject.HasOnSideAnyPointFrom(_clip))
                {
                    _clip._vertexManager.MoveAll(moveVector);
                }
            }

            private List<PolygonIntersectionPoint> _GetEntryPoints()
            {
                List<PolygonIntersectionPoint> res = new List<PolygonIntersectionPoint>();

                PointD firstClipPoint = _clip._vertexManager.GetVertex(0).Location;
                int moduloRes = (_subject.Contains(firstClipPoint) ? 0
                                                                   : 1);
                
                int cnt = 0;
                foreach (var intersectionList in _clipPoints)
                {
                    foreach(var intersectionPoint in intersectionList)
                    {
                        if(cnt % 2 == moduloRes)
                        {
                            res.Add(intersectionPoint);
                        }

                        ++cnt;
                    }
                }

                return res;
            }

            Polygon _GetIntersectionPolygon(PolygonIntersectionPoint start, HashSet<PointD> visited)
            {
                int i = start.OtherSegmentPosition.X;
                int j = start.OtherSegmentPosition.Y;

                Polygon currentPolygon = _subject;
                List<PolygonIntersectionPoint>[] currentPoints = _subjectPoints;

                List<PointD> vertices = new List<PointD>();

                PointD point = currentPoints[i][j].Location;

                do
                {
                    vertices.Add(point);
                    visited.Add(point);
                    
                    ++j;
                    if (j >= currentPoints[i].Count) // we move to a vertex
                    {
                        i = (i + 1) % currentPoints.Length;
                        j = -1;

                        point = currentPolygon._vertexManager.GetVertex(i).Location;
                    }
                    else // we move to the next intersection point
                    {
                        int newI = currentPoints[i][j].OtherSegmentPosition.X;
                        int newJ = currentPoints[i][j].OtherSegmentPosition.Y;

                        i = newI;
                        j = newJ;

                        currentPolygon = (currentPolygon == _subject ? _clip
                                                                     : _subject);

                        currentPoints = (currentPoints == _subjectPoints ? _clipPoints
                                                                         : _subjectPoints);

                        point = currentPoints[i][j].Location;
                    }
                } while (point != start.Location);

                return new Polygon(vertices.ToArray(), _vertexColor, _sideColor);
            }
        }
    }
}
