using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        public Bitmap NormalVectorsMap
        {
            get
            {
                return _normalVectorsMap;
            }

            set
            {
                _normalVectorsMap = value;
            }
        }

        public Bitmap HeightMap
        {
            get
            {
                return _heightMap;
            }

            set
            {
                _heightMap = value;
            }
        }

        public Bitmap Texture
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

        public FillingDialog()
        {
            InitializeComponent();

            _texture = null;
            _normalVectorsMap = null;
            _heightMap = null;
            _lightColor = Color.White;

            openFileDialog.RestoreDirectory = true;
            
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
            
            Bitmap iconImage = new Bitmap(image, panel.Width, panel.Height);
            panel.CreateGraphics().DrawImage(iconImage, 0, 0);
        }

        
        private void okButton_Click(object sender, EventArgs e)
        {
        }
        
        private void cancelButton_Click(object sender, EventArgs e)
        {
        }

        private void FillingDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }
    }
}
