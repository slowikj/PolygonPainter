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
        private Bitmap _texture, _normalVectorsMap, _heightMap;
        Color _lightColor;
        private bool _canExit;

        private bool _fromFile;
        
        public FillingInfo FillingInfo
        {
            get
            {
                return (_fromFile ? new FillingInfo(_texture, _normalVectorsMap, _heightMap, _lightColor)
                                  : new FillingInfo(_texture, null, _heightMap, _lightColor));
            }
        }

        public FillingDialog()
        {
            InitializeComponent();

            _texture = null;
            _normalVectorsMap = null;
            _heightMap = null;
            _lightColor = Color.White;
            _fromFile = false;
            
            _canExit = true;
        }
        
        private void lightColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            DialogResult dialogResult = colorDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                colorPanel.BackColor = colorDialog.Color;
                _lightColor = colorDialog.Color;
            }
        }

        private void textureButton_Click(object sender, EventArgs e)
        {
            Bitmap image = _GetImage();
            if (image != null)
            {
                _SetImage(image, ref _texture, texturePanel);
            }
        }
        
        private void normalVectorsButton_Click(object sender, EventArgs e)
        {
            Bitmap image = _GetImage();
            if (image != null)
            {
                _SetImage(image, ref _normalVectorsMap, normalVectorsPanel);
            }
        }
        
        private void heightMapButton_Click(object sender, EventArgs e)
        {
            Bitmap image = _GetImage();
            if (image != null)
            {
                _SetImage(image, ref _heightMap, heightMapPanel);
            }
        }

        private Bitmap _GetImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult dialogResult = openFileDialog.ShowDialog();
            
            return dialogResult == DialogResult.OK ? new Bitmap(openFileDialog.FileName)
                                                   : null;
        }

        private void _SetImage(Bitmap image, ref Bitmap destination, Panel panel)
        {
            destination = image;

            //Bitmap iconImage = _GetResizedImage(image, panel.Width, panel.Height);
            Bitmap iconImage = new Bitmap(image, panel.Width, panel.Height);
            panel.CreateGraphics().DrawImage(iconImage, 0, 0);
        }

        
        //private Bitmap _GetResizedImage(Image image, int width, int height)
        //{
        //    Bitmap res = new Bitmap(image, width, height);

        //    using (Graphics graphics = Graphics.FromImage(res))
        //    {
        //        //set the resize quality modes to high quality
        //        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //        //draw the image into the target bitmap
        //        graphics.DrawImage(image, 0, 0, res.Width, res.Height);
        //    }

        //    return res;
        //}

        private void okButton_Click(object sender, EventArgs e)
        {
            if (_texture == null || (_fromFile == true && _normalVectorsMap == null) || _heightMap == null)
            {
                MessageBox.Show("Some attributes hasn't been chosen");
                _canExit = false;
            }
        }

        private void fromFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _fromFile = fromFileCheckBox.Checked;
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
                _canExit = true;
            }
        }
    }
}
