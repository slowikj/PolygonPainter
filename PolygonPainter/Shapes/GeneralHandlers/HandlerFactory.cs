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
using PolygonPainter.Interfaces;

namespace PolygonPainter.Shapes.GeneralHandlers
{
    public class HandlerFactory
    {
        private List<Shape> _shapes;
        private Color _markingColor;
        private CheckBox _automaticRelationBox;

        public HandlerFactory(List<Shape> shapes, Color markingColor, CheckBox automaticRelationBox)
        {
            _shapes = shapes;
            _markingColor = markingColor;
            _automaticRelationBox = automaticRelationBox;
        }

        public IHandler GetEntireShapeHandler(PointD clickedPoint)
        {
            for (int i = 0; i < _shapes.Count; ++i)
            {
                IHandler res = _shapes[i].GetEntireShapeHandler(clickedPoint, _shapes, i);
                if (!(res is EmptyHandler))
                    return res;
            }

            return new EmptyHandler();
        }

        public IHandler GetPartOfShapeHandler(PointD clickedPoint)
        {
            for (int i = 0; i < _shapes.Count; ++i)
            {
                IHandler res = _shapes[i].GetPartOfShapeHandler(clickedPoint, _shapes, i, _automaticRelationBox);
                if (!(res is EmptyHandler))
                    return res;
            }

            return new EmptyHandler();
        }
    }
}
