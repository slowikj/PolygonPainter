using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonPainter.Modes.LightManagers
{
    public class ManualChangeLight : LightManager
    {
        private double[] _lightPoint;
        private readonly int pixelsChange = 10;

        public ManualChangeLight(int canvasWidth, int canvasHeight)
            : base(canvasWidth, canvasHeight)
        {
            _lightPoint = new double[3] { canvasWidth / 2, canvasHeight / 2, 30 };
        }
        
        public override double[] GetVectorToLight(int x, int y)
        {
            return new double[3] { _lightPoint[0] - x, _lightPoint[1] - y, _lightPoint[2] };
        }

        public override void KeyDown(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    _lightPoint[1] = Math.Max(0, _lightPoint[1] - pixelsChange);
                    break;
                case Keys.Down:
                    _lightPoint[1] = Math.Min(_canvasHeight, _lightPoint[1] + pixelsChange);
                    break;
                case Keys.Left:
                    _lightPoint[0] = Math.Max(0, _lightPoint[0] - pixelsChange);
                    break;
                case Keys.Right:
                    _lightPoint[0] = Math.Min(_canvasWidth, _lightPoint[0] + pixelsChange);
                    break;
                case Keys.W:
                    _lightPoint[2] = Math.Min(2000, _lightPoint[2] + pixelsChange);
                    break;
                case Keys.S:
                    _lightPoint[2] = Math.Max(1, _lightPoint[2] - pixelsChange);
                    break;
            }

        }

        public override void TimerTick()
        {
        }
    }
}
