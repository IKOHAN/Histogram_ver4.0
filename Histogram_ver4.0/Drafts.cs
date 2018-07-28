using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{

    #region Image

    //public Image(Pixel[] img, int w, int h)
    //{
    //    width = w;
    //    height = h;
    //    data = img;
    //}
    //#endregion
    //#region Image_Project
    //public Image_Project(Image_Project img, bool[] map, int w, int h)
    //{
    //    Update(img.GetState);
    //    image = new Image(img.GetImage, map, w, h);
    //    Analize_image(map);
    //}

    //#endregion
    //#region Histogram
    //private Histogram(IImage img, GetComponent getHist, int range, bool[] map)
    //{
    //    histogram = RelateFrequnce(ImageCounter(img, getHist, range, map), Img_length(map));
    //}

    #endregion
    #region
    //public Mask(int w, int h)
    //{
    //    width = w;
    //    height = h;
    //    map = new bool[h, w];
    //    for (int i = 0; i < map.Length; i++)
    //    {
    //        int r = i / width;
    //        int c = i % width;
    //        map[r, c] = true;
    //    }
    //    length = w * h;
    //}


    //public Mask(bool[,] mask, int w, int h)
    //{
    //    map = mask;
    //    //length = MaskSize();
    //    width = w;
    //    height = h;
    //}
    #endregion

    #region
    //public Histogram(float[] imghist, int img_length)
    //{
    //    histogram = RelateFrequnce(imghist, img_length);
    //}
    #endregion
    #region
    //public CLayer(Image img, float[] hist, GetComponent colorComponent, ICLayerState state)
    //{
    //    List<int[]> clustBase = state.ClusterBorders(hist);
    //    clusters = new List<Histogram_Part>();
    //    for (int i = 0; i < clustBase.Count; i++)
    //    {
    //        clusters.Add(new Histogram_Part(clustBase[i], img.Width, img.Height));
    //    }
    //    getCComponent = colorComponent;
    //    this.state = state;
    //    state.ClusterSearch(img, colorComponent, clusters);
    //}
    #endregion
    #region
    //class DistribPack : Pack, IPack
    //{
    //    const double C1 = 7;
    //    double volume_limit;
    //    double histDistanceLimit;
    //    double procentAverDiff_Limit;
    //    double[] baseHistWidth_Limit;
    //    double distanceCenter_Limit;
    //    Distance distance;
    //    GetNorma norm;
    //    List<IStats> pack;

    //    bool Hist_distance(IStats s1, IStats s2)
    //    {
    //        bool b1 = Hist_distance(s1.GetFirstHist, s2.GetFirstHist);
    //        bool b2 = Hist_distance(s1.GetSecondHist, s2.GetSecondHist);
    //        bool b3 = Hist_distance(s1.GetThirdHist, s2.GetThirdHist);
    //        return b1 && b2 && b3;
    //    }
    //    bool Hist_distance(Histogram h1, Histogram h2)
    //    {
    //        double sum = 0;
    //        for (int i = 0; i < h1.GetHistogram.Length; i++)
    //        {
    //            double tmp = ( h1.GetHistogram[i] - h2.GetHistogram[i] );
    //            sum += tmp * tmp;
    //        }
    //        sum = Math.Sqrt(sum);
    //        return sum <= histDistanceLimit;
    //    }
    //    bool Statistic_test(IStats s1, IStats s2)
    //    {
    //        bool first = Statistic_test(s1.Dispersion[0], s2.Dispersion[0], s1.Volume, s2.Volume, s1.Average[0], s2.Average[0]);
    //        bool second = Statistic_test(s1.Dispersion[1], s2.Dispersion[1], s1.Volume, s2.Volume, s1.Average[1], s2.Average[1]);
    //        bool third = Statistic_test(s1.Dispersion[2], s2.Dispersion[2], s1.Volume, s2.Volume, s1.Average[2], s2.Average[2]);
    //        return first && second && third;
    //    }
    //    bool Statistic_test(double s1, double s2, double l1, double l2, double a1, double a2)
    //    {
    //        if (s1 == s2 && a1 == a2)
    //        {
    //            return true;
    //        }
    //        if (s1 == 0 || s2 == 0)
    //        {
    //            return a1 == a2;
    //        }
    //        else
    //        {
    //            double disp1;
    //            double disp2;
    //            int len1;
    //            int len2;
    //            double av1;
    //            double av2;
    //            if (s1 >= s2)
    //            {
    //                disp1 = s1;
    //                disp2 = s2;
    //                len1 = (int) l1;
    //                len2 = (int) l2;
    //                av1 = a1;
    //                av2 = a2;
    //            }
    //            else
    //            {
    //                disp1 = s2;
    //                disp2 = s1;
    //                len1 = (int) l2;
    //                len2 = (int) l1;
    //                av1 = a2;
    //                av2 = a1;
    //            }

    //            double kvantF = alglib.invfdistribution(len1, len2, 0.975);
    //            double kvantT;
    //            double denum;
    //            double nummult;
    //            double mu;
    //            if (disp1 / disp2 <= kvantF)
    //            {
    //                mu = len1 + len2 - 2;
    //                kvantT = alglib.invstudenttdistribution((int) mu, 0.975);
    //                denum = Math.Sqrt(disp1 * disp1 / len1 + disp2 * disp2 / len2);
    //                nummult = 1d;
    //            }
    //            else
    //            {
    //                mu = ( disp1 * disp1 / len1 + disp2 * disp2 / len2 ) * ( disp1 * disp1 / len1 + disp2 * disp2 / len2 ) * ( 1d / ( ( ( 1d / ( len1 - 1d ) ) * ( disp1 * disp1 / len1 ) * ( disp1 * disp1 / len1 ) ) + ( ( 1d / ( len2 - 1d ) ) * ( disp2 * disp2 / len2 ) * ( disp2 * disp2 / len2 ) ) ) );
    //                kvantT = alglib.invstudenttdistribution((int) mu, 0.975);
    //                denum = Math.Sqrt(( ( len1 - 1 ) * disp1 + ( len2 - 1 ) * disp2 ) / ( len1 + len2 - 2 ));
    //                nummult = Math.Sqrt(( (double) len1 * (double) len2 ) / ( len1 + len2 ));

    //            }
    //            double t = ( Math.Abs(av1 - av2) / denum ) * nummult;
    //            if (t <= kvantT)
    //                return true;
    //            else
    //                return false;
    //            //if (disp1 / disp2 <= kvantF)
    //            //{
    //            //    double kvantT = alglib.invstudenttdistribution(len1 + len2 - 2, 0.975);
    //            //    double denum = Math.Sqrt(disp1 * disp1 + disp2 * disp2);
    //            //    double num = Math.Abs(av1 - av2) * Math.Sqrt(len1 + len2);
    //            //    if (num / denum <= kvantT)
    //            //        return true;
    //            //    else
    //            //        return false;
    //            //}
    //            //else
    //            //{
    //            //    return false;
    //            //}


    //        }
    //    }





    //    public DistribPack()
    //    {

    //    }
    //    public IPack Create(Image_Project img)
    //    {
    //        return new DistribPack(img);
    //    }
    //    public DistribPack(Image_Project img)
    //    {
    //        distance = ND;
    //        norm = Evcklid;
    //        volume_limit = 0.005;
    //        histDistanceLimit = 0.45;
    //        pack = new List<IStats>();
    //        List<IColor[]> prevcenters = CentersLoad(img);
    //        var tmplist = GetClustMask(img, prevcenters);
    //        for (int i = 0; i < tmplist.Count; i++)
    //        {
    //            pack.Add(new StatsForDistrib(img, tmplist[i], norm));
    //        }
    //        pack = UniteSame(img, pack);
    //    }
    //    public IPack Create(Image_Project img, Parts parts)
    //    {
    //        return new DistribPack(img, parts);
    //    }
    //    public DistribPack(Image_Project img, Parts parts)
    //    {
    //        distance = DN;
    //        norm = Evcklid;
    //        volume_limit = 0.015;
    //        var size = new Point(img.GetImage.Width, img.GetImage.Height);
    //        pack = new List<IStats>();
    //        int row = parts.Row;
    //        int column = parts.Column;
    //        IPack[,] tmppack = new IPack[row, column];
    //        for (int i = 0; i < row; i++)
    //        {
    //            for (int j = 0; j < column; j++)
    //            {
    //                tmppack[i, j] = new DistribPack().Create(parts[i, j]);
    //            }
    //        }
    //    }
    //    List<IStats> UniteSame(Image_Project img, List<IStats> clust)
    //    {
    //        var res = clust;
    //        for (int i = 0; i < res.Count - 1; i++)
    //        {
    //            var s1 = res[i] as StatsForDistrib;
    //            for (int j = i + 1; j < res.Count; j++)
    //            {
    //                var s2 = res[j] as StatsForDistrib;
    //                if (i == 1 && j == 4)
    //                {
    //                    var log = new Logger(s1, s2);
    //                }
    //                // if(DispersionLimit(s1)&&DispersionLimit(s2))
    //                if (Statistic_test(s1, s2))//||Hist_distance(s1,s2))
    //                {
    //                    res.Add(new StatsForDistrib().Create(img, UniteMaps(s1.GetMap, s2.GetMap), norm));
    //                    res.RemoveAt(j);
    //                    res.RemoveAt(i);
    //                    i = -1;
    //                    break;
    //                }
    //            }
    //        }
    //        return res;
    //    }

    //    #region ????????????????????????????????????
    //    List<Mask> GetClustMask(Image_Project img, List<IColor[]> center)
    //    {
    //        var empty = new List<bool>();
    //        var res = new List<Mask>();
    //        int w = img.GetImage.Width;
    //        int h = img.GetImage.Height;
    //        int numb_clust = 0;
    //        float dist = int.MaxValue;
    //        for (int i = 0; i < center.Count; i++)
    //        {
    //            empty.Add(true);
    //            res.Add(new Mask(w, h, false));
    //        }
    //        for (int i = 0; i < h; i++)
    //        {
    //            for (int j = 0; j < w; j++)
    //            {
    //                Pixel tmp = img.GetImage[i, j];
    //                numb_clust = -1;
    //                dist = int.MaxValue;
    //                for (int n = 0; n < center.Count; n++)
    //                {
    //                    //if (Clust_assignment(tmp.GetColor, center[n]))
    //                    //    numb_clust = n;
    //                    float tmpd = (float) distance(tmp.GetColor, center[n][1], norm);
    //                    if (tmpd < dist)
    //                    {
    //                        dist = tmpd;
    //                        numb_clust = n;
    //                    }
    //                }
    //                res[numb_clust][i, j] = true;
    //                empty[numb_clust] = false;
    //            }
    //        }
    //        var clustvol = new List<int>();
    //        for (int i = 0; i < res.Count; i++)
    //            clustvol.Add(res[i].Length);
    //        EmptyCleaner(empty, center, clustvol, res);
    //        SmallClear(res, center, clustvol, img.GetImage.GetLength(), img.GetState.GetRange);
    //        return res;
    //    }
    //    #endregion
    //    bool Clust_assignment(IColor tmp, IColor[] clust)
    //    {
    //        bool l1, l2, l3;
    //        if (clust[0].GetVal1 > clust[2].GetVal1)
    //            l1 = ( tmp.GetVal1 >= clust[0].GetVal1 ) || ( tmp.GetVal1 <= clust[2].GetVal1 );
    //        else
    //            l1 = ( tmp.GetVal1 >= clust[0].GetVal1 && tmp.GetVal1 <= clust[2].GetVal1 );
    //        l2 = ( tmp.GetVal2 >= clust[0].GetVal2 && tmp.GetVal2 <= clust[2].GetVal2 );
    //        l3 = ( tmp.GetVal3 >= clust[0].GetVal3 && tmp.GetVal3 <= clust[2].GetVal3 );
    //        return l1 && l2 && l3;
    //    }
    //    void SmallClear(List<Mask> maps, List<IColor[]> center, List<int> clustv, int length, IRange range)
    //    {
    //        for (int i = 0; i < maps.Count; i++)
    //        {
    //            if (( (double) clustv[i] / (double) length ) < volume_limit)
    //            {
    //                int tm = SearchNeighbor(i, center, range);
    //                Mask tmp = UniteMaps(maps[i], maps[tm]);
    //                int tmpl = ( tm < i ) ? tm : i;
    //                int tmpr = ( tm >= i ) ? tm : i;
    //                maps.Add(tmp);
    //                maps.RemoveAt(tmpr);
    //                maps.RemoveAt(tmpl);
    //                clustv.Add(clustv[tm] + clustv[i]);
    //                clustv.RemoveAt(tmpr);
    //                clustv.RemoveAt(tmpl);
    //                center.Add(CentersUnite(center[i], center[tm], range));
    //                center.RemoveAt(tmpr);
    //                center.RemoveAt(tmpl);
    //                i = -1;
    //            }
    //        }
    //    }
    //    int SearchNeighbor(int indx, List<IColor[]> centers, IRange range)
    //    {
    //        float dist = 765;
    //        float sum = 0;
    //        int res = -1;
    //        IColor tmp = centers[indx][1];
    //        for (int i = 0; i < centers.Count; i++)
    //        {
    //            sum = 0;
    //            if (range.GetFirst == 256)
    //            {
    //                sum += Math.Abs(tmp.GetVal1 - centers[i][1].GetVal1);
    //            }
    //            else
    //            {
    //                float max = ( tmp.GetVal1 > centers[i][1].GetVal1 ) ? tmp.GetVal1 : centers[i][1].GetVal1;
    //                float min = ( tmp.GetVal1 < centers[i][1].GetVal1 ) ? tmp.GetVal1 : centers[i][1].GetVal1;
    //                sum += ( range.GetFirst - max + min < max - min ) ? range.GetFirst - max + min : max - min;
    //            }
    //            sum += Math.Abs(tmp.GetVal2 - centers[i][1].GetVal2);
    //            sum += Math.Abs(tmp.GetVal3 - centers[i][1].GetVal3);
    //            if (sum > 0 && sum < dist)
    //            {
    //                dist = sum;
    //                res = i;
    //            }
    //        }
    //        return res;

    //    }
    //    IColor[] CentersUnite(IColor[] s1, IColor[] s2, IRange range)
    //    {
    //        IColor state = s1[0];
    //        float leftb1 = ( s1[0].GetVal1 < s2[0].GetVal1 ) ? s1[0].GetVal1 : s2[0].GetVal1;
    //        float leftb2 = ( s1[0].GetVal2 < s2[0].GetVal2 ) ? s1[0].GetVal2 : s2[0].GetVal2;
    //        float leftb3 = ( s1[0].GetVal3 < s2[0].GetVal3 ) ? s1[0].GetVal3 : s2[0].GetVal3;
    //        float rightb1 = ( s1[2].GetVal1 > s2[2].GetVal1 ) ? s1[2].GetVal1 : s2[2].GetVal1;
    //        float rightb2 = ( s1[2].GetVal2 > s2[2].GetVal2 ) ? s1[2].GetVal2 : s2[2].GetVal2;
    //        float rightb3 = ( s1[2].GetVal3 > s2[2].GetVal3 ) ? s1[2].GetVal3 : s2[2].GetVal3;
    //        float cent1;
    //        if (range.GetFirst == 256)
    //            cent1 = ( rightb1 + leftb1 ) / 2;
    //        else
    //            cent1 = ( range.GetFirst - leftb1 + rightb1 < rightb1 - leftb1 ) ? ( range.GetFirst + rightb1 + leftb1 ) % range.GetFirst : ( rightb1 + leftb1 ) / 2;
    //        float cent2 = ( rightb2 + leftb2 ) / 2;
    //        float cent3 = ( rightb3 + leftb3 ) / 2;
    //        return new IColor[] { state.Create(leftb1, cent1, rightb1), state.Create(leftb2, cent2, rightb2), state.Create(leftb3, cent3, rightb3) };
    //    }
    //    void SmallClear(List<Mask> maps, List<IColor> center, List<int> clustv, int length)
    //    {
    //        for (int i = 0; i < maps.Count; i++)
    //        {
    //            if (( (double) clustv[i] / (double) length ) < volume_limit)
    //            {
    //                int tm = NearestCenter(i, center, DN);////////
    //                Mask tmp = UniteMaps(maps[i], maps[tm]);
    //                int tmpl = ( tm < i ) ? tm : i;
    //                int tmpr = ( tm >= i ) ? tm : i;
    //                maps.Add(tmp);
    //                maps.RemoveAt(tmpr);
    //                maps.RemoveAt(tmpl);
    //                clustv.Add(clustv[tm] + clustv[i]);
    //                clustv.RemoveAt(tmpr);
    //                clustv.RemoveAt(tmpl);
    //                center.Add(center[tm]);
    //                center.RemoveAt(tmpr);
    //                center.RemoveAt(tmpl);
    //                i = -1;
    //            }
    //        }
    //    }
    //    void EmptyCleaner(List<bool> empty, List<IColor[]> center, List<int> clustv, List<Mask> maps)
    //    {
    //        for (int i = 0; i < empty.Count; i++)
    //        {
    //            if (empty[i])
    //            {
    //                maps.RemoveAt(i);
    //                clustv.RemoveAt(i);
    //                center.RemoveAt(i);
    //                empty.RemoveAt(i);
    //                i--;
    //            }
    //        }
    //    }
    //    void EmptyCleaner(List<bool> empty, List<IColor> center, List<int> clustv, List<Mask> maps)
    //    {
    //        for (int i = 0; i < empty.Count; i++)
    //        {
    //            if (empty[i])
    //            {
    //                maps.RemoveAt(i);
    //                clustv.RemoveAt(i);
    //                center.RemoveAt(i);
    //                empty.RemoveAt(i);
    //                i--;
    //            }
    //        }
    //    }
    //    List<IColor[]> CentersLoad(Image_Project img)
    //    {
    //        var res = new List<IColor[]>();
    //        for (int i = 0; i < img.GetFirstLayer.GetClust.Count; i++)
    //        {
    //            int first_med;
    //            //first_med = img.GetFirstLayer.GetClust[i].GetPos;
    //            int lb1 = img.GetFirstLayer.GetClust[i].GetLeftBord;
    //            int rb1 = img.GetFirstLayer.GetClust[i].GetRightBord;
    //            first_med = ( lb1 > rb1 ) ? ( lb1 + 360 + rb1 ) / 2 % 360 : ( lb1 + rb1 ) / 2;
    //            for (int j = 0; j < img.GetSecondLayer.GetClust.Count; j++)
    //            {
    //                int second_med;
    //                int lb2 = img.GetSecondLayer.GetClust[j].GetLeftBord;
    //                int rb2 = img.GetSecondLayer.GetClust[j].GetRightBord;
    //                //second_med = img.GetSecondLayer.GetClust[j].GetPos;
    //                second_med = ( img.GetSecondLayer.GetClust[j].GetLeftBord + img.GetSecondLayer.GetClust[j].GetRightBord ) / 2;
    //                for (int n = 0; n < img.GetThirdLayer.GetClust.Count; n++)
    //                {
    //                    int third_med;
    //                    int lb3 = img.GetThirdLayer.GetClust[n].GetLeftBord;
    //                    int rb3 = img.GetThirdLayer.GetClust[n].GetRightBord;
    //                    //third_med = img.GetThirdLayer.GetClust[n].GetPos;
    //                    third_med = ( img.GetThirdLayer.GetClust[n].GetLeftBord + img.GetThirdLayer.GetClust[n].GetRightBord ) / 2;
    //                    res.Add(new IColor[] { img.GetImage.GetState.Create(lb1, lb2, lb3), img.GetImage.GetState.Create(first_med, second_med, third_med), img.GetImage.GetState.Create(rb1, rb2, rb3) });
    //                }
    //            }
    //        }
    //        return res;
    //    }
    //    public List<IStats> GetClusters
    //    {
    //        get => pack;
    //    }
    //    public IStats this[int x]
    //    {
    //        get => pack[x];
    //    }
    //}
    #endregion
    #region MyRegion
    //class StatsForDistrib : IStats
    //{
    //    Mask pixmap;
    //    IColor center;
    //    float volume;
    //    float[] dispersion;
    //    float[] average;
    //    Image image;
    //    Histogram firstHistogram, secondHistogram, thirdHistogram;
    //    public StatsForDistrib()
    //    {
    //    }
    //    public IStats Create(Image_Project img, Mask map, GetNorma getNorma = null)
    //    {
    //        if (getNorma != null)
    //            return new StatsForDistrib(img, map, getNorma);
    //        else
    //            throw new UndefinedNorma();
    //    }
    //    public StatsForDistrib(Image_Project img, Mask map, GetNorma getNorma)
    //    {
    //        pixmap = map;
    //        image = new Image(img.GetImage, map, img.GetImage.Width, img.GetImage.Height);
    //        firstHistogram = new Histogram(image, CTake.GetFirst, img.GetState.GetRange.GetFirst);
    //        secondHistogram = new Histogram(image, CTake.GetSecond, img.GetState.GetRange.GetSecond);
    //        thirdHistogram = new Histogram(image, CTake.GetThird, img.GetState.GetRange.GetThird);
    //        double w = img.GetImage.Width;
    //        double h = img.GetImage.Height;
    //        average = new float[3];
    //        dispersion = new float[3];
    //        float[] color = new float[3];
    //        average[0] = 0;
    //        average[1] = 0;
    //        average[2] = 0;
    //        dispersion[0] = 0;
    //        dispersion[1] = 0;
    //        dispersion[2] = 0;
    //        color[0] = 0;
    //        color[1] = 0;
    //        color[2] = 0;
    //        for (int i = 0; i < map.Height; i++)
    //        {
    //            for (int j = 0; j < map.Width; j++)
    //                if (map[i, j])
    //                {
    //                    color[0] += img.GetImage[i, j].GetColor.GetVal1;
    //                    color[1] += img.GetImage[i, j].GetColor.GetVal2;
    //                    color[2] += img.GetImage[i, j].GetColor.GetVal3;
    //                }
    //        }
    //        for (int i = 0; i < firstHistogram.GetHistogram.Length; i++)
    //        {
    //            average[0] += i * firstHistogram.GetHistogram[i];
    //        }
    //        for (int i = 0; i < secondHistogram.GetHistogram.Length; i++)
    //        {
    //            average[1] += i * secondHistogram.GetHistogram[i];
    //        }
    //        for (int i = 0; i < thirdHistogram.GetHistogram.Length; i++)
    //        {
    //            average[2] += i * thirdHistogram.GetHistogram[i];
    //        }
    //        for (int i = 0; i < firstHistogram.GetHistogram.Length; i++)
    //        {
    //            dispersion[0] += ( i - average[0] ) * ( i - average[0] ) * firstHistogram.GetHistogram[i];
    //        }
    //        for (int i = 0; i < secondHistogram.GetHistogram.Length; i++)
    //        {
    //            dispersion[1] += ( i - average[1] ) * ( i - average[1] ) * secondHistogram.GetHistogram[i];
    //        }
    //        for (int i = 0; i < thirdHistogram.GetHistogram.Length; i++)
    //        {
    //            dispersion[2] += ( i - average[2] ) * ( i - average[2] ) * thirdHistogram.GetHistogram[i];
    //        }
    //        volume = map.Length;
    //        color[0] /= volume;
    //        color[1] /= volume;
    //        color[2] /= volume;
    //        dispersion[0] = (float) Math.Sqrt(( volume * dispersion[0] ) / ( volume - 1d ));
    //        dispersion[1] = (float) Math.Sqrt(( volume * dispersion[1] ) / ( volume - 1d ));
    //        dispersion[2] = (float) Math.Sqrt(( volume * dispersion[2] ) / ( volume - 1d ));
    //        center = img.GetState.ColorState.Create(color);
    //    }
    //    public Mask GetMap
    //    {
    //        get => pixmap;
    //        set => pixmap = value;
    //    }
    //    public float Volume
    //    {
    //        get => volume;
    //    }
    //    public override string ToString()
    //    {
    //        return string.Format("First channel : dispersion {0}/average{1} \n Second channel : dispersion {2}/average{3} \n Third channel : dispersion {4}/average{5}", dispersion[0], average[0], dispersion[1], average[1], dispersion[2], average[2]);
    //    }

    //    public Image GetImage
    //    {
    //        get => image;
    //    }
    //    public Histogram GetFirstHist
    //    {
    //        get => firstHistogram;
    //    }
    //    public Histogram GetSecondHist
    //    {
    //        get => secondHistogram;
    //    }
    //    public Histogram GetThirdHist
    //    {
    //        get => thirdHistogram;
    //    }
    //    public IColor Center
    //    {
    //        get => center;
    //    }
    //    public IColor[] GetCenter
    //    {
    //        get => throw new NotImplementedException();
    //    }
    //    public float[] Dispersion
    //    {
    //        get => dispersion;
    //    }
    //    public float[] Average
    //    {
    //        get => average;
    //    }
    //}
    #endregion
}
