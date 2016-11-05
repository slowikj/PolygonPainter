using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonPainter.Shapes.PolygonClasses
{
    public partial class PolygonFiller
    {
        private Color _color;

        public PolygonFiller(Color color)
        {
            _color = color;
        }
        
        public void Draw(Graphics g, List<Vertex> vertices)
        {
            Dictionary<int, List<ActiveEdge>> edges = _GetEdgesTable(vertices);
            int yMin = (int)vertices.Min(v => v.Location.Y);
            int yMax = (int)vertices.Max(v => v.Location.Y);
            
            List<ActiveEdge> activeEdges = new List<ActiveEdge>();
            for(int y = yMax; y >= yMin; --y)
            {
                int erased = activeEdges.RemoveAll(e => e.YLast == y);
                activeEdges.ForEach(e => e.UpdateX());
                
                if (edges.ContainsKey(y))
                {
                    activeEdges.AddRange(edges[y]);
                }
                activeEdges.Sort((a, b) => a.CurrentX.CompareTo(b.CurrentX));
                
                for(int i = 0; i < activeEdges.Count; i += 2)
                {
                    //MessageBox.Show("rysuje " + ((int)activeEdges[i].CurrentX).ToString() + " " + ((int)activeEdges[i + 1].CurrentX).ToString());

                    _DrawLine(g, y, (int)activeEdges[i].CurrentX, (int)activeEdges[i + 1].CurrentX);
                }
            }
        }

        private void _DrawLine(Graphics g, int y, int begX, int endX)
        {
            for(int x = begX; x <= endX; ++x)
            {
                g.DrawRectangle(new Pen(_GetColor(x, y)), x, y, (float)0.5, (float)0.5);
            }
        }

        private Color _GetColor(int x, int y)
        {
            return _color;
        }
        
        private Dictionary<int, List<ActiveEdge>> _GetEdgesTable(List<Vertex> vertices)
        {
            Dictionary<int, List<ActiveEdge>> edges = new Dictionary<int, List<ActiveEdge>>();

            for (int i = 0; i < vertices.Count; ++i)
            {
                Line edge = new Line(vertices[i].Location,
                                     vertices[(i + 1) % vertices.Count].Location);

                _ProcessAddingEdge(edge, edges);             
            }

            //string res = "";
            //foreach (var elem in edges)
            //{
            //    res += " " + "( ";
            //    foreach (var x in elem.Value)
            //        res += x.ToString();
            //    res += ") ";
            //}

            //MessageBox.Show(res);
            
            return edges;
        }

        private void _ProcessAddingEdge(Line edge, Dictionary<int, List<ActiveEdge>> edges)
        {
            if (!edge.IsHorizontal)
            {
                int key = (int)edge.Begin.Y;

                if (!edges.ContainsKey(key))
                {
                    edges.Add(key, new List<ActiveEdge>());
                }

                edges[key].Add(new ActiveEdge(edge));
            }
        }
    }
}
