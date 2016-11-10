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

namespace PolygonPainter.Shapes.PolygonClasses
{
    public class VertexManager
    {
        protected List<Vertex> _vertices;
        protected List<Relation> _relations;

        public int NumberOfVertices
        {
            get
            {
                return _vertices.Count;
            }
        }

        public List<Vertex> Vertices
        {
            get
            {
                return _vertices;
            }
        }

        public PointD[] VertexLocations
        {
            get
            {
                return _vertices.Select(v => v.Location).ToArray();
            }
        }

        public Vertex GetVertex(int index)
        {
            return _vertices[index];
        }
        
        public VertexManager()
        {
            _vertices = new List<Vertex>();
            _relations = new List<Relation>();
        }

        public void AddAutomaticRelations()
        {
            for (int i = 0; i < this.NumberOfVertices; ++i)
            {
                if (!_relations[i].IsFixed)
                {
                    FreeVector vSide = new FreeVector(_vertices[i].Location,
                                                     _vertices[(i + 1) % this.NumberOfVertices].Location);

                    if (vSide.IsApproxVertical())
                        _relations[i] = new VerticalRelation(false);
                    else if (vSide.IsApproxHorizontal())
                        _relations[i] = new HorizontalRelation(false);
                    else
                        _relations[i] = new EmptyRelation();
                }
            }
        }

        public void FixRelations()
        {
            for(int i = 0; i < this.NumberOfVertices; ++i)
            {
                if (!_relations[i].IsFixed && !_relations[i].IsEmpty())
                {
                    Relation newRelation = _relations[i].Copy();
                    newRelation.IsFixed = true;

                    _relations[i] = new EmptyRelation();

                    this.SetRelation(i, newRelation);
                }
            }
        }

        public bool UnsetRelation(int sideIndex)
        {
            bool res = !(_relations[sideIndex].IsEmpty());
            _relations[sideIndex] = new EmptyRelation();

            return true;
        }

        public void DrawVertices(PaintTools paintTools)
        {
            foreach (Vertex vertex in _vertices)
                vertex.Draw(paintTools);
        }

        public void DrawRelations(PaintTools paintTools)
        {
            for (int i = 0; i < this.NumberOfVertices; ++i)
                _relations[i].Draw(paintTools, new Segment(_vertices[i].Location,
                                               _vertices[(i + 1) % this.NumberOfVertices].Location));
        }

        public void SetColorOfVertex(int vertexIndex, Color newColor)
        {
            PointD vertexLocation = _vertices[vertexIndex].Location;
            _vertices[vertexIndex] = new Vertex(vertexLocation, newColor);
        }

        public void MoveAll(FreeVector vector)
        {
            _MoveVertices(Enumerable.Range(0, this.NumberOfVertices).ToArray(), vector);
        }

        public void AppendVertex(Vertex newVertex)
        {
            _vertices.Add(newVertex);
            _relations.Add(new EmptyRelation());
        }

        public void AddVertex(int sideIndex, Vertex newVertex)
        {
            int nextIndex = (sideIndex + 1) % this.NumberOfVertices;
            _relations[sideIndex] = new EmptyRelation();
            _InsertVertex(nextIndex, newVertex);
        }

        public void DeleteVertex(int vertexIndex)
        {
            int indexBefore = ((vertexIndex - 1) + this.NumberOfVertices) % this.NumberOfVertices;

            _relations[indexBefore] = new EmptyRelation();

            _vertices.RemoveAt(vertexIndex);
            _relations.RemoveAt(vertexIndex);
        }

        public bool TryToMoveVertex(int vertexIndex, FreeVector vector)
        {
            var L = _GetMoveVectors(vertexIndex, vector, 1);
            if (L.Length == 0)
                return false;

            var P = _GetMoveVectors(vertexIndex, vector, -1);
            if (P.Length == 0)
                return false;

            P[vertexIndex] = new FreeVector(new PointD(0, 0));

            FreeVector[] vectors = new FreeVector[this.NumberOfVertices];
            for(int i = 0; i < this.NumberOfVertices; ++i)
            {
                if (!L[i].IsEmpty && !P[i].IsEmpty)
                    return false;

                vectors[i] = L[i] + P[i];
            }

            _MoveVertices(vectors);

            return true;
        }

