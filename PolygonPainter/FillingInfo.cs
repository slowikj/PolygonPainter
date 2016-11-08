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
        private FastBitmap _normalVectors;
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

        public FastBitmap NormalVectors
        {
            get
            {
                return _normalVectors;
            }
            set
            {
                _normalVectors = value;
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
        

        public FillingInfo (FastBitmap texture, FastBitmap normalVectors, Color lightColor)
        {
            _texture = texture;
            _normalVectors = normalVectors;
            _lightColor = lightColor;
        }

        public FillingInfo ()
        {
            _texture = null;
            _normalVectors = null;
            _lightColor = Color.White;
        }
    }
}
