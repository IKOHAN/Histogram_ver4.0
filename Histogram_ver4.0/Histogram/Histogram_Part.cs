using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    class Histogram_Part: IComparable<Histogram_Part>
    {
        int position, leftdelta, rightdelta;
        Mask modeMap;
        public Histogram_Part()
        {

        }
        public Histogram_Part(int[] bound, int w,int h)
        {
            leftdelta = bound[0];
            position = bound[1];
            rightdelta = bound[2];
            modeMap = new Mask(w, h);
        }
        public Histogram_Part(int pos, int l_D, int r_D, bool[,] map)
        {
            position = pos;
            leftdelta = l_D;
            rightdelta = r_D;
            modeMap = new Mask(map);
        }
        public int GetPos
        {
            get => position;
        }
        public int GetLeftBord
        {
            get => leftdelta;
        }
        public int GetRightBord
        {
            get => rightdelta;
        }
        public Mask GetMap
        {
            get => modeMap;
            set => modeMap = value;
        }
        public int CompareTo(Histogram_Part other)
        {
            return position.CompareTo(other.position);
        }
    }
}
