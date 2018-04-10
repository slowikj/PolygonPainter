using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonPainter
{
    public class Vector3D
    {
        private double[] _v;

        public double Length
        {
            get
            {
                return Math.Sqrt(_v.Select(x => x * x).Sum());
            }
        }

        public double this[int index]
        {
            get
            {
                return _v[index];
            }
            set
            {
                _v[index] = value;
            }
        }

        public Vector3D(double x, double y, double z)
        {
            _v = new double[] { x, y, z };
        }
        
        public Vector3D(double[] v)
        {
            _v = v;
        }

        public Vector3D(Color color)
        {
            _v = new double[] { color.R, color.G, color.B };
        }
        
        public void Normalize()
        {
            double l = this.Length;

            if (l == 0)
                return;

            for(int i = 0; i < 3; ++i)
            {
                _v[i] /= l;
            }
        }

        public Vector3D Zip(Vector3D other)
        {
            return new Vector3D(this._v.Zip(other._v, (a, b) => a * b).ToArray());
        }

        public static Vector3D operator* (Vector3D vector, double d)
        {
            return new Vector3D(vector._v.Select(x => x * d).ToArray());
        }

        public static Vector3D operator /(Vector3D vector, double d)
        {
            return new Vector3D(vector._v.Select(x => x / d).ToArray());
        }

        public static Vector3D operator- (Vector3D a, Vector3D b)
        {
            return new Vector3D(a._v.Zip(b._v, (x, y) => x - y).ToArray());
        }

        public static Vector3D operator+ (Vector3D a, Vector3D b)
        {
            return new Vector3D(a._v.Zip(b._v, (x, y) => x + y).ToArray());
        }

        public double Sum()
        {
            return _v.Sum();
        }

    }
}
