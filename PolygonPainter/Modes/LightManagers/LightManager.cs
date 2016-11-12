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
    public abstract class LightManager
    {
        protected int _canvasWidth, _canvasHeight;

        public LightManager(int canvasWidth = 0, int canvasHeight = 0)
        {
            _canvasWidth = canvasWidth;
            _canvasHeight = canvasHeight;
        }

        public abstract void TimerTick();
        public abstract void KeyDown(Keys key);
        public abstract double[] GetVectorToLight(int x, int y);
    }
}
