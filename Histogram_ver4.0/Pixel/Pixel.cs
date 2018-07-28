using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Histogram_ver4._0
{
    class Pixel
    {
        Point position;
        IColor color;
        public Pixel()
        {
        }
        public Pixel(Point pos, IColor col)
        {
            position = pos;
            color = col;
        }
        //public Pixel(Point pos, IColor state, params float[] value)
        //{
        //    position = pos;
        //    color = state.Create(value);
        //}
        public Pixel GetNegate()
        {
            return new Pixel(position, color.Negate(color));
        }
        public Point GetPos
        {
            get => position;
        }
        public IColor GetColor
        {
            get => color;
        }
    }
}
