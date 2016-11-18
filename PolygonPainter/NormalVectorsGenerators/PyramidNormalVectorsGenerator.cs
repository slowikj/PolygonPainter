using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonPainter.NormalVectorsGenerators
{
    public class PyramidNormalVectorsGenerator : NormalVectorsGenerator
    {
        public PyramidNormalVectorsGenerator(int canvasWidth, int canvasHeight)
            : base(canvasWidth, canvasHeight)
        {
        }
        
        protected override double[] _GetNormalVector(int x, int y)
        {
            double[] N = null;

            PointD a = new PointD(0, 0);
            PointD b = new PointD(0, _canvasHeight);
            PointD c = new PointD(_canvasWidth, _canvasHeight);
            PointD d = new PointD(_canvasWidth, 0);

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

            return N;
        }

    }
}
