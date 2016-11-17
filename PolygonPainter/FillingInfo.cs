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
        private readonly string _defaultTexturePath = "..\\..\\..\\sample_images\\white.png";
        private readonly string _defaultHeightMapPath = "..\\..\\..\\sample_images\\height.bmp";

        private FastBitmap _texture;
        private FastBitmap _normalVectorsMap;
        private FastBitmap _heightMap;

        private double[,][] _normalVectors;

        private Color _lightColor;

        public FastBitmap Texture
        {
            get
            {
                return _texture;
            }

            set
            {
                _texture = value;
            }
        }

        public FastBitmap NormalVectorsMap
        {
            get
            {
                return _normalVectorsMap;
            }

            set
            {
                _normalVectorsMap = value;
            }
        }

        public FastBitmap HeightMap
        {
            get
            {
                return _heightMap;
            }

            set
            {
                _heightMap = value;
                _normalVectors = _GetNormalVectors(Texture.Width, Texture.Height);
            }
        }

        public Color LightColor
        {
            get
            {
                return _lightColor;
            }

            set
            {
                _lightColor = value;
            }
        }
        
        public FillingInfo()
        {
            _lightColor = Color.White;

            _texture = new FastBitmap(new Bitmap(_defaultTexturePath));

            _normalVectorsMap = null;
            _heightMap = new FastBitmap(new Bitmap(_defaultHeightMapPath));

            this.RecomputeNormalVectors();
        }

        public FillingInfo (Bitmap texture, Bitmap normalVectorsMap, Bitmap heightMap, Color lightColor)
        {
            _texture = (texture != null ? new FastBitmap(texture, true)
                                        : new FastBitmap(new Bitmap(_defaultTexturePath)));

            _normalVectorsMap = (normalVectorsMap != null ? new FastBitmap(normalVectorsMap, true)
                                                          : null);

            _heightMap = (heightMap != null ? new FastBitmap(heightMap, true)
                                            : new FastBitmap(new Bitmap(_defaultHeightMapPath)));

            _lightColor = lightColor;

            this.RecomputeNormalVectors();
        }

        public void RecomputeNormalVectors()
        {
            _normalVectors = _GetNormalVectors(_texture.Width, _texture.Height);
        }

        public double[] GetNormalVector(int x, int y)
        {
            return _normalVectors[x, y];
        }

        public Color GetPixelOfTexture(int x, int y)
        {
            return _texture.GetPixel(x, y);
        }

        public Color GetPixelOfNormalVectorsMap(int x, int y)
        {
            return _normalVectorsMap.GetPixel(x, y);
        }

        public Color GetPixelOfHeightMap(int x, int y)
        {
            return _heightMap.GetPixel(x, y);
        }

        public Color GetLightColor()
        {
            return _lightColor;
        }

        private double[,][] _GetNormalVectors(int w, int h)
        {
            double[,][] res = new double[w, h][];
            for(int i = 0; i < w; ++i)
            {
                for(int j = 0; j < h; ++j)
                {
                    res[i, j] = _GetNormalVector(i, j);
                }
            }

            return res;
        }

        private double[] _GetNormalVector(int x, int y)
        {
            double[] N = null;
            if (NormalVectorsMap != null)
            {
                Color colorFromNormalMap = NormalVectorsMap.GetPixel(x, y);
                N = new double[3] {(double)colorFromNormalMap.R / 127.5 - 1,
                                   (double)colorFromNormalMap.G / 127.5 - 1,
                                   (double)colorFromNormalMap.B / 255};
            }
            else // pyramid
            {
                N = _GetPyramidNormalVector(x, y); 
            }
            
            // bump mapping
            double[] dhX = _GetDH(x, y, x + 1, y);
            double[] dhY = _GetDH(x, y, x, y + 1);

            double[] T = new double[] { 1, 0, (N[2] == 0 ? 0 : (-N[0] / N[2])) };
            //T = _Normalized(T);

            double[] B = new double[] { 0, 1, (N[2] == 0 ? 0 : (-N[1] / N[2])) };
            //B = _Normalized(B);

            double[] tmp1 = dhX.Zip(T, (xx, yy) => xx * yy).ToArray();
            double[] tmp2 = dhY.Zip(B, (xx, yy) => xx * yy).ToArray();
            double[] D = tmp1.Zip(tmp2, (xx, yy) => xx + yy).ToArray();
            //D = _Normalized(D);
            //N = _Normalized(N);

            double[] NN = D.Zip(N, (xx, yy) => xx + yy).ToArray();
            
            return NN;
        }

        private double[] _GetPyramidNormalVector(int x, int y)
        {
            double[] N = null;
             
            int width = Texture.Width;
            int height = Texture.Height;

            PointD a = new PointD(0, 0);
            PointD b = new PointD(0, Texture.Height);
            PointD c = new PointD(Texture.Width, Texture.Height);
            PointD d = new PointD(Texture.Width, 0);

            FreeVector cs = new FreeVector(c, a) / 2;
            PointD s = c + cs;

            FreeVector bs = new FreeVector(b, d) / 2;

            // determine the place
            Direction bsDirection = bs.GetDirection(new FreeVector(new PointD(x - b.X, y - b.Y)));
            Direction csDirection = cs.GetDirection(new FreeVector(new PointD(x - c.X, y - c.Y)));
            
            if (bsDirection == Direction.Anticlockwise && csDirection == Direction.Clockwise) // 0
            {
                N = new double[] { 0, s.Y, cs.Length };
            }
            else if (bsDirection == Direction.Clockwise && csDirection == Direction.Clockwise) // 1
            {
                N = new double[] { -s.X, 0, cs.Length };
            }
            else if (bsDirection == Direction.Clockwise && csDirection == Direction.Anticlockwise) // 2
            {
                N = new double[] { 0, -s.Y, cs.Length };
            }
            else if (bsDirection == Direction.Anticlockwise && csDirection == Direction.Anticlockwise) // 3
            {
                N = new double[] { s.X, 0, cs.Length };
            }

            //N[0] = N[0] / cs.Length - 1;
            //N[1] = N[1] / cs.Length - 1;
            //N[2] /= cs.Length;

            return N;
        }

        private double[] _GetDH(int x, int y, int xx, int yy)
        {
            Color c = HeightMap.GetPixel(xx, yy);
            Color d = HeightMap.GetPixel(x, y);

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