        public bool SetRelation(int sideIndex, Relation relation)
        {
            if (relation.IsEmpty())
                return false;

            if ((relation is HorizontalRelation || relation is VerticalRelation)
                && _TwoNeighbourSidesWouldHaveTheSameOrientation(sideIndex, relation.ToString()))
                throw new OperationImpossibleException("two neighbour relations can't be vertical/horizontal");

            if (!(relation is LengthRelation) && !(_relations[sideIndex] is EmptyRelation))
                throw new OperationImpossibleException("The relation has already been set");

            int firstVertexIndex = sideIndex;
            PointD a = _vertices[firstVertexIndex].Location;

            int secondVertexIndex = (sideIndex + 1) % this.NumberOfVertices;
            PointD b = _vertices[secondVertexIndex].Location;

            switch (relation.ToString())
            {
                case "VerticalRelation":
                    if (!this._TryToMoveForSettingRelation(firstVertexIndex, new FreeVector(a, new PointD(b.X, a.Y)), -1)
                     && !this._TryToMoveForSettingRelation(secondVertexIndex, new FreeVector(b, new PointD(a.X, b.Y)), 1))
                        throw new OperationImpossibleException("impossible to move vertex in order to get a vertical side");

                    break;
                case "HorizontalRelation":
                    if (!_TryToMoveForSettingRelation(firstVertexIndex, new FreeVector(a, new PointD(a.X, b.Y)), -1)
                     && !_TryToMoveForSettingRelation(secondVertexIndex, new FreeVector(b, new PointD(b.X, a.Y)), 1))
                        throw new OperationImpossibleException("impossible to move vertex in order to get o horizontal side");

                    break;
                case "LengthRelation":
                    int r = (relation as LengthRelation).Length;
                    FreeVector v = new FreeVector(a, b);
                    v.Length = r;

                    PointD wantedPointB = a + v;
                    PointD wantedPointA = b + (-v);
                    if (!_TryToMoveForSettingRelation(secondVertexIndex, new FreeVector(b, wantedPointB), 1)
                     && !_TryToMoveForSettingRelation(firstVertexIndex, new FreeVector(a, wantedPointA), -1)
                     && !_CheckPointsOnCircle(firstVertexIndex, b, r, -1)
                     && !_CheckPointsOnCircle(secondVertexIndex, a, r, 1))
                        throw new OperationImpossibleException("impossible to change the length of the side");

                    break;
            }

            _relations[sideIndex] = relation;

            return true;
        }

        private bool _CheckPointsOnCircle(int vertexIndex, PointD middle, int radius, int inc)
        {
            PointD a = _vertices[vertexIndex].Location;

            foreach (PointD potentialPoint in this.GetNextPointOfCircle(middle, radius))
                if (this._TryToMoveForSettingRelation(vertexIndex, new FreeVector(a, potentialPoint), inc))
                {
                    return true;
                }

            return false;
        }

        // algorithm for "drawing" a circle from Wikipedia
        private IEnumerable<Point> GetNextPointOfCircle(PointD a, int radius)
        {
            int x0 = (int)a.X, y0 = (int)a.Y;
            int x = radius;
            int y = 0;
            int err = 0;

            while (x >= y)
            {
                yield return new Point(x0 + x, y0 + y);
                yield return new Point(x0 + y, y0 + x);
                yield return new Point(x0 - y, y0 + x);
                yield return new Point(x0 - x, y0 - y);
                yield return new Point(x0 - y, y0 - x);
                yield return new Point(x0 + y, y0 - x);
                yield return new Point(x0 + x, y0 - y);

                y += 1;
                err += 1 + 2 * y;
                if (2 * (err - x) + 1 > 0)
                {
                    x -= 1;
                    err += 1 - 2 * x;
                }
            }
        }

        private bool _TryToMoveForSettingRelation(int startIndex, FreeVector vector, int inc)
        {
            var L = _GetMoveVectors(startIndex, vector, inc);

            if (L.Length > 0)
            {
                _MoveVertices(L);
                return true;
            }
            else
                return false;
        }


        private FreeVector[] _GetMoveVectors(int startIndex, FreeVector startVector, int inc)
        {
            FreeVector[] res = new FreeVector[this.NumberOfVertices];
            for (int i = 0; i < this.NumberOfVertices; ++i)
                res[i] = new FreeVector(new PointD(0, 0));

            Func<int, int> next = (x => (x + 1) % this.NumberOfVertices);
            Func<int, int> prev = (x => (x - 1 + this.NumberOfVertices) % this.NumberOfVertices);
            Func<int, bool> IsEndReached = (x => x == startIndex);
            int sideIndex = startIndex;
            int vertexIndex = next(startIndex) ;
            res[startIndex] = startVector;

            if (inc == -1)
            {
                next = (x => (x - 1 + this.NumberOfVertices) % this.NumberOfVertices);
                prev = (x => (x + 1) % this.NumberOfVertices);
                IsEndReached = (x => next(x) == startIndex);
                sideIndex = next(startIndex);
                vertexIndex = next(startIndex);
            }

            while (!IsEndReached(vertexIndex))
            {
                bool canFinish = false;

                switch (_relations[sideIndex].ToString())
                {
                    case "EmptyRelation":
                        canFinish = _ProcessEmptyRelation(sideIndex, vertexIndex, res, next, prev);
                        break;
                    case "HorizontalRelation":
                        canFinish = _ProcessHorizontalRelation(sideIndex, vertexIndex, res, next, prev);
                        break;
                    case "VerticalRelation":
                        canFinish = _ProcessVerticalRelation(sideIndex, vertexIndex, res, next, prev);
                        break;
                    case "LengthRelation":
                        canFinish = _ProcessLengthRelation(sideIndex, vertexIndex, res, next, prev);
                        break;
                }
                
                if (canFinish)
                    break;
                
                vertexIndex = next(vertexIndex);
                sideIndex = next(sideIndex);
            }

            if (IsEndReached(vertexIndex))
                return new FreeVector[0];

            return res;
        }

