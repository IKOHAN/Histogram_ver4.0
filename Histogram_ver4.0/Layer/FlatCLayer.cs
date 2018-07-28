using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    class FlatCLayer:LapFlat,ICLayerState
    {
        delegate List<int> Filter(List<int> peaks, float[] source);
        event Filter OnFiltering;
        readonly double coef;
        int e;
        const int C1 = 7;
        public ICLayerState Create(double coef)
        {
            return new FlatCLayer(coef);
        }
        public FlatCLayer()
        {
        }
        public FlatCLayer(double coef)
        {
            this.coef = coef;
            //OnFiltering += Original;
            OnFiltering += Triangle_Limit_Filter;
            OnFiltering += Tan_sign_Filter;
            OnFiltering += Moveable_Window_Filter;
            OnFiltering += Pristavka_Filter;
        }
        public List<int[]> ClusterBorders(float[] source)
        {
            e = source.Length / 15;
            var res = new List<int[]>();
            int peakcount = 0;
            List<int> Peaks = PeakSearch(source);
            peakcount = Peaks.Count;
            Peaks = PeakDoppleClear(Peaks, source);
            peakcount = Peaks.Count;
            int startbound = 0;
            int lastbound;
            for (int i = 0; i < Peaks.Count - 1; i++)
            {
                lastbound = Peaks[i] + HollowSearch(ArrForHollow(Peaks[i], Peaks[i + 1], source));
                res.Add(new int[] { startbound, Peaks[i], lastbound });
                startbound = lastbound;
            }
            lastbound = source.Length - 1;
            res.Add(new int[] { startbound, Peaks.Last(), lastbound });
            return res;
        }
        public List<int> PeakSearch(float[] source)
        {
            return PeakPristavka(source);
        }
        List<int> PeakPristavka(float[] source)
        {
            var tmpPeak = new List<int>();
            if (source[0] > source[1])
                tmpPeak.Add(0);
            for (int i = 1; i < source.Length - 1; i++)
            {
                if (source[i] >= source[i - 1] && source[i] >= source[i + 1])
                    tmpPeak.Add(i);
            }
            if (source[source.Length - 1] > source[source.Length - 2])
                tmpPeak.Add(source.Length - 1);
            tmpPeak.Sort();
            return tmpPeak;
        }
        //List<int> PeakHillClimb(float[] source)
        //{
        //    var res = new List<int>();
        //    int point_numb = 20;
        //    int tmp = 0;
        //    int step = source.Length / point_numb;
        //    for (int i = 0; i < point_numb + 1; i++)
        //    {
        //        tmp = i * step;
        //        int hillPos = Hill_climb(tmp, source);
        //        if (hillPos != -1 && !res.Contains(hillPos))
        //            res.Add(hillPos);
        //    }
        //    res.Sort();
        //    return res;
        //}
        //int Hill_climb(int pos, float[] arr)
        //{
        //    int last_pos = pos;
        //    int current_pos = pos;
        //    double current_value = arr[pos];
        //    bool flag_ = true;
        //    for (int i = current_pos - e; i <= current_pos + e; i++)
        //    {
        //        int truepos;
        //        if (i >= 0 && i < arr.Length)
        //            truepos = i;
        //        else
        //            continue;
        //        if (arr[truepos] >= current_value)
        //        {
        //            flag_ = false;
        //            if (arr[truepos] > current_value)
        //            {
        //                current_pos = truepos;
        //                current_value = arr[truepos];
        //            }
        //        }
        //    }
        //    if (flag_ && current_pos == last_pos)
        //        return -1;
        //    return current_pos;
        //}
        public List<int> PeakDoppleClear(List<int> res, float[] source)
        {
            return OnFiltering(res, source);
        }
        //List<int> Original(List<int> peaks, float[] source)
        //{
        //    return peaks;
        //}
        //List<int> Relative_Height_Filter(List<int> peaks, float[] source)
        //{
        //    var res = peaks;
        //    if (res.Count <= 1)
        //        return res;
        //    for (int i = 0; i < res.Count - 1; i++)
        //    {
        //        float min = int.MaxValue;
        //        int pos = -1;
        //        for (int j = res[i]; j < source.Length && j <= res[i + 1]; j++)
        //        {
        //            if (source[j] < min)
        //            {
        //                pos = j;
        //                min = source[j];
        //            }
        //        }
        //        if (pos != -1)
        //        {
        //            float big_hill = source[res[i]] > source[res[i + 1]] ? source[res[i]] : source[res[i + 1]];
        //            float small_hill = source[res[i]] < source[res[i + 1]] ? source[res[i]] : source[res[i + 1]];
        //            int small_pos = source[res[i]] < source[res[i + 1]] ? i : i + 1;
        //            if (Math.Abs(small_hill - min) / Math.Abs(big_hill - min) < 0.15)
        //            {
        //                res.RemoveAt(small_pos);
        //                i--;
        //            }
        //        }
        //    }
        //    int peakssearch = res.Count;
        //    //   OnFiltering -= Relative_Height_Filter;
        //    return res;
        //}
        //List<int> Cross_LevelLine_Filter(List<int> peaks, float[] source)
        //{
        //    var res = peaks;
        //    if (res.Count <= 1)
        //        return res;
        //    for (int i = 1; i < res.Count - 1; i++)
        //    {
        //        int min = ( source[res[i - 1]] >= source[res[i + 1]] ) ? res[i + 1] : res[i - 1];
        //        int length1 = Math.Abs(res[i - 1] - res[i + 1]);
        //        float length2 = Math.Abs(source[res[i - 1]] - source[res[i + 1]]);
        //        int length3 = Math.Abs(res[i] - min);
        //        float resss = length2 * length3 / length1;
        //        if (source[res[i]] < source[min] + resss)
        //        {
        //            res.RemoveAt(i);
        //            i--;
        //        }
        //    }
        //    int peakssearch = res.Count;
        //    //    OnFiltering -= Cross_LevelLine_Filter;
        //    return res;
        //}
        List<int> Moveable_Window_Filter(List<int> peaks, float[] source)
        {
            var res = peaks;
            if (res.Count <= 1)
                return res;
            for (int i = 0; i < res.Count - 1; i++)
            {
                if (res[i + 1] - res[i] < e)
                {
                    if (source[res[i + 1]] >= source[res[i]])
                        res.RemoveAt(i);
                    else
                        res.RemoveAt(i + 1);
                    i--;
                }
            }
            for (int i = 0; i < res.Count; i++)
                if (source[res[i]] == 0)
                {
                    res.RemoveAt(i);
                }
            int peakssearch = res.Count;
            //  OnFiltering -= Moveable_Window_Filter;
            return res;
        }
        List<int> Tan_sign_Filter(List<int> peaks, float[] source)
        {
            var res = peaks;
            if (res.Count <4)
                return res;
            for (int i = 1; i < res.Count - 1; i++)
            {
                sbyte tan1 = (sbyte) ( ( ( source[res[i]] - source[res[i - 1]] ) / ( res[i] - res[i - 1] ) >= 0 ) ? 1 : -1 );
                sbyte tan2 = (sbyte) ( ( ( source[res[i + 1]] - source[res[i]] ) / ( res[i + 1] - res[i] ) >= 0 ) ? 1 : -1 );
                if (tan1 * tan2 >= 0)
                {
                    res.RemoveAt(i);
                    i--;
                }
                else
                {
                    if (tan1 <= 0 && tan2 >= 0)
                    {
                        res.RemoveAt(i);
                    }
                }
            }
            int peakssearch = res.Count;
            //   OnFiltering -= Tan_sign_Filter;
            return res;
        }
        List<int> Triangle_Limit_Filter(List<int> peaks, float[] source)
        {
            var res = peaks;
            if (res.Count <= 1)
                return res;
            for (int i = 0; i < res.Count; i++)
            {
                int h = (int) ( coef * source[res[i]] );
                int lpos = res[i] - h;
                int rpos = res[i] + h;
                for (int j = 0; j < res.Count; j++)
                {
                    int length;
                    if (res[j] < res[i] && res[j] > lpos)
                    {
                        length = res[j] - lpos;
                        if (source[res[j]] < length)
                        {
                            res.RemoveAt(j);
                            j--;
                            i--;
                        }
                    }
                    else
                    {
                        if (res[j] > res[i] && res[j] < rpos)
                        {
                            length = rpos - res[j];
                            if (source[res[j]] < length)
                            {
                                res.RemoveAt(j);
                                j--;
                            }
                        }
                    }

                }
            }
            int peakssearch = res.Count;
            //      OnFiltering -= Triangle_Limit_Filter;
            return res;
        }
        List<int> Pristavka_Filter(List<int> peaks, float[] source)
        {
            var res = peaks;
            if (res.Count ==1)// < 4)
                return res;
            bool flag = false;
            do
            {
                var tmpPeak = new List<int>();
                if (source[res[0]] > source[res[1]])
                {
                    tmpPeak.Add(res[0]);
                }
                if (source[res.Last()] > source[res[res.Count - 2]])
                {
                    tmpPeak.Add(res.Last());
                }
                for (int i = 1; i < res.Count - 1; i++)
                {
                    if (source[res[i]] >= source[res[i - 1]] && source[res[i]] >= source[res[i + 1]])
                        tmpPeak.Add(res[i]);
                }
                tmpPeak.Sort();
                res = tmpPeak;
                for (int i = 1; i < res.Count - 1; i++)
                {
                    bool cond1 = Math.Abs(res[i] - res[i - 1]) < 2.5 * C1;
                    bool cond2 = Math.Abs(res[i] - res[i + 1]) < 2.5 * C1;
                    if (cond1 && cond2)
                    {
                        flag = true;
                        break;
                    }
                    else
                        flag = false;
                }

            } while (flag && res.Count != 1);
            //  OnFiltering -= Pristavka_Filter;
            int peakssearch = res.Count;
            return res;
        }
        public float[] ArrForHollow(int left, int right, float[] source)
        {
            float[] res;
            if (right < left)
            {
                int tmp = left;
                left = right;
                right = tmp;
            }
            int length = right - left;
            res = new float[length];
            for (int i = 0; i < length; i++)
            {
                res[i] = source[left + i];
            }
            return res;
        }
        public void ClusterSearch(Image img, GetComponent colorComponent, List<Histogram_Part> clusters)
        {

            //    clustvol.Add(clusters[i].GetMap.Length);
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    if (img.GetMap[i, j])
                    {
                        float tmpcolor = colorComponent(img[i, j].GetColor);
                        for (int n = 0; n < clusters.Count; n++)
                        {
                            if (tmpcolor >= clusters[n].GetLeftBord && tmpcolor <= clusters[n].GetRightBord)
                            {
                                //clustvol[n]++;
                                clusters[n].GetMap[i, j] = true;
                            }
                            else
                                clusters[n].GetMap[i, j] = false;
                        }
                    }
                }
            }
            var clustvol = new List<int>();
            for (int i = 0; i < clusters.Count; i++)
                clustvol.Add(clusters[i].GetMap.Length);
                SmallClear(clusters, clustvol, img.GetLength());
        }
        void SmallClear(List<Histogram_Part> maps, List<int> clustv, int length)
        {
            for (int i = 0; i < maps.Count; i++)
            {
                if (( (double) clustv[i] / (double) length ) < 0.005)
                {
                    int tm = NearestCenter(i, maps);
                    bool[,] tmp = UniteMaps(maps[i].GetMap, maps[tm].GetMap);
                    int tmpl = ( tm < i ) ? tm : i;
                    int tmpr = ( tm >= i ) ? tm : i;
                    var load = new Histogram_Part(maps[tm].GetPos, maps[tmpl].GetLeftBord, maps[tmpr].GetRightBord, tmp);
                    maps.Add(load);
                    maps.RemoveAt(tm);
                    maps.RemoveAt(i);
                    clustv.Add(clustv[tm] + clustv[i]);
                    clustv.RemoveAt(tm);
                    clustv.RemoveAt(i);
                    if (maps.Count == 1)
                    {
                        break;
                    }
                    else
                    i = -1;
                }
            }
        }
        int NearestCenter(int indxsource, List<Histogram_Part> centers)
        {
            int dL;
            int dR;
            if (indxsource != 0)
                dL = Distance(centers[indxsource], centers[indxsource - 1]);
            else
                return indxsource + 1;
            if (indxsource != centers.Count - 1)
                dR = Distance(centers[indxsource], centers[indxsource + 1]);
            else
                return indxsource - 1;
            return ( dL < dR ) ? indxsource - 1 : indxsource + 1;
        }
        int Distance(Histogram_Part tmp, Histogram_Part t)
        {
            return Math.Abs(tmp.GetPos - t.GetPos);
        }

    }
}
