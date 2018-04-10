using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PolygonPainter.Shapes.PolygonClasses;

namespace PolygonPainter
{
    public class FillingInfo
    {
        private FastBitmap _texture;
        private FastBitmap _heightMap;

        private double[,][] _normalVectors;
        private double[,][] _bumpedNormalVectors;

        private Color _lightColor;

        public FastBitmap Texture
        {
            get
            {
                return _texture;
            }
        }

        public FastBitmap HeightMap
        {
            get
            {
                return _heightMap;
            }
        }

        public Color LightColor
        {
            get
            {
                return _lightColor;
            }
        }

        public double[,][] NormalVectors
        {
            get
            {
                return _normalVectors;
            }
        }

        public FillingInfo (FastBitmap texture, double[,][] normalVectors, FastBitmap heightMap, Color lightColor)
        {
            _texture = texture;
            _normalVectors = normalVectors;

            _heightMap = heightMap;
            _lightColor = lightColor;

            _bumpedNormalVectors = null;
        }
        
        public double[] GetBumpedNormalVector(int x, int y)
        {
            if(_bumpedNormalVectors == null)
            {
                _bumpedNormalVectors = _GetBumpedNormalVectors();
            }

            return _bumpedNormalVectors[x, y];
        }

        public Color GetPixelOfTexture(int x, int y)
        {
            return _texture.GetPixel(x, y);
        }
        
        public Color GetPixelOfHeightMap(int x, int y)
        {
            return _heightMap.GetPixel(x, y);
        }

        public Color GetLightColor()
        {
            return _lightColor;
        }

        private double[,][] _GetBumpedNormalVectors()
        {
            int width = _normalVectors.GetLength(0);
            int height = _normalVectors.GetLength(1);


            double[,][] res = new double[width, height][];

            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    res[i, j] = _GetBumpedNormalVector(i, j);
                }
            }

            return res;
        }
        
        private double[] _GetBumpedNormalVector(int x, int y)
        {
            double[] N = _normalVectors[x, y];

            // bump mapping
            double[] dhX = _GetDH(x, y, x + 1, y);
            double[] dhY = _GetDH(x, y, x, y + 1);

            double[] T = new double[] { 1, 0, (N[2] == 0 ? 0 : (-N[0] / N[2])) };
            T = _Normalized(T);

            double[] B = new double[] { 0, 1, (N[2] == 0 ? 0 : (-N[1] / N[2])) };
            B = _Normalized(B);

            double[] tmp1 = dhX.Zip(T, (xx, yy) => xx * yy).ToArray();
            double[] tmp2 = dhY.Zip(B, (xx, yy) => xx * yy).ToArray();
            double[] D = tmp1.Zip(tmp2, (xx, yy) => xx + yy).ToArray();
            //D = _Normalized(D);
            //N = _Normalized(N);

            double[] NN = D.Zip(N, (xx, yy) => xx + yy).ToArray();
            
            return _Normalized(NN);
        }
        
        private double[] _GetDH(int x, int y, int xx, int yy)
        {
            Color c = _heightMap.GetPixel(xx, yy);
            Color d = _heightMap.GetPixel(x, y);

            return new double[] { c.R - d.R, c.G - d.G, c.B - d.B };
        }

        private double[] _Normalized(double[] a)
        {
            double l = Math.Sqrt(a.Select(x => x * x).Sum());

            if (l == 0)
                return a;

            return a.Select(x => x / l).ToArray();
        }

        private double[] _GetVector(Color color)
        {
            double[] res = new double[3] { color.R, color.G, color.B };
            return res;
        }
    }
}
