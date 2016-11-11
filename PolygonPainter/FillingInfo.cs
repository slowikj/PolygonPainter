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
    public class FillingInfo
    {
        private FastBitmap _texture;
        private FastBitmap _normalVectorsMap;
        private FastBitmap _heightMap;

        private Color _lightColor;
        
        public FillingInfo (Bitmap texture, Bitmap normalVectorsMap, Bitmap heightMap, Color lightColor)
        {
            _texture = new FastBitmap(texture, true);
            _normalVectorsMap = new FastBitmap(normalVectorsMap, true);
            _heightMap = new FastBitmap(heightMap, true);
            _lightColor = lightColor;
        }

        public Color GetPixelOfTexture(int x, int y)
        {
            return _GetPixelOf(_texture, x, y);
        }

        public Color GetPixelOfNormalVectorsMap(int x, int y)
        {
            return _GetPixelOf(_normalVectorsMap, x, y);
        }

        public Color GetPixelOfHeightMap(int x, int y)
        {
            return _GetPixelOf(_heightMap, x, y);
        }

        public Color GetLightColor()
        {
            return _lightColor;
        }

        private Color _GetPixelOf(FastBitmap bitmap, int x, int y)
        {
            if(x >= bitmap.Width || y >= bitmap.Height)
            {
                x *= 2;
                y *= 2;

                bitmap.Resize(Math.Max(x + 1, bitmap.Width),
                              Math.Max(y + 1, bitmap.Height));
            }

            return bitmap.GetPixel(x, y);
        }
        
    }
}
