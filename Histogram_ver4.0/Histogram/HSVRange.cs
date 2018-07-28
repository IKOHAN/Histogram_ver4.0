using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    class HSVRange : IRange
    {
        public int GetFirst
        {
            get => 360;
        }
        public int GetSecond
        {
            get => 101;
        }
        public int GetThird
        {
            get => 101;
        }
    }
}
