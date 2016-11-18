using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonPainter.NormalVectorsGenerators
{
    class FromTextureNormalVectorsGenerator : NormalVectorsGenerator
    {
        private FastBitmap _normalVectorsMap;

        public FromTextureNormalVectorsGenerator(FastBitmap normalVectorsMap)
            : base(normalVectorsMap.Width, normalVectorsMap.Height)
        {
            _normalVectorsMap = normalVectorsMap;
        }

        protected override double[] _GetNormalVector (int x, int y)
        {
            Color colorFromNormalMap = _normalVectorsMap.GetPixel(x, y);
            return new double[3] {(double)colorFromNormalMap.R / 127.5 - 1,
                                   (double)colorFromNormalMap.G / 127.5 - 1,
                                   (double)colorFromNormalMap.B / 255};
        }
    }
}
