using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    /// <summary>
    /// Color component take.
    /// </summary>
    static class CTake
    {
        public static float GetFirst(IColor color)
        {
            return color.GetVal1;
        }
        public static float GetSecond(IColor color)
        {
            return color.GetVal2;
        }
        public static float GetThird(IColor color)
        {
            return color.GetVal3;
        }
    }
}
