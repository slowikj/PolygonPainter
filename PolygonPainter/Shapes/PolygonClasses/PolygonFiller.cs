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
                    _DrawSegment(paintTools, y, (int)activeEdges[i].CurrentX, (int)activeEdges[i + 1].CurrentX);
                }
            }
        }

        private void _DrawSegment(PaintTools paintTools, int y, int begX, int endX)
        {
            for(int x = begX; x <= endX; ++x)
            {
                if (paintTools.Bitmap.IsInside(x, y))
                {
                    paintTools.Bitmap.SetPixel(x, y, _GetColor(x, y));
                }
            }
        }

        private Color _GetColor(int x, int y)
        {
            Color objectColor = _fillingInfo.GetPixelOfTexture(x, y);
            double[] objectColorVector = _GetVector(objectColor);

            Color lightColor = _fillingInfo.GetLightColor();
            double[] lightColorVector = _GetVector(lightColor);

            Color colorFromNormalMap = _fillingInfo.GetPixelOfNormalVectorsMap(x, y);
            double[] N = new double[3] {(double)colorFromNormalMap.R / 127.5 - 1,
                                        (double)colorFromNormalMap.G / 127.5 - 1,
                                        (double)colorFromNormalMap.B / 255};

            //double[] N = new double[3] { 0, 0, 1 };
            N = _Normalized(N);

            double[] L = new double[3] { 421 - x, 300 - y, 100 };
            L = _Normalized(L);

            double cos = N.Zip(L, (a, b) => a * b)
                          .Sum();

            if (cos <= 0)
            {
                return Color.Black;
            }

            double[] res = objectColorVector.Zip(lightColorVector, (a, b) => (a * b * cos / 255))
                                            .ToArray();

            return Color.FromArgb(objectColor.A, (int)res[0], (int)res[1], (int)res[2]);
        }

        private double[] _Normalized(double[] a)
        {
            double l = Math.Sqrt(a.Select(x => x * x).Sum());

            return a.Select(x => x / l).ToArray();
        }

        private double[] _GetVector(Color color)
        {
            double[] res = new double[3] { color.R, color.G, color.B };
            return res;
        }
        
        private Dictionary<int, List<ActiveEdge>> _GetEdgesTable(List<Vertex> vertices)
        {
            Dictionary<int, List<ActiveEdge>> edges = new Dictionary<int, List<ActiveEdge>>();

            for (int i = 0; i < vertices.Count; ++i)
            {
                Segment edge = new Segment(vertices[i].Location,
                                           vertices[(i + 1) % vertices.Count].Location);

                _AddEdge(edge, edges);             
            }
   
            return edges;
        }

        private void _AddEdge(Segment edge, Dictionary<int, List<ActiveEdge>> edges)
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
