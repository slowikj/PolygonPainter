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
            _texture = new FastBitmap(texture, false);
            _normalVectorsMap = new FastBitmap(normalVectorsMap, false);
            _heightMap = new FastBitmap(heightMap, false);
            _lightColor = lightColor;
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
        
    }
}
