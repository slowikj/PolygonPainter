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
        class PolygonIntersectionComputer
        {
            private Polygon _clip;
            private Polygon _subject;
            private List<IntersectionPoint>[] _clipPoints;
            private List<IntersectionPoint>[] _subjectPoints;
            
            class IntersectionPoint
            {
                private PointD _location;
                private bool _isEntry;
                private Point _otherLinePostion;

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

                public bool IsEntry
                {
                    get
                    {
                        return _isEntry;
                    }
                    set
                    {
                        _isEntry = value;
                    }
                }

                public Point OtherLinePosition
                {
                    get
                    {
                        return _otherLinePostion;
                    }

                    set
                    {
                        _otherLinePostion = value;
                    }
                }

                public IntersectionPoint(PointD location)
                {
                    _location = location;
                }
            }

            public PolygonIntersectionComputer(Polygon clip, Polygon subject)
            {
                _clip = clip;
                _subject = subject;
            }

            public Polygon[] GetIntersectionPolygons()
            {
                // trzeba najpierw sprawdzic skrajne przypadki, czy jedna figura nie jest wewnatrz drugiej (wtedy zwracamy jedna z figur)
                // lub czy nie sa calkowicie rozlaczne (wtedy zwracamy new Polygon[0])

                _PrepareIntersectionPointsLists();
                //wybrac punktu wejsciowe
                // iterowac rozpoczynajac od kazdego nieodwiedzonego punktu wejsciowego
                // az dojdziemy do poczatkowego punktu
                // wtedy podajemy nowy polygon na liste i znowu iterujemy rozpoczynajac od kolejnego
                // punktu wejsciowego na liscie

                return null;
            }

            private void _PrepareIntersectionPointsLists()
            {
                _clipPoints = _GetEmptyIntersectionPointsList(_clip.NumberOfVertices);
                _subjectPoints = _GetEmptyIntersectionPointsList(_subject.NumberOfVertices);

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
                        PointD? p = _clip._GetSide(i).GetIntersectionWith(_subject._GetSide(j));

                        if (p.HasValue)
                        {
                            _clipPoints[i].Add(new IntersectionPoint(p.Value));
                            _subjectPoints[j].Add(new IntersectionPoint(p.Value));
                        }
                    }
                }
            }

            private void _SortPoints(Polygon polygon, List<IntersectionPoint>[] points)
            {
                for(int i = 0; i < points.Length; ++i)
                {
                    PointD edgeBegin = polygon._vertexManager.GetVertex(i).Location;
                    points[i].Sort((a, b) => Shape.DistanceSquared(edgeBegin, a.Location)
                                                  .CompareTo(Shape.DistanceSquared(edgeBegin, b.Location)));                                                           
                }
            }

            private List<IntersectionPoint>[] _GetEmptyIntersectionPointsList(int size)
            {
                return (new List<IntersectionPoint>[size]).Select(x => new List<IntersectionPoint>())
                                                          .ToArray();
            }

            private void _SetOtherLocationPointers(List<IntersectionPoint>[] points, List<IntersectionPoint>[] otherPoints)
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
                        otherPoints[i][j].OtherLinePosition = listPositions[otherPoints[i][j].Location];
                    }
                }
            }
        }
    }
}
