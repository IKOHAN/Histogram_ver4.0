﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    class RGBRange : IRange
    {
        public int GetFirst
        {
            get => 256;
        }
        public int GetSecond
        {
            get => 256;
        }
        public int GetThird
        {
            get => 256;
        }
    }
}