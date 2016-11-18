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
    public abstract class NormalVectorsGenerator
    {
        protected int _canvasWidth, _canvasHeight;
        protected double[,][] _normalVectors;
      
        public NormalVectorsGenerator(int canvasWidth, int canvasHeigth)
        {
            _canvasWidth = canvasWidth;
            _canvasHeight = canvasHeigth;

            _normalVectors = null;
        }

        public double[,][] GetNormalVectors()
        {
            if(_normalVectors == null)
            {
                _normalVectors = _GetNormalVectors();
            }

            return _normalVectors;
        }
        
        protected double[,][] _GetNormalVectors()
        {
            double[,][] res = new double[_canvasWidth, _canvasHeight][];

            for (int i = 0; i < _canvasWidth; ++i)
            {
                for (int j = 0; j < _canvasHeight; ++j)
                {
                    res[i, j] = _GetNormalVector(i, j);
                }
            }

            return res;
        }

        protected abstract double[] _GetNormalVector(int x, int y);
    }
}
