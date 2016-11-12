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
    public class SelectMode : Mode
    {
        protected IHandler _currentHandler;
        protected HandlerFactory _handlerFactory;
        protected CheckBox _automaticRelationBox;

        public SelectMode(List<Shape> shapes, PictureBox canvas, Color markingColor, CheckBox automaticRelationBox)
            : base(shapes, canvas)
        {
            _currentHandler = new EmptyHandler();
            _automaticRelationBox = automaticRelationBox;
            _handlerFactory = new HandlerFactory(_shapes, markingColor, _automaticRelationBox);
        }

        public override void MouseClick(object obj, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _SelectPartOfShape(e);
            }
        }

        public override void MousedoubleClick(object obj, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    _SelectEntireShape(e);
                    
                    break;
                case MouseButtons.Left:
                    _DeleteObject();

                    break;
            }
        }

        private void _SelectEntireShape(MouseEventArgs e)
        {
            _ChangeHandler(e, false);
        }

        private void _SelectPartOfShape(MouseEventArgs e)
        {
            _ChangeHandler(e, true);
        }

        private void _ChangeHandler(MouseEventArgs e, bool wasOnlyOneClick)
        {
            _currentHandler.Unmark();

            _currentHandler = _GetChangedMarkedObject(e, wasOnlyOneClick);

            _currentHandler.Mark();

            this.UpdateCanvas();
        }

        private void _DeleteObject()
        {
            _currentHandler.Delete();
            if (!(_currentHandler is EmptyHandler))
            {
                _currentHandler = new EmptyHandler();
                this.UpdateCanvas();
            }
        }

        public override string ToString()
        {
            return "SelectMode";
        }

        private IHandler _GetChangedMarkedObject(MouseEventArgs e, bool wasOnlyOneClick)
        {
           
            IHandler res = (wasOnlyOneClick ? _handlerFactory.GetPartOfShapeHandler(e.Location)
                                            : _handlerFactory.GetEntireShapeHandler(e.Location));
            
            return res;
        }
        
        public override void MouseMove(object obj, MouseEventArgs e)
        {
           if (e.Button == MouseButtons.Left)
           {
               if (_currentHandler.Move(e.Location))
               {
                    this.UpdateCanvas();
               }
           } 
        }

        public override void MouseUp(object obj, MouseEventArgs e)
        {
            if (_automaticRelationBox.Checked && e.Button == MouseButtons.Left)
            {
                try
                {
                    _currentHandler.FixAutomaticRelations();
                }
                catch (OperationImpossibleException mes)
                {
                    MessageBox.Show(mes.Message);
                }

                this.UpdateCanvas();
            }
        }

        public override bool IsModeChangeForbidden()
        {
            return false;
        }

        public override void Clear()
        {
            _currentHandler.Unmark();
            _currentHandler = new EmptyHandler();
            this.UpdateCanvas();
        }

        public override void KeyEventHandler(Keys keyData)
        {
        }
    }
}
