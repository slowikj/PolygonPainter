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
using PolygonPainter.Shapes.GeneralHandlers;
using PolygonPainter.Modes.LightManagers;


namespace PolygonPainter.Shapes.PolygonClasses
{
    public partial class Polygon : Shape
    {
        protected Color _defaultVertexColor, _defaultSideColor;
        protected List<Color> _sideColors;
        protected VertexManager _vertexManager;

        protected PolygonFiller _filler;

        protected PolygonInclusionChecker _inclusionChecker;

        public int NumberOfVertices
        {
            get
            {
                return _vertexManager.NumberOfVertices;
            }
        }

        public Polygon(PointD[] points, Color vertexColor, Color sideColor)
            : this(vertexColor, sideColor)
        {
            foreach(PointD point in points)
            {
                _vertexManager.AppendVertex(new Vertex(point, vertexColor));
                _sideColors.Add(_defaultSideColor);
            }
        }

        public Polygon(Color vertexColor, Color sideColor)
        {
            _defaultVertexColor = vertexColor;
            _defaultSideColor = sideColor;

            _vertexManager = new VertexManager();
            _sideColors = new List<Color>();

            _filler = null;

            _inclusionChecker = new PolygonInclusionChecker(this);
        }
        
        public override void DrawContours(PaintTools paintTools)
        {
            int numberOfVertices = this.NumberOfVertices;
            PointD firstPoint = _vertexManager.GetVertex(0).Location;
            PointD lastPoint = _vertexManager.GetVertex(numberOfVertices - 1).Location;

            Segment closingSegment = new Segment(lastPoint, firstPoint,
                                        _sideColors[numberOfVertices - 1]);
            closingSegment.Draw(paintTools);

            _DrawPolyline(paintTools);
            _vertexManager.DrawRelations(paintTools);
        }

        public override void DrawFilling(PaintTools paintTools)
        {
            _filler?.Fill(paintTools, _vertexManager.Vertices);
        }

        public override void SetFilling(FillingInfo fillingInfo, LightManager lightManager)
        {
            _filler = new PolygonFiller(fillingInfo, lightManager);
        }

        public override void ChangeLightManager(LightManager lightManager)
        {
            _filler.LightManager = lightManager;
        }

        public override void DeleteFilling()
        {
            _filler = null;
        }

        public override IHandler GetPartOfShapeHandler(PointD clickedPoint, List<Shape> polygons, int polygonIndex, CheckBox checkBox,
                                                       Color? markingColor)
        {
            int vertexIndex = _GetIndexOfVertexClickedBy(clickedPoint);
            if (vertexIndex != -1)
                return new PolygonVertexHandler(this, polygons, polygonIndex, vertexIndex, checkBox, markingColor);

            int sideIndex = _GetIndexOfSideClickedBy(clickedPoint);
            if (sideIndex != -1)
                return new PolygonSideHandler(this, polygons, polygonIndex, sideIndex, clickedPoint, null, markingColor);

            return new EmptyHandler();
        }

        public override IHandler GetEntireShapeHandler(PointD clickedPoint, List<Shape> polygons,
                                                       int polygonIndex, Color? markingColor)
        {
            if (IsClickedBy(clickedPoint))
                return new EntirePolygonHandler(this, polygons, polygonIndex, clickedPoint, null, markingColor);

            return new EmptyHandler();
        }

        public IVertexAdder GetVertexAdder()
        {
            return new PolygonVertexAdder(this);
        }

        public IRelationSetter GetRelationSetter(PointD clickedPoint)
        {
            int sideIndex = _GetIndexOfSideClickedBy(clickedPoint);

            if (sideIndex == -1)
                return new EmptyRelationSetter();
            else
                return new PolygonRelationSetter(this, sideIndex);
        }

        public Polygon[] GetIntersectionPolygons(Polygon other)
        {
            if(this.HasSelfIntersections() || other.HasSelfIntersections())
            {
                throw new OperationImpossibleException("at least one polygon has self intersections!");
            }

            PolygonIntersectionComputer intersectionComputer = new PolygonIntersectionComputer(this, other);

            return intersectionComputer.GetIntersectionPolygons();
        }

