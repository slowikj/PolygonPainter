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
        private FastBitmap _texture, _normalVectorsMap, _heightMap;
        Color _colorOfLight;
        
        private int _canvasWidth, _canvasHeight;
        
        public Color ColorOfLight
        {
            get
            {
                return _colorOfLight;
            }
        }

        public NormalVectorsType NormalVectorsType
        {
            get
            {
                if (!pyramidCheckBox.Checked && _normalVectorsMap == null)
                {
                    return NormalVectorsType.None;
                }
                else if(pyramidCheckBox.Checked)
                {
                    return NormalVectorsType.Pyramid;
                }
                else
                {
                    return NormalVectorsType.FromTexture;
                }
            }
        }

        public FastBitmap NormalVectorsMap
        {
            get
            {
                return _normalVectorsMap;
            }
        }

        public FastBitmap HeightMap
        {
            get
            {
                return _heightMap;
            }
        }

        public FastBitmap Texture
        {
            get
            {
                return _texture;
            }
        }
        
        public FillingDialog(int canvasWidth, int canvasHeight)
        {
            InitializeComponent();

            _texture = null;
            _normalVectorsMap = null;
            _heightMap = null;
            _colorOfLight = Color.Empty;

            _canvasWidth = canvasWidth;
            _canvasHeight = canvasHeight;

            openFileDialog.RestoreDirectory = true;
            
        }
        
        private void lightColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            DialogResult dialogResult = colorDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                colorPanel.BackColor = colorDialog.Color;
                _colorOfLight = colorDialog.Color;
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

        private void _SetImage(Bitmap image, ref FastBitmap destination, Panel panel)
        {
            destination = new FastBitmap(image, _canvasWidth, _canvasHeight);
            
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
