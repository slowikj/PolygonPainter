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
using PolygonPainter.Modes.LightManagers;

namespace PolygonPainter.Modes
{
    public class FillMode : Mode
    {
        private LightManager _currentLightManager;
        private Dictionary<string, LightManager> _lightManagers;

        public FillMode(List<Shape> shapes, PictureBox canvas)
            : base(shapes, canvas)
        {
            _lightManagers = _GetLightManagers();
            _currentLightManager = _lightManagers["ManualChangeLight"];
        }

        public void ChangeLightManager(string lightManagerName)
        {
            _currentLightManager = _lightManagers[lightManagerName];

            for(int i = 0; i < _shapes.Count; ++i)
            {
                _shapes[i].ChangeLightManager(_currentLightManager);
            }

            this.UpdateCanvas();
        }

        private Dictionary<string, LightManager> _GetLightManagers()
        {
            Dictionary<string, LightManager> res = new Dictionary<string, LightManager>();

            res.Add("AnimatedOnSphereLight", new AnimatedOnSphereLight(_canvas.Width, _canvas.Height));
            res.Add("ManualChangeLight", new ManualChangeLight(_canvas.Width, _canvas.Height));
            res.Add("StaticLight", new StaticLight());

            return res;
        }

        public override void KeyEventHandler(Keys keyData)
        {
            _currentLightManager.KeyDown(keyData);
            this.UpdateCanvas();
        }

        public override void TimerTickHandler()
        {
          //  if (_currentLightManager is AnimatedOnSphereLight)
          //  {
                _currentLightManager.TimerTick();

                this.UpdateCanvas();
        //    }
        }

        public override bool IsModeChangeForbidden()
        {
            return false;
        }

        public override void MouseClick(object obj, MouseEventArgs e)
        {
            int shapeIndex = _GetClickedShapeIndex(e.Location);

            if (shapeIndex != -1)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        _SetFilling(shapeIndex);
                        break;
                    case MouseButtons.Right:
                        _DeleteFilling(shapeIndex);
                        break;
                }
            }
        }

        private void _SetFilling(int shapeIndex)
        {
            FillingInfo filling = _GetFilling();

            if (filling != null)
            {
                _shapes[shapeIndex].SetFilling(filling, _currentLightManager);

                this.UpdateCanvas();
            }
        }

        private void _DeleteFilling(int shapeIndex)
        {
            _shapes[shapeIndex].DeleteFilling();

            this.UpdateCanvas();
        }

        private FillingInfo _GetFilling()
        {
            FillingDialog fillingDialog = new FillingDialog();
            DialogResult dialogResult = fillingDialog.ShowDialog();
            
            switch (dialogResult)
            {
                case DialogResult.OK:
                    return fillingDialog.FillingInfo;
                default:
                    return null;
            }
        }

        private int _GetClickedShapeIndex(Point p)
        {
            for (int i = _shapes.Count - 1; i >= 0; --i)
            {
                if (_shapes[i].IsClickedBy(p))
                    return i;
            }

            return -1;
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

        public override void Clear()
        {
        }
    }
}
