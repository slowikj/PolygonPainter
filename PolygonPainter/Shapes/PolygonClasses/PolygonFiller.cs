using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PolygonPainter.Interfaces;

namespace PolygonPainter.Shapes.PolygonClasses
{
    public partial class PolygonFiller
    {
        private FillingInfo _fillingInfo;
        
        public PolygonFiller(FillingInfo fillingInfo)
        {
            _fillingInfo = fillingInfo;
        }
        
        public void Fill(PaintTools paintTools, List<Vertex> vertices)
        {
            Dictionary<int, List<ActiveEdge>> edges = _GetEdgesTable(vertices);
            int yMin = (int)vertices.Min(v => v.Location.Y);
            int yMax = (int)vertices.Max(v => v.Location.Y);
            
            List<ActiveEdge> activeEdges = new List<ActiveEdge>();
            for(int y = yMax; y >= yMin; --y)
            {
                activeEdges.RemoveAll(e => e.YLast == y);
                activeEdges.ForEach(e => e.UpdateX());
                
                if (edges.ContainsKey(y))
                {
                    activeEdges.AddRange(edges[y]);
                }
                activeEdges.Sort((a, b) => a.CurrentX.CompareTo(b.CurrentX));
                
                for(int i = 0; i < activeEdges.Count; i += 2)
                {
                    _DrawLine(paintTools, y, (int)activeEdges[i].CurrentX, (int)activeEdges[i + 1].CurrentX);
                }
            }
        }

        private void _DrawLine(PaintTools paintTools, int y, int begX, int endX)
        {
            for(int x = begX; x <= endX; ++x)
            {
                if(paintTools.Bitmap.IsInside(x, y))
                    paintTools.Bitmap.SetPixel(x, y, _GetColor(x, y));
            }
        }

        private Color _GetColor(int x, int y)
        {
            return _fillingInfo.Texture.GetPixel(x % _fillingInfo.Texture.Width,
                                                 y % _fillingInfo.Texture.Height);
        }
        
        private Dictionary<int, List<ActiveEdge>> _GetEdgesTable(List<Vertex> vertices)
        {
            Dictionary<int, List<ActiveEdge>> edges = new Dictionary<int, List<ActiveEdge>>();

            for (int i = 0; i < vertices.Count; ++i)
            {
                Line edge = new Line(vertices[i].Location,
                                     vertices[(i + 1) % vertices.Count].Location);

                _AddEdge(edge, edges);             
            }
   
            return edges;
        }

        private void _AddEdge(Line edge, Dictionary<int, List<ActiveEdge>> edges)
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
