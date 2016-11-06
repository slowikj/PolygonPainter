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

        public FillingInfo (FastBitmap texture, Color lightColor)
        {
            _texture = texture;
            _lightColor = lightColor;
        }

        public FillingInfo ()
        {
            _texture = null;
            _lightColor = Color.White;
        }
    }
}
