using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Histogram_ver4._0
{
    class RGBOutput: IOutputState
    {
        public IColor GenColor(IColor color)
        {
            if (color is RGB)
                return color;
            else
                throw new IncorrectSchemeInput();
        }
        public IColor FirstColor(IColor color)
        {
            return new RGB().Create(color.GetVal1, 0, 0);
        }

        public IColor SecondColor(IColor color)
        {
            return new RGB().Create(0, color.GetVal2, 0);
        }

        public IColor ThirdColor(IColor color)
        {
            return new RGB().Create(0, 0, color.GetVal3);
        }

        public LinearGradientBrush FirstHist(Histogram_Part clust = null)
        {
            var myBrush = new LinearGradientBrush(Colors.Black, Colors.Red, new Point(0, 0), new Point(1, 0));
            if (clust != null)
            {
                float left = clust.GetLeftBord / 255f;
                float right = clust.GetRightBord / 255f;
                var grad = new GradientStopCollection {
                    new GradientStop(Colors.White, 0.0),
                    new GradientStop(Colors.White, left),
                    new GradientStop(Color.FromScRgb(1,  left,0,0), left),
                    new GradientStop(Color.FromScRgb(1,  right,0, 0), right),
                    new GradientStop(Colors.White, right),
                    new GradientStop(Colors.White, 1.0)
                };
                myBrush = new LinearGradientBrush(grad, 0.0);
            }
            return myBrush;
        }

        public LinearGradientBrush SecondHist(Histogram_Part clust = null)
        {
            var myBrush = new LinearGradientBrush(Colors.Black, Colors.Green, new Point(0, 0), new Point(1, 0));
            if (clust != null)
            {
                float left = clust.GetLeftBord / 255f;
                float right = clust.GetRightBord / 255f;
                var grad = new GradientStopCollection {
                    new GradientStop(Colors.White, 0.0),
                    new GradientStop(Colors.White, left),
                    new GradientStop(Color.FromScRgb(1, 0, left, 0), left),
                    new GradientStop(Color.FromScRgb(1, 0, right, 0), right),
                    new GradientStop(Colors.White, right),
                    new GradientStop(Colors.White, 1.0)
                };
                myBrush = new LinearGradientBrush(grad, 0.0);
            }
            return myBrush;
        }

        public LinearGradientBrush ThirdHist(Histogram_Part clust = null)
        {
            var myBrush = new LinearGradientBrush(Colors.Black, Colors.Blue, new Point(0, 0), new Point(1, 0));
            if (clust != null)
            {
                float left = clust.GetLeftBord / 255f;
                float right = clust.GetRightBord / 255f;
                var grad = new GradientStopCollection {
                    new GradientStop(Colors.White, 0.0),
                    new GradientStop(Colors.White, left),
                    new GradientStop(Color.FromScRgb(1, 0,0, left), left),
                    new GradientStop(Color.FromScRgb(1, 0, 0,right), right),
                    new GradientStop(Colors.White, right),
                    new GradientStop(Colors.White, 1.0)
                };
                myBrush = new LinearGradientBrush(grad, 0.0);
            }
            return myBrush;
        }
    }
}
