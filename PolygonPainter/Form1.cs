﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PolygonPainter.Shapes;
using PolygonPainter.Modes;

using PolygonPainter.Shapes.PolygonClasses;

namespace PolygonPainter
{
    public partial class Form1 : Form
    {
        private readonly Color _DEFAULT_VERTEX_COLOR = Color.Red,
                               _DEFAULT_LINE_COLOR = Color.BlueViolet,
                               _DEFAULT_SELECT_COLOR = Color.Green;

        private List<Shape> _Shapes;
        private Mode _currentMode;
        private Dictionary<String, Mode> _modes;
        private Dictionary<String, RadioButton> _modeButtons;

        private Pen _meshPen;

        public Form1()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.Selectable,
                          true);

            _Shapes = new List<Shape>();
            
            _modes = _PrepareModesDictionary();
            _modeButtons = _PrepareModeButtonsDictionary();
            _currentMode = _modes["AddPolygonMode"];

            _meshPen = new Pen(Color.LightGray);
            
        }

        Dictionary<String, Mode> _PrepareModesDictionary()
        {
            Dictionary<String, Mode> res = new Dictionary<string, Mode>();
            res.Add("AddPolygonMode", new AddPolygonMode(_Shapes, canvas, _DEFAULT_VERTEX_COLOR, _DEFAULT_LINE_COLOR));
            res.Add("SelectMode", new SelectMode(_Shapes, canvas, _DEFAULT_SELECT_COLOR, automaticRelationBox));
            res.Add("AddVertexToPolygonMode", new AddVertexToPolygonMode(_Shapes, canvas));
            res.Add("SetRelationMode", new SetRelationMode(_Shapes, canvas));
            res.Add("FillPolygonMode", new FillPolygonMode(_Shapes, canvas));

            return res;
        }

        Dictionary<String, RadioButton> _PrepareModeButtonsDictionary()
        {
            Dictionary<String, RadioButton> res = new Dictionary<string, RadioButton>();

            res["AddPolygonMode"] = addPolygonButton;
            res["SelectMode"] = selectButton;
            res["AddVertexToPolygonMode"] = addVertexToPolygonButton;
            res["SetRelationMode"] = setRelationButton;
            res["FillPolygonMode"] = fillPolygonButton;

            return res;
        }

        private void addPolygonButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_modeButtons["AddPolygonMode"].Checked)
                _ChangeMode("AddPolygonMode");
        }

        private void addVertexToPolygonButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_modeButtons["AddVertexToPolygonMode"].Checked)
                _ChangeMode("AddVertexToPolygonMode");
        }

        private void selectButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_modeButtons["SelectMode"].Checked)
                _ChangeMode("SelectMode");
        }

        private void setRelationButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_modeButtons["SetRelationMode"].Checked)
                _ChangeMode("SetRelationMode");
        }

        private void fillPolygonButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_modeButtons["FillPolygonMode"].Checked)
                _ChangeMode("FillPolygonMode");
        }
        
        private void _ChangeMode (String modeName)
        {
            if (_currentMode.IsModeChangeForbidden() && _currentMode != _modes[modeName])
            {
                MessageBox.Show(String.Format("{0} mode is forbidden now!", modeName));
                _modeButtons[_currentMode.ToString()].Checked = true;
            }
            else
            {
                _currentMode = _modes[modeName];
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            _currentMode.MouseMove(sender, e);
        }
        
        private void canvas_MouseClick(object sender, MouseEventArgs e)
        {
            _currentMode.MouseClick(sender, e);
        }

       
        private void canvas_MousefloatClick(object sender, MouseEventArgs e)
        {
            _currentMode.MousefloatClick(sender, e);
        }

        private void automaticRelationBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            _currentMode.MouseUp(sender, e);
        }
        
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            ShapesDrawnLabel.Text = _Shapes.Count.ToString();
            
            Bitmap bitmap = new Bitmap(canvas.Width, canvas.Height, e.Graphics);
            
            using (Graphics newG = Graphics.FromImage(bitmap))
            {
                var context = BufferedGraphicsManager.Current;
                BufferedGraphics buf = context.Allocate(newG,
                                                        canvas.DisplayRectangle);
                Graphics g = buf.Graphics;
                

                g.Clear(Color.White);

                _DrawGrid(g);

                foreach (Shape shape in _Shapes)
                    shape.Draw(g);

                buf.Render();
            }

            e.Graphics.DrawImage(bitmap, new PointF(0, 0));
        }

        private void _DrawGrid(Graphics g)
        {
            Line line = new Line(new PointF(), new PointF(), _meshPen.Color);

            int numOfCells = 50, cellSize = 30;
            for (int y = 0; y < numOfCells; ++y)
            {
                line.Begin = new PointF(0, y * cellSize);
                line.End = new PointF(numOfCells * cellSize, y * cellSize);

                line.Draw(g);
            }

            for (int x = 0; x < numOfCells; ++x)
            {
                line.Begin = new PointF(x * cellSize, 0);
                line.End = new PointF(x * cellSize, numOfCells * cellSize);

                line.Draw(g);
            }
        }
    }
}
