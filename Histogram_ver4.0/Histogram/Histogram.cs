using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    delegate float GetComponent(IColor color);
    class Histogram
    {
        float[] histogram;
        public Histogram()
        {
        }
        public Histogram(Image img, GetComponent getHist, int range)
        {
            histogram = RelateFrequnce(ImageCounter(img, getHist, range), img.GetMap.Length);
        }

        float[] ImageCounter(Image img, GetComponent getHist, int range)
        {
            float[] res = new float[range];
                for (int i = 0; i < img.Height; i++)
                {
                for(int j = 0; j <img.Width;j++)
                    if (img.GetMap[i,j])
                        res[(int) getHist(img[i,j].GetColor)]++;
                }
            return res;
        }
        float[] RelateFrequnce(float[] imghist, int img_length)
        {
            float[] res = new float[imghist.Length];
            for (int i = 0; i < imghist.Length; i++)
            {
                res[i] = imghist[i] / img_length;
            }
            return res;
        }
        public float[] GetHistogram
        {
            get => histogram;
        }
        public double[] Scale()
        {
            double[] res = new double[histogram.Length];
            double tmp = histogram.Max();
            for (int i = 0; i < histogram.Length; i++)
            {
                res[i] = histogram[i] * 100d;
            }
            return res;
        }
        //double[] SmoothHistogram(double[] origVal)
        //{
        //    double[] res = new double[origVal.Length];
        //    double[] mask = new double[] { 0.125, 0.75, 0.125 };
        //    res[0] = ( mask[0] + mask[1] ) * origVal[0] + mask[2] * origVal[1];
        //    for (int i = 1; i < origVal.Length - 1; i++)
        //    {
        //        res[i] = origVal[i - 1] * mask[0] + origVal[i] * mask[1] + origVal[i + 1] * mask[2];
        //    }
        //    res[res.Length - 1] = ( mask[2] + mask[1] ) * origVal[origVal.Length - 1] + mask[0] * origVal[origVal.Length - 2];
        //    return res;
        //}
    }
}