        public override bool IsClickedBy(PointD p)
        {
            return _GetIndexOfVertexClickedBy(p) != -1
                || _GetIndexOfSideClickedBy(p) != -1;
        }

        public override double Area()
        {
            double res = 0;
            for (int i = 0; i < this.NumberOfVertices; ++i)
            {
                int a = i;
                int b = (i + 1) % this.NumberOfVertices;

                FreeVector v = new FreeVector(_vertexManager.GetVertex(a).Location);
                FreeVector u = new FreeVector(_vertexManager.GetVertex(b).Location);

                //res += (_vertexManager.GetVertex(b).Location.X - _vertexManager.GetVertex(a).Location.X)
                //    *  (_vertexManager.GetVertex(b).Location.Y + _vertexManager.GetVertex(a).Location.Y);

                res += (u.CrossProduct(v));
            }

            return res / 2;
        }

        public bool HasSelfIntersections()
        {
            Func<int, int, bool> isNeighbour = ((i, j) => (i + 1) % this.NumberOfVertices == j
                                                         || (j + 1) % this.NumberOfVertices == i);

            for (int i = 0; i < this.NumberOfVertices; ++i)
            {
                for (int j = i + 1; j < this.NumberOfVertices; ++j)
                {
                    if (!isNeighbour(i, j) && _GetSide(i).GetIntersectionWith(_GetSide(j))
                                                         .Point
                                                         .HasValue)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        public bool Contains(PointD p)
        {
            return _inclusionChecker.Contains(p);
        }

        public bool Contains(Polygon other)
        {
            return _inclusionChecker.Contains(other);
        }

        public bool IsDisjointWith(Polygon other)
        {
            return _inclusionChecker.IsDisjointWith(other);
        }

        public bool HasOnSide(PointD p)
        {
            return _inclusionChecker.HasOnSide(p);
        }

        public PolygonPositionRelation GetRelationWith (Polygon other)
        {
            if(this.Contains(other))
            {
                return PolygonPositionRelation.Contains;
            }
            else if(other.Contains(this))
            {
                return PolygonPositionRelation.IsInside;
            }
            else if (this.IsDisjointWith(other))
            {
                return PolygonPositionRelation.Disjoint;
            }
            else
            {
                return PolygonPositionRelation.Intersects;
            }
        }

        public bool HasOnSideAnyPointFrom(Polygon other)
        {
            return other._vertexManager.Vertices
                        .Select(v => this.HasOnSide(v.Location))
                        .Any(res => res == true);
        }

        protected void _DrawPolyline(PaintTools paintTools)
        {
            for (int i = 0; i < this.NumberOfVertices - 1; ++i)
            {
                Segment side = new Segment(_vertexManager.GetVertex(i).Location,
                                     _vertexManager.GetVertex(i + 1).Location,
                                     _sideColors[i]);
                side.Draw(paintTools);
            }

            _vertexManager.DrawVertices(paintTools);
        }

        protected bool _IsSideClickedBy(PointD p)
        {
            return _GetIndexOfSideClickedBy(p) != -1;
        }

        protected int _GetIndexOfVertexClickedBy(PointD p)
        {
            for (int i = 0; i < this.NumberOfVertices; ++i)
                if (_vertexManager.GetVertex(i).IsClickedBy(p))
                    return i;

            return -1;
        }

        protected int _GetIndexOfSideClickedBy(PointD p)
        {
            for (int i = 0; i < this.NumberOfVertices; ++i)
            {
                if (_GetSide(i).IsClickedBy(p))
                    return i;
            }

            return -1;
        }

        protected bool _IsFirstVertexClickedBy(PointD p)
        {
            return _GetIndexOfVertexClickedBy(p) == 0;
        }

        protected Segment _GetSide(int sideIndex)
        {
            int numberOfVertices = NumberOfVertices;

            return new Segment(_vertexManager.GetVertex(sideIndex).Location,
                            _vertexManager.GetVertex((sideIndex + 1) % numberOfVertices).Location,
                            _sideColors[sideIndex]);
        }

    }
}
