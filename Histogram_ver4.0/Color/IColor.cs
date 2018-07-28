using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    interface IColor
    {
        IColor Create(params float[] value);
        IColor Create(IColor color);
        IColor Negate(IColor color);
        float GetVal1
        {
            get;
        }
        float GetVal2
        {
            get;
        }
        float GetVal3
        {
            get;
        }
    }
}
