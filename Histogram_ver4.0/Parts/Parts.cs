using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Histogram_ver4._0
{
    class Parts
    {
        readonly int row, column;
        List<Image_Project> parts;
        public Parts()
        {
        }
        public Parts(Image_Project img, int r, int c)
        {
            parts = new List<Image_Project>(r * c);
            row = r;
            column = c;
            var imginparts = GetImgParts(img);
            for (int i = 0; i < imginparts.Count; i++)
            {
                parts.Add(new Image_Project(imginparts[i], img.GetState));
            }
        }
        List<Image> GetImgParts(Image_Project img)
        {
            var img_res = new List<Image>(row * column);
            int length = img.GetImage.GetLength();
            int width = img.GetImage.Width,
                heigth = img.GetImage.Height,
                partwidth = width / column,
                partheight = heigth / row,
                restwidth = width % column,
                restheight = heigth % row;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    int w = partwidth + ( ( j + 1 ) / column == 1 ? restwidth : 0 );
                    int h = partheight + ( (i+1) / row == 1 ? restheight : 0 );
                    var tmp = new Mask(w, h, new Point(j * partwidth, j * partwidth + w), new Point(i * partheight, i * partheight + h), true);
                    img_res.Add(new Image(img.GetImage.GetState,tmp, w, h));
                }
            }
            for (int i = 0; i < row; i++)
            {
                int tmp_height = ( i < row - 1 ) ? partheight : partheight + restheight;
                try
                {
                    for (int o = 0; o < tmp_height; o++)
                    {
                        try
                        {
                            for (int j = 0; j < column; j++)
                            {
                                int tmp_width = ( j < column - 1 ) ? partwidth : partwidth + restwidth;
                                try
                                {
                                    for (int n = 0; n < tmp_width; n++)
                                    {
                                        int startx = img_res[i * column + j].GetMap.GetWBound.X;
                                        int starty = img_res[i*column+j].GetMap.GetHBound.X;
                                        img_res[i * column + j][o , n] = img.GetImage[ starty + o, startx + n];
                                    }
                                }
                                catch (Exception)
                                {
                                    throw new Exception("n out");
                                }
                            }
                        }
                        catch (Exception)
                        {
                            throw new Exception("j out");
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("o out");
                }
            }
            return img_res;
        }
        //public List<Image_Project> GetParts
        //{
        //    get => parts;
        //}
        public Image_Project this[int r, int c]
        {
            get {
#if DEBUG
                Ok(r, c);
#endif
                return parts[r * column + c];
            }
        }
//        public Image_Project this[int i]
//        {
//            get {
//#if DEBUG
//                Ok(i);
//#endif
//                return parts[i];
//            }
//        }
        public int Column
        {
            get => column;
        }
        public int Row
        {
            get => row;
        }
        //public bool IsEmpty()
        //{
        //    if (parts.Count > 0)
        //        return false;
        //    else
        //        return true;
        //}
        //public int GetLength()
        //{
        //    return parts.Count;
        //}
        //public void Clear()
        //{
        //    parts.Clear();
        //}
        bool Ok(int x)
        {
            if (x < 0 || x >= column * row)
                throw new IndexOutOfRangeException(string.Format("Trying to access pixel {0} in {1} parts", x, row * column));
            return true;
        }
        bool Ok(int r, int c)
        {
            if (r < 0 || r >= row || c < 0 || c >= column)
                throw new IndexOutOfRangeException(string.Format("Trying to access pixel ({0}, {1}) in {2}x{3} parts", r, c, column, row));
            return true;
        }
    }
}
