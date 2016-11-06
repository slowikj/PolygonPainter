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
    public partial class FillingDialog : Form
    {
        private FillingInfo _fillingInfo;
        private bool _canExit;

        public FillingInfo FillingInfo
        {
            get
            {
                return _fillingInfo;
            }
        }

        public FillingDialog()
        {
            InitializeComponent();

            _fillingInfo = new FillingInfo();
            _canExit = true;
        }
        
        private void lightColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            DialogResult dialogResult = colorDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                colorPanel.BackColor = colorDialog.Color;
                _fillingInfo.LightColor = colorDialog.Color;
            }
        }

        private void textureButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                Bitmap image = new Bitmap(openFileDialog.FileName);
                _fillingInfo.Texture = new FastBitmap(image);

                Bitmap iconImage = GetResizedImage(image, textureButton.Width, texturePanel.Height);
                texturePanel.CreateGraphics().DrawImage(iconImage, 0, 0);
            }
        }

        private Bitmap GetResizedImage(Image image, int width, int height)
        {
            Bitmap res = new Bitmap(image, width, height);

            using (Graphics graphics = Graphics.FromImage(res))
            {
                //set the resize quality modes to high quality
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //draw the image into the target bitmap
                graphics.DrawImage(image, 0, 0, res.Width, res.Height);
            }

            return res;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (_fillingInfo.Texture == null)
            {
                MessageBox.Show("No texture has been chosen");
                _canExit = false;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _canExit = true;
        }

        private void FillingDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_canExit)
            {
                e.Cancel = true;
            }
        }
   }
}
