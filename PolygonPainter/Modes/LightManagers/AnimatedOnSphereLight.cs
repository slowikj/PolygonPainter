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
        private readonly int _pixelChange = 10;

        private double _x_0, _y_0, _z_0, _r;
        private int _beg, _end;
      
        private int _currentX, _currentY;
        private double _currentZ;

        public AnimatedOnSphereLight(int canvasWidth, int canvasHeight)
            : base(canvasWidth, canvasHeight)
        {
            _x_0 = (double)canvasWidth / 2;
            _y_0 = (double)canvasHeight / 2;
            _r = canvasHeight * Math.Sqrt(2) / 2;
            _z_0 = 0;

            _beg = 0;
            _end = _canvasHeight;

            //_beg = 200;
            //_end = 300;
            //_r = 50;

            //_x_0 = 250;
            //_y_0 = 250;

        }

        public override double[] GetVectorToLight(int x, int y)
        {
            return new double[3] { _currentX - x, _currentY - y, _currentZ };
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
            _currentX = (_currentX >= _end ? _beg : _currentX + _pixelChange);
            _currentY = (_currentY >= _end ? _beg : _currentY + _pixelChange);
        }

        private double _GetZ(int x, int y)
        {
            double a = (x - _x_0) * (x - _x_0) + (y - _y_0) * (y - _y_0);
            
            return Math.Sqrt(_r * _r - a) + _z_0;
        }
    }
}
