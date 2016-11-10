using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonPainter
{
    public struct SegmentIntersectionInfo
    {
        private PointD? _point;
        private IntersectionType _type;

        public PointD? Point
        {
            get
            {
                return _point;
            }
        }

        public IntersectionType Type
        {
            get
            {
                return _type;
            }
        }

        public SegmentIntersectionInfo(PointD? point, IntersectionType type)
        {
            _point = point;
            _type = type;
        }
    }
}
