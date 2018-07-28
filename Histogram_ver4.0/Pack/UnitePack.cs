using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Histogram_ver4._0
{
    delegate bool LimiT(IStats s1, IStats s2);
    class UnitePack : Pack, IPack, IObserver
    {
        static IState state;
        const double C1 = 7;
        double volume_limit;
        double histDistanceLimit;
        double procentAverDiff_Limit;
        float[] baseHistWidth_Limit;
        double distanceCenter_Limit;
        Distance distance;
        GetNorma norm;
        List<IStats> pack;
        public List<IStats> GetClusters
        {
            get => pack;
        }
        public IStats this[int x]
        {
            get => pack[x];
        }
        #region Tests 
        bool Hist_distance_Test(IStats s1, IStats s2)
        {
            bool b1 = Hist_distance_Test(s1.GetFirstHist, s2.GetFirstHist);
            bool b2 = Hist_distance_Test(s1.GetSecondHist, s2.GetSecondHist);
            bool b3 = Hist_distance_Test(s1.GetThirdHist, s2.GetThirdHist);
            return b1 && b2 && b3;
        }
        bool Hist_distance_Test(Histogram h1, Histogram h2)
        {
            double sum = 0;
            for (int i = 0; i < h1.GetHistogram.Length; i++)
            {
                double tmp = ( h1.GetHistogram[i] - h2.GetHistogram[i] );
                sum += tmp * tmp;
            }
            sum = Math.Sqrt(sum);
            return sum <= histDistanceLimit;
        }
        bool Statistic_test(IStats s1, IStats s2)
        {
            bool first = Statistic_test(s1.Dispersion[0], s2.Dispersion[0], s1.Volume, s2.Volume, s1.Average[0], s2.Average[0]);
            bool second = Statistic_test(s1.Dispersion[1], s2.Dispersion[1], s1.Volume, s2.Volume, s1.Average[1], s2.Average[1]);
            bool third = Statistic_test(s1.Dispersion[2], s2.Dispersion[2], s1.Volume, s2.Volume, s1.Average[2], s2.Average[2]);
            return first && second && third;
        }
        bool Statistic_test(double s1, double s2, double l1, double l2, double a1, double a2)
        {
            if (s1 == s2 && a1 == a2)
            {
                return true;
            }
            if (s1 == 0 || s2 == 0)
            {
                return a1 == a2;
            }
            else
            {
                double disp1;
                double disp2;
                int len1;
                int len2;
                double av1;
                double av2;
                if (s1 >= s2)
                {
                    disp1 = s1;
                    disp2 = s2;
                    len1 = (int) l1;
                    len2 = (int) l2;
                    av1 = a1;
                    av2 = a2;
                }
                else
                {
                    disp1 = s2;
                    disp2 = s1;
                    len1 = (int) l2;
                    len2 = (int) l1;
                    av1 = a2;
                    av2 = a1;
                }

                double kvantF = alglib.invfdistribution(len1, len2, 0.975);
                double kvantT;
                double denum;
                double nummult;
                double mu;
                if (disp1 / disp2 <= kvantF)
                {
                    mu = len1 + len2 - 2;
                    kvantT = alglib.invstudenttdistribution((int) mu, 0.975);
                    denum = Math.Sqrt(disp1 * disp1 / len1 + disp2 * disp2 / len2);
                    nummult = 1d;
                }
                else
                {
                    mu = ( disp1 * disp1 / len1 + disp2 * disp2 / len2 ) * ( disp1 * disp1 / len1 + disp2 * disp2 / len2 ) * ( 1d / ( ( ( 1d / ( len1 - 1d ) ) * ( disp1 * disp1 / len1 ) * ( disp1 * disp1 / len1 ) ) + ( ( 1d / ( len2 - 1d ) ) * ( disp2 * disp2 / len2 ) * ( disp2 * disp2 / len2 ) ) ) );
                    kvantT = alglib.invstudenttdistribution((int) mu, 0.975);
                    denum = Math.Sqrt(( ( len1 - 1 ) * disp1 + ( len2 - 1 ) * disp2 ) / ( len1 + len2 - 2 ));
                    nummult = Math.Sqrt(( (double) len1 * (double) len2 ) / ( len1 + len2 ));

                }
                double t = ( Math.Abs(av1 - av2) / denum ) * nummult;
                if (t <= kvantT)
                    return true;
                else
                    return false;
                #region
                //if (disp1 / disp2 <= kvantF)
                //{
                //    double kvantT = alglib.invstudenttdistribution(len1 + len2 - 2, 0.975);
                //    double denum = Math.Sqrt(disp1 * disp1 + disp2 * disp2);
                //    double num = Math.Abs(av1 - av2) * Math.Sqrt(len1 + len2);
                //    if (num / denum <= kvantT)
                //        return true;
                //    else
                //        return false;
                //}
                //else
                //{
                //    return false;
                //}
                #endregion

            }
        }
        bool BaseHistogramCrossing_Test(IStats s1, IStats s2)
        {
            bool f0 = BaseHistogramCrossing_Test(s1.GetCenter[0].GetVal1, s2.GetCenter[0].GetVal1, s1.GetCenter[2].GetVal1, s2.GetCenter[2].GetVal1, baseHistWidth_Limit[0], true);
            bool f1 = BaseHistogramCrossing_Test(s1.GetCenter[0].GetVal2, s2.GetCenter[0].GetVal2, s1.GetCenter[2].GetVal2, s2.GetCenter[2].GetVal2, baseHistWidth_Limit[1]);
            bool f2 = BaseHistogramCrossing_Test(s1.GetCenter[0].GetVal3, s2.GetCenter[0].GetVal3, s1.GetCenter[2].GetVal3, s2.GetCenter[2].GetVal3, baseHistWidth_Limit[2]);
            return f0 && f1 && f2;
        }
        bool BaseHistogramCrossing_Test(float lb1, float lb2, float rb1, float rb2, float limit, bool circle = true)
        {
            float dist1, dist2, cross;
            if (state.ColorState == new HSV())
            {
                float max1 = ( rb1 > lb1 ) ? rb1 : lb1;
                float min1 = ( rb1 < lb1 ) ? rb1 : lb1;
                float max2 = ( rb2 > lb2 ) ? rb2 : lb2;
                float min2 = ( rb2 < lb2 ) ? rb2 : lb2;
                dist1 = Math.Min(new HSVRange().GetFirst - max1 + min1, max1 - min1);
                dist2 = Math.Min(new HSVRange().GetFirst - max2 + min2, max2 - min2);
                float tmp1 = Math.Min(new HSVRange().GetFirst - max1 + min2, max1 - min2);// ( max1 - min2 > 0 ) ? max1 - min2 : new HSVRange().GetFirst);
                float tmp2 = Math.Min(new HSVRange().GetFirst - max2 + min1, max2 - min2);//( max2 - min1 > 0 ) ? max2 - min2 : new HSVRange().GetFirst);
                float tmp = Math.Min(tmp1, tmp2);
                float tmmp = Math.Min(dist1, dist2);
                cross = Math.Min(tmp, tmmp);
                if (cross < 0)
                    return false;
            }
            else
            {
                if (lb1 < rb1 && lb2 < rb2)
                {
                    dist1 = rb1 - lb1;
                    dist2 = rb2 - lb2;
                    cross = ( ( rb1 < rb2 ) ? rb1 : rb2 ) - ( ( lb1 > lb2 ) ? lb1 : lb2 );
                }
                else
                    return false;
            }

            return ( cross / dist1 <= limit ) && ( cross / dist2 <= limit );
        }
        bool BaseHistogramCrossing_Test(float lb1, float lb2, float rb1, float rb2, float limit)
        {
            float dist1, dist2, cross;

            if (lb1 < rb1 && lb2 < rb2)
            {
                dist1 = rb1 - lb1;
                dist2 = rb2 - lb2;
                cross = ( ( rb1 < rb2 ) ? rb1 : rb2 ) - ( ( lb1 > lb2 ) ? lb1 : lb2 );
            }
            else
                return false;

            return ( cross / dist1 <= limit ) && ( cross / dist2 <= limit );
        }
        bool AverageDiff_Test(IStats s1, IStats s2)
        {
            bool f0 = AverageDiff_Test(s1.Average[0], s2.Average[0]);
            bool f1 = AverageDiff_Test(s1.Average[1], s2.Average[1]);
            bool f2 = AverageDiff_Test(s1.Average[2], s2.Average[2]);
            return f0 && f1 && f2;
        }
        bool AverageDiff_Test(float av1, float av2)
        {
            float tmp = ( av1 > av2 ) ? av1 - av2 : av2 - av1;
            float perc1 = tmp / av1;
            float perc2 = tmp / av2;
            return ( perc1 <= procentAverDiff_Limit ) && ( perc2 <= procentAverDiff_Limit );
        }
        bool DistanceCenter_Test(IStats s1, IStats s2)
        {
            bool f0 = DistanceCenter_Test(s1.GetCenter[1], s2.GetCenter[1]);
            return f0;

        }
        bool DistanceCenter_Test(IColor c1, IColor c2)
        {
            return distance(c1, c2, norm) <= distanceCenter_Limit;
        }
        #endregion
        public UnitePack()
        {
        }
        public IPack Create(Image_Project img)
        {
            return new UnitePack(img);
        }
        public UnitePack(Image_Project img)
        {
            distance = ND;
            norm = Evcklid;
            volume_limit = 0.005;
            histDistanceLimit = 0.45;
            //на медузе ок остальное так себе
            procentAverDiff_Limit = 0.55;
            //?
            baseHistWidth_Limit = new float[] { 1f - 0.2126f, 1f - 0.7152f, 1f - 0.0722f };
            distanceCenter_Limit = 50;
            //50 для медузы песня
            pack = new List<IStats>();
            List<IColor[]> prevcenters = CentersLoad(img);
            var tmplist = GetClustMask(img, prevcenters);
            for (int i = 0; i < tmplist.Count; i++)
            {
                pack.Add(new StatsForUnite(img, tmplist[i], norm));
            }
            pack = UniteSame(img, pack);
        }
        public IPack Create(Image_Project img, Parts parts)
        {
            return new UnitePack(img, parts);
        }
        public UnitePack(Image_Project img, Parts parts)
        {
            distance = ND;
            norm = Evcklid;
            volume_limit = 0.015;
            histDistanceLimit = 0.35;
            //на медузе ок остальное так себе
            procentAverDiff_Limit = 0.35;
            //?
            baseHistWidth_Limit = new float[] { 1f - 0.2126f, 1f - 0.7152f, 1f - 0.0722f };
            distanceCenter_Limit = 35;
            //50 для медузы песня


            var size = new Point(img.GetImage.Height,img.GetImage.Width);
            pack = new List<IStats>();
            int row = parts.Row;
            int column = parts.Column;
            List<StatsForUnite>[,] tmppack = new List<StatsForUnite>[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    List<IColor[]> prevcenters = CentersLoad(parts[i, j]);
                    var tmpmask = GetClustMask(img, parts[i, j], prevcenters);
                    tmppack[i,j] = new List<StatsForUnite>();
                    for (int n = 0; n < tmpmask.Count; n++)
                    {
                        tmppack[i, j].Add(new StatsForUnite(parts[i, j], tmpmask[n], norm));
                    }
                }
            }
            var mask_array = new List<Mask>();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    for (int n = 0; n < tmppack[i, j].Count; n++)
                    {
                        Mask tmp = SameSearch(n, tmppack, parts, new Point(i, j), size);
                        AddOrUnite(mask_array, tmp);
                    }
                }
            }
            for (int i = 0; i < mask_array.Count; i++)
            {
                pack.Add(new StatsForUnite().Create(img, mask_array[i], norm));
            }

        }
        #region MyRegion

        List<IColor[]> CentersLoad(Image_Project img)
        {
            var res = new List<IColor[]>();
            for (int i = 0; i < img.GetFirstLayer.GetClust.Count; i++)
            {
                int first_med;
                //first_med = img.GetFirstLayer.GetClust[i].GetPos;
                int lb1 = img.GetFirstLayer.GetClust[i].GetLeftBord;
                int rb1 = img.GetFirstLayer.GetClust[i].GetRightBord;
                first_med = ( lb1 > rb1 ) ? ( lb1 + 360 + rb1 ) / 2 % 360 : ( lb1 + rb1 ) / 2;
                for (int j = 0; j < img.GetSecondLayer.GetClust.Count; j++)
                {
                    int second_med;
                    int lb2 = img.GetSecondLayer.GetClust[j].GetLeftBord;
                    int rb2 = img.GetSecondLayer.GetClust[j].GetRightBord;
                    //second_med = img.GetSecondLayer.GetClust[j].GetPos;
                    second_med = ( img.GetSecondLayer.GetClust[j].GetLeftBord + img.GetSecondLayer.GetClust[j].GetRightBord ) / 2;
                    for (int n = 0; n < img.GetThirdLayer.GetClust.Count; n++)
                    {
                        int third_med;
                        int lb3 = img.GetThirdLayer.GetClust[n].GetLeftBord;
                        int rb3 = img.GetThirdLayer.GetClust[n].GetRightBord;
                        //third_med = img.GetThirdLayer.GetClust[n].GetPos;
                        third_med = ( img.GetThirdLayer.GetClust[n].GetLeftBord + img.GetThirdLayer.GetClust[n].GetRightBord ) / 2;
                        res.Add(new IColor[] { img.GetImage.GetState.Create(lb1, lb2, lb3), img.GetImage.GetState.Create(first_med, second_med, third_med), img.GetImage.GetState.Create(rb1, rb2, rb3) });
                    }
                }
            }
            return res;
        }
        List<IStats> UniteSame(Image_Project img, List<IStats> clust)
        {
            var res = clust;
            for (int i = 0; i < res.Count - 1; i++)
            {
                var s1 = res[i] as StatsForUnite;
                for (int j = i + 1; j < res.Count; j++)
                {
                    var s2 = res[j] as StatsForUnite;
                    //if (i == 1 && j == 4)
                    //{
                    //    var log = new Logger(s1, s2);
                    //}
                    //if (Statistic_test(s1, s2))
                    //if(Hist_distance_Test(s1,s2))
                    //if (AverageDiff_Test(s1, s2))
                    //if(BaseHistogramCrossing_Test(s1,s2))
                    if (DistanceCenter_Test(s1, s2))
                    {
                        res.Add(new StatsForUnite().Create(img, UniteMaps(s1.GetMap, s2.GetMap), norm));
                        res.RemoveAt(j);
                        res.RemoveAt(i);
                        i = -1;
                        break;
                    }
                }
            }
            return res;
        }
        List<Mask> GetClustMask(Image_Project img, List<IColor[]> center)
        {
            var empty = new List<bool>();
            var res = new List<Mask>();
            int w = img.GetImage.Width;
            int h = img.GetImage.Height;
            int numb_clust = 0;
            Point wdir = img.GetImage.GetMap.GetWBound;
            Point hdir = img.GetImage.GetMap.GetHBound;
            float dist = int.MaxValue;
            for (int i = 0; i < center.Count; i++)
            {
                empty.Add(true);
                res.Add(new Mask(w, h, wdir, hdir, false));
            }
            for (int i = hdir.X; i < hdir.Y; i++)
            {
                for (int j = wdir.X; j < wdir.Y; j++)
                {
                    Pixel tmp = img.GetImage[i-hdir.X, j - wdir.X];
                    numb_clust = -1;
                    dist = int.MaxValue;
                    for (int n = 0; n < center.Count; n++)
                    {
                        if (Clust_assignment(tmp.GetColor, center[n]))
                            numb_clust = n;
                        //float tmpd = (float) distance(tmp.GetColor, center[n][1], norm);
                        //if (tmpd < dist)
                        //{
                        //    dist = tmpd;
                        //    numb_clust = n;
                        //}
                    }
                    try
                    {
                        res[numb_clust][i - hdir.X, j - wdir.X] = true;
                        empty[numb_clust] = false;
                    }
                    catch (IndexOutOfRangeException) { throw new Exception(); }
                }
            }
            var clustvol = new List<int>();
            for (int i = 0; i < res.Count; i++)
                clustvol.Add(res[i].Length);
            EmptyCleaner(empty, center, clustvol, res);
            SmallClear(res, center, clustvol, img.GetImage.GetLength(), img.GetState.GetRange);
            return res;
        }
        List<Mask> GetClustMask(Image_Project img, Image_Project part, List<IColor[]> center)
        {
            var empty = new List<bool>();
            var res = new List<Mask>();
            int w = img.GetImage.Width;
            int h = img.GetImage.Height;
            int numb_clust = 0;
            Point wdir = part.GetImage.GetMap.GetWBound;
            Point hdir = part.GetImage.GetMap.GetHBound;
            float dist = int.MaxValue;
            for (int i = 0; i < center.Count; i++)
            {
                empty.Add(true);
                res.Add(new Mask(w, h, wdir, hdir, false));
            }
            for (int i = hdir.X; i < hdir.Y; i++)
            {
                for (int j = wdir.X; j < wdir.Y; j++)
                {
                    Pixel tmp = img.GetImage[i, j];
                    numb_clust = -1;
                    dist = int.MaxValue;
                    for (int n = 0; n < center.Count; n++)
                    {
                        if (Clust_assignment(tmp.GetColor, center[n]))
                        {
                            numb_clust = n;
                            break;
                        }
                        //float tmpd = (float) distance(tmp.GetColor, center[n][1], norm);
                        //if (tmpd < dist)
                        //{
                        //    dist = tmpd;
                        //    numb_clust = n;
                        //}
                    }
                    try
                    {
                        res[numb_clust][i, j] = true;
                        empty[numb_clust] = false;
                    }
                    catch (IndexOutOfRangeException) { throw new Exception(); }
                }
            }
            var clustvol = new List<int>();
            for (int i = 0; i < res.Count; i++)
                clustvol.Add(res[i].Length);
            EmptyCleaner(empty, center, clustvol, res);
            SmallClear(res, center, clustvol, img.GetImage.GetLength(), img.GetState.GetRange);
            return res;
        }
        bool Clust_assignment(IColor tmp, IColor[] clust)
        {
            bool l1, l2, l3;
            if (state.GetRange is HSVRange)
            {
                if (clust[0].GetVal1 > clust[2].GetVal1)
                    l1 = ( tmp.GetVal1 >= clust[0].GetVal1 ) || ( tmp.GetVal1 <= clust[2].GetVal1 );
                else
                    l1 = ( tmp.GetVal1 >= clust[0].GetVal1 && tmp.GetVal1 <= clust[2].GetVal1 );
            }
            else
            {
                l1 = ( tmp.GetVal1 >= clust[0].GetVal1 && tmp.GetVal1 <= clust[2].GetVal1 );
            }
            l2 = ( tmp.GetVal2 >= clust[0].GetVal2 && tmp.GetVal2 <= clust[2].GetVal2 );
            l3 = ( tmp.GetVal3 >= clust[0].GetVal3 && tmp.GetVal3 <= clust[2].GetVal3 );
            return l1 && l2 && l3;
        }

        void SmallClear(List<Mask> maps, List<IColor[]> center, List<int> clustv, int length, IRange range)
        {
            for (int i = 0; i < maps.Count; i++)
            {
                if (( (double) clustv[i] / (double) length ) < volume_limit)
                {
                    int tm = SearchNeighbor(i, center, range);
                    Mask tmp = UniteMaps(maps[i], maps[tm]);
                    int tmpl = ( tm < i ) ? tm : i;
                    int tmpr = ( tm >= i ) ? tm : i;
                    maps.Add(tmp);
                    maps.RemoveAt(tmpr);
                    maps.RemoveAt(tmpl);
                    clustv.Add(clustv[tm] + clustv[i]);
                    clustv.RemoveAt(tmpr);
                    clustv.RemoveAt(tmpl);
                    center.Add(CentersUnite(center[i], center[tm]));
                    center.RemoveAt(tmpr);
                    center.RemoveAt(tmpl);
                    i = -1;
                }
            }
        }
        int SearchNeighbor(int indx, List<IColor[]> centers, IRange range)
        {
            float dist = 765;
            float sum = 0;
            int res = -1;
            IColor tmp = centers[indx][1];
            for (int i = 0; i < centers.Count; i++)
            {
                sum = 0;
                if (range is RGBRange)
                {
                    sum += Math.Abs(tmp.GetVal1 - centers[i][1].GetVal1);
                }
                else
                {
                    float max = ( tmp.GetVal1 > centers[i][1].GetVal1 ) ? tmp.GetVal1 : centers[i][1].GetVal1;
                    float min = ( tmp.GetVal1 < centers[i][1].GetVal1 ) ? tmp.GetVal1 : centers[i][1].GetVal1;
                    sum += ( range.GetFirst - max + min < max - min ) ? range.GetFirst - max + min : max - min;
                }
                sum += Math.Abs(tmp.GetVal2 - centers[i][1].GetVal2);
                sum += Math.Abs(tmp.GetVal3 - centers[i][1].GetVal3);
                if (sum >= 0 && sum <= dist)
                {
                    dist = sum;
                    res = i;
                }
            }
            return res;

        }
        IColor[] CentersUnite(IColor[] s1, IColor[] s2)
        {
            float leftb1 = ( s1[0].GetVal1 <= s2[0].GetVal1 ) ? s1[0].GetVal1 : s2[0].GetVal1;
            float leftb2 = ( s1[0].GetVal2 <= s2[0].GetVal2 ) ? s1[0].GetVal2 : s2[0].GetVal2;
            float leftb3 = ( s1[0].GetVal3 <= s2[0].GetVal3 ) ? s1[0].GetVal3 : s2[0].GetVal3;
            float rightb1 = ( s1[2].GetVal1 >= s2[2].GetVal1 ) ? s1[2].GetVal1 : s2[2].GetVal1;
            float rightb2 = ( s1[2].GetVal2 >= s2[2].GetVal2 ) ? s1[2].GetVal2 : s2[2].GetVal2;
            float rightb3 = ( s1[2].GetVal3 >= s2[2].GetVal3 ) ? s1[2].GetVal3 : s2[2].GetVal3;
            float cent1;
            if (state.ColorState == new RGB())
                cent1 = ( rightb1 + leftb1 ) / 2;
            else
                cent1 = ( state.GetRange.GetFirst - leftb1 + rightb1 < rightb1 - leftb1 ) ? ( ( state.GetRange.GetFirst + rightb1 + leftb1 ) % state.GetRange.GetFirst ) / 2 : ( rightb1 + leftb1 ) / 2;
            float cent2 = ( rightb2 + leftb2 ) / 2;
            float cent3 = ( rightb3 + leftb3 ) / 2;
            return new IColor[] { state.ColorState.Create(leftb1, cent1, rightb1), state.ColorState.Create(leftb2, cent2, rightb2), state.ColorState.Create(leftb3, cent3, rightb3) };
        }
        void EmptyCleaner(List<bool> empty, List<IColor[]> center, List<int> clustv, List<Mask> maps)
        {
            for (int i = 0; i < empty.Count; i++)
            {
                if (empty[i])
                {
                    maps.RemoveAt(i);
                    clustv.RemoveAt(i);
                    center.RemoveAt(i);
                    empty.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Update(IState state)
        {
            UnitePack.state = state;
        }
        #endregion



        void AddOrUnite(List<Mask> clusters, Mask newmask)
        {
            bool flag = true;
            if (clusters.Count != 0)
            {
                for (int i = 0; i < clusters.Count; i++)
                {
                    if (MapsCrossing(newmask, clusters[i]))
                    {
                        Mask tmp = UniteMaps(clusters[i], newmask);
                        clusters.RemoveAt(i);
                        clusters.Add(tmp);
                        flag = false;
                    }
                }
                if (flag)
                    clusters.Add(newmask);
            }
            else
            {
                clusters.Add(newmask);
            }
        }
        Mask SameSearch(int LoOkOnIndx, List<StatsForUnite>[,] Packs, Parts parts, Point LoOkOnPos, Point size)
        {
            int column = parts.Column;
            int row = parts.Row;
            int indx_r = LoOkOnPos.X;
            int indx_c = LoOkOnPos.Y;
            var OnUnite = new List<Mask>();
            Image_Project LoOkOnParts = parts[indx_r, indx_c];
            IStats LoOkOnClust = Packs[indx_r, indx_c][LoOkOnIndx];
            OnUnite.Add(Parttoentire(LoOkOnParts.GetImage, LoOkOnClust.GetMap, size.X, size.Y));
            List<int> right = new List<int>(), down = new List<int>(), left = new List<int>(), up = new List<int>();
            if (indx_c + 1 < column)
                right = GetMatch(LoOkOnClust, Packs[indx_r, indx_c + 1], RightStripe, LeftStripe, LoOkOnParts, parts[indx_r, indx_c + 1]);

            if (right.Count != 0)
                for (int i = 0; i < right.Count; i++)
                    OnUnite.Add(Parttoentire(parts[indx_r, indx_c + 1].GetImage, Packs[indx_r, indx_c + 1][right[i]].GetMap, size.X, size.Y));

            if (indx_r + 1 < row)
                down = GetMatch(LoOkOnClust, Packs[indx_r + 1, indx_c], DownStripe, UpStripe, LoOkOnParts, parts[indx_r + 1, indx_c]);

            if (down.Count != 0)
                for (int i = 0; i < down.Count; i++)
                    OnUnite.Add(Parttoentire(parts[indx_r + 1, indx_c].GetImage, Packs[indx_r + 1, indx_c][down[i]].GetMap, size.X, size.Y));
            if (OnUnite.Count == 1)
                return OnUnite[0];
            else
                return UniteMaps(OnUnite);
        }
        List<int> GetMatch(IStats LoOkOnClust, List<StatsForUnite> neighborClust, Stripe stripeLoOkOn, Stripe neighbor, Image_Project LoOk, Image_Project neighborr)
        {
            var res = new List<int>();
            var s1 = LoOkOnClust;
            for (int j = 0; j < neighborClust.Count; j++)
            {
                var s2 = neighborClust[j];
                //if (i == 1 && j == 4)
                //{
                //    var log = new Logger(s1, s2);
                //}
                //if (Statistic_test(s1, s2))
                //if(Hist_distance_Test(s1,s2))
                //if (AverageDiff_Test(s1, s2))
                //if(BaseHistogramCrossing_Test(s1,s2))
                if (DistanceCenter_Test(s1, s2))
                {
                    if (Unite(stripeLoOkOn(s1.GetMap), neighbor(s2.GetMap)))
                    {
                        res.Add(j);

                    }
                }
            }
            return res;
        }
        bool Unite(bool[] map1, bool[] map2)
        {

            for (int i = 0; i < map1.Length; i++)
            {
                bool m2l = ( i == 0 ) ? false : map2[i - 1];
                bool m2c = map2[i];
                bool m2r = ( i == map2.Length - 1 ) ? false : map2[i + 1];
                if (map1[i] && ( m2c || m2l || m2r ))
                    return true;
            }
            return false;
        }
    }
}
