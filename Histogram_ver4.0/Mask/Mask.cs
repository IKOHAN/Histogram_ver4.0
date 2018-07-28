using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Histogram_ver4._0
{
    class Mask
    {
        Point widthBound;
        Point heightBound;
        readonly int width;
        readonly int height;
        bool[,] map;
        public Mask()
        {

        }
        public Mask(bool[,] maska)
        {
            height = maska.GetLength(0);
            width = maska.GetLength(1);

            widthBound = new Point(0, width);
            heightBound = new Point(0, height);

            map = maska;
        }
        public Mask(int w, int h, bool inv = true)
        {
            width = w;
            height = h;
            widthBound = new Point(0, width);
            heightBound = new Point(0, height);
            map = new bool[h, w];
            for (int i = 0; i < map.Length; i++)
            {
                int r = i / width;
                int c = i % width;
                map[r, c] = inv;
            }
        }
        public Mask(bool[,] mask, Point wBound, Point hBound)
        {
            map = mask;
            height = mask.GetLength(0);
            width = mask.GetLength(1);
            widthBound = wBound;
            heightBound = hBound;
        }
        public Mask(int w, int h, Point wBound, Point hBound, bool inv = true)
        {
            map = new bool[h, w];
            width = w;
            height = h;
            widthBound = wBound;
            heightBound = hBound;
            for (int i = 0; i < map.Length; i++)
            {
                int r = i / width;
                int c = i % width;
                map[r, c] = inv;
            }
        }

        int MaskSize()
        {
            int res=0;
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (map[i, j])
                        res++;
            return res;
        }
        public bool this[int i]
        {
            get => map[i / width, i % width];
        }
        public bool this[int row,int column]
        {
            get => map[row, column];
            set => map[row, column] = value;
        }
        public bool[,] GetMask
        {
            get=> map;
            set => map = value;
        }
        public int Width
        {
            get => width;
        }
        public int Height
        {
            get => height;
        }
        public int Length
        {
            get => MaskSize();
        }
        public Point GetHBound
        {
            get => heightBound;
            set => heightBound = value;
        }
        public Point GetWBound
        {
            get => widthBound;
            set => widthBound = value;
        }
    }
}
