using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonPainter
{
    public class OperationImpossibleException : Exception
    {
        public OperationImpossibleException(string message)
            : base(message)
        {
        }

        public override string ToString()
        {
            return "OperationImpossibleException";
        }
    }
}
