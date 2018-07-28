using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    delegate double GetNorma(IColor color);
    interface IStats
    {
        IStats Create(Image_Project img, Mask map, GetNorma getNorma = null);
        Mask GetMap
        {
            get;
            set;
        }
        Image GetImage
        {
            get;
        }
        Histogram GetFirstHist
        {
            get;
        }
        Histogram GetSecondHist
        {
            get;
        }
        Histogram GetThirdHist
        {
            get;
        }
        IColor[] GetCenter
        {
            get;
        }
        float[] Dispersion
        {
            get;
        }
        float[] Average
        {
            get;
        }

        float Volume
        {
            get;
        }
        string ToString();
    }
}
