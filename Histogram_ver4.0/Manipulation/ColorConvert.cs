using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    class ColorConvert
    {
        public static IColor HSVtoRGB(IColor color)
        {
            if (color is HSV)
            {
                float min, inc, dec;
                int H = ( (int) ( color.GetVal1 / 60f ) ) % 6;
                min = ( ( 100f - color.GetVal2 ) * color.GetVal3 ) / 100f;
                float a = ( color.GetVal3 - min ) * ( ( color.GetVal1 % 60f ) / 60f );
                inc = min + a;
                dec = color.GetVal3 - a;
                float[] tmp = new float[3];
                switch (H)
                {
                    case 0:
                        tmp[0] = color.GetVal3;
                        tmp[1] = inc;
                        tmp[2] = min;
                        break;
                    case 1:
                        tmp[0] = dec;
                        tmp[1] = color.GetVal3;
                        tmp[2] = min;
                        break;
                    case 2:
                        tmp[0] = min;
                        tmp[1] = color.GetVal3;
                        tmp[2] = inc;
                        break;
                    case 3:
                        tmp[0] = min;
                        tmp[1] = dec;
                        tmp[2] = color.GetVal3;
                        break;
                    case 4:
                        tmp[0] = inc;
                        tmp[1] = min;
                        tmp[2] = color.GetVal3;
                        break;
                    case 5:
                        tmp[0] = color.GetVal3;
                        tmp[1] = min;
                        tmp[2] = dec;
                        break;
                }
                tmp[0] *= 2.55f;
                tmp[1] *= 2.55f;
                tmp[2] *= 2.55f;
                return new RGB().Create(tmp);
            }
            else
                if (color is RGB)
                return color;
            else
                throw new IncorrectSchemeInput();
        }
        public static IColor RGBtoHSV(IColor color)
        {
            if (color is RGB)
            {
                float[] tmp = new float[3];
                tmp[0] = color.GetVal1 / 255f;
                tmp[1] = color.GetVal2 / 255f;
                tmp[2] = color.GetVal3 / 255f;
                float[] res = new float[3];
                float max, min;
                max = tmp.Max();
                min = tmp.Min();
                if (max == min)
                    res[0] = 0;
                else
                if (max == tmp[0])
                {
                    res[0] = 60f * (float) ( ( tmp[1] - tmp[2] ) / ( max - min ) );
                    res[0] += ( tmp[1] >= tmp[2] ) ? 0 : 360;
                }
                else
                                if (max == tmp[1])
                    res[0] = 60f * (float) ( ( tmp[2] - tmp[0] ) / ( max - min ) ) + 120;
                else
                    res[0] = 60f * (float) ( ( tmp[0] - tmp[1] ) / ( max - min ) ) + 240;
                res[1] = ( max == 0f ) ? 0f : ( 1f - ( min / max ) ) * 100f;
                res[2] = max * 100f;
                return new HSV().Create(res);
            }
            else
                if (color is HSV)
                return color;
            else
                throw new IncorrectSchemeInput();
        }
        //public static Image_Project Change_Color(Image_Project img)
        //{
        //    IState state;
        //    if (img.GetState is HSVState)
        //    {
        //        state = new RGBState();
        //    }
        //    else
        //    {
        //        if (img.GetState is RGBState)
        //        {
        //            state = new HSVState();
        //        }
        //        else
        //            state = new RGBState();
        //    }
        //    Image res = new Image(state.ColorState,img.GetImage.Width, img.GetImage.Height);
        //    for (int i = 0; i < res.GetLength(); i++)
        //    {
        //        res[i] = new Pixel(img.GetImage[i].GetPos, img.GetImage.GetState.Create(img.GetImage[i].GetColor));
        //    }
        //    return new Image_Project(res, state);

        //}
    }
    class IncorrectSchemeInput : Exception
    {
        public override string Message
        {
            get => "Неопределенная цветовая схема.";
        }
    }
}
