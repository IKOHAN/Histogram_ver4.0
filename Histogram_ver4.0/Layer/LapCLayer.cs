using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    class LapCLayer:LapFlat,ICLayerState
    {
        delegate List<int> Filter(List<int> peaks, float[] source);
        event Filter OnFiltering;
        readonly double coef;
        int e;
        const int C1 = 5;
        public ICLayerState Create(double coef)
        {
            return new LapCLayer(coef);
        }
        public LapCLayer(double coef)
        {
            this.coef = coef;
            //OnFiltering += Original;
            //OnFiltering += Triangle_Limit_Filter;

            OnFiltering += Pristavka_Filter;
            OnFiltering += Moveable_Window_Filter;
            OnFiltering += Tan_sign_Filter;
        }
        public LapCLayer()
        {
        }
        public List<int[]> ClusterBorders(float[] source)
        {
            e = source.Length / 15;
            var res = new List<int[]>();
            List<int> Peaks = PeakSearch(source);
            if (Peaks.Count == 0)
                Peaks.Add(0);
            else
            Peaks = PeakDoppleClear(Peaks, source);
            if (Peaks.Count != 1)
            {
                int startbound = Peaks.Last() + HollowSearch(ArrForHollow(Peaks.Last(), Peaks.First(), source));
                int uroboros_startbound = startbound;
                int lastbound;
                for (int i = 0; i < Peaks.Count - 1; i++)
                {
                    lastbound = Peaks[i] + HollowSearch(ArrForHollow(Peaks[i], Peaks[i + 1], source));
                    res.Add(new int[] { startbound, Peaks[i], lastbound });
                    startbound = lastbound;
                }
                res.Add(new int[] { startbound, Peaks.Last(), uroboros_startbound });
            }
            else
                res.Add(new int[] { 0, Peaks.Last(), source.Length });
            return res;
        }
        public List<int> PeakSearch(float[] source)
        {
            return PeakPristavka(source);
        }
        List<int> PeakPristavka(float[] source)
        {
            var tmpPeak = new List<int>();
            for (int i = 1; i < source.Length; i++)
            {
                int trueposL = i - 1;
                int trueposR = ( i == source.Length - 1 ) ? 0 : i + 1;
                if (source[i] > source[trueposL] && source[i] > source[trueposR])
                    tmpPeak.Add(i);
            }
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
        //int Hill_climb(int pos, float[] source)
        //{
        //    int last_pos = pos;
        //    int current_pos = pos;
        //    double current_value = source[pos];
        //    bool flag_ = true;
        //    for (int i = current_pos - e; i <= current_pos + e; i++)
        //    {
        //        int truepos = ( i < 0 ) ? source.Length + i : ( i > source.Length - 1 ) ? i - source.Length : i;
        //        if (source[truepos] >= current_value)
        //        {
        //            flag_ = false;
        //            if (source[truepos] > current_value)
        //            {
        //                current_pos = truepos;
        //                current_value = source[truepos];
        //            }
        //        }
        //    }
        //    if (flag_ && current_pos == last_pos)
        //        return -1;
        //    return current_pos;
        //}
        public List<int> PeakDoppleClear(List<int> res, float[] source)
        {
            for (int i = 0; i < res.Count; i++)
                if (source[res[i]] == 0)
                {
                    res.RemoveAt(i);
                }
            return OnFiltering(res, source);
        }
        //List<int> Original(List<int> peaks, float[] source)
        //{
        //    return peaks;
        //}
        //List<int> Relative_Height_Filter(List<int> peaks, float[] source)
        //{
        //    var res = peaks;
        //    if (res.Count < 3)
        //        return res;
        //    for (int i = 0; i < res.Count; i++)
        //    {
        //        float min = int.MaxValue;
        //        int pos = -1;
        //        int left = res[i];
        //        int right = ( i == res.Count - 1 ) ? res.First() : res[i + 1];
        //        int pole = Math.Abs(source.Length - left + right);
        //        int woutpole = Math.Abs(right - left);
        //        int truelen = ( pole < woutpole ) ? pole : woutpole;
        //        for (int j = res[i]; j <= res[i] + truelen; j++)
        //        {
        //            int truepos = ( j > source.Length - 1 ) ? j - source.Length : j;
        //            if (source[truepos] < min)
        //            {
        //                pos = truepos;
        //                min = source[truepos];
        //            }
        //        }
        //        if (pos != -1)
        //        {
        //            int righthill = ( i == res.Count - 1 ) ? res.First() : res[i + 1];
        //            float big_hill = source[res[i]] > source[righthill] ? source[res[i]] : source[righthill];
        //            float small_hill = source[res[i]] < source[righthill] ? source[res[i]] : source[righthill];
        //            int small_pos = source[res[i]] < source[righthill] ? i : ( i == res.Count - 1 ) ? 0 : i + 1;
        //            if (Math.Abs(small_hill - min) / Math.Abs(big_hill - min) < 0.15)
        //            {
        //                res.RemoveAt(small_pos);
        //                i--;
        //            }
        //        }
        //    }
        //    //   OnFiltering -= Relative_Height_Filter;
        //    return res;
        //}
        //List<int> Cross_LevelLine_Filter(List<int> peaks, float[] source)
        //{
        //    var res = peaks;
        //    if (res.Count < 3)
        //        return res;
        //    for (int i = 1; i < res.Count; i++)
        //    {
        //        int left = res[i - 1];
        //        int right = ( i == res.Count - 1 ) ? res[0] : res[i + 1];
        //        int length1, length3;
        //        float length2;
        //        if (right < left)
        //        {
        //            length1 = source.Length - left + right;
        //            length2 = source[left] - source[right];
        //            if (left < res[i])
        //                length3 = res[i] - left;
        //            else
        //                length3 = source.Length - left + res[i];
        //        }
        //        else
        //        {
        //            length1 = right - left;
        //            length2 = source[left] - source[right];
        //            length3 = res[i] - left;
        //        }
        //        float resss = length2 * length3 / length1;
        //        if (source[res[i]] < source[left] + resss)
        //        {
        //            res.RemoveAt(i);
        //            i--;
        //        }
        //    }
        //    //   OnFiltering -= Cross_LevelLine_Filter;
        //    return res;
        //}
        List<int> Moveable_Window_Filter(List<int> peaks, float[] source)
        {
            var res = peaks;
            if (res.Count < 2)
                return res;
            for (int i = 0; i < res.Count; i++)
            {
                int left = res[i];
                int rightpos = ( i == res.Count - 1 ) ? 0 : i + 1;
                int right = res[rightpos];
                int length1;
                if (right < left)
                    length1 = source.Length - left + right;
                else
                    length1 = right - left;
                if (length1 < e)
                {
                    if (source[right] >= source[left])
                        res.RemoveAt(i);
                    else
                        res.RemoveAt(rightpos);
                    i--;
                }
            }
            // OnFiltering -= Moveable_Window_Filter;
            return res;
        }
        List<int> Tan_sign_Filter(List<int> peaks, float[] source)
        {
            var res = peaks;
            if (res.Count < 4)
                return res;
            for (int i = 1; i < res.Count; i++)
            {
                int left = res[i - 1];
                int center = res[i];
                int right = ( i == res.Count - 1 ) ? res[0] : res[i + 1];
                int xdiff1 = 0, xdiff2 = 0;
                float ydiff1, ydiff2;
                if (left > center && center < right)
                {
                    xdiff1 = source.Length - left + center;
                    xdiff2 = right - center;
                }
                else
                {
                    if (left < center && center > right)
                    {
                        xdiff1 = center - left;
                        xdiff2 = source.Length - center + right;
                    }
                    if (left < center && center < right)
                    {
                        xdiff1 = center - left;
                        xdiff2 = right - center;
                    }
                }
                ydiff1 = source[center] - source[left];
                ydiff2 = source[right] - source[center];
                try
                {
                    sbyte tan1 = (sbyte) ( ( ydiff1 / xdiff1 >= 0 ) ? 1 : -1 );
                    sbyte tan2 = (sbyte) ( ( ydiff2 / xdiff2 >= 0 ) ? 1 : -1 );
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
                catch (Exception)
                {
                    throw new Exception("Unbelievable situation.");
                }

            }
            //    OnFiltering -= Tan_sign_Filter;
            return res;
        }
        //List<int> Triangle_Limit_Filter(List<int> peaks, float[] source)
        //{
        //    var res = peaks;
        //    if (res.Count == 1)
        //        return res;
        //    for (int i = 0; i < res.Count; i++)
        //    {
        //        int h = (int) ( coef * source[res[i]] );
        //        int lpos = res[i] - h;
        //        lpos += ( lpos < 0 ) ? source.Length : 0;
        //        int rpos = res[i] + h;
        //        rpos -= ( rpos > source.Length ) ? source.Length : 0;
        //        int length;
        //        if (lpos < res[i] && res[i] < rpos)
        //        {
        //            for (int j = 0; j < res.Count; j++)
        //            {
        //                if (lpos < res[j] && res[j] < res[i])
        //                {
        //                    length = res[j] - lpos;
        //                    if (source[res[j]] < length)
        //                    {
        //                        res.RemoveAt(j);
        //                        i -= 2;
        //                        break;
        //                    }
        //                }
        //                else
        //                {
        //                    if (res[i] < res[j] && res[j] < rpos)
        //                    {
        //                        length = rpos - res[j];
        //                        if (source[res[j]] < length)
        //                        {
        //                            res.RemoveAt(j);
        //                            i--;
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (lpos < res[i] && res[i] > rpos)
        //            {
        //                for (int j = 0; j < res.Count; j++)
        //                {
        //                    if (lpos < res[j] && res[j] < res[i])
        //                    {
        //                        length = res[j] - lpos;
        //                        if (source[res[j]] < length)
        //                        {
        //                            res.RemoveAt(j);
        //                            i -= 2;
        //                            break;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (res[i] < res[j] && res[j] < source.Length)
        //                        {
        //                            length = source.Length - res[j] + rpos;
        //                            if (source[res[j]] < length)
        //                            {
        //                                res.RemoveAt(j);
        //                                i--;
        //                                break;
        //                            }
        //                        }
        //                        if (0 <= res[j] && res[j] < rpos)
        //                        {
        //                            length = rpos - res[j];
        //                            if (source[res[j]] < length)
        //                            {
        //                                res.RemoveAt(j);
        //                                i -= 2;
        //                                break;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (lpos > res[i] && res[i] < rpos)
        //                {
        //                    for (int j = 0; j < res.Count; j++)
        //                    {
        //                        if (res[i] < res[j] && res[j] < rpos)
        //                        {
        //                            length = rpos - res[j];
        //                            if (source[res[j]] < length)
        //                            {
        //                                res.RemoveAt(j);
        //                                i--;
        //                                break;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (lpos < res[j] && res[j] < source.Length)
        //                            {
        //                                length = res[j] - lpos;
        //                                if (source[res[j]] < length)
        //                                {
        //                                    res.RemoveAt(j);
        //                                    i--;
        //                                    break;
        //                                }
        //                            }
        //                            if (0 <= res[j] && res[j] < res[i])
        //                            {
        //                                length = source.Length - lpos + res[j];
        //                                if (source[res[j]] < length)
        //                                {
        //                                    res.RemoveAt(j);
        //                                    i -= 2;
        //                                    break;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    //    OnFiltering -= Triangle_Limit_Filter;
        //    return res;
        //}
        List<int> Pristavka_Filter(List<int> peaks, float[] source)////////////////////////////////////////////////////////////
        {
            var res = peaks;
            if (res.Count < 4)
                return res;
            bool flag = false;
            do
            {
                var tmpPeak = new List<int>();
                for (int i = 1; i < res.Count; i++)
                {
                    int left = res[i - 1];
                    int right = ( i == res.Count - 1 ) ? res[0] : res[i + 1];
                    if (source[res[i]] > source[left] && source[res[i]] > source[right])
                        tmpPeak.Add(res[i]);
                }
                tmpPeak.Sort();
                res = tmpPeak;
                for (int i = 1; i < res.Count; i++)
                {
                    int left = res[i - 1];
                    int right = ( i == res.Count - 1 ) ? res[0] : res[i + 1];
                    int lengthl = res[i] - left;
                    int lengthr = ( right < res[i] ) ? source.Length - res[i] + right : right - res[i];
                    bool cond1 = Math.Abs(lengthl) < 2.5 * C1;
                    bool cond2 = Math.Abs(lengthr) < 2.5 * C1;
                    if (cond1 && cond2)
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            } while (flag && res.Count > 2);
            //   OnFiltering -= Pristavka_Filter;
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
            int length = ( ( source.Length - right + left ) < ( right - left ) ) ? source.Length - right + left + 1 : right - left + 1;
            res = new float[length];
            if (( source.Length - right + left ) < ( right - left ))
                for (int i = 0; i < length; i++)
                {
                    res[i] = source[( right + i ) % source.Length];
                }
            else
                for (int i = 0; i < length; i++)
                {
                    res[i] = source[( left + i ) % source.Length];
                }
            return res;
        }
        public void ClusterSearch(Image img, GetComponent colorComponent, List<Histogram_Part> clusters)
        {
            //    clustvol.Add(0);
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    if (img.GetMap.GetMask[i,j])
                    {
                        float tmpcolor = colorComponent(img[i,j].GetColor);
                        for (int n = 0; n < clusters.Count; n++)
                        {
                            int min = clusters[n].GetLeftBord;
                            int max = clusters[n].GetRightBord;
                            bool condition;
                            if (max > 0 && max < min)
                                condition = ( tmpcolor >= min || tmpcolor <= max );
                            else
                                condition = ( tmpcolor >= min && tmpcolor <= max );
                            if (condition)
                            {
                                //clustvol[n]++;
                                clusters[n].GetMap.GetMask[i,j] = true;
                            }
                            else
                                clusters[n].GetMap.GetMask[i,j] = false;
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
                    maps.RemoveAt(tmpr);
                    maps.RemoveAt(tmpl);
                    clustv.Add(clustv[tm] + clustv[i]);
                    clustv.RemoveAt(tmpr);
                    clustv.RemoveAt(tmpl);
                    i = 0;
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
                dL = Distance(centers.Last(), centers[indxsource], 360);
            if (indxsource != centers.Count - 1)
                dR = Distance(centers[indxsource], centers[indxsource + 1]);
            else
                dR = Distance(centers[indxsource], centers.First(), 360);
            if (indxsource == 0)
                return ( dL < dR ) ? centers.Count - 1 : 1;
            if (indxsource == centers.Count - 1)
                return ( dL < dR ) ? centers.Count - 2 : 0;
            return ( dL < dR ) ? indxsource - 1 : indxsource + 1;
        }
        int Distance(Histogram_Part tmp, Histogram_Part t)
        {
            return Math.Abs(tmp.GetPos - t.GetPos);
        }
        int Distance(Histogram_Part tmp, Histogram_Part t, int range)
        {
            return ( range - tmp.GetPos + t.GetPos < tmp.GetPos - t.GetPos ) ? range - tmp.GetPos + t.GetPos : tmp.GetPos - t.GetPos;
        }
    }
}
