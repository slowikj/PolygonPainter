using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PolygonPainter.Shapes;
using PolygonPainter.Shapes.PolygonClasses;
using PolygonPainter.Interfaces;
using PolygonPainter.Shapes.GeneralHandlers;

namespace PolygonPainter.Modes
{
    public class IntersectionMode : Mode
    {
        private List<Polygon.EntirePolygonHandler> _handlers;

        private HandlerFactory _handlerFactory;
        
        public IntersectionMode(List<Shape> shapes, PictureBox canvas)
            : base(shapes, canvas)
        {
            _handlers = new List<Polygon.EntirePolygonHandler>();
            _handlerFactory = new HandlerFactory(_shapes, Color.Blue, null);
        }

        public override void Clear()
        {
            foreach (var handler in _handlers)
            {
                handler.Unmark();
            }

            _handlers.Clear();

            this.UpdateCanvas();
        }

        public override bool IsModeChangeForbidden()
        {
            return false;
        }

        public override void MouseClick(object obj, MouseEventArgs e)
        {
            switch(e.Button)
            {
                case MouseButtons.Left:
                    _UpdateChosenPolygons(e.Location);

                    if (_handlers.Count == 2)
                    {
                        _ComputeIntersection();
                    }

                    break;
                case MouseButtons.Right:
                    this.Clear();
                    break;
            }

            this.UpdateCanvas();
        }


        public override void MousedoubleClick(object obj, MouseEventArgs e)
        {
        }

        public override void MouseMove(object obj, MouseEventArgs e)
        {
        }

        public override void MouseUp(object obj, MouseEventArgs e)
        {
        }
        
        private void _UpdateChosenPolygons(Point p)
        {
            if (_handlers.Count == 2)
            {
                this.Clear();
            }
            
            IHandler handler = _handlerFactory.GetEntireShapeHandler(p);

            if (handler is EmptyHandler)
            {
                return;
            }

            var polygonHandler = handler  as Polygon.EntirePolygonHandler;

            if (!_handlers.Any(elem => elem.PolygonIndex == polygonHandler.PolygonIndex))
            {
                _handlers.Add(polygonHandler);
                polygonHandler.Mark();
            }
        }

        private void _ComputeIntersection()
        {
            int i = _handlers[0].PolygonIndex;
            int j = _handlers[1].PolygonIndex;
            
            try
            {
                Polygon[] newPolygons = (_shapes[i] as Polygon).GetIntersectionPolygons(_shapes[j] as Polygon);

                _DeleteIntersectedPolygons();
                _AppendNewPolygons(newPolygons);

                this.Clear();
                this.UpdateCanvas();
            }
            catch (OperationImpossibleException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void _AppendNewPolygons(Polygon[] newPolygons)
        {
            foreach (Polygon newPolygon in newPolygons)
            {
                _shapes.Add(newPolygon);
            }
        }

        private void _DeleteIntersectedPolygons()
        {
            _handlers.Sort((x, y) => y.PolygonIndex.CompareTo(x.PolygonIndex));
            foreach (var handler in _handlers)
            {
                handler.Delete();
            }
        }

        public override void KeyEventHandler(Keys keyData)
        {
        }

        public override void TimerTickHandler()
        {
        }
    }
}
