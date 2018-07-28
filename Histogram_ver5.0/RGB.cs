using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver5._0
{
    class RGB : ColorBase
    {
        const float rcoef = 255, gcoef=255, bcoef=255;
        public RGB():base()
        {
        }
        public RGB(params float[] value)
        {

        }
        public RGB(ColorBase color)
        {
                
        }
        public override float GetVal1
        {
            get {
             return   v1* rcoef;
            }
        }
        public override float GetVal2
        {
            get => v2*gcoef;
        }
        public override float GetVal3
        {
            get => v3*bcoef;
        }
    }
}
