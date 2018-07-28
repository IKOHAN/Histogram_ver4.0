using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Histogram_ver4._0
{
    class HSVOutput:IOutputState
    {
        public IColor GenColor(IColor color)
        {
            if (color is HSV)
                return ColorConvert.HSVtoRGB(color);
            else
                throw new IncorrectSchemeInput();
        }
        public IColor FirstColor(IColor color)
        {
            int H = (int) ( color.GetVal1 / 60 ) % 6;
            float min, inc, dec;
            min = 0;
            inc = 100 * ( ( color.GetVal1 % 60 ) / 60 ) * 2.55f;
            dec = ( 100 - inc ) * 2.55f;
            return GetColor(H, 100, min, inc, dec);
        }

        public IColor SecondColor(IColor color)
        {
            int H = (int) ( color.GetVal1 / 60 ) % 6;
            float min, inc, dec;
            min = ( 100 - color.GetVal2 ) * 2.55f;
            inc = ( ( 100 - color.GetVal2 ) + color.GetVal2 * ( color.GetVal1 % 60 ) / 60 ) * 2.55f;
            dec = ( 100 - color.GetVal2 * color.GetVal1 % 60 / 60 ) * 2.55f;
            return GetColor(H, 100, min, inc, dec);
        }

        public IColor ThirdColor(IColor color)
        {
            int H = (int) ( color.GetVal1 / 60 ) % 6;
            float min, inc, dec;
            min = 0;
            inc = ( color.GetVal3 * ( color.GetVal1 % 60 ) / 60 ) * 2.55f;
            dec = ( color.GetVal3 - inc ) * 2.55f;
            return GetColor(H, color.GetVal3, min, inc, dec);
        }
        IColor GetColor(int H, float V, float min, float inc, float dec)
        {
            V *= 2.55f;
            switch (H)
            {
                case 0:
                    return new RGB().Create(V, inc, min);
                case 1:
                    return new RGB().Create(dec, V, min);
                case 2:
                    return new RGB().Create(min, V, inc);
                case 3:
                    return new RGB().Create(min, dec, V);
                case 4:
                    return new RGB().Create(inc, min, V);
                case 5:
                    return new RGB().Create(V, min, dec);
                default:
                    return new RGB().Create(0, 0, 0);

            }
        }

        public LinearGradientBrush FirstHist(Histogram_Part clust = null)
        {
            var grad = new GradientStopCollection {
                new GradientStop(Colors.Red, 0.0),
                new GradientStop(Colors.Yellow, 1d / 6d),
                new GradientStop(Colors.Green, 2d / 6d),
                new GradientStop(Colors.Cyan, 3d / 6d),
                new GradientStop(Colors.Blue, 4d / 6d),
                new GradientStop(Colors.Magenta, 5d / 6d),
                new GradientStop(Colors.Red, 1.0)
            };
            if (clust != null)
            {
                float left = clust.GetLeftBord / 360f;
                float right = clust.GetRightBord / 360f;
                IColor tmpl = FirstColor(new HSV().Create(clust.GetLeftBord, 100, 100));
                IColor tmpr = FirstColor(new HSV().Create(clust.GetRightBord, 100, 100));
                grad = new GradientStopCollection();
                if (clust.GetLeftBord > clust.GetRightBord)
                    grad.Add(new GradientStop(Colors.Red, 0.0));
                else
                {
                    grad.Add(new GradientStop(Colors.White, 0.0));
                }
                grad.Add(new GradientStop(Colors.White, left));
                grad.Add(new GradientStop(Color.FromArgb(255, (byte) tmpl.GetVal1, (byte) tmpl.GetVal2, (byte) tmpl.GetVal3), left));

                if (1f / 6f > left && 1f / 6f < right)
                    grad.Add(new GradientStop(Colors.Yellow, 1d / 6d));
                if (2f / 6f > left && 2f / 6f < right)
                    grad.Add(new GradientStop(Colors.Green, 2d / 6d));
                if (3f / 6f > left && 3f / 6f < right)
                    grad.Add(new GradientStop(Colors.Cyan, 3d / 6d));
                if (4f / 6f > left && 4f / 6f < right)
                    grad.Add(new GradientStop(Colors.Blue, 4d / 6d));
                if (5f / 6f > left && 5f / 6f < right)
                    grad.Add(new GradientStop(Colors.Magenta, 5d / 6d));
                grad.Add(new GradientStop(Color.FromArgb(255, (byte) tmpr.GetVal1, (byte) tmpr.GetVal2, (byte) tmpr.GetVal3), right));
                grad.Add(new GradientStop(Colors.White, right));
                if (clust.GetLeftBord > clust.GetRightBord)
                    grad.Add(new GradientStop(Colors.Red, 1.0));
                else
                {
                    grad.Add(new GradientStop(Colors.White, 1.0));
                }
            }
            var myBrush = new LinearGradientBrush(grad, 0.0);
            return myBrush;
        }

        public LinearGradientBrush SecondHist(Histogram_Part clust = null)
        {
            var grad = new GradientStopCollection {
                new GradientStop(Colors.White,0.0),
                new GradientStop(Colors.Red,1.0)
            };
            if (clust != null)
            {
                float left = clust.GetLeftBord / 100f;
                float right = clust.GetRightBord / 100f;
                IColor tmpl = SecondColor(new HSV().Create(0, clust.GetLeftBord, 100));
                IColor tmpr = SecondColor(new HSV().Create(0, clust.GetRightBord, 100));
                grad = new GradientStopCollection {
                    new GradientStop(Colors.White, 0.0),
                    new GradientStop(Colors.White, left),
                    new GradientStop(Color.FromArgb(255, (byte) tmpl.GetVal1, (byte) tmpl.GetVal2, (byte) tmpl.GetVal3), left),
                    new GradientStop(Color.FromArgb(255, (byte) tmpr.GetVal1, (byte) tmpr.GetVal2, (byte) tmpr.GetVal3), right),
                    new GradientStop(Colors.White, right),
                    new GradientStop(Colors.White, 1.0)
                };
            }
            var myBrush = new LinearGradientBrush(grad, 0.0);
            return myBrush;
        }


        public LinearGradientBrush ThirdHist(Histogram_Part clust = null)
        {
            var grad = new GradientStopCollection {
                new GradientStop(Colors.Black, 0.0),
                new GradientStop(Colors.Red, 1.0)
            };
            if (clust != null)
            {
                float left = clust.GetLeftBord / 100f;
                float right = clust.GetRightBord / 100f;
                IColor tmpl = ThirdColor(new HSV().Create(0, 100, clust.GetLeftBord));
                IColor tmpr = ThirdColor(new HSV().Create(0, 100, clust.GetRightBord));
                grad = new GradientStopCollection {
                    new GradientStop(Colors.White, 0.0),
                    new GradientStop(Colors.White, left),
                    new GradientStop(Color.FromArgb(255, (byte) tmpl.GetVal1, (byte) tmpl.GetVal2, (byte) tmpl.GetVal3), left),
                    new GradientStop(Color.FromArgb(255, (byte) tmpr.GetVal1, (byte) tmpr.GetVal2, (byte) tmpr.GetVal3), right),
                    new GradientStop(Colors.White, right),
                    new GradientStop(Colors.White, 1.0)
                };
            }
            var myBrush = new LinearGradientBrush(grad, 0.0);
            return myBrush;
        }
    }
}
