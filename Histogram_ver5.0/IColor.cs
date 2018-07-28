using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver5._0
{
    abstract class ColorBase
    {
        protected float v1, v2, v3;
        public ColorBase()
        {
            v1 = -1;
            v2 = -1;
            v3 = -1;
        }
        public ColorBase(params float[] value)
        {
            try
            {
                if (ValueChecker(value[0]))
                    v1 = value[0];
                if (ValueChecker(value[1]))
                    v2 = value[1];
                if (ValueChecker(value[2]))
                    v3 = value[2];
            }
            catch (IndexOutOfRangeException) { throw new Exception("Out of Range/Not enough params."); }
            catch (Exception) { throw new Exception("Unknown error."); }
        }
        protected virtual bool ValueChecker(float value)
        {
            if (value >= 0 && value <= 1)
                return true;
            else
                throw new Exception("Incorrect color value in ColorBase class.");
        }
        public abstract float GetVal1 { get; }
        public abstract float GetVal2 { get; }
        public abstract float GetVal3 { get; }
    }
}
