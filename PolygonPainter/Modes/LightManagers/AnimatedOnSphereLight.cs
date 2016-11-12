using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonPainter.Modes.LightManagers
{
    public class AnimatedOnSphereLight : LightManager
    {
        private readonly int _pixelChange = 30;
        private int _r, _x_0, _y_0, _z_0;

        private int _currentX, _currentY;
        private double _currentZ;

        public AnimatedOnSphereLight(int canvasWidth, int canvasHeight)
            : base(canvasWidth, canvasHeight)
        {
            canvasHeight = 100;
            canvasWidth = 100;

            _x_0 = canvasWidth / 2;
            _y_0 = canvasHeight / 2;
            _z_0 = 0;
            _r = canvasWidth / 2;

            _currentX  = _currentY = 0;
            _currentZ = _GetZ(_currentX, _currentY);
        }

        public override double[] GetVectorToLight(int x, int y)
        {
            return new double[3] { _currentX - x, _currentZ - y, _currentZ };
        }

        public override void KeyDown(Keys keyData)
        {
        }

        public override void TimerTick()
        {
            _MoveLightPoint();
        }

        private void _MoveLightPoint()
        {
            _MoveXY();

            _currentZ = _GetZ(_currentX, _currentY);
        }

        private void _MoveXY()
        {
            if (_currentY == _canvasHeight)
            {
                _currentX = (_currentX == _canvasWidth ? 0
                                                       : Math.Min(_canvasWidth, _currentX + _pixelChange));
                _currentY = 0;
            }
            else
            {
                _currentY = Math.Min(_canvasHeight, _currentY + _pixelChange);
            }
        }

        private double _GetZ(int x, int y)
        {
            double a = (_currentX - x) * (_currentX - x) + (_currentY - y) * (_currentY - y);

            if (a < 0)
                throw new Exception();

            return Math.Sqrt(_r * _r - a) + _z_0;
        }
    }
}
