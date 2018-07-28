using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    interface ICLayerState
    {
        List<int[]> ClusterBorders(float[] source);
        List<int> PeakSearch(float[] source);
        List<int> PeakDoppleClear(List<int> res, float[] source);
        float[] ArrForHollow(int left, int right, float[] source);
        void ClusterSearch(Image img, GetComponent colorComponent, List<Histogram_Part> clusters);
    }
    abstract class LapFlat
    {
        protected Random rnd = new Random();
        /// <summary>
        /// На вход подать массив с пиками на концах
        /// Ищется порог для этих пиков
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected int HollowSearch(float[] source)
        {
            #region
            //int start = 0;
            //int finish = source.Length-1;
            //int length = ( finish - start ) / 2;
            //int res = ( finish - start ) / 2;
            //for (int i = 0; i < length; i++)
            //{
            //    if (source[start + i] > source[finish - i])
            //    {
            //        res++;
            //        continue;
            //    }
            //    if (source[start + i] < source[finish - i])
            //    {
            //        res--;
            //        continue;
            //    }
            //    if (source[start + i] == source[finish - i])
            //        continue;
            //}
            //return res;

            //if (min == 0)
            //    otherBound = HollowBound(source,min, source.Length-1, -1);
            //else
            //    otherBound = HollowBound(source, min, 0, 1);
            //int start = ( min < otherBound ) ? min : otherBound;
            //int finish = ( min > otherBound ) ? min : otherBound;
            //int res = start + Minimum_pos(source, start, finish);
            //return res;


            #endregion
            int minpos = ( source.First() >= source.Last() ) ? source.Length - 1 : 0;
            var leftbound = Leftboundsearch(source, source[minpos]);
            var rightbound = Rightboundsearch(source, source[minpos]);
            if (rightbound[0] < leftbound[0])
                rightbound.RemoveAt(0);
            if (leftbound.Last() > rightbound.Last())
                leftbound.RemoveAt(leftbound.Count - 1);
            int start = 0;
            int finish = 0;
            int delta = 0;
            for (int i = 0; i < rightbound.Count && i < leftbound.Count; i++)
            {
                if (rightbound[i] - leftbound[i] > delta)
                {
                    delta = rightbound[i] - leftbound[i];
                    start = leftbound[i];
                    finish = rightbound[i];
                }
            }
            return Minimum_pos(source, start, finish);
        }
        List<int> Leftboundsearch(float[] source, float min)
        {
            var res = new List<int>();
            for (int i = 0; i < source.Length - 1; i++)
            {
                if (source[i] >= min && source[i + 1] <= min)
                    res.Add(i);
            }
            if (res.Count == 0)
                res.Add(( source.First() >= source.Last() ) ? source.Length - 1 : 0);

            return res;
        }
        List<int> Rightboundsearch(float[] source, float min)
        {
            var res = new List<int>();
            for (int i = 0; i < source.Length - 1; i++)
            {
                if (source[i] <= min && source[i + 1] >= min)
                    res.Add(i);
            }
            if (res.Count == 0)
                res.Add(( source.First() >= source.Last() ) ? source.Length - 1 : 0);
            return res;

        }
        //List<int> Leftboundsearch(float[] source, float min, int start, int direct)
        //{
        //    var res = new List<int>();
        //    for (int i = start + direct; i < source.Length - 1 && i > 0; i += direct)
        //    {
        //        if (source[i] >= min && source[i + direct] <= min)
        //            res.Add(i);
        //    }
        //    if (res.Count == 0)
        //        res.Add(start);

        //    return res;
        //}
        //List<int> Rightboundsearch(float[] source, float min, int start, int direct)
        //{
        //    var res = new List<int>();
        //    for (int i = start + direct; i < source.Length - 1 && i > 0; i += direct)
        //    {
        //        if (source[i] <= min && source[i + direct] >= min)
        //            res.Add(i);
        //    }
        //    if (res.Count == 0)
        //        res.Add(Math.Abs(source.Length - 1 - start));
        //    return res;
        //}
        //int HollowBound(float[] source, int target, int start, int direct)
        //{
        //    int minindx = start + direct;
        //    for (int i = start; i == target - direct; i += direct)
        //    {
        //        if (source[i] >= source[target] && source[i + direct] <= source[target])
        //            minindx = i;
        //    }
        //    return minindx;
        //}
        int Minimum_pos(float[] source, int start, int finish)
        {
            int length = ( finish - start ) / 2;
            int res = ( finish - start ) / 2;
            for (int i = 0; i < length; i++)
            {
                if (source[start + i] > source[finish - i])
                {
                    res++;
                    continue;
                }
                if (source[start + i] < source[finish - i])
                {
                    res--;
                    continue;
                }
                if (source[start + i] == source[finish - i])
                    continue;
            }
            return res;
        }
       protected bool[,] UniteMaps(Mask map1, Mask map2)
        {
            var res = new bool[map1.Height, map1.Width];
            for (int i = 0; i < map1.Height; i++)
                for (int j = 0; j < map1.Width; j++)
                    res[i, j] = map1[i, j] || map2[i, j];
            return res;
        }
    }
}