        private bool _ProcessEmptyRelation(int sideIndex, int vertexIndex, FreeVector[] vector,
                                           Func<int,int> next, Func<int, int> prev)
        {
            return true;
        }

        private bool _ProcessHorizontalRelation(int sideIndex, int vertexIndex, FreeVector[] vector,
                                                Func<int, int> next, Func<int, int> prev)
        {
            FreeVector sideVector = new FreeVector(_vertices[vertexIndex].Location,
                                                   _vertices[prev(vertexIndex)].Location + vector[prev(vertexIndex)]);

            if (sideVector.IsHorizontal)
                return true;

            switch (_relations[next(sideIndex)].ToString())
            {
                case "EmptyRelation":
                    vector[vertexIndex] = vector[prev(vertexIndex)];
                    return true;
                case "VerticalRelation":
                    double xw = _vertices[vertexIndex].Location.X;
                    double yw = (_vertices[prev(vertexIndex)].Location + vector[prev(vertexIndex)]).Y;

                    FreeVector v = new FreeVector(_vertices[vertexIndex].Location,
                                                  new PointD(xw, yw));

                    vector[vertexIndex] = v;
                    return true;
                case "LengthRelation":
                    Circle c = new Circle(_vertices[next(vertexIndex)].Location,
                                          (_relations[next(sideIndex)] as LengthRelation).Length);

                    double y = (_vertices[prev(vertexIndex)].Location + vector[prev(vertexIndex)]).Y;
                    PointD[] p = c.GetIntersectionPointsWithHorizontalLine(y);

                    PointD? chosenPoint = _GetTheNearestPoint(p, _vertices[vertexIndex].Location);
                    if (!chosenPoint.HasValue)
                    {
                        vector[vertexIndex] = vector[prev(vertexIndex)];
                        return false;
                    }

                    vector[vertexIndex] = new FreeVector(_vertices[vertexIndex].Location, chosenPoint.Value);
                    return true;
            }

            return true; //?
        }

        private PointD? _GetTheNearestPoint(PointD[] p, PointD from)
        {
            PointD chosenPoint = new PointD();
            switch (p.Length)
            {
                case 0:
                    return null;
                case 1:
                    chosenPoint = p[0];
                    break;
                case 2:
                    var dist = p.Select(x => Shape.DistanceSquared(x, from)).ToArray();
                    chosenPoint = (dist[0] > dist[1] ? p[1] : p[0]);
                    break;
            }

            return chosenPoint;
        }

        private bool _ProcessVerticalRelation(int sideIndex, int vertexIndex, FreeVector[] vector,
                                                Func<int, int> next, Func<int, int> prev)
        {
            FreeVector sideVector = new FreeVector(_vertices[prev(vertexIndex)].Location + vector[prev(vertexIndex)],
                                                   _vertices[vertexIndex].Location);

            if (sideVector.IsVertical)
                return true;

            switch (_relations[next(sideIndex)].ToString())
            {
                case "EmptyRelation":
                    vector[vertexIndex] = vector[prev(vertexIndex)];
                    return true;
                case "HorizontalRelation":
                    double xw = (_vertices[prev(vertexIndex)].Location + vector[prev(vertexIndex)]).X;
                    double yw = _vertices[vertexIndex].Location.Y;

                    vector[vertexIndex] = new FreeVector(_vertices[vertexIndex].Location,
                                                         new PointD(xw, yw));

                    return true;
                case "LengthRelation":
                    Circle c = new Circle(_vertices[next(vertexIndex)].Location,
                                          (_relations[next(sideIndex)] as LengthRelation).Length);

                    double x = (_vertices[prev(vertexIndex)].Location + vector[prev(vertexIndex)]).X;
                    PointD[] p = c.GetIntersectionPointsWithVerticalLine(x);
                    PointD? chosenPoint = _GetTheNearestPoint(p, _vertices[vertexIndex].Location);
                    
                    if (!chosenPoint.HasValue)
                    {
                        vector[vertexIndex] = vector[prev(vertexIndex)];
                        return false;
                    }

                    vector[vertexIndex] = new FreeVector(_vertices[vertexIndex].Location, chosenPoint.Value);
                    return true;
            }

            return true;
        }

