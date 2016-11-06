using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PolygonPainter.Shapes;
using PolygonPainter.Modes;
using System.Drawing.Imaging;

namespace PolygonPainter
{
    public class FastBitmap
    {
        private Bitmap _bitmap;
        private BitmapData _bData;

        public FastBitmap(Bitmap bitmap, int width, int height)
        {
            _bitmap = bitmap;
            _bData = _bitmap.LockBits(new Rectangle(0, 0, width, height),
                                                ImageLockMode.ReadWrite,
                                                _bitmap.PixelFormat);
        }

        public unsafe void SetPixel(int x, int y, Color c)
        {
            //if (!_IsInside(x, y, _bitmap.Width, _bitmap.Height))
            //    return;

            byte* scan0 = (byte*)_bData.Scan0.ToPointer();
            scan0[y * _bData.Stride + x * 4] = c.B;
            scan0[y * _bData.Stride + x * 4 + 1] = c.G;
            scan0[y * _bData.Stride + x * 4 + 2] = c.R;
            scan0[y * _bData.Stride + x * 4 + 3] = c.A;
        }

        private bool _IsInside(int x, int y, int width, int height)
        {
            return 0 <= x && x <= width && 0 <= y && y <= height;
        }

        private void _Render()
        {
            _bitmap.UnlockBits(_bData);
        }

        public Bitmap GetBitmap()
        {
            _Render();

            return _bitmap;
        }
    }
}
