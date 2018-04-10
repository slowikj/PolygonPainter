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
        private const int DEFAULT_WIDTH = 600, DEFAULT_HEIGHT = 600;
        
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

        public FastBitmap(Bitmap bitmap,
                          int canvasWidth,
                          int canvasHeight)
        {
            _bitmap = new Bitmap(canvasWidth, canvasHeight);
            _Lock();
            _FillBitmap(bitmap);
        }

        public FastBitmap(Bitmap bitmap, bool makeCopy = true)
        {
            _bitmap = (makeCopy ? new Bitmap(bitmap)
                                : bitmap);
            
            _Lock();
        }


        private void _FillBitmap(Bitmap b)
        {
            FastBitmap givenB = new FastBitmap(b);

            for(int i = 0; i < this.Width; ++i)
            {
                for(int j = 0; j < this.Height; ++j)
                {
                    this.SetPixel(i, j, givenB.GetPixel(i % givenB.Width, j % givenB.Height));
                }
            }
        }
        
        public unsafe void SetPixel(int x, int y, Color c)
        {
            if (!IsInside(x, y))
            {
                return;
            }

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

        private void _Unlock()
        {
            _bitmap.UnlockBits(_bData);
            _isLocked = false;
        }

        public unsafe Color GetPixel(int x, int y)
        {
            if (!IsInside(x, y))
            {
                return Color.Black;
            }
            
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
            return 0 <= x && x < this.Width && 0 <= y && y < this.Height;
        }
        
        public Bitmap GetBitmap()
        {
            _Unlock();

            return _bitmap;
        }
    }
}
