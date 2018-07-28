using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    class UndefinedNorma : Exception
    {
        public override string Message
        {
            get => "Неопределенная норма.";
        }
    }
}
