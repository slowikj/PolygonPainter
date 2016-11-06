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

        private bool _isLocked;

        public int Width
        {
            get
            {
                return _bitmap.Width;
            }
        }

        public int Height
        {
            get
            {
                return _bitmap.Height;
            }
        }

        public FastBitmap(Bitmap bitmap)
        {
            _bitmap = new Bitmap(bitmap);
            _Lock();
        }
        
        public unsafe void SetPixel(int x, int y, Color c)
        {
            if (!_isLocked)
            {
                _Lock();
            }

            byte* scan0 = (byte*)_bData.Scan0.ToPointer();
            scan0[y * _bData.Stride + x * 4] = c.B;
            scan0[y * _bData.Stride + x * 4 + 1] = c.G;
            scan0[y * _bData.Stride + x * 4 + 2] = c.R;
            scan0[y * _bData.Stride + x * 4 + 3] = c.A;
        }

        private void _Lock()
        {
            _bData = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height),
                                                ImageLockMode.ReadWrite,
                                                _bitmap.PixelFormat);

            _isLocked = true;
        }

        public unsafe Color GetPixel(int x, int y)
        {
            if (! _isLocked)
            {
                _Lock();
            }

            byte* scan0 = (byte*)_bData.Scan0.ToPointer();

            byte B = scan0[y * _bData.Stride + x * 4];
            byte G = scan0[y * _bData.Stride + x * 4 + 1];
            byte R = scan0[y * _bData.Stride + x * 4 + 2];
            byte A = scan0[y * _bData.Stride + x * 4 + 3];

            return Color.FromArgb(A, R, G, B);
        }

        public bool IsInside(int x, int y)
        {
            return 0 <= x && x <= this.Width && 0 <= y && y <= this.Height;
        }
        
        public Bitmap GetBitmap()
        {
            _bitmap.UnlockBits(_bData);
            _isLocked = false;

            return _bitmap;
        }
    }
}
