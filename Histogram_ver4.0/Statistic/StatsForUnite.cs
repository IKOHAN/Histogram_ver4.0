using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    class StatsForUnite:IStats
    {
        Mask pixmap;
        IColor[] center;
        float volume;
        float[] dispersion;
        float[] average;
        Image image;
        Histogram firstHistogram, secondHistogram, thirdHistogram;
        public StatsForUnite()
        {
        }
        public IStats Create(Image_Project img, Mask map, GetNorma getNorma = null)
        {
            if (getNorma != null)
                return new StatsForUnite(img, map, getNorma);
            else
                throw new UndefinedNorma();
        }
        public StatsForUnite(Image_Project img, Mask map, GetNorma getNorma)
        {
            pixmap = map;
            image = new Image(img.GetImage, map);
            firstHistogram = new Histogram(image, CTake.GetFirst, img.GetState.GetRange.GetFirst);
            secondHistogram = new Histogram(image, CTake.GetSecond, img.GetState.GetRange.GetSecond);
            thirdHistogram = new Histogram(image, CTake.GetThird, img.GetState.GetRange.GetThird);
            double w = img.GetImage.Width;
            double h = img.GetImage.Height;
            average = new float[3];
            dispersion = new float[3];
            average[0] = 0;
            average[1] = 0;
            average[2] = 0;
            dispersion[0] = 0;
            dispersion[1] = 0;
            dispersion[2] = 0;
            for (int i = 0; i < firstHistogram.GetHistogram.Length; i++)
            {
                average[0] += i * firstHistogram.GetHistogram[i];
            }
            for (int i = 0; i < secondHistogram.GetHistogram.Length; i++)
            {
                average[1] += i * secondHistogram.GetHistogram[i];
            }
            for (int i = 0; i < thirdHistogram.GetHistogram.Length; i++)
            {
                average[2] += i * thirdHistogram.GetHistogram[i];
            }
            for (int i = 0; i < firstHistogram.GetHistogram.Length; i++)
            {
                dispersion[0] += ( i - average[0] ) * ( i - average[0] ) * firstHistogram.GetHistogram[i];
            }
            for (int i = 0; i < secondHistogram.GetHistogram.Length; i++)
            {
                dispersion[1] += ( i - average[1] ) * ( i - average[1] ) * secondHistogram.GetHistogram[i];
            }
            for (int i = 0; i < thirdHistogram.GetHistogram.Length; i++)
            {
                dispersion[2] += ( i - average[2] ) * ( i - average[2] ) * thirdHistogram.GetHistogram[i];
            }
            volume = map.Length;
            dispersion[0] =(float) Math.Sqrt(( volume * dispersion[0] ) / ( volume - 1d ));
            dispersion[1] =(float) Math.Sqrt(( volume * dispersion[1] ) / ( volume - 1d ));
            dispersion[2] = (float)Math.Sqrt(( volume * dispersion[2] ) / ( volume - 1d ));
            var tmp = img.GetState.ColorState.Create(average);
            center = CenterSearch(tmp);
        }
        IColor[] CenterSearch(IColor color)
        {
            var res = new IColor[3];
            float[] c0 = new float[3], c2 = new float[3];
            c0[0] = (float) ( color.GetVal1 - 2f * dispersion[0] );
            c0[1] = (float) ( color.GetVal2 - 2f * dispersion[1] );
            c0[2] = (float) ( color.GetVal3 - 2f * dispersion[2] );

            c2[0] = (float) ( color.GetVal1 + 2f * dispersion[0] );
            c2[1] = (float) ( color.GetVal2 + 2f * dispersion[1] );
            c2[2] = (float) ( color.GetVal3 + 2f * dispersion[2] );
            res[0] = color.Create(c0);
            res[1] = color;
            res[2] = color.Create(c2);
            return res;
        }
        IColor[] CenterSearch(IColor color,Image_Project img)
        {
            var res = new IColor[3];
            float[] c1 = new float[3], c2 = new float[3], c3 = new float[3];
            if (img.GetState.ColorState == new HSV())
            {
                for (int i = 0; i < img.GetFirstLayer.GetClust.Count; i++)
                {
                    if (Clust_Assign(color.GetVal1, img.GetFirstLayer.GetClust[i], new HSVRange()))
                    {
                        c1[0] = img.GetFirstLayer.GetClust[i].GetLeftBord;
                        c2[0] = color.GetVal1;
                        c3[0] = img.GetFirstLayer.GetClust[i].GetRightBord;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < img.GetFirstLayer.GetClust.Count; i++)
                {
                    if (Clust_Assign(color.GetVal1, img.GetFirstLayer.GetClust[i]))
                    {
                        c1[0] = img.GetFirstLayer.GetClust[i].GetLeftBord;
                        c2[0] = color.GetVal1;
                        c3[0] = img.GetFirstLayer.GetClust[i].GetRightBord;
                        break;
                    }
                }
            }
            for (int i = 0; i < img.GetSecondLayer.GetClust.Count; i++)
            {
                if (Clust_Assign(color.GetVal2, img.GetSecondLayer.GetClust[i]))
                {
                    c1[1] = img.GetSecondLayer.GetClust[i].GetLeftBord;
                    c2[1] = color.GetVal2;
                    c3[1] = img.GetSecondLayer.GetClust[i].GetRightBord;
                    break;
                }
            }
            for (int i = 0; i < img.GetThirdLayer.GetClust.Count; i++)
            {
                if (Clust_Assign(color.GetVal3, img.GetThirdLayer.GetClust[i]))
                {
                    c1[2] = img.GetThirdLayer.GetClust[i].GetLeftBord;
                    c2[2] = color.GetVal3;
                    c3[2] = img.GetThirdLayer.GetClust[i].GetRightBord;
                    break;
                }
            }
            res[0] = img.GetState.ColorState.Create(c1);
            res[1] = img.GetState.ColorState.Create(c2);
            res[2] = img.GetState.ColorState.Create(c3);
            return res;
        }
        bool Clust_Assign(float d, Histogram_Part clust,IRange range)
        {
            if (clust.GetLeftBord > clust.GetRightBord)
            {
                return ( d >= clust.GetLeftBord && d <= range.GetFirst ) || ( d >= 0 && d <= clust.GetRightBord );
            }
            else
            {
                return ( d >= clust.GetLeftBord && d <= clust.GetRightBord );
            }
        }
        bool Clust_Assign(float d, Histogram_Part clust)
        {

            return ( d >= clust.GetLeftBord && d <= clust.GetRightBord );
        }

        public Mask GetMap
        {
            get => pixmap;
            set => pixmap=value;
        }
        public Image GetImage
        {
            get => image;
        }
        public Histogram GetFirstHist
        {
            get => firstHistogram;
        }
        public Histogram GetSecondHist
        {
            get => secondHistogram;
        }
        public Histogram GetThirdHist
        {
            get => thirdHistogram;
        }
        public IColor[] GetCenter
        {
            get => center;
        }
        public float[] Dispersion
        {
            get => dispersion;
        }
        public float[] Average
        {
            get => average;
        }
        public float Volume
        {
            get => volume;
        }
    }
}
