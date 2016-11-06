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


namespace PolygonPainter.Shapes.PolygonClasses
{
    public partial class Polygon : Shape
    {
        protected Color _defaultVertexColor, _defaultSideColor;
        protected List<Color> _sideColors;
        protected VertexManager _vertexManager;

        protected PolygonFiller _filler;

        public int NumberOfVertices
        {
            get
            {
                return _vertexManager.NumberOfVertices;
            }
        }

        public PolygonFiller Filler
        {
            set
            {
                _filler = value;
            }
        }
        
        public Polygon (Color vertexColor, Color lineColor)
        {
            _defaultVertexColor = vertexColor;
            _defaultSideColor = lineColor;

            _vertexManager = new VertexManager();
            _sideColors = new List<Color>();

            _filler = null;
        }
        
        public override void DrawContours(PaintTools paintTools)
        {
            int numberOfVertices = this.NumberOfVertices;
            PointF firstPoint = _vertexManager.GetVertex(0).Location;
            PointF lastPoint = _vertexManager.GetVertex(numberOfVertices - 1).Location;

            Line closingLine = new Line(lastPoint, firstPoint,
                                        _sideColors[numberOfVertices - 1]);
            closingLine.Draw(paintTools);

            _DrawPolyline(paintTools);
            _vertexManager.DrawRelations(paintTools);
        }

        public override void DrawFilling(PaintTools paintTools)
        {
            _filler?.Draw(paintTools, _vertexManager.Vertices);
        }

        public override IHandler GetPartOfShapeHandler(PointF clickedPoint, List<Shape> polygons, int polygonIndex, CheckBox checkBox)
        {
            int vertexIndex = _GetIndexOfVertexClickedBy(clickedPoint);
            if (vertexIndex != -1)
                return new PolygonVertexHandler(this, polygons, polygonIndex, vertexIndex, checkBox);

            int sideIndex = _GetIndexOfSideClickedBy(clickedPoint);
            if (sideIndex != -1)
                return new PolygonSideHandler(this, polygons, polygonIndex, sideIndex, clickedPoint);

            return new EmptyHandler();
        }

        public override IHandler GetEntireShapeHandler(PointF clickedPoint, List<Shape> polygons, int polygonIndex)
        {
            if (IsClickedBy(clickedPoint))
                return new EntirePolygonHandler(this, polygons, polygonIndex, clickedPoint);

            return new EmptyHandler();
        }
        
        public IVertexAdder GetVertexAdder()
        {
            return new PolygonVertexAdder(this);
        }

        public IRelationSetter GetRelationSetter(PointF clickedPoint)
        {
            int sideIndex = _GetIndexOfSideClickedBy(clickedPoint);

            if (sideIndex == -1)
                return new EmptyRelationSetter();
            else
                return new PolygonRelationSetter(this, sideIndex);

            //return sideIndex == -1 ? new EmptyRelationSetter()
            //                       : new PolygonRelationSetter(this, sideIndex);
        }

        public override bool IsClickedBy(PointF p)
        {
            return _GetIndexOfVertexClickedBy(p) != -1
                || _GetIndexOfSideClickedBy(p) != -1;
        }
        
        protected void _DrawPolyline(PaintTools paintTools)
        {
            for (int i = 0; i < this.NumberOfVertices - 1; ++i)
            {
                Line side = new Line(_vertexManager.GetVertex(i).Location,
                                     _vertexManager.GetVertex(i + 1).Location,
                                     _sideColors[i]);
                side.Draw(paintTools);
            }

            _vertexManager.DrawVertices(paintTools);
        }

        protected bool _IsSideClickedBy(PointF p)
        {
            return _GetIndexOfSideClickedBy(p) != -1;
        }

        protected int _GetIndexOfVertexClickedBy(PointF p)
        {
            for (int i = 0; i < this.NumberOfVertices; ++i)
                if (_vertexManager.GetVertex(i).IsClickedBy(p))
                    return i;

            return -1;
        }

        protected int _GetIndexOfSideClickedBy(PointF p)
        {
            for (int i = 0; i < this.NumberOfVertices; ++i)
            {
                if (_GetSide(i).IsClickedBy(p))
                    return i;
            }

            return -1;
        }
        
        protected bool _IsFirstVertexClickedBy(PointF p)
        {
            return _GetIndexOfVertexClickedBy(p) == 0;
        }

        protected Line _GetSide(int sideIndex)
        {
            int numberOfVertices = NumberOfVertices;

            return new Line(_vertexManager.GetVertex(sideIndex).Location,
                            _vertexManager.GetVertex((sideIndex + 1) % numberOfVertices).Location,
                            _sideColors[sideIndex]);
        }

    }
}
