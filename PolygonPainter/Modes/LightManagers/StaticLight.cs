using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonPainter.Modes.LightManagers
{
    public class StaticLight : LightManager
    {
        private double[] _v;

        public StaticLight()
        {
            _v = new double[3] { 0, 0, 1 };
        }

        public override double[] GetVectorToLight(int x, int y)
        {
            return _v;
        }

        public override void KeyDown(Keys key)
        {
        }

        public override void TimerTick()
        {
        }
    }
}
