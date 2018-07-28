using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    class OutOfRange:Exception
    {
        public override string Message
        {
            get => "Попытка доступа к данным за границей маски.";
        }
    }
    class Except
    {
    }
}
