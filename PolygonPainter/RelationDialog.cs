using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PolygonPainter.Shapes.PolygonClasses.Relations;

namespace PolygonPainter
{
    public partial class RelationDialog : Form
    {
        private Relation _relation;

        public Relation Relation
        {
            get
            {
                return _relation;
            }
        }

        public RelationDialog()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (verticalButton.Checked)
                    _relation = new VerticalRelation();
                else if (horizontalButton.Checked)
                    _relation = new HorizontalRelation();
                else if (lengthButton.Checked)
                    _relation = new LengthRelation(int.Parse(lengthTextBox.Text));
            }
            catch(Exception)
            {
                _relation = new EmptyRelation();
            }

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _relation = new EmptyRelation();

            this.Close();
        }
    }
}
