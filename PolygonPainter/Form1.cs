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
using PolygonPainter.Modes;

using PolygonPainter.Shapes.PolygonClasses;
using System.Drawing.Imaging;

namespace PolygonPainter
{
    public partial class Form1 : Form
    {
        private readonly Color _DEFAULT_VERTEX_COLOR = Color.Red,
                               _DEFAULT_SIDE_COLOR = Color.BlueViolet,
                               _DEFAULT_SELECT_COLOR = Color.Green;

        private List<Shape> _shapes;
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

            _shapes = new List<Shape>();
            
            _modes = _PrepareModesDictionary();
            _modeButtons = _PrepareModeButtonsDictionary();
            _currentMode = _modes["AddPolygonMode"];

            _meshPen = new Pen(Color.LightGray);

            MessageBox.Show(canvas.Width.ToString() + " " + canvas.Height.ToString());

            //Segment a = new Segment(new Point(0, 0), new Point(5, 5));
            //Segment b = new Segment(new Point(3, 3), new Point(3, 0));
            //SegmentIntersectionInfo res = a.GetIntersectionWith(b);

            //MessageBox.Show((res.Point.HasValue ? res.Point.Value.ToString() : "null") + " " + res.Type.ToString());

            //PolygonCreator pc = new PolygonCreator(Color.Black, Color.BlueViolet);
            //pc.AddVertexClickedBy(new Point(5, 5));
            //pc.AddVertexClickedBy(new Point(13, 5));
            //pc.AddVertexClickedBy(new Point(13, 13));
            //pc.AddVertexClickedBy(new Point(5, 13));
            //pc.AddVertexClickedBy(new Point(5, 5));
            //MessageBox.Show(pc.Area().ToString());

            //pc = new PolygonCreator(Color.Black, Color.BlueViolet);
            //pc.AddVertexClickedBy(new Point(23, 23));
            //pc.AddVertexClickedBy(new Point(23, 5));
            //pc.AddVertexClickedBy(new Point(5, 5));
            //pc.AddVertexClickedBy(new Point(23, 23));

            //MessageBox.Show(pc.Area().ToString());
        }

        Dictionary<String, Mode> _PrepareModesDictionary()
        {
            Dictionary<String, Mode> res = new Dictionary<string, Mode>();
            res.Add("AddPolygonMode", new AddPolygonMode(_shapes, canvas, _DEFAULT_VERTEX_COLOR, _DEFAULT_SIDE_COLOR));
            res.Add("SelectMode", new SelectMode(_shapes, canvas, _DEFAULT_SELECT_COLOR, automaticRelationBox));
            res.Add("AddVertexToPolygonMode", new AddVertexToPolygonMode(_shapes, canvas));
            res.Add("SetRelationMode", new SetRelationMode(_shapes, canvas));
            res.Add("FillMode", new FillMode(_shapes, canvas));
            res.Add("IntersectionMode", new IntersectionMode(_shapes, canvas));


            return res;
        }

        Dictionary<String, RadioButton> _PrepareModeButtonsDictionary()
        {
            Dictionary<String, RadioButton> res = new Dictionary<string, RadioButton>();

            res["AddPolygonMode"] = addPolygonButton;
            res["SelectMode"] = selectButton;
            res["AddVertexToPolygonMode"] = addVertexToPolygonButton;
            res["SetRelationMode"] = setRelationButton;
            res["FillMode"] = fillButton;
            res["IntersectionMode"] = intersectionButton;

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
        
        private void fillButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_modeButtons["FillMode"].Checked)
                _ChangeMode("FillMode");
        }


        private void intersectionButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_modeButtons["IntersectionMode"].Checked)
                _ChangeMode("IntersectionMode");
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
                _currentMode.Clear();
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

       
        private void canvas_MousedoubleClick(object sender, MouseEventArgs e)
        {
            _currentMode.MousedoubleClick(sender, e);
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
            ShapesDrawnLabel.Text = _shapes.Count.ToString();
            
            FastBitmap fastBitmap = new FastBitmap(new Bitmap(canvas.Width, canvas.Height, e.Graphics));

            PaintTools paintTools = new PaintTools(canvas, fastBitmap, e.Graphics);

            _DrawGrid(paintTools);
            
            _DrawShapes(paintTools);
        }

        private void _DrawShapes(PaintTools paintTools)
        {
            foreach (Shape shape in _shapes)
            {
                shape.DrawFilling(paintTools);
            }

            paintTools.Graphics.DrawImage(paintTools.Bitmap.GetBitmap(), new Point(0, 0));

            foreach (Shape shape in _shapes)
            {
                shape.DrawContours(paintTools);
            }
        }

        private void _DrawGrid(PaintTools paintTools)
        {
            int numOfCells = 50, cellSize = 30;
            for (int y = 0; y < numOfCells; ++y)
            {
                Segment segment = new Segment(new PointD(0, y * cellSize),
                                              new PointD(numOfCells * cellSize, y * cellSize),
                                              _meshPen.Color);
            
                segment.Draw(paintTools);
            }

            for (int x = 0; x < numOfCells; ++x)
            {
                Segment segment = new Segment(new PointD(x * cellSize, 0),
                                              new PointD(x * cellSize, numOfCells * cellSize),
                                              _meshPen.Color);
                
                segment.Draw(paintTools);
            }
        }
    }
}