        private bool _ProcessLengthRelation(int sideIndex, int vertexIndex, FreeVector[] vector,
                                           Func<int, int> next, Func<int, int> prev)
        {
            FreeVector v = new FreeVector(_vertices[prev(vertexIndex)].Location + vector[prev(vertexIndex)],
                                          _vertices[vertexIndex].Location);

            double L1 = (_relations[sideIndex] as LengthRelation).Length;
            if (v.Length == L1)
                return true;

            switch (_relations[next(sideIndex)].ToString())
            {
                case "EmptyRelation":
                    {
                        v = new FreeVector(_vertices[prev(vertexIndex)].Location,
                                           _vertices[vertexIndex].Location);

                        PointD newPoint = (_vertices[prev(vertexIndex)].Location + vector[prev(vertexIndex)]) +(v);

                        vector[vertexIndex] = new FreeVector(_vertices[vertexIndex].Location, newPoint);

                        return true;
                    }
                case "HorizontalRelation":
                    {
                        double y = _vertices[vertexIndex].Location.Y;
                        Circle c = new Circle(_vertices[prev(vertexIndex)].Location + vector[prev(vertexIndex)], L1);
                        PointD[] p = c.GetIntersectionPointsWithHorizontalLine(y);
                        PointD? chosenPoint = _GetTheNearestPoint(p, _vertices[vertexIndex].Location);
                        if (!chosenPoint.HasValue)
                        {
                            vector[vertexIndex] = vector[prev(vertexIndex)];
                            return false;
                        }

                        vector[vertexIndex] = new FreeVector(_vertices[vertexIndex].Location, chosenPoint.Value);
                        return true;
                    }
                case "VerticalRelation":
                    {
                        Circle c = new Circle(_vertices[prev(vertexIndex)].Location + vector[prev(vertexIndex)], L1);

                        double x = _vertices[vertexIndex].Location.X;
                        PointD[] p = c.GetIntersectionPointsWithVerticalLine(x);
                        PointD? chosenPoint = _GetTheNearestPoint(p, _vertices[vertexIndex].Location);

                        if (!chosenPoint.HasValue)
                        {
                            vector[vertexIndex] = vector[prev(vertexIndex)];
                            return false;
                        }

                     
                        vector[vertexIndex] = new FreeVector(_vertices[vertexIndex].Location, chosenPoint.Value);
                        return true;
                    }
                case "LengthRelation":
                    {
                        Circle c1 = new Circle(_vertices[prev(vertexIndex)].Location + vector[prev(vertexIndex)], L1);

                        double L2 = (_relations[next(sideIndex)] as LengthRelation).Length;
                        Circle c2 = new Circle(_vertices[next(vertexIndex)].Location, L2);

                        PointD[] p = c1.GetIntersectionPointsWithCircle(c2);
                        PointD? chosenPoint = _GetTheNearestPoint(p, _vertices[vertexIndex].Location);

                        if (!chosenPoint.HasValue)
                        {
                            vector[vertexIndex] = vector[prev(vertexIndex)];
                            return false;
                        }

                        vector[vertexIndex] = new FreeVector(_vertices[vertexIndex].Location, chosenPoint.Value);
                        

                        return true;
                    }
            }

            return true;

        }

        private bool _TwoNeighbourSidesWouldHaveTheSameOrientation(int sideIndex, String relationName)
        {
            int nextSideIndex = (sideIndex + 1) % this.NumberOfVertices;
            int previousSideIndex = ((sideIndex - 1) + this.NumberOfVertices) % this.NumberOfVertices;

            if (_relations[nextSideIndex].ToString() == relationName
                 || _relations[previousSideIndex].ToString() == relationName)
                return true;

            return false;
        }
        
        protected void _MoveVertices(FreeVector[] vectors)
        {
            for(int i = 0; i < vectors.Length; ++i)
            {
                Vertex v = _vertices[i];
                v.Location = v.Location + vectors[i];

                _vertices[i] = v;
            }
        }

        protected void _MoveVertices(int[] vertexIndices, FreeVector vector)
        {
            foreach (int vertexIndex in vertexIndices)
            {
                Vertex v = _vertices[vertexIndex];
                v.Location = v.Location + vector;

                _vertices[vertexIndex] = v;
            }
        }
        
        protected void _InsertVertex(int where, Vertex vertex)
        {
            _vertices.Insert(where, vertex);
            _relations.Insert(where, new EmptyRelation());
        }
    }
}
