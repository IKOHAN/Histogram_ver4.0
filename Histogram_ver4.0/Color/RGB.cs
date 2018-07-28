using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    class RGB:IColor
    {
        float r, g, b;
        public RGB()
        {

        }
        public IColor Create(IColor color)
        {
            if (color is RGB)
                return color;
            else
                return ColorConvert.HSVtoRGB(color);
        }
        public IColor Create(params float[] value)
        {
            return new RGB(RGBlim(value[0]), RGBlim(value[1]), RGBlim(value[2]));
        }
        private RGB(params float[] value)
        {
            r = value[0];
            g = value[1];
            b = value[2];
        }
        public IColor Negate(IColor color)
        {
            float[] re = new float[3];
            re[0] = ( 255f - color.GetVal1 );
            re[1] = ( 255f - color.GetVal2 );
            re[2] = ( 255f - color.GetVal3 );
            return new RGB().Create(re);
        }
        public float GetVal1
        {
            get => r;
        }
        public float GetVal2
        {
            get => g;
        }
        public float GetVal3
        {
            get => b;
        }
        float RGBlim(float val)
        {
            if (val >= 0 && val <= 255)
                return val;
            else
                return ( val < 0 ) ? 0 : 255f;
        }
        public static IColor operator -(RGB c1, IColor c2)
        {
            return new RGB().Create(Math.Abs(c1.GetVal1 - c2.GetVal1),Math.Abs( c1.GetVal2 - c2.GetVal2), Math.Abs(c1.GetVal3 - c2.GetVal3));
        }
        //public static IColor operator +(RGB c1, IColor c2)
        //{
        //    return new RGB().Create(c1.GetVal1 + c2.GetVal1, c1.GetVal2 + c2.GetVal2, c1.GetVal3 + c2.GetVal3);
        //}
        //public static IColor operator /(RGB c, float l)
        //{
        //    return new RGB().Create(c.GetVal1 / l, c.GetVal2 / l, c.GetVal3 / l);
        //}
    }
}
