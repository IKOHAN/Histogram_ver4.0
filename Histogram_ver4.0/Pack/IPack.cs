using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Histogram_ver4._0
{
    delegate double Distance(IColor c1, IColor c2, GetNorma norma);
    delegate bool[] Stripe(Mask map);
    interface IPack
    {
        IPack Create(Image_Project img);
        IPack Create(Image_Project img, Parts parts);
        List<IStats> GetClusters
        {
            get;
        }
        IStats this[int x]
        {
            get;
        }
    }
    abstract class Pack
    {
        protected int NearestCenter(int indxsource, List<IColor> centers, Distance distance)
        {
            double d = float.MaxValue;
            int indx = 0;
            for (int i = 0; i < centers.Count; i++)
            {
                if (i != indxsource)
                {
                    double dtmp = distance(centers[indxsource], centers[i], Brightness);
                    if (dtmp < d)
                    {
                        indx = i;
                        d = dtmp;
                    }
                }
            }
            return indx;
        }
        #region
        protected double DN(IColor c1, IColor c2, GetNorma norma)
        {
            if (c1 is RGB && c2 is RGB)
            {
                return Math.Abs(norma(c1) - norma(c2));
            }
            else
                if (c1 is HSV && c2 is HSV)
            {
                return Math.Abs(norma(c1) - norma(c2));
            }
            else
                throw new IncorrectSchemeInput();
        }
        protected double ND(IColor c1, IColor c2, GetNorma norma)
        {
            if (c1 is RGB && c2 is RGB)
            {
                return Math.Abs(norma(( c1 as RGB ) - c2));
            }
            else
                if (c1 is HSV && c2 is HSV)
            {
                return Math.Abs(norma(( c1 as HSV ) - c2));
            }
            else
                throw new IncorrectSchemeInput();
        }
        protected double Brightness(IColor color)
        {
            if (color is RGB)
                return 0.2126f * color.GetVal1 + 0.7152f * color.GetVal2 + 0.0722f * color.GetVal3;
            else
               if (color is HSV)
                return color.GetVal3;
            else
                throw new IncorrectSchemeInput();
        }
        protected double Manhhatten(IColor color)
        {
            if (color is RGB)
                return ( color.GetVal1 + color.GetVal2 + color.GetVal3 ) / 3d;
            else
                if (color is HSV)
                return ( color.GetVal1 / 3.6 + color.GetVal2 + color.GetVal3 ) / 3d;
            else
                throw new IncorrectSchemeInput();
        }
        protected double Evcklid(IColor color)
        {
            if (color is RGB)
                return Math.Sqrt(( color.GetVal1 * color.GetVal1 + color.GetVal2 * color.GetVal2 + color.GetVal3 * color.GetVal3 ) / 3d);
            else
                if (color is HSV)
                return Math.Sqrt(( ( color.GetVal1 / 3.6 ) * ( color.GetVal1 / 3.6 ) + color.GetVal2 * color.GetVal2 + color.GetVal3 * color.GetVal3 ) / 3d);
            else
                throw new IncorrectSchemeInput();
        }
        protected double Maximum(IColor color)
        {
            if (color is RGB)
                return ( color.GetVal1 > color.GetVal2 ) ? ( ( color.GetVal1 > color.GetVal3 ) ? color.GetVal1 : color.GetVal3 ) : ( ( color.GetVal2 > color.GetVal3 ) ? color.GetVal2 : color.GetVal3 );
            else
                if (color is HSV)
                return ( color.GetVal1 / 3.6 > color.GetVal2 ) ? ( ( color.GetVal1 / 3.6 > color.GetVal3 ) ? color.GetVal1 / 3.6 : color.GetVal3 ) : ( ( color.GetVal2 > color.GetVal3 ) ? color.GetVal2 : color.GetVal3 );
            else
                throw new IncorrectSchemeInput();
        }
        #endregion


        protected bool[] UpStripe(Mask map)
        {
            int w = map.GetWBound.Y - map.GetWBound.X;
            bool[] res = new bool[w];
            int indxstartH = map.GetHBound.X;
            for (int j = map.GetWBound.X; j < map.GetWBound.Y; j++)
            {
                res[j] = map[indxstartH, j];
            }
            return res;
        }
        protected bool[] LeftStripe(Mask map)
        {
            int h = map.GetHBound.Y - map.GetHBound.X;
            bool[] res = new bool[h];
            int wStart = map.GetWBound.Y;
            for (int i = map.GetHBound.X; i < map.GetHBound.Y; i++)
                res[i] = map[i, wStart];
            return res;
        }
        protected bool[] DownStripe(Mask map)
        {
            int w = map.GetWBound.Y - map.GetWBound.X;
            bool[] res = new bool[w];
            int hStart = map.GetHBound.Y;
            for (int i = map.GetWBound.X; i < map.GetWBound.Y; i++)
                res[i] = map[hStart, i];
            return res;
        }
        protected bool[] RightStripe(Mask map)
        {
            int h = map.GetHBound.Y - map.GetHBound.X;
            bool[] res = new bool[h];
            int wStart = map.GetWBound.X;
            for (int i = map.GetHBound.X; i < map.GetHBound.Y; i++)
                res[i] = map[i,wStart];
            return res;
        }
        protected Mask Parttoentire(Image part, Mask map, int w, int h)
        {
            var res = new Mask(w,h,map.GetWBound,map.GetHBound,false);
            for (int i = map.GetHBound.X; i < map.GetHBound.Y; i++)
            {
                for (int j = map.GetWBound.X; j < map.GetWBound.Y; j++)
                {
                    if (map[i, j])
                    {
                        res.GetMask[i, j] = true;
                    }
                }
            }           
            return res;
        }
        protected bool Unite(Mask map1, Mask map2)
        {

            for (int i = 0; i < map1.Length; i++)
            {
                //  bool m2l = ( i == 0 ) ? false : map2[i - 1];
                // bool m2c = map2[i];
                // bool m2r = ( i == map2.Length - 1 ) ? false : map2[i + 1];
                if (map1[i] && map2[i])// ( m2c || m2l || m2r ))
                    return true;
            }
            return false;
        }
        protected Mask UniteMaps(Mask map1, Mask map2)
        {
            int wstart = ( map1.GetWBound.X > map2.GetWBound.X ) ? map2.GetWBound.X : map1.GetWBound.X;
            int wfin = ( map1.GetWBound.Y > map2.GetWBound.Y ) ? map1.GetWBound.Y : map2.GetWBound.Y;
            int hstart = ( map1.GetHBound.X > map2.GetHBound.X ) ? map2.GetHBound.X : map1.GetHBound.X;
            int hfin = ( map1.GetHBound.Y > map2.GetHBound.Y ) ? map1.GetHBound.Y : map2.GetHBound.Y;
            var res = new bool[map1.Height,map1.Width];
            for (int i = 0; i < map1.Height; i++)
            {
                for(int j = 0;j <map1.Width;j++)
                res[i,j] = map1[i,j] || map2[i,j];
            }
            return new Mask(res,new Point(wstart,wfin),new Point(hstart,hfin));
        }
        protected bool[] UniteMaps(params bool[][] maps)
        {
            var res = new bool[maps[0].Length];
            for (int i = 0; i < maps[0].Length; i++)
            {
                for (int j = 0; j < maps.Length; j++)
                {
                    if (maps[j][i])
                    {
                        res[i] = true;
                        break;
                    }
                }
            }
            return res;
        }
        protected Mask UniteMaps(IList<Mask> maps)
        {
            var res = new bool[maps[0].Height,maps[0].Width];
            for (int i = 0; i < maps[0].Height; i++)
            {
                for (int j = 0; j < maps[0].Width; j++)
                {
                    for (int n = 0; n < maps.Count; n++)
                    {
                        if (maps[n][i,j])
                        {
                            res[i,j] = true;
                            break;
                        }
                    }
                }
            }
            return new Mask(res);
        }
        protected bool MapsCrossing(Mask map1, Mask map2)
        {
            int wstart = ( map1.GetWBound.X > map2.GetWBound.X ) ? map2.GetWBound.X : map1.GetWBound.X;
            int wfin = ( map1.GetWBound.Y > map2.GetWBound.Y ) ? map1.GetWBound.Y : map2.GetWBound.Y;
            int hstart = ( map1.GetHBound.X > map2.GetHBound.X ) ? map2.GetHBound.X : map1.GetHBound.X;
            int hfin = ( map1.GetHBound.Y > map2.GetHBound.Y ) ? map1.GetHBound.Y : map2.GetHBound.Y;
            for (int i = hstart; i < hfin; i++)
            {
                for (int j = wstart; j < wfin; j++)
                {
                    if (map1[i, j] && map2[i, j])
                        return true;
                }
            }            
            return false;
        }
    }
}
