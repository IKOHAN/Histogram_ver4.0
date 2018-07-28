using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    class HSV:IColor
    {
        float h, s, v;
        public HSV()
        {
        }
        public IColor Create(IColor color)
        {
            if (color is HSV)
                return color;
            else
                return ColorConvert.RGBtoHSV(color);
        }
        public IColor Create(params float[] value)
        {
            return new HSV(Hlim(value[0]), SVlim(value[1]), SVlim(value[2]));
        }
        private HSV(params float[] value)
        {
            h = value[0];
            s = value[1];
            v = value[2];
        }

        public IColor Negate(IColor color)
        {
            float[] re = new float[3];
            re[0] = ( color.GetVal1 + 180f );
            re[1] = ( color.GetVal2 );
            re[2] = ( color.GetVal3 );
            return new HSV().Create(re);
        }

        public float GetVal1
        {
            get => h;
        }
        public float GetVal2
        {
            get => s;
        }
        public float GetVal3
        {
            get => v;
        }
        float Hlim(float val)
        {
            if (val >= -360 && val <= 720)
                if (val >= 0 && val <= 360)
                    return val;
                else
                    return ( ( val < 0 ) ? 360 + val : val - 360 );
            else
                return 0;
        }
        float SVlim(float val)
        {
            if (val >= 0 && val < 100)
                return val;
            else
                return ( val < 0 ) ? 0 : 100;
        }
        public static IColor operator -(HSV c1, IColor c2)
        {
            return new HSV().Create(Math.Abs(c1.GetVal1 - c2.GetVal1), (c1.GetVal2 - c2.GetVal2), Math.Abs(c1.GetVal3 - c2.GetVal3));
        }
    }
}
