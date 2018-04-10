using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PolygonPainter.NormalVectorsGenerators;

namespace PolygonPainter
{
    public class FillingInfoGenerator
    {
        private readonly string _defaultTexturePath = "..\\..\\..\\sample_images\\tulipany.jpg";
        private readonly string _defaultNormalVectorsMapPath = "..\\..\\..\\sample_images\\199_norm.JPG";
        private readonly string _defaultHeightMapPath = "..\\..\\..\\sample_images\\height.bmp";

        private int _canvasWidth, _canvasHeight;

        private FillingInfo _previousFilling;
        private FillingDialog _userPreferences;

        public FillingInfoGenerator(FillingInfo previousFilling, FillingDialog userPreferences,
                                    int canvasWidth, int canvasHeight)
        {
            _previousFilling = previousFilling;
            _userPreferences = userPreferences;

            _canvasWidth = canvasWidth;
            _canvasHeight = canvasHeight;
        }

        public FillingInfo GetFilling()
        {
            return new FillingInfo(_GetTexture(),
                                   _GetNormalVectors(),
                                   _GetHeightMap(),
                                   _GetColorOfLight());
        }

        private FastBitmap _GetTexture()
        {
            if (_userPreferences.Texture == null)
            {
                return _previousFilling == null ? _GetDefaultTexture()
                                                : _previousFilling.Texture;
            }
            else
            {
                return _userPreferences.Texture;
            }
        }

        private double[,][] _GetNormalVectors()
        {
            NormalVectorsGenerator normalVectorsGenerator = null;

            switch(_userPreferences.NormalVectorsType)
            {
                case NormalVectorsType.FromTexture:
                    normalVectorsGenerator = new FromTextureNormalVectorsGenerator(_userPreferences.NormalVectorsMap);
                    break;
                case NormalVectorsType.Pyramid:
                    normalVectorsGenerator = new PyramidNormalVectorsGenerator(_canvasWidth, _canvasHeight);
                    break;
                case NormalVectorsType.None:
                    return _previousFilling?.NormalVectors ?? _GetDefaultNormalVectors();
            }

            return normalVectorsGenerator.GetNormalVectors();
        }

        private FastBitmap _GetHeightMap()
        {
            if (_userPreferences.HeightMap == null)
            {
                return _previousFilling?.HeightMap ?? _GetDefaultHeightMap();
            }
            else
            {
                return _userPreferences.HeightMap;
            }
        }

        private Color _GetColorOfLight()
        {
            if(_userPreferences.ColorOfLight == Color.Empty)
            {
                return _previousFilling?.LightColor ?? _GetDefaultColorOfLight();
            }
            else
            {
                return _userPreferences.ColorOfLight;
            }
        }
        
        private FastBitmap _GetDefaultTexture()
        {
            return new FastBitmap(new Bitmap(_defaultTexturePath), _canvasWidth, _canvasHeight);
        }

        private double[,][] _GetDefaultNormalVectors()
        {
            FromTextureNormalVectorsGenerator normalVectorsGenerator;
            normalVectorsGenerator = new FromTextureNormalVectorsGenerator(new FastBitmap(new Bitmap(_defaultNormalVectorsMapPath),
                                                                                          _canvasWidth,
                                                                                          _canvasHeight));

            return normalVectorsGenerator.GetNormalVectors();
        }

        private FastBitmap _GetDefaultHeightMap()
        {
            return new FastBitmap(new Bitmap(_defaultHeightMapPath), _canvasWidth, _canvasHeight);
        }

        private Color _GetDefaultColorOfLight()
        {
            return Color.White;
        }
    }
}
