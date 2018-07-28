using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    class Image_Processing:IObserver
    {
        delegate bool[] Stripe(bool[] map, int w, int h);
        static IColor color;
        public Image_Project Median_Filter(Image_Project img, int size)
        {
            int offset = ( size - 1 ) / 2;
            var imgtmp = img.GetImage.Wider_img(img.GetImage, offset);
            IColor[] rgbwin = new IColor[size * size];
            var res = new Image( img.GetImage.GetState,img.GetImage.Width, img.GetImage.Height);
            try
            {
                for (int y = offset; y < imgtmp.Height - offset; y++)
                {
                    for (int x = offset; x < imgtmp.Width - offset; x++)
                    {
                        for (int i = 0, ibox = -offset; ibox <= offset; i++, ibox++)
                            for (int j = 0, jbox = -offset; jbox <= offset; j++, jbox++)
                            {
                                rgbwin[i * size + j] = imgtmp[ y + ibox, x + jbox].GetColor;
                            }
                        var pos = new Point(x - offset, y - offset);
                        res[ pos.Y,pos.X] = new Pixel(pos, Mediana(rgbwin, size));

                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new Image_Project(res, img.GetState);
        }
        public Image_Project Negate_Filter(Image_Project img)
        {
            var tmp = new Image(img.GetImage);
            Negate(tmp);
            return new Image_Project(tmp, img.GetState);
        }
        void Negate(Image img)
        {
            for (int i = 0; i < img.Height; i++)
                for(int j = 0; j<img.Width;j++)
                img.Data[i,j] = img.Data[i,j].GetNegate();
        }
        IColor Mediana(IColor[] window, int matrsize)
        {
            try
            {
                int size = matrsize * matrsize;
                float[] rr = new float[size];
                float[] gg = new float[size];
                float[] bb = new float[size];
                for (int i = 0; i < size; i++)
                {
                    rr[i] = window[i].GetVal1;
                    gg[i] = window[i].GetVal2;
                    bb[i] = window[i].GetVal3;
                }
                Array.Sort(rr);
                Array.Sort(gg);
                Array.Sort(bb);
                return color.Create(rr[size / 2], gg[size / 2], bb[size / 2]);
            }
            catch (Exception)
            {
                throw new Exception("Метод вернувший ошибку: RGB.Mediana.");
            }
        }
        public void Update(IState state)
        {
            color = state.ColorState;
        }
    }
}
